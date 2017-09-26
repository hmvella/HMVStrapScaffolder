using System;

namespace HMVScaffolder.Mvc.ReadMe
{
	internal static class ReadMeFormatting
	{
		public const string Indentation = "    ";

		public readonly static string NewLineAndIndentation;

		static ReadMeFormatting()
		{
			ReadMeFormatting.NewLineAndIndentation = string.Concat(Environment.NewLine, "    ");
		}
	}
}