using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RecordPro
{
	public static class ImageFunctions
	{
		/// <summary>
		/// Get the image for the specified file location.
		/// If the image does not exist, a default one will be provided.
		/// </summary>
		/// <param name="fileLocation">The location of the file</param>
		public static ImageSource GetAppImage(this string fileLocation)
		{
            var icon = NativeMethods.ExtractAssociatedIcon(IntPtr.Zero, new StringBuilder(fileLocation), out ushort index);
            if (icon != null)
			{
				var options = BitmapSizeOptions.FromWidthAndHeight(256, 256);
				var image = Imaging.CreateBitmapSourceFromHIcon(icon, 
					new Int32Rect(),	options);
				return image;
			}
			else
			{
				return new BitmapImage(new Uri("Application.png", UriKind.Relative)) ;
			}
		}

        /// <summary>
		/// Loads the default image
		/// </summary>
		/// <param name="gender">The gender of the current user.</param>
		public static void LoadDefaultImage(Gender gender)
        {
            // Get the main window
            MainWindow window = (MainWindow)Application.mWindow;

            var image = GetDefaultImage(gender);
            window.Avatar.Source = image;
        }

        /// <summary>
        /// Gets the default image location
        /// </summary>
        /// <param name="gender">The gender of the current user.</param>
        public static BitmapSource GetDefaultImage(Gender gender)
        {
            string Url;
            switch (gender)
            {
                case Gender.Male:
                    Url = "Generic Avatar (Male).png";
                    break;
                case Gender.Female:
                    Url = "Generic Avatar (Female).png";
                    break;
                default:
                    Url = "Generic Avatar (Unisex).png";
                    break;
            }
            return new BitmapImage(new Uri(Url, UriKind.Relative));
        }
    }
}
