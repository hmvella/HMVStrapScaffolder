using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace HMVScaffolder.Mvc
{
	public interface IEditorIntegration
	{
		IVsWindowFrame CreateAndOpenReadme(string text);

		void FormatDocument(string filePath);

		IEditorInterfaces GetOrOpenDocument(string path);

		void OpenFileInEditor(string filePath);

		IDisposable SuppressChangeNotifications(string filePath);
	}
}