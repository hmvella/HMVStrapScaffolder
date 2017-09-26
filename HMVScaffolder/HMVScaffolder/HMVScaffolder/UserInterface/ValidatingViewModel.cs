using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	public class ValidatingViewModel : NotifyPropertyChanged, IDataErrorInfo
	{
		private bool _isValid;

		public string Error
		{
			get
			{
				if (!this.ErrorMessages.Any<KeyValuePair<string, string>>())
				{
					return null;
				}
				return this.ErrorMessages.First<KeyValuePair<string, string>>().Value;
			}
		}

		private Dictionary<string, string> ErrorMessages
		{
			get;
			set;
		}

		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
			private set
			{
				base.OnPropertyChanged<bool>(ref this._isValid, value, "IsValid");
			}
		}

		public string this[string columnName]
		{
			get
			{
				string str;
				if (columnName == null)
				{
					throw new ArgumentNullException("columnName");
				}
				this.ErrorMessages.TryGetValue(columnName, out str);
				return str;
			}
		}

		public ValidatingViewModel()
		{
			this.ErrorMessages = new Dictionary<string, string>();
			this.IsValid = true;
		}

		protected static void DisplayErrorMessage(IDialogHost dialogHost, string errorMessage)
		{
			if (dialogHost == null)
			{
				throw new ArgumentNullException("dialogHost");
			}
			dialogHost.ShowErrorMessage(errorMessage, null);
		}

		protected void SetValidationMessage(string message, [CallerMemberName] string propertyName = null)
		{
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			if (message == null)
			{
				this.ErrorMessages.Remove(propertyName);
				if (!this.ErrorMessages.Any<KeyValuePair<string, string>>())
				{
					this.IsValid = true;
					return;
				}
			}
			else if (message != null)
			{
				this.ErrorMessages[propertyName] = message;
				this.IsValid = false;
			}
		}
	}
}