
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shell;






namespace RecordPro
{
	/// <summary>
	/// Interaction logic for AddStudentsDialog.xaml
	/// </summary>
	public partial class AddStudentsDialog : Window
	{
		public string SelectedUser { get; set; }
		public string SelectedLocation { get; set; }

		public AddStudentsDialog()
		{
			InitializeComponent();
		}
		private void dialog_Loaded(object sender, RoutedEventArgs e)
		{
			LoadStudents();
		}

		/// <summary>
		/// Load a list of all users
		/// </summary>
		private void LoadStudents()
		{
			string usersLocation = (string)Application.Current.Properties["Users Location"];

			// Update the progress
			Application.PrepareProgress("Loading Students");
			try
			{
				DirectoryInfo newDirectoryInfo = new DirectoryInfo(usersLocation);
				int count = newDirectoryInfo.GetDirectories().Length;

				// Only continue if there is at least one user
				if (count == 0)
                {
                    return;
                }

                var userList = (Collection<string>)Application.Current.Properties["Students"];
				double progressUpdateValue = 1 / count;
				foreach (var folder in newDirectoryInfo.EnumerateDirectories())
				{
					// Only proceed if the user is not part of the administrator's profile
					if (userList.Contains(folder.Name))
                    {
                        continue;
                    }

                    string data;
					string location = System.IO.Path.Combine(folder.FullName, "config.txt");
					using (var newReader = new StreamReader(location))
                    {
                        data = newReader.ReadToEnd();
                    }

                    string name = StringFunctions.GetValue(data, "Name");

					Button newButton = new Button() { Content = name, Tag = folder.Name };
					newButton.Click += newButton_Click;
					students.Children.Add(newButton);

					// Update progress
					if (Application.mWindow != null)
                    {
                        Application.mWindow.progress.Value += progressUpdateValue;
                    }
                }
			}
			catch (IOException)
			{
				TaskDialog.ShowDialog("File Error", "An error has occurred. ",
					"The list of users could not be retrieved.",
					TaskDialogButtons.Ok, TaskDialogIcon.Error);
			}
			catch (SecurityException)
			{
				TaskDialog.ShowDialog("File Error", "The list of users could not be retrieved",
					"The program does not have the appropriate permission.",
					TaskDialogButtons.Ok, TaskDialogIcon.Error);
			}

			// Update the progress
			Application.CompleteProgress();
		}

		void newButton_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			if (button != null)
			{
				SelectedUser = button.Content.ToString();
				SelectedLocation = button.Tag.ToString();
				this.DialogResult = true;
				this.Close();
			}
		}

		private void SystemCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void SystemCommands_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Command == SystemCommands.CloseWindowCommand)
            {
                SystemCommands.CloseWindow(this);
            }
        }

	}
}
