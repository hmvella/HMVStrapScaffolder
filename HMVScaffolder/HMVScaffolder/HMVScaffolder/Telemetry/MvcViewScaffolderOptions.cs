using System;

namespace HMVScaffolder.Mvc
{
	[Flags]
	internal enum MvcViewScaffolderOptions : uint
	{
		None = 0,
		CreatedView = 1,
		Unused = 2,
		IsStronglyTypedView = 4,
		IsUsingDataContext = 8,
		IsPartialView = 16,
		IsReferenceScriptLibrariesSelected = 32,
		IsUseLayoutPageSelected = 64,
		IsLayoutPageSpecified = 128
	}
}