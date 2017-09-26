using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.ComponentModelHost;
using NuGet.VisualStudio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HMVScaffolder.Mvc
{
    internal class PackageVersions
	{
		private const string PackageVersionsFile = "Templates\\PackageVersions{0}.xml";

		private const string PackageElementXPath = "/Packages/Package";

		private readonly static Version _latestKnownPackageVersion;

		private static IDictionary<string, IDictionary<string, string>> PackageFileContents
		{
			get;
			set;
		}

		static PackageVersions()
		{
			PackageVersions._latestKnownPackageVersion = new Version(5, 2, 3);
		}

		public PackageVersions()
		{
		}

		internal static IEnumerable<IVsPackageMetadata> GetInstalledPackages(CodeGenerationContext context)
		{
			IVsPackageInstallerServices service = context.ServiceProvider.GetService<IComponentModel, SComponentModel>().GetService<IVsPackageInstallerServices>();
			return service.GetInstalledPackages(context.ActiveProject);
		}

		internal static void GetPackageFileNameForPackage(CodeGenerationContext context, IEnumerable<IVsPackageMetadata> installedPackages, string packageId, string assemblyReferenceName, Version minSupportedAssemblyReferenceVersion, ref string packageFileName)
		{
			Version assemblyVersion;
			if (ProjectReferences.IsAssemblyReferenced(context.ActiveProject, assemblyReferenceName))
			{
				IVsPackageMetadata variable = (
					from package in installedPackages
					where string.Equals(packageId, package.Id, StringComparison.OrdinalIgnoreCase)
					select package).FirstOrDefault<IVsPackageMetadata>();
				if (variable == null || !SemanticVersionParser.TryParse(variable.VersionString, out assemblyVersion))
				{
					assemblyVersion = ProjectReferences.GetAssemblyVersion(context.ActiveProject, assemblyReferenceName);
				}
				if (assemblyVersion >= minSupportedAssemblyReferenceVersion && assemblyVersion <= PackageVersions._latestKnownPackageVersion)
				{
					packageFileName = PackageVersions.GetPackageVersionsFileName(assemblyVersion);
				}
			}
		}

		private static string GetPackageReferenceFileName(CodeGenerationContext context)
		{
			IEnumerable<IVsPackageMetadata> installedPackages = PackageVersions.GetInstalledPackages(context);
			string packageVersionsFileName = PackageVersions.GetPackageVersionsFileName(PackageVersions._latestKnownPackageVersion);
			PackageVersions.GetPackageFileNameForPackage(context, installedPackages, NuGetPackages.MvcNuGetPackageId, AssemblyVersions.MvcAssemblyName, AssemblyVersions.MvcAssemblyMinVersion, ref packageVersionsFileName);
			PackageVersions.GetPackageFileNameForPackage(context, installedPackages, NuGetPackages.WebApiNuGetPackageId, AssemblyVersions.WebApiAssemblyName, AssemblyVersions.WebApiAssemblyMinVersion, ref packageVersionsFileName);
			return packageVersionsFileName;
		}

		public static IDictionary<string, string> GetPackageVersions(CodeGenerationContext context)
		{
			IDictionary<string, string> versions;
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			string packageReferenceFileName = PackageVersions.GetPackageReferenceFileName(context);
			if (PackageVersions.PackageFileContents != null)
			{
				PackageVersions.PackageFileContents.TryGetValue(packageReferenceFileName, out versions);
				if (versions != null)
				{
					return versions;
				}
			}
			else
			{
				PackageVersions.PackageFileContents = new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
			}
			versions = VersionFileReader.GetVersions(packageReferenceFileName, "/Packages/Package");
			if (versions == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The {0} file is missing from the installed template folder.", packageReferenceFileName));

            }
			PackageVersions.PackageFileContents[packageReferenceFileName] = versions;
			return versions;
		}

		private static string GetPackageVersionsFileName(Version version)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			object[] major = new object[] { version.Major, ".", version.Minor, ".", version.Build };
			return string.Format(invariantCulture, "Templates\\PackageVersions{0}.xml", string.Concat(major));
		}
	}
}