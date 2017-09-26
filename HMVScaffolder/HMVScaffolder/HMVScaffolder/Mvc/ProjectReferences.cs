using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using System;
using VSLangProj;

namespace HMVScaffolder.Mvc
{
	internal static class ProjectReferences
	{
		public static Version GetAssemblyVersion(Project activeProject, string assemblyName)
		{
			if (activeProject == null)
			{
				throw new ArgumentNullException("activeProject");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
            Reference assemblyReference = ProjectExtensions.GetAssemblyReference(activeProject, assemblyName);
			if (assemblyReference != null && assemblyReference.Version != null)
			{
				return new Version(assemblyReference.Version);
			}
			return AssemblyVersions.GetLatestAssemblyVersion(assemblyName);
		}

		public static bool IsAssemblyReferenced(Project activeProject, string assemblyReferenceName)
		{
			if (activeProject == null)
			{
				throw new ArgumentNullException("activeProject");
			}
			if (assemblyReferenceName == null)
			{
				throw new ArgumentNullException("assemblyReferenceName");
			}
			return ProjectExtensions.GetAssemblyReference(activeProject, assemblyReferenceName) != null;
		}
	}
}