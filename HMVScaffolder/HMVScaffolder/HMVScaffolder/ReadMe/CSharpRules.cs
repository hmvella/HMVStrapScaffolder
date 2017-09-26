using System;

namespace HMVScaffolder.Mvc.ReadMe
{
	internal class CSharpRules : LanguageRules
	{
		private const string ImportKeyword = "using";

		public override string StatementEnding
		{
			get
			{
				return ";";
			}
		}

		public CSharpRules()
		{
		}

		public override string CreateDelegateText(string delegateExpression)
		{
			return delegateExpression;
		}

		public override string CreateFunction(string functionName)
		{
			string[] newLineAndIndentation = new string[] { ReadMeFormatting.NewLineAndIndentation, "protected void ", functionName, "()", ReadMeFormatting.NewLineAndIndentation, "{", ReadMeFormatting.NewLineAndIndentation, "}" };
			return string.Concat(newLineAndIndentation);
		}

		public override string CreateNamespaceImportText(string namespaceName)
		{
			return string.Concat("    using ", namespaceName, this.StatementEnding);
		}
	}
}