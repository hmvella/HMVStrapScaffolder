using System;

namespace HMVScaffolder.Mvc
{
	internal static class ScaffolderVersions
	{
		public readonly static Version MvcScaffolderVersion;

		public readonly static Version WebApiScaffolderVersion;

		static ScaffolderVersions()
		{
			ScaffolderVersions.MvcScaffolderVersion = new Version(5, 0, 0, 0);
			ScaffolderVersions.WebApiScaffolderVersion = new Version(2, 0, 0, 0);
		}
	}
}