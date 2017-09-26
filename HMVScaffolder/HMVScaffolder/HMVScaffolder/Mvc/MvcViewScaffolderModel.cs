using EnvDTE;
using HMVScaffolder.Mvc.VisualStudio;
using Microsoft.AspNet.Scaffolding;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HMVScaffolder.Mvc
{
    public class MvcViewScaffolderModel : ScaffolderModel, IScaffoldingSettings
	{
		private const string DefaultViewNameKey = "DefaultViewName";

		private const string ControllerFolderNameKey = "ControllerFolderName";

		public string DataContextName
		{
			get;
			set;
		}

		public ModelType DataContextType
		{
			get;
			set;
		}

		public IEnumerable<ModelType> DataContextTypes
		{
			get;
			private set;
		}

		public bool IsDataContextSupported
		{
			get;
			set;
		}

		public bool IsLayoutPageSelected
		{
			get;
			set;
		}

		public bool IsModelClassSupported
		{
			get;
			set;
		}

		public bool IsPartialViewSelected
		{
			get;
			set;
		}

		public bool IsReferenceScriptLibrariesSelected
		{
			get;
			set;
		}

		public string LayoutPageFile
		{
			get;
			set;
		}

		public ModelType ModelType
		{
			get;
			set;
		}

		public IEnumerable<ModelType> ModelTypes
		{
			get;
			private set;
		}

		public override string OutputFileFullPath
		{
			get
			{
				return this.ViewFileFullPath;
			}
		}

		public string ViewFileExtension
		{
			get
			{
				return MvcProjectUtil.GetViewFileExtension(ProjectExtensions.GetCodeLanguage(base.ActiveProject));
			}
		}

		public string ViewFileFullPath
		{
			get
			{
				if (this.ViewName == null)
				{
					return null;
				}
				return base.GetFileFullPath(this.ViewName, this.ViewFileExtension);
			}
		}

		public string ViewName
		{
			get;
			set;
		}

		public ViewTemplate ViewTemplate
		{
			get;
			set;
		}

		public IEnumerable<ViewTemplate> ViewTemplates
		{
			get;
			set;
		}

		public MvcViewScaffolderModel(CodeGenerationContext context) : base(context)
		{
			string str = null;
			this.IsLayoutPageSelected = true;
			this.IsPartialViewSelected = false;
			this.IsReferenceScriptLibrariesSelected = true;
			base.AreaName = MvcViewScaffolderModel.GetAreaNameFromSelection(context.ActiveProjectItem);
			this.ViewTemplates = Enumerable.Empty<ViewTemplate>();
			IEnumerable<CodeType> allCodeTypes = base.ServiceProvider.GetService<ICodeTypeService>().GetAllCodeTypes(base.ActiveProject);
			List<ModelType> modelTypes = new List<ModelType>();
			List<ModelType> modelTypes1 = new List<ModelType>();
			foreach (CodeType allCodeType in allCodeTypes)
			{
				if (!Microsoft.AspNet.Scaffolding.EntityFramework.CodeTypeExtensions.IsValidDbContextType(allCodeType))
				{
					if (!Microsoft.AspNet.Scaffolding.EntityFramework.CodeTypeExtensions.IsValidWebProjectEntityType(allCodeType))
					{
						continue;
					}
					modelTypes.Add(new ModelType(allCodeType));
				}
				else
				{
					modelTypes1.Add(new ModelType(allCodeType));
				}
			}
			this.ModelTypes = modelTypes;
			this.DataContextTypes = modelTypes1;
			this.IsModelClassSupported = true;
			if (!context.Items.TryGetProperty<string>("DefaultViewName", out str))
			{
				this.ViewName = this.GetGeneratedName(MvcProjectUtil.ViewName, this.ViewFileExtension);
			}
			else
			{
				this.ViewName = str;
			}
			if (context.Items.TryGetProperty<string>("ControllerFolderName", out str) && str != null)
			{
				base.SelectionRelativePath = Path.Combine(base.AreaRelativePath, "Views", str);
			}
		}

		public string GenerateDefaultDataContextTypeName()
		{
			Project activeProject = base.ActiveProject;
			string defaultModelsNamespace = MvcProjectUtil.GetDefaultModelsNamespace(ProjectExtensions.GetDefaultNamespace(activeProject));
			CodeDomProvider codeDomProvider = ValidationUtil.GenerateCodeDomProvider(ProjectExtensions.GetCodeLanguage(activeProject));
			string str = codeDomProvider.CreateValidIdentifier(ProjectExtensions.GetDefaultNamespace(activeProject).Replace(".", string.Empty));
			this.DataContextName = string.Concat(defaultModelsNamespace, ".", str, MvcProjectUtil.DataContextSuffix);
			return this.DataContextName;
		}

		private static string GetAreaNameFromSelection(ProjectItem projectItem)
		{
			if (projectItem == null)
			{
				return string.Empty;
			}
			string projectRelativePath = projectItem.GetProjectRelativePath();
			string str = string.Concat("Areas", MvcProjectUtil.PathSeparator);
			if (projectRelativePath.StartsWith(str, StringComparison.OrdinalIgnoreCase))
			{
				string str1 = projectRelativePath.Remove(0, str.Length);
				int num = str1.IndexOf(MvcProjectUtil.PathSeparator, StringComparison.OrdinalIgnoreCase);
				if (num != -1)
				{
					return str1.Substring(0, num);
				}
			}
			return string.Empty;
		}

		public string GetGeneratedName(string resourceName, string fileExtension)
		{
			string i;
			int num = 0;
			int num1 = 1;
			for (i = string.Format(CultureInfo.InvariantCulture, resourceName, string.Empty); File.Exists(Path.Combine(base.SelectionFullPath, string.Concat(i, ".", fileExtension))); i = string.Format(CultureInfo.InvariantCulture, resourceName, num))
			{
				num = num1;
				num1 = num + 1;
			}
			return i;
		}

		public virtual void LoadSettings(IProjectSettings settings)
		{
			bool flag;
			string str;
			if (settings.TryGetBool("WebStackScaffolding_IsLayoutPageSelected", out flag))
			{
				this.IsLayoutPageSelected = flag;
			}
			if (settings.TryGetBool("WebStackScaffolding_IsPartialViewSelected", out flag))
			{
				this.IsPartialViewSelected = flag;
			}
			if (settings.TryGetBool("WebStackScaffolding_IsReferencingScriptLibrariesSelected", out flag))
			{
				this.IsReferenceScriptLibrariesSelected = flag;
			}
			if (settings.TryGetString("WebStackScaffolding_LayoutPageFile", out str))
			{
				this.LayoutPageFile = str;
			}
			if (this.IsModelClassSupported && settings.TryGetString("WebStackScaffolding_DbContextTypeFullName", out str))
			{
				this.DataContextType = (
					from t in this.DataContextTypes
					where string.Equals(t.TypeName, str, StringComparison.Ordinal)
					select t).FirstOrDefault<ModelType>();
			}
		}

		public virtual void SaveSettings(IProjectSettings settings)
		{
			settings.SetBool("WebStackScaffolding_IsLayoutPageSelected", this.IsLayoutPageSelected);
			settings.SetBool("WebStackScaffolding_IsPartialViewSelected", this.IsPartialViewSelected);
			settings.SetBool("WebStackScaffolding_IsReferencingScriptLibrariesSelected", this.IsReferenceScriptLibrariesSelected);
			if (!this.IsLayoutPageSelected)
			{
				settings["WebStackScaffolding_LayoutPageFile"] = null;
			}
			else
			{
				settings["WebStackScaffolding_LayoutPageFile"] = this.LayoutPageFile;
			}
			if (this.IsModelClassSupported && this.DataContextType != null)
			{
				settings["WebStackScaffolding_DbContextTypeFullName"] = this.DataContextType.TypeName;
			}
		}

		public string ValidateDataContextType(ModelType dataContextType)
		{
			if (dataContextType == null)
			{
				return "Database context type name must be non-empty.";

            }
			return null;
		}

		public string ValidateDbContextName(string dbContextName)
		{
			if (string.IsNullOrWhiteSpace(dbContextName))
			{
                return "Database context type name must be non-empty.";

            }
			return null;
		}

		public string ValidateModelType(ModelType modelType)
		{
			if (modelType == null)
			{
				return "Model name must be non-empty.";

            }
			return null;
		}

		public string ValidateViewName(string viewName)
		{
			if (string.IsNullOrWhiteSpace(viewName))
			{
				return "View name must be non-empty.";

            }
			return null;
		}

		public string ValidateViewTemplate(ViewTemplate viewTemplate)
		{
			if (viewTemplate == null)
			{
				return "Template name must be non-empty.";

            }
			return null;
		}

		public bool ViewExists(string viewName)
		{
			return File.Exists(base.GetFileFullPath(viewName, this.ViewFileExtension));
		}
	}
}