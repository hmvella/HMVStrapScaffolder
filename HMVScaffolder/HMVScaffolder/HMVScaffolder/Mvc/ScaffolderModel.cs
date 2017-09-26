using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	public abstract class ScaffolderModel
	{
		private string _selectionRelativePath;

		public Project ActiveProject
		{
			get
			{
				return this.Context.ActiveProject;
			}
		}

		protected string ActiveProjectFullPath
		{
			get
			{
				return ProjectExtensions.GetFullPath(this.ActiveProject);
			}
		}

		protected ProjectItem ActiveProjectItem
		{
			get
			{
				return this.Context.ActiveProjectItem;
			}
		}

		public string AreaName
		{
			get;
			set;
		}

		public string AreaRelativePath
		{
			get
			{
				if (string.IsNullOrEmpty(this.AreaName))
				{
					return string.Empty;
				}
				return string.Concat("Areas", Path.DirectorySeparatorChar, this.AreaName);
			}
		}

		public string CodeFileExtension
		{
			get
			{
				return ProjectExtensions.GetCodeLanguage(this.ActiveProject).CodeFileExtension;
			}
		}

		internal CodeGenerationContext Context
		{
			get;
			private set;
		}

		public bool IsOverwritingFiles
		{
			get;
			set;
		}

		public abstract string OutputFileFullPath
		{
			get;
		}

		public string SelectionFullPath
		{
			get
			{
				return Path.Combine(ProjectExtensions.GetFullPath(this.ActiveProject), this.SelectionRelativePath);
			}
		}

		public string SelectionRelativePath
		{
			get
			{
				if (this._selectionRelativePath != null)
				{
					return this._selectionRelativePath;
				}
				if (this.ActiveProjectItem == null)
				{
					return string.Empty;
				}
				return this.ActiveProjectItem.GetProjectRelativePath();
			}
			protected set
			{
				Path.Combine(ProjectExtensions.GetFullPath(this.ActiveProject), value ?? string.Empty);
				this._selectionRelativePath = value;
			}
		}

		protected IServiceProvider ServiceProvider
		{
			get;
			private set;
		}

		protected ScaffolderModel(CodeGenerationContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.Context = context;
			this.ServiceProvider = context.ServiceProvider;
		}

		public string GetErrorIfInvalidIdentifier(string text)
		{
			return ValidationUtil.GetErrorIfInvalidIdentifier(text, ProjectExtensions.GetCodeLanguage(this.ActiveProject));
		}

		protected string GetFileFullPath(string name, string codeFileExtension)
		{
			string empty = string.Empty;
			if (this.SelectionFullPath != null && this.IsValidIdentifier(name))
			{
				empty = Path.Combine(this.SelectionFullPath, string.Concat(name, ".", codeFileExtension));
			}
			return empty;
		}

		public bool IsValidIdentifier(string text)
		{
			return string.IsNullOrEmpty(this.GetErrorIfInvalidIdentifier(text));
		}
	}
}