using HMVScaffolder.Mvc;
using HMVScaffolder.Mvc.VisualStudio;
using System;
using System.Collections.Generic;
using System.Xml;

namespace HMVScaffolder.Mvc.Configuration
{
	internal class MvcConfigurationEditor : ConfigurationEditor
	{
		private static Dictionary<string, string> RequiredSettings
		{
			get
			{
				Dictionary<string, string> strs = new Dictionary<string, string>()
				{
					{ "webpages:Version", AssemblyVersions.WebPagesAssemblyVersion.ToString(4) },
					{ "webpages:Enabled", "false" },
					{ "ClientValidationEnabled", "true" },
					{ "UnobtrusiveJavaScriptEnabled", "true" }
				};
				return strs;
			}
		}

		public MvcConfigurationEditor(IVisualStudioIntegration visualStudio) : base(visualStudio)
		{
		}

		protected override XmlDocument TransformDocument(XmlDocument document)
		{
			XmlElement xmlElement = (XmlElement)document.SelectSingleNode(XmlConstants.ConfigurationPath);
			XmlElement xmlElement1 = (XmlElement)document.SelectSingleNode(XmlConstants.AppSettingsPath);
			if (xmlElement1 == null)
			{
				xmlElement1 = document.CreateElement(XmlConstants.AppSettings);
				xmlElement.AppendChild(xmlElement1);
			}
			foreach (KeyValuePair<string, string> requiredSetting in MvcConfigurationEditor.RequiredSettings)
			{
				string[] addAppSettingPath = new string[] { XmlConstants.AddAppSettingPath, "[@", XmlConstants.Key, "='", requiredSetting.Key, "']" };
				XmlElement xmlElement2 = (XmlElement)document.SelectSingleNode(string.Concat(addAppSettingPath));
				if (xmlElement2 != null)
				{
					continue;
				}
				xmlElement2 = document.CreateElement(XmlConstants.Add);
				xmlElement1.AppendChild(xmlElement2);
				XmlAttribute key = document.CreateAttribute(XmlConstants.Key);
				xmlElement2.Attributes.Append(key);
				key.Value = requiredSetting.Key;
				XmlAttribute value = document.CreateAttribute(XmlConstants.Value);
				xmlElement2.Attributes.Append(value);
				value.Value = requiredSetting.Value;
			}
			return document;
		}
	}
}