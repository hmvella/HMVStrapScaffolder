using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    public class MvcAreaScaffolder : InteractiveScaffolder<MvcAreaScaffolderModel, MvcFrameworkDependency>
    {
        private const string TemplateName = "Area";

        private const string TemplateFolderName = "MvcArea";

        public override IEnumerable<string> TemplateFolders
        {
            get
            {
                return base.GetSearchFolders("MvcArea");
            }
        }

        public MvcAreaScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
        {
        }

        ////////protected override ValidatingDialogWindow CreateDialog()
        ////////{
        ////////    return new AreaScaffolderDialog();
        ////////}

        protected override MvcAreaScaffolderModel CreateModel()
        {
            return new MvcAreaScaffolderModel(base.Context);
        }

        protected override object CreateViewModel(MvcAreaScaffolderModel model)
        {
            //return new MvcAreaScaffolderViewModel(model); JF replaced this
            return new object();
        }

        private void CreateWebConfigFile(string areaRelativePath)
        {
            if (areaRelativePath == null)
            {
                throw new ArgumentNullException("areaRelativePath");
            }
            bool flag = AddDependencyUtil.IsBundleConfigPresent(base.Context);
            IDictionary<string, object> templateParametersForWebConfig = AddDependencyUtil.GetTemplateParametersForWebConfig(flag, base.Context);
            string str = Path.Combine(areaRelativePath, "Views", "web");
            base.AddFileFromTemplate(base.Context.ActiveProject, str, "web", templateParametersForWebConfig, true);
        }

        private void GenerateAreaRegistrationCode(string areaName, string areaClassName, string areaNamespace, string outputPath)
        {
            if (areaNamespace == null)
            {
                throw new ArgumentNullException("areaNamespace");
            }
            if (outputPath == null)
            {
                throw new ArgumentNullException("outputPath");
            }
            IDictionary<string, object> strs = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "AreaName", areaName },
                { "AreaClassName", areaClassName },
                { "Namespace", areaNamespace }
            };
            base.AddFileFromTemplate(base.Context.ActiveProject, outputPath, "Area", strs, true);
        }

        protected internal override void Scaffold()
        {
            string areaName = base.Model.AreaName;
            Project activeProject = base.Context.ActiveProject;
            string[] strArrays = new string[] { "Areas" };
            base.AddFolder(activeProject, Path.Combine(strArrays));
            ProjectItem projectItem = base.AddFolder(base.Context.ActiveProject, Path.Combine("Areas", areaName));
            base.AddFolder(base.Context.ActiveProject, Path.Combine("Areas", areaName, "Models"));
            base.AddFolder(base.Context.ActiveProject, Path.Combine("Areas", areaName, "Controllers"));
            base.AddFolder(base.Context.ActiveProject, Path.Combine("Areas", areaName, "Views"));
            base.AddFolder(base.Context.ActiveProject, Path.Combine("Areas", areaName, "Views", "Shared"));
            string str = string.Concat(base.Model.AreaName, MvcProjectUtil.AreaRegistration);
            string defaultNamespace = ProjectExtensions.GetDefaultNamespace(projectItem);
            string str1 = Path.Combine(base.Model.AreaRelativePath, string.Concat(areaName, MvcProjectUtil.AreaRegistration));
            this.GenerateAreaRegistrationCode(areaName, str, defaultNamespace, str1);
            this.CreateWebConfigFile(base.Model.AreaRelativePath);
        }
    }
}