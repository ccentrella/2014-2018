using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Path = System.IO.Path;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Printing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RecordPro
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        //string appLocation;
        //bool reloadEnabled;
        public Home()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //UpdateAppLocation(); // Update the app location
            LoadSettings(); // Load all settings
            LoadRecent(); // Load all recent files
                          //await LoadAppsAsync(); // Load all apps
        }

        /// <summary>
        /// Load all settings
        /// </summary>
        /// <returns></returns>
        private void LoadSettings()
        {
            User currentUser = (User)Application.Current.Properties["Current User Information"];
            School currentSchool = (School)Application.Current.Properties["School"];
            this.DataContext = currentUser;
            schoolPane.DataContext = currentSchool;

            // Now load all settings
            if (currentUser.ShowHomePopup)
            {
                var newAd = new Ad("Your profile at a glance", new HomeAd()) { Owner = Application.mWindow };
                if (newAd.ShowDialog() == true)
                {
                    currentUser.ShowHomePopup = false;
                    Application.Current.Properties["Current User Information"] = currentUser;
                }
            }

            if (currentUser.UserStatus == UserStatus.Denied && Compatibility.IsWindows8OrHigher && currentUser.Notification8)
            {
                Notifications.SendNotification("You have been denied.", "Please contact your teacher for more information.");
            }
        }

        /// <summary>
        /// Loads the list of recent files
        /// </summary>
        private void LoadRecent()
        {
            // Load the user
            User currentUser = (User)Application.Current.Properties["Current User Information"];
            var recent = (Collection<string>)Application.Current.Properties["Recent"];
            var recentFileEnumerator = recent.GetEnumerator();
            int fileCount = 0;
            recentFiles.Children.Clear();

            // Load the user's recent files
            while (fileCount < currentUser.MaxRecentFiles &&
                recentFileEnumerator.MoveNext())
            {
                string path = (string)recentFileEnumerator.Current;
                string title = System.IO.Path.GetFileNameWithoutExtension(path);
                Hyperlink fileHyperlink = new Hyperlink(new Run(title)) { ToolTip = path };
                fileHyperlink.SetResourceReference(Hyperlink.StyleProperty, "LinkHyperlink");
                fileHyperlink.Click += fileHyperlink_Click;
                var newItem = new TextBlock(fileHyperlink);
                var newMenu = new ContextMenu();
                var recentFileMenuItem = new MenuItem() { Header = "_Remove", Tag = fileHyperlink };
                recentFileMenuItem.Click += recentFileMenuItem_Click;
                newMenu.Items.Add(recentFileMenuItem);
                newItem.ContextMenu = newMenu;
                recentFiles.Children.Add(newItem);
                fileCount++;
            }
        }

        void recentFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item != null && item.Tag != null)
            {
                RemoveFile((Hyperlink)item.Tag);
            }
        }

        private void fileHyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            Debug.Assert(link != null && link.ToolTip != null);
            string path = link.ToolTip.ToString();
            try
            {
                Process.Start(path);
                path.UpdateRecent();
                LoadRecent();
            }
            catch (Win32Exception)
            {
                var result = TaskDialog.ShowDialog("File Error",
                    "This file could not be opened.", "Do you want to remove it from this list?",
                    TaskDialogButtons.Yes | TaskDialogButtons.No, TaskDialogIcon.Warning);
                if (result == TaskDialogResult.Yes)
                {
                    RemoveFile(link);
                }
            }
        }

        /// <summary>
        /// Removes the file from the recent list
        /// </summary>
        /// <param name="link">The file to remove</param>
        private void RemoveFile(Hyperlink link)
        {
            Debug.Assert(link != null && link.Parent != null && link.ToolTip != null);
            string file = link.ToolTip.ToString();
            TextBlock parent = (TextBlock)link.Parent;
            recentFiles.Children.Remove(parent);

            // Delete the file from the user's recent list
            file.DeleteRecent();
            LoadRecent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var user = (User)Application.Current.Properties["Current User Information"];
            string location = Path.Combine(user.FileLocation, "Grades", user.RecentGradeLevel + ".xml");
            var newDialog = new AddAssignment(user) { Owner = Application.mWindow };
            if (newDialog.ShowDialog() == true)
            {
                Assignment.AddAssignment(newDialog.Assignment);
                user.RecentCourse = newDialog.Assignment.Course;
                user.RecentGradeLevel = newDialog.Assignment.GradeLevel;
            }
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            var newDialog = new PrintDialog();
            if (newDialog.ShowDialog() == true)
            {
                var document = new FixedDocument();
                document.Pages.Add(CreateRecordCardPage());
                var writer = PrintQueue.CreateXpsDocumentWriter(newDialog.PrintQueue);
                writer.WriteAsync(document);
                newDialog.PrintDocument(document.DocumentPaginator, "Record Pro 2018 Report Card");
            }
        }

        private PageContent CreateRecordCardPage()
        {
            var user = (User)Application.Current.Properties["Current User Information"];
            FixedPage page = new FixedPage();
            page.Margin = new Thickness(50);
            page.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri("Record Pro Logo.png", UriKind.Relative))
            });
            page.Children.Add(new Label()
            {
                Content = "Record Pro 2018",
                Style = (Style)FindResource("CrystalHeadings2Dark"),
                Margin = new Thickness(35, 0, 0, 0),

            });
            page.Children.Add(new Label()
            {
                Content = string.Format("Here's a report for {0} ({1})", user.UserName, user.Name),
                Style = (Style)FindResource("CrystalHeadings2Dark"),
                Margin = new Thickness(250, 25, 0, 0),
            });
            var reportCard = (StackPanel)FindResource("CurrentReportCard");
            reportCard.DataContext = DataContext;
            reportCard.Margin = new Thickness(0, 50, 0, 0);
            page.Children.Add(reportCard);
            page.Children.Add(new Separator() { Margin = new Thickness(0, 250, 0, 0) });
            var schoolInfo = (Grid)FindResource("SchoolInfo");
            schoolInfo.DataContext = schoolPane.DataContext;
            schoolInfo.Margin = new Thickness(350, 255, 0, 0);
            page.Children.Add(schoolInfo);
            var newContent = new PageContent();
            newContent.Child = page;
            return newContent;
        }
    }
}
