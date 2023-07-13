using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;


namespace RecordPro
{
    /// <summary>
    /// Interaction logic for Assignments.xaml
    /// </summary>
    public partial class Assignments : Page
    {
        string fileLocation;
        int currentPage = 1;
        int searchCount; // The amount of files that the search contains
        ParallelQuery<string> files = null;

        public Assignments()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // Close this page and brings the user home
            this.NavigationService.Navigate(new Home());
        }

        /// <summary>
        /// Sets the file location for the selected grade
        /// </summary>
        private async void UpdateGradeAsync()
        {
            string selectedGrade = (string)grades.SelectedItem;
            User user = (User)grades.DataContext;

            // Prepare to load the user
            ClearResults();

            SetFileLocation(selectedGrade); // Update the locations
            await DeleteEmptyDirectoriesAsync(); // Delete all empty directories.
        }

        /// <summary>
        /// Clear the list of results
        /// </summary>
        private void ClearResults()
        {
            results.Items.Clear();
            files = null;
            currentPage = 1;
            searchCount = 0;
        }

        /// <summary>
        /// Sets the new locations. If the user has multiple file locations,
        /// the grade name will be appended to the file location.
        /// </summary>
        /// <param name="gradeName">The name of the grade to load</param>
        private void SetFileLocation(string gradeName)
        {
            User user = (User)grades.DataContext;
            string customLocation = user.AssignmentLocation;

            // Set the location
            if (Directory.Exists(customLocation) & string.IsNullOrEmpty(gradeName))
            {
                fileLocation = customLocation;
            }
            else if (Directory.Exists(customLocation) & !string.IsNullOrEmpty(gradeName))
            {
                fileLocation = Path.Combine(customLocation, gradeName);
            }
            else if (!Directory.Exists(customLocation) & string.IsNullOrEmpty(gradeName))
            {
                fileLocation = Path.Combine(user.FileLocation, "Assignments");
            }
            else // A specific grade name has been entered, but the user has no custom location
            {
                fileLocation = Path.Combine(user.FileLocation, "Assignments", gradeName);
            }
        }

        /// <summary>
        /// Asynchronously delete all empty directories in parallel
        /// </summary>
        private async Task DeleteEmptyDirectoriesAsync()
        {
            // Only continue if the location is valid
            if (!Directory.Exists(fileLocation))
            {
                return;
            }

            Application.PrepareProgress("Deleting empty directories.");

            // Search for every empty directory in parallel.
            try
            {
                var folderInfo = new DirectoryInfo(fileLocation);
                double incrementValue = 0;

                // Check each folder
                var folders = (from folder in folderInfo.EnumerateDirectories().AsParallel()
                               let dInfo = new DirectoryInfo(folder.FullName)
                               let files = dInfo.GetFiles("*", SearchOption.AllDirectories)
                               where files.Length == 0
                               select Task.Run(async () =>
                               {
                                   dInfo.Delete(true);
                                   await Dispatcher.BeginInvoke(new Action(() =>
                                       Application.IncrementProgress(0.05)));
                               })).ToArray();

                // Begin the operation
                incrementValue = folders.Count();
                await Task.WhenAll(folders);
            }
            catch (IOException)
            {
                TaskDialog.ShowDialog("File Error", "An error occurred.",
                "Empty directories could not be deleted.",
                TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "Empty directories could not be deleted.",
                "Record Pro does not have the required permission.",
                TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Access Denied", "Empty directories could not be deleted.",
                "Access was denied.");
            }

            Application.CompleteProgress();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers(); // Load all users
            UpdateGradeAsync();

            // See if we should enable the students box
            if ((bool?)Application.Current.Properties["IsTeacher"] == true)
            {
                users.Visibility = Visibility.Visible;
            }
        }



        /// <summary>
        /// Asynchronously load all users
        /// </summary>
        /// <returns>A task object, used to manipulate the method</returns>
        private void LoadUsers()
        {
            bool isTeacher;

            if (Application.Current.Properties["IsTeacher"].ToString() == "True")
            {
                isTeacher = true;
            }
            else
            {
                isTeacher = false;
            }

            // Acquire the list of students
            string usersLocation = Application.Current.Properties["Users Location"].ToString();
            var students = new Collection<User>((from student in Directory.EnumerateDirectories(usersLocation).AsParallel()
                                                 where User.UserIsStudent(student)
                                                 let user = User.GetUser(student)
                                                 orderby user.UserName
                                                 select user).ToArray());
            users.DataContext = students;
            users.SelectedIndex = 0;

            // Show the combo-box if necessary
            if (isTeacher)
            {
                users.Visibility = Visibility.Visible;
            }
            else
            {
                users.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// Opens all items that are selected
        /// </summary>
        private void OpenFiles()
        {
            var items = (from ListBoxItem item in results.SelectedItems
                         let name = Path.GetFileName(item.ToolTip.ToString())
                         select new
                         {
                             Location = item.ToolTip.ToString(),
                             Name = name
                         }).ToArray();

            // Open every file that is selected
            foreach (var item in items)
            {
                try
                {
                    Process.Start(item.Location);
                }
                catch (Win32Exception ex)
                {
                    string message = string.Format("{0} could not be opened.", item.Name);
                    TaskDialog.ShowDialog("File Error", message, ex.Message,
                        TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                    continue;
                }

                // Update the user's recent lists
                item.Location.UpdateRecent();
            }
        }


        /// <summary>
        /// Searches asynchronously.
        /// </summary>
        private async void Search()
        {
            #region Variables
            ComboBoxItem selectedWeek = week.SelectedItem as ComboBoxItem;
            ComboBoxItem selectedDay = day.SelectedItem as ComboBoxItem;
            string currentWeek; // The selected week
            string currentDay;  // The selected day
            string currentCourse = course.Text.ToUpper(); // The selected course
            string searchText = searchBox.Text.ToUpper(); // The text that the user has entered
            string[] quoteList = searchText.CheckQuotes(); // The list containing all quotes
            string[] stringList = searchText.CheckText(); // The list containing all words
            #endregion

            // Get the week
            if (selectedWeek == null)
            {
                currentWeek = "ANY";
            }
            else
            {
                currentWeek = selectedWeek.Content.ToString().ToUpper();
            }

            // Get the day
            if (selectedDay == null)
            {
                currentDay = "ANY";
            }
            else
            {
                currentDay = selectedDay.Content.ToString().ToUpper();
            }

            // Close the search popup and clear the results pane
            SearchPopup.IsOpen = false; // Close the search popup
            ClearResults(); // Clear all old results

            Application.PrepareProgress("Loading Files"); // Prepare progress notifications

            // Attempt to get the list of matches
            try
            {
                files = from file in Directory.EnumerateFiles(fileLocation,
                               "*", SearchOption.AllDirectories).AsParallel()
                        let upperFile = file.ToUpper()
                        let fileName = Path.GetFileName(upperFile)
                        let percentage = ContainsText(upperFile, stringList.ToArray())
                        let attributes = File.GetAttributes(file)
                        where !attributes.HasFlag(FileAttributes.Hidden)
                        where upperFile.Contains("\\" + currentCourse + "\\") | currentCourse == "ANY"
                        where upperFile.Contains("\\" + currentWeek + "\\") | currentWeek == "ANY"
                        where upperFile.Contains("\\" + currentDay + "\\") | currentDay == "ANY"
                        where ContainsQuotes(upperFile, quoteList.ToArray())
                        where percentage >= 33
                        orderby percentage descending, fileName
                        select file;
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "The search could not be completed.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Access Denied", "The search could not be completed.",
                "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "The search could not be completed.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }

            // Ensure that there is at least one file
            searchCount = files.Count();
            if (searchCount == 0)
            {
                Application.CompleteProgress();
                return;
            }

            // Add the first page to the results pane
            foreach (var file in files.Take(20))
            {
                await AddResult(file);
                await Dispatcher.BeginInvoke(new Action(() =>
                    Application.IncrementProgress(0.05)));
            }

            Application.CompleteProgress(); // Notify the user that the operation is complete
        }



        /// <summary>
        /// Determines the percentage of words that were found
        /// </summary>
        /// <param name="fileName">The full location of the file</param>
        /// <param name="stringList">The list of words</param>
        /// <returns>The percentage of words that were found</returns>
        private static double ContainsText(string fileName, string[] stringList)
        {
            string searchText = string.Join(" ", stringList);
            int matches = 0; // The total number of matches found in the file
            double percentage = 0; // The total percentage of words that were found
            int count = stringList.Count(); // The total number of words

            // If the file name or location contains the exact string, put this first
            if (fileName.Replace("\\", " ").Contains(searchText))
            {
                return 101;
            }

            // Get the percentage
            foreach (var item in stringList)
            {
                if (fileName.Contains(item))
                {
                    matches++;
                }
            }

            // Calculate the percentage
            if (count == 0)
            {
                percentage = 100;
            }
            else
            {
                percentage = (double)matches / count * 100;
            }

            // Return the percentage
            return percentage;
        }

        /// <summary>
        /// True if the filename contains quotes. Otherwise, false
        /// </summary>
        /// <param name="fileName">The file name to examine</param>
        /// <param name="quoteList">The list of quotes</param>
        /// <returns>Whether or not the file name contains all required quotes</returns>
        private static bool ContainsQuotes(string fileName, string[] quoteList)
        {
            // Enumerate through every quote.
            // If the quote doesn't exist, terminate the operation.
            foreach (var quote in quoteList)
            {
                if (!fileName.Contains(quote))
                {
                    return false;
                }
            }

            // If we've made it this far, all required quotes exist
            return true;
        }

        /// <summary>
        /// Adds the result to the results pane
        /// </summary>
        /// <param name="file">The file to add</param>
        /// <returns>A task describing the async status</returns>
        private async Task AddResult(string file)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var newListItem = new ListBoxItem()
                {
                    ToolTip = file,
                    Content = Path.GetFileName(file)
                };
                results.Items.Add(newListItem);
            }));
        }

        /// <summary>
        /// Creates the specified file and searches
        /// </summary>
        private void CreateFile()
        {
            #region Variables
            string currentCourse = course.Text;
            ComboBoxItem selectedWeek = week.SelectedItem as ComboBoxItem;
            ComboBoxItem selectedDay = day.SelectedItem as ComboBoxItem;
            string currentWeek; // The selected week
            string currentDay;  // The selected day
            StringBuilder searchLocation = new StringBuilder(fileLocation + "\\");
            string fileName; // The name of the file to be created
            string parentDirectory;
            #endregion

            #region Prep

            SearchPopup.IsOpen = false; // Close the search pop-up

            // Ensure the name is valid
            if (IOFunctions.ContainsInvalidCharacters(searchBox.Text))
            {
                string charStr = @"\ / : * ? "" < > |";
                var result = TaskDialog.ShowDialog("Invalid File Name", "The file name is invalid.",
                    string.Format("File names cannot contain the following characters:\n{0}",
                charStr), TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }

            // Get the week
            if (selectedWeek == null)
            {
                currentWeek = "ANY";
            }
            else
            {
                currentWeek = selectedWeek.Content.ToString();
            }

            // Get the day
            if (selectedDay == null)
            {
                currentDay = "ANY";
            }
            else
            {
                currentDay = selectedDay.Content.ToString();
            }

            // Ensure the file location exists
            if (!Directory.Exists(fileLocation))
            {
                TaskDialog.ShowDialog("File Error", "The file location doesn't exist.",
                    "Invalid File Location", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                return;
            }

            // Update the file location
            if (currentCourse != "Any")
            {
                searchLocation.Append(currentCourse + "\\");
            }

            if (currentWeek != "Any")
            {
                searchLocation.Append(currentWeek + "\\");
            }

            if (currentDay != "Any")
            {
                searchLocation.Append(currentDay + "\\");
            }

            // Set the new file name
            fileName = GetFileName(searchLocation);



            // Ensure that the file does not already exist
            if (File.Exists(fileName))
            {
                var result = TaskDialog.ShowDialog("Warning", "The file already exists.",
                    "Do you want to overwrite the file?", TaskDialogButtons.Yes |
                    TaskDialogButtons.No, TaskDialogIcon.Warning);
                if (result != TaskDialogResult.Yes)
                {
                    return;
                }
            }
            #endregion

            try
            {
                // Attempt to create the file and all directories in its path
                parentDirectory = Path.GetDirectoryName(fileName);
                Directory.CreateDirectory(parentDirectory);
                File.WriteAllText(fileName, "");

                // Search for all files with the new properties
                Search();
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "The file could not be created.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Access Denied", "The file could not be created.",
                "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "The file could not be created.",
                "Record Pro does not have the required permission.",
                TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }

        /// <summary>
        /// Checks if the file name contains an extension
        /// </summary>
        /// <param name="fileName">The name of the file to be examined</param>
        /// <returns>True if the file name contains an extension. Otherwise, false.</returns>
        private static bool ContainsExtension(string fileName)
        {
            int startIndex = 0;
            while (startIndex < fileName.Length)
            {
                int periodLoc = fileName.IndexOf(".", startIndex);
                if (periodLoc == -1)
                {
                    return false;
                }

                /* If the period is not the last character and the character following the
				 * extension is not a space, then an extension has been found. */
                char periodFChar = fileName[periodLoc + 1];
                if (!char.IsWhiteSpace(periodFChar))
                {
                    return true;
                }
                else
                {
                    startIndex = periodLoc + 1;
                }
            }
            return false;
        }

        /// <summary>
        /// Automatically adds an auto-generated name or extension to a file name
        /// </summary>
        /// <param name="searchLocation">The location containing the new file</param>
        /// <returns>The file name with an auto-generated name or extension if necessary</returns>
        private string GetFileName(StringBuilder searchLocation)
        {
            string fileName;

            // Automatically add a name or extension if necessary
            if (!string.IsNullOrWhiteSpace(searchBox.Text))
            {
                bool foundExtension = ContainsExtension(searchBox.Text);
                searchLocation.Append(searchBox.Text);

                // Add the .docx extension if none has been found
                if (!foundExtension)
                {
                    searchLocation.Append(".docx");
                }

                // Convert the string builder to a string
                fileName = searchLocation.ToString();
            }
            else
            {
                fileName = searchLocation.ToString();
                string tempLocation = fileName + "Assignment.docx";

                // Find a match that doesn't already exist.
                if (!File.Exists(tempLocation))
                {
                    fileName = tempLocation;
                }
                else
                {
                    for (int i = 2; i <= int.MaxValue; i++)
                    {
                        tempLocation = fileName + "Assignment " + i + ".docx";
                        if (!File.Exists(tempLocation))
                        {
                            fileName = tempLocation;
                            break;
                        }
                    }
                }
            }
            return fileName;
        }

        private void items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFiles();
        }

        private void openSearchPopup_Click(object sender, RoutedEventArgs e)
        {
            // Open the search popup
            createButton.Visibility = Visibility.Collapsed;
            searchButton.Visibility = Visibility.Visible;
            headerLabel.Content = "Search";
            SearchPopup.IsOpen = true;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Search(); // Search for assignments
        }

        private void SearchPopup_Opened(object sender, EventArgs e)
        {
            int lastItem = course.SelectedIndex; // The last item that was selected

            // Ensure the file location exists
            Directory.CreateDirectory(fileLocation);

            // Prepare the course combobox
            AddCourses();
            course.Focus();

            // Reselect the appropriate item
            if (lastItem > -1)
            {
                course.SelectedIndex = lastItem;
            }
        }

        /// <summary>
        /// Adds all courses to the combobox
        /// </summary>
        private void AddCourses()
        {
            User user = (User)grades.DataContext;
            string selectedGrade = (string)grades.SelectedItem;

            // Ensure a valid grade is selected
            if (selectedGrade == null)
            {
                return;
            }
            string location = Path.Combine(user.FileLocation,"Grades", selectedGrade + ".xml");
            var courseList = User.GetCourses(location);
            if (courseList != null)
            {
                courseList.Insert(0, "Any"); // The user can always choose any course
                course.ItemsSource = courseList;
                course.SelectedIndex = 0;
            }
        }

        private void items_KeyDown(object sender, KeyEventArgs e)
        {
            // If the delete key was pressed, delete the selected files.
            if (e.Key == Key.Delete)
            {
                DeleteFiles();
            }

            // If the enter key was pressed, open the selected files.
            else if (e.Key == Key.Enter)
            {
                OpenFiles();
            }
        }

        /// <summary>
        /// Deletes all selected files
        /// </summary>
        private async void DeleteFiles()
        {
            var files = (from ListBoxItem item in results.SelectedItems
                         let name = item.ToolTip.ToString()
                         select Task.Run(() => IOFunctions.DeleteFile(name))).ToArray();
            string message;
            int count = files.Count();
            #region WarnUser

            // Prepare warning
            if (count == 1)
            {
                message = ("Are you sure you want to delete (1) file?");
            }
            else
            {
                message = string.Format("Are you sure you want to delete ({0}) files?",
                    count);
            }

            // Warn the user
            var result = TaskDialog.ShowDialog("Delete Files?",
            message, "This is permanent and cannot be undone.",
            TaskDialogButtons.Yes | TaskDialogButtons.No, TaskDialogIcon.Warning);
            if (result != TaskDialogResult.Yes)
            {
                return;
            }
            #endregion

            // Attempt to delete the file
            await Task.WhenAll(files);

            Search(); // Update the results
        }

        private void items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Smoothly slide the file panel in and out
            if (results.SelectedItems.Count > 0)
            {
                DoubleAnimation dAnimation = new DoubleAnimation()
                {
                    To = filePanel.ActualHeight,
                    Duration = new TimeSpan(0, 0, 0, 0, 500)
                };
                filePanel.BeginAnimation(StackPanel.HeightProperty, dAnimation);
            }
            else
            {
                DoubleAnimation dAnimation = new DoubleAnimation()
                {
                    To = 0,
                    Duration = new TimeSpan(0, 0, 0, 0, 500)
                };
                filePanel.BeginAnimation(StackPanel.HeightProperty, dAnimation);
            }
        }

        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {
            DeleteFiles(); // Delete all selected items
        }

        private void openButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFiles(); // Open all items that are selected
        }

        private void openParentDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            ShowInFolder(); // Open the parent directory for each selected item
        }

        /// <summary>
        /// Open the parent directory for each selected item
        /// </summary>
        private void ShowInFolder()
        {
            var fileList = (from ListBoxItem item in results.SelectedItems
                            let name = Path.GetFileName(item.ToolTip.ToString())
                            select new
                            {
                                Location = item.ToolTip.ToString(),
                                Name = name
                            }).ToArray();

            // Open each folder
            foreach (var file in fileList)
            {
                try
                {
                    Process.Start(file.Location);
                }
                catch (Win32Exception ex)
                {
                    string message = string.Format("The folder {0} "
                    + "could not be opened.", file.Name);
                    TaskDialog.ShowDialog("File Error", message, ex.Message,
                        TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                    continue;
                }

            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the search popup
            searchButton.Visibility = Visibility.Collapsed;
            createButton.Visibility = Visibility.Visible;
            headerLabel.Content = "Create File";
            SearchPopup.IsOpen = true;
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            CreateFile(); // Create the new file
        }

        private void openFolder_Click(object sender, RoutedEventArgs e)
        {
            // Only continue if the file location exists
            if (!Directory.Exists(fileLocation))
            {
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(fileLocation);
            }
            catch (Win32Exception ex)
            {
                TaskDialog.ShowDialog("File Error", "The folder could not be opened.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }

        private void renameButton_Click(object sender, RoutedEventArgs e)
        {
            bool renamedFile = false; // True if the results must be updated
            var fileList = (from ListBoxItem item in results.SelectedItems
                            select item.ToolTip.ToString()).ToArray();

            foreach (var file in fileList)
            {
                if (IOFunctions.RenameFile(file))
                {
                    renamedFile = true;
                }
            }

            // Search  if necessary
            if (renamedFile)
            {
                Search();
            }
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Allow the user to start a new search
            course.SelectedIndex = 0;
            week.SelectedIndex = 0;
            day.SelectedIndex = 0;
            searchBox.Clear();
        }

        private void user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grades.DataContext = (User)users.SelectedItem;        }


        private void grade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGradeAsync();
        }

        private void PreviousPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                e.CanExecute = true;
            }
        }

        private void NextPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (files != null && searchCount > currentPage * 20)
            {
                e.CanExecute = true;
            }
        }

        private async void PreviousPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (files != null && currentPage > 1)
            {
                results.Items.Clear();
                currentPage--;

                Application.PrepareProgress("Loading Files"); // Prepare progress notifications

                // Add the files to the results pane and update the current page
                foreach (var file in files.Skip((currentPage - 1) * 20).Take(20))
                {
                    await AddResult(file);
                    await Dispatcher.BeginInvoke(new Action(() =>
                        Application.IncrementProgress(0.05)));
                }

                Application.CompleteProgress(); // Notify the user that the operation is complete
            }
        }

        private async void NextPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (files != null && currentPage * 20 < searchCount)
            {
                results.Items.Clear();

                Application.PrepareProgress("Loading Files"); // Prepare progress notifications

                // Add the files to the results pane and update the current page
                foreach (var file in files.Skip(currentPage++ * 20).Take(20))
                {
                    await AddResult(file);
                    await Dispatcher.BeginInvoke(new Action(() =>
                        Application.IncrementProgress(0.05)));
                }

                Application.CompleteProgress(); // Notify the user that the operation is complete
            }
        }
    }
}
