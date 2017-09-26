using EnvDTE;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc.UserInterface
{
	public class ModelType
	{
		public EnvDTE.CodeType CodeType
		{
			get;
			set;
		}

		public string DisplayName
		{
			get;
			set;
		}

		public string ShortTypeName
		{
			get;
			set;
		}

		public string TypeName
		{
			get;
			set;
		}

		public ModelType(EnvDTE.CodeType codeType)
		{
			if (codeType == null)
			{
				throw new ArgumentNullException("codeType");
			}
			this.CodeType = codeType;
			this.TypeName = codeType.FullName;
			this.ShortTypeName = codeType.Name;
			this.DisplayName = (codeType.Namespace == null || string.IsNullOrWhiteSpace(codeType.Namespace.FullName) ? codeType.Name : string.Format(CultureInfo.InvariantCulture, "{0} ({1})", codeType.Name, codeType.Namespace.FullName));
		}

		public ModelType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.CodeType = null;
			this.TypeName = typeName;
			this.DisplayName = typeName;
			this.ShortTypeName = typeName;
		}
	}
}