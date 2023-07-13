using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Path = System.IO.Path;
namespace RecordPro
{
    /// <summary>
    /// Interaction logic for ClassView.xaml
    /// </summary>
    public partial class ClassView : Page
    {
        public ClassView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var currentUser = (User)Application.Current.Properties["Current User Information"];
            var usersLocation = (string)Application.Current.Properties["Users Location"];
            if (currentUser.Students == null)
            {
                return;
            }
            var students = new Collection<User>((from userString in currentUser.Students
                                                 let userLocation = Path.Combine(usersLocation, userString)
                                                 select new User(userLocation)).ToArray());
            studentPane.ItemsSource = students;
        }
    }
}
