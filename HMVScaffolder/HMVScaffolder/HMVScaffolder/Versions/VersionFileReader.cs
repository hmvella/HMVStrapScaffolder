using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace HMVScaffolder.Mvc
{
    internal class VersionFileReader
	{
		private const string IdAttribute = "Id";

		private const string VersionAttribute = "Version";

		public VersionFileReader()
		{
		}

		internal static IDictionary<string, string> GetVersions(string xmlFile, string elementXPath)
		{
			if (string.IsNullOrWhiteSpace(xmlFile))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument {0} must be non-empty and non-null.", "xmlFile"));
			}
			if (string.IsNullOrWhiteSpace(elementXPath))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument {0} must be non-empty and non-null.", "elementXPath"));
			}
			XmlDocument xmlDocument = new XmlDocument(); //JF
            string dirStr = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Templates\\";
            string str = Path.Combine(dirStr, xmlFile);
			if (!File.Exists(str))
			{
				return null;
			}
			using (TextReader textReader = File.OpenText(str))
			{
				xmlDocument.Load(textReader);
			}
			Dictionary<string, string> strs = new Dictionary<string, string>();
			foreach (XmlElement xmlElement in xmlDocument.SelectNodes(elementXPath))
			{
				string attribute = xmlElement.GetAttribute("Id");
				strs.Add(attribute, xmlElement.GetAttribute("Version"));
			}
			return strs;
		}
	}
}