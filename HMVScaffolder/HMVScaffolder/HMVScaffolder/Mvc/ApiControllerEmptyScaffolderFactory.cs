using System.ComponentModel.Composition;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class ApiControllerEmptyScaffolderFactory : ScaffolderFactory<WebApiFrameworkDependency>
	{
		public ApiControllerEmptyScaffolderFactory() : base(new CodeGeneratorInformation("Web API 2 Controller – Empty", "An empty Web API controller.", "Justin Farrugia", ScaffolderVersions.WebApiScaffolderVersion, "ApiControllerEmptyScaffolder", ScaffolderIcons.Controller, new string[] { "Controller" }, new string[] { Categories.Common, Categories.WebApi }))
		{
		}

		protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new ApiControllerEmptyScaffolder(context, base.Information);
		}
	}
}