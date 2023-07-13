using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace RecordPro
{
    public class User
    {
        private string _image;

        /// <summary>
        /// Creates and sets a new user
        /// </summary>
        /// <param name="location">The location of the new user</param>
        public User(string location)
        {
            #region SetDefaultValues
            RecentWidth = new GridLength(150, GridUnitType.Star);
            FunctionsWidth = new GridLength(300, GridUnitType.Star);
            AppsWidth = new GridLength(200, GridUnitType.Star);
            CalendarCalendarWidth = new GridLength(2, GridUnitType.Star);
            CalendarDetailsWidth = new GridLength(1, GridUnitType.Star);
            CalendarTreeViewWidth = new GridLength(1, GridUnitType.Star);
            ShowCalendarDetails = true;
            ShowHomePopup = true;
            ShowCalendarPopup = true;
            ShowDeleteAccountButton = true;
            Gender = Gender.Unknown;
            FileLocation = location;
            SetGrades(location);
            SetReportCards(location);
            SearchCount = 10;
            AutoSearch = true;
            #endregion

            string data = GetUserData(location);
            ContactInfo = new ContactInfo(data);

            // Initialize each value
            var props = from prop in this.GetType().GetProperties()
                        where prop.CanWrite
                        where prop.Name != "StorageLocation"
                        where prop.Name != "Grades"
                        let propName = prop.Name
                        let propValue = data.GetValue(propName)
                        where !string.IsNullOrWhiteSpace(propValue) || prop.GetValue(this) == null
                        select new { PropertyInfo = prop, Value = propValue };

            foreach (var prop in props)
            {
                SetValue(prop.PropertyInfo, prop.Value, this);
            }
        }

        /// <summary>
        /// Determines if the user is able to view records for this student
        /// </summary>
        /// <param name="userLocation">The location of the user</param>
        /// <returns>True if the user has control over this student. Otherwise, false.</returns>
        public static bool UserIsStudent(string userLocation)
        {
            string userName = Path.GetFileName(userLocation);
            User currentUser = (User)Application.Current.Properties["Current User Information"];
            string currentUserLocation = (string)Application.Current.Properties["Current User Location"];
            var userList = currentUser.Students;
            string currentUserName = Path.GetFileName(currentUserLocation);
            if (userName == currentUserName | (userList!= null && userList.Contains(userName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Set all of the user's report cards
        /// </summary>
        /// <param name="location">The location containing the user's information</param>
        void SetReportCards(string location)
        {
            ReportCards = new ObservableCollection<ReportCard>();
            var gradeLocation = Path.Combine(location, "Grades");

            try
            {
                // Get report cards
                var dirInfo = new DirectoryInfo(gradeLocation);
                var grades = from file in dirInfo.GetFiles()
                             where file.Extension == ".xml"
                             orderby file.LastWriteTime
                             select new ReportCard(file.FullName);

                // Add report card
                foreach (var grade in grades)
                {
                    ReportCards.Add(grade);
                }

                // Set default report card
                if (grades.Count() > 0)
                {
                    CurrentReportCard = grades.ElementAt(0);
                }
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "Report cards could not be loaded.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("File Error", "Report cards could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "Report cards could not be loaded.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }

        public static bool IsCurrentUser(User user)
        {
            return true;
        }

        /// <summary>
        /// Returns the current user or gets a new user object.
        /// </summary>
        /// <param name="fileLocation">The location of the student file.</param>
        /// <returns>A user object, either the current user or a new user.</returns>
        public static User GetUser(string fileLocation)
        {
            User currentUser = (User)Application.Current.Properties["Current User Information"];
            if (currentUser.FileLocation == fileLocation)
            {
                return new User(currentUser) { UserName = "(Me)" };
            }
            else
            {
                return new User(fileLocation);
            }
        }

        internal static (string name, string userName) GetNameAndUserName(string fileLocation)
        {
            string data = GetUserData(fileLocation);
            return (data.GetValue("Name"), data.GetValue("UserName"));
        }

        /// <summary>
        /// Creates and sets a new user. This overload also loads 
        /// all of the user's assignment for the selected day.
        /// </summary>
        /// <param name="location">The location of the new user</param>
        /// <param name="selectedDay">The current day to load assignments from</param>
        public User(string location, DateTime selectedDay)
            : this(location)
        {
            SelectedDay = selectedDay;
            LoadAssignments(location, selectedDay);
        }

        /// <summary>
        /// Creates a new user instance using another instance
        /// </summary>
        /// <param name="user">The original user instance</param>
        public User(User user)
        {
            foreach (var prop in user.GetType().GetProperties())
            {
                if (prop.CanWrite)
                {
                    prop.SetValue(this, prop.GetValue(user));
                }
            }
        }

        /// <summary>
        /// Gets a list of the user's courses for a specified grade
        /// </summary>
        /// <param name="location">The location of the user's grade file</param>
        /// <returns>An array containing all of the user's courses for the specified grade.
        /// If the user's grade file is not supported, null will be returned.</returns>
        public static Collection<string> GetCourses(string location)
        {
            try
            {
                XDocument doc = XDocument.Load(location);
                XElement element = doc.Element("Document").Element("Courses");
                if (element != null)
                {
                    var courses = from item in element.Value.EnumerateStrings()
                                  orderby item
                                  select item;
                    return new Collection<string>(courses.ToList());
                }
                else
                {
                    return null;
                }
            }
            catch (XmlException)
            {
                TaskDialog.ShowDialog("Warning", "The list of courses could not be loaded.",
                    "The file is not formatted properly.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Warning", "The list of courses could not be loaded.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Warning", "The list of courses could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Warning", "The list of courses could not be loaded.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return null; // If we reach this point, an error occurred
        }

        public static bool SetCourses(string location, Collection<string> courseList)
        {
            string courseString = string.Join(",", courseList);
            try
            {
                XDocument doc = XDocument.Load(location);
                doc.Element("Document").Element("Courses").SetValue(courseString);
                doc.Save(location);
                return true; // The operation succeeded
            }
            catch (XmlException)
            {
                TaskDialog.ShowDialog("Warning", "The list of courses could not be saved.",
                    "The file is not formatted properly.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Warning", "The list of courses could not be saved.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Warning", "The list of courses could not be saved.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Warning", "The list of courses could not be saved.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return false; // The operation failed
        }

        /// <summary>
        /// Set the user's list of grades
        /// </summary>
        /// <param name="location">The location containing the user's profile</param>
        private void SetGrades(string location)
        {
            string path = Path.Combine(location, "Grades");

            try
            {
                var list = from file in Directory.EnumerateFiles(path)
                           let name = Path.GetFileNameWithoutExtension(file)
                           select name;

                Grades = new ObservableCollection<string>(list.ToList());
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Warning ", "The grades list could not be loaded.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Warning", "The grades list could not be loaded.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Warning", "The grades list could not be loaded.",
                  "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }

        /// <summary>
        /// Sets a value
        /// </summary>
        /// <param name="prop">The property info object containing all property details</param>
        /// <param name="parent">The object owning the property</param>
        /// <param name="value">The value to be used</param>
        private static void SetValue(PropertyInfo prop, string value, object parent)
        {
            Type type = prop.PropertyType;

            if (type == typeof(string))
            {
                prop.SetValue(parent, value);
            }
            else if (type == (typeof(ObservableCollection<string>)))
            {
                prop.SetValue(parent, new ObservableCollection<string>(value.EnumerateStrings()));
            }
            else if (type == typeof(ObservableCollection<DateTime>))
            {
                var collection = new ObservableCollection<DateTime>();
                var strings = value.EnumerateStrings();
                foreach (var str in strings)
                {
                    if (DateTime.TryParse(str, out DateTime dateTime))
                    {
                        collection.Add(dateTime);
                    }
                }
                prop.SetValue(parent, collection);
            }
            else if (type == typeof(bool))
            {
                if (bool.TryParse(value, out bool b))
                {
                    prop.SetValue(parent, b);
                }
            }
            else if (type == typeof(GridLength))
            {
                if (double.TryParse(value, out double d))
                {
                    prop.SetValue(parent, new GridLength(d, GridUnitType.Star));
                }
            }
            else if (type == typeof(Gender))
            {
                if (Enum.TryParse(value, out Gender gender))
                {
                    prop.SetValue(parent, gender);
                }
            }
            else if (type == typeof(UserStatus))
            {
                if (Enum.TryParse(value, out UserStatus status))
                {
                    prop.SetValue(parent, status);
                }
            }
            else if (type == typeof(int))
            {
                if (int.TryParse(value, out int i))
                {
                    prop.SetValue(parent, i);
                }
            }
            else if (type == typeof(DateTime) | type == typeof(DateTime?))
            {
                if (DateTime.TryParse(value, out DateTime dateTime))
                {
                    prop.SetValue(parent, dateTime);
                }
            }

        }



        /// <summary>
        /// Loads the user assignments for the selected day.
        /// </summary>
        /// <param name="location">The user's location</param>
        /// <param name="selectedDay">The day that will be used to load the assignments</param>
        private void LoadAssignments(string location, DateTime selectedDay)
        {
            string gradeLocation = Path.Combine(location, "Grades");
            var assignments = from assignment in Assignment.GetAssignments(gradeLocation).AsParallel()
                              where assignment.Date.Contains(selectedDay)
                              orderby assignment.Course, assignment.Details
                              select assignment;

            // Update the list of assignments
            Assignments = new ObservableCollection<Assignment>();
            foreach (var assign in assignments)
            {
                Assignments.Add(assign);
            }
        }

        /// <summary>
        /// Gets the user's data. This is not meant to be used by callers.
        /// </summary>
        /// <param name="location">The user's location</param>
        private static string GetUserData(string location)
        {
            string config = Path.Combine(location, "config.txt");
            try
            {
                using (var newReader = new StreamReader(config))
                {
                    return newReader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "The user data could not be loaded.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("File Error", "The user data could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "The user data could not be loaded.",
                    "The program does not have the required permission.");
            }
            return string.Empty; // The operation failed
        }

        internal static void RenameCourses(string gradeLocation, string oldCourseName, string courseName)
        {
            var assignments = Assignment.LoadAssignmentFile(gradeLocation);
            foreach (var assignment in assignments)
            {
                if (assignment.Course == oldCourseName)
                {
                    assignment.Course = courseName;
                }
            }
            Assignment.SaveAssignmentFile(gradeLocation, assignments);
        }

        internal static void DeleteCourses(string gradeLocation, string courseName)
        {
            var assignments = (from assignment in Assignment.LoadAssignmentFile(gradeLocation)
                               where assignment.Course != courseName
                               select assignment).ToArray();

            Assignment.SaveAssignmentFile(gradeLocation, new Collection<Assignment>(assignments));
        }

        #region Properties
        /// <summary>
        /// The day which is used to retrieve the user's assignments
        /// </summary>
        public DateTime? SelectedDay { get; private set; }

        /// <summary>
        /// The day that the user was born. This is used to calculate the age.
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// The width of the recent column
        /// </summary>
        public GridLength RecentWidth { get; set; }

        /// <summary>
        /// The width of the functions column
        /// </summary>
        public GridLength FunctionsWidth { get; set; }

        /// <summary>
        /// The width of the apps column
        /// </summary>
        public GridLength AppsWidth { get; set; }

        /// <summary>
        /// The width of the calendar column
        /// </summary>
        public GridLength CalendarDetailsWidth { get; set; }

        /// <summary>
        /// The width of the preview column
        /// </summary>
        public GridLength CalendarCalendarWidth { get; set; }

        /// <summary>
        /// The width of the treeview column
        /// </summary>
        public GridLength CalendarTreeViewWidth { get; set; }

        /// <summary>
        /// The user's gender
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// The user's verification status
        /// </summary>
        public UserStatus UserStatus { get; set; }

        /// <summary>
        /// The most recent files to be displayed at one time
        /// </summary>
        public int MaxRecentFiles { get; set; }

        /// <summary>
        /// The list of assignments that the user completed for the specified day
        /// </summary>
        public ObservableCollection<Assignment> Assignments { get; private set; }

        /// <summary>
        /// True if the search box automatically pops up results as you type. Otherwise, false.
        /// </summary>
        public bool AutoSearch { get; set; }

        /// <summary>
        /// Specifies how many searches can show up on a page
        /// </summary>
        public int SearchCount { get; set; }

        /// <summary>
        /// True if notification sounds should be enabled. Otherwise, false.
        /// </summary>
        public bool EnableNotificationSounds { get; set; }

        /// <summary>
        /// True if notifications are enabled. Otherwise, false.
        /// </summary>
        public bool ShowNotifications { get; set; }

        /// <summary>
        /// Specifies whether the view record option is enabled
        /// </summary>
        public bool ViewRecordEnabled { get; set; }

        /// <summary>
        /// Specifies whether the calendar pop-up is enabled
        /// </summary>
        public bool ShowCalendarPopup { get; set; }

        /// <summary>
        /// Specifies whether the calendar button is enabled
        /// </summary>
        public bool ShowCalendar { get; set; }

        /// <summary>
        /// Specifies whether the calculator button is enabled
        /// </summary>
        public bool ShowCalculator { get; set; }

        /// <summary>
        /// Specifies whether or not recent files are displayed
        /// </summary>
        public bool ShowRecentFiles { get; set; }

        /// <summary>
        /// Gets whether or not the user is an administrator
        /// </summary>
        public bool IsTeacher { get; internal set; }

        /// <summary>
        ///  Specifies whether or not the users should be expanded
        /// </summary>
        public bool ExpandUsers { get; set; }

        /// <summary>
        ///  Specifies whether or not the courses should be expanded
        /// </summary>
        public bool ExpandCourses { get; set; }

        /// <summary>
        /// Specified whether or not spell checking is enabled
        /// </summary>
        public bool Spellcheck { get; set; }

        /// <summary>
        /// Specifies whether or not the calendar preview button is enabled
        /// </summary>
        public bool ShowCalendarDetails { get; set; }

        /// <summary>
        /// Specifies whether or not the delete account button is enabled
        /// </summary>
        public bool ShowDeleteAccountButton { get; set; }

        /// <summary>
        /// Specifies whether this notification should be enabled
        /// </summary>
        public bool Notification1 { get; set; }

        /// <summary>
        /// Specifies whether this notification should be enabled
        /// </summary>
        public bool Notification2 { get; set; }

        /// <summary>
        /// Specifies whether this notification should be enabled
        /// </summary>
        public bool Notification3 { get; set; }

        /// <summary>
        /// Specifies whether this notification should be enabled
        /// </summary>
        public bool Notification4 { get; set; }

        /// <summary>
        /// Specifies whether this notification should be enabled
        /// </summary>
        public bool Notification5 { get; set; }

        /// <summary>
        /// Specifies whether this notification should be enabled
        /// </summary>
        public bool Notification6 { get; set; }

        /// <summary>
        /// Specifies whether this notification should be enabled
        /// </summary>
        public bool Notification7 { get; set; }

        /// <summary>
        /// Specifies whether this notification should be enabled
        /// </summary>
        public bool Notification8 { get; set; }

        /// <summary>
        /// Gets the app location
        /// </summary>
        public string AppLocation { get; set; }

        /// <summary>
        /// The user's name. Multiple users can have the same name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's user name. All user names must be unique.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The location of the user's files
        /// </summary>
        public string FileLocation { get; private set; }

        /// <summary>
        /// The full location of the image
        /// </summary>
        public string FullImageLocation { get; private set; }

        /// <summary>
        /// The user's theme
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// The user's motto
        /// </summary>
        public string Motto { get; set; }

        /// <summary>
        /// The user's custom file location, if applicable
        /// </summary>
        public string AssignmentLocation { get; set; }

        /// <summary>
        /// The user's age
        /// </summary>
        public string Age
        {
            get
            {
                var dateDiff = DateTime.Today - Birthdate;
                int years = dateDiff.Days / 365;
                if (years > 1)
                {
                    return string.Format("{0} years old", years);
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// The user's computer image
        /// </summary>
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                FullImageLocation = Path.Combine(FileLocation, value);
            }
        }


        /// <summary>
        /// The user's password. Multiple users can have the same password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// A list of all students that this user has.
        /// This only applies to administrators.
        /// </summary>
        public ObservableCollection<string> Students { get; private set; }

        public ObservableCollection<string> Classes { get; private set; }

        /// <summary>
        /// All grades that the student is working on or has completed
        /// </summary>
        public ObservableCollection<string> Grades { get; private set; }

        /// <summary>
        /// All sick days in which the student did not do school
        /// </summary>
        public ObservableCollection<DateTime> SickDays { get; private set; }

        /// <summary>
        /// Al field trips completed by the student
        /// </summary>
        public ObservableCollection<DateTime> FieldTrips { get; private set; }

        /// <summary>
        /// The user's contact information
        /// </summary>
        public ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Specified whether the home pop-up should be shown
        /// </summary>
        public bool ShowHomePopup { get; set; }

        /// <summary>
        /// The user's report card for the current grade level
        /// </summary>
        public ReportCard CurrentReportCard { get; set; }

        /// <summary>
        /// All of the user's report cards
        /// </summary>
        public ObservableCollection<ReportCard> ReportCards { get; private set; }

        /// <summary>
        /// The grade that the user last used
        /// </summary>
        public string RecentGradeLevel { get; set; }

        /// <summary>
        /// The course that the user last used
        /// </summary>
        public string RecentCourse { get; set; }
        #endregion
    }
}
