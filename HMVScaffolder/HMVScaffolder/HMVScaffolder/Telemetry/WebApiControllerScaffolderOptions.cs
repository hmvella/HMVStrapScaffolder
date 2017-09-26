using System;

namespace Microsoft.AspNet.Scaffolding.Mvc.Telemetry
{
	[Flags]
	internal enum WebApiControllerScaffolderOptions : uint
	{
		None,
		CreatedController,
		IsAsyncSelected
	}
}