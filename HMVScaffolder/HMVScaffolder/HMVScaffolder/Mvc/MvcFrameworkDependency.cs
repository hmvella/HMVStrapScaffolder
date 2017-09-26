using Microsoft.AspNet.Scaffolding;
using HMVScaffolder.Mvc.Configuration;
using HMVScaffolder.Mvc.Telemetry;
using HMVScaffolder.Mvc.VisualStudio;
using Microsoft.AspNet.Scaffolding.NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	[Export]
	public class MvcFrameworkDependency : IFrameworkDependency
	{
		private INuGetRepository Repository
		{
			get;
			set;
		}

		private IVisualStudioIntegration VisualStudioIntegration
		{
			get;
			set;
		}

		[ImportingConstructor]
		public MvcFrameworkDependency(INuGetRepository repository, IVisualStudioIntegration visualStudioIntegration)
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

		public FrameworkDependencyStatus EnsureDependencyInstalled(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.AddTelemetryData("DependencyScaffolderOptions", (uint)3);
			MvcFullDependencyInstaller mvcFullDependencyInstaller = new MvcFullDependencyInstaller(context, this.VisualStudioIntegration, this.Repository);
			return mvcFullDependencyInstaller.Install();
		}

		public IEnumerable<NuGetPackage> GetRequiredPackages(CodeGenerationContext context)
		{
			return 
				from id in NuGetPackages.MvcFullPackageSet
				select this.Repository.GetPackage(context, id);
		}

		public bool IsDependencyInstalled(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return ProjectReferences.IsAssemblyReferenced(context.ActiveProject, AssemblyVersions.MvcAssemblyName);
		}

		public bool IsSupported(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return ScaffolderFilter.DisplayMvcScaffolders(context);
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
			MvcControllerScaffolderOptions mvcControllerScaffolderOption = MvcControllerScaffolderOptions.CreatedController;
			if (model.IsAsyncSelected)
			{
				mvcControllerScaffolderOption |= MvcControllerScaffolderOptions.IsAsyncSelected;
			}
			if (model.IsViewGenerationSelected)
			{
				mvcControllerScaffolderOption |= MvcControllerScaffolderOptions.CreatedViews;
				if (model.IsReferenceScriptLibrariesSelected)
				{
					mvcControllerScaffolderOption |= MvcControllerScaffolderOptions.IsReferenceScriptLibrariesSelected;
				}
				if (model.IsLayoutPageSelected)
				{
					mvcControllerScaffolderOption |= MvcControllerScaffolderOptions.IsUseLayoutPageSelected;
					if (!string.IsNullOrWhiteSpace(model.LayoutPageFile))
					{
						mvcControllerScaffolderOption |= MvcControllerScaffolderOptions.IsLayoutPageSpecified;
					}
				}
			}
			context.AddTelemetryData("MvcControllerScaffolderOptions", (uint)mvcControllerScaffolderOption);
		}

		public void UpdateConfiguration(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			(new MvcConfigurationEditor(this.VisualStudioIntegration)).Edit(context.ActiveProject);
		}
	}
}