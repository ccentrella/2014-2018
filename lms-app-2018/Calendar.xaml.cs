using System;
using System.IO;
using Path = System.IO.Path;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace RecordPro
{
	/// <summary>
	/// Interaction logic for Calendar.xaml
	/// </summary>
	public partial class Calendar : Page
	{
		Collection<User> users;

		public Calendar()
		{
			InitializeComponent();
		}

		private void Page_Initialized(object sender, EventArgs e)
		{
			var currentUser = (User)Application.Current.Properties["Current User Information"];
			toggleDetails.IsChecked = (bool)currentUser.ShowCalendarDetails;
			calendar.SelectedDate = DateTime.Today;
			detailsPane.DataContext = null;
			UpdateInterface();
		}

		private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateRecords(calendar.SelectedDate);
			Mouse.Capture(null);
		}

		/// <summary>
		/// Update all records for the current day
		/// </summary>
		private void UpdateRecords(DateTime? date)
		{
			UpdateDetailsPane();

			if (date == null)
            {
                return;
            }

            DateTime selectedDay = (DateTime)date;
			string usersLocation = (string)Application.Current.Properties["Users Location"];
			users = new Collection<User>((from user in Directory.EnumerateDirectories(usersLocation).AsParallel()
										  where User.UserIsStudent(user)
										  let newUser = new User(user, selectedDay)
										  where newUser.Assignments.Count > 0
										  orderby newUser.UserName
										  select newUser).ToArray());
			this.DataContext = users;
		}

		/// <summary>
		/// Gets the user that completed the assignment.
		/// </summary>
		/// <param name="assignment">The assignment that the user completed</param>
		/// <returns>The user that completed the assignment.
		/// If the user could not be found, null is returned.</returns>
		private User GetUser(Assignment assignment)
		{
			foreach (var user in users)
            {
                if (user.Assignments.Contains(assignment, new AssignmentComparer()))
                {
                    return user;
                }
            }

            return null;
		}

		
		private void AssignmentCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			// First, make sure that the page has been fully loaded
			if (!IsInitialized)
            {
                e.CanExecute = false;
            }
            else if (recordPane.SelectedValue != null &&
				recordPane.SelectedValue.GetType() == typeof(Assignment))
            {
                e.CanExecute = true;
            }
        }

		/// <summary>
		/// Updates the details pane
		/// </summary>
		private void UpdateDetailsPane()
		{
			var value = recordPane.SelectedValue;
			if (value != null && value.GetType() == typeof(Assignment))
			{
				var user = GetUser((Assignment)recordPane.SelectedValue);
				userImage.DataContext = user;
				userLabel.DataContext = user;
				detailsPane.DataContext = value;
			}
			else
			{
				detailsPane.DataContext = null;
				userImage.DataContext = null;
				userLabel.DataContext = null;
			}
		}

		private void ShowCalendar_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (IsInitialized && calendar.SelectedDate != DateTime.Today)
            {
                e.CanExecute = true;
            }
        }
		private void ShowCalendar_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			calendar.SelectedDate = DateTime.Today;
            calendar.DisplayDate = DateTime.Today;
		}

		private void ModifyAssignment_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Modify((Assignment)recordPane.SelectedValue);
		}
		private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var selectedItem = (Assignment)recordPane.SelectedValue;
			try
			{
				Clipboard.SetText(selectedItem.Details);
			}
			catch (COMException)
			{
				TaskDialog.ShowDialog("Clipboard in use", "The details could not be copied.",
					"The clipboard is in use.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
		}
		private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Delete((Assignment)recordPane.SelectedValue);
		}

		/// <summary>
		/// Modifies an assignment
		/// </summary>
		/// <param name="assignment">The assignment to modify</param>
		private void Modify(Assignment assignment)
		{
			var user = GetUser(assignment);
			var newModify = new AddAssignment(user, assignment) { Owner = Application.mWindow,
				Title = "Modify Assignment - Record Pro" };
			newModify.okButton.Content = "_Update";
			if (newModify.ShowDialog() == true)
            {
                UpdateAssignment(assignment, newModify.Assignment);
            }
        }

		/// <summary>
		/// Deletes the selected item
		/// </summary>
		/// <param name="source">The item to be deleted</param>
		private void Delete(Assignment source)
		{
			// Only continue if the user agrees to delete the files
			var result = TaskDialog.ShowDialog("Delete Assignment?",
				"Are you sure you want to delete this assignment?", "This cannot be undone.",
				TaskDialogButtons.Yes | TaskDialogButtons.No, TaskDialogIcon.Warning);

			if (result == TaskDialogResult.No)
            {
                return;
            }

            try
			{
				var assignments = Assignment.LoadAssignmentFile(source.FileLocation);
				foreach (var assignment in assignments)
                {
                    if (source.Equals(assignment))
					{
						// Remove the assignment
						assignments.Remove(assignment);
						Assignment.SaveAssignmentFile(source.FileLocation, assignments);
						UpdateRecords(calendar.SelectedDate);
						break;
					}
                }
            }
			catch (IOException ex)
			{
				TaskDialog.ShowDialog("Warning", "The assignment could not be deleted.",
					ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (UnauthorizedAccessException)
			{
				TaskDialog.ShowDialog("Warning", "The assignment could not be deleted.",
					"Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (SecurityException)
			{
				TaskDialog.ShowDialog("Warning", "The assignment could not be deleted.",
					"The program does not have the required permission.",
					TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
		}

		/// <summary>
		/// Updates an assignment
		/// </summary>
		/// <param name="oldAssignment">The old assignment</param>
		/// <param name="newAssignment">The new assignment</param>
		private void UpdateAssignment(Assignment oldAssignment, Assignment newAssignment)
		{
			var oldList = Assignment.LoadAssignmentFile(oldAssignment.FileLocation);
			oldList.Remove(oldAssignment);
			if (newAssignment.FileLocation == oldAssignment.FileLocation)
            {
                oldList.Add(newAssignment);
            }
            else
			{
				var newList = Assignment.LoadAssignmentFile(newAssignment.FileLocation);
				newList.Add(newAssignment);
				Assignment.SaveAssignmentFile(newAssignment.FileLocation, newList);
			}
			Assignment.SaveAssignmentFile(newAssignment.FileLocation, oldList);
			UpdateRecords(calendar.SelectedDate);
		}

		private void recordPane_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			UpdateDetailsPane();
		}

		private void toggleDetails_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (toggleDetails.IsChecked == true)
			{
				UpdateInterface();
			}
			else
			{
				detailsDefinition.Width = new GridLength(0.0);
			}

			var currentUser = (User)Application.Current.Properties["Current User Information"];
			currentUser.ShowCalendarDetails = (bool)toggleDetails.IsChecked;
			Application.Current.Properties["Current User Information"] = currentUser;
		}

		/// <summary>
		/// Load the user's interface
		/// </summary>
		private void UpdateInterface()
		{
			var currentUser = (User)Application.Current.Properties["Current User Information"];
			detailsDefinition.Width = currentUser.CalendarDetailsWidth;
			calendarDefinition.Width = currentUser.CalendarCalendarWidth;
			treeViewDefinition.Width = currentUser.CalendarTreeViewWidth;
		}

		private void recordPaneSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
		{
			SaveInterface();
		}

		/// <summary>
		/// Saves the interface to the user's settings
		/// </summary>
		private void SaveInterface()
		{
			var currentUser = (User)Application.Current.Properties["Current User Information"];
			currentUser.CalendarDetailsWidth = detailsDefinition.Width;
			currentUser.CalendarCalendarWidth = calendarDefinition.Width;
			currentUser.CalendarTreeViewWidth = treeViewDefinition.Width;
			Application.Current.Properties["Current User Information"] = currentUser;
		}

		private void detailsSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
		{
			SaveInterface();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			var currentUser = (User)Application.Current.Properties["Current User Information"];
			if (currentUser.ShowCalendarPopup)
			{
				var ad = new Ad("View everything in a flash", new CalendarAd()) { Owner = Application.mWindow };
				if (ad.ShowDialog() == true)
                {
                    currentUser.ShowCalendarPopup = false;
                }
            }
		}
	}
}
