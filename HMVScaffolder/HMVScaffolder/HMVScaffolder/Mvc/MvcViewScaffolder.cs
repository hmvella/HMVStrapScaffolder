using EnvDTE;
using HMVScaffolder.Mvc.Telemetry;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using Microsoft.AspNet.Scaffolding.NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HMVScaffolder.Mvc
{
    public class MvcViewScaffolder : InteractiveScaffolder<MvcViewScaffolderModel, MvcFrameworkDependency>
	{
		private const string ViewWithModelFolder = "MvcView";

		private const string ViewWithoutModelFolder = "MvcViewWithoutModel";

		public override IEnumerable<string> TemplateFolders
		{
			get
			{
				return this.TemplateFoldersInternal ?? base.TemplateFolders;
			}
		}

		private IEnumerable<string> TemplateFoldersInternal
		{
			get;
			set;
		}

		public MvcViewScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

		protected internal override void AddRuntimePackages(List<NuGetPackage> packages)
		{
			object obj = null;
			base.AddRuntimePackages(packages);
			if (base.Model.IsReferenceScriptLibrariesSelected && !AddDependencyUtil.IsBundleConfigPresent(base.Context))
			{
				string[] jQueryPackageSet = NuGetPackages.JQueryPackageSet;
				for (int i = 0; i < (int)jQueryPackageSet.Length; i++)
				{
					string str = jQueryPackageSet[i];
					if (!packages.Any<NuGetPackage>((NuGetPackage p) => string.Equals(p.PackageId, str, StringComparison.Ordinal)))
					{
						packages.Add(base.Repository.GetPackage(base.Context, str));
					}
				}
			}
			if (base.Context.Items.ContainsProperty("MVC_IsLayoutPageCreated"))
			{
				base.Context.Items.TryGetProperty<object>("MVC_IsLayoutPageCreated", out obj);
				if ((bool)obj)
				{
					string[] layoutPageDependencyPackageSet = NuGetPackages.LayoutPageDependencyPackageSet;
					for (int j = 0; j < (int)layoutPageDependencyPackageSet.Length; j++)
					{
						string str1 = layoutPageDependencyPackageSet[j];
						if (!packages.Any<NuGetPackage>((NuGetPackage p) => string.Equals(p.PackageId, str1, StringComparison.Ordinal)))
						{
							packages.Add(base.Repository.GetPackage(base.Context, str1));
						}
					}
				}
			}
		}

		protected internal override void AddScaffoldDependencies(List<NuGetPackage> packages)
		{
			if (base.Model.DataContextType != null)
			{
				IEntityFrameworkService service = base.Context.ServiceProvider.GetService<IEntityFrameworkService>();
				packages.AddRange(service.Dependencies);
			}
		}

		private void AddTemplates(HashSet<ViewTemplate> templates, string directory, bool isModelRequired)
		{
			if (!Directory.Exists(directory))
			{
				return;
			}
			string str = string.Concat(".", ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject).CodeFileExtension, ".t4");
			foreach (string str1 in Directory.EnumerateFiles(directory, string.Concat("*", str)))
			{
				string fileName = Path.GetFileName(str1);
				string str2 = fileName.Substring(0, fileName.LastIndexOf(str, StringComparison.OrdinalIgnoreCase));
				templates.Add(new ViewTemplate(str2, isModelRequired));
			}
		}

		private IDictionary<string, object> AddViewTemplateParameters(ModelMetadata modelMetadata, CodeGenerationContext context)
		{
			if (modelMetadata == null)
			{
				throw new ArgumentNullException("modelMetadata");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (string.IsNullOrEmpty(base.Model.ViewName))
			{
				throw new InvalidOperationException("The view name is invalid.");
			}
			IDictionary<string, object> strs = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
			{
				{ "ModelMetadata", modelMetadata },
				{ "ViewName", base.Model.ViewName },
				{ "LayoutPageFile", base.Model.LayoutPageFile ?? string.Empty },
				{ "IsPartialView", base.Model.IsPartialViewSelected },
				{ "IsLayoutPageSelected", base.Model.IsLayoutPageSelected },
				{ "ReferenceScriptLibraries", base.Model.IsReferenceScriptLibrariesSelected },
				{ "IsBundleConfigPresent", AddDependencyUtil.IsBundleConfigPresent(context) },
				{ "JQueryVersion", base.Repository.GetPackageVersion(base.Context, NuGetPackages.JQueryNuGetPackageId) }
			};
			Version assemblyVersion = ProjectReferences.GetAssemblyVersion(base.Context.ActiveProject, AssemblyVersions.MvcAssemblyName);
			strs.Add("MvcVersion", assemblyVersion);
			if (base.Model.ModelType != null)
			{
				CodeType codeType = base.Model.ModelType.CodeType;
				strs.Add("ViewDataTypeName", codeType.FullName);
				strs.Add("ViewDataTypeShortName", codeType.Name);
				strs.Add("ViewDataType", codeType);
			}
			else
			{
				strs.Add("ViewDataTypeName", string.Empty);
			}
			return strs;
		}

        protected override ValidatingDialogWindow CreateDialog()
        {
            return new ViewScaffolderDialog();
        }

        protected override MvcViewScaffolderModel CreateModel()
		{
			MvcViewScaffolderModel mvcViewScaffolderModel = new MvcViewScaffolderModel(base.Context);
			HashSet<ViewTemplate> viewTemplates = new HashSet<ViewTemplate>();
			string[] searchFolders = this.GetSearchFolders(true);
			for (int i = 0; i < (int)searchFolders.Length; i++)
			{
				this.AddTemplates(viewTemplates, searchFolders[i], true);
			}
			string[] strArrays = this.GetSearchFolders(false);
			for (int j = 0; j < (int)strArrays.Length; j++)
			{
				this.AddTemplates(viewTemplates, strArrays[j], false);
			}
			mvcViewScaffolderModel.ViewTemplates = viewTemplates.ToArray<ViewTemplate>();
			return mvcViewScaffolderModel;
		}

        protected override object CreateViewModel(MvcViewScaffolderModel model)
        {
            return new MvcViewScaffolderViewModel(model);
        }

        private void EnsureLayoutPageAndDependenciesCreated()
		{
			if (base.Model.IsLayoutPageSelected && string.IsNullOrEmpty(base.Model.LayoutPageFile))
			{
				bool flag = AddDependencyUtil.IsBundleConfigPresent(base.Context);
				MvcFullDependencyInstaller mvcFullDependencyInstaller = new MvcFullDependencyInstaller(base.Context, base.VisualStudioIntegration, base.Repository);
				mvcFullDependencyInstaller.EnsureLayoutPageAndDependenciesCreated(base.Model.AreaName, base.Model.AreaRelativePath, flag);
			}
		}

		protected void GenerateView(ModelMetadata modelMetadata, string outputFolder)
		{
			if (modelMetadata == null)
			{
				throw new ArgumentNullException("modelMetadata");
			}
			if (outputFolder == null)
			{
				throw new ArgumentNullException("outputFolder");
			}
			this.EnsureLayoutPageAndDependenciesCreated();
			if (outputFolder.Length > 0)
			{
				base.AddFolder(base.Context.ActiveProject, outputFolder);
			}
			IDictionary<string, object> strs = this.AddViewTemplateParameters(modelMetadata, base.Context);
			try
			{
				this.TemplateFoldersInternal = 
					from d in this.GetSearchFolders(base.Model.ViewTemplate.IsModelRequired)
					where Directory.Exists(d)
					select d;
				base.AddFileFromTemplate(base.Context.ActiveProject, Path.Combine(outputFolder, base.Model.ViewName), base.Model.ViewTemplate.Name, strs, !base.Model.IsOverwritingFiles);
			}
			finally
			{
				this.TemplateFoldersInternal = null;
			}
		}

		public void GenerateViewForController(ControllerScaffolderModel controllerModel, ModelMetadata modelMetadata, string viewName)
		{
			if (controllerModel == null)
			{
				throw new ArgumentNullException("controllerModel");
			}
			if (modelMetadata == null)
			{
				throw new ArgumentNullException("modelMetadata");
			}
			if (string.IsNullOrEmpty(viewName))
			{
				throw new ArgumentException("Template name is invalid.", "viewName");
			}
			base.Model.ViewName = viewName;
			if (!string.Equals(viewName, MvcViewTemplates.Index, StringComparison.Ordinal))
			{
				base.Model.ViewTemplate = new ViewTemplate(viewName, true);
			}
			else
			{
				base.Model.ViewTemplate = new ViewTemplate(MvcViewTemplates.List, true);
			}
			base.Model.IsPartialViewSelected = false;
			base.Model.IsOverwritingFiles = true;
			base.Model.IsReferenceScriptLibrariesSelected = controllerModel.IsReferenceScriptLibrariesSelected;
			base.Model.IsLayoutPageSelected = controllerModel.IsLayoutPageSelected;
			base.Model.LayoutPageFile = controllerModel.LayoutPageFile;
			base.Model.DataContextType = controllerModel.DataContextType;
			base.Model.ModelType = controllerModel.ModelType;
			string str = Path.Combine(controllerModel.AreaRelativePath, "Views", controllerModel.ControllerRootName);
			this.GenerateView(modelMetadata, str);
		}

		private string[] GetSearchFolders(bool isModelRequired)
		{
			if (isModelRequired)
			{
				return base.GetSearchFolders("MvcView");
			}
			return base.GetSearchFolders("MvcViewWithoutModel");
		}

		private MvcViewScaffolderOptions GetTelemetryOptions()
		{
			MvcViewScaffolderOptions mvcViewScaffolderOption = MvcViewScaffolderOptions.CreatedView;
			if (!string.IsNullOrWhiteSpace(base.Model.LayoutPageFile))
			{
				mvcViewScaffolderOption |= MvcViewScaffolderOptions.IsLayoutPageSpecified;
			}
			if (base.Model.IsLayoutPageSelected)
			{
				mvcViewScaffolderOption |= MvcViewScaffolderOptions.IsUseLayoutPageSelected;
			}
			if (base.Model.IsPartialViewSelected)
			{
				mvcViewScaffolderOption |= MvcViewScaffolderOptions.IsPartialView;
			}
			if (base.Model.IsReferenceScriptLibrariesSelected)
			{
				mvcViewScaffolderOption |= MvcViewScaffolderOptions.IsReferenceScriptLibrariesSelected;
			}
			if (base.Model.ViewTemplate != null && base.Model.ViewTemplate.IsModelRequired)
			{
				mvcViewScaffolderOption |= MvcViewScaffolderOptions.IsStronglyTypedView;
			}
			if (base.Model.DataContextType != null)
			{
				mvcViewScaffolderOption |= MvcViewScaffolderOptions.IsUsingDataContext;
			}
			return mvcViewScaffolderOption;
		}

		protected internal override void Scaffold()
		{
			ModelMetadata codeModelModelMetadatum;
			if (!base.Model.ViewTemplate.IsModelRequired)
			{
				codeModelModelMetadatum = new CodeModelModelMetadata();
				base.Model.IsReferenceScriptLibrariesSelected = false;
			}
			else if (base.Model.DataContextType == null)
			{
				codeModelModelMetadatum = (base.Model.ModelType == null ? new CodeModelModelMetadata() : new CodeModelModelMetadata(base.Model.ModelType.CodeType));
			}
			else
			{
				IEntityFrameworkService service = base.Context.ServiceProvider.GetService<IEntityFrameworkService>();
				codeModelModelMetadatum = service.AddRequiredEntity(base.Context, base.Model.DataContextType.TypeName, base.Model.ModelType.TypeName);
			}
			try
			{
				this.GenerateView(codeModelModelMetadatum, base.Model.SelectionRelativePath);
			}
			finally
			{
				base.Context.AddTelemetryData("MvcViewScaffolderOptions", (uint)this.GetTelemetryOptions());
				base.Context.AddTelemetryData("MvcViewTemplateName", base.Model.ViewTemplate.Name);
			}
		}
    }
}