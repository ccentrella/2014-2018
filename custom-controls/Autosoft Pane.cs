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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autosoft_Controls_2017
{

	/// <summary>
	///  Represents window-like panes that can be moved and resized, and that are contained in a PanePanel.
	/// </summary>
	[ContentPropertyAttribute("Content")]
	public class Pane : Control
	{
		static Pane()
		{
			// Override style.
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Pane), new FrameworkPropertyMetadata(typeof(Pane)));
		}

		/// <summary>
		/// The Header dependency property.
		/// </summary>
		public static DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof(string), typeof(Pane));

		/// <summary>
		/// The Content dependency property.
		/// </summary>
		public static DependencyProperty ContentProperty =
		 DependencyProperty.Register("Content", typeof(object), typeof(Pane));

		/// <summary>
		/// The PinButton dependency property.
		/// </summary>
		public static DependencyProperty PinButtonContentProperty =
			DependencyProperty.Register("PinButtonContent", typeof(object), typeof(Pane));

		/// <summary>
		/// The UnpinButton dependency property.
		/// </summary>
		/// 
		public static DependencyProperty UnpinButtonContentProperty =
			 DependencyProperty.Register("UnpinButtonContent", typeof(object), typeof(Pane));


		/// <summary>
		/// The CloseButton dependency property.
		/// </summary>
		public static DependencyProperty CloseButtonContentProperty =
		DependencyProperty.Register("CloseButtonContent", typeof(object), typeof(Pane));

		/// <summary>
		/// Represents the header of the control, which usually consists of text.
		/// </summary>
		public string Header
		{
			get { return (string)GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}

		/// <summary>
		/// Represents the main body of the control.
		/// </summary>
		public object Content
		{
			get { return (object)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		/// <summary>
		/// Represents the content for the pin button.
		/// </summary>
		public object PinButtonContent
		{
			get { return (object)GetValue(PinButtonContentProperty); }
			set { SetValue(PinButtonContentProperty, value); }
		}

		/// <summary>
		/// Represents the content for the unpin button.
		/// </summary>
		public object UnpinButtonContent
		{
			get { return (object)GetValue(UnpinButtonContentProperty); }
			set { SetValue(UnpinButtonContentProperty, value); }
		}

		/// <summary>
		/// Represents the content for the close button.
		/// </summary>
		public object CloseButtonContent
		{
			get { return (object)GetValue(CloseButtonContentProperty); }
			set { SetValue(CloseButtonContentProperty, value); }
		}
	}
}
