using HMVScaffolder.Mvc;
using Microsoft.AspNet.Scaffolding.Mvc.Configuration;
using Microsoft.AspNet.Scaffolding.Mvc.Telemetry;
using Microsoft.AspNet.Scaffolding.Mvc.VisualStudio;
using Microsoft.AspNet.Scaffolding.NuGet;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    public class MvcDependencyScaffolder : CodeGenerator
	{
		private const string DependencyInstallerKey = "DependencyInstaller";

		private const string PackagesKey = "Packages";

		private INuGetRepository Repository
		{
			get;
			set;
		}

		private IEnumerable<NuGetPackage> RuntimePackages
		{
			get
			{
				string[] property = base.Context.Items.GetProperty<string[]>("Packages");
				return (
					from id in property
					select this.Repository.GetPackage(base.Context, id)).ToList<NuGetPackage>();
			}
		}

		private IVisualStudioIntegration VisualStudioIntegration
		{
			get;
			set;
		}

		public MvcDependencyScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
			this.Repository = context.Items.GetProperty<INuGetRepository>(typeof(INuGetRepository));
			this.VisualStudioIntegration = context.Items.GetProperty<IVisualStudioIntegration>(typeof(IVisualStudioIntegration));
		}

		////////private ValidatingDialogWindow CreateDialog()
		////////{
		////////	return new DependencyScaffolderDialog();
		////////}

		public override void GenerateCode()
		{
			MvcDependencyInstaller property = base.Context.Items.GetProperty<MvcDependencyInstaller>("DependencyInstaller");
			FrameworkDependencyStatus frameworkDependencyStatu = property.Install();
			MvcConfigurationEditor mvcConfigurationEditor = new MvcConfigurationEditor(this.VisualStudioIntegration);
			mvcConfigurationEditor.Edit(base.Context.ActiveProject);
			if (frameworkDependencyStatu.IsReadmeRequired)
			{
				this.VisualStudioIntegration.Editor.CreateAndOpenReadme(frameworkDependencyStatu.ReadmeText);
			}
			foreach (NuGetPackage runtimePackage in this.RuntimePackages)
			{
				base.Context.Packages.Add(runtimePackage);
			}
		}

		public override bool ShowUIAndValidate()
		{
            ////////ValidatingDialogWindow validatingDialogWindow = this.CreateDialog();
            ////////MvcDependencyScaffolderViewModel mvcDependencyScaffolderViewModel = new MvcDependencyScaffolderViewModel(new MvcDependencyScaffolderModel(base.Context()));
            ////////validatingDialogWindow.DataContext = mvcDependencyScaffolderViewModel;
            ////////bool? nullable = validatingDialogWindow.ShowModal();
            ////////if ((!nullable.GetValueOrDefault() ? true : !nullable.HasValue))
            ////////{
            ////////	return false;
            ////////}
            ////////if (!mvcDependencyScaffolderViewModel.IsFullSelected)
            ////////{
            ////////	base.Context().Items().set_Item("DependencyInstaller", new MvcMinimalDependencyInstaller(base.Context(), this.VisualStudioIntegration));
            ////////	base.Context().Items().set_Item("Packages", NuGetPackages.MvcMinimalPackageSet);
            ////////	base.Context().AddTelemetryData("DependencyScaffolderOptions", (uint)2);
            ////////}
            ////////else
            ////////{
            ////////	base.Context().Items().set_Item("DependencyInstaller", new MvcFullDependencyInstaller(base.Context(), this.VisualStudioIntegration, this.Repository));
            ////////	base.Context().Items().set_Item("Packages", NuGetPackages.MvcFullPackageSet);
            ////////	base.Context().AddTelemetryData("DependencyScaffolderOptions", (uint)3);
            ////////}
            ////////return true;


            //////// Bring up the selection dialog and allow user to select a model type
            //////SelectModelWindow window = new SelectModelWindow(_viewModel);
            //////bool? showDialog = window.ShowDialog();
            //////return showDialog ?? false;

            return true; //JF no views and thus no windows to show
        }
	}
}