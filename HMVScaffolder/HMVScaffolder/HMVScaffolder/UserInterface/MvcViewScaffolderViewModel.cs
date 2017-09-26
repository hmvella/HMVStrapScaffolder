using HMVScaffolder.Properties;
using Microsoft.AspNet.Scaffolding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HMVScaffolder.Mvc
{
    public class MvcViewScaffolderViewModel : ViewModel, IDialogSettings
	{
		private IDialogHost _dialogHost;

		private string _viewName;

		private ViewTemplate _viewTemplate;

		private string _viewTemplateSearchText;

		private bool _isViewWithModelSelected;

		private ModelType _modelType;

		private string _modelTypeName;

		private string _dataContextTypeName;

		private ModelType _dataContextType;

		private bool _isPartialViewSelected;

		private bool _isLayoutPageSelected;

		private bool _isReferenceScriptLibrariesSelected;

		private string _layoutPageFile;

		public ModelType DataContextType
		{
			get
			{
				return this._dataContextType;
			}
			set
			{
				if (base.OnPropertyChanged<ModelType>(ref this._dataContextType, value, "DataContextType"))
				{
					this.Model.DataContextType = value;
				}
			}
		}

		public string DataContextTypeName
		{
			get
			{
				return this._dataContextTypeName;
			}
			set
			{
				if (base.OnPropertyChanged<string>(ref this._dataContextTypeName, value, "DataContextTypeName") && this.IsViewWithModelSelected && this.IsDataContextSelectorVisible && this.DataContextType != null && this.DataContextType.DisplayName.StartsWith(value, StringComparison.Ordinal))
				{
					this._dataContextTypeName = this.DataContextType.DisplayName;
				}
			}
		}

		public ListCollectionView DataContextTypes
		{
			get;
			private set;
		}

		internal ObservableCollection<ModelType> DataContextTypesInternal
		{
			get;
			private set;
		}

		public IDialogHost DialogHost
		{
			get
			{
				return this._dialogHost;
			}
			set
			{
				IDialogHost dialogHost = this._dialogHost;
				if (base.OnPropertyChanged<IDialogHost>(ref this._dialogHost, value, "DialogHost"))
				{
					if (dialogHost != null)
					{
						dialogHost.Closing -= new EventHandler<CancelEventArgs>(this.DialogHost_Closing);
					}
					if (value != null)
					{
						value.Closing += new EventHandler<CancelEventArgs>(this.DialogHost_Closing);
					}
				}
			}
		}

		public bool IsDataContextSelectorVisible
		{
			get;
			private set;
		}

		public bool IsLayoutPageSelected
		{
			get
			{
				return this._isLayoutPageSelected;
			}
			set
			{
				if (base.OnPropertyChanged<bool>(ref this._isLayoutPageSelected, value, "IsLayoutPageSelected"))
				{
					this.Model.IsLayoutPageSelected = value;
				}
			}
		}

		public bool IsPartialViewSelected
		{
			get
			{
				return this._isPartialViewSelected;
			}
			set
			{
				if (base.OnPropertyChanged<bool>(ref this._isPartialViewSelected, value, "IsPartialViewSelected"))
				{
					this.Model.IsPartialViewSelected = value;
				}
			}
		}

		public bool IsReferenceScriptLibrariesSelected
		{
			get
			{
				return this._isReferenceScriptLibrariesSelected;
			}
			set
			{
				if (base.OnPropertyChanged<bool>(ref this._isReferenceScriptLibrariesSelected, value, "IsReferenceScriptLibrariesSelected"))
				{
					this.Model.IsReferenceScriptLibrariesSelected = value;
				}
			}
		}

		public bool IsViewTemplateSelectorVisible
		{
			get;
			private set;
		}

		public bool IsViewWithModelSelected
		{
			get
			{
				return this._isViewWithModelSelected;
			}
			set
			{
				base.OnPropertyChanged<bool>(ref this._isViewWithModelSelected, value, "IsViewWithModelSelected");
				if (!value)
				{
					base.SetValidationMessage(null, "ModelType");
					return;
				}
				base.SetValidationMessage(this.Model.ValidateModelType(this.ModelType), "ModelType");
			}
		}

		public string LayoutPageFile
		{
			get
			{
				return this._layoutPageFile;
			}
			set
			{
				if (base.OnPropertyChanged<string>(ref this._layoutPageFile, value, "LayoutPageFile"))
				{
					this.Model.LayoutPageFile = value;
				}
			}
		}

		protected MvcViewScaffolderModel Model
		{
			get;
			set;
		}

		public ModelType ModelType
		{
			get
			{
				return this._modelType;
			}
			set
			{
				if (base.OnPropertyChanged<ModelType>(ref this._modelType, value, "ModelType"))
				{
					this.Model.ModelType = value;
					if (this.IsViewWithModelSelected)
					{
						base.SetValidationMessage(this.Model.ValidateModelType(value), "ModelType");
					}
				}
			}
		}

		public string ModelTypeName
		{
			get
			{
				return this._modelTypeName;
			}
			set
			{
				if (base.OnPropertyChanged<string>(ref this._modelTypeName, value, "ModelTypeName") && this.IsViewWithModelSelected && this.ModelType != null && this.ModelType.DisplayName.StartsWith(value, StringComparison.Ordinal))
				{
					this._modelTypeName = this.ModelType.DisplayName;
				}
			}
		}

		public ICollectionView ModelTypes
		{
			get;
			private set;
		}

		internal ObservableCollection<ModelType> ModelTypesInternal
		{
			get;
			private set;
		}

		public ICommand SelectLayoutCommand
		{
			get;
			private set;
		}

		public string ViewName
		{
			get
			{
				return this._viewName;
			}
			set
			{
				string str;
				if (base.OnPropertyChanged<string>(ref this._viewName, value, "ViewName"))
				{
					if (value == null)
					{
						str = null;
					}
					else
					{
						str = value.Trim();
					}
					this._viewName = str;
					base.SetValidationMessage(this.Model.ValidateViewName(this._viewName), "ViewName");
					this.Model.ViewName = this._viewName;
				}
			}
		}

		public ViewTemplate ViewTemplate
		{
			get
			{
				return this._viewTemplate;
			}
			set
			{
				if (base.OnPropertyChanged<ViewTemplate>(ref this._viewTemplate, value, "ViewTemplate"))
				{
					base.SetValidationMessage(this.Model.ValidateViewTemplate(this._viewTemplate), "ViewTemplate");
					this.Model.ViewTemplate = this._viewTemplate;
					if (value == null)
					{
						this.IsViewWithModelSelected = true;
						return;
					}
					this.IsViewWithModelSelected = value.IsModelRequired;
				}
			}
		}

		public ICollectionView ViewTemplates
		{
			get;
			private set;
		}

		public string ViewTemplateSearchText
		{
			get
			{
				return this._viewTemplateSearchText;
			}
			set
			{
				if (base.OnPropertyChanged<string>(ref this._viewTemplateSearchText, value, "ViewTemplateSearchText") && this.ViewTemplate != null && this.ViewTemplate.DisplayName.StartsWith(value, StringComparison.Ordinal))
				{
					this._viewTemplateSearchText = this.ViewTemplate.DisplayName;
				}
			}
		}

		public IEnumerable<ViewTemplate> ViewTemplatesInternal
		{
			get;
			private set;
		}

		public MvcViewScaffolderViewModel(MvcViewScaffolderModel model) : base(model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.Model = model;
			this.ViewTemplatesInternal = model.ViewTemplates;
			ViewTemplate viewTemplate = new ViewTemplate(MvcViewTemplates.Empty, false);
			if (this.ViewTemplatesInternal.Any<ViewTemplate>())
			{
				this.ViewTemplate = this.ViewTemplatesInternal.FirstOrDefault<ViewTemplate>((ViewTemplate view) => view.Equals(viewTemplate)) ?? this.ViewTemplatesInternal.First<ViewTemplate>();
				this.ViewTemplateSearchText = this.ViewTemplate.DisplayName;
				this.IsViewTemplateSelectorVisible = this.ViewTemplatesInternal.Count<ViewTemplate>() > 1;
			}
			this.ViewTemplates = CollectionViewSource.GetDefaultView(this.ViewTemplatesInternal);
			this.ViewTemplates.SortDescriptions.Add(new SortDescription("DisplayName", ListSortDirection.Ascending));
			this.IsPartialViewSelected = model.IsPartialViewSelected;
			this.IsReferenceScriptLibrariesSelected = model.IsReferenceScriptLibrariesSelected;
			this.IsLayoutPageSelected = model.IsLayoutPageSelected;
			this.LayoutPageFile = model.LayoutPageFile;
			this.ViewName = model.ViewName;
			base.SetValidationMessage(model.ValidateViewName(this.ViewName), "ViewName");
			this.DataContextTypesInternal = new ObservableCollection<ModelType>();
			this.ModelTypesInternal = new ObservableCollection<ModelType>();
			foreach (ModelType modelType in this.Model.ModelTypes)
			{
				this.ModelTypesInternal.Add(modelType);
			}
			foreach (ModelType dataContextType in this.Model.DataContextTypes)
			{
				this.DataContextTypesInternal.Add(dataContextType);
			}
			this.DataContextTypes = new ListCollectionView(this.DataContextTypesInternal)
			{
				CustomSort = new DataContextModelTypeComparer()
			};
			this.ModelTypes = CollectionViewSource.GetDefaultView(this.ModelTypesInternal);
			this.ModelTypes.SortDescriptions.Add(new SortDescription("ShortTypeName", ListSortDirection.Ascending));
			this.IsDataContextSelectorVisible = this.DataContextTypesInternal.Any<ModelType>();
			if (this.IsDataContextSelectorVisible && model.DataContextType != null)
			{
				this.DataContextType = this.Model.DataContextType;
				this.DataContextTypeName = this.DataContextType.DisplayName;
			}
			if (this.ViewTemplate == null)
			{
				base.SetValidationMessage(this.Model.ValidateViewTemplate(null), "ViewTemplate");
			}
			else if (!this.ViewTemplate.IsModelRequired)
			{
				base.SetValidationMessage(null, "ModelType");
			}
			else
			{
				base.SetValidationMessage(this.Model.ValidateModelType(null), "ModelType");
			}
			this.SelectLayoutCommand = new RelayCommand(new Action<object>(this.SelectLayout));
		}

		private void DialogHost_Closing(object sender, CancelEventArgs e)
		{
			string errorIfInvalidIdentifier = this.Model.GetErrorIfInvalidIdentifier(this.ViewName);
			if (!string.IsNullOrEmpty(errorIfInvalidIdentifier))
			{
				ValidatingViewModel.DisplayErrorMessage(this.DialogHost, errorIfInvalidIdentifier);
				e.Cancel = true;
				return;
			}
			if (this.Model.ViewExists(this.ViewName))
			{
				if (this.DialogHost.RequestConfirmation(string.Format(CultureInfo.CurrentCulture, ResourcesExt.OverwriteMessage, this.ViewName), ResourcesExt.AddViewWindowTitle) == MessageBoxResult.Yes)
				{
					this.Model.IsOverwritingFiles = true;
					return;
				}
				e.Cancel = true;
			}
		}

		public string GenerateDefaultDataContextTypeName()
		{
			return this.Model.GenerateDefaultDataContextTypeName();
		}

		public virtual void LoadDialogSettings(IProjectSettings settings)
		{
			double num;
			if (settings.TryGetDouble("WebStackScaffolding_ViewDialogWidth", out num))
			{
				base.DialogWidth = num;
			}
		}

		public virtual void SaveDialogSettings(IProjectSettings settings)
		{
			settings["WebStackScaffolding_ViewDialogWidth"] = base.DialogWidth.ToString();
		}

		private void SelectLayout(object unused)
		{
			string masterPageVbHtmlFilter;
			string str;
			ProjectLanguage codeLanguage = ProjectExtensions.GetCodeLanguage(this.Model.ActiveProject);
			
				masterPageVbHtmlFilter = ResourcesExt.MasterPageCsHtmlFilter;
			if (this.DialogHost.TrySelectFile(this.Model.ActiveProject, ResourcesExt.LayoutPageSelectorHeading, masterPageVbHtmlFilter, "WebStackScaffolding_LayoutPageFile", out str))
			{
				this.LayoutPageFile = str;
			}
		}
	}
}