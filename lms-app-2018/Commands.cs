using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecordPro
{
	public class Commands
	{
		public static RoutedUICommand ModifyAssignment = new
	RoutedUICommand("_Modify Assignment", "ModifyAssignment", typeof(Commands));

		public static RoutedUICommand ShowToday =
			new RoutedUICommand("Show _Today", "ShowToday", typeof(Commands));

		public static RoutedUICommand ToggleDetails =
			new RoutedUICommand("Toggle _Details", "ToggleDetails", typeof(Commands));
	}
}
