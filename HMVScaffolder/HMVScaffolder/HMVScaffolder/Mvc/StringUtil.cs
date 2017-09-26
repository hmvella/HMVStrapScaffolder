using System;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	internal static class StringUtil
	{
		internal static string ToLowerInvariantFirstChar(this string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input == string.Empty)
			{
				return input;
			}
			return string.Concat(input.Substring(0, 1).ToLowerInvariant(), input.Substring(1));
		}
	}
}