using System;

namespace HMVScaffolder.Mvc
{
	internal interface IThreadedWaitDialogService
	{
		IDisposable ShowProgressDialog(string caption, string message, int startDelay);
	}
}