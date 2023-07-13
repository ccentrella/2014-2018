using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;





namespace RecordPro
{
    /// <summary>
    /// Interaction logic for HomePane.xaml
    /// </summary>
    public partial class HomePane : Page
    {
        public HomePane()
        {
            InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            // Get the main window
            MainWindow window = Application.mWindow as MainWindow;
            if (window == null)
            {
                return;
            }

            window.AvatarPopup.IsOpen = false; // Close the small pane

            // Navigate to the home page
            var newHome = new Home();
            window.MainFrame.Navigate(newHome);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = Application.mWindow as MainWindow;

            // Navigate to the settings dialog
            if (window != null)
            {
                window.AvatarPopup.IsOpen = false;
                var newSettings = new Options();
                window.MainFrame.Navigate(newSettings);
            }
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            Application.SignOut();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if ((bool?)Application.Current.Properties["Validated"] != true)
            {
                homeButton.Visibility = Visibility.Hidden;
                settingsButton.Visibility = Visibility.Hidden;
            }
        }

    }
}
