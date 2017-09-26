using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Mvc.ReadMe;
using Microsoft.AspNet.Scaffolding.Mvc.VisualStudio;
using Microsoft.VisualStudio.Utilities;
using System;
using System.IO;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	public class ApiDependencyInstaller : DependencyInstaller
	{
		private const string WebApiAppStartNamespace = "System.Web.Http";

		protected override string[] SearchFolders
		{
			get
			{
				string[] strArrays = new string[] { Path.Combine(TemplateSearchDirectories.InstalledTemplateRoot, "ApiDependencyCodeGenerator") };
				return strArrays;
			}
		}

		public ApiDependencyInstaller(CodeGenerationContext context, IVisualStudioIntegration visualStudioIntegration) : base(context, visualStudioIntegration)
		{
		}

		protected override void CreateStaticFilesAndFolders()
		{
			base.CreateStaticFilesAndFolders();
			if (!base.Context.Items.ContainsProperty("MVC_IsControllersFolderCreated") && !Directory.Exists(Path.Combine(ProjectExtensions.GetFullPath(base.Context.ActiveProject), "Controllers")))
			{
				base.Context.Items.AddProperty("MVC_IsControllersFolderCreated", true);
			}
			base.ActionsService.AddFolder(base.Context.ActiveProject, "Controllers");
		}

		protected override FrameworkDependencyStatus GenerateConfiguration()
		{
			if (base.TryCreateGlobalAsax())
			{
				return FrameworkDependencyStatus.InstallSuccessful;
			}
			WebApiReadMe webApiReadMe = new WebApiReadMe(ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject), base.Context.ActiveProject.Name, base.AppStartFileNames);
			return FrameworkDependencyStatus.FromReadme(webApiReadMe.CreateReadMeText());
		}

		protected override void GenerateFiles()
		{
			base.CreateAppStartFiles("WebApiConfig", "System.Web.Http");
		}
	}
}