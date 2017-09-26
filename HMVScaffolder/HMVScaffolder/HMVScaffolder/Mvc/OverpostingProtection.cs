using EnvDTE;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HMVScaffolder.Mvc
{
    internal static class OverpostingProtection
	{
		private const string BindAttributeTypeName = "System.Web.Mvc.BindAttribute";

		private const string OverpostingFWLink = "https://go.microsoft.com/fwlink/?LinkId=317598";

		public static string WarningMessage
		{
			get
			{
				return "To protect from overposting attacks, please enable the specific properties you want to bind to.";
			}
		}

		public static string GetBindAttributeIncludeText(ModelMetadata model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			IEnumerable<string> properties = 
				from p in model.Properties
				where !p.IsAssociation
				select p.PropertyName;
			return string.Join(",", properties);
		}

		public static bool IsOverpostingProtectionRequired(CodeType model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			return !model.Attributes.OfType<CodeAttribute>().Any<CodeAttribute>((CodeAttribute a) => string.Equals(a.FullName, "System.Web.Mvc.BindAttribute", StringComparison.Ordinal));
		}
	}
}