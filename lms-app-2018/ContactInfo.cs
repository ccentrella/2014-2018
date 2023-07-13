using Autosoft_Controls_2017;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
	public class ContactInfo
	{
		public ContactInfo()
		{
			Country = "United States";
            Phone = new Phone();
		}

		public ContactInfo(string data)
			: this()
		{
			var props = from prop in this.GetType().GetProperties()
						let value = data.GetValue(prop.Name)
						where prop.CanWrite
						where !string.IsNullOrEmpty(value)
						select new { PropertyInfo = prop, Value = value };

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
            else if (type == typeof(Uri))
			{
                Uri.TryCreate((string)value, UriKind.RelativeOrAbsolute, out Uri result);
                prop.SetValue(parent, result);
			}
            else if (type == typeof(Phone))
            {
                var phone = LoadPhone(value);
                prop.SetValue(parent, phone);
            }
        }

        /// <summary>
        /// Loads a phone number from the specified string
        /// </summary>
        /// <param name="value">The string containing the phone number</param>
        /// <returns>A phone object, compatible with the PhoneBox control.</returns>
        public static Phone LoadPhone(string value)
        {
            int length = value.Length;
            Phone phone = new Phone();

            // Set the phone
            if (length >= 3)
            {
                if (int.TryParse(value.Substring(0, 3), out int areaCode))
                {
                    phone.AreaCode = areaCode;
                }
            }
            if (length >= 6)
            {
                if (int.TryParse(value.Substring(3, 3), out int middleDigits))
                {
                    phone.MiddleDigits = middleDigits;
                }
            }
            if (length >= 10)
            {
                if (int.TryParse(value.Substring(6, 4), out int lastDigits))
                {
                    phone.LastDigits = lastDigits;
                }
            }
            if (length >= 11)
            {
                if (int.TryParse(value.Substring(10), out int extension))
                {
                    phone.Extension = extension;
                }
            }

            // Return the phone
            return phone;

        }

        /// <summary>
        /// The street address
        /// </summary>
        public string Address { get; set; }

		/// <summary>
		/// The main phone number
		/// </summary>
		public Phone Phone { get; set; }

		/// <summary>
		/// The main email address
		/// </summary>
		public string EmailAddress { get; set; }

		/// <summary>
		/// The website (if applicable)
		/// </summary>
		public Uri Website { get; set; }

		/// <summary>
		/// The city
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// The state
		/// </summary>
		public string State { get; set; }

		/// <summary>
		/// The country. The default is United States
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// The Zip Code
		/// </summary>
		public string ZipCode { get; set; }
	}
}
