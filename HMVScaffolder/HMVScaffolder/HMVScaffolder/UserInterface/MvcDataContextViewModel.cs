using HMVScaffolder.Mvc.VisualStudio;
using System;

namespace HMVScaffolder.Mvc
{
    public class MvcDataContextViewModel : ViewModel, IDialogSettings
	{
		private string _dataContextName;

		public string DataContextName
		{
			get
			{
				return this._dataContextName;
			}
			set
			{
				if (base.OnPropertyChanged<string>(ref this._dataContextName, value, "DataContextName"))
				{
					this.Model.DataContextName = value;
					base.SetValidationMessage(this.Model.ValidateDbContextName(value), "DataContextName");
				}
			}
		}

		protected ControllerScaffolderModel Model
		{
			get;
			set;
		}

		public MvcDataContextViewModel(ControllerScaffolderModel model) : base(model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.Model = model;
			this.DataContextName = model.DataContextName;
		}

		public virtual void LoadDialogSettings(IProjectSettings settings)
		{
			double num;
			if (settings.TryGetDouble("WebStackScaffolding_DbContextDialogWidth", out num))
			{
				base.DialogWidth = num;
			}
		}

		public virtual void SaveDialogSettings(IProjectSettings settings)
		{
			settings["WebStackScaffolding_DbContextDialogWidth"] = base.DialogWidth.ToString();
		}
	}
}