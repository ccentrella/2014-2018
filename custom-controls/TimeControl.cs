using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Autosoft_Controls_2017
{
	/// <summary>
	/// Represents a control that allows the user to select hours, minutes, and seconds
	/// </summary>
	[TemplatePart(Name = "PART_Hour", Type = typeof(NumericUpDown))]
	[TemplatePart(Name = "PART_Minute", Type = typeof(NumericUpDown))]
	[TemplatePart(Name = "PART_Second", Type = typeof(NumericUpDown))]
	public class TimeControl : Control
	{
		NumericUpDown hour;
		NumericUpDown minute;
		NumericUpDown second;

		public TimeControl()
		{
			//this.Resources.MergedDictionaries.Add(SharedDictionaryManager.SharedDictionary);
		}

		/// <summary>
		/// Occurs when the template is applied
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			// Get the template parts
			hour = base.GetTemplateChild("PART_Hour") as NumericUpDown;
			minute = base.GetTemplateChild("PART_Minute") as NumericUpDown;
			second = base.GetTemplateChild("PART_Second") as NumericUpDown;

			// Get each event for when the text is changed
			if (hour != null)
				hour.ValueChanged += hour_ValueChanged;
			if (minute != null)
				minute.ValueChanged += minute_ValueChanged;
			if (second != null)
				second.ValueChanged += second_valueChanged;

			UpdateControls(Time, this); // Ensure the time controls are updated
		}

		private void second_valueChanged(object sender, RoutedEventArgs e)
		{
			UpdateTime();
		}

		private void minute_ValueChanged(object sender, RoutedEventArgs e)
		{
			UpdateTime();
		}

		private void hour_ValueChanged(object sender, RoutedEventArgs e)
		{
			UpdateTime();
		}

		private void UpdateTime()
		{
			if (hour != null & minute != null & second != null)
			{
                Time = new TimeSpan((int)hour.Value, (int)minute.Value, (int)second.Value);
			}
		}

		static TimeControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeControl),
				new FrameworkPropertyMetadata(typeof(TimeControl)));

			// Register the Time dependency property
			TimeProperty = DependencyProperty.Register("Time", typeof(TimeSpan),
				typeof(TimeControl), new PropertyMetadata(new TimeSpan(0, 5, 0),
					new PropertyChangedCallback(OnTimeChanged)));
		}

		private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue == e.OldValue)
				return;

			TimeSpan value = (TimeSpan)e.NewValue;
			var r = (TimeControl)d;
			value = UpdateControls(value, r);
		}

		private static TimeSpan UpdateControls(TimeSpan value, TimeControl r)
		{
			var hour = r.GetTemplateChild("PART_Hour") as NumericUpDown;
			var minute = r.GetTemplateChild("PART_Minute") as NumericUpDown;
			var second = r.GetTemplateChild("PART_Second") as NumericUpDown;

			if (hour != null)
				hour.Value = value.Hours;
			if (minute != null)
				minute.Value = value.Minutes;
			if (second != null)
				second.Value = value.Seconds;
			return value;
		}

		/// <summary>
		/// Represents the combined time
		/// </summary>
		public TimeSpan Time
		{
			get { return (TimeSpan)GetValue(TimeProperty); }
			set { SetValue(TimeProperty, value); }
		}

		/// <summary>
		/// The Time dependency property
		/// </summary>
		public static DependencyProperty TimeProperty;

	}
}
