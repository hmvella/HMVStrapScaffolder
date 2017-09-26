using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using System;
using System.Collections.Generic;
using System.IO;

namespace HMVScaffolder.Mvc
{
    public abstract class ControllerScaffolder<TFramework> : InteractiveScaffolder<ControllerScaffolderModel, TFramework>
    where TFramework : IFrameworkDependency
    {
        private const string TemplateName = "Controller";

        protected internal abstract string TemplateFolderName
        {
            get;
        }

        public override IEnumerable<string> TemplateFolders
        {
            get
            {
                return base.GetSearchFolders(this.TemplateFolderName);
            }
        }

        protected ControllerScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
        {
        }

        protected virtual void AddTemplateParameters(IDictionary<string, object> templateParameters)
        {
            if (templateParameters == null)
            {
                throw new ArgumentNullException("templateParameters");
            }
            if (string.IsNullOrEmpty(base.Model.ControllerName))
            {
                throw new InvalidOperationException("The controller name is invalid.");
            }
            templateParameters.Add("ControllerName", base.Model.ControllerName);
            templateParameters.Add("Namespace", base.Model.ControllerNamespace);
            templateParameters.Add("ControllerRootName", base.Model.ControllerRootName);
            templateParameters.Add("AreaName", base.Model.AreaName ?? string.Empty);
        }

        protected override ValidatingDialogWindow CreateDialog()
        {
            return new ControllerScaffolderDialog();
        }

        private static SortedSet<string> CreateHashSetBasedOnCodeLanguage(ProjectLanguage projectLanguage)
        {
            if ((object)projectLanguage == (object)ProjectLanguage.CSharp)
            {
                return new SortedSet<string>(StringComparer.Ordinal);
            }
            if ((object)projectLanguage != (object)ProjectLanguage.VisualBasic)
            {
                throw new InvalidOperationException("The project language is not supported.");
            }
            return new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        protected override ControllerScaffolderModel CreateModel()
        {
            ControllerScaffolderModel controllerScaffolderModel = new ControllerScaffolderModel(base.Context);
            controllerScaffolderModel.ControllerName = controllerScaffolderModel.GetGeneratedName(MvcProjectUtil.ControllerName, controllerScaffolderModel.CodeFileExtension);

            this.OnModelCreated(controllerScaffolderModel);
            return controllerScaffolderModel;
        }

        protected override object CreateViewModel(ControllerScaffolderModel model)
        {
            return new MvcControllerScaffolderViewModel(model);
        }

        protected void GenerateController(IDictionary<string, object> templateParameters)
        {
            if (templateParameters == null)
            {
                throw new ArgumentNullException("templateParameters");
            }
            this.AddTemplateParameters(templateParameters);
            if (base.Model.IsViewFolderRequired)
            {
                string str = Path.Combine(base.Model.AreaRelativePath, "Views", base.Model.ControllerRootName);
                base.AddFolder(base.Context.ActiveProject, str);
            }
            base.Model.SelectionRelativePath = "Controllers";
            string str1 = Path.Combine(base.Model.SelectionRelativePath, base.Model.ControllerName);
            base.AddFileFromTemplate(base.Context.ActiveProject, str1, "Controller", templateParameters, !base.Model.IsOverwritingFiles);
        }

        protected HashSet<string> GetRequiredNamespaces(IEnumerable<CodeType> codeTypes)
        {
            string fullName;
            if (codeTypes == null)
            {
                throw new ArgumentNullException("codeTypes");
            }
            SortedSet<string> strs = ControllerScaffolder<TFramework>.CreateHashSetBasedOnCodeLanguage(ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject));
            strs.Add(base.Model.ControllerNamespace);
            foreach (CodeType codeType in codeTypes)
            {
                if (codeType.Namespace == null)
                {
                    fullName = null;
                }
                else
                {
                    fullName = codeType.Namespace.FullName;
                }
                string str = fullName;
                if (string.IsNullOrEmpty(str))
                {
                    if (string.Equals(codeType.FullName, codeType.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    int num = codeType.FullName.LastIndexOf(string.Concat(".", codeType.Name), StringComparison.Ordinal);
                    string str1 = codeType.FullName.Remove(num);
                    strs.Add(str1);
                }
                else
                {
                    strs.Add(str);
                }
            }
            strs.Remove(base.Model.ControllerNamespace);
            return new HashSet<string>(strs);
        }

        protected virtual void OnModelCreated(ControllerScaffolderModel model)
        {
        }

        protected internal override void Scaffold()
        {
            try
            {
                this.GenerateController(new Dictionary<string, object>(StringComparer.Ordinal));
            }
            finally
            {
                base.Framework.RecordControllerTelemetryOptions(base.Context, base.Model);
            }
        }
    }
}