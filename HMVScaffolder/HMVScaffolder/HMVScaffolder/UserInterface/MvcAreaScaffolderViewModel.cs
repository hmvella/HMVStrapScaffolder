using Microsoft.AspNet.Scaffolding.Mvc;
using Microsoft.AspNet.Scaffolding.Mvc.Properties;
using Microsoft.AspNet.Scaffolding.Mvc.VisualStudio;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Scaffolding.Mvc.UserInterface
{
	public class MvcAreaScaffolderViewModel : ViewModel, IDialogSettings
	{
		private string _areaName;

		private IDialogHost _dialogHost;

		public string AreaName
		{
			get
			{
				return this._areaName;
			}
			set
			{
				string str;
				if (base.OnPropertyChanged<string>(ref this._areaName, value, "AreaName"))
				{
					if (value == null)
					{
						str = null;
					}
					else
					{
						str = value.Trim();
					}
					this._areaName = str;
					base.SetValidationMessage(this.Model.ValidateAreaName(this._areaName), "AreaName");
					this.Model.AreaName = this._areaName;
				}
			}
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

		private MvcAreaScaffolderModel Model
		{
			get;
			set;
		}

		public MvcAreaScaffolderViewModel(MvcAreaScaffolderModel model) : base(model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.Model = model;
			this.AreaName = model.AreaName;
			base.SetValidationMessage(this.Model.ValidateAreaName(this.AreaName), "AreaName");
		}

		private void DialogHost_Closing(object sender, CancelEventArgs e)
		{
			string errorIfInvalidIdentifier = this.Model.GetErrorIfInvalidIdentifier(this.AreaName);
			if (!string.IsNullOrEmpty(errorIfInvalidIdentifier))
			{
				ValidatingViewModel.DisplayErrorMessage(this.DialogHost, errorIfInvalidIdentifier);
				e.Cancel = true;
				return;
			}
			if (this.Model.AreaExists(this.AreaName))
			{
				ValidatingViewModel.DisplayErrorMessage(this.DialogHost, Resources.AreaExistsMessage);
				e.Cancel = true;
			}
		}

		public virtual void LoadDialogSettings(IProjectSettings settings)
		{
			double num;
			if (settings.TryGetDouble("WebStackScaffolding_AreaDialogWidth", out num))
			{
				base.DialogWidth = num;
			}
		}

		public virtual void SaveDialogSettings(IProjectSettings settings)
		{
			settings["WebStackScaffolding_AreaDialogWidth"] = base.DialogWidth.ToString();
		}
	}
}