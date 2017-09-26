using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using HMVScaffolder.Mvc.VisualStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	public abstract class DependencyInstaller
	{
		private const string GlobalAsaxClassName = "Global";

		private const string GlobalAsaxMvcClassName = "MvcApplication";

		private const string GlobalAsaxWebApiClassName = "WebApiApplication";

		private const string JQueryBundleSearchText = "ScriptBundle(\"~/bundles/jquery\")";

		protected ICodeGeneratorActionsService ActionsService
		{
			get;
			private set;
		}

		protected Dictionary<string, string> AppStartFileNames
		{
			get;
			private set;
		}

		protected ICodeTypeService CodeTypeService
		{
			get
			{
				return this.Context.ServiceProvider.GetService<ICodeTypeService>();
			}
		}

		protected CodeGenerationContext Context
		{
			get;
			private set;
		}

		protected ICodeGeneratorFilesLocator FilesLocatorService
		{
			get;
			private set;
		}

		protected abstract string[] SearchFolders
		{
			get;
		}

		protected IVisualStudioIntegration VisualStudioIntegration
		{
			get;
			private set;
		}

		protected DependencyInstaller(CodeGenerationContext context, IVisualStudioIntegration visualStudioIntegration)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (visualStudioIntegration == null)
			{
				throw new ArgumentNullException("visualStudioIntegration");
			}
			this.Context = context;
			this.VisualStudioIntegration = visualStudioIntegration;
			this.ActionsService = context.ServiceProvider.GetService<ICodeGeneratorActionsService>();
			this.FilesLocatorService = context.ServiceProvider.GetService<ICodeGeneratorFilesLocator>();
			this.AppStartFileNames = new Dictionary<string, string>();
		}

		protected void CreateAppStartFiles(string configFileName, string productNamespace)
		{
			string str = configFileName;
			int num = 2;
			while (true)
			{
				Project activeProject = this.Context.ActiveProject;
				CodeType codeType = this.CodeTypeService.GetCodeType(activeProject, string.Concat(ProjectExtensions.GetDefaultNamespace(activeProject), ".", configFileName));
				string str1 = string.Concat(configFileName, ".", ProjectExtensions.GetCodeLanguage(this.Context.ActiveProject).CodeFileExtension);
				string str2 = Path.Combine(ProjectExtensions.GetFullPath(this.Context.ActiveProject), "App_Start", str1);
				if (!File.Exists(Path.Combine(ProjectExtensions.GetFullPath(this.Context.ActiveProject), "App_Start", str1)) && codeType == null)
				{
					this.AppStartFileNames.Add(str, configFileName);
					this.GenerateT4File(str, configFileName, "App_Start");
					return;
				}
				if (codeType != null && !codeType.Name.StartsWith("BundleConfig", StringComparison.OrdinalIgnoreCase) && CodeTypeFilter.IsProductNamespaceImported(codeType, productNamespace))
				{
					this.AppStartFileNames.Add(str, configFileName);
					return;
				}
				if (codeType != null && codeType.Name.StartsWith("BundleConfig", StringComparison.OrdinalIgnoreCase) && AddDependencyUtil.IsSearchTextPresent(str2, "ScriptBundle(\"~/bundles/jquery\")"))
				{
					break;
				}
				configFileName = string.Concat(str, num);
				num++;
			}
			this.AppStartFileNames.Add(str, configFileName);
		}

		protected virtual void CreateStaticFilesAndFolders()
		{
			this.ActionsService.CreateAppDataFolder(this.Context.ActiveProject);
			this.ActionsService.AddFolder(this.Context.ActiveProject, "App_Start");
		}

		protected virtual FrameworkDependencyStatus GenerateConfiguration()
		{
			return FrameworkDependencyStatus.InstallSuccessful;
		}

		protected virtual void GenerateFiles()
		{
		}

		private void GenerateT4File(string templateName, string outputFileName, string path)
		{
			IDictionary<string, object> strs = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
			{
				{ "Namespace", ProjectExtensions.GetDefaultNamespace(this.Context.ActiveProject) }
			};
			foreach (KeyValuePair<string, string> appStartFileName in this.AppStartFileNames)
			{
				strs.Add(appStartFileName.Key, appStartFileName.Value);
			}
			string codeFileExtension = ProjectExtensions.GetCodeLanguage(this.Context.ActiveProject).CodeFileExtension;
			string str = Path.Combine(path, outputFileName);
			string textTemplatePath = this.FilesLocatorService.GetTextTemplatePath(templateName, this.SearchFolders, codeFileExtension);
			this.ActionsService.AddFileFromTemplate(this.Context.ActiveProject, str, textTemplatePath, strs, true);
		}

		public FrameworkDependencyStatus Install()
		{
			this.CreateStaticFilesAndFolders();
			this.GenerateFiles();
			return this.GenerateConfiguration();
		}

		private bool IsGlobalAsaxPresent()
		{
			if ((this.CodeTypeService.GetCodeType(this.Context.ActiveProject, "Global") != null || this.CodeTypeService.GetCodeType(this.Context.ActiveProject, "MvcApplication") != null ? true : this.CodeTypeService.GetCodeType(this.Context.ActiveProject, "WebApiApplication") != null))
			{
				return true;
			}
			return File.Exists(Path.Combine(ProjectExtensions.GetFullPath(this.Context.ActiveProject), string.Empty, "Global.asax"));
		}

		protected bool TryCreateGlobalAsax()
		{
			if (this.IsGlobalAsaxPresent())
			{
				return false;
			}
			this.GenerateT4File("Global", "Global", string.Empty);
			this.GenerateT4File("Global.asax", "Global.asax", string.Empty);
			return true;
		}
	}
}