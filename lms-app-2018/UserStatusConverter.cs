using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace RecordPro
{
	public class UserStatusToBackgroundConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
            {
                return Brushes.DarkRed;
            }

            UserStatus status;
			Enum.TryParse(value.ToString(), out status);

			// Return the appropriate brush
			switch (status)
			{
				case UserStatus.Verified:
					return Brushes.ForestGreen;
				case UserStatus.NotVerified:
					return Brushes.Orange;
				default:
					return Brushes.DarkRed;
			}
		}


		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
	public class UserStatusToStringConverter: IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
            {
                return "Denied";
            }

            UserStatus status;
			Enum.TryParse(value.ToString(), out status);

			// Return the appropriate string
			switch (status)
			{
				case UserStatus.Verified:
					return "Verified";
				case UserStatus.NotVerified:
					return "Not Verified";
				default:
					return "Denied";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
	public class UserStatusToImageConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
            {
                return "Failing Grade.png";
            }

            UserStatus status;
			Enum.TryParse(value.ToString(), out status);

			// Return the appropriate image
			switch (status)
			{
				case UserStatus.Verified:
					return "Excellent Grade.png";
				case UserStatus.NotVerified:
					return "Poor Grade.png";
				default:
					return "Failing Grade.png";
			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}

}
