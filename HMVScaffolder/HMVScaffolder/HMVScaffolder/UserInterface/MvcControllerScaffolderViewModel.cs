using HMVScaffolder.Mvc.VisualStudio;
using HMVScaffolder.Properties;
using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HMVScaffolder.Mvc
{
	public class MvcControllerScaffolderViewModel : ViewModel, IDialogSettings
	{
		private IDialogHost _dialogHost;

		private string _controllerName;

		private ModelType _modelType;

		private string _modelTypeName;

		private string _dataContextTypeName;

		private ModelType _dataContextType;

		private bool _isViewGenerationSelected;

		private bool _isLayoutPageSelected;

		private bool _isReferenceScriptLibrariesSelected;

		private string _layoutPageFile;

		private bool _isAsyncSelected;

		private ModelType AddedDataContextItem
		{
			get;
			set;
		}

		public ICommand AddNewDataContextCommand
		{
			get;
			private set;
		}

		public ImageSource AsyncInformationIcon
		{
			get;
			private set;
		}

		public string ControllerName
		{
			get
			{
				return this._controllerName;
			}
			set
			{
				string str;
				if (base.OnPropertyChanged<string>(ref this._controllerName, value, "ControllerName"))
				{
					if (value == null)
					{
						str = null;
					}
					else
					{
						str = value.Trim();
					}
					this._controllerName = str;
					base.SetValidationMessage(this.Model.ValidateControllerName(this._controllerName), "ControllerName");
					this.Model.ControllerName = this._controllerName;
				}
			}
		}

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
					if (this.Model.IsModelClassSupported)
					{
						base.SetValidationMessage(this.Model.ValidateDataContextType(value), "DataContextType");
					}
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
				if (base.OnPropertyChanged<string>(ref this._dataContextTypeName, value, "DataContextTypeName") && this.IsModelClassSupported && this.DataContextType != null && this.DataContextType.DisplayName.StartsWith(value, StringComparison.Ordinal))
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

		public bool IsAsyncSelected
		{
			get
			{
				return this._isAsyncSelected;
			}
			set
			{
				if (base.OnPropertyChanged<bool>(ref this._isAsyncSelected, value, "IsAsyncSelected"))
				{
					this._isAsyncSelected = value;
					this.Model.IsAsyncSelected = this._isAsyncSelected;
				}
			}
		}

		public bool IsAsyncSupported
		{
			get
			{
				return this.Model.IsAsyncSupported;
			}
		}

		public bool IsDataContextSupported
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

		public bool IsModelClassSupported
		{
			get;
			private set;
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

		public bool IsViewGenerationSelected
		{
			get
			{
				return this._isViewGenerationSelected;
			}
			set
			{
				if (base.OnPropertyChanged<bool>(ref this._isViewGenerationSelected, value, "IsViewGenerationSelected"))
				{
					this.Model.IsViewGenerationSelected = value;
				}
			}
		}

		public bool IsViewGenerationSupported
		{
			get;
			private set;
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

		protected ControllerScaffolderModel Model
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
				string shortTypeName;
				if (base.OnPropertyChanged<ModelType>(ref this._modelType, value, "ModelType") && this.Model.IsModelClassSupported)
				{
					base.SetValidationMessage(this.Model.ValidateModelType(value), "ModelType");
					if (!this.IsControllerNameUserSet(this.Model.ModelType, this.ControllerName))
					{
						ControllerScaffolderModel model = this.Model;
						if (value == null)
						{
							shortTypeName = null;
						}
						else
						{
							shortTypeName = value.ShortTypeName;
						}
						this.ControllerName = model.GenerateControllerName(shortTypeName);
					}
					this.Model.ModelType = value;
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
				if (base.OnPropertyChanged<string>(ref this._modelTypeName, value, "ModelTypeName") && this.Model.IsModelClassSupported && this.ModelType != null && this.ModelType.DisplayName.StartsWith(value, StringComparison.Ordinal))
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

		public MvcControllerScaffolderViewModel(ControllerScaffolderModel model) : base(model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.Model = model;
			this.ControllerName = model.ControllerName;
			this.IsAsyncSelected = model.IsAsyncSelected;
			base.SetValidationMessage(this.Model.ValidateControllerName(this.ControllerName), "ControllerName");
			this.IsViewGenerationSupported = this.Model.IsViewGenerationSupported;
			if (this.IsViewGenerationSupported)
			{
				this.IsViewGenerationSelected = this.Model.IsViewGenerationSelected;
				this.IsLayoutPageSelected = this.Model.IsLayoutPageSelected;
				this.IsReferenceScriptLibrariesSelected = this.Model.IsReferenceScriptLibrariesSelected;
				this.LayoutPageFile = model.LayoutPageFile;
			}
			this.DataContextTypesInternal = new ObservableCollection<ModelType>();
			this.ModelTypesInternal = new ObservableCollection<ModelType>();
			this.DataContextTypes = new ListCollectionView(this.DataContextTypesInternal)
			{
				CustomSort = new DataContextModelTypeComparer()
			};
			this.ModelTypes = CollectionViewSource.GetDefaultView(this.ModelTypesInternal);
			this.ModelTypes.SortDescriptions.Add(new SortDescription("ShortTypeName", ListSortDirection.Ascending));
			this.IsModelClassSupported = this.Model.IsModelClassSupported;
			this.IsDataContextSupported = this.Model.IsDataContextSupported;
			if (this.Model.IsModelClassSupported)
			{
				foreach (ModelType modelType in this.Model.ModelTypes)
				{
					this.ModelTypesInternal.Add(modelType);
				}
				base.SetValidationMessage(this.Model.ValidateModelType(null), "ModelType");
			}
			if (this.Model.IsDataContextSupported)
			{
				foreach (ModelType dataContextType in this.Model.DataContextTypes)
				{
					this.DataContextTypesInternal.Add(dataContextType);
				}
				if (model.DataContextType != null)
				{
					this.DataContextType = this.Model.DataContextType;
					this.DataContextTypeName = this.DataContextType.DisplayName;
				}
				base.SetValidationMessage(this.Model.ValidateDataContextType(this.DataContextType), "DataContextType");
			}
			this.AddNewDataContextCommand = new RelayCommand(new Action<object>(this.AddNewDataContext));
			this.SelectLayoutCommand = new RelayCommand(new Action<object>(this.SelectLayout));
			this.AsyncInformationIcon = MvcControllerScaffolderViewModel.GetInformationIcon();
		}

		public ModelType AddNewDataContext(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (this.AddedDataContextItem != null)
			{
				this.DataContextType = null;
				this.DataContextTypeName = null;
				this.DataContextTypesInternal.Remove(this.AddedDataContextItem);
			}
			this.AddedDataContextItem = new ModelType(typeName);
			this.DataContextTypesInternal.Add(this.AddedDataContextItem);
			return this.AddedDataContextItem;
		}

        private void AddNewDataContext(object param)
        {
            //////////CreateDataContextDialog createDataContextDialog = new CreateDataContextDialog();
            this.Model.DataContextName = this.GenerateDefaultDataContextTypeName();
            MvcDataContextViewModel mvcDataContextViewModel = new MvcDataContextViewModel(this.Model);
            //////////createDataContextDialog.DataContext = mvcDataContextViewModel;
            //////////bool? nullable = createDataContextDialog.ShowModal();
            //////////if ((!nullable.GetValueOrDefault() ? true : !nullable.HasValue))
            //////////{
            //////////    this.DataContextType = null;
            //////////    this.DataContextTypeName = null;
            //////////    return;
            //////////}
            this.DataContextType = this.AddNewDataContext(mvcDataContextViewModel.DataContextName);
            this.DataContextTypeName = this.DataContextType.DisplayName;
        }

        private void DialogHost_Closing(object sender, CancelEventArgs e)
		{
			string errorIfInvalidIdentifier = this.Model.GetErrorIfInvalidIdentifier(this.ControllerName);
			if (!string.IsNullOrEmpty(errorIfInvalidIdentifier))
			{
				ValidatingViewModel.DisplayErrorMessage(this.DialogHost, errorIfInvalidIdentifier);
				e.Cancel = true;
				return;
			}
			if (this.Model.ControllerExists(this.ControllerName))
			{
				if (this.DialogHost.RequestConfirmation(string.Format(CultureInfo.CurrentCulture, "A file with the name {0} already exists. Do you want to replace it?", this.ControllerName), "Add Controller") == MessageBoxResult.Yes)

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

		private static BitmapSource GetInformationIcon()
		{
			return Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Information.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
		}

		private bool IsControllerNameUserSet(ModelType previousModelSelected, string controllerName)
		{
			if (string.IsNullOrWhiteSpace(controllerName) || string.IsNullOrWhiteSpace(this.Model.ControllerRootName))
			{
				return false;
			}
			if (previousModelSelected != null && string.Equals(this.Model.GenerateControllerName(previousModelSelected.ShortTypeName), controllerName, StringComparison.Ordinal))
			{
				return false;
			}
			return true;
		}

		public virtual void LoadDialogSettings(IProjectSettings settings)
		{
			double num;
			if (settings.TryGetDouble("WebStackScaffolding_ControllerDialogWidth", out num))
			{
				base.DialogWidth = num;
			}
		}

		public virtual void SaveDialogSettings(IProjectSettings settings)
		{
			settings["WebStackScaffolding_ControllerDialogWidth"] = base.DialogWidth.ToString();
		}

		private void SelectLayout(object unused)
		{
			string masterPageVbHtmlFilter;
			string str;
			ProjectLanguage codeLanguage = ProjectExtensions.GetCodeLanguage(this.Model.ActiveProject);
			
	        masterPageVbHtmlFilter = "Layout Pages (*.cshtml)|*.cshtml";

            if (this.DialogHost.TrySelectFile(this.Model.ActiveProject, "Select a Layout Page", masterPageVbHtmlFilter, "WebStackScaffolding_LayoutPageFile", out str))
			{
				this.LayoutPageFile = str;
			}
		}
	}
}