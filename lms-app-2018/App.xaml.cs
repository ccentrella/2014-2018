namespace RecordPro
{
    using System.Collections.ObjectModel;
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Shell;
    using System.Windows.Threading;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class Application : System.Windows.Application
    {
        /// <summary>
        /// The window that refers to the current main window instance
        /// </summary>
        internal static MainWindow mWindow; // This will be used to refer to the main window.

        /// <summary>
        /// The default file location
        /// </summary>
        private static string DefaultFileLocation = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData), "Autosoft", "Record Pro", "2018");

        /// <summary>
        /// The registry location, not including the HKEY modifier
        /// </summary>
        public const string RegistryLocation = @"Software\Autosoft\Record Pro\2018";

        /// <summary>
        /// Restarts the application
        /// </summary>
        public static void Restart()
        {
            var location = Assembly.GetExecutingAssembly().CodeBase;
            Application.Current.Shutdown();
            Environment.SetEnvironmentVariable("CanOpen", "True");
            Process.Start(location);
        }

        /// <summary>
        /// Updates the theme
        /// </summary>
        internal static void UpdateTheme()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the user's image
        /// </summary>
        /// <param name="imageLocation">The location of the user's image</param>
        public static void LoadImage(string imageLocation)
        {
            try
            {
                // This ensures that we won't run into any format exceptions, even if the 
                // file is deleted before the image is loaded.
                if (File.Exists(imageLocation))
                {
                    var newImage = new BitmapImage(new Uri(imageLocation, UriKind.Relative))
                    {
                        DecodePixelHeight = 50,
                        DecodePixelWidth = 50
                    };
                    mWindow.Avatar.Source = new BitmapImage(new Uri(imageLocation, UriKind.Absolute));
                }
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Image Error - Record Pro", "Your image could not be loaded.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }
       
        /// <summary>
        /// Notify the user that an operation is starting
        /// </summary>
        /// <param name="message">The message to be used for the status</param>
        public static void PrepareProgress(string message)
        {
            Debug.Assert(mWindow != null);
            mWindow.progress.Value = 0;
            mWindow.progress.Visibility = System.Windows.Visibility.Visible;
            mWindow.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            mWindow.progress.ToolTip = message;
        }

        /// <summary>
        /// Increments progress by the specified value
        /// </summary>
        /// <param name="value">The value to increment the progress by</param>
        public static void IncrementProgress(double value)
        {
            Debug.Assert(mWindow != null);
            double totalProgress = Application.mWindow.progress.Value + value;
            if (totalProgress <= 1)
            {
                Application.mWindow.progress.Value = totalProgress;
            }
            else
            {
                Application.mWindow.progress.Value = 1;
            }
        }

        /// <summary>
        /// Notifies the user that an operation is complete
        /// </summary>
        public static void CompleteProgress()
        {
            Debug.Assert(mWindow != null);
            mWindow.progress.Visibility = System.Windows.Visibility.Hidden;
            mWindow.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
            mWindow.progress.ToolTip = null;
        }

        /// <summary>
        /// Set the default theme
        /// </summary>
        /// <param name="newThemeName">The name of the new theme</param>
        private static void SetDefaultTheme(string newThemeName)
        {
            try
            {
                using (var registryKey = Registry.CurrentUser.CreateSubKey(RegistryLocation))
                {
                    registryKey.SetValue("Default Theme", newThemeName);
                }
            }
            catch (IOException)
            {
                TaskDialog.ShowDialog("Theming Error", "The theme could not be loaded.",
                    "The default theme could not be changed.", TaskDialogButtons.Ok,
                    TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Theming Error", "The default theme could not be changed.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Theming Error", "The theme could not be changed.",
                    "The program does not have the required permission.", TaskDialogButtons.Ok,
                    TaskDialogIcon.Warning);
            }
        }

        /// <summary>
        /// Get the default theme
        /// </summary>
        private static void GetDefaultTheme()
        {
            string theme = "Crystal 2017 Modern";
            try
            {
                using (var registryKey = Registry.CurrentUser.CreateSubKey(RegistryLocation))
                {
                    theme = (string)registryKey.GetValue("Default Theme", "Crystal 2017 Modern");
                }
            }
            catch (IOException)
            {
                TaskDialog.ShowDialog("Theme Error", "The theme could not be accessed.",
                "The theme could not be loaded.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Theme Error", "The theme could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Theme Error", "The theme could not be loaded.",
                "Record Pro does not have the required permission.",
                TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            Application.Current.Properties["Default Theme"] = theme;
        }

        /// <summary>
        /// Changes the file location used for records
        /// </summary>
        /// <param name="newLocation">The new folder location</param>
        private static void ChangeFileLocation(string newLocation)
        {
            // Retrieve the old location
            string fileLocation = (string)Application.Current.Properties["File Location"];
            Debug.Assert(fileLocation != null);

            // Attempt to move all information to the new location
            if (MoveFileLocation(newLocation, fileLocation))
            {
                // Now update the program
                Application.Current.Properties["File Location"] = newLocation;
                UpdateLocations();

                // Finally, notify the user
                MessageBox.Show("The file location was successfully updated.",
                    "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Attempts to move all directories and update the file location
        /// </summary>
        /// <param name="newLocation">The new file location</param>
        /// <param name="fileLocation">The original file location</param>
        /// <returns>True if the operation succeeded.
        /// Otherwise, false.</returns>
        private static bool MoveFileLocation(string newLocation, string fileLocation)
        {
            try
            {
                MoveDirectory(fileLocation, newLocation);
                Debug.Assert(Directory.Exists(newLocation));
                using (var newKey = Registry.CurrentUser.CreateSubKey(RegistryLocation))
                {
                    newKey.SetValue("File Location", newLocation);
                }

                return true; // The operation succeeded
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "The operation could not be completed.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("File Error", "The operation could not be completed.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "The operation could not be completed.",
                    "The program does not have the required permission",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return false; // The method will only reach this point if an error occurred
        }

        /// <summary>
        /// Moves a directory to another directory.
        /// </summary>
        /// <remarks>Using this method, directories can be moved across volumes.</remarks>
        /// <param name="sourceLocation">The original location</param>
        /// <param name="destLocation">The new location</param>
        /// <exception cref="IOException"/>
        /// <exception cref="ArgumentNullException"/>
        ///<exception cref="ArgumentException"/>
        ///<exception cref="UnauthorizedAccessException"/>
        ///<exception cref="PathTooLongException"/>
        ///<exception cref="DirectoryTooLongException"/>
        ///<exception cref="NotSupportedException"/>
        private async static void MoveDirectory(string sourceLocation, string destLocation)
        {
            if (sourceLocation == destLocation)
            {
                return;
            }

            string directoryName = Path.GetFileName(sourceLocation);

            // Update the name of each file
            var files = (from file in Directory.EnumerateFiles(sourceLocation,
                          "*", SearchOption.AllDirectories).AsParallel()
                         let newFile = file.Replace(sourceLocation, destLocation)
                         select new Task(() => File.Move(file, newFile))).ToArray();

            // Update all files in parallel
            await Task.WhenAll(files);
        }

        /// <summary>
        /// Removes the administrator tag from a user
        /// </summary>
        /// <param name="userName">The user name where this tag will be removed</param>
        /// <param name="userPassword">The password where this tag will be removed</param>
        private static void DisableAdministrator(string userName, string userPassword)
        {
            string usersLocation = Application.Current.Properties["Users Location"].ToString();
            Debug.Assert(usersLocation != null);
            foreach (var folder in Directory.EnumerateDirectories(usersLocation))
            {
                var fileLocation = folder + "\\config.txt";
                string data;

                // Attempt to change the administrator tag
                try
                {
                    data = File.ReadAllText(fileLocation);
                    var dataUserName = StringFunctions.GetValue(data, "UserName").ToUpperInvariant();
                    var dataPassword = StringFunctions.GetValue(data, "Password");
                    if (dataUserName == userName.ToUpperInvariant() & dataPassword == userPassword)
                    {
                        File.WriteAllText(fileLocation, StringFunctions.ReplaceValue(data, "IsTeacher", "False"));
                        MessageBox.Show("The administrator tag has been removed.",
                            "Information - Record Pro", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        return;
                    }
                }
                catch (IOException ex)
                {
                    TaskDialog.ShowDialog("File Error", "The operation could not be completed.",
                        ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }
                catch (SecurityException)
                {
                    TaskDialog.ShowDialog("File Error", "The operation could not be completed.",
                        "The program does not have the required permission.",
                        TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }
                catch (UnauthorizedAccessException)
                {
                    TaskDialog.ShowDialog("Access Denied", "The operation could not be completed",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Adds the administrator tag to a user
        /// </summary>
        /// <param name="userName">The user name where this tag will be added</param>
        /// <param name="userPassword">The password where this tag will be added</param>		
        private static void EnableAdministrator(string userName, string userPassword)
        {
            string usersLocation = Application.Current.Properties["Users Location"].ToString();
            Debug.Assert(usersLocation != null);
            foreach (var folder in Directory.EnumerateDirectories(usersLocation))
            {
                var fileLocation = folder + "\\config.txt";
                string data;

                // Attempt to change the administrator tag
                try
                {
                    data = File.ReadAllText(fileLocation);
                    var dataUserName = StringFunctions.GetValue(data, "UserName").ToUpperInvariant();
                    var dataPassword = StringFunctions.GetValue(data, "Password");
                    if (dataUserName == userName.ToUpperInvariant() & dataPassword == userPassword)
                    {
                        File.WriteAllText(fileLocation, StringFunctions.ReplaceValue(data, "IsTeacher", "True"));
                        MessageBox.Show("The administrator tag has been added.",
                            "Information - Record Pro", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        return;
                    }
                }
                catch (IOException ex)
                {
                    TaskDialog.ShowDialog("File Error", "The operation could not be completed.",
                        ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }
                catch (SecurityException)
                {
                    TaskDialog.ShowDialog("File Error", "The operation could not be completed.",
                        "The program does not have the required permission.",
                        TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }
                catch (UnauthorizedAccessException)
                {
                    TaskDialog.ShowDialog("Access Denied", "The operation could not be completed",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Update the file location
        /// </summary>
        private static void UpdateFileLocation()
        {
            string savedFileLocation = "";
            try
            {
                using (var registryKey = Registry.CurrentUser.CreateSubKey(RegistryLocation))
                {
                    savedFileLocation = registryKey.GetValue("File Location", DefaultFileLocation).ToString();
                }
            }
            catch (IOException)
            {
                TaskDialog.ShowDialog("File Error", "An error has occurred.",
                "The file location could not be retrieved.", TaskDialogButtons.Ok,
                TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Access Denied", "The file location could not be retrieved.",
                "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "The file location could not be retrieved.",
                "The program does not the required permission.",
                TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }

            // Update the file location
            if (!Directory.Exists(savedFileLocation))
            {
                savedFileLocation = DefaultFileLocation;
            }

            Application.Current.Properties["File Location"] = savedFileLocation;

            UpdateLocations();  // Update all locations.
            UpdateSchoolInfo(); // Get school info and store it in properties
        }

        /// <summary>
        /// Updates locations based on the file location.
        /// </summary>
        public static void UpdateLocations()
        {
            string fileLocation = (string)Application.Current.Properties["File Location"];
            Debug.Assert(fileLocation != null);
            Application.Current.Properties["Users Location"] = Path.Combine(fileLocation, "Users");
            Application.Current.Properties["Theme Location"] = Path.Combine(fileLocation, "Themes");
        }

        private void App_Started(object sender, StartupEventArgs e)
        {
            // For debugging purposes only. This should be removed when Record Pro is installed.
            NativeMethods.SetCurrentProcessExplicitAppUserModelID("Record Pro 2018");

            // Initialize all locations
            UpdateFileLocation();
            var args = Environment.GetCommandLineArgs().ToList();
            args.RemoveAt(0); // We don't want the file location of the program, which is the first argument

            // Check each argument
            args.ForEach((arg) =>
            {
                int i = args.IndexOf(arg);
                arg = arg.ToUpper();
                if (arg == "/NT")
                {
                    Application.Current.Properties["Enable Theming"] = false;
                }
                else if (arg == "/ENABLEADMIN" && args.Count > i + 2)
                {
                    EnableAdministrator(args[i + 1], args[i + 2]);
                }
                else if (arg == "/DISABLEADMIN" && args.Count > i + 2)
                {
                    DisableAdministrator(args[i + 1], args[i + 2]);
                }
                else if (arg == "/CHANGELOCATION" && args.Count > i + 1)
                {
                    ChangeFileLocation(args[i + 1]);
                }
                else if (arg == "/CHANGETHEME" && args.Count > i + 1)
                {
                    SetDefaultTheme(args[i + 1]);
                }
            });

            // Load the default theme
            GetDefaultTheme();
            ResetTheme();
        }

        /// <summary>
        /// Gets school information and saves it in a school object
        /// </summary>
        public static void UpdateSchoolInfo()
        {
            string fileLocation = (string)Application.Current.Properties["File Location"];
            string schoolLocation = Path.Combine(fileLocation, "School Info");
            Application.Current.Properties["School"] = new School(schoolLocation);
        }

        /// <summary>
        /// Loads the user's theme
        /// </summary>
        /// <param name="data">The data which contains the theme attribute</param>
        public async static void LoadTheme(string data)
        {
            #region Prep
            var stopwatch = Stopwatch.StartNew(); // Used to measure performance
            string theme = StringFunctions.GetValue(data, "Theme");
            int oldThemeCount = Application.Current.Resources.MergedDictionaries.Count;
            string themePath = (string)Application.Current.Properties["Theme Location"];
            string themeLocation;
            string themeName;

            // Only continue if a valid theme has been entered and the theme location is not null
            if (string.IsNullOrWhiteSpace(theme) | themePath == null)
            {
                return;
            }

            themeLocation = System.IO.Path.Combine(themePath, theme);
            themeName = System.IO.Path.Combine(themeLocation);

            // Only continue if the selected theme has not been currently loaded
            string currentTheme = (string)Application.Current.Properties["Current Theme"];
            if (currentTheme != null && currentTheme == themeName)
            {
                return;
            }

            // Only continue if themes are enabled
            if ((bool?)Application.Current.Properties["Enable Theming"] == false)
            {
                return;
            }

            Application.Current.Properties["Current Theme"] = themeName;
            Debug.WriteLine("It took {0} Ms for prep theme work",
                stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            #endregion

            try
            {
                // Add all of the new themes, which will be placed at the end of the collection
                var themeFiles = (from file in Directory.EnumerateFiles(themeLocation).AsParallel()
                                  let dictionary = new ResourceDictionary() { Source = new Uri(file, UriKind.Absolute) }
                                  select new Task(() =>
                                  {
                                      Application.Current.Resources.MergedDictionaries.Add(dictionary);
                                  })).ToArray();
                #region LoadThemes
                await Task.WhenAll(themeFiles);
                Debug.WriteLine("It took {0} Ms to load the new theme",
                    stopwatch.ElapsedMilliseconds);
                stopwatch.Restart();

                // Now remove all of the old themes
                Parallel.For(0, oldThemeCount, ((i) =>
                    {
                        Application.Current.Resources.MergedDictionaries.RemoveAt(i);
                    }));
                #endregion
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Theming Error", "The theme could not be updated.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Access Denied", "The theme could not be updated.",
                "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Theming Error", "The theme could not be updated.",
                "The program does not have the required permission.",
                TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            stopwatch.Stop();
            Debug.WriteLine("It took {0} Ms to remove the old theme", stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Resets the theme
        /// </summary>
        public static void ResetTheme()
        {
            // Get the default theme
            string defaultTheme = Application.Current.Properties["Default Theme"].ToString();

            // Ensure the theme is not null
            if (defaultTheme == null)
            {
                defaultTheme = "Crystal 2017 Modern";
            }

            // Reset the theme
            string resetTheme = string.Format("Theme = \"{0}\"", defaultTheme);
            LoadTheme(resetTheme);
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is TargetInvocationException ex && ex.InnerException.GetType() == typeof(XamlParseException))
            {
                TaskDialog.ShowDialog("Theming Error", "A theming error occurred.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }

        /// <summary>
        /// Logs on the specified user
        ///	</summary>
        ///	<param name="data">The data containing information for the user</param>
        ///	<param name="folderLocation">The location of the user folder</param>
        public static async void LogOn(string data, string folderLocation)
        {
            #region Variables
            string userName = StringFunctions.GetValue(data, "Name");
            string usersLocation = (string)Application.Current.Properties["Users Location"];
            string gradeLocation = Path.Combine(folderLocation, "Grades");
            string currentUserLocation = (string)Application.Current.Properties["Current User"];
            string recentLocation = Path.Combine(folderLocation, "recent.txt");
            string recent = "";
            #endregion

            Debug.Assert(usersLocation != null);
            Debug.Assert(mWindow != null);

            #region LoadUser
            try
            {
                Directory.CreateDirectory(folderLocation);
                Directory.CreateDirectory(gradeLocation);
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "The login process could not be completed.",
                ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return;
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Access Denied", "The login process could not be completed",
                "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return;
            }

            // When the user first uses Record Pro, the recent location won't exist
            if (File.Exists(recentLocation))
            {
                try
                {
                    using (var newReader = new StreamReader(recentLocation))
                    {
                        recent = await newReader.ReadToEndAsync();
                    }
                }
                catch (IOException ex)
                {
                    TaskDialog.ShowDialog("File Error", "The recent file list could not be loaded.",
                        ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                }
            }

            // Update user information
            Application.Current.Properties["Current User"] = userName;
            Application.Current.Properties["Current User Location"] = folderLocation;
            Application.Current.Properties["Grade File Location"] = gradeLocation;
            LoadUser(folderLocation, recent);
            if (StringFunctions.GetValue(data, "IsTeacher") == "True")
            {
                System.Windows.Application.Current.Properties["IsTeacher"] = true;
                System.Windows.Application.Current.Properties["Students"] = data.EnumerateStrings("Students");
            }
            else
            {
                System.Windows.Application.Current.Properties["IsTeacher"] = false;
            }
            #endregion

            // Close the popup
            mWindow.DataContext = Application.Current.Properties["Current User Information"];
            mWindow.AvatarPopup.IsOpen = false;
            mWindow.searchPane.Visibility = Visibility.Visible;
            #region ValidateUser

            // Validate the user
            if (ValidateUser(folderLocation))
            {
                // Load the user window.
                var newHome = new Home();
                mWindow.MainFrame.Navigate(newHome);
                Application.LoadTheme(data); // Load the theme
            }
            else
            {
                var newRegistration = new ProductRegistration(data, folderLocation);
                mWindow.MainFrame.Navigate(newRegistration);
            }
            #endregion
        }

        /// <summary>
        /// Creates a new user object and stores in application properties
        /// </summary>
        /// <param name="location">The user's location</param>
        /// <param name="recent">The data containing the user's recent files</param>
        private static void LoadUser(string settingsFile, string recent)
        {
            var newUser = new User(settingsFile);

            // Save the user in settings
            Application.Current.Properties["Current User Information"] = newUser;
            Application.Current.Properties["Recent"] = recent.EnumerateStrings();
        }

        /// <summary>
        /// Saves all of the user's settings
        /// </summary>
        private static void SaveUser()
        {
            #region Variables
            var recent = (Collection<string>)Application.Current.Properties["Recent"];
            string recentString = string.Join(",", recent);
            User currentUser = (User)Application.Current.Properties["Current User Information"];
            string userLocation = (string)Application.Current.Properties["Current User Location"];
            string configLocation = Path.Combine(userLocation, "config.txt");
            string recentLocation = Path.Combine(userLocation, "recent.txt");
            StringBuilder userInfo = new StringBuilder();
            #endregion

            // If no user is logged on, current user is null
            if (currentUser == null)
            {
                return;
            }

            // Set each property
            var props = from prop in currentUser.GetType().GetProperties()
                        where prop.CanWrite
                        let str = GetString(prop.GetValue(currentUser))
                        where !string.IsNullOrEmpty(str)
                        where prop.Name != "ContactInfo"
                        where prop.Name != "FileLocation"
                        where prop.Name != "ReportCards"
                        where prop.Name != "CurrentReportCard"
                        select new { Name = prop.Name, Text = str };
            var contactInfo = currentUser.ContactInfo.GetType().GetProperties();
            var cProps = from prop in contactInfo
                         where prop.GetValue(currentUser.ContactInfo) != null
                         select new { Name = prop.Name, Text = GetString(prop.GetValue(currentUser.ContactInfo)) };
            foreach (var prop in props)
            {
                userInfo.AppendFormat("{0} = \"{1}\"\r\n", prop.Name, prop.Text);
            }

            foreach (var prop in cProps)
            {
                userInfo.AppendFormat("{0} = \"{1}\"\r\n", prop.Name, prop.Text);
            }


            // Update the settings and recent files
            try
            {
                using (var newWriter = new StreamWriter(configLocation))
                {
                    newWriter.Write(userInfo.ToString());
                }

                using (var newWriter = new StreamWriter(recentLocation))
                {
                    newWriter.Write(recentString);
                }
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("File Error", "The settings could not be saved.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "The settings could not be saved.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "The settings could not be saved.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }

        /// <summary>
        /// Converts the property value to a string.
        /// </summary>
        /// <param name="item">The item to convert</param>
        /// <returns>The string containing the value</returns>
        private static string GetString(object item)
        {
            if (item == null)
            {
                return null;
            }
            else if (item.GetType() == typeof(Collection<string>))
            {
                var collection = (Collection<string>)item;
                var s = string.Join(",", collection.ToArray());
                return s;
            }
            else if (item.GetType() == typeof(Collection<DateTime>))
            {
                var collection = (Collection<DateTime>)item;
                var s = string.Join(",", collection.ToArray());
                return s;
            }
            else
            {
                return item.ToString();
            }
        }

        /// <summary>
        /// Logs out the specified user
        /// </summary>
        public static void SignOut()
        {
            SaveUser(); // Saves the user's settings
            mWindow.AvatarPopup.IsOpen = false; // Close the small pane
            ImageFunctions.LoadDefaultImage(Gender.Unknown);
            mWindow.UserHeader.Content = "Sign In"; // Update text
            mWindow.searchPane.Visibility = Visibility.Collapsed;
            Current.Properties["Current User"] = "None";
            Current.Properties["Current User Information"] = null;
            Current.Properties["IsTeacher"] = false;
            Current.Properties["Validated"] = false;
            mWindow.DataContext = null;

            // Navigate to the welcome screen.
            var newWelcome = new Welcome();
            mWindow.MainFrame.Navigate(newWelcome);

            // Reset the theme
            Application.ResetTheme();
        }

        /// <summary>
        /// Validates the user
        /// </summary>
        /// <param name="folderLocation">The location containing the user's files</param>
        /// <returns>Whether or not the user has paid for this product</returns>
        public static bool ValidateUser(string folderLocation)
        {
            string validationFile = Path.Combine(folderLocation, "Validation.txt");
            string folderName = System.IO.Path.GetFileName(folderLocation);
            char[] folderArray = folderName.ToCharArray();
            DateTime modificationDate;
            string data;

            // If the validation file does not exist, then the user hasn't activated his account.
            if (!File.Exists(validationFile))
            {
                return false;
            }

            try
            {
                using (var newReader = new StreamReader(validationFile))
                {
                    data = newReader.ReadToEnd();
                }

                modificationDate = File.GetLastWriteTime(validationFile);
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

            string modificationDateString = modificationDate.ToString();
            string separator = modificationDateString[6].ToString() + modificationDateString[0].ToString()
                + modificationDateString[5].ToString() + modificationDateString[4].ToString();
            string correctData = string.Join(separator, folderArray);

            // Validate the data
            if (data != correctData)
            {
                return false;
            }
            else
            {
                System.Windows.Application.Current.Properties["Validated"] = true;
                return true;
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Application.Current.Properties["Current User"] != null &&
                (string)Application.Current.Properties["Current User"] != "None")
            {
                SaveUser(); // Saves the user's settings
            }
        }

    }
}
