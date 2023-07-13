using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
    public static class NativeMethods
    {

        [DllImport("shell32.dll")]
        static internal extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder lpIconPath,
            out ushort lpiIcon);


        /// <summary>
        /// Shows a dialog to the user consisting of a heading and instructions, in addition to an icon and buttons.
        /// </summary>
        /// <param name="hwndParent">The parent handle to use.</param>
        /// <param name="hInstance">The handle instance to use.</param>
        /// <param name="title">The text to be displayed in the title bar.</param>
        /// <param name="mainInstruction">The text to be displayed in the heading.</param>
        /// <param name="content">The instructions to be displayed.</param>
        /// <param name="buttons">An enumeration of different buttons that can be shown.</param>
        /// <param name="icon">The icon that will be shown.</param>
        /// <returns>A TaskDialogResult, indicating which button the user clicked.</returns>
        [DllImport("comctl32.dll", PreserveSig = false, CharSet = CharSet.Unicode)]
        internal static extern TaskDialogResult TaskDialog(
            IntPtr hwndParent,
            IntPtr hInstance,
            string title,
            string mainInstruction,
            string content,
            TaskDialogButtons buttons,
            TaskDialogIcon icon);

       
        [DllImport("shell32.dll", SetLastError = true)]
        internal static extern void SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppID);

        

    }
}

