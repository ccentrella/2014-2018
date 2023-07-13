using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Autosoft_Controls_2017
{
	/// <summary>
	/// Defines a basic color palette
	/// </summary>
	[TemplatePart(Name = "PART_Parent", Type = typeof(WrapPanel))]
	class ColorDialog : Control
	{
		WrapPanel panel;
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			panel = base.GetTemplateChild("PART_Parent") as WrapPanel;

		}

		/// <summary>
		/// The selected color dependency property
		/// </summary>
		public static DependencyProperty SelectedColorProperty;

		public  ColorDialog()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorDialog), new FrameworkPropertyMetadata(typeof(NumericUpDown)));

			// Register the selected color dependency property
			DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(ColorDialog));
		}

		/// <summary>
		/// Gets or sets the selected color
		/// </summary>
		public Brush SelectedColor
		{
			get { return (Brush)GetValue(SelectedColorProperty); }
			set
			{
				SetValue(SelectedColorProperty, value);
				if (panel != null)
				{
					foreach (var child in panel.Children)
					{
						ToggleButton button = child as ToggleButton;

						// Only continue if the item is a toggle button
						if (button != null && button.Background == value)
						{
							var newPath = new Path();
						}
						else if (button != null)
						{
							button.Content = "";
						}
					}
				}
			}

		}

		/// <summary>
		/// Occurs when the selected color is changed.
		/// </summary>
		/// <param name="e">The arguments passed to the ValueChanged Handler.</param>
		protected virtual void OnColorChanged(SelectedColorChangedEventArgs e)
		{
			var SelectedColorEventHandler = SelectedColorChanged;
			if (SelectedColorEventHandler != null)
			{
				SelectedColorEventHandler(this, e);
			}
		}

		/// <summary>
		/// The ValueChanged event is raised before the selected color changes.
		/// </summary>
		public event EventHandler<SelectedColorChangedEventArgs> SelectedColorChanged;

		/// <summary>
		/// This represents the arguments to be used when the selected color changes.
		/// </summary>
		public class SelectedColorChangedEventArgs : EventArgs
		{
			/// <summary>
			/// This represents the old value.
			/// </summary>
			public double OldValue { get; protected set; }

			/// <summary>
			/// This represents the new value.
			/// </summary> 
			public double NewValue { get; protected set; }

			/// <summary>
			/// This represents whether to change the value, or not.
			/// </summary>
			public bool Canceled { get; set; }

			/// <summary>
			/// The ValueChangedEventArgs Constructor
			/// </summary>
			/// <param name="passedOldValue">The old value of the range</param>
			/// <param name="passedNewValue">The new value of the range</param>
			public SelectedColorChangedEventArgs(double passedOldValue, double passedNewValue)
			{
				OldValue = passedOldValue;
				NewValue = passedNewValue;
			}

		}
	}
}
