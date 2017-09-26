using Microsoft.AspNet.Scaffolding;
using System;
using System.IO;

namespace HMVScaffolder.Mvc
{
    public static class MvcProjectUtil
	{
		public readonly static string AreaRegistration;

		public readonly static string ControllerSuffix;

		public readonly static string DataContextSuffix;

		public readonly static string ControllerName;

		public readonly static string PartialViewName;

		public readonly static string ViewName;

		public readonly static string DefaultNamespace;

		public readonly static string PathSeparator;

		public readonly static string ControllerNameRegex;

		public readonly static string DataContextNameRegex;

		static MvcProjectUtil()
		{
			AreaRegistration = "AreaRegistration";
			ControllerSuffix = "Controller";
			DataContextSuffix = "Context";
			ControllerName = "Default{0}Controller";
			PartialViewName = "Partial{0}";
            ViewName = "View{0}";
			DefaultNamespace = "DefaultNamespace";
			PathSeparator = Path.DirectorySeparatorChar.ToString();
			ControllerNameRegex = string.Concat("\\b([_\\d\\w]*)", MvcProjectUtil.ControllerSuffix, "$");
			DataContextNameRegex = string.Concat("\\b([_\\d\\w]*)", MvcProjectUtil.DataContextSuffix, "$");
		}

		public static bool EndsWithController(string name)
		{
			return name.EndsWith(MvcProjectUtil.ControllerSuffix, StringComparison.Ordinal);
		}

		public static string EnsureTrailingBackSlash(string str)
		{
			if (str != null && !str.EndsWith(MvcProjectUtil.PathSeparator, StringComparison.Ordinal))
			{
				str = string.Concat(str, MvcProjectUtil.PathSeparator);
			}
			return str;
		}

		public static string GetDefaultModelsNamespace(string projectDefaultNamespace)
		{
			if (string.IsNullOrEmpty(projectDefaultNamespace))
			{
				return "Models";
			}
			return string.Concat(projectDefaultNamespace, ".Models");
		}

		internal static string GetViewFileExtension(ProjectLanguage language)
		{
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}
			if ((object)language == (object)ProjectLanguage.CSharp)
			{
				return "cshtml";
			}
			if ((object)language != (object)ProjectLanguage.VisualBasic)
			{
				throw new InvalidOperationException("The project language is not supported.");
			}
			return "vbhtml";
		}

		public static string StripControllerName(string fullControllerName)
		{
			if (string.IsNullOrEmpty(fullControllerName))
			{
				return fullControllerName;
			}
			if (!MvcProjectUtil.EndsWithController(fullControllerName))
			{
				return fullControllerName;
			}
			return fullControllerName.Substring(0, fullControllerName.Length - MvcProjectUtil.ControllerSuffix.Length);
		}
	}
}