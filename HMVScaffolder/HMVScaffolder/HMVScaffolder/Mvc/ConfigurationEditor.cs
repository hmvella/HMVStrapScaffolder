using EnvDTE;
using HMVScaffolder.Mvc.Configuration;
using HMVScaffolder.Mvc.VisualStudio;
using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.Text;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace HMVScaffolder.Mvc
{
    internal abstract class ConfigurationEditor
	{
		protected IVisualStudioIntegration VisualStudio
		{
			get;
			private set;
		}

		protected ConfigurationEditor(IVisualStudioIntegration visualStudio)
		{
			this.VisualStudio = visualStudio;
		}

		public void Edit(Project project)
		{
			string str = Path.Combine(ProjectExtensions.GetFullPath(project), "web.config");
			if (project.DTE.SourceControl.IsItemUnderSCC(str) && !project.DTE.SourceControl.IsItemCheckedOut(str) && !project.DTE.SourceControl.CheckOutItem(str))
			{
				return;
			}
			IEditorInterfaces orOpenDocument = this.VisualStudio.Editor.GetOrOpenDocument(str);
			Marshal.ThrowExceptionForHR(orOpenDocument.VsTextBuffer.Reload(1));
			ITextBuffer textBuffer = orOpenDocument.TextBuffer;
			using (ITextEdit textEdit = textBuffer.CreateEdit())
			{
				string editedText = this.GetEditedText(textEdit.Snapshot.GetText());
				if (editedText != null)
				{
					textEdit.Replace(new Span(0, textBuffer.CurrentSnapshot.Length), editedText);
					textEdit.Apply();
					this.VisualStudio.Editor.FormatDocument(str);
					Document document = project.DTE.Documents.Item(str);
					document.Save("");
				}
			}
		}

		public string GetEditedText(string input)
		{
			XmlDocument xmlDocument;
			try
			{
				xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(input);
			}
			catch (XmlException xmlException)
			{
				throw new InvalidOperationException("The web.config file contains invalid XML or has an invalid root element.", xmlException);

            }
			if ((XmlElement)xmlDocument.SelectSingleNode(XmlConstants.ConfigurationPath) == null)
			{
				throw new InvalidOperationException("The web.config file contains invalid XML or has an invalid root element.", null);

            }
			xmlDocument = this.TransformDocument(xmlDocument);
			XmlWriterSettings xmlWriterSetting = new XmlWriterSettings()
			{
				NewLineHandling = NewLineHandling.Replace,
				NewLineChars = Environment.NewLine,
				Indent = true,
				IndentChars = "    "
			};
			XmlWriterSettings xmlWriterSetting1 = xmlWriterSetting;
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSetting1))
			{
				xmlDocument.WriteTo(xmlWriter);
			}
			string str = stringBuilder.ToString();
			if (string.Equals(input, str, StringComparison.Ordinal))
			{
				return null;
			}
			return str;
		}

		protected abstract XmlDocument TransformDocument(XmlDocument document);
	}
}