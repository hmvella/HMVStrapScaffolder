using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.NuGet;
using System;
using System.Collections.Generic;

namespace HMVScaffolder.Mvc
{
	public interface IFrameworkDependency
	{
		FrameworkDependencyStatus EnsureDependencyInstalled(CodeGenerationContext context);

		IEnumerable<NuGetPackage> GetRequiredPackages(CodeGenerationContext context);

		bool IsDependencyInstalled(CodeGenerationContext context);

		bool IsSupported(CodeGenerationContext context);

		void RecordControllerTelemetryOptions(CodeGenerationContext context, ControllerScaffolderModel model);

		void UpdateConfiguration(CodeGenerationContext context);
	}
}