using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HMVScaffolder.Mvc
{
	public class NotifyPropertyChanged : INotifyPropertyChanged
	{
		public NotifyPropertyChanged()
		{
		}

		protected bool OnPropertyChanged<T>(ref T propertyRef, T value, [CallerMemberName] string propertyName = null)
		{
			if (object.Equals(propertyRef, value))
			{
				return false;
			}
			propertyRef = value;
			this.OnPropertyChanged(propertyName);
			return true;
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
			if (propertyChangedEventHandler != null)
			{
				propertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}