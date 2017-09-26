using System;

namespace HMVScaffolder.Mvc
{
    public class ViewTemplate : IEquatable<ViewTemplate>
	{
		public string DisplayName
		{
			get;
			private set;
		}

		public bool IsModelRequired
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			private set;
		}

		public ViewTemplate(string name, bool isModelRequired)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Template name must be non-empty.");

            }
			this.Name = name;
			this.IsModelRequired = isModelRequired;
			if (this.IsModelRequired)
			{
				this.DisplayName = this.Name;
				return;
			}
			this.DisplayName = string.Concat(this.Name, " ", "(without model)");

        }

		public bool Equals(ViewTemplate other)
		{
			if (other == null)
			{
				return false;
			}
			if (!string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			return this.IsModelRequired == other.IsModelRequired;
		}

		public override bool Equals(object obj)
		{
			ViewTemplate viewTemplate = obj as ViewTemplate;
			if (viewTemplate == null)
			{
				return false;
			}
			return this.Equals(viewTemplate);
		}

		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.Name) ^ this.IsModelRequired.GetHashCode();
		}
	}
}