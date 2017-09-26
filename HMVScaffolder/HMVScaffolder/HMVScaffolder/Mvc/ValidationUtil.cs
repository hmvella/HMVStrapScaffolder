using Microsoft.AspNet.Scaffolding;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HMVScaffolder.Mvc
{
    internal static class ValidationUtil
	{
		private readonly static char[] _invalidCharacters;

		internal static char[] DisplayInvalidCharacters
		{
			get
			{
				return (
					from c in (IEnumerable<char>)ValidationUtil._invalidCharacters
					where c != '\0'
					select c).ToArray<char>();
			}
		}

		static ValidationUtil()
		{
			ValidationUtil._invalidCharacters = ((IEnumerable<char>)(new char[] { '.', '-', '@', '+' })).Concat<char>(Path.GetInvalidFileNameChars()).ToArray<char>();
		}

		public static CodeDomProvider GenerateCodeDomProvider(ProjectLanguage projectLanguage)
		{
			if (projectLanguage == null)
			{
				throw new ArgumentNullException("projectLanguage");
			}
			if (!CodeDomProvider.IsDefinedLanguage(projectLanguage.ToString()))
			{
				throw new InvalidOperationException("The project language is not supported.");

            }
			return CodeDomProvider.CreateProvider(projectLanguage.ToString());
		}

		public static string GetErrorIfInvalidIdentifier(string text, ProjectLanguage projectLanguage)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "The name is invalid because it is empty.";

            }
			if (text.IndexOfAny(ValidationUtil._invalidCharacters) >= 0)
			{
				return string.Format(CultureInfo.CurrentCulture, "The name is not allowed to contain any of these characters: {0}", string.Join<char>(" ", ValidationUtil.DisplayInvalidCharacters));
			}
			if (text.Any<char>((char c) => char.IsWhiteSpace(c)))
			{
				return "The name is invalid because it has white spaces.";

            }
			if (!ValidationUtil.GenerateCodeDomProvider(projectLanguage).IsValidIdentifier(text))
			{
				return "The name is invalid because it is a reserved name.";

            }
			return null;
		}
	}
}