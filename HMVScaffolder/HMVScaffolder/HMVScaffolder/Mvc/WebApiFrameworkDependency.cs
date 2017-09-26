using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Mvc.Telemetry;
using Microsoft.AspNet.Scaffolding.Mvc.VisualStudio;
using Microsoft.AspNet.Scaffolding.NuGet;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	[Export]
	public class WebApiFrameworkDependency : IFrameworkDependency
	{
		protected INuGetRepository Repository
		{
			get;
			private set;
		}

		protected IVisualStudioIntegration VisualStudioIntegration
		{
			get;
			private set;
		}

		[ImportingConstructor]
		public WebApiFrameworkDependency(INuGetRepository repository, IVisualStudioIntegration visualStudioIntegration)
		{
			if (repository == null)
			{
				throw new ArgumentNullException("repository");
			}
			if (visualStudioIntegration == null)
			{
				throw new ArgumentNullException("visualStudioIntegration");
			}
			this.Repository = repository;
			this.VisualStudioIntegration = visualStudioIntegration;
		}

		public virtual FrameworkDependencyStatus EnsureDependencyInstalled(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.AddTelemetryData("DependencyScaffolderOptions", (uint)4);
			return (new ApiDependencyInstaller(context, this.VisualStudioIntegration)).Install();
		}

		public virtual IEnumerable<NuGetPackage> GetRequiredPackages(CodeGenerationContext context)
		{
			return 
				from id in NuGetPackages.WebApiPackageSet
				select this.Repository.GetPackage(context, id);
		}

		public virtual bool IsDependencyInstalled(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			bool flag = ProjectReferences.IsAssemblyReferenced(context.ActiveProject, AssemblyVersions.WebApiAssemblyName);
			context.Items.AddProperty("MVC_IsWebApiAssemblyReferenced", flag);
			return flag;
		}

		public virtual bool IsSupported(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return ScaffolderFilter.DisplayWebApiScaffolders(context);
		}

		public void RecordControllerTelemetryOptions(CodeGenerationContext context, ControllerScaffolderModel model)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			WebApiControllerScaffolderOptions webApiControllerScaffolderOption = WebApiControllerScaffolderOptions.CreatedController;
			if (model.IsAsyncSelected)
			{
				webApiControllerScaffolderOption |= WebApiControllerScaffolderOptions.IsAsyncSelected;
			}
			context.AddTelemetryData("WebApiControllerScaffolderOptions", (uint)webApiControllerScaffolderOption);
		}

		public virtual void UpdateConfiguration(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			Project activeProject = context.ActiveProject;
			string str = Path.Combine(ProjectExtensions.GetFullPath(activeProject), "web.config");
			this.VisualStudioIntegration.Editor.GetOrOpenDocument(str);
		}
	}
}