using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HMVScaffolder.Mvc
{
	[Export(typeof(IVisualStudioIntegration))]
	internal class VisualStudioIntegration : IVisualStudioIntegration
	{
		public IComponentModel ComponentModel
		{
			get
			{
				return (IComponentModel)this.ServiceProvider.GetService(typeof(SComponentModel));
			}
		}

		public IEditorIntegration Editor
		{
			get
			{
				return JustDecompileGenerated_Editor();
			}
			set
			{
				JustDecompileGenerated_set_Editor(value);
			}
		}

		private IEditorIntegration JustDecompileGenerated_Editor_k__BackingField;

		public IEditorIntegration JustDecompileGenerated_Editor()
		{
			return this.JustDecompileGenerated_Editor_k__BackingField;
		}

		private void JustDecompileGenerated_set_Editor(IEditorIntegration value)
		{
			this.JustDecompileGenerated_Editor_k__BackingField = value;
		}

		public Microsoft.VisualStudio.OLE.Interop.IServiceProvider OleServiceProvider
		{
			get
			{
				return (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)this.ServiceProvider.GetService(typeof(SDTE));
			}
		}

		[Import(typeof(SVsServiceProvider))]
		public System.IServiceProvider ServiceProvider
		{
			get
			{
				return JustDecompileGenerated_ServiceProvider();
			}
			set
			{
				JustDecompileGenerated_set_ServiceProvider(value);
			}
		}

		private System.IServiceProvider JustDecompileGenerated_ServiceProvider_k__BackingField;

		public System.IServiceProvider JustDecompileGenerated_ServiceProvider()
		{
			return this.JustDecompileGenerated_ServiceProvider_k__BackingField;
		}

		private void JustDecompileGenerated_set_ServiceProvider(System.IServiceProvider value)
		{
			this.JustDecompileGenerated_ServiceProvider_k__BackingField = value;
		}

		public VisualStudioIntegration()
		{
			this.Editor = new EditorIntegration(this);
		}

		private IVsBuildPropertyStorage GetBuildPropertyStorage(Project project)
		{
			IVsHierarchy vsHierarchy;
			IVsSolution service = (IVsSolution)this.ServiceProvider.GetService(typeof(SVsSolution));
			int projectOfUniqueName = service.GetProjectOfUniqueName(project.FullName, out vsHierarchy);
			Marshal.ThrowExceptionForHR(projectOfUniqueName);
			return vsHierarchy as IVsBuildPropertyStorage;
		}

		public IProjectSettings GetProjectSettings(Project project)
		{
			if (project == null)
			{
				throw new ArgumentNullException("project");
			}
			IVsBuildPropertyStorage buildPropertyStorage = this.GetBuildPropertyStorage(project);
			if (buildPropertyStorage == null)
			{
				return null;
			}
			return new ProjectSettings(buildPropertyStorage);
		}

		public void ShowErrorMessage(string caption, string message)
		{
			if (caption == null)
			{
				throw new ArgumentNullException("caption");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			VsShellUtilities.ShowMessageBox(this.ServiceProvider, message, caption, OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
		}
	}
}