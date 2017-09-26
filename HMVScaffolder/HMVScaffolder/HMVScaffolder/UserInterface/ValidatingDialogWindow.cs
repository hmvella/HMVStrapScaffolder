using EnvDTE;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel;
using System.Windows;

namespace HMVScaffolder.Mvc
{
    public class ValidatingDialogWindow : DialogWindow
    {
        private readonly static DependencyProperty DialogHostProperty;

        public IDialogHost DialogHost
        {
            get
            {
                return (IDialogHost)base.GetValue(ValidatingDialogWindow.DialogHostProperty);
            }
            set
            {
                base.SetValue(ValidatingDialogWindow.DialogHostProperty, value);
            }
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return Microsoft.VisualStudio.Shell.ServiceProvider.GlobalProvider;
            }
        }

        static ValidatingDialogWindow()
        {
            ValidatingDialogWindow.DialogHostProperty = DependencyProperty.Register("DialogHost", typeof(IDialogHost), typeof(ValidatingDialogWindow), new PropertyMetadata());
        }

        public ValidatingDialogWindow()
        {
            base.Loaded += new RoutedEventHandler(this.Dialog_Loaded);
        }

        private void Dialog_Loaded(object sender, EventArgs e)
        {
            base.Loaded -= new RoutedEventHandler(this.Dialog_Loaded);
            this.DialogHost = new ValidatingDialogWindow.ValidatingDialogHost(this);
        }

        protected bool TryClose()
        {
            ValidatingDialogWindow.ValidatingDialogHost dialogHost = (ValidatingDialogWindow.ValidatingDialogHost)this.DialogHost ?? new ValidatingDialogWindow.ValidatingDialogHost(this);
            IDataErrorInfo dataContext = (IDataErrorInfo)base.DataContext;
            if (dataContext != null)
            {
                string error = dataContext.Error;
                if (error != null)
                {
                    dialogHost.ShowErrorMessage(error, null);
                    return false;
                }
            }
            if (!dialogHost.OnClosing())
            {
                return false;
            }
            base.DialogResult = new bool?(true);
            return true;
        }

        private class ValidatingDialogHost : IDialogHost
        {
            private ValidatingDialogWindow Dialog
            {
                get;
                set;
            }

            public ValidatingDialogHost(ValidatingDialogWindow dialog)
            {
                this.Dialog = dialog;
            }

            public bool OnClosing()
            {
                EventHandler<CancelEventArgs> eventHandler = this.Closing;
                if (eventHandler == null)
                {
                    return true;
                }
                CancelEventArgs cancelEventArg = new CancelEventArgs(false);
                eventHandler(this, cancelEventArg);
                return !cancelEventArg.Cancel;
            }

            public MessageBoxResult RequestConfirmation(string message, string caption)
            {
                IVsUIShell service = (IVsUIShell)this.Dialog.ServiceProvider.GetService(typeof(SVsUIShell));
                if (!VsShellUtilities.PromptYesNo(message, caption ?? this.Dialog.Title, OLEMSGICON.OLEMSGICON_QUERY, service))
                {
                    return MessageBoxResult.No;
                }
                return MessageBoxResult.Yes;
            }

            public void ShowErrorMessage(string message, string caption)
            {
                VsShellUtilities.ShowMessageBox(this.Dialog.ServiceProvider, message, caption ?? this.Dialog.Title, OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            public bool TrySelectFile(Project project, string title, string filter, string storageKey, out string file)
            {
                IVsHierarchy vsHierarchy;
                if (!NativeMethods.Succeeded(((IVsSolution)this.Dialog.ServiceProvider.GetService(typeof(SVsSolution))).GetProjectOfUniqueName(project.FullName, out vsHierarchy)))
                {
                    file = null;
                    return false;
                }
                string item = null;
                ProjectSettings projectSetting = null;
                IVsBuildPropertyStorage vsBuildPropertyStorage = null;
                if (storageKey != null)
                {
                    vsBuildPropertyStorage = vsHierarchy as IVsBuildPropertyStorage;
                }
                if (vsBuildPropertyStorage != null)
                {
                    try
                    {
                        projectSetting = new ProjectSettings(vsBuildPropertyStorage);
                        item = projectSetting[storageKey];
                    }
                    catch
                    {
                        projectSetting = null;
                    }
                }
                if (!ProjectItemSelector.TrySelectItem(vsHierarchy, title, filter, item, out file))
                {
                    return false;
                }
                if (projectSetting != null)
                {
                    try
                    {
                        projectSetting[storageKey] = file;
                    }
                    catch
                    {
                        projectSetting = null;
                    }
                }
                return true;
            }

            public event EventHandler<CancelEventArgs> Closing;
        }
    }
}