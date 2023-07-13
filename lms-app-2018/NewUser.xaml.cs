using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
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




using IO = System.IO;

namespace RecordPro
{
	/// <summary>
	/// Interaction logic for NewUser.xaml
	/// </summary>
	public partial class NewUser : Page
	{
		/// <summary>
		/// Defines a command used to indicate a positive response
		/// </summary>
		public static RoutedUICommand OkayCommand = new RoutedUICommand("Okay", "OkayCommand", typeof(Page));

		string oldImageLocation;
		OpenFileDialog openFileDialog1 = new OpenFileDialog()
		{
			Title = "Upload Image - Record Pro",
			ValidateNames = true,
			CheckPathExists = true,
			Filter = "Image Files | *.png; *.ico; *.jpg; *.jpeg; *.tiff; *.gif; *.bmp; *.wmf | All Files | *.*"
		};

		public NewUser()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			// Go either to the welcome screen to the home page
			if (Application.Current.Properties["Current User"] == null || (string)Application.Current.Properties["Current User"] == "None")
			{
				var newWelcome = new Welcome();
				this.NavigationService.Navigate(newWelcome);
			}
			else
			{
				var newHome = new Home();
				this.NavigationService.Navigate(newHome);
			}
		}

		private void Okay_Executed(object sender, RoutedEventArgs e)
		{
			AddUser(); // Attempt to add the new user
		}

		/// <summary>
		/// Attempts to add the new user
		/// </summary>
		private async void AddUser()
		{
			string usersLocation = (string)Application.Current.Properties["Users Location"];
			MainWindow window = Application.mWindow as MainWindow;
			StringBuilder newBuilder = new StringBuilder();

			#region Validation
			//  Get the binding variables for validation
			var genderExpression = gender.GetBindingExpression(ComboBox.TextProperty);
			var birthDateExpression = birthDate.GetBindingExpression(DatePicker.TextProperty);
			var nameExpression = name.GetBindingExpression(TextBox.TextProperty);
			var userNameExpression = userName.GetBindingExpression(TextBox.TextProperty);

			// Validate all fields
			if (genderExpression != null)
            {
                genderExpression.UpdateSource();
            }

            if (gender.SelectedIndex == 0)
            {
                Validation.MarkInvalid(genderExpression, new ValidationError(new ExceptionValidationRule(), genderExpression));
            }

            if (birthDateExpression != null)
            {
                birthDateExpression.UpdateSource();
            }

            if (nameExpression != null)
            {
                nameExpression.UpdateSource();
            }

            if (userNameExpression != null)
            {
                userNameExpression.UpdateSource();
            }

            // If there are any validation errors, immediately deny the operation
            foreach (var item in MainGrid.Children)
			{
				DependencyObject dependencyObject = item as DependencyObject;
				if (dependencyObject == null)
                {
                    continue;
                }

                if (Validation.GetHasError(dependencyObject))
                {
                    return;
                }
            }

			// Ensure the passwords match.
			if (password.Password != confirmPassword.Password)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle,
					IntPtr.Zero, "Incorrect Passwords", "The passwords do not match.", "Please retype the password.",
					TaskDialogButtons.Ok, TaskDialogIcon.Warning);
				password.Clear();
				confirmPassword.Clear();
				return;
			}

			// Ensure valid passwords have been entered
			if (password.Password.Length < 8 || string.IsNullOrWhiteSpace(password.Password))
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle,
					IntPtr.Zero, "Invalid Password", "This password is not acceptable.",
					"The password must contain at least 8 characters. Passwords with only space will not be accepted.",
					TaskDialogButtons.Ok, TaskDialogIcon.Warning);
				password.Clear();
				confirmPassword.Clear();
				return;
			}
			#endregion

			if (window == null)
			{
				MessageBox.Show("Error", "An error has occurred. Please try again.", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (!Directory.Exists(usersLocation))
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(window).Handle, IntPtr.Zero,
					"Setup Error", "Record Pro has not been properly installed.",
					"The file location needs to be updated. Please consult the documentation or contact Autosoft.",
					TaskDialogButtons.Ok, TaskDialogIcon.Error);
				return;
			}

			// Prepare the new user
			newBuilder.AppendFormat("Name = \"{0}\" UserName = \"{1}\" Password = \"{2}\" Gender = \"{3}\" Image = \"{4}\" BirthDate = \"{5}\"",
				name.Text,userName.Text, password.Password, gender.Text, image.Content, birthDate.SelectedDate);
			Guid guid = Guid.NewGuid();
			string folderName = guid.ToString("B");
			string newLocation = IO.Path.Combine(usersLocation, folderName);
			string newConfigLocation = IO.Path.Combine(newLocation, "config.txt");
			string data = newBuilder.ToString();
			string newImageLocation = "";
			if (image.Content != null)
            {
                newImageLocation = IO.Path.Combine(" ", newLocation, image.Content.ToString());
            }

            // Create the new user
            await CreateUser(newLocation, newConfigLocation, data, newImageLocation);
		}

		/// <summary>
		/// Adds the new user
		/// </summary>
		/// <param name="newLocation">The location for the new user</param>
		/// <param name="newConfigLocation">The configuration file for the new user</param>
		/// <param name="data">The data for the new user</param>
		/// <param name="newImageLocation">The image for the new user</param>
		/// <returns>A task which describes progress and other information</returns>
		private async Task CreateUser(string newLocation, string newConfigLocation, string data, string newImageLocation)
		{
			try
			{
				// Create the user
				IO.Directory.CreateDirectory(newLocation);
				IO.Directory.CreateDirectory(IO.Path.Combine(newLocation, "Grades"));
				using (var newWriter = new IO.StreamWriter(newConfigLocation))
				{
					await newWriter.WriteAsync(data);
				}
			}
			catch (IO.IOException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "File Error",
					"An error has occurred. The user could not be saved.",
					TaskDialogButtons.Ok, TaskDialogIcon.Error);
				return;
			}
			catch (ArgumentException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "Invalid Characters Found",
					"An error has occurred. Invalid characters have been found in the users path. Please contact the Administrator.",
					TaskDialogButtons.Ok, TaskDialogIcon.Error);
				return;
			}
			catch (UnauthorizedAccessException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "Access Denied",
									"An error has occurred. Access to the users path has been denied. If the problem persists, please contact the Administrator. "
								+ "The settings could not be saved.", TaskDialogButtons.Ok, TaskDialogIcon.Error);
				return;
			}
			catch (SecurityException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "Access Denied",
									"An error has occurred. Access to the users path has been denied. If the problem persists, please contact the Administrator. "
								+ "The settings could not be saved.", TaskDialogButtons.Ok, TaskDialogIcon.Error);
				return;
			}
			catch (NotSupportedException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "Formatting Error",
									"An error has occurred. The users path is not formatted correctly or an invalid name has been entered. The user could not be saved. "
					+ "If the problem persists, please contact the administrator.", TaskDialogButtons.Ok, TaskDialogIcon.Error);
				return;
			}

			try
			{
				// Attempt to upload the user's image
				if (IO.File.Exists(oldImageLocation))
                {
                    IO.File.Copy(oldImageLocation, newImageLocation);
                }
            }
			catch (IO.IOException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "File Error",
					"An error has occurred. The image could not be uploaded.",
					TaskDialogButtons.Ok, TaskDialogIcon.Error);
			}

			catch (UnauthorizedAccessException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "Access Denied",
									"An error has occurred. Access to the user path has been denied. "
				+ "The image could not be updated If the problem persists, please contact the Administrator. "
								+ "The image could not be saved.", TaskDialogButtons.Ok, TaskDialogIcon.Error);
			}

			catch (NotSupportedException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "Formatting Error",
									"An error has occurred. The user path is not formatted correctly. The image could not be updated. Please contact the Administrator.",
									TaskDialogButtons.Ok, TaskDialogIcon.Error);
			}

			// Only continue if no user is currently logged in.
			if (Application.Current.Properties.Contains("Current User") && Application.Current.Properties["Current User"].ToString() != "None")
			{
				// Load the new pane
				var newWindow = new Home();
				this.NavigationService.Navigate(newWindow);
				return;
			}

			#region Logon User
			try
			{
				// Load the image
				if (Application.mWindow != null && IO.File.Exists(newImageLocation))
				{
					var newImage = new BitmapImage();
					newImage.BeginInit();
					newImage.UriSource = new Uri(newImageLocation, UriKind.Absolute);
					newImage.DecodePixelWidth = 40;
					newImage.EndInit();
					Application.mWindow.Avatar.Source = newImage;
				}
				else
                {
                    LoadDefaultImage(); // Load the default image
                }
            }
			catch (IO.FileNotFoundException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "The image could not be updated.",
				"An error has occurred. The image could not be found. Please contact the Administrator.",
				TaskDialogButtons.Ok, TaskDialogIcon.Error);

				LoadDefaultImage(); // Load the default image
			}
			catch (UriFormatException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "The image could not be updated.",
									"An error has occurred. The image could not be loaded. Please contact the Administrator.",
									TaskDialogButtons.Ok, TaskDialogIcon.Error);
				LoadDefaultImage(); // Load the default image
			}
			catch (UnauthorizedAccessException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Image Error - Record Pro", "The image could not be updated.",
					"An error has occurred. Access to the image location is denied. The image could not be update. If the problem continues, please contact an administrator.",
				TaskDialogButtons.Ok, TaskDialogIcon.Error);
				LoadDefaultImage(); // Load the default image
			}

			// Logon the user
			Application.LogOn(data, newLocation);
			#endregion
		}

		/// <summary>
		/// Loads the default image
		/// </summary>
		private void LoadDefaultImage()
		{
			string Url; // The URL of the image to display.
			if (gender.Text == "Male")
            {
                Url = "Generic Avatar (Male).png";
            }
            else if (gender.Text == "Female")
            {
                Url = "Generic Avatar (Female).png";
            }
            else
            {
                Url = "Generic Avatar (Unisex).png";
            }

            try
			{
				// Update the window
				if (Application.mWindow != null)
				{
					var newImage = new BitmapImage();
					newImage.BeginInit();
					newImage.UriSource = new Uri(Url, UriKind.Relative);
					newImage.DecodePixelWidth = 40;
					newImage.EndInit();
					Application.mWindow.Avatar.Source = newImage;
				}
			}
			catch (IO.FileNotFoundException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "The default image could not be loaded.",
						"An error has occurred. The default image could not be loaded. If the problem continues, please contact the Administrator.",
						TaskDialogButtons.Ok, TaskDialogIcon.Error);
			}
			catch (UriFormatException)
			{
				RecordPro.NativeMethods.TaskDialog(new WindowInteropHelper(Application.Current.MainWindow).Handle, IntPtr.Zero, "Error - Record Pro", "The default image could not be loaded.",
						"An error has occurred. The default image could not be loaded. If the problem continues, please contact the Administrator.",
						TaskDialogButtons.Ok, TaskDialogIcon.Error);
			}
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			// Prompt the user to enter a file name and update the image if it exists
			if (openFileDialog1.ShowDialog() == true)
			{
				oldImageLocation = openFileDialog1.FileName;
				image.Content = IO.Path.GetFileName(oldImageLocation);
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			name.Focus(); // Allow the user to begin typing when the window is opened
		}

		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			foreach (var item in MainGrid.Children)
			{
				DependencyObject dependencyObject = item as DependencyObject;
				if (dependencyObject == null)
                {
                    continue;
                }

                if (Validation.GetHasError(dependencyObject))
				{
					e.CanExecute = false;
					return;
				}
			}

			// We must make sure the terms and conditions box is checked
			if (termsandConditions.IsChecked == true)
            {
                e.CanExecute = true;
            }
        }
	}
}
