using System.IO;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    public class MvcAreaScaffolderModel : ScaffolderModel
	{
		public string AreaFileFullPath
		{
			get
			{
				if (base.AreaName == null)
				{
					return null;
				}
				return this.GetAreaFileFullPath(base.AreaName);
			}
		}

		public override string OutputFileFullPath
		{
			get
			{
				return this.AreaFileFullPath;
			}
		}

		public MvcAreaScaffolderModel(CodeGenerationContext context) : base(context)
		{
		}

		public bool AreaExists(string areaName)
		{
			return Directory.Exists(this.GetAreaFullPath(areaName));
		}

		private string GetAreaFileFullPath(string areaName)
		{
			string empty = string.Empty;
			if (base.IsValidIdentifier(areaName))
			{
				empty = Path.Combine(this.GetAreaFullPath(areaName), string.Concat(areaName, MvcProjectUtil.AreaRegistration, ".", base.CodeFileExtension));
			}
			return empty;
		}

		private string GetAreaFullPath(string areaName)
		{
			string empty = string.Empty;
			if (base.ActiveProjectFullPath != null && base.IsValidIdentifier(areaName))
			{
				empty = Path.Combine(base.ActiveProjectFullPath, "Areas", areaName);
			}
			return empty;
		}

		public string ValidateAreaName(string areaName)
		{
			if (string.IsNullOrWhiteSpace(areaName))
			{
				return "Area name must be non-empty.";

            }
			return null;
		}
	}
}