using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;

namespace HMVScaffolder.Mvc
{
	public interface IEditorInterfaces
	{
		ITextBuffer TextBuffer
		{
			get;
		}

		ITextDocument TextDocument
		{
			get;
		}

		IVsTextBuffer VsTextBuffer
		{
			get;
		}
	}
}