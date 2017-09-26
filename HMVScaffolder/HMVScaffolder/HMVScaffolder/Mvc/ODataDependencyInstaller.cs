using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Mvc.ReadMe;
using Microsoft.AspNet.Scaffolding.Mvc.VisualStudio;
using Microsoft.VisualStudio.Utilities;
using System;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	public class ODataDependencyInstaller : ApiDependencyInstaller
	{
		public ODataDependencyInstaller(CodeGenerationContext context, IVisualStudioIntegration visualStudioIntegration) : base(context, visualStudioIntegration)
		{
		}

		protected override FrameworkDependencyStatus GenerateConfiguration()
		{
			object obj = null;
			object obj1 = null;
			base.Context.Items.TryGetProperty<object>("MVC_IsODataAssemblyReferenced", out obj);
			base.Context.Items.TryGetProperty<object>("MVC_IsWebApiAssemblyReferenced", out obj1);
			bool flag = !(bool)obj;
			bool flag1 = !(bool)obj1;
			bool flag2 = base.TryCreateGlobalAsax();
			if (flag2 && flag)
			{
				return FrameworkDependencyStatus.FromReadme(ODataReadMe.CreateReadMeText());
			}
			if (!flag2)
			{
				string empty = string.Empty;
				if (flag)
				{
					empty = string.Concat(empty, ODataReadMe.CreateReadMeText());
				}
				if (flag1)
				{
					WebApiReadMe webApiReadMe = new WebApiReadMe(ProjectExtensions.GetCodeLanguage(base.Context.ActiveProject), base.Context.ActiveProject.Name, base.AppStartFileNames);
					empty = string.Concat(empty, webApiReadMe.CreateReadMeText());
				}
				if (!string.IsNullOrEmpty(empty))
				{
					return FrameworkDependencyStatus.FromReadme(empty);
				}
			}
			return FrameworkDependencyStatus.InstallSuccessful;
		}
	}
}