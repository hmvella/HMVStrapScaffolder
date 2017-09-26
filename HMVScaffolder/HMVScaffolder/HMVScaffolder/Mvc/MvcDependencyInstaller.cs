using Microsoft.AspNet.Scaffolding;
using HMVScaffolder.Mvc.VisualStudio;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace HMVScaffolder.Mvc
{
	public abstract class MvcDependencyInstaller : DependencyInstaller
	{
		protected const string MvcAppStartNamespace = "System.Web.Mvc";

		protected MvcDependencyInstaller(CodeGenerationContext context, IVisualStudioIntegration visualStudioIntegration) : base(context, visualStudioIntegration)
		{
		}

		protected override void CreateStaticFilesAndFolders()
		{
			base.CreateStaticFilesAndFolders();
			base.ActionsService.AddFolder(base.Context.ActiveProject, "Models");
			if (!base.Context.Items.ContainsProperty("MVC_IsControllersFolderCreated") && !Directory.Exists(Path.Combine(ProjectExtensions.GetFullPath(base.Context.ActiveProject), "Controllers")))
			{
				base.Context.Items.AddProperty("MVC_IsControllersFolderCreated", true);
			}
			base.ActionsService.AddFolder(base.Context.ActiveProject, "Controllers");
			base.ActionsService.AddFolder(base.Context.ActiveProject, "Views");
		}

		protected override void GenerateFiles()
		{
			base.CreateAppStartFiles("RouteConfig", "System.Web.Mvc");
		}

		protected void GenerateWebConfig(bool isBundleConfigPresent)
		{
			IDictionary<string, object> templateParametersForWebConfig = AddDependencyUtil.GetTemplateParametersForWebConfig(isBundleConfigPresent, base.Context);
			string codeFileExtension = ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject).CodeFileExtension;
			string str = Path.Combine("Views", "web");
			string textTemplatePath = base.FilesLocatorService.GetTextTemplatePath("web", this.SearchFolders, codeFileExtension);
			base.ActionsService.AddFileFromTemplate(base.Context.ActiveProject, str, textTemplatePath, templateParametersForWebConfig, true);
		}
	}
}