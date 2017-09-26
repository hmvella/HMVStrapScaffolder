using System;
using System.Windows.Markup;

namespace HMVScaffolder.Mvc
{
	[MarkupExtensionReturnType(typeof(string))]
	public class RemoveSubstringExtension : MarkupExtension
	{
		private object _text;

		private string _remove;

		public RemoveSubstringExtension(object text) : this(text, null)
		{
		}

		public RemoveSubstringExtension(object text, string remove)
		{
			this._text = text;
			this._remove = remove;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (this._text == null)
			{
				return null;
			}
			string str = this._remove ?? "_";
			string str1 = null;
			MarkupExtension markupExtension = this._text as MarkupExtension;
			if (markupExtension != null)
			{
				str1 = markupExtension.ProvideValue(serviceProvider) as string;
			}
			if (str1 == null)
			{
				str1 = this._text as string;
			}
			if (str1 == null)
			{
				return null;
			}
			return str1.Replace(str, string.Empty);
		}
	}
}