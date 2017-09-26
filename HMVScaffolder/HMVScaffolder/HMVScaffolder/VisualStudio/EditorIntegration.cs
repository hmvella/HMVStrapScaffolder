using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace HMVScaffolder.Mvc
{
    internal class EditorIntegration : IEditorIntegration
    {
        private VisualStudioIntegration VisualStudio
        {
            get;
            set;
        }

        public EditorIntegration(VisualStudioIntegration visualStudio)
        {
            this.VisualStudio = visualStudio;
        }

        public IVsWindowFrame CreateAndOpenReadme(string text)
        {
            Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider;
            IVsUIHierarchy vsUIHierarchy;
            uint num;
            IVsWindowFrame vsWindowFrame;
            string tempFilename = EditorIntegration.GetTempFilename("readme", "txt");
            File.WriteAllText(tempFilename, text);
            IVsUIShellOpenDocument service = (IVsUIShellOpenDocument)this.VisualStudio.ServiceProvider.GetService(typeof(SVsUIShellOpenDocument));
            Guid textViewGuid = VSConstants.LOGVIEWID.TextView_guid;
            if (!NativeMethods.Succeeded(service.OpenDocumentViaProject(tempFilename, ref textViewGuid, out serviceProvider, out vsUIHierarchy, out num, out vsWindowFrame)))
            {
                return null;
            }
            vsUIHierarchy.SetProperty(num, -2057, true);
            if (NativeMethods.Succeeded(vsWindowFrame.Show()))
            {
                return vsWindowFrame;
            }
            return null;
        }

        public void FormatDocument(string filePath)
        {
            IVsUIHierarchy vsUIHierarchy = null;
            uint num = 0;
            IVsWindowFrame vsWindowFrame = null;
            if (!VsShellUtilities.IsDocumentOpen(this.VisualStudio.ServiceProvider, filePath, Guid.Empty, out vsUIHierarchy, out num, out vsWindowFrame))
            {
                return;
            }
            IOleCommandTarget textView = (IOleCommandTarget)VsShellUtilities.GetTextView(vsWindowFrame);
            Guid gUID = typeof(VSConstants.VSStd2KCmdID).GUID;
            OLECMD[] oLECMDArray = new OLECMD[1];
            OLECMD oLECMD = new OLECMD()
            {
                cmdID = 143
            };
            oLECMDArray[0] = oLECMD;
            OLECMD[] oLECMDArray1 = oLECMDArray;
            int num1 = textView.QueryStatus(ref gUID, 1, oLECMDArray1, IntPtr.Zero);
            Marshal.ThrowExceptionForHR(num1);
            if (oLECMDArray1[0].cmdf == 3)
            {
                num1 = textView.Exec(ref gUID, 143, 0, IntPtr.Zero, IntPtr.Zero);
                Marshal.ThrowExceptionForHR(num1);
            }
        }

        public IEditorInterfaces GetOrOpenDocument(string path)
        {
            IVsHierarchy vsHierarchy;
            uint num;
            uint num1;
            IEditorInterfaces editorInterface;
            this.OpenFileInEditor(path);
            IVsRunningDocumentTable service = (IVsRunningDocumentTable)this.VisualStudio.ServiceProvider.GetService(typeof(SVsRunningDocumentTable));
            IntPtr zero = IntPtr.Zero;
            try
            {
                if (service.FindAndLockDocument(0, path, out vsHierarchy, out num, out zero, out num1) == 0 && zero != IntPtr.Zero)
                {
                    IVsTextBuffer objectForIUnknown = Marshal.GetObjectForIUnknown(zero) as IVsTextBuffer;
                    if (objectForIUnknown != null)
                    {
                        IVsEditorAdaptersFactoryService vsEditorAdaptersFactoryService = this.VisualStudio.ComponentModel.GetService<IVsEditorAdaptersFactoryService>();
                        ITextBuffer documentBuffer = vsEditorAdaptersFactoryService.GetDocumentBuffer(objectForIUnknown);
                        ITextDocument property = documentBuffer.Properties.GetProperty<ITextDocument>(typeof(ITextDocument));
                        editorInterface = new EditorIntegration.EditorInterfaces(documentBuffer, property, objectForIUnknown);
                        return editorInterface;
                    }
                }
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Unable to open file '{0}'.", path));
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Marshal.Release(zero);
                }
            }
            return editorInterface;
        }

        private static string GetTempFilename(string baseName, string extension)
        {
            string i;
            object[] objArray = null;
            int num = 1;
            string tempPath = Path.GetTempPath();
            for (i = Path.Combine(tempPath, string.Concat(baseName, ".", extension)); File.Exists(i); i = Path.Combine(tempPath, string.Concat(objArray)))
            {
                objArray = new object[] { baseName, null, null, null };
                int num1 = num;
                num = num1 + 1;
                objArray[1] = num1;
                objArray[2] = ".";
                objArray[3] = extension;
            }
            return i;
        }

        public void OpenFileInEditor(string filePath)
        {
            DTE service = (DTE)this.VisualStudio.ServiceProvider.GetService(typeof(SDTE));
            if (File.Exists(filePath) && !service.ItemOperations.IsFileOpen(filePath, "{FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF}"))
            {
                using (IDisposable disposable = this.SuppressChangeNotifications(filePath))
                {
                }
                service.ItemOperations.OpenFile(filePath, "{00000000-0000-0000-0000-000000000000}");
            }
        }

        private void ResumeChangeNotifications(string filePath)
        {
            IVsFileChangeEx service = (IVsFileChangeEx)this.VisualStudio.ServiceProvider.GetService(typeof(IVsFileChangeEx));
            try
            {
                Marshal.ThrowExceptionForHR(service.SyncFile(filePath));
            }
            finally
            {
                Marshal.ThrowExceptionForHR(service.IgnoreFile(0, filePath, 0));
            }
        }

        public IDisposable SuppressChangeNotifications(string filePath)
        {
            IVsFileChangeEx service = (IVsFileChangeEx)this.VisualStudio.ServiceProvider.GetService(typeof(IVsFileChangeEx));
            Marshal.ThrowExceptionForHR(service.IgnoreFile(0, filePath, 1));
            return new EditorIntegration.NotificationActivator(this, filePath);
        }

        private class EditorInterfaces : IEditorInterfaces
        {
            private ITextBuffer BackingField1;

            public ITextBuffer TextBuffer
            {
                get
                {
                    return this.BackingField1;
                }
                set
                {
                    this.BackingField1 = value;
                }
            }

            private ITextDocument BackingField2;

            public ITextDocument TextDocument
            {
                get
                {
                    return this.BackingField2;
                }
                set
                {
                    this.BackingField2 = value;
                }
            }

            private IVsTextBuffer BackingField3;

            public IVsTextBuffer VsTextBuffer
            {
                get
                {
                    return this.BackingField3;
                }
                set
                {
                    this.BackingField3 = value;
                }
            }

            public EditorInterfaces(ITextBuffer textBuffer, ITextDocument textDocument, IVsTextBuffer vsTextBuffer)
            {
                this.TextBuffer = textBuffer;
                this.TextDocument = textDocument;
                this.VsTextBuffer = vsTextBuffer;
            }
        }

        private class NotificationActivator : IDisposable
        {
            private readonly EditorIntegration _editorIntegration;

            private readonly string _filePath;

            public NotificationActivator(EditorIntegration editorIntegration, string filePath)
            {
                this._editorIntegration = editorIntegration;
                this._filePath = filePath;
            }

            public void Dispose()
            {
                this._editorIntegration.ResumeChangeNotifications(this._filePath);
            }
        }
    }
}