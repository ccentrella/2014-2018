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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace Image_Creator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		SaveFileDialog saveDialog = new SaveFileDialog()
		{
			Title = "Save File - Image Creator",
			Filter = "Image Creator Files|*.icdw",
			FileName = "Untitled"
		};
		SaveFileDialog imageDialog = new SaveFileDialog()
		{
			Title = "Convert to Image - Image Creator",
			Filter = "PNG Image|*.png|JPEG Image|*.jpg|Tiff Image|*.tiff|"
			+ "GIF Image|*.gif|Window Bitmap|*.bmp|Window Media Format|*.wdp",
			FileName = "Image"
		};
		OpenFileDialog openDialog = new OpenFileDialog()
		{
			Title = "Open File - Image Creator",
			Filter = "Image Creator Files|*.icdw",

		};
		string previousData = ""; // Check if the item must be saved

		/// <summary>
		/// Defines a command used to reset the zoom
		/// </summary>
		public static RoutedUICommand ResetZoom = new RoutedUICommand("_Reset Zoom", "ResetZoom", typeof(MainWindow));

		/// <summary>
		/// Defines a command used to save the file as an image
		/// </summary>
		public static RoutedUICommand SaveImageAs = new RoutedUICommand("Save As _Image", "SaveAsImage", typeof(MainWindow));

		/// <summary>
		/// Defines a command used to change the color of the border on the drawing
		/// </summary>
		public static RoutedUICommand ChangeBorderColor = new RoutedUICommand(
			"Change _Border Color", "ChangeBorderColor", typeof(MainWindow));

		/// <summary>
		/// Defines a command used to change the color of the fill on the drawing 
		/// </summary>
		public static RoutedUICommand ChangeFillColor = new RoutedUICommand(
			"Change _Fill Color", "ChangeFillColor", typeof(MainWindow));

		/// <summary>
		/// Defines a command used to change the thickness of the border on the drawing
		/// </summary>
		public static RoutedUICommand ChangeBorderThickness = new RoutedUICommand(
			"Change Border _Thickness", "ChangeBorderThickness", typeof(MainWindow));

		string fileName = "Untitled";
		public MainWindow()
		{
			InitializeComponent();
		}


		private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Save();
		}

		/// <summary>
		/// Prompts the user to save the file
		/// </summary>
		private MessageBoxResult PromptUserToSaveFile()
		{
			string fileNameString = System.IO.Path.GetFileName(fileName);
			var result = MessageBox.Show("Do you want to save the changes you made to " + fileNameString + "?",
			"Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
			return result;
		}

		/// <summary>
		/// Saves the file if one exists. Otherwise, prompts the user to create a new file.
		/// </summary>
		/// <returns>True if the file was saved; otherwise, false</returns>
		private bool Save()
		{
			// Ensure the file has been saved or opened before
			if (fileName != "Untitled")
			{
				try
				{
					using (var writer = new StreamWriter(fileName))
					{
						writer.Write(data.Text);
						previousData = data.Text;
					}
					return true;
				}
				catch (UnauthorizedAccessException)
				{
					MessageBox.Show("An error has occurred. The file could not be saved. Access has been denied.",
						"Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return false;
				}
				catch (SecurityException)
				{
					MessageBox.Show("An error has occurred. The file could not be saved. The program does not have the required permission.",
						"Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return false;
				}
				catch (ArgumentException)
				{
					MessageBox.Show("An error has occurred. The file could not be saved. Invalid characters were found in the file path.",
											"Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return false;
				}
				catch (IOException)
				{
					MessageBox.Show("An error has occurred. The file could not be saved.",
											"Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return false;
				}
			}
			else
			{
				return SaveAs();
			}
		}

		/// <summary>
		/// Prompts the user for a file name and saves the file
		/// </summary>
		/// <returns>True if the file was saved; otherwise, false.</returns>
		private bool SaveAs()
		{
			saveDialog.FileName = "Untitled";
			if (saveDialog.ShowDialog() == true)
			{
				fileName = saveDialog.FileName; // Change the file location
				return Save(); // Now save the file
			}
			else
			{
				return false;
			}
		}

		private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SaveAs(); // Show a save as dialog
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Open(); // Open the file
		}

		/// <summary>
		/// Shows the open dialog and opens a new file
		/// </summary>
		private void Open()
		{
			// Prompt the user to save changes
			if (data.Text != previousData)
			{
				var result = PromptUserToSaveFile();
				if (result == MessageBoxResult.Cancel)
					return;
				else if (result == MessageBoxResult.Yes && !Save())
					return;
			}

			// Now open the file
			if (openDialog.ShowDialog() == true)
			{
				Open(openDialog.FileName);
			}
		}

		/// <summary>
		/// Opens a file
		/// </summary>
		/// <param name="fileLocation">The location of the file to open</param>
		private void Open(string fileLocation)
		{
			try
			{
				fileName = fileLocation;
				using (var reader = new StreamReader(fileName))
				{
					data.Text = reader.ReadToEnd();
				}
				previousData = data.Text;

				// Update the jump list
				JumpList.AddToRecentCategory(new JumpPath() { Path = fileName });
			}
			catch (ArgumentException)
			{
				MessageBox.Show("An error has occurred. The file could not be opened. Invalid characters were found in the file path.",
										"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (IOException)
			{
				MessageBox.Show("An error has occurred. The file could not be opened.",
										"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (previousData == data.Text & fileName != "Untitled")
				e.CanExecute = false;
			else
				e.CanExecute = true;
		}

		private void SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Prompt the user to save changes
			if (data.Text != previousData)
			{
				var result = PromptUserToSaveFile();
				if (result == MessageBoxResult.Cancel)
					e.Cancel = true;
				else if (result == MessageBoxResult.Yes && !Save())
					e.Cancel = true;
			}
		}

		private void ResetZoom_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			viewBox.Width = double.NaN;
			viewBox.Height = double.NaN;
		}
		private void DecreaseZoom_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			viewBox.Width = viewBox.ActualWidth / 2;
			viewBox.Height = viewBox.Height / 2;
		}



		private void IncreaseZoom_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			viewBox.Width = viewBox.ActualWidth * 2;
			viewBox.Height = viewBox.ActualHeight * 2;
		}

		private void DecreaseZoom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (viewBox.ActualWidth / 2 > 0 & viewBox.ActualHeight / 2 > 0)
				e.CanExecute = true;
			else
				e.CanExecute = false;
		}

		private void IncreaseZoom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (viewBox.ActualWidth * 2 <= double.MaxValue & viewBox.ActualHeight * 2 <= double.MaxValue)
				e.CanExecute = true;
			else
				e.CanExecute = false;
		}

		private void ResetZoom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (!double.IsNaN(viewBox.Height) | !double.IsNaN(viewBox.Width))
				e.CanExecute = true;
			else
				e.CanExecute = false;
		}

		private void New_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			// Prompt the user to save changes
			if (data.Text != previousData)
			{
				var result = PromptUserToSaveFile();
				if (result == MessageBoxResult.Cancel)
					return;
				else if (result == MessageBoxResult.Yes && !Save())
					return;
			}

			// Prepare the new file
			fileName = "Untitled";
			previousData = "";
			data.Clear();
		}

		private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (previousData != "" | data.Text != "" | fileName != "Untitled")
				e.CanExecute = true;
			else
				e.CanExecute = false;
		}

		private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (data.SelectionLength > 0)
				e.CanExecute = true;
			else
				e.CanExecute = false;
		}

		private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			// Clear all selected data
			int index = data.SelectionStart;
			data.Text = data.Text.Remove(data.SelectionStart, data.SelectionLength);

			// Move the cursor to the appropriate location
			data.SelectionStart = index;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Open the selected file
			var args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
				Open(args[1]);
		}
		private void CreateImage()
		{
			var pen = new Pen(path.Stroke, 5);

			// Only continue if the user clicked okay
			imageDialog.FileName = "Image";
			if (imageDialog.ShowDialog() != true)
				return;

			// Use the correct encoder
			BitmapEncoder encoder;
			var extension = System.IO.Path.GetExtension(imageDialog.FileName).ToLowerInvariant();
			if (extension == ".png")
				encoder = new PngBitmapEncoder();
			else if (extension == ".jpg")
				encoder = new JpegBitmapEncoder();
			else if (extension == ".tiff")
				encoder = new TiffBitmapEncoder();
			else if (extension == ".gif")
				encoder = new GifBitmapEncoder();
			else if (extension == ".wdp")
				encoder = new WmpBitmapEncoder();
			else if (extension == ".bmp")
				encoder = new BmpBitmapEncoder();
			else
			{
				MessageBox.Show("An invalid file format was selected. Please selected a valid file format.",
					"Invalid Format", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Encode the image
			DrawingVisual newVisual = new DrawingVisual();
			DrawingContext newContext = newVisual.RenderOpen();
			newContext.DrawDrawing(new GeometryDrawing(path.Fill, pen, path.Data));
			newContext.Close();
			double width = 1;
			if (viewBox.ActualHeight > 0 & viewBox.ActualWidth > 0)
				width = viewBox.ActualWidth / viewBox.ActualHeight;
			width *= 2400;
			var source = new RenderTargetBitmap((int)width, 2400, (int)width, 2400, PixelFormats.Pbgra32);
			source.Render(newVisual);
			encoder.Frames.Add(BitmapFrame.Create(source));

			// Save the image
			try
			{
				using (Stream stream = File.Create(imageDialog.FileName))
				{
					encoder.Save(stream);
				}
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show("An error has occurred. The image could not be saved. Access has been denied.",
					"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (SecurityException)
			{
				MessageBox.Show("An error has occurred. The image could not be saved. The program does not have the required permission.",
					"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (ArgumentException)
			{
				MessageBox.Show("An error has occurred. The image could not be saved. Invalid characters were found in the file path.",
										"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (IOException)
			{
				MessageBox.Show("An error has occurred. The image could not be saved.",
										"Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void SaveImageAs_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			CreateImage(); // Save the path as an image
		}

		private void SaveImageAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true; // This command will always be enabled
		}

		private void ChangeBorderThickness_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void ChangeBorderThickness_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			thicknessPopup.IsOpen = true;
		}
	}
}
