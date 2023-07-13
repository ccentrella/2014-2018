using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace RecordPro
{
	/// <summary>
	/// Interaction logic for ManageCourses.xaml
	/// </summary>
	public partial class ManageGrades : Page
	{
		public ManageGrades()
		{
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			LoadUsers();
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
			this.DataContext = students;
            var currentUser = (User)Application.Current.Properties["Current User Information"];
            UserComboBox.SelectedIndex = 0;

			// Show the combo-box if necessary
			if (isTeacher)
			{
				UserComboBox.Visibility = Visibility.Visible;
			}
			else
			{
				UserComboBox.Visibility = Visibility.Collapsed;
			}
		}

		private void UserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			User selectedItem = (User)UserComboBox.SelectedItem;
			LoadGrades(Path.Combine(selectedItem.FileLocation,"Grades")); // Load all grades for the current user
		}

		/// <summary>
		/// Load the user's grade
		/// </summary>
		/// <param name="folderLocation">The folder that contains the user's grades</param>
		private void LoadGrades(string folderLocation)
		{
			courses.ItemsSource = null;

			// Only continue if the directory exists
			if (!Directory.Exists(folderLocation))
			{
				return;
			}

			try
			{
				DirectoryInfo newDirectoryInfo = new DirectoryInfo(folderLocation);
				var files = new Collection<RPGrade>((from file in newDirectoryInfo.GetFiles()
							let name = Path.GetFileNameWithoutExtension(file.FullName)
							select new RPGrade(name, file.FullName)).ToArray());
				grades.ItemsSource = files;
			}
			catch (IOException ex)
			{
				TaskDialog.ShowDialog("Error - Record Pro", "Some grades could not be loaded.", ex.Message,
					TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (SecurityException)
			{
				TaskDialog.ShowDialog("Error - Record Pro", "Some grades could not be loaded.",
					"The program does not have the required permission.",
					TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
		}

		private void NewGrade_Click(object sender, RoutedEventArgs e)
		{
			// Prompt the user to enter the name of the new grade.
			var newDialog = new InputDialog("Enter Grade Name", "Please enter the name of the new grade:");
			newDialog.Owner = Application.mWindow;
			if (newDialog.ShowDialog() == true)
            {
                CreateGrade(newDialog.userInput.Text);
            }
        }

		/// <summary>
		/// Add the new grade to the user's profile.
		/// </summary>
		/// <param name="name">The name of the new grade</param>
		private void CreateGrade(string name)
		{
			string fileLocation;

			// Acquire the file location
			User selectedItem = (User)UserComboBox.SelectedItem;
			fileLocation = Path.Combine(selectedItem.FileLocation, "Grades", name + ".xml");

			// Ensure the grade doesn't already exist
			if (File.Exists(fileLocation))
			{
				var result = TaskDialog.ShowDialog("Grade Already Exists", "The grade already exists.",
					"Please enter a new grade name.", TaskDialogButtons.Ok | TaskDialogButtons.Cancel,
					TaskDialogIcon.Warning);
				if (result == TaskDialogResult.Ok)
				{
					// Prompt the user to enter the name of the new grade.
					var newDialog = new InputDialog("Enter Grade Name", "Please enter the name of the new grade:");
					newDialog.Owner = Application.mWindow;
					if (newDialog.ShowDialog() == true)
                    {
                        CreateGrade(newDialog.userInput.Text);
                    }
                }
				else
                {
                    return;
                }
            }

			try
			{
				// Create the grade
				XElement element = new XElement("Document");
				element.SetElementValue("Courses", "");
				element.SetElementValue("Assignments", "");
				element.Save(fileLocation);
				LoadGrades(Path.Combine(selectedItem.FileLocation,"Grades"));
			}
			catch (IOException ex)
			{
				TaskDialog.ShowDialog("Error - Record Pro", "The grade could not be created.",
					ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (UnauthorizedAccessException)
			{
				TaskDialog.ShowDialog("Error - Record Pro", "The grade could not be created.",
					"Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (SecurityException)
			{
				TaskDialog.ShowDialog("Error - Record Pro", "The grade could not be created.",
					"The program does not have the required permission.");
			}
		}

		private void RenameGradeButton_Click(object sender, RoutedEventArgs e)
		{
			var renameDialog = new InputDialog("Enter Grade Name", "Please enter the new grade name:")
			{
				Owner = Application.mWindow
			};
			if (renameDialog.ShowDialog() == true)
			{
				// Ensure the name is valid
				if (IOFunctions.ContainsInvalidCharacters(renameDialog.userInput.Text)
					|| string.IsNullOrWhiteSpace(renameDialog.userInput.Text))
				{
					string charStr = @"\ / : * ? "" < > |";
					var result = TaskDialog.ShowDialog("Invalid File Name", "The file name is invalid.",
						string.Format("File names cannot contain the following characters:\n{0}",
					charStr), TaskDialogButtons.Ok, TaskDialogIcon.Warning);
				}
				RenameGrade(renameDialog.userInput.Text); // Rename the file
			}
		}

		/// <summary>
		/// Renames the appropriate grade
		/// </summary>
		/// <param name="fileName">The name of the new grade</param>
		private void RenameGrade(string gradeName)
		{
			var selectedGrade = (RPGrade)grades.SelectedItem;
			string oldFileLocation = selectedGrade.Location.ToString();
			string gradeLocation = Path.GetDirectoryName(oldFileLocation);
			string newFileLocation = Path.Combine(gradeLocation, gradeName + ".xml");

			// Ensure the file doesn't already exist. If the user enters the same name as before, silently ignore it.
			if (File.Exists(newFileLocation) && newFileLocation != oldFileLocation)
			{
				var result = TaskDialog.ShowDialog("Grade Already Exists", "The grade already exists.",
				"Please enter a new grade name.", TaskDialogButtons.Ok | TaskDialogButtons.Cancel,
				TaskDialogIcon.Warning);
				if (result == TaskDialogResult.Ok)
				{
					// Prompt the user to enter the name of the new grade.
					var newDialog = new InputDialog("Enter Grade Name", "Please enter the name of the new grade:");
					newDialog.Owner = Application.mWindow;
					if (newDialog.ShowDialog() == true)
                    {
                        RenameGrade(newDialog.userInput.Text);
                    }
                }
				return;
			}
			else if (File.Exists(newFileLocation))
			{
				return;
			}

			try
			{
				// Rename the file and update the list box
				File.Move(oldFileLocation, newFileLocation);
				LoadGrades(gradeLocation);
			}
			catch (IOException ex)
			{
				TaskDialog.ShowDialog("Warning - Record Pro", "The grade could not be renamed.", ex.Message,
					TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (UnauthorizedAccessException)
			{
				TaskDialog.ShowDialog("Warning - Record Pro", "The grade could not be renamed.",
					"Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
		}

		private void grades_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Delete)
            {
                DeleteSelectedGrade(); // Remove the selected grade
            }
        }

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			DeleteSelectedGrade(); // Delete the selected grade
		}

		/// <summary>
		/// Deletes the selected file
		/// </summary>
		private void DeleteSelectedGrade()
		{
			var selectedGrade = (RPGrade)grades.SelectedItem;
			string location = selectedGrade.Location.ToString();
			string gradeLocation = Path.GetDirectoryName(location);

			// Warn the user before continuing
			var result = TaskDialog.ShowDialog("Delete Grade?", "Are you sure you want to delete the grade?",
				"This is permanent and cannot be undone.", TaskDialogButtons.Yes | TaskDialogButtons.No,
				TaskDialogIcon.Warning);
			if (result == TaskDialogResult.No)
            {
                return;
            }

            // Begin the operation
            try
			{
				File.Delete(location);
				LoadGrades(gradeLocation);
			}
			catch (IOException ex)
			{
				TaskDialog.ShowDialog("Error - Record Pro", "The grade could not be deleted.", ex.Message,
					TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (UnauthorizedAccessException)
			{
				TaskDialog.ShowDialog("Error - Record Pro", "The grade could not be deleted.",
					"Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
		}

		private void grades_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Animate the grade panel and show all courses for the current grade
			DoubleAnimation newAnimation = new DoubleAnimation() { Duration = TimeSpan.Parse("0:0:0.5") };
			if (grades.SelectedItems.Count > 0)
			{
				var selectedItem = (RPGrade)grades.SelectedItem;
				LoadCourses(selectedItem.Location);
				newAnimation.To = 150;
			}
			// Close the grade panel
			else
			{
				newAnimation.To = 0;
				courses.ItemsSource = null; // Clear all courses and hide the pane
			}
			gradeOptions.BeginAnimation(StackPanel.MaxHeightProperty, newAnimation);
		}

		/// <summary>
		/// Load all courses for the specified grade
		/// </summary>
		/// <param name="gradeLocation">The location of the folder containing the user's files</param>
		/// <returns>True if the operation succeeded. Otherwise, false.</returns>
		private bool LoadCourses(string gradeLocation)
		{
			var courseList = User.GetCourses(gradeLocation);
			courses.ItemsSource = courseList;
			return courseList != null;
		}

		private void courses_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Animate the course panel
			DoubleAnimation newAnimation = new DoubleAnimation() { Duration = TimeSpan.Parse("0:0:0.5") };
			if (courses.SelectedItems.Count > 0)
            {
                newAnimation.To = 150;
            }
            else
            {
                newAnimation.To = 0;
            }

            courseOptions.BeginAnimation(StackPanel.MaxHeightProperty, newAnimation);
		}

		private void NewCourseButton_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = (RPGrade)grades.SelectedItem;

			// Prompt the user to enter the name of the new course.
			var newDialog = new InputDialog("Enter Course Name", "Please enter the name of the new course:")
			{
				Owner = Application.mWindow
			};

			// If the user clicks OK, add the new course
			if (newDialog.ShowDialog() == true)
            {
                CreateCourse(newDialog.userInput.Text, selectedItem.Location);
            }
        }

		/// <summary>
		/// Adds a new course
		/// </summary>
		/// <param name="courseName">The name of the course to add</param>
		/// <param name="gradeLocation">The location of the grade file</param>
		private void CreateCourse(string courseName, string gradeLocation)
		{
			var courseList = from string item in courses.ItemsSource
							 select item;
			if (courseList.Contains(courseName))
			{
				var result = TaskDialog.ShowDialog("Course Already Exists", "The course already exists.",
					"Please enter a new course name.", TaskDialogButtons.Ok | TaskDialogButtons.Cancel
					, TaskDialogIcon.Warning);
				if (result == TaskDialogResult.Ok)
				{
					// Prompt the user to enter the name of the new grade.
					var newDialog = new InputDialog("Enter Grade Name", "Please enter the name of the new grade:");
					newDialog.Owner = Application.mWindow;
					if (newDialog.ShowDialog() == true)
                    {
                        CreateCourse(newDialog.userInput.Text, gradeLocation);
                    }
                }
				return;
			}

			// Now begin the operation
			var newList = new List<string>(courseList);
			newList.Add(courseName);
			if (User.SetCourses(gradeLocation, new Collection<string>(newList)))
			{
				LoadCourses(gradeLocation);
			}
		}

		private void RenameCourse_Click(object sender, RoutedEventArgs e)
		{
			var selectedGrade = (RPGrade)grades.SelectedItem;

			// Prompt the user for a new name
			var newDialog = new InputDialog("Rename Course", "Please enter the new course name:");
			if (Application.mWindow != null)
            {
                newDialog.Owner = Application.mWindow;
            }

            // If OK was clicked, rename the course
            if (newDialog.ShowDialog() == true)
            {
                RenameCourse(newDialog.userInput.Text, selectedGrade.Location);
            }
        }

		/// <summary>
		/// Renames the selected course
		/// </summary>
		/// <param name="courseName">The new name of the course</param>
		/// <param name="gradeLocation">The location of the grade file</param>
		private void RenameCourse(string courseName, string gradeLocation)
		{
			var selectedItem = (string)courses.SelectedItem;
			var courseList = from string item in courses.ItemsSource
							 select item;
			if (courseList.Contains(courseName) && selectedItem != courseName)
			{
				var result = TaskDialog.ShowDialog("Course Already Exists", "The course already exists.",
					"Please enter a new course name.", TaskDialogButtons.Ok | TaskDialogButtons.Cancel,
					TaskDialogIcon.Warning);
				if (result == TaskDialogResult.Ok)
				{
					// Prompt the user to enter the name of the new grade.
					var newDialog = new InputDialog("Enter Grade Name", "Please enter the name of the new grade:");
					newDialog.Owner = Application.mWindow;
					if (newDialog.ShowDialog() == true)
                    {
                        RenameCourse(newDialog.userInput.Text, gradeLocation);
                    }
                }
				return;
			}

			// Now begin the operation
			var newList = new List<string>(courseList);
			newList.Remove(selectedItem);
			newList.Add(courseName);
			if (User.SetCourses(gradeLocation, new Collection<string>(newList)))
			{
				User.RenameCourses(gradeLocation, selectedItem, courseName);
				LoadCourses(gradeLocation);
			}
		}

		private void courses_KeyDown(object sender, KeyEventArgs e)
		{
			// Remove the selected course
			DeleteSelectedCourse();
		}

		private void DeleteCourse_Click(object sender, RoutedEventArgs e)
		{
			// Remove the selected course
			DeleteSelectedCourse();
		}

		/// <summary>
		/// Deletes the selected course
		/// </summary>
		private void DeleteSelectedCourse()
		{
			var selectedGrade = (RPGrade)grades.SelectedItem;
			string selectedCourse = (string)courses.SelectedItem;
			string gradeLocation = selectedGrade.Location;
			var courseList = from string item in courses.ItemsSource
							 select item;

			// Warn the user before continuing
			var result = TaskDialog.ShowDialog("Delete Grade?", "Are you sure you want to delete the grade?",
				"This is permanent and cannot be undone.", TaskDialogButtons.Yes | TaskDialogButtons.No,
				TaskDialogIcon.Warning);
			if (result == TaskDialogResult.No)
            {
                return;
            }

            // Now begin the operation
            var newList = new List<string>(courseList);
			newList.Remove(selectedCourse);
			if (User.SetCourses(gradeLocation, new Collection<string>(newList)))
			{
				User.DeleteCourses(gradeLocation, selectedCourse);
				LoadCourses(gradeLocation);
			}
		}
	}
}
