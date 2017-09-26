using EnvDTE;
using System;
using System.ComponentModel;
using System.Windows;

namespace HMVScaffolder.Mvc
{
	public interface IDialogHost
	{
		MessageBoxResult RequestConfirmation(string message, string caption);

		void ShowErrorMessage(string message, string caption);

		bool TrySelectFile(Project project, string title, string filter, string storageKey, out string file);

		event EventHandler<CancelEventArgs> Closing;
	}
}