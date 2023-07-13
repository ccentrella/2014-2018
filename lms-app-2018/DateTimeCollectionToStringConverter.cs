using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RecordPro
{
    public class DateTimeCollectionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = (Collection<DateTime>)value;
            var strings = new Collection<string>();
            foreach (var date in collection)
            {
                if (DateTime.Today - date < TimeSpan.FromDays(7))
                {
                    switch (date.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            strings.Add("Sunday");
                            break;
                        case DayOfWeek.Monday:
                            strings.Add("Monday");
                            break;
                        case DayOfWeek.Tuesday:
                            strings.Add("Tuesday");
                            break;
                        case DayOfWeek.Wednesday:
                            strings.Add("Wednesday");
                            break;
                        case DayOfWeek.Thursday:
                            strings.Add("Thursday");
                            break;
                        case DayOfWeek.Friday:
                            strings.Add("Friday");
                            break;
                        case DayOfWeek.Saturday:
                            strings.Add("Saturday");
                            break;
                        default:
                            break;
                    }
                }
                else if (DateTime.Today.Year == date.Year)
                {
                    strings.Add(date.ToString("dddd, MMMM dd"));
                }
                else
                {
                    strings.Add(date.ToLongDateString());
                }
            }
            return string.Join("; ",strings);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
