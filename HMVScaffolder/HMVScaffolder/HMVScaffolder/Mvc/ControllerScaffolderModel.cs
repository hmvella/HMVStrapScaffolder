using Microsoft.AspNet.Scaffolding.EntityFramework;
using HMVScaffolder.Mvc.VisualStudio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.AspNet.Scaffolding;

namespace HMVScaffolder.Mvc
{
    public class ControllerScaffolderModel : MvcViewScaffolderModel
	{
		public string ControllerFileFullPath
		{
			get
			{
				if (this.ControllerName == null)
				{
					return null;
				}
				return base.GetFileFullPath(this.ControllerName, base.CodeFileExtension);
			}
		}

		public string ControllerName
		{
			get;
			set;
		}

		public string ControllerNamespace
		{
			get
			{
				if (base.ActiveProjectItem != null)
				{
					return ProjectExtensions.GetDefaultNamespace(base.ActiveProjectItem);
				}
				if (!this.IsControllersFolderCreated())
				{
					return ProjectExtensions.GetDefaultNamespace(base.ActiveProject);
				}
				return string.Concat(ProjectExtensions.GetDefaultNamespace(base.ActiveProject), ".Controllers");
			}
		}

		public string ControllerRootName
		{
			get
			{
				return MvcProjectUtil.StripControllerName(this.ControllerName);
			}
		}

		public bool IsAsyncSelected
		{
			get;
			set;
		}

		public bool IsAsyncSupported
		{
			get;
			set;
		}

		public bool IsViewFolderRequired
		{
			get;
			set;
		}

		public bool IsViewGenerationSelected
		{
			get;
			set;
		}

		public bool IsViewGenerationSupported
		{
			get;
			set;
		}

		public override string OutputFileFullPath
		{
			get
			{
				return this.ControllerFileFullPath;
			}
		}

		public new string SelectionRelativePath
		{
			get
			{
				return base.SelectionRelativePath;
			}
			set
			{
				if (base.ActiveProjectItem == null && this.IsControllersFolderCreated())
				{
					base.SelectionRelativePath = value;
				}
			}
		}

		public ControllerScaffolderModel(CodeGenerationContext context) : base(context)
		{
			base.IsModelClassSupported = false;
			this.IsAsyncSupported = this.IsUsingEntityFrameworkWithAsyncSupport();
		}

		public bool ControllerExists(string controllerName)
		{
			return File.Exists(base.GetFileFullPath(controllerName, base.CodeFileExtension));
		}

		public string GenerateControllerName(string modelClassName)
		{
			if (string.IsNullOrWhiteSpace(modelClassName))
			{
				return null;
			}
			IEntityFrameworkService service = base.Context.ServiceProvider.GetService<IEntityFrameworkService>();
			string pluralizedWord = service.GetPluralizedWord(modelClassName, CultureInfo.GetCultureInfo(1033)) ?? modelClassName;
			return base.GetGeneratedName(string.Concat(pluralizedWord, "{0}", MvcProjectUtil.ControllerSuffix), base.CodeFileExtension);
		}

		private bool IsControllersFolderCreated()
		{
			object obj = null;
			base.Context.Items.TryGetProperty<object>("MVC_IsControllersFolderCreated", out obj);
			if (obj == null)
			{
				return false;
			}
			return (bool)obj;
		}

		private bool IsUsingEntityFrameworkWithAsyncSupport()
		{
			bool version;
			using (IEnumerator<string> enumerator = ProjectExtensions.GetAssemblyReferences(base.ActiveProject).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AssemblyName assemblyName = AssemblyName.GetAssemblyName(enumerator.Current);
					if (!assemblyName.Name.Equals(AssemblyVersions.AsyncEntityFrameworkAssemblyName))
					{
						continue;
					}
					version = assemblyName.Version >= AssemblyVersions.AsyncEntityFrameworkMinVersion;
					return version;
				}
				return true;
			}
			return version;
		}

		public override void LoadSettings(IProjectSettings settings)
		{
			bool flag;
			base.LoadSettings(settings);
			if (this.IsViewGenerationSupported)
			{
				if (!settings.TryGetBool("WebStackScaffolding_IsViewGenerationSelected", out flag))
				{
					this.IsViewGenerationSelected = true;
				}
				else
				{
					this.IsViewGenerationSelected = flag;
				}
			}
			if (this.IsAsyncSupported && settings.TryGetBool("WebStackScaffolding_IsAsyncSelected", out flag))
			{
				this.IsAsyncSelected = flag;
			}
		}

		public override void SaveSettings(IProjectSettings settings)
		{
			base.SaveSettings(settings);
			if (this.IsViewGenerationSupported)
			{
				settings.SetBool("WebStackScaffolding_IsViewGenerationSelected", this.IsViewGenerationSelected);
			}
			if (this.IsAsyncSupported)
			{
				settings.SetBool("WebStackScaffolding_IsAsyncSelected", this.IsAsyncSelected);
			}
		}

		public string ValidateControllerName(string controllerName)
		{
			if (string.IsNullOrWhiteSpace(controllerName))
			{
				return "Controller name must be non-empty.";
			}
			if (string.Equals(controllerName, MvcProjectUtil.ControllerSuffix, StringComparison.OrdinalIgnoreCase))
			{
				return "The name is invalid because it is a reserved name.";

            }
			return null;
		}
	}
}