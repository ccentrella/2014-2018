using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RecordPro
{
    class Class : INotifyPropertyChanged
    {
        /// <summary>
        /// Creates a new Class object from the specified file
        /// </summary>
        /// <param name="fileLocation">The file containing class information</param>
        public Class(string fileLocation)
        {
            try
            {
                var data = XDocument.Load(fileLocation);
                var className = data.Element("Document").Attribute("Class Name");
                var students = data.Element("Students");
                var teachers = data.Element("Teachers");
                if (className == null | teachers == null | students == null)
                {
                    TaskDialog.ShowDialog("Invalid Class", "An invalid class file has been found",
                        "Class could not be loaded.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
                    return;
                }
                ClassName = className.Value;
                var studentList = from user in students.Elements()
                                  let location = user.Attribute("Location").Value
                                  select new User(location);
                var teacherList = from user in students.Elements()
                                  let location = user.Attribute("Location").Value
                                  select new User(location);
                Teachers = new ObservableCollection<User>(teacherList);
            }
            catch (XmlException)
            {
                TaskDialog.ShowDialog("File Error", "File is not formatted property.",
                    "The class could not be created.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "The class object could not be created.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("File Error", "The class object could not be created.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "The class object could not be created.",
                    "The program does not have the required permission.",
                    TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
        }

        string className;
        ObservableCollection<User> students;
        ObservableCollection<User> teachers;

        public string ClassName { get => className; set { className = value; OnPropertyChanged("ClassName"); } }

        public ObservableCollection<User> Students { get => students; protected set { students = value; OnPropertyChanged("Students"); } }

        public ObservableCollection<User> Teachers { get => teachers; protected set { teachers = value; OnPropertyChanged("Teachers"); } }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) =>
  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Collection<Class> GetClasses()
        {
            string fileLocation = (string)Application.Current.Properties["File Location"];
            var classLocation = Path.Combine(fileLocation, "Classes");
            var collection = new Collection<Class>();
            try
            {
                foreach (var file in Directory.EnumerateFiles(classLocation))
                {
                    collection.Add(new Class(file));
                }
            }
            catch (IOException ex)
            {
                TaskDialog.ShowDialog("File Error", "The classes could not be loaded.",
                    ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (SecurityException)
            {
                TaskDialog.ShowDialog("File Error", "The classes could not be loaded.",
                    "The program does not have the required permission.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                TaskDialog.ShowDialog("File Error", "The classes could not be loaded.",
                    "Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
            }
            return collection;
        }
    }
}
