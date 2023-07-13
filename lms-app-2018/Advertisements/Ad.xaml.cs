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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecordPro
{
	/// <summary>
	/// Interaction logic for Ad.xaml
	/// </summary>
	public partial class Ad : Window
	{
		public Ad(string windowTitle, object source)
		{
			InitializeComponent();
			if (source != null)
			{
				this.Title = windowTitle;
				this.MainFrame.Content = source;
			}
			else
            {
                this.Close();
            }
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}

		private void SystemCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void SystemCommands_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SystemCommands.CloseWindow(this);
		}

			private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DoubleAnimation newAnimation = new DoubleAnimation(1, TimeSpan.Parse("0:0:0.5"));
			newAnimation.EasingFunction = new BounceEase();
			this.BeginAnimation(MainWindow.OpacityProperty, newAnimation);
		}
	}
}
