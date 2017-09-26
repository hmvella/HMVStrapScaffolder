using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Mvc.ReadMe;
using Microsoft.AspNet.Scaffolding.Mvc.VisualStudio;
using System;
using System.IO;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	public class MvcMinimalDependencyInstaller : MvcDependencyInstaller
	{
		protected override string[] SearchFolders
		{
			get
			{
				string[] strArrays = new string[] { Path.Combine(TemplateSearchDirectories.InstalledTemplateRoot, "MvcMinimalDependencyCodeGenerator") };
				return strArrays;
			}
		}

		public MvcMinimalDependencyInstaller(CodeGenerationContext context, IVisualStudioIntegration visualStudioIntegration) : base(context, visualStudioIntegration)
		{
		}

		protected override FrameworkDependencyStatus GenerateConfiguration()
		{
			if (base.TryCreateGlobalAsax())
			{
				return FrameworkDependencyStatus.InstallSuccessful;
			}
			MvcMinimalDependencyReadMe mvcMinimalDependencyReadMe = new MvcMinimalDependencyReadMe(ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject), base.Context.ActiveProject.Name, base.AppStartFileNames);
			return FrameworkDependencyStatus.FromReadme(mvcMinimalDependencyReadMe.CreateReadMeText());
		}

		protected override void GenerateFiles()
		{
			base.GenerateFiles();
			base.GenerateWebConfig(false);
		}
	}
}