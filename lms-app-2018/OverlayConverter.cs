using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RecordPro
{
	class OverlayConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			// If a user is logged in, return the image, which should be valid.
			// Otherwise, just return null, and no overlay will be provided
			var image = value as BitmapImage;
	
			return image == null || image.UriSource.OriginalString	== 
				"Generic Avatar (Unisex).png" ? null : value as BitmapImage;
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
