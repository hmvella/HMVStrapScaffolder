using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using System;
using System.Collections.Generic;
using System.IO;

namespace HMVScaffolder.Mvc
{
    internal static class AddDependencyUtil
    {
        private const string OptimizationNamespace = "System.Web.Optimization";

        public static IDictionary<string, object> GetTemplateParametersForWebConfig(bool isBundleConfigPresent, CodeGenerationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            string defaultNamespace = ProjectExtensions.GetDefaultNamespace(context.ActiveProject);
            Dictionary<string, object> strs = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            HashSet<string> strs1 = new HashSet<string>(StringComparer.Ordinal);
            if (isBundleConfigPresent)
            {
                strs1.Add("System.Web.Optimization");
            }
            strs1.Add(defaultNamespace);
            strs.Add("RequiredNamespaces", strs1);
            Version assemblyVersion = ProjectReferences.GetAssemblyVersion(context.ActiveProject, AssemblyVersions.MvcAssemblyName);
            strs["MvcVersion"] = assemblyVersion;
            return strs;
        }

        public static bool IsBundleConfigPresent(CodeGenerationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            Project activeProject = context.ActiveProject;
            string defaultNamespace = ProjectExtensions.GetDefaultNamespace(activeProject);
            if (context.ServiceProvider.GetService<ICodeTypeService>().GetCodeType(activeProject, string.Concat(defaultNamespace, ".BundleConfig")) != null)
            {
                return true;
            }
            string str = string.Concat("BundleConfig.", ProjectExtensions.GetCodeLanguage(activeProject).CodeFileExtension);
            return File.Exists(Path.Combine(ProjectExtensions.GetFullPath(activeProject), "App_Start", str));
        }

        public static bool IsSearchTextPresent(string fileFullPath, string searchText)
        {
            bool flag;
            if (fileFullPath == null)
            {
                throw new ArgumentNullException("fileFullPath");
            }
            if (searchText == null)
            {
                throw new ArgumentNullException("searchText");
            }
            try
            {
                flag = File.ReadAllText(fileFullPath).Contains(searchText);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }
}