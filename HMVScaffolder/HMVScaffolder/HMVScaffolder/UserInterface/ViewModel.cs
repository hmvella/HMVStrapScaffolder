using Microsoft.AspNet.Scaffolding;
using System;
using System.Windows;

namespace HMVScaffolder.Mvc
{
    public abstract class ViewModel : ValidatingViewModel
	{
		private double _dialogWidth;

		protected CodeGenerationContext Context
		{
			get;
			private set;
		}

		public double DialogWidth
		{
			get
			{
				return this._dialogWidth;
			}
			set
			{
				if (base.OnPropertyChanged<double>(ref this._dialogWidth, value, "DialogWidth"))
				{
					this.SaveDialogSettings();
				}
			}
		}

		public ViewModel(ScaffolderModel model)
		{
			this.Context = model.Context;
			this.LoadDialogSettings();
			if (this.DialogWidth == 0 || this.DialogWidth > SystemParameters.PrimaryScreenWidth)
			{
				this.DialogWidth = 600;
			}
		}

		protected void LoadDialogSettings()
		{
			IDialogSettings dialogSetting = this as IDialogSettings;
			IVisualStudioIntegration property = this.Context.Items.GetProperty<IVisualStudioIntegration>(typeof(IVisualStudioIntegration));
			if (dialogSetting != null && property != null)
			{
				IProjectSettings projectSettings = property.GetProjectSettings(this.Context.ActiveProject);
				if (projectSettings != null)
				{
					try
					{
						dialogSetting.LoadDialogSettings(projectSettings);
					}
					catch (Exception exception)
					{
					}
				}
			}
		}

		protected void SaveDialogSettings()
		{
			IDialogSettings dialogSetting = this as IDialogSettings;
			IVisualStudioIntegration property = this.Context.Items.GetProperty<IVisualStudioIntegration>(typeof(IVisualStudioIntegration));
			if (dialogSetting != null && property != null)
			{
				IProjectSettings projectSettings = property.GetProjectSettings(this.Context.ActiveProject);
				if (projectSettings != null)
				{
					try
					{
						dialogSetting.SaveDialogSettings(projectSettings);
					}
					catch (Exception exception)
					{
					}
				}
			}
		}
	}
}