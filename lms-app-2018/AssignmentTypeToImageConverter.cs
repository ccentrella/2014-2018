using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RecordPro
{
	class AssignmentTypeToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var assignmentType = (AssignmentType)value;
			switch (assignmentType)
			{
				case AssignmentType.Homework:
					return new Uri("Homework.png", UriKind.Relative);
				case AssignmentType.Quiz:
					return new Uri("Quiz.png", UriKind.Relative);
				case AssignmentType.Exam:
					return new Uri("Test.png", UriKind.Relative);
				default:
					return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
