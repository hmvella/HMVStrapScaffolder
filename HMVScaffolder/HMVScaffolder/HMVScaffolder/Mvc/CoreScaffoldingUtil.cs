using System;
using System.IO;
using System.Text;

namespace HMVScaffolder.Mvc
{
	internal static class CoreScaffoldingUtil
	{
		private readonly static string _pathSeparator;

		static CoreScaffoldingUtil()
		{
			CoreScaffoldingUtil._pathSeparator = Path.DirectorySeparatorChar.ToString();
		}

		public static string MakeRelativePath(string fullPath, string basePath)
		{
			string str = basePath;
			string str1 = fullPath;
			StringBuilder stringBuilder = new StringBuilder();
			if (!str.EndsWith(CoreScaffoldingUtil._pathSeparator, StringComparison.OrdinalIgnoreCase))
			{
				str = string.Concat(str, CoreScaffoldingUtil._pathSeparator);
			}
			while (!string.IsNullOrEmpty(str))
			{
				if (str1.StartsWith(str, StringComparison.OrdinalIgnoreCase))
				{
					stringBuilder.Append(fullPath.Remove(0, str.Length));
					if (string.Equals(stringBuilder.ToString(), CoreScaffoldingUtil._pathSeparator, StringComparison.OrdinalIgnoreCase))
					{
						stringBuilder.Clear();
					}
					return stringBuilder.ToString();
				}
				str = str.Remove(str.Length - 1);
				int num = str.LastIndexOf(CoreScaffoldingUtil._pathSeparator, StringComparison.OrdinalIgnoreCase);
				if (-1 == num)
				{
					return fullPath;
				}
				str = str.Remove(num + 1);
				stringBuilder.Append("..");
				stringBuilder.Append(CoreScaffoldingUtil._pathSeparator);
			}
			return fullPath;
		}
	}
}