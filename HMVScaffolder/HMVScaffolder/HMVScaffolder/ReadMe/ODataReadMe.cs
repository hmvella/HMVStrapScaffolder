using System;

namespace Microsoft.AspNet.Scaffolding.Mvc.ReadMe
{
    internal class ODataReadMe
	{
		public ODataReadMe()
		{
		}

		internal static string CreateReadMeText()
		{
			return string.Concat(Environment.NewLine, "The Web API configuration must be updated for each OData controller created by scaffolding. Instructions are available in the generated controller.", Environment.NewLine, Environment.NewLine);

        }
	}
}