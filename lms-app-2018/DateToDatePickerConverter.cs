using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace RecordPro
{
	class DateToDatePickerConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var collection = (ObservableCollection<DateTime>)value;
			var dateCollection = new ObservableCollection<DatePicker>();
			foreach (var item in collection)
            {
                dateCollection.Add(new DatePicker() { SelectedDate = item, Margin = new System.Windows.Thickness(5, 0, 5, 0) });
            }

            return dateCollection;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var collection = (ObservableCollection<DatePicker>)value;
			var dateCollection = new ObservableCollection<DateTime>();
			foreach (var item in collection)
            {
                dateCollection.Add(item.SelectedDate.Value);
            }

            return dateCollection;
		}
	}
}
