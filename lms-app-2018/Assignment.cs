namespace RecordPro
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Security;
    using System.Xml;
    using System.Xml.Linq;
    using Path = System.IO.Path;

    public class Assignment : INotifyPropertyChanged
    {
        private string name;
        private string userName;
        private string course;
        private ObservableCollection<DateTime> date;
        private string details;
        private TimeSpan? time;
        private byte? grade;
        private AssignmentType assignmentType;
        private string notes;
        private bool assistanceNeeded;
        private string gradeLevel;
        private string fileLocation;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The user's name
        /// </summary>
        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }

        /// <summary>
        /// The user's username
        /// </summary>
        public string UserName { get => userName; set { userName = value; OnPropertyChanged("UserName"); } }

        /// <summary>
        /// Gets or sets course name for the assignment.
        /// </summary>
        public string Course { get => course; set { course = value; OnPropertyChanged("Course"); } }

        /// <summary>
        /// The dates when the assignment was completed
        /// </summary>
        public ObservableCollection<DateTime> Date { get => date; set { date = value; OnPropertyChanged("Date"); } }

        /// <summary>
        /// The details for the assignment.
        /// </summary>
        public string Details { get => details; set { details = value; OnPropertyChanged("Details"); } }

        /// <summary>
        /// Gets or sets the amount of time spent on the assignment.
        /// </summary>
        public TimeSpan? Time { get => time; set { time = value; OnPropertyChanged("Time"); } }

        /// <summary>
        /// Gets or sets the grade for the assignment.
        /// </summary>
        public byte? Grade { get => grade; set { grade = value; OnPropertyChanged("Grade"); } }

        /// <summary>
        /// The type of the assignment
        /// </summary>
        public AssignmentType AssignmentType { get => assignmentType; set { assignmentType = value; OnPropertyChanged("AssignmentType"); } }

        /// <summary>
        /// Any additional information regarding the assignment.
        /// </summary>
        public string Notes { get => notes; set { notes = value; OnPropertyChanged("Notes"); } }

        /// <summary>
        /// Specifies whether the user needed assistance to complete the assignment
        /// </summary>
        public bool AssistanceNeeded { get => assistanceNeeded; set { assistanceNeeded = value; OnPropertyChanged("AssistanceNeeded"); } }

        /// <summary>
        /// This assignment's grade level
        /// </summary>
        public string GradeLevel
        {
            get => gradeLevel;
            set
            {
                var parentLocation = Path.GetDirectoryName(FileLocation);
                if (value != null)
                {
                    FileLocation = Path.Combine(parentLocation, value + ".xml");
                    gradeLevel = value;
                    OnPropertyChanged("GradeLevel");
                }
            }
        }

        /// <summary>
        /// The full file location of the file
        /// </summary>
        public string FileLocation { get => fileLocation; set { fileLocation = value; OnPropertyChanged("FileLocation"); } }

        public Assignment()
        {
            Date = new ObservableCollection<DateTime>();
        }

        public Assignment(string course, ObservableCollection<DateTime> date, string details, byte? grade, string notes,
            bool assistanceNeeded, TimeSpan? time, AssignmentType type, string location, string gradeLevel)
        {
            Course = course;
            Date = date;
            Details = details;
            Grade = grade;
            Notes = notes;
            AssistanceNeeded = assistanceNeeded;
            Time = time;
            AssignmentType = type;
            FileLocation = location;
            GradeLevel = gradeLevel;
            var nameTuple = User.GetNameAndUserName(Path.GetDirectoryName(Path.GetDirectoryName(FileLocation)));
            Name = nameTuple.name;
            UserName = nameTuple.userName;

        }

        public Assignment(string fileLocation, string recentGradeLevel, string recentCourse)
            : this()
        {
            FileLocation = fileLocation;
            GradeLevel = recentGradeLevel;
            Course = recentCourse;
            var nameTuple = User.GetNameAndUserName(Path.GetDirectoryName(Path.GetDirectoryName(fileLocation)));
            Name = nameTuple.name;
            UserName = nameTuple.userName;
        }

        public void OnPropertyChanged(string propertyName) =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Returns a list of assignments
        /// </summary>
        /// <param name="gradeLocation">The grade location containing the assignment</param>
        /// <returns>A collection containing the assignments</returns>
        public static Collection<Assignment> GetAssignments(string gradeLocation)
        {
            var assignmentList = new List<Assignment>();
            try
            {
                var list = from file in Directory.EnumerateFiles(gradeLocation).AsParallel()
                           where Path.GetExtension(file) == ".xml"
                           let assignments = LoadAssignmentFile(file)
                           select assignments;

                foreach (var assignment in list)
                {
                    assignmentList.AddRange(assignment);
                }
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Warning - Record Pro", "Some assignments could not be loaded.",
                  ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }

            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Warning - Record Pro", "Some assignments could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Warning - Record Pro", "Some assignments could not be loaded.",
                    "The program does not the required permission.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            // Return the list of assignments
            return new Collection<Assignment>(assignmentList);
        }
        /// <summary>
        /// Gets all assignments for the current user, but also all student assignments
        /// </summary>
        /// <returns>A collection containing the assignments</returns>
        public static Collection<Assignment> GetAllAssignments()
        {
            string fileLocation = (string)Application.Current.Properties["Users Location"];
            var currentUser = (User)Application.Current.Properties["Current User Information"];
            var assignmentList = new List<Assignment>();
            try
            {
                var list = from dir in Directory.EnumerateDirectories(fileLocation).AsParallel()
                           where User.UserIsStudent(dir)
                           select dir;
                foreach (var dir in list)
                {
                    assignmentList.AddRange(GetAssignments(dir + "\\Grades"));
                }
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Warning - Record Pro", "Some assignments could not be loaded.",
                  ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("Warning - Record Pro", "Some assignments could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Warning - Record Pro", "Some assignments could not be loaded.",
                    "The program does not the required permission.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }

            // Return the list of assignments
            return new Collection<Assignment>(assignmentList);
        }

        public static IObservable<Assignment> Suggest(string match)
        {
            var assignmentList = GetAllAssignments();

            var assignments = from assignment in assignmentList
                              where IsValidMatch(match, assignment)
                              select assignment;
            return assignments.ToObservable();
        }


        /// <summary>
        /// Determines whether or not a valid match has been entered
        /// </summary>
        /// <param name="text">The text to check for</param>
        /// <param name="assignment">The assignment to check</param>
        /// <returns></returns>
        public static bool IsValidMatch(string text, Assignment assignment)
        {
            if (text.Contains("ASSISTANCE NEEDED") & !assignment.AssistanceNeeded)
            {
                return false;
            }
            else
            {
                text = text.Replace("ASSISTANCE NEEDED", "");
            }
            if (text.Contains("HOMEWORK") & assignment.AssignmentType != AssignmentType.Homework)
            {
                return false;
            }
            else
            {
                text = text.Replace("HOMEWORK", "");
            }
            if (text.Contains("QUIZ") & assignment.AssignmentType != AssignmentType.Quiz)
            {
                return false;
            }
            else
            {
                text = text.Replace("QUIZ", "");
            }
            if (text.Contains("EXAM") & assignment.AssignmentType != AssignmentType.Exam)
            {
                return false;
            }
            else
            {
                text = text.Replace("EXAM", "");
            }
            var stringList = new List<string>(text.CheckText()); // The list containing all words
            foreach (var str in stringList)
            {
                if (assignment.Course != null && assignment.Course.ToUpper().Contains(str))
                {
                    continue;
                }
                else if (assignment.Notes != null && assignment.Notes.ToUpper().Contains(str))
                {
                    continue;
                }
                else if (assignment.GradeLevel.ToUpper().Contains(str))
                {
                    continue;
                }
                else if (assignment.Details != null && assignment.Details.ToUpper().Contains(str))
                {
                    continue;
                }
                else if (byte.TryParse(str, out byte grade) && grade == assignment.Grade)
                {
                    continue;
                }
                else if (ConvertToTime(stringList, str, assignment))
                {
                    continue;
                }
                else
                {
                    if (assignment.UserName.ToUpper().Contains(str) | assignment.Name.ToUpper().Contains(str))
                    {
                        continue;
                    }
                    else if (str != "MINUTES" && str != "HOURS" && str != "SECONDS" &
                        str != "MINUTE" && str != "HOUR" && str != "SECOND")
                    {
                        return false;
                    }
                }
            }

            // If we reach here, then everything worked out
            return true;
        }

        private static bool ConvertToTime(List<string> stringList, string str, Assignment assignment)
        {
            int location = stringList.IndexOf(str);
            if (location + 1 == stringList.Count | !int.TryParse(str, out int i))
                return false;
            string nextStr = stringList[location + 1];
            if ((nextStr == "MINUTES" || nextStr == "MINUTE") && assignment.Time.HasValue &&
                assignment.Time.Value.Minutes == i)
            {
                return true;
            }
            else if ((nextStr == "HOURS" || nextStr == "HOUR") && assignment.Time.HasValue &&
                assignment.Time.Value.Hours == i)
            {
                return true;
            }
            else if ((nextStr == "SECONDS" || nextStr == "SECOND") && assignment.Time.HasValue &&
                assignment.Time.Value.Seconds == i)
            {
                return true;
            }
            return false; // If we reach here, this is not a valid time
        }

        /// <summary>
        /// Load the specified assignment file
        /// </summary>
        /// <param name="file">The assignment file to load</param>
        /// <returns>A list, containing all of the assignments</returns>
        public static Collection<Assignment> LoadAssignmentFile(string file)
        {
            string gradeLevel = Path.GetFileNameWithoutExtension(file);
            var assignments = new List<Assignment>();
            try
            {
                XDocument doc = XDocument.Load(file);
                var list = doc.Element("Document").Element("Assignments");

                // This should not be null. If it is, notify the user
                if (list == null)
                {
                    throw new XmlException();
                }

                foreach (var element in list.Elements())
                {
                    var assign = GetAssignmentFromData(element, gradeLevel, file);
                    if (assign != null)
                    {
                        assignments.Add(assign);
                    }
                }
            }
            catch (XmlException)
            {
                TaskDialog.ShowDialog("File Error", "File is not formatted property.",
                    "Some records may not have been loaded.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "Not all assignments could be loaded.",
                    ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("File Error", "Not all assignments could be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "Not all assignments could not be loaded.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return new Collection<Assignment>(assignments);
        }

        /// <summary>
        /// Load the specified assignment file for the selected today
        /// </summary>
        /// <param name="file">The assignment file to load</param>
        /// <returns>A list, containing all of the assignments</returns>
        public static Collection<Assignment> LoadAssignmentFile(string file, DateTime day)
        {
            string gradeLevel = Path.GetFileNameWithoutExtension(file);
            var assignments = new List<Assignment>();
            try
            {
                XDocument doc = XDocument.Load(file);
                var list = doc.Element("Document").Element("Assignments").Elements();

                // This should not be null. If it is, notify the user
                if (list == null)
                {
                    throw new XmlException();
                }

                foreach (var element in list)
                {
                    XAttribute elementDate = element.Attribute(XName.Get("Date"));
                    DateTime date;
                    if (elementDate != null && DateTime.TryParse(elementDate.Value, out date) && date == day)
                    {
                        assignments.Add(GetAssignmentFromData(element, gradeLevel, file));
                    }
                }
            }
            catch (XmlException)
            {
                TaskDialog.ShowDialog("File Error", "File is not formatted property.",
                    "Some records may not have been loaded.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "Not all assignments could be loaded.",
                    ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("File Error", "Not all assignments could be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "Not all assignments could not be loaded.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return new Collection<Assignment>(assignments);
        }

        /// <summary>
        /// Converts data to an assignment
        /// </summary>
        /// <param name="data">The data containing the assignment to add</param>
        /// <param name="gradeLevel">The grade level for this assignment</param>
        /// <param name="location">The location of the file containing the assignment</param>
        private static Assignment GetAssignmentFromData(XElement data, string gradeLevel, string location)
        {
            #region Variables
            var nameTuple = User.GetNameAndUserName(Path.GetDirectoryName(Path.GetDirectoryName(location)));
            Assignment newAssign = new Assignment()
            {
                FileLocation = location,
                GradeLevel = gradeLevel,
                Name = nameTuple.name,
                UserName = nameTuple.userName,
            };
            var course = data.Attribute(XName.Get("Course"));
            var details = data.Attribute(XName.Get("Details"));
            var notes = data.Attribute(XName.Get("Notes"));
            var datesStr = data.Attribute(XName.Get("Date"));
            var gradeStr = data.Attribute(XName.Get("Grade"));
            var timeStr = data.Attribute(XName.Get("Time"));
            var assistanceNeededStr = data.Attribute(XName.Get("AssistanceNeeded"));
            var assignTypeStr = data.Attribute(XName.Get("AssignmentType"));
            var datesList = new List<DateTime>();
            byte grade;
            TimeSpan time;
            bool assistanceNeeded;
            AssignmentType assignType;
            #endregion

            if (datesStr == null || course == null)
            {
                return null;
            }

            var datesStrArray = datesStr.Value.EnumerateStrings();
            foreach (var dateStr in datesStrArray)
            {
                DateTime date;
                if (DateTime.TryParse(dateStr, out date))
                {
                    datesList.Add(date);
                }
            }

            if (datesList.Count == 0)
            {
                return null;
            }

            #region ParseValues
            if (details != null)
            {
                newAssign.Details = details.Value;
            }

            if (notes != null)
            {
                newAssign.Notes = notes.Value;
            }

            if (gradeStr != null && byte.TryParse(gradeStr.Value, out grade))
            {
                newAssign.Grade = grade;
            }

            if (timeStr != null && TimeSpan.TryParse(timeStr.Value, out time))
            {
                newAssign.Time = time;
            }

            if (assistanceNeededStr != null && bool.TryParse(assistanceNeededStr.Value,
                out assistanceNeeded))
            {
                newAssign.AssistanceNeeded = assistanceNeeded;
            }

            if (assignTypeStr != null && Enum.TryParse(assignTypeStr.Value, out assignType))
            {
                newAssign.AssignmentType = assignType;
            }

            newAssign.Date = new ObservableCollection<DateTime>(datesList);
            newAssign.Course = course.Value;
            #endregion

            return newAssign;
        }

        /// <summary>
        /// Determines whether the assignment is equal to the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            var other = (Assignment)obj;
            return (other.AssignmentType == AssignmentType && other.AssistanceNeeded
                                       == AssistanceNeeded && other.Course == Course
                                       && DatesAreEqual(other.Date, Date) && other.Details == Details
                                      && other.FileLocation == FileLocation && other.Grade == Grade
                                       && other.GradeLevel == GradeLevel && other.Notes
                                       == Notes && other.Time == Time && other.UserName == UserName && other.Name == Name);
        }

        /// <summary>
        /// Returns true if two date collections are equal
        /// </summary>
        ///<param name="date1">The first date collection</param>
        ///<param name="date2">The other date collection</param>
        /// <returns></returns>
        private bool DatesAreEqual(ObservableCollection<DateTime> date1, ObservableCollection<DateTime> date2)
        {
            if (date1.Count != date2.Count)
            {
                return false;
            }

            foreach (var date in date1)
            {
                if (!date2.Contains(date))
                {
                    return false;
                }
            }

            // If we've made it this far, the dates are correct
            return true;
        }

        /// <summary>
        /// Gets the default hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Saves the assignment file
        /// </summary>
        /// <param name="fileLocation">The file location</param>
        /// <param name="assignments">The list of assignments</param>
        /// <returns>True if the operation succeeded. Otherwise, false</returns>
        public static bool SaveAssignmentFile(string fileLocation, Collection<Assignment> assignments)
        {
            try
            {
                var newNode = from assignment in assignments
                              let newElement = GetElement(assignment)
                              select newElement;
                XDocument doc = XDocument.Load(fileLocation);
                doc.Element("Document").Element("Assignments").ReplaceNodes(newNode);
                doc.Save(fileLocation);
                return true;
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("Warning", "The assignment could not be saved.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("Warning", "The assignment could not be saved.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return false; // If we reach this point, an error occurred
        }

        /// <summary>
        /// Converts an assignment to XML
        /// </summary>
        /// <param name="assignment">The assignment to converter</param>
        /// <returns>The XML object</returns>
        private static XElement GetElement(Assignment assignment)
        {
            string dates = string.Join(",", assignment.Date);
            var newElement = new XElement(XName.Get("Assignment"));
            newElement.SetAttributeValue(XName.Get("Date"), dates);
            newElement.SetAttributeValue(XName.Get("Course"), assignment.Course);
            newElement.SetAttributeValue(XName.Get("Details"), assignment.Details);
            newElement.SetAttributeValue(XName.Get("Notes"), assignment.Notes);
            newElement.SetAttributeValue(XName.Get("Grade"), assignment.Grade);
            newElement.SetAttributeValue(XName.Get("AssignmentType"), assignment.AssignmentType);
            newElement.SetAttributeValue(XName.Get("AssistanceNeeded"), assignment.AssistanceNeeded);
            if (assignment.Time != null)
            {
                newElement.SetAttributeValue(XName.Get("Time"), assignment.Time.Value.ToString());
            }

            return newElement;
        }

        /// <summary>
        /// Adds an assignment
        /// </summary>
        /// <param name="assignment">The assignment to add</param>
        internal static void AddAssignment(Assignment assignment)
        {
            var assignments = LoadAssignmentFile(assignment.FileLocation);
            assignments.Add(assignment);
            SaveAssignmentFile(assignment.FileLocation, assignments);
        }
    }
    public enum AssignmentType
    {
        Homework,
        Quiz,
        Exam,
    }

}
