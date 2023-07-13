using System;
using System.Collections.Generic;
using Path = System.IO.Path;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace RecordPro
{
    /// <summary>
    /// Interaction logic for ConfigureBackup2.xaml
    /// </summary>
    public partial class ConfigureBackup2 : Page
    {
        string customLocation;
        string _hour;
        string _frequency;
        public ConfigureBackup2(string frequency, string hour)
        {
            _hour = hour;
            _frequency = frequency;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CreateBackup())
            {
                TaskDialog.ShowDialog("Backup successful", "Automatic backup has been set up.",
                    "If something happens, Record Pro will prompt you to restore your data.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Information);
                foreach (var window in Application.Current.Windows)
                {
                    if (window is NavigationWindow nWindow)
                    {
                        nWindow.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to create the backup directory and returns whether or not the operation succeeded
        /// </summary>
        /// <returns>True if the operation succeeded. Otherwise, false.</returns>
        private bool CreateBackup()
        {
            string backupLocation;

            if ((string)location.SelectedItem == "Custom")
            {
                backupLocation = Path.Combine(customLocation, "Autosoft",
                    "Record Pro", "2018", "Backups");
            }
            else if ((string)location.SelectedItem == "This PC")
            {
                backupLocation = Path.Combine(Environment.GetFolderPath(
       Environment.SpecialFolder.CommonApplicationData), "Autosoft",
       "Record Pro", "2018", "Backups");
            }
            else
            {
                backupLocation = Path.Combine((string)location.SelectedItem,
                   "Autosoft", "Record Pro", "2018", "Backups");
            }

            try
            {
                Directory.CreateDirectory(backupLocation);
                using (var registryKey = Registry.CurrentUser.CreateSubKey(Application.RegistryLocation))
                {
                    registryKey.SetValue("Backup Frequency", _frequency);
                    registryKey.SetValue("Backup Hour", _hour);
                    registryKey.SetValue("Backup Location", backupLocation);
                    registryKey.SetValue("Backup Enabled", "True");
                }
                return true; // Everything's okay if we reached this point
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "Backup was not set up successfully",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "Backup was not set up successfully",
                   "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return false; // The operation failed
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var drives = new List<string>(Directory.GetLogicalDrives());
                drives.Remove("C:\\");
                drives.Add("This PC");
                drives.Add("Custom");
                location.ItemsSource = drives;
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "The list of drives couldn't be loaded.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                this.NavigationService.GoBack();
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "The list of drives couldn't be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                this.NavigationService.GoBack();
            }
        }

              private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newDialog = new System.Windows.Forms.FolderBrowserDialog()
            {
                Description = "Please select where to store backups."
            };
            if (newDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                customLocation = newDialog.SelectedPath;
                fileLocation.Content = customLocation;
            }

        }
    }
}
