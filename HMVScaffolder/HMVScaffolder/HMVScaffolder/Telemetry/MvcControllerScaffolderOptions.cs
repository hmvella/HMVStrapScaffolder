using System;

namespace HMVScaffolder.Mvc.Telemetry
{
	[Flags]
	internal enum MvcControllerScaffolderOptions : uint
	{
		None = 0,
		CreatedController = 1,
		IsAsyncSelected = 2,
		CreatedViews = 4,
		IsReferenceScriptLibrariesSelected = 8,
		IsUseLayoutPageSelected = 16,
		IsLayoutPageSpecified = 32
	}
}