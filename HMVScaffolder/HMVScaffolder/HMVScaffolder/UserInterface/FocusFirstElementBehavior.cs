using System;
using System.Windows;
using System.Windows.Input;

namespace HMVScaffolder.Mvc
{
	public static class FocusFirstElementBehavior
	{
		public readonly static DependencyProperty IsEnabledProperty;

		static FocusFirstElementBehavior()
		{
			IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(FocusFirstElementBehavior), new PropertyMetadata(new PropertyChangedCallback(FocusFirstElementBehavior.OnIsEnabledChanged)));
		}

		private static void Content_Rendered(object sender, EventArgs e)
		{
			Window window = sender as Window;
			if (window == null)
			{
				return;
			}
			window.ContentRendered -= new EventHandler(FocusFirstElementBehavior.Content_Rendered);
			window.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
		}

		public static ResizeMode GetIsEnabled(DependencyObject dependencyObject)
		{
			return (ResizeMode)dependencyObject.GetValue(IsEnabledProperty);
		}

		private static void OnIsEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			Window window = sender as Window;
			if (window == null)
			{
				return;
			}
			if (!(bool)e.NewValue)
			{
				window.ContentRendered -= new EventHandler(Content_Rendered);
			}
			else if (!window.IsLoaded)
			{
				window.ContentRendered += new EventHandler(Content_Rendered);
				return;
			}
		}

		public static void SetIsEnabled(DependencyObject dependencyObject, bool value)
		{
			dependencyObject.SetValue(IsEnabledProperty, value);
		}
	}
}