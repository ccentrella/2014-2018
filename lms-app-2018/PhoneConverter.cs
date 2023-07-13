using Autosoft_Controls_2017;
using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace RecordPro
{
    class PhoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder builder = new StringBuilder();
            Phone phone = (Phone)value;
            if (phone == null)
            {
                return null;
            }

            if (phone.AreaCode != null)
            {
                builder.Append(string.Join("", "(", phone.AreaCode, ")"));
            }
            if (phone.MiddleDigits != null)
            {
                builder.Append(" " + phone.MiddleDigits);
            }
            if (phone.LastDigits != null)
            {
                builder.Append(" - " + phone.LastDigits);
            }
            if (phone.Extension != null)
            {
                builder.Append(" Ext: " + phone.Extension);
            }
            return builder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
