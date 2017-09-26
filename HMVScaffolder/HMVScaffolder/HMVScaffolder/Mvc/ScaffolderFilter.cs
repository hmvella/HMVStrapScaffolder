using Microsoft.AspNet.Scaffolding;
using System;
using System.Runtime.Versioning;
using VSLangProj;

namespace HMVScaffolder.Mvc
{
	internal static class ScaffolderFilter
	{
		public static bool DisplayMvcScaffolders(CodeGenerationContext codeGenerationContext)
		{
			if (!ScaffolderFilter.IsApplicableProject(codeGenerationContext))
			{
				return false;
			}
			ScaffolderFilter.ReferenceDetails referenceDetail = ScaffolderFilter.IsValidProjectReference(codeGenerationContext, AssemblyVersions.MvcAssemblyName, AssemblyVersions.MvcAssemblyMinVersion, AssemblyVersions.MvcAssemblyMaxVersion);
			ScaffolderFilter.ReferenceDetails referenceDetail1 = ScaffolderFilter.IsValidProjectReference(codeGenerationContext, AssemblyVersions.MobileServiceAssemblyName, new Version(1, 0, 0, 0), new Version(2147483647, 0, 0, 0));
			if (referenceDetail == ScaffolderFilter.ReferenceDetails.ReferenceVersionSupported)
			{
				return true;
			}
			if (referenceDetail != ScaffolderFilter.ReferenceDetails.ReferenceDoesNotExist)
			{
				return false;
			}
			return referenceDetail1 == ScaffolderFilter.ReferenceDetails.ReferenceDoesNotExist;
		}

		public static bool DisplayODataScaffolders(CodeGenerationContext codeGenerationContext)
		{
			return ScaffolderFilter.DisplayScaffolders(codeGenerationContext, AssemblyVersions.ODataAssemblyName, AssemblyVersions.ODataAssemblyMinVersion, AssemblyVersions.ODataAssemblyMaxVersion);
		}

		private static bool DisplayScaffolders(CodeGenerationContext context, string projectReferenceName, Version minVersion, Version maxExcludedVersion)
		{
			if (!ScaffolderFilter.IsApplicableProject(context))
			{
				return false;
			}
			return ScaffolderFilter.IsValidProjectReference(context, projectReferenceName, minVersion, maxExcludedVersion) != ScaffolderFilter.ReferenceDetails.ReferenceVersionNotSupported;
		}

		public static bool DisplayWebApiScaffolders(CodeGenerationContext codeGenerationContext)
		{
			return ScaffolderFilter.DisplayScaffolders(codeGenerationContext, AssemblyVersions.WebApiAssemblyName, AssemblyVersions.WebApiAssemblyMinVersion, AssemblyVersions.WebApiAssemblyMaxVersion);
		}

		private static bool IsApplicableProject(CodeGenerationContext context)
		{
			FrameworkName targetFramework;
			bool flag;
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if ((object)ProjectLanguage.CSharp == (object)ProjectExtensions.GetCodeLanguage(context.ActiveProject) || (object)ProjectLanguage.VisualBasic == (object)ProjectExtensions.GetCodeLanguage(context.ActiveProject))
			{
				targetFramework = null;
				try
				{
					targetFramework = ProjectExtensions.GetTargetFramework(context.ActiveProject);
					goto Label0;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
			return false;
		Label0:
			if (targetFramework != null && targetFramework.Identifier == ".NETFramework" && targetFramework.Version >= new Version(4, 5))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private static ScaffolderFilter.ReferenceDetails IsValidProjectReference(CodeGenerationContext context, string projectReferenceName, Version minVersion, Version maxExcludedVersion)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			Reference assemblyReference = ProjectExtensions.GetAssemblyReference(context.ActiveProject, projectReferenceName);
			if (assemblyReference == null)
			{
				return ScaffolderFilter.ReferenceDetails.ReferenceDoesNotExist;
			}
			Version version = null;
			if (Version.TryParse(assemblyReference.Version, out version) && version.Major >= minVersion.Major && version.Major < maxExcludedVersion.Major)
			{
				return ScaffolderFilter.ReferenceDetails.ReferenceVersionSupported;
			}
			return ScaffolderFilter.ReferenceDetails.ReferenceVersionNotSupported;
		}

		private enum ReferenceDetails
		{
			ReferenceDoesNotExist,
			ReferenceVersionNotSupported,
			ReferenceVersionSupported
		}
	}
}