using EnvDTE;
using EnvDTE80;
using System;
using System.Collections;

namespace HMVScaffolder.Mvc
{
	internal static class CodeTypeFilter
	{
		private static bool IsImportPresentUnderNamespace(CodeElement codeElement, string productNamespace)
		{
			bool flag;
			IEnumerator enumerator = codeElement.Children.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (!CodeTypeFilter.IsNamespaceImportPresent((CodeElement)enumerator.Current, productNamespace))
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return flag;
		}

		private static bool IsNamespaceImportPresent(CodeElement codeElement, string productNamespace)
		{
			if (!codeElement.Kind.Equals(vsCMElement.vsCMElementImportStmt))
			{
				return false;
			}
			return string.Equals(((CodeImport)codeElement).Namespace, productNamespace, StringComparison.OrdinalIgnoreCase);
		}

		public static bool IsProductNamespaceImported(CodeType codeType, string productNamespace)
		{
			bool flag;
			if (codeType == null)
			{
				throw new ArgumentNullException("codeType");
			}
			if (productNamespace == null)
			{
				throw new ArgumentNullException("productNamespace");
			}
			FileCodeModel fileCodeModel = codeType.ProjectItem.FileCodeModel;
			if (fileCodeModel != null)
			{
				IEnumerator enumerator = fileCodeModel.CodeElements.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						CodeElement current = (CodeElement)enumerator.Current;
						if (!CodeTypeFilter.IsNamespaceImportPresent(current, productNamespace))
						{
							if (!current.Kind.Equals(vsCMElement.vsCMElementNamespace) || !CodeTypeFilter.IsImportPresentUnderNamespace(current, productNamespace))
							{
								continue;
							}
							flag = true;
							return flag;
						}
						else
						{
							flag = true;
							return flag;
						}
					}
					return false;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				return flag;
			}
			return false;
		}
	}
}