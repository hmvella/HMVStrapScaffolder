using HMVScaffolder.Mvc.Telemetry;
using HMVScaffolder.Mvc.VisualStudio;
using Microsoft.AspNet.Scaffolding.NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Scaffolding;

namespace HMVScaffolder.Mvc
{
    public abstract class InteractiveScaffolder<TModel, TFramework> : CodeGenerator
    where TModel : ScaffolderModel
    where TFramework : IFrameworkDependency
    {
        private TModel _model;

        private object _viewModel;

        private int waitDialogDelayInSeconds;

        public sealed override IEnumerable<NuGetPackage> Dependencies
        {
            get
            {
                List<NuGetPackage> nuGetPackages = new List<NuGetPackage>();
                this.AddScaffoldDependencies(nuGetPackages);
                return nuGetPackages;
            }
        }

        protected TFramework Framework
        {
            get;
            private set;
        }

        public TModel Model
        {
            get
            {
                if (this._model == null)
                {
                    this._model = this.CreateModel();
                    this.LoadSettings(this._model);
                }
                return this._model;
            }
        }

        protected INuGetRepository Repository
        {
            get;
            private set;
        }

        private IEnumerable<NuGetPackage> RuntimePackages
        {
            get
            {
                List<NuGetPackage> nuGetPackages = new List<NuGetPackage>();
                if (!this.Framework.IsDependencyInstalled(base.Context))
                {
                    TFramework framework = this.Framework;
                    nuGetPackages.AddRange(framework.GetRequiredPackages(base.Context));
                }
                this.AddRuntimePackages(nuGetPackages);
                return nuGetPackages;
            }
        }

        private object ViewModel
        {
            get
            {
                if (this._viewModel == null)
                {
                    this._viewModel = this.CreateViewModel(this.Model);
                }
                return this._viewModel;
            }
        }

        protected IVisualStudioIntegration VisualStudioIntegration
        {
            get;
            private set;
        }

        protected InteractiveScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
        {
            this.Framework = context.Items.GetProperty<TFramework>(typeof(TFramework));
            this.Repository = context.Items.GetProperty<INuGetRepository>(typeof(INuGetRepository));
            this.VisualStudioIntegration = context.Items.GetProperty<IVisualStudioIntegration>(typeof(IVisualStudioIntegration));
        }

        protected internal virtual void AddRuntimePackages(List<NuGetPackage> packages)
        {
        }

        protected internal virtual void AddScaffoldDependencies(List<NuGetPackage> packages)
        {
        }

        protected abstract ValidatingDialogWindow CreateDialog();

        protected abstract TModel CreateModel();

        protected abstract object CreateViewModel(TModel model);

        public override void GenerateCode()
        {
            FrameworkDependencyStatus installNotNeeded = FrameworkDependencyStatus.InstallNotNeeded; //JF
            if (!this.Framework.IsDependencyInstalled(base.Context))
            {
                installNotNeeded = this.Framework.EnsureDependencyInstalled(base.Context);
            }
            else
            {
                base.Context.AddTelemetryData("DependencyScaffolderOptions", (uint)1);
            }
            this.Scaffold();
            if (installNotNeeded.IsNewDependencyInstall)
            {
                this.Framework.UpdateConfiguration(base.Context);
            }
            if (this.Model.OutputFileFullPath != null)
            {
                this.VisualStudioIntegration.Editor.OpenFileInEditor(this.Model.OutputFileFullPath);
            }
            if (installNotNeeded.IsReadmeRequired)
            {
                this.VisualStudioIntegration.Editor.CreateAndOpenReadme(installNotNeeded.ReadmeText);
            }
            this.SaveSettings(this.Model);
            foreach (NuGetPackage runtimePackage in this.RuntimePackages)
            {
                base.Context.Packages.Add(runtimePackage);
            }
        }

        protected string[] GetSearchFolders(string templateFolderName)
        {
            if (templateFolderName == null)
            {
                throw new ArgumentNullException("templateFolderName");
            }
            string[] strArrays = new string[] { Path.Combine(TemplateSearchDirectories.GetProjectTemplateRoot(base.Context.ActiveProject), templateFolderName), Path.Combine(TemplateSearchDirectories.InstalledTemplateRoot, templateFolderName) };
            string[] finalStrArrays = (from p in strArrays
                                       where p.Contains(templateFolderName)
                                       //where Directory.Exists(templateFolderName) JF: edited
                                       select p).ToArray<string>();
            return finalStrArrays;
                
        }

        private void LoadSettings(TModel model)
        {
            IScaffoldingSettings scaffoldingSetting = (object)model as IScaffoldingSettings;
            if (scaffoldingSetting != null && this.VisualStudioIntegration != null)
            {
                IProjectSettings projectSettings = this.VisualStudioIntegration.GetProjectSettings(base.Context.ActiveProject);
                if (projectSettings != null)
                {
                    try
                    {
                        scaffoldingSetting.LoadSettings(projectSettings);
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }
        }

        private void SaveSettings(TModel model)
        {
            IScaffoldingSettings scaffoldingSetting = (object)model as IScaffoldingSettings;
            if (scaffoldingSetting != null && this.VisualStudioIntegration != null)
            {
                IProjectSettings projectSettings = this.VisualStudioIntegration.GetProjectSettings(base.Context.ActiveProject);
                if (projectSettings != null)
                {
                    try
                    {
                        scaffoldingSetting.SaveSettings(projectSettings);
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }
        }

        protected internal abstract void Scaffold();

        private IDisposable ShowProgressDialog(string caption, string message)
        {
            IThreadedWaitDialogService vsShellDialogService = new VsShellDialogService(base.ServiceProvider);
            return vsShellDialogService.ShowProgressDialog(caption, message, this.waitDialogDelayInSeconds);
        }

        public override bool ShowUIAndValidate()
        {
            ValidatingDialogWindow viewModel;
            using (IDisposable disposable = this.ShowProgressDialog("Gathering Information", "Gathering information, please wait..."))
            {
                viewModel = this.CreateDialog();
                viewModel.DataContext = this.ViewModel;
            }
            bool? nullable = viewModel.ShowModal();
            viewModel.DataContext = null;
            bool? nullable1 = nullable;
            if (!nullable1.GetValueOrDefault())
            {
                return false;
            }
            return nullable1.HasValue;
        }

    }
}