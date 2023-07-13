using Windows.Foundation;
using Windows.System;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using System.Reflection;
using System.IO;
using System;
using System.Xml;

namespace RecordPro
{
    /// <summary>
    /// Enables notifications on Windows 8 and later
    /// </summary>
    public class Notifications
    {
        /// <summary>
        /// Shows a simple notification.
        /// </summary>
        /// <param name="text">The details to show the user</param>
        //[System.Diagnostics.Conditional("_UWP")]
        public static void SendNotification(string text)
        {
            User user = (User)Application.Current.Properties["Current User Information"];
            string imageLocation = Path.Combine(Path.GetDirectoryName(
     Assembly.GetExecutingAssembly().Location), "Record Pro Logo.png");
            string xmlString = @"
		<toast>
			<visual>
				<binding template='ToastImageAndText01'>
					<image id='1' src ='" + imageLocation + @"' alt='Autosoft Record Pro'></image>
					<text id='1'>" + text + @"</text>
				</binding>
			</visual>
        <audio src='ms-winsoundevent:Notification.None'/>
		</toast>";

            if (user.EnableNotificationSounds)
            {
                xmlString = @"
                <toast>
			<visual>
				<binding template='ToastImageAndText01'>
					<image id='1' src ='" + imageLocation + @"' alt='Autosoft Record Pro'></image>
					<text id='1'>" + text + @"</text>
				</binding>
			</visual>
		</toast>";
            }

            // Only continue if the user allows notifications
            if (!user.ShowNotifications)
            {
                return;
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlString);

            var notification = new ToastNotification(document);
            notification.Activated += Notification_Activated;
            ToastNotificationManager.CreateToastNotifier("Record Pro 2018").Show(notification);
        }

        /// <summary>
        /// Shows a more advanced notification.
        /// </summary>
        /// <param name="heading">Th heading to show the user</param>
        /// <param name="text">The details to show the user</param>
        //[System.Diagnostics.Conditional("_UWP")]
        public static void SendNotification(string heading, string text)
        {
            User user = (User)Application.Current.Properties["Current User Information"];
            string imageLocation = Path.Combine(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location), "Record Pro Logo.png");
            string xmlString = @"
		<toast>
			<visual>
				<binding template='ToastImageAndText02'>
					<text id='1'>" + heading + @"</text>
					<text id='2'>" + text + @"</text>
					<image id='1' src ='" + imageLocation + @"' alt='Autosoft Record Pro'></image>
				</binding>
			</visual>
        <audio src='ms-winsoundevent:Notification.None'/>
		</toast>";

            if (user.EnableNotificationSounds)
            {
                xmlString = @"  <toast>
			<visual>
				<binding template='ToastImageAndText02'>
					<text id='1'>" + heading + @"</text>
					<text id='2'>" + text + @"</text>
					<image id='1' src ='" + imageLocation + @"' alt='Autosoft Record Pro'></image>
				</binding>
			</visual>
		</toast>";
            }

            // Only continue if the user allows notifications
            if (!user.ShowNotifications)
            {
                return;
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlString);

            var notification = new ToastNotification(document);
            notification.Activated += Notification_Activated;
            ToastNotificationManager.CreateToastNotifier("Record Pro 2018").Show(notification);
        }

         private static void Notification_Activated(ToastNotification sender, object args)
        {
            Application.mWindow.Dispatcher.BeginInvoke(new Action(() =>
            {
                Application.mWindow.Activate();
                Application.mWindow.WindowState = System.Windows.WindowState.Normal;
            }));
        }


    }
}
