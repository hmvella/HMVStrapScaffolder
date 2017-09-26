using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Mvc.VisualStudio;
using Microsoft.AspNet.Scaffolding.NuGet;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	[Export]
	public class ODataFrameworkDependency : WebApiFrameworkDependency
	{
		[ImportingConstructor]
		public ODataFrameworkDependency(INuGetRepository repository, IVisualStudioIntegration visualStudioIntegration) : base(repository, visualStudioIntegration)
		{
		}

		public override FrameworkDependencyStatus EnsureDependencyInstalled(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return (new ODataDependencyInstaller(context, base.VisualStudioIntegration)).Install();
		}

		public override IEnumerable<NuGetPackage> GetRequiredPackages(CodeGenerationContext context)
		{
			return base.GetRequiredPackages(context).Concat<NuGetPackage>(
				from id in NuGetPackages.ODataPackageSet
				select this.Repository.GetPackage(context, id));
		}

		public override bool IsDependencyInstalled(CodeGenerationContext context)
		{
			bool flag = ProjectReferences.IsAssemblyReferenced(context.ActiveProject, AssemblyVersions.ODataAssemblyName);
			context.Items.AddProperty("MVC_IsODataAssemblyReferenced", flag);
			if (base.IsDependencyInstalled(context))
			{
				return flag;
			}
			return false;
		}

		public bool IsODataLegacy(CodeGenerationContext context)
		{
			Version version;
			string packageVersion = base.Repository.GetPackageVersion(context, NuGetPackages.ODataNuGetPackageId);
			SemanticVersionParser.TryParse(packageVersion, out version);
			return version < new Version(5, 2, 0);
		}

		public override bool IsSupported(CodeGenerationContext context)
		{
			if (!base.IsSupported(context))
			{
				return false;
			}
			return ScaffolderFilter.DisplayODataScaffolders(context);
		}
	}
}