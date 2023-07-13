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
	class GradeToBackgroundConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			byte convertedValue;
			if (value == null || !byte.TryParse(value.ToString(), out convertedValue))
            {
                return null;
            }
            else if (convertedValue >= 90)
            {
                return Brushes.Green;
            }
            else if (convertedValue >= 80)
            {
                return Brushes.DarkBlue;
            }
            else if (convertedValue >= 70)
            {
                return Brushes.Orange;
            }
            else if (convertedValue >= 65)
            {
                return Brushes.OrangeRed;
            }
            else
            {
                return Brushes.Red;
            }
        }

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
	class GradeToImageConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			byte convertedValue;
			if (value == null || !byte.TryParse(value.ToString(), out convertedValue))
            {
                return null;
            }
            else if (convertedValue >= 90)
            {
                return new Uri("Excellent Grade.png", UriKind.Relative);
            }
            else if (convertedValue >= 70 && convertedValue < 80)
            {
                return new Uri("Poor Grade.png", UriKind.Relative);
            }
            else if (convertedValue <= 70)
            {
                return new Uri("Failing Grade.png", UriKind.Relative);
            }
            else
            {
                return null;
            }
        }

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
	class GradeToToolTipConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			byte convertedValue;
			if (value == null || !byte.TryParse(value.ToString(), out convertedValue))
            {
                return null;
            }
            else if (convertedValue >= 90)
            {
                return "Congratulations!";
            }
            else if (convertedValue >= 70 && convertedValue < 80)
            {
                return "Try harder.";
            }
            else if (convertedValue < 70)
            {
                return "Try again.";
            }
            else
            {
                return null;
            }
        }

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
