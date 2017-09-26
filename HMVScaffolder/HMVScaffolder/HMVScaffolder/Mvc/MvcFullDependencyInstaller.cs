using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using HMVScaffolder.Mvc.ReadMe;
using HMVScaffolder.Mvc.VisualStudio;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	public class MvcFullDependencyInstaller : MvcDependencyInstaller
	{
		private INuGetRepository Repository
		{
			get;
			set;
		}

		protected override string[] SearchFolders
		{
			get
			{
				string[] strArrays = new string[] { Path.Combine(TemplateSearchDirectories.InstalledTemplateRoot, "MvcFullDependencyCodeGenerator") };
				return strArrays;
			}
		}

		public MvcFullDependencyInstaller(CodeGenerationContext context, IVisualStudioIntegration visualStudioIntegration, INuGetRepository repository) : base(context, visualStudioIntegration)
		{
			this.Repository = repository;
		}

		protected override void CreateStaticFilesAndFolders()
		{
			base.CreateStaticFilesAndFolders();
			string viewFileExtension = MvcProjectUtil.GetViewFileExtension(ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject));
			string str = Path.Combine("Views", "Shared");
			base.ActionsService.AddFolder(base.Context.ActiveProject, str);
			string str1 = Path.ChangeExtension("Error", viewFileExtension);
			base.ActionsService.AddFile(base.Context.ActiveProject, Path.Combine(str, str1), base.FilesLocatorService.GetStaticFilePath(str1, this.SearchFolders), true);
		}

		public void EnsureLayoutPageAndDependenciesCreated(string areaName, string areaRelativePath, bool isBundleConfigPresent)
		{
			string str;
			string viewFileExtension = MvcProjectUtil.GetViewFileExtension(ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject));
			string str1 = Path.ChangeExtension("_ViewStart", viewFileExtension);
			str = (!string.IsNullOrEmpty(areaRelativePath) ? Path.Combine(areaRelativePath, "Views") : "Views");
			if (!File.Exists(Path.Combine(ProjectExtensions.GetFullPath(base.Context.ActiveProject), str, str1)))
			{
				string codeFileExtension = ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject).CodeFileExtension;
				string textTemplatePath = base.FilesLocatorService.GetTextTemplatePath("_ViewStart", this.SearchFolders, codeFileExtension);
				ICodeGeneratorActionsService actionsService = base.ActionsService;
				Project activeProject = base.Context.ActiveProject;
				string str2 = Path.Combine(str, "_ViewStart");
				Dictionary<string, object> strs = new Dictionary<string, object>()
				{
					{ "AreaName", areaName }
				};
				actionsService.AddFileFromTemplate(activeProject, str2, textTemplatePath, strs, true);
				string str3 = Path.ChangeExtension("_Layout", viewFileExtension);
				string str4 = Path.Combine(str, "Shared");
				if (!File.Exists(Path.Combine(ProjectExtensions.GetFullPath(base.Context.ActiveProject), str4, str3)))
				{
					textTemplatePath = base.FilesLocatorService.GetTextTemplatePath("_Layout", this.SearchFolders, codeFileExtension);
					ICodeGeneratorActionsService codeGeneratorActionsService = base.ActionsService;
					Project project = base.Context.ActiveProject;
					string str5 = Path.Combine(str4, "_Layout");
					Dictionary<string, object> strs1 = new Dictionary<string, object>()
					{
						{ "IsBundleConfigPresent", isBundleConfigPresent },
						{ "JQueryVersion", this.Repository.GetPackageVersion(base.Context, NuGetPackages.JQueryNuGetPackageId) },
						{ "ModernizrVersion", this.Repository.GetPackageVersion(base.Context, NuGetPackages.ModernizrNuGetPackageId) }
					};
					codeGeneratorActionsService.AddFileFromTemplate(project, str5, textTemplatePath, strs1, true);
					if (!base.Context.Items.ContainsProperty("MVC_IsLayoutPageCreated"))
					{
						base.Context.Items.AddProperty("MVC_IsLayoutPageCreated", true);
					}
					textTemplatePath = base.FilesLocatorService.GetTextTemplatePath("Site", this.SearchFolders, codeFileExtension);
					Version assemblyVersion = ProjectReferences.GetAssemblyVersion(base.Context.ActiveProject, AssemblyVersions.MvcAssemblyName);
					ICodeGeneratorActionsService actionsService1 = base.ActionsService;
					Project activeProject1 = base.Context.ActiveProject;
					string str6 = Path.Combine("Content", "Site");
					Dictionary<string, object> strs2 = new Dictionary<string, object>()
					{
						{ "MvcVersion", assemblyVersion }
					};
					actionsService1.AddFileFromTemplate(activeProject1, str6, textTemplatePath, strs2, true);
				}
			}
		}

		protected override FrameworkDependencyStatus GenerateConfiguration()
		{
			if (base.TryCreateGlobalAsax())
			{
				return FrameworkDependencyStatus.InstallSuccessful;
			}
			MvcFullDependencyReadMe mvcFullDependencyReadMe = new MvcFullDependencyReadMe(ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject), base.Context.ActiveProject.Name, base.AppStartFileNames);
			return FrameworkDependencyStatus.FromReadme(mvcFullDependencyReadMe.CreateReadMeText());
		}

		protected override void GenerateFiles()
		{
			base.GenerateFiles();
			base.CreateAppStartFiles("BundleConfig", "System.Web.Mvc");
			base.CreateAppStartFiles("FilterConfig", "System.Web.Mvc");
			this.EnsureLayoutPageAndDependenciesCreated(string.Empty, string.Empty, true);
			base.GenerateWebConfig(true);
		}
	}
}