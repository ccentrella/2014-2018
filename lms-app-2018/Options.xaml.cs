
using System;
using System.IO;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using IO = System.IO;
using System.Runtime;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

namespace RecordPro
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class Options : Page
    {
        delegate void mainDelegate(); // Used for asynchronous operations
        OpenFileDialog openFileDialog1 = new OpenFileDialog()
        {
            Title = "Upload Image - Record Pro",
            ValidateNames = true,
            CheckPathExists = true,
            Filter = "Image Files | *.png; *.ico; *.jpg; *.jpeg; *.tiff; *.gif; *.bmp; *.wmf | All Files | *.*"
        };
        string imageLocation;

        /// <summary>
        /// The location where the configuration file for the current user is stored
        /// </summary>
        string configLocation = System.IO.Path.Combine((string)
            Application.Current.Properties["Current User Location"], "config.txt");

        public Options()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyData())
            {
                Application.Current.Properties["Current User Information"] = this.DataContext;
                Application.LoadImage(imageLocation);
                Application.UpdateTheme();
                GoHome();
            }
        }

        /// <summary>
        /// Determines whether or not the data is verified.
        /// </summary>
        /// <returns>True if data is verified. Otherwise, false.</returns>
        private bool VerifyData()
        {
            if (passwordTextBox.Password.Length < 8)
            {
                passwordTextBox.Clear();
                confirmPasswordTextBox.Clear();
                TaskDialog.ShowDialog("Wrong password length", "Your password is invalid.",
                    "Passwords must be at least eight characters.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return false;
            }
            else if (passwordTextBox.Password != confirmPasswordTextBox.Password)
            {
                passwordTextBox.Clear();
                confirmPasswordTextBox.Clear();
                TaskDialog.ShowDialog("Passwords don't match", "Your passwords don't match.",
                    "Please try again.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return false;
            }

            else if (!VerifyUserName())
            {
                return false;
            }

            else if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                TaskDialog.ShowDialog("Invalid Name", "Your name is invalid.",
                    "Please ensure that your name contains at least one character other than spaces.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return false;
            }
            else if (BirthDate.SelectedDate == null)
            {
                TaskDialog.ShowDialog("Invalid Date", "Please insert a valid date of birth.",
                    "This is used to list students' ages and enables students to be sorted by age.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }

            // If we reach this point, then everything is okay.
            return true;
        }

        /// <summary>
        /// Verifies that the user name is not in use
        /// </summary>
        /// <returns></returns>
        private bool VerifyUserName()
        {
            User currentUser = (User)Application.Current.Properties["Current User Information"];
            string usersLocation = (string)Application.Current.Properties["Users Location"];

            // First, ensure that the username isn't blank
            if (string.IsNullOrWhiteSpace(UserNameTextBox.Text))
            {
                TaskDialog.ShowDialog("Invalid Username", "Your username is invalid.",
                    "Please ensure that your username contains at least one character other than spaces.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return false;
            }

            try
            {
                foreach (var user in Directory.EnumerateDirectories(usersLocation))
                {
                    string fullLocation = Path.Combine(user, "config.txt");
                    string data = File.ReadAllText(fullLocation);
                    if (data.GetValue("UserName") == UserNameTextBox.Text && user != currentUser.FileLocation)
                    {
                        TaskDialog.ShowDialog("Username Already Exists", "You can't choose this username.",
                            "Someone already has this username.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                        return false;
                    }
                }
                return true; // If we make it this far, everything is okay
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Warning - Record Pro",
                    "You can't choose this username because it could not be verified.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Warning - Record Pro",
                    "You can't choose this username because it could not be verified",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return false;
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Warning - Record Pro",
                    "You can't choose this username because it could not be verified.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return false;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoHome(); // Navigate to the home page
        }

        /// <summary>
        /// Navigates to the home page
        /// </summary>
        private void GoHome()
        {
            var newHome = new Home();
            this.NavigationService.Navigate(newHome);
        }

        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)Application.Current.Properties["Current User Information"];

            // Prompt the user to enter a file name
            if (openFileDialog1.ShowDialog() == true)
            {
                string fileName = Path.GetFileName(openFileDialog1.FileName);
                imageLocation = Path.Combine(user.FileLocation, fileName);
                try
                {
                    // Upload the image
                    File.Copy(openFileDialog1.FileName, imageLocation, true);

                    // Update the image label
                    ImageLabel.Content = fileName;
                }
                catch (IOException ex)
                {
                    TaskDialog.ShowDialog("Error - Record Pro", "The image couldn't be updated", ex.Message,
                        TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }

                catch (UnauthorizedAccessException)
                {
                    TaskDialog.ShowDialog("Error - Record Pro", "The image couldn't be updated.",
                        "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }
            }
        }

        private void ClearImage_Click(object sender, RoutedEventArgs e)
        {
            ImageLabel.Content = "";
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            // Warn the user before continuing
            if (TaskDialog.ShowDialog("Permanently delete account?", "Are you sure you want to delete your entire account?",
                "This will permanently delete your entire profile, and optionally all your assignments.",
                TaskDialogButtons.Yes | TaskDialogButtons.No, TaskDialogIcon.Warning) == TaskDialogResult.No)
            {
                return;
            }
            TaskDialog.ShowDialog("Closing program", "Record Pro will now close itself.",
                "This is required in order to delete your account.",
                TaskDialogButtons.Ok, TaskDialogIcon.Information);

            //  Close the program
            Process.Start("DeleteRPAccount.exe");
            Application.Current.Shutdown();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            if (!Compatibility.IsWindows8OrHigher)
            {
                notificationsTab.Visibility = Visibility.Collapsed;
            }

            User user = (User)Application.Current.Properties["Current User Information"];
            User newUser = new User(user);
            DataContext = newUser;
            passwordTextBox.Password = newUser.Password;
            confirmPasswordTextBox.Password = newUser.Password;
            var themes = LoadThemes();
            if (themes != null)
            {
                theme.ItemsSource = new Collection<string>((from string theme in LoadThemes()
                                                            let name = Path.GetFileName(theme)
                                                            orderby theme
                                                            select name).ToArray());
            }
            LoadBackup();
        }

        private void LoadBackup()
        {
            try
            {
                using (var registryKey = Registry.CurrentUser.CreateSubKey(Application.RegistryLocation))
                {
                    var backupEnabled = (string)registryKey.GetValue("Backup Enabled", "False");
                    if (backupEnabled == "True")
                    {
                        var backupFrequency = (string)registryKey.GetValue("Backup Frequency", "Every day");
                        var backupHour = (string)registryKey.GetValue("Backup Hour", "Midnight");
                        var backupLocation = (string)registryKey.GetValue("Backup Location", "");
                        backupFrequencyLabel.Content = backupFrequency;
                        backupHourLabel.Content = backupHour;
                        backupLocationLabel.Content = backupLocation;
                        if (string.IsNullOrEmpty(backupHour))
                        {
                            backupHourLabel.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            backupHourLabel.Visibility = Visibility.Visible;
                        }
                        backupStatus.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        backupStatus.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "Backup settings could not be loaded.",
                ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "Backup settings could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "Backup settings could not be loaded.",
                "Record Pro does not have the required permission.",
                TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }

        /// <summary>
        /// Adds all themes to the themes list
        /// </summary>
        private string[] LoadThemes()
        {
            string themeLocation = (string)Application.Current.Properties["Theme Location"];
            try
            {
                return Directory.EnumerateDirectories(themeLocation).ToArray();
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "The themes list could not be loaded.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "The themes list could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "The themes list could not be loaded.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return null;
        }

        private void setupBackup_Click(object sender, RoutedEventArgs e)
        {
            var newBackup = new BackupDialog() { Owner = Application.mWindow };
            newBackup.ShowDialog();
            LoadBackup();
        }

        private void stopBackup_Click(object sender, RoutedEventArgs e)
        {
            var result = TaskDialog.ShowDialog("Stop backing up files", "Are you sure you want to stop backup?",
                "Your data will no longer be able to be recovered if backup is disabled.",
                TaskDialogButtons.Yes | TaskDialogButtons.No, TaskDialogIcon.Warning);
            if (result == TaskDialogResult.Yes)
            {
                RemoveBackup();
            }
        }

        private void RemoveBackup()
        {
            try
            {
                using (var newKey = Registry.CurrentUser.CreateSubKey(Application.RegistryLocation))
                {
                    newKey.SetValue("Backup Enabled", "False");
                }
                LoadBackup(); // Update the interface
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "Backup settings could not be saved.",
                ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "Backup settings could not be saved.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Error - Record Pro", "Backup settings could not be saved.",
                "Record Pro does not have the required permission.",
                TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }

        }
    }
}
