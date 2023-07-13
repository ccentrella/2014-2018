using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace RecordPro
{
	public class UserImageConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
            {
                return null;
            }

            // Load the user's image. If the user has no image, then load the default one
            User user = (User)value;
			if (File.Exists(user.FullImageLocation))
			{
				try
				{
					var newSource = new BitmapImage(new Uri(user.FullImageLocation, UriKind.Absolute));
					return newSource;
				}
				catch (IOException)
				{
					var defaultGender = Enum.Parse(typeof(Gender),parameter.ToString());
					return ImageFunctions.GetDefaultImage((Gender)defaultGender);
				}
			}
			else
			{
				var defaultGender = user.Gender;
				return ImageFunctions.GetDefaultImage((Gender)defaultGender);
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
