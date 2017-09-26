using HMVScaffolder.Mvc.VisualStudio;
using System;

namespace HMVScaffolder.Mvc
{
	public interface IScaffoldingSettings
	{
		void LoadSettings(IProjectSettings settings);

		void SaveSettings(IProjectSettings settings);
	}
}