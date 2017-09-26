using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using System;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	internal static class ProjectItemExtensions
	{
		public static string GetProjectRelativePath(this ProjectItem projectItem)
		{
			Project containingProject = projectItem.ContainingProject;
			string str = null;
			string fullPath = ProjectExtensions.GetFullPath(containingProject);
			fullPath = MvcProjectUtil.EnsureTrailingBackSlash(fullPath);
			string fullPath1 = ProjectExtensions.GetFullPath(projectItem);
			if (!string.IsNullOrEmpty(fullPath) && !string.IsNullOrEmpty(fullPath1))
			{
				str = CoreScaffoldingUtil.MakeRelativePath(fullPath1, fullPath);
			}
			return str;
		}
	}
}