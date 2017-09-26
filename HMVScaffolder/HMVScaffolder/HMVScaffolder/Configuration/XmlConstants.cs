using System;

namespace HMVScaffolder.Mvc.Configuration
{
	internal static class XmlConstants
	{
		public readonly static string ConfigurationPath;

		public readonly static string AppSettings;

		public readonly static string AppSettingsPath;

		public readonly static string Remove;

		public readonly static string RemoveAppSettingPath;

		public readonly static string Add;

		public readonly static string AddAppSettingPath;

		public readonly static string Key;

		public readonly static string Value;

		static XmlConstants()
		{
			XmlConstants.ConfigurationPath = "/configuration";
			XmlConstants.AppSettings = "appSettings";
			XmlConstants.AppSettingsPath = string.Concat(XmlConstants.ConfigurationPath, "/", XmlConstants.AppSettings);
			XmlConstants.Remove = "remove";
			XmlConstants.RemoveAppSettingPath = string.Concat(XmlConstants.AppSettingsPath, "/", XmlConstants.Remove);
			XmlConstants.Add = "add";
			XmlConstants.AddAppSettingPath = string.Concat(XmlConstants.AppSettingsPath, "/", XmlConstants.Add);
			XmlConstants.Key = "key";
			XmlConstants.Value = "value";
		}
	}
}