using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autosoft_Controls_2017
{
	/// <summary>
	/// Represents a control with a textbox, an increment button, and a decrement button.
	/// </summary>
	[TemplatePart(Name = "PART_Increment", Type = typeof(RepeatButton))]
	[TemplatePart(Name = "PART_Decrement", Type = typeof(RepeatButton))]
	public class NumericUpDown : Control
	{
		// Initialize controls.
		RepeatButton IncrementButton = new RepeatButton();
		RepeatButton DecrementButton = new RepeatButton();

		public NumericUpDown()
		{
			//this.Resources.MergedDictionaries.Add(SharedDictionaryManager.SharedDictionary);
		}

		/// <summary>
		/// Occurs when the template is applied
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			// Retrieve the increment button.
			IncrementButton = base.GetTemplateChild("PART_Increment") as RepeatButton;

			// Retrieve the decrementButton.
			DecrementButton = base.GetTemplateChild("PART_Decrement") as RepeatButton;

			// Hook up the event handler for the increment button.
			if (IncrementButton != null)
				IncrementButton.Click += new RoutedEventHandler(IncrementButton_Click);

			// Hook up the event handler for the decrement button.
			if (DecrementButton != null)
				DecrementButton.Click += new RoutedEventHandler(DecrementButton_Click);
			CheckButtons();
		}

		static NumericUpDown()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));

			// Register the MinValue dependency property.
			MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(NumericUpDown),
				new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnMinValueChanged),
					new CoerceValueCallback(CoerceMinValue)));

			// Register the MaxValue dependency property.
			MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(NumericUpDown),
				new FrameworkPropertyMetadata(100.0, new PropertyChangedCallback(OnMaxValueChanged),
					new CoerceValueCallback(CoerceMaxValue)));

			// Register the Value dependency property.
			ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(NumericUpDown),
				new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnValueChanged),
					new CoerceValueCallback(CoerceValue)), new ValidateValueCallback(ValidateValue));

			// Register the IncrementAmount dependency property.
			IncrementAmountProperty = DependencyProperty.Register("IncrementAmount", typeof(double), typeof(NumericUpDown),
				new FrameworkPropertyMetadata(1.0), new ValidateValueCallback(ValidateIncrementDecrement));

			// Register the DecrementAmount dependency property.
			DecrementAmountProperty = DependencyProperty.Register("DecrementAmount", typeof(double), typeof(NumericUpDown),
				new FrameworkPropertyMetadata(1.0), new ValidateValueCallback(ValidateIncrementDecrement));

			// Register the IsDecrementEqual dependency property.
			IsDecrementEqualProperty = DependencyProperty.Register("IsDecrementEqual", typeof(bool), typeof(NumericUpDown),
				new FrameworkPropertyMetadata(true));

			// Register the NumberOfDecimalPlaces dependency property.
			NumberOfDecimalPlacesProperty = DependencyProperty.Register("NumberOfDecimalPlaces", typeof(int), typeof(NumericUpDown),
				new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnNumberOfDecimalPlacesChanged)),
				new ValidateValueCallback(ValidateNumberOfDecimalPlaces));
		}

		private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			obj.CoerceValue(MinValueProperty);
			obj.CoerceValue(MaxValueProperty);
			NumericUpDown r = (NumericUpDown)obj;
			var e = new RoutedPropertyChangedEventArgs<double>((double)args.OldValue,(double)args.NewValue,ValueChangedEvent);
			r.OnValueChanged(e);
			r.CheckButtons();
		}

		private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			d.CoerceValue(ValueProperty);
			NumericUpDown r = (NumericUpDown)d;
			r.CheckButtons();
		}

		private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			d.CoerceValue(ValueProperty);
			NumericUpDown r = (NumericUpDown)d;
			r.CheckButtons();
		}

		private static void OnNumberOfDecimalPlacesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown r = (NumericUpDown)d;
			r.CheckButtons();
		}

		private static object CoerceValue(DependencyObject d, object value)
		{
			NumericUpDown r = (NumericUpDown)d;
			double current = (double)value;

			if (current < r.MinValue)
				current = r.MinValue;

			if (current > r.MaxValue)
				current = r.MaxValue;

			return current;
		}

		private static object CoerceMinValue(DependencyObject d, object value)
		{
			NumericUpDown r = (NumericUpDown)d;
			double current = (double)value;

			if (current > r.MaxValue)
				current = r.MaxValue;

			if (current > r.Value)
				r.SetValue(ValueProperty, current);

			return current;
		}

		private static object CoerceMaxValue(DependencyObject d, object value)
		{
			NumericUpDown r = (NumericUpDown)d;
			double current = (double)value;

			if (current < r.MinValue)
				current = r.MinValue;

			if (r.Value > current)
				r.SetValue(ValueProperty, current);

			return current;
		}



		private static bool ValidateNumberOfDecimalPlaces(object value)
		{
			int i = (int)value;
			return i >= 0 & i < 28;
		}

		private static bool ValidateValue(object value)
		{
			double d;
			return double.TryParse(value.ToString(), out d);
		}

		private static bool ValidateIncrementDecrement(object value)
		{
			double d = (double)value;
			return d > 0;
		}

		private void IncrementButton_Click(object sender, RoutedEventArgs e)
		{
			Value += IncrementAmount; // Update the value
		}

		private void DecrementButton_Click(object sender, RoutedEventArgs e)
		{
			if (IsDecrementEqual)
				Value -= IncrementAmount;
			else
				Value -= DecrementAmount;
		}

		/// <summary>
		/// The MinValue dependency property.
		/// </summary>
		public static DependencyProperty MinValueProperty;

		/// <summary>
		/// The MaxValue dependency property.
		/// </summary>
		public static DependencyProperty MaxValueProperty;

		/// <summary>
		/// The Value dependency property.
		/// </summary>
		public static DependencyProperty ValueProperty;

		/// <summary>
		/// The IncrementAmount dependency property.
		/// </summary>
		public static DependencyProperty IncrementAmountProperty;

		/// <summary>
		/// The DependencyAmount dependency property.
		/// </summary>
		public static DependencyProperty DecrementAmountProperty;

		/// <summary>
		/// The IsDecrementEqual dependency property.
		/// </summary>
		public static DependencyProperty IsDecrementEqualProperty;

		/// <summary>
		/// The NumberOfDecimalPlaces dependency property.
		/// </summary>
		public static DependencyProperty NumberOfDecimalPlacesProperty;

		/// <summary>
		/// Check whether the increment or decrement buttons should be enabled
		/// </summary>
		public void CheckButtons()
		{
			var roundedValue = Math.Round(Value, NumberOfDecimalPlaces, MidpointRounding.AwayFromZero);

			// Check if the increment button should be enabled.
			if (roundedValue + IncrementAmount <= MaxValue & IncrementButton != null)
				IncrementButton.IsEnabled = true;
			else if (IncrementButton != null)
				IncrementButton.IsEnabled = false;

			// Check if the decrement button should be enabled.
			if (roundedValue - DecrementAmount >= MinValue & !IsDecrementEqual & DecrementButton != null)
				DecrementButton.IsEnabled = true;
			else if (roundedValue - IncrementAmount >= MinValue & IsDecrementEqual & DecrementButton != null)
				DecrementButton.IsEnabled = true;
			else if (DecrementButton != null)
				DecrementButton.IsEnabled = false;
		}

		/// <summary>
		/// The minimum value for the range.
		/// </summary>
		/// 
		public double MinValue
		{
			get { return (double)GetValue(MinValueProperty); }
			set { SetValue(MinValueProperty, value); }
		}

		/// <summary>
		/// The maximum value for the range.
		/// </summary>
		public double MaxValue
		{
			get { return (double)GetValue(MaxValueProperty); }
			set { SetValue(MaxValueProperty, value); }
		}

		/// <summary>
		/// The current value for the range.
		/// </summary>
		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}


		/// <summary>
		/// The amount to increment the value every time the up button is clicked.
		/// </summary>
		public double IncrementAmount
		{
			get { return (double)GetValue(IncrementAmountProperty); }
			set { SetValue(IncrementAmountProperty, value); }
		}

		/// <summary>
		/// The amount to decrement the value every time the down button is clicked.
		/// </summary>
		public double DecrementAmount
		{
			get { return (double)GetValue(DecrementAmountProperty); }
			set { SetValue(DecrementAmountProperty, value); }
		}

		/// <summary>
		/// Represents whether or not the down button decrement value is the same as the up button increment value.
		/// </summary>
		public bool IsDecrementEqual
		{
			get { return (bool)GetValue(IsDecrementEqualProperty); }
			set { SetValue(IsDecrementEqualProperty, value); }
		}

		/// <summary>
		/// Represents the number of decimal places to automatically round the value.
		/// </summary>
		public int NumberOfDecimalPlaces
		{
			get { return (int)GetValue(NumberOfDecimalPlacesProperty); }
			set { SetValue(NumberOfDecimalPlacesProperty, value); }
		}

		/// <summary>
		/// Occurs when the  value of the range changes
		/// </summary>
		public event RoutedEventHandler ValueChanged
		{
			add { AddHandler(ValueChangedEvent, value); }
			remove { RemoveHandler(ValueChangedEvent, value); }
		}

		/// <summary>
		/// Occurs when the  value of the range changes
		/// </summary>
		public static readonly RoutedEvent ValueChangedEvent =
			EventManager.RegisterRoutedEvent("ValueChanged", 
			RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericUpDown));

			/// <summary>
		/// Occurs when the value of the range is changed.
		/// </summary>
		/// <param name="e">The arguments passed to the ValueChanged Handler.</param>
		protected virtual void OnValueChanged(RoutedEventArgs e)
		{
			RaiseEvent(e);
		}

	}
}

