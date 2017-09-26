using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.AspNet.Scaffolding.Mvc.ReadMe
{
    internal class WebApiReadMe : ReadMeGenerator
	{
		private const string WebApiProductTitle = "ASP.NET Web API";

		private readonly static string _webApiCurrentVersion;

		protected override IEnumerable<string> Namespaces
		{
			get
			{
				yield return "System.Web.Http";
				yield return "System.Web.Routing";
			}
		}

		private string WebApiCodeSnippet
		{
			get
			{
				return string.Concat(ReadMeFormatting.NewLineAndIndentation, "GlobalConfiguration.Configure({0})", base.LanguageRules.StatementEnding);
			}
		}

		static WebApiReadMe()
		{
			WebApiReadMe._webApiCurrentVersion = string.Concat("ASP.NET Web API ", ScaffolderVersions.WebApiScaffolderVersion.Major);
		}

		public WebApiReadMe(Microsoft.AspNet.Scaffolding.ProjectLanguage language, string projectName, IDictionary<string, string> fileNames) : base(language, projectName, fileNames)
		{
		}

		protected override void AddCodeSnippet()
		{
			base.AddCodeSnippet();
			base.Builder.AppendLine("3. Add the following lines to the beginning of the Application_Start method:");

            base.Builder.AppendFormat(CultureInfo.InvariantCulture, this.WebApiCodeSnippet, base.LanguageRules.CreateDelegateText(string.Concat(base.AppStartFileNames["WebApiConfig"], ".Register")));
		}

		protected override void AddHeading()
		{
			StringBuilder builder = base.Builder;
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			string scaffoldReadMeHeading = "Visual Studio has added the {0} dependencies for {1} to project '{2}'. The {3} file in the project may require additional changes to enable { 4}.";
            object[] scaffoldFullSet = new object[] { "full set of", WebApiReadMe._webApiCurrentVersion, base.ProjectName, base.GlobalAsaxCodeBehindFilename, "ASP.NET Web API" };
			builder.AppendFormat(currentCulture, scaffoldReadMeHeading, scaffoldFullSet);
			base.Builder.AppendLine();
		}
	}
}