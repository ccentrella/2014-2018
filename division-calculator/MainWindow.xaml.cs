using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Division_Calculator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>

	public partial class MainWindow : Window
	{
		bool Divisorbool = false;
		public MainWindow()
		{

			InitializeComponent();

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Button SenderButton = (Button)sender;
			if (!Divisorbool)
			{
				Dividend.SelectedText = (string)SenderButton.Content;
				Dividend.SelectionStart += Dividend.SelectionLength;
			}
			else
			{
				Divisor.SelectedText = (string)SenderButton.Content;
				Divisor.SelectionStart += Divisor.SelectionLength;
			}
		}

		private void Clear_Click(object sender, RoutedEventArgs e)
		{

			Dividend.Clear();
			Divisor.Clear();
			Answer.Content = "";
			Dividend.Focus();
		}

		private void Dividend_GotFocus(object sender, RoutedEventArgs e)
		{
			Divisorbool = false;
		}

		private void Divisor_GotFocus(object sender, RoutedEventArgs e)
		{
			Divisorbool = true;
		}

		private void Equals_Click(object sender, RoutedEventArgs e)
		{
			CheckAnswer();
		}

		private void CheckAnswer()
		{
			int Dividendint;
			int Divisorint;
			int Total;
			string result;
			// If either of the text-boxes isn't an integer, get out.
			if (int.TryParse(Dividend.Text, out Dividendint) == false |
				int.TryParse(Divisor.Text, out Divisorint) == false)
			{
				MessageBox.Show("One or more of the values is invalid.\n\nPlease enter valid numbers.",
					"Invalid Numbers", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			try
			{
				if (Divisorint == 0)
					Total = 0;
				else
					Total = Dividendint / Divisorint;
				result = Total.ToString();
				if (Divisorint != 0)
				{
					int ModResult = Dividendint % Divisorint;
					if (ModResult != 0)
					{
						result += " R " + ModResult.ToString();
					}
				}
				Answer.Content = result;
			}
			catch (Exception)
			{
				MessageBox.Show("One or more of the values is invalid.\n\nPlease enter valid numbers.",
					"Invalid Numbers", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		private void Copy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (Answer.Content != null)
				e.CanExecute = true;
		}

		private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Copy();
		}

		private void Copy()
		{
			try
			{
				Clipboard.SetText(Answer.Content.ToString());
			}
			catch (COMException)
			{
				MessageBox.Show("An error has occurred. The clipboard could not be accessed.",
					"Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

	}
}
