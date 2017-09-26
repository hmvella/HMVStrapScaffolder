using System;

namespace HMVScaffolder.Mvc
{
	public interface IDialogSettings
	{
		void LoadDialogSettings(IProjectSettings settings);

		void SaveDialogSettings(IProjectSettings settings);
	}
}