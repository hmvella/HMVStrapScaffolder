using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace HMVScaffolder.Mvc.VisualStudio
{
	internal class VsShellDialogService : IThreadedWaitDialogService
	{
		private readonly IServiceProvider _serviceProvider;

		public VsShellDialogService(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this._serviceProvider = serviceProvider;
		}

		public IDisposable ShowProgressDialog(string caption, string message, int startDelay)
		{
			IVsThreadedWaitDialog2 vsThreadedWaitDialog2;
			IVsThreadedWaitDialogFactory service = this._serviceProvider.GetService(typeof(SVsThreadedWaitDialogFactory)) as IVsThreadedWaitDialogFactory;
			Marshal.ThrowExceptionForHR(service.CreateInstance(out vsThreadedWaitDialog2));
			string str = null;
			int num = vsThreadedWaitDialog2.StartWaitDialog(caption, message, null, null, str, startDelay, false, true);
			Marshal.ThrowExceptionForHR(num);
			Cursor overrideCursor = Mouse.OverrideCursor;
			Mouse.OverrideCursor = Cursors.Wait;
			return new VsShellDialogService.ProgressDialog(vsThreadedWaitDialog2, overrideCursor);
		}

		private class ProgressDialog : IDisposable
		{
			public Cursor OriginalCursor
			{
				get;
				set;
			}

			public IVsThreadedWaitDialog2 VsWaitDialog
			{
				get;
				set;
			}

			public ProgressDialog(IVsThreadedWaitDialog2 vsWaitDialog, Cursor originalCursor)
			{
				this.VsWaitDialog = vsWaitDialog;
				this.OriginalCursor = originalCursor;
			}

			public void Dispose()
			{
				int num;
				Mouse.OverrideCursor = this.OriginalCursor;
				Marshal.ThrowExceptionForHR(this.VsWaitDialog.EndWaitDialog(out num));
			}
		}
	}
}