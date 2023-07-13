namespace RecordPro
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Security;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using System.Windows.Shell;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate void mainDelegate(); // Used for asynchronous programming
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Show the avatar popup
            AvatarPopup.IsOpen = true;
        }

        private void AvatarPopup_Opened(object sender, EventArgs e)
        {
            string currentUser = (string)Application.Current.Properties["Current User"];

            // If no user is logged in, load the avatar pane.
            // Otherwise, load the home pane. 
            if (currentUser == null || currentUser == "None")
            {
                SmallPane.Navigate(new SignIn1());
            }
            else
            {
                SmallPane.Navigate(new HomePane());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Enable the program to have access to the main window.
            Application.mWindow = this;
        }

        private void SystemCommands_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            CheckCommands(e);
        }

        private void CheckCommands(System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (e.Command == SystemCommands.CloseWindowCommand | e.Command == SystemCommands.MinimizeWindowCommand)
            {
                e.CanExecute = true;
            }
            else if (e.Command == SystemCommands.RestoreWindowCommand)
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else if (e.Command == SystemCommands.MaximizeWindowCommand)
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    e.CanExecute = false;
                }
                else
                {
                    e.CanExecute = true;
                }
            }
        }


        private void AvatarPopup_Closed(object sender, EventArgs e)
        {
            string currentUser = (string)Application.Current.Properties["Current User"];

            // If no user is logged in, load the default image and update the label
            if (currentUser == null || currentUser == "None")
            {
                ImageFunctions.LoadDefaultImage(Gender.Unknown);
                UserHeader.Content = "Sign In";
            }
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (MainFrame.Content is Page p && !string.IsNullOrEmpty(p.Title))
            {
                this.Title = string.Format("{0} - Record Pro", p.Title);
            }
            else
            {
                this.Title = "Record Pro";
            }
        }

        private void SearchBox_SearchStarted(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void SearchBox_SearchChanged(object sender, RoutedEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Searches for a match
        /// </summary>
        private void Search()
        {
            User user = (User)Application.Current.Properties["Current User Information"];
            string text = searchBox.Text;
            var TextChanged = Observable.FromEventPattern(searchBox, "TextChanged");
            var matches = TextChanged.Publish(textChanged =>
                    from suggestion in Assignment.Suggest(text.ToUpper().Replace(",", ""))
                    .TakeUntil(textChanged)
                    select suggestion).Take(user.SearchCount);
            resultsBox.ItemsSource = matches.ToEnumerable();
            var view = CollectionViewSource.GetDefaultView(resultsBox.ItemsSource);
            view.GroupDescriptions.Clear();
            view.GroupDescriptions.Add(new PropertyGroupDescription("UserName"));
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            User currentUser = (User)Application.Current.Properties["Current User Information"];
            if (currentUser.AutoSearch)
            {
                Search();
                resultsText.Content = "Results";
            }
            else
            {
                resultsText.Content = "Press enter to search";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "";
        }

        private void resultsBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Only continue if a valid item is selected
            if (resultsBox.SelectedIndex == -1)
                return;

            var item = (Assignment)resultsBox.SelectedItem;
            var date = item.Date[0];
            var newCalendar = new Calendar();
            MainFrame.Navigate(newCalendar);
            newCalendar.calendar.SelectedDate = date;
            newCalendar.calendar.DisplayDate = date;
            searchBox.Clear();
        }

        private void searchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Search();
                resultsText.Content = "Results";
            }
        }

       
    }
}
