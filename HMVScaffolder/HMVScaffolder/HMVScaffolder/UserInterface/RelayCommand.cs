using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;

namespace HMVScaffolder.Mvc
{
	public class RelayCommand : ICommand
	{
		private Predicate<object> CanExecuteDelegate
		{
			get;
			set;
		}

		private Action<object> ExecuteDelegate
		{
			get;
			set;
		}

		public RelayCommand(Action<object> execute) : this(execute, null)
		{
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this.ExecuteDelegate = execute;
			this.CanExecuteDelegate = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			if (this.CanExecuteDelegate == null)
			{
				return true;
			}
			return this.CanExecuteDelegate(parameter);
		}

		public void Execute(object parameter)
		{
			this.ExecuteDelegate(parameter);
		}

		public void SuggestRequery()
		{
			EventHandler eventHandler = this.CanExecuteChanged;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		public event EventHandler CanExecuteChanged;
	}
}