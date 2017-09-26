using Microsoft.AspNet.Scaffolding.Mvc;
using Microsoft.AspNet.Scaffolding.Mvc.VisualStudio;
using System;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Scaffolding.Mvc.UserInterface
{
	public class MvcDependencyScaffolderViewModel : ViewModel, IDialogSettings
	{
		private bool _isMinimal;

		private bool _isFull;

		public bool IsFullSelected
		{
			get
			{
				return this._isFull;
			}
			set
			{
				base.OnPropertyChanged<bool>(ref this._isFull, value, "IsFullSelected");
			}
		}

		public bool IsMinimalSelected
		{
			get
			{
				return this._isMinimal;
			}
			set
			{
				base.OnPropertyChanged<bool>(ref this._isMinimal, value, "IsMinimalSelected");
			}
		}

		protected MvcDependencyScaffolderModel Model
		{
			get;
			set;
		}

		public MvcDependencyScaffolderViewModel(MvcDependencyScaffolderModel model) : base(model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.Model = model;
			this.IsMinimalSelected = true;
		}

		public virtual void LoadDialogSettings(IProjectSettings settings)
		{
			double num;
			if (settings.TryGetDouble("WebStackScaffolding_DependencyDialogWidth", out num))
			{
				base.DialogWidth = num;
			}
		}

		public virtual void SaveDialogSettings(IProjectSettings settings)
		{
			settings["WebStackScaffolding_DependencyDialogWidth"] = base.DialogWidth.ToString();
		}
	}
}