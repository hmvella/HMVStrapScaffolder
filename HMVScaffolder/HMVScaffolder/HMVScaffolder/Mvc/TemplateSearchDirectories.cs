using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using System;
using System.IO;
using System.Reflection;

namespace HMVScaffolder.Mvc
{
	internal static class TemplateSearchDirectories
	{
		public static string InstalledTemplateRoot
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(typeof(TemplateSearchDirectories).Assembly.Location), "Templates");
			}
		}

		public static string GetProjectTemplateRoot(Project project)
		{
			if (project == null)
			{
				throw new ArgumentNullException("project");
			}
			return Path.Combine(ProjectExtensions.GetFullPath(project), "CodeTemplates");
		}
	}
}