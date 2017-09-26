using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace HMVScaffolder.Mvc
{
    public static class SelectOnFocusBehavior
	{
		public readonly static DependencyProperty SelectOnFocusProperty;

		static SelectOnFocusBehavior()
		{
            SelectOnFocusProperty = DependencyProperty.RegisterAttached("SelectOnFocus", typeof(string), typeof(SelectOnFocusBehavior), new UIPropertyMetadata(null, new PropertyChangedCallback(SelectOnFocusBehavior.OnSelectOnFocusPropertyChanged)));
		}

		public static string GetSelectOnFocus(DependencyObject obj)
		{
			return (string)obj.GetValue(SelectOnFocusProperty);
		}

		private static void OnSelectOnFocusPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (textBox != null && e.NewValue is string)
			{
				textBox.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(OnTextBoxGotKeyboardFocus);
				textBox.TextChanged += new TextChangedEventHandler(OnTextBoxGotKeyboardFocus);
			}
		}

		private static void OnTextBoxGotKeyboardFocus(object sender, RoutedEventArgs e)
		{
			int index;
			int length;
			if (!ReferenceEquals(sender, e.OriginalSource))
			{
				return;
			}
			TextBox originalSource = e.OriginalSource as TextBox;
			if (originalSource != null)
			{
				string text = originalSource.Text;
				string selectOnFocus = GetSelectOnFocus(originalSource);
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				if (!string.IsNullOrEmpty(selectOnFocus))
				{
					Match match = Regex.Match(text, selectOnFocus);
					if (match.Success)
					{
						if (match.Groups.Count <= 1 || !match.Groups[1].Success)
						{
							index = match.Index;
							length = match.Length;
						}
						else
						{
							Group item = match.Groups[1];
							index = item.Index;
							length = item.Length;
						}
						Action action = () => originalSource.Select(index, length);
						originalSource.Dispatcher.BeginInvoke(action, DispatcherPriority.ContextIdle, new object[0]);
					}
				}
				else
				{
					Action action1 = () => originalSource.SelectAll();
					originalSource.Dispatcher.BeginInvoke(action1, DispatcherPriority.ContextIdle, new object[0]);
				}
				originalSource.TextChanged -= new TextChangedEventHandler(SelectOnFocusBehavior.OnTextBoxGotKeyboardFocus);
			}
		}

		public static void SetSelectOnFocus(DependencyObject obj, string value)
		{
			obj.SetValue(SelectOnFocusProperty, value);
		}
	}
}