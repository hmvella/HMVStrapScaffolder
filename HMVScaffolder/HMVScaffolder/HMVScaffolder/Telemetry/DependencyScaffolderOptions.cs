using System;

namespace Microsoft.AspNet.Scaffolding.Mvc.Telemetry
{
	internal enum DependencyScaffolderOptions : uint
	{
		None,
		AlreadyInstalled,
		MvcMinimal,
		MvcFull,
		WebApi
	}
}