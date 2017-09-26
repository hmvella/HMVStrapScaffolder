using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HMVScaffolder.Mvc.ReadMe
{
    internal class MvcMinimalDependencyReadMe : ReadMeGenerator
	{
		private string MvcMinCodeSnippet
		{
			get
			{
				string[] newLineAndIndentation = new string[] { ReadMeFormatting.NewLineAndIndentation, "AreaRegistration.RegisterAllAreas()", base.LanguageRules.StatementEnding, ReadMeFormatting.NewLineAndIndentation, "{0}.RegisterRoutes(RouteTable.Routes)", base.LanguageRules.StatementEnding };
				return string.Concat(newLineAndIndentation);
			}
		}

		protected override IEnumerable<string> Namespaces
		{
			get
			{
				yield return "System.Web.Mvc";
				yield return "System.Web.Routing";
			}
		}

		public MvcMinimalDependencyReadMe(Microsoft.AspNet.Scaffolding.ProjectLanguage language, string projectName, IDictionary<string, string> fileNames) : base(language, projectName, fileNames)
		{
		}

		protected override void AddCodeSnippet()
		{
			base.AddCodeSnippet();
			base.Builder.AppendLine("3. Add the following lines to the end of the Application_Start method:");

            base.Builder.AppendFormat(CultureInfo.InvariantCulture, this.MvcMinCodeSnippet, base.AppStartFileNames["RouteConfig"]);
		}

		protected override void AddHeading()
		{
			StringBuilder builder = base.Builder;
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			string scaffoldReadMeHeading = "Visual Studio has added the {0} dependencies for {1} to project '{2}'. The {3} file in the project may require additional changes to enable { 4}.";
            object[] scaffoldMvcMinimalSet = new object[] { "minimal set of", ReadMeGenerator.MvcCurrentVersion, base.ProjectName, base.GlobalAsaxCodeBehindFilename, ReadMeGenerator.Mvc };
			builder.AppendFormat(currentCulture, scaffoldReadMeHeading, scaffoldMvcMinimalSet);
			base.Builder.AppendLine();
		}
	}
}