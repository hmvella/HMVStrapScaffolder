using System;

namespace HMVScaffolder.Mvc.ReadMe
{
	internal class VisualBasicRules : LanguageRules
	{
		private const string ImportKeyword = "Imports";

		public override string StatementEnding
		{
			get
			{
				return string.Empty;
			}
		}

		public VisualBasicRules()
		{
		}

		public override string CreateDelegateText(string delegateExpression)
		{
			return string.Concat("AddressOf ", delegateExpression);
		}

		public override string CreateFunction(string functionName)
		{
			string[] newLineAndIndentation = new string[] { ReadMeFormatting.NewLineAndIndentation, "protected Sub ", functionName, "()", ReadMeFormatting.NewLineAndIndentation, string.Empty, ReadMeFormatting.NewLineAndIndentation, "End Sub" };
			return string.Concat(newLineAndIndentation);
		}

		public override string CreateNamespaceImportText(string namespaceName)
		{
			return string.Concat("    Imports ", namespaceName, this.StatementEnding);
		}
	}
}