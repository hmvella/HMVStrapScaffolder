using System;
using System.Collections.Generic;
using System.Globalization;

namespace HMVScaffolder.Mvc
{
    internal static class AssemblyVersions
	{
		private const string AssemblyVersionsFile = "Templates\\AssemblyVersions.xml";

		private const string AssemblyElementXPath = "/Assemblies/Assembly";

		public readonly static string MvcAssemblyName;

		public readonly static string MobileServiceAssemblyName;

		public readonly static string WebApiAssemblyName;

		public readonly static string ODataAssemblyName;

		public readonly static Version MvcAssemblyMinVersion;

		public readonly static Version MvcAssemblyMaxVersion;

		public readonly static Version WebApiAssemblyMinVersion;

		public readonly static Version WebApiAssemblyMaxVersion;

		public readonly static Version WebPagesAssemblyVersion;

		public readonly static Version ODataAssemblyMinVersion;

		public readonly static Version ODataAssemblyMaxVersion;

		public readonly static string AsyncEntityFrameworkAssemblyName;

		public readonly static Version AsyncEntityFrameworkMinVersion;

		private static IDictionary<string, string> Versions
		{
			get;
			set;
		}

		static AssemblyVersions()
		{
			AssemblyVersions.MvcAssemblyName = "System.Web.Mvc";
			AssemblyVersions.MobileServiceAssemblyName = "Microsoft.WindowsAzure.Mobile.Service";
			AssemblyVersions.WebApiAssemblyName = "System.Web.Http";
			AssemblyVersions.ODataAssemblyName = "System.Web.Http.OData";
			AssemblyVersions.MvcAssemblyMinVersion = new Version(5, 0, 0);
			AssemblyVersions.MvcAssemblyMaxVersion = new Version(6, 0, 0, 0);
			AssemblyVersions.WebApiAssemblyMinVersion = new Version(5, 0, 0);
			AssemblyVersions.WebApiAssemblyMaxVersion = new Version(6, 0, 0, 0);
			AssemblyVersions.WebPagesAssemblyVersion = new Version(3, 0, 0, 0);
			AssemblyVersions.ODataAssemblyMinVersion = new Version(5, 0, 0);
			AssemblyVersions.ODataAssemblyMaxVersion = new Version(6, 0, 0, 0);
			AssemblyVersions.AsyncEntityFrameworkAssemblyName = "EntityFramework";
			AssemblyVersions.AsyncEntityFrameworkMinVersion = new Version(6, 0, 0);
		}

		public static Version GetLatestAssemblyVersion(string assemblyName)
		{
			string str;
			Version version;
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyReferenceName");
			}
			if (AssemblyVersions.Versions == null)
			{
				AssemblyVersions.Versions = VersionFileReader.GetVersions("Templates\\AssemblyVersions.xml", "/Assemblies/Assembly");
				if (AssemblyVersions.Versions == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The {0} file is missing from the installed template folder.", "Templates\\AssemblyVersions.xml"));
				}
			}
			AssemblyVersions.Versions.TryGetValue(assemblyName, out str);
			if (!Version.TryParse(str, out version))
			{
				return null;
			}
			return version;
		}
	}
}