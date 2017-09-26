using System;

namespace HMVScaffolder.Mvc.ReadMe
{
	internal abstract class LanguageRules
	{
		public abstract string StatementEnding
		{
			get;
		}

		protected LanguageRules()
		{
		}

		public abstract string CreateDelegateText(string delegateExpression);

		public abstract string CreateFunction(string functionName);

		public abstract string CreateNamespaceImportText(string namespaceName);
	}
}