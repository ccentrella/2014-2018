using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Autosoft_Clock
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer timer1 = new DispatcherTimer() { Interval = new TimeSpan(10000) };
		DispatcherTimer timer2 = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
		Stopwatch Stopwatch1 = new Stopwatch();
		bool stopped = false; // Determines whether or not all text should be cleared when the timer is started

		public MainWindow()
		{
			InitializeComponent();
			timer1.Tick += timer1_Tick;
			timer2.Tick += timer2_Tick;
			timer2.Start(); // Start updating the time
		}

		void timer2_Tick(object sender, EventArgs e)
		{
			currentTime.Content = DateTime.Now.ToShortTimeString(); // Update the time
			currentTime.ToolTip = DateTime.Now.ToLongDateString(); // Update the date
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			UpdateTime();
		}

		/// <summary>
		/// Updates the time label
		/// </summary>
		private void UpdateTime()
		{
			#region Variables
			string hours = Stopwatch1.Elapsed.Hours.ToString();
			string minutes = Stopwatch1.Elapsed.Minutes.ToString();
			string seconds = Stopwatch1.Elapsed.Seconds.ToString();
			string milliseconds = Stopwatch1.Elapsed.Milliseconds.ToString();
			StringBuilder newStringBuilder = new StringBuilder();
			#endregion

			if (Stopwatch1.Elapsed.Hours < 10)
				hours = "0" + hours;
			if (Stopwatch1.Elapsed.Minutes < 10)
				minutes = "0" + minutes;
			if (Stopwatch1.Elapsed.Seconds < 10)
				seconds = "0" + seconds;
			if (Stopwatch1.Elapsed.Milliseconds < 10)
				milliseconds = "00" + milliseconds;
			else if (Stopwatch1.Elapsed.Milliseconds < 100)
				milliseconds = "0" + milliseconds;

			// Show the results
			newStringBuilder.AppendFormat("{0}:{1}:{2}.{3}", hours, minutes, seconds, milliseconds);
			timeLabel.Content = newStringBuilder.ToString();

			if (SmallClockPopup.IsOpen)
				SmallClockPopup.ToolTip = String.Format("Click here to restore clock ({0})", newStringBuilder.ToString());
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove(); // Move the window while the left mouse button is being held down
		}

		private void startButton_Click(object sender, RoutedEventArgs e)
		{
			Start();
		}

		private void pauseButton_Click(object sender, RoutedEventArgs e)
		{
			Pause();
		}

		private void stopButton_Click(object sender, RoutedEventArgs e)
		{
			Stop();
		}

		private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// If the main ellipse was double clicked, close the program
			Ellipse EllipseSource = e.OriginalSource as Ellipse;
			if (EllipseSource != null && EllipseSource.Name == "MainEllipse")
				Application.Current.Shutdown();
		}

		/// <summary>
		///  Starts the stopwatch
		/// </summary>

		private void Start()
		{
			// When this button is clicked, the time cannot be in a stopped state
			if (stopped)
			{
				stopped = false;
				Stopwatch1.Reset();
			}

			// Hide start button, and show the pause and stop buttons
			startButton.Visibility = Visibility.Collapsed;
			pauseButton.Visibility = Visibility.Visible;
			stopButton.Visibility = Visibility.Visible;

			// Start the stopwatch and timer
			Stopwatch1.Start();
			timer1.Start();
		}

		/// <summary>
		/// Pauses the stopwatch
		/// </summary>
		private void Pause()
		{
			Stopwatch1.Stop();
			timer1.Stop();

			// Hide the pause button and show the start button
			pauseButton.Visibility = Visibility.Collapsed;
			startButton.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Stops the stopwatch
		/// </summary>
		private void Stop()
		{
			stopped = true;
			Stopwatch1.Stop();
			timer1.Stop();

			// Hide the pause button and stop button, and show the start button
			pauseButton.Visibility = Visibility.Collapsed;
			stopButton.Visibility = Visibility.Collapsed;
			startButton.Visibility = Visibility.Visible;
		}

		// When a key is pressed, toggle between on and paused
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (AppScreen.Visibility == Visibility.Collapsed)
				return;

			if (e.Key == Key.Enter | e.Key == Key.Space)
			{
				if (timer1.IsEnabled)
					Pause();
				else
					Start();
			}
		}

		private void HideButton_Click(object sender, RoutedEventArgs e)
		{
			// Hide the app
			AppScreen.Visibility = Visibility.Collapsed;

			// Position and show the clock button
			double lWindowMargin = this.Left;
			Random newRandom = new Random();
			double lMargin = SystemParameters.MaximumWindowTrackWidth - lWindowMargin - 20;
			int minHeight = 100 - (int)this.Top;
			int maxHeight = (int)SystemParameters.MaximizedPrimaryScreenHeight - (int)this.Top - 100;
			SmallClockPopup.ToolTip = String.Format("Click here to restore clock ({0})", timeLabel.Content.ToString());
			SmallClockPopup.HorizontalOffset = lMargin;
			SmallClockPopup.VerticalOffset = newRandom.Next(minHeight, maxHeight);
			SmallClockPopup.IsOpen = true;
		}

		private void SmallClock_Click(object sender, RoutedEventArgs e)
		{
			SmallClockPopup.IsOpen = false; // Hide the clock button
			AppScreen.Visibility = Visibility.Visible; // Show the app screen
		}

		private void CopyButton_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(timeLabel.Content.ToString()); // Copy the current time to the clipboard
		}
	}
}
