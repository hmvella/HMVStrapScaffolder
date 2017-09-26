using System;
using System.Windows;

namespace HMVScaffolder.Mvc
{
	public static class WindowAutoSizeBehavior
	{
		public readonly static DependencyProperty IsEnabledProperty;

		static WindowAutoSizeBehavior()
		{
			WindowAutoSizeBehavior.IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(WindowAutoSizeBehavior), new PropertyMetadata(new PropertyChangedCallback(WindowAutoSizeBehavior.OnIsEnabledChanged)));
		}

		public static ResizeMode GetIsEnabled(DependencyObject dependencyObject)
		{
			return (ResizeMode)dependencyObject.GetValue(WindowAutoSizeBehavior.IsEnabledProperty);
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
				window.ContentRendered -= new EventHandler(WindowAutoSizeBehavior.Window_ContentRendered);
			}
			else if (!window.IsLoaded)
			{
				window.ContentRendered += new EventHandler(WindowAutoSizeBehavior.Window_ContentRendered);
				return;
			}
		}

		public static void SetIsEnabled(DependencyObject dependencyObject, bool value)
		{
			dependencyObject.SetValue(WindowAutoSizeBehavior.IsEnabledProperty, value);
		}

		private static void Window_ContentRendered(object sender, EventArgs e)
		{
			Window actualHeight = sender as Window;
			if (actualHeight == null)
			{
				return;
			}
			actualHeight.ContentRendered -= new EventHandler(WindowAutoSizeBehavior.Window_ContentRendered);
			actualHeight.Height = actualHeight.ActualHeight;
			if (actualHeight.SizeToContent.HasFlag(SizeToContent.Height) && DependencyPropertyHelper.GetValueSource(actualHeight, FrameworkElement.MinHeightProperty).BaseValueSource != BaseValueSource.Local)
			{
				actualHeight.MinHeight = actualHeight.ActualHeight;
			}
			if (actualHeight.SizeToContent.HasFlag(SizeToContent.Height) && DependencyPropertyHelper.GetValueSource(actualHeight, FrameworkElement.MaxHeightProperty).BaseValueSource != BaseValueSource.Local)
			{
				actualHeight.MaxHeight = actualHeight.ActualHeight;
			}
			actualHeight.Width = actualHeight.ActualWidth;
			if (actualHeight.SizeToContent.HasFlag(SizeToContent.Width) && DependencyPropertyHelper.GetValueSource(actualHeight, FrameworkElement.MinWidthProperty).BaseValueSource != BaseValueSource.Local)
			{
				actualHeight.MinWidth = actualHeight.ActualWidth;
			}
			if (actualHeight.SizeToContent.HasFlag(SizeToContent.Width) && DependencyPropertyHelper.GetValueSource(actualHeight, FrameworkElement.MaxWidthProperty).BaseValueSource != BaseValueSource.Local)
			{
				actualHeight.MaxWidth = actualHeight.ActualHeight;
			}
			actualHeight.SizeToContent = SizeToContent.Manual;
		}
	}
}