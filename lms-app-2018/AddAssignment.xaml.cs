using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Path = System.IO.Path;

namespace RecordPro
{
    /// <summary>
    /// Interaction logic for ModifyAssignment.xaml
    /// </summary>
    public partial class AddAssignment : Window
    {
        /// <summary>
        /// Represents the assignment object.
        /// </summary>
        public Assignment Assignment { get; protected set; }

        /// <summary>
        /// Represents the current user
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// Initializes a new instance of AddAssignment, and is used for modifying an assignment
        /// </summary>
        /// <param name="user">The user who completed the assignment</param>
        /// <param name="assignment">The assignment that the user completed</param>
        public AddAssignment(User user, Assignment assignment)
        {
            InitializeComponent();

            // Set up the dialog
            Assignment = new Assignment(assignment.Course, assignment.Date,
                assignment.Details, assignment.Grade, assignment.Notes,
                assignment.AssistanceNeeded, assignment.Time,
            assignment.AssignmentType, assignment.FileLocation, assignment.GradeLevel);

            // Update data context etc. Since the assignment already existed,
            // there is need to check for a blank course or grade level
            UpdateUser(user);
            this.DataContext = Assignment; // Load the current assignment
            okButton.DataContext = Assignment;
        }

        /// <summary>
        /// Initializes a new instance of AddAssignment, and is used for adding an assignment
        /// </summary>
        /// <param name="user">The user who completed the assignment</param>
        public AddAssignment(User user)
        {
            InitializeComponent();
            LoadUsers(); // Load the list of users. The current user will be selected by default.
        }

        /// <summary>
        /// Loads data binding for the current user
        /// </summary>
        /// <param name="user"></param>
        private void UpdateUser(User user)
        {
            User = user;
            userImage.DataContext = user;

            // Update spell checking settings
            var binding = new Binding("Spellcheck") { Source = user };
            notes.SetBinding(SpellCheck.IsEnabledProperty, binding);
            details.SetBinding(SpellCheck.IsEnabledProperty, binding);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog, since the user has saved the assignment
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Loads a list of all users and automatically selects the current user.
        /// </summary>
        private void LoadUsers()
        {
            // Acquire the list of students
            string usersLocation = Application.Current.Properties["Users Location"].ToString();
            var students = new Collection<User>((from student in Directory.EnumerateDirectories(usersLocation).AsParallel()
                                                 where User.UserIsStudent(student)
                                                 let user = User.GetUser(student)
                                                 orderby user.UserName
                                                 select user).ToArray());

            // Load all users and automatically select the current user.
            UserComboBox.DataContext = students;
            UserComboBox.SelectedIndex = 0;

            // Show the combo-box if necessary
            User currentUser = (User)Application.Current.Properties["Current User Information"];
            if (currentUser.IsTeacher)
            {
                UserComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                UserComboBox.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Loads the current user
        /// </summary>
        /// <param name="user">The user to load</param>
        private void LoadUser(User user)
        {
            string fileLocation = Path.Combine(user.FileLocation, "Grades", user.RecentGradeLevel + ".xml");
            Assignment = new Assignment(fileLocation, user.RecentGradeLevel, user.RecentCourse);
            Assignment.Date.Add(DateTime.Today);

            // Update data context etc. Since the assignment already existed,
            // there is need to check for a blank course or grade level
            UpdateUser(user);
            this.DataContext = Assignment; // Load the current assignment
            okButton.DataContext = Assignment;

            // Ensure the user has at least one grade level
            if (user.Grades.Count == 0)
            {
                TaskDialog.ShowDialog("No grade", "You don't have any grades.",
                    "Please add a grade by clicking the manage grades link on the home screen.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                DialogResult = false;
                Close();
            }
            else if (string.IsNullOrEmpty(Assignment.GradeLevel))
            {
                Assignment.GradeLevel = (string)gradeLevel.Items[0]; // Automatically select an item if necessary
            }
        }

        private void UserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedItem = (User)UserComboBox.SelectedItem;
            LoadUser(selectedItem); // Load all info for the current user
        }

        private void gradeLevel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            gradeLevel.ItemsSource = User.Grades; // Automatically update the grade level items source
        }
        private void addDetails_Click(object sender, RoutedEventArgs e)
        {
            details.Text = "Enter details here";
        }

        private void addtime_Click(object sender, RoutedEventArgs e)
        {
            time.Time = new TimeSpan(0, 5, 0);
        }

        private void addGrade_Click(object sender, RoutedEventArgs e)
        {
            grade.Value = 80;
        }

        private void addNotes_Click(object sender, RoutedEventArgs e)
        {
            notes.Text = "Enter comments here";
        }

        private void addDate_Click(object sender, RoutedEventArgs e)
        {
            var collection = (ObservableCollection<DatePicker>)datesPanel.ItemsSource;
            collection.Add(new DatePicker() { SelectedDate = DateTime.Today, Margin = new Thickness(5, 0, 5, 0) });
            UpdateDate();
        }

        private void datesPanel_KeyDown(object sender, KeyEventArgs e)
        {
            var collection = (ObservableCollection<DatePicker>)datesPanel.ItemsSource;
            if (e.Key == Key.Delete | e.SystemKey == Key.Delete && collection.Count > 1)
            {
                collection.Remove((DatePicker)datesPanel.SelectedItem);
            }
            UpdateDate();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDate();
        }

        private void UpdateDate()
        {
            var expression = datesPanel.GetBindingExpression(ListBox.ItemsSourceProperty);
            expression.UpdateSource();
        }

        private void gradeLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If the grade level is null, don't dock the user for not having a course.
            if (course.Items.Count == 0 && gradeLevel.SelectedItem != null)
            {
                TaskDialog.ShowDialog("No course", "You don't have any courses.",
                    "Please add a course by clicking the manage grades link on the home screen. Then select your course.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            else if (course.Items.Count > 0 && string.IsNullOrEmpty(Assignment.Course))
            {
                course.SelectedIndex = 0; // Automatically select an item if necessary
            }

            // Update the okay button
            UpdateOkButton();
        }

        private void UpdateOkButton()
        {
            var expression = okButton.GetBindingExpression(Button.IsEnabledProperty);
            expression.UpdateTarget();
        }
    }
}
