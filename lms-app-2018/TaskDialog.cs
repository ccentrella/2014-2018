using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
	public enum TaskDialogResult
	{
		Ok = 1,
		Cancel = 2,
		Retry = 4,
		Yes = 6,
		No = 7,
		Close = 8
	}

	[Flags]
	public enum TaskDialogButtons
	{
		Ok = 0x0001,
		Yes = 0x002,
		No = 0x004,
		Cancel = 0x0008,
		Retry = 0x0010,
		Close = 0x0020
	}

	public enum TaskDialogIcon
	{
		Warning = 65535,
		Error = 65534,
		Information = 65533,
		Shield = 65532

	}

	public static class TaskDialog
	{
		/// <summary>
		/// Shows a task dialog, using the information icon and the OK button.
		/// </summary>
		/// <param name="title">The title of the dialog</param>
		/// <param name="heading">The heading for the dialog</param>
		/// <param name="data">The data for the dialog</param>
		public static void ShowDialog(string title, string heading, string data)
		{
			RecordPro.NativeMethods.TaskDialog(IntPtr.Zero, IntPtr.Zero, title, heading,
			   data, TaskDialogButtons.Ok,
			  TaskDialogIcon.Information);
		}

		/// <summary>
		/// Shows a task dialog, using the given options.
		/// </summary>
		/// <param name="title">The title of the dialog</param>
		/// <param name="heading">The heading for the dialog</param>
		/// <param name="data">The data for the dialog</param>
		/// <param name="buttons">The buttons to display</param>
		/// <param name="icon">The icon to display</param>
		public static TaskDialogResult ShowDialog(string title, string heading, string data,
		TaskDialogButtons buttons, TaskDialogIcon icon)
		{
			return RecordPro.NativeMethods.TaskDialog(IntPtr.Zero, IntPtr.Zero, title, heading,
				   data, buttons, icon);
		}
	}
}
