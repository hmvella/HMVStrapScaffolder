using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	internal static class ProjectSettingsExtensions
	{
		public static void SetBool(this IProjectSettings settings, string key, bool value)
		{
			settings[key] = value.ToString(CultureInfo.InvariantCulture);
		}

		public static bool TryGetBool(this IProjectSettings settings, string key, out bool value)
		{
			bool flag;
			string item = settings[key];
			if (item != null && bool.TryParse(item, out flag))
			{
				value = flag;
				return true;
			}
			value = false;
			return false;
		}

		public static bool TryGetDouble(this IProjectSettings settings, string key, out double value)
		{
			double num;
			string item = settings[key];
			if (item != null && double.TryParse(item, out num))
			{
				value = num;
				return true;
			}
			value = 0;
			return false;
		}

		public static bool TryGetString(this IProjectSettings settings, string key, out string value)
		{
			value = settings[key];
			return value != null;
		}
	}
}