using System;
using System.Text.RegularExpressions;

namespace HMVScaffolder.Mvc
{
	internal static class SemanticVersionParser
	{
		private const RegexOptions Flags = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

		private readonly static Regex _semanticVersionRegex;

		static SemanticVersionParser()
		{
			SemanticVersionParser._semanticVersionRegex = new Regex("^(?<Version>\\d+(\\s*\\.\\s*\\d+){0,3})(?<Release>-[a-z][0-9a-z-]*)?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
		}

		internal static bool TryParse(string versionString, out Version version)
		{
			version = null;
			if (string.IsNullOrWhiteSpace(versionString))
			{
				return false;
			}
			Match match = SemanticVersionParser._semanticVersionRegex.Match(versionString.Trim());
			if (match.Success && Version.TryParse(match.Groups["Version"].Value, out version))
			{
				return true;
			}
			return false;
		}
	}
}