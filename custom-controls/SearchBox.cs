using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Autosoft_Controls_2017
{
	/// <summary>
	/// Represents a search control
	/// </summary>
	[TemplatePart(Name = "PART_ShowSearchButton", Type = typeof(Button))]
	[TemplatePart(Name = "PART_SearchButton", Type = typeof(Button))]
	[TemplatePart(Name = "PART_CancelButton", Type = typeof(Button))]
	[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
	public class SearchBox : TextBox
	{
		Button showSearchButton, searchButton, cancelButton;
		TextBox textBox;

		/// <summary>
		/// Initializes a new instance of SearchControl
		/// </summary>
		public SearchBox()
		{
			//this.Resources.MergedDictionaries.Add(SharedDictionaryManager.SharedDictionary);
		}

		/// <summary>
		/// Specified whether the search box should update while the user is typing text
		/// </summary>
		public bool AutomaticallyEnter
		{
			get { return (bool)GetValue(AutomaticallyEnterProperty); }
			set { SetValue(AutomaticallyEnterProperty, value); }
		}

		/// <summary>
		/// Specified whether or not the user is currently searching
		/// </summary>
		public bool IsSearching
		{
			get { return (bool)GetValue(IsSearchingProperty); }
		}

		/// <summary>
		/// Specifies whether the search textbox is visible
		/// </summary>
		public bool IsSearchOpen
		{
			get { return (bool)GetValue(IsSearchOpenProperty); }
			set { SetValue(IsSearchOpenProperty, value); }
		}

		/// <summary>
		/// Occurs when the template is applied
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			showSearchButton = base.GetTemplateChild("PART_ShowSearchButton") as Button;
			searchButton = base.GetTemplateChild("PART_SearchButton") as Button;
			cancelButton = base.GetTemplateChild("PART_CancelButton") as Button;
			textBox = base.GetTemplateChild("PART_TextBox") as TextBox;

			if (showSearchButton != null)
				showSearchButton.Click += showSearchButton_Click;

			if (searchButton != null)
				searchButton.Click += searchButton_Click;

			if (cancelButton != null)
				cancelButton.Click += cancelButton_Click;

			if (textBox != null)
			{
				textBox.TextChanged += textBox_TextChanged;
				textBox.KeyDown += textBox_KeyDown;
			}
		}

		/// <summary>
		/// The AutomaticallyEnter dependency property
		/// </summary>
		public static readonly DependencyProperty AutomaticallyEnterProperty;

		/// <summary>
		/// The IsSearching dependency property key
		/// </summary>
		private static readonly DependencyPropertyKey IsSearchingPropertyKey;

		/// <summary>
		/// The IsSearching dependency property
		/// </summary>
		public static readonly DependencyProperty IsSearchingProperty;

		/// <summary>
		/// The IsSearchOpen dependency property
		/// </summary>
		public static readonly DependencyProperty IsSearchOpenProperty;

		private void showSearchButton_Click(object sender, RoutedEventArgs e)
		{
			OnSearchButtonShown(new RoutedEventArgs(SearchButtonShownEvent));
		}

		private void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				OnSearchStarted(new RoutedEventArgs(SearchStartedEvent));
			else if (e.Key == Key.Escape)
				OnSearchCanceled(new RoutedEventArgs(SearchCanceledEvent));
		}

		private void textBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			OnSearchChanged(new RoutedEventArgs(SearchChangedEvent));
		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			OnSearchCanceled(new RoutedEventArgs(SearchCanceledEvent));
		}

		private void searchButton_Click(object sender, RoutedEventArgs e)
		{
			OnSearchStarted(new RoutedEventArgs(SearchStartedEvent));
		}

		static SearchBox()
		{
			// Override style
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox),
				new FrameworkPropertyMetadata(typeof(SearchBox)));

			// Register the AutomticallyEnter dependency property
			AutomaticallyEnterProperty = DependencyProperty.Register("AutomaticallyEnter",
				typeof(bool), typeof(SearchBox), new PropertyMetadata(true));

			// Register the IsSearching dependency property
			IsSearchingPropertyKey = DependencyProperty.RegisterReadOnly("IsSearching",
				typeof(bool), typeof(SearchBox), new PropertyMetadata());
			IsSearchingProperty = IsSearchingPropertyKey.DependencyProperty;

			// Register the IsSearchOpen dependency property
			IsSearchOpenProperty = DependencyProperty.Register("IsSearchOpen",
				typeof(bool), typeof(SearchBox));

			// Register the SearchStarted event
			SearchStartedEvent = EventManager.RegisterRoutedEvent("SearchStarted",
				RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchBox));

			// Register the SearchChanged event
			SearchChangedEvent = EventManager.RegisterRoutedEvent("SearchChanged",
				RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchBox));

			// Register the SearchCanceled event
			SearchCanceledEvent = EventManager.RegisterRoutedEvent("SearchCanceled",
				RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchBox));

			// Register the SearchCompleted event
			SearchCompletedEvent = EventManager.RegisterRoutedEvent("SearchCompleted",
				RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchBox));

			// Register the SearchButtonShown event
			SearchButtonShownEvent = EventManager.RegisterRoutedEvent("SearchButtonShown",
				RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchBox));
		}

		/// <summary>
		/// The SearchStarted event handler
		/// </summary>
		public event RoutedEventHandler SearchStarted
		{
			add { AddHandler(SearchStartedEvent, value); }
			remove { RemoveHandler(SearchStartedEvent, value); }
		}

		/// <summary>
		/// The SearchChanged event handler
		/// </summary>
		public event RoutedEventHandler SearchChanged
		{
			add { AddHandler(SearchChangedEvent, value); }
			remove { RemoveHandler(SearchChangedEvent, value); }
		}

		/// <summary>
		/// The SearchCanceled event handler
		/// </summary>
		public event RoutedEventHandler SearchCanceled
		{
			add { AddHandler(SearchCanceledEvent, value); }
			remove { RemoveHandler(SearchCanceledEvent, value); }
		}

		/// <summary>
		/// The SearchCompleted event handler
		/// </summary>
		public event RoutedEventHandler SearchCompleted
		{
			add { AddHandler(SearchCompletedEvent, value); }
			remove { RemoveHandler(SearchCompletedEvent, value); }
		}

		public event RoutedEventHandler SearchButtonShown
		{
			add { AddHandler(SearchButtonShownEvent, value); }
			remove { RemoveHandler(SearchButtonShownEvent, value); }
		}

		/// <summary>
		/// The SearchStarted event
		/// </summary>
		public static readonly RoutedEvent SearchStartedEvent;

		/// <summary>
		/// The SearchChanged event
		/// </summary>
		public static readonly RoutedEvent SearchChangedEvent;

		/// <summary>
		/// The SearchCanceled event
		/// </summary>
		public static readonly RoutedEvent SearchCanceledEvent;

		/// <summary>
		/// The SearchCompleted event
		/// </summary>
		public static readonly RoutedEvent SearchCompletedEvent;

		/// <summary>
		/// The SearchButtonShown event
		/// </summary>
		public static readonly RoutedEvent SearchButtonShownEvent;

		/// <summary>
		/// Occurs when the search is changed
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSearchStarted(RoutedEventArgs e)
		{
			SetValue(IsSearchingPropertyKey, true);
			RaiseEvent(e);

			OnSearchCompleted(new RoutedEventArgs(SearchCompletedEvent));
		}

		/// <summary>
		/// Occurs when the search is changed
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSearchChanged(RoutedEventArgs e)
		{
			if (AutomaticallyEnter & !IsSearching)
				OnSearchStarted(new RoutedEventArgs(SearchStartedEvent));
			else if (IsSearching)
				RaiseEvent(e);

			OnSearchCompleted(new RoutedEventArgs(SearchCompletedEvent));
		}

		/// <summary>
		/// Occurs when the search is canceled
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSearchCanceled(RoutedEventArgs e)
		{
			SetValue(IsSearchingPropertyKey, false);
			RaiseEvent(e);
			IsSearchOpen = false;
		}

		/// <summary>
		/// Occurs when the search is completed
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSearchCompleted(RoutedEventArgs e)
		{
			SetValue(IsSearchingPropertyKey, false);
			RaiseEvent(e);
		}

		/// <summary>
		/// Occurs when the search button is shown
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSearchButtonShown(RoutedEventArgs e)
		{
			IsSearchOpen = true;
			RaiseEvent(e);
		}

	}


}
