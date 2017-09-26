using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HMVScaffolder.Mvc.ReadMe
{
    internal class MvcFullDependencyReadMe : MvcMinimalDependencyReadMe
	{
		private string MvcFullCodeSnippet
		{
			get
			{
				string[] newLineAndIndentation = new string[] { ReadMeFormatting.NewLineAndIndentation, "{0}.RegisterGlobalFilters(GlobalFilters.Filters)", base.LanguageRules.StatementEnding, ReadMeFormatting.NewLineAndIndentation, "{1}.RegisterBundles(BundleTable.Bundles)", base.LanguageRules.StatementEnding };
				return string.Concat(newLineAndIndentation);
			}
		}

		protected override IEnumerable<string> Namespaces
		{
			get
			{
				yield return "System.Web.Mvc";
				yield return "System.Web.Routing";
				yield return "System.Web.Optimization";
			}
		}

		public MvcFullDependencyReadMe(Microsoft.AspNet.Scaffolding.ProjectLanguage language, string projectName, IDictionary<string, string> fileNames) : base(language, projectName, fileNames)
		{
		}

		protected override void AddCodeSnippet()
		{
			base.AddCodeSnippet();
			base.Builder.AppendFormat(CultureInfo.InvariantCulture, this.MvcFullCodeSnippet, base.AppStartFileNames["FilterConfig"], base.AppStartFileNames["BundleConfig"]);
		}

		protected override void AddHeading()
		{
			StringBuilder builder = base.Builder;
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			string scaffoldReadMeHeading = "Visual Studio has added the {0} dependencies for {1} to project '{2}'. The {3} file in the project may require additional changes to enable { 4}.";
            object[] scaffoldFullSet = new object[] { "full set of", ReadMeGenerator.MvcCurrentVersion, base.ProjectName, base.GlobalAsaxCodeBehindFilename, ReadMeGenerator.Mvc };

            builder.AppendFormat(currentCulture, scaffoldReadMeHeading, scaffoldFullSet);
			base.Builder.AppendLine();
		}
	}
}