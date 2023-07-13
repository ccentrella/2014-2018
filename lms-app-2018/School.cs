using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
	public class School
	{
		/// <summary>
		/// Creates a new school object
		/// </summary>
		/// <param name="location">The location of the school's information</param>
		public School(string location)
		{
			var data = GetSchoolData(location);
			ContactInfo = new ContactInfo(data);
			// Initialize each value
			var props = from prop in this.GetType().GetProperties()
						where prop.CanWrite
						where prop.GetType() != typeof (ContactInfo)
						let propName = prop.Name
						let propValue = data.GetValue(propName)
						select new { PropertyInfo = prop, Value = propValue };

			foreach (var prop in props)
            {
                SetValue(prop.PropertyInfo, prop.Value, this);
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
            else if (type == typeof(Collection<DateTime>))
			{
				var collection = new Collection<DateTime>();
				var strings = value.EnumerateStrings();
				foreach (var str in strings)
				{
					DateTime dateTime;
					if (DateTime.TryParse(str, out dateTime))
                    {
                        collection.Add(dateTime);
                    }
                }
				prop.SetValue(parent, collection);
			}
			else if (type == typeof(Uri))
			{
				Uri result;
				Uri.TryCreate(value, UriKind.Absolute, out result);
					prop.SetValue(parent,result);
			}
		}

		/// <summary>
		/// A list of all holidays for the school
		/// </summary>
		public Collection<DateTime> Holidays { get; private set; }

		/// <summary>
		/// A list of all vacation days for the school
		/// </summary>
		public Collection<DateTime> VacationDays { get; private set; }

		/// <summary>
		/// The school's contact info
		/// </summary>
		public ContactInfo ContactInfo { get;set; }

		/// <summary>
		/// The name of the school
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The main school image
		/// </summary>
		public Uri ImageLocation { get; set; }

		/// <summary>
		/// Gets the user's data. This is not meant to be used by callers.
		/// </summary>
		/// <param name="location">The user's location</param>
		private static string GetSchoolData(string location)
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
				TaskDialog.ShowDialog("File Error", "The school information could not be loaded.",
					ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (UnauthorizedAccessException)
			{
				TaskDialog.ShowDialog("File Error", "The school information could not be loaded.",
					"Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (SecurityException)
			{
				TaskDialog.ShowDialog("File Error", "The school information could not be loaded.",
					"The program does not have the required permission.");
			}
			return string.Empty; // The operation failed
		}
			}
}
