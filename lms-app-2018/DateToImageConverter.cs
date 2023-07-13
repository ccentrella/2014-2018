using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RecordPro
{
	class DateToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			// Ensure a valid value has been entered
			if (value == null)
            {
                return null;
            }

            var date = (DateTime)value;
			var school = (School)Application.Current.Properties["School"];

			// If school is null, don't throw an exception
			if (school == null)
            {
                return null;
            }

            if (school.Holidays.Contains(date))
            {
                return "Holiday.png";
            }
            else if (school.VacationDays.Contains(date))
            {
                return "Vacation.png";
            }
            else if (date.DayOfWeek == DayOfWeek.Saturday | date.DayOfWeek ==  DayOfWeek.Sunday)
            {
                return "Weekend.png";
            }
            else
            {
                return "School Day.png";
            }
        }

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
