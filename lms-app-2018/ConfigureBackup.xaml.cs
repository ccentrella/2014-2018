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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecordPro
{
    /// <summary>
    /// Interaction logic for ConfigureBackup.xaml
    /// </summary>
    public partial class ConfigureBackup : Page
    {
        public ConfigureBackup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var freq = (ComboBoxItem)frequency.SelectedValue;
            var hour = (ComboBoxItem)when.SelectedValue;
            if (timePane.Visibility == Visibility.Visible)
            {
                this.NavigationService.Navigate(new ConfigureBackup2(
                    (string)freq.Content, (string)hour.Content));
            }
            else
            {
                this.NavigationService.Navigate(new ConfigureBackup2(
                   (string)freq.Content, ""));
            }
        }
    }
}
