using System;
using System.Collections.Generic;
using System.Text;

namespace HMVScaffolder.Mvc.ReadMe
{
    internal abstract class ReadMeGenerator
	{
		protected readonly static string Mvc;

		protected readonly static string MvcCurrentVersion;

		protected readonly static string FunctionName;

		protected IDictionary<string, string> AppStartFileNames
		{
			get;
			private set;
		}

		protected StringBuilder Builder
		{
			get;
			private set;
		}

		protected string GlobalAsaxCodeBehindFilename
		{
			get
			{
				return string.Concat("Global.asax.", this.ProjectLanguage.CodeFileExtension);
			}
		}

		protected ReadMe.LanguageRules LanguageRules
		{
			get;
			private set;
		}

		protected abstract IEnumerable<string> Namespaces
		{
			get;
		}

		protected Microsoft.AspNet.Scaffolding.ProjectLanguage ProjectLanguage
		{
			get;
			private set;
		}

		protected string ProjectName
		{
			get;
			private set;
		}

		static ReadMeGenerator()
		{
			ReadMeGenerator.Mvc = "ASP.NET MVC";
			ReadMeGenerator.MvcCurrentVersion = string.Concat(ReadMeGenerator.Mvc, " ", ScaffolderVersions.MvcScaffolderVersion.Major);
			ReadMeGenerator.FunctionName = "Application_Start";
		}

		protected ReadMeGenerator(Microsoft.AspNet.Scaffolding.ProjectLanguage projectLanguage, string projectName, IDictionary<string, string> appStartFileNames)
		{
			this.ProjectLanguage = projectLanguage;
			this.ProjectName = projectName;
			this.AppStartFileNames = appStartFileNames;
			this.Builder = new StringBuilder();
			this.LanguageRules = ReadMeGenerator.GetRuleGenerator(projectLanguage);
		}

		protected virtual void AddCodeSnippet()
		{
			this.Builder.AppendLine();
		}

		protected abstract void AddHeading();

		private void AddNamespaces()
		{
			this.Builder.AppendLine();
			this.Builder.AppendLine("1. Add the following namespace references:");

            this.Builder.AppendLine();
			foreach (string @namespace in this.Namespaces)
			{
				this.Builder.AppendLine(this.LanguageRules.CreateNamespaceImportText(@namespace));
			}
		}

		private void AddNewFunctionMessage()
		{
			this.Builder.AppendLine();
			this.Builder.AppendLine("2. If the code does not already define an Application_Start method, add the following method:");

            this.Builder.AppendLine(this.LanguageRules.CreateFunction(ReadMeGenerator.FunctionName));
		}

		public string CreateReadMeText()
		{
			this.AddHeading();
			this.AddNamespaces();
			this.AddNewFunctionMessage();
			this.AddCodeSnippet();
			return this.Builder.ToString();
		}

		private static ReadMe.LanguageRules GetRuleGenerator(Microsoft.AspNet.Scaffolding.ProjectLanguage projectLanguage)
		{
			if ((object)projectLanguage == (object)Microsoft.AspNet.Scaffolding.ProjectLanguage.CSharp)
			{
				return new CSharpRules();
			}
			if ((object)projectLanguage != (object)Microsoft.AspNet.Scaffolding.ProjectLanguage.VisualBasic)
			{
				throw new InvalidOperationException("The project language is not supported.");

            }
			return new VisualBasicRules();
		}
	}
}