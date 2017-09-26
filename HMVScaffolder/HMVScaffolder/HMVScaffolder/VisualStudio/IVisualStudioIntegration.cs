using EnvDTE;
using System;

namespace HMVScaffolder.Mvc
{
	public interface IVisualStudioIntegration
	{
		IEditorIntegration Editor
		{
			get;
		}

		IServiceProvider ServiceProvider
		{
			get;
		}

		IProjectSettings GetProjectSettings(Project project);

		void ShowErrorMessage(string caption, string message);
	}
}