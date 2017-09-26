using System.ComponentModel.Composition;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class ApiControllerWithContextScaffolderFactory : ScaffolderFactory<WebApiFrameworkDependency>
	{
		public ApiControllerWithContextScaffolderFactory() : base(new CodeGeneratorInformation("Web API 2 Controller with actions, using Entity Framework", "A Web API controller with REST actions to create, read, update, delete, and list entities from an Entity Framework data context.", "Justin Farrugia", ScaffolderVersions.WebApiScaffolderVersion, "ApiControllerWithContextScaffolder", ScaffolderIcons.Controller, new string[] { "Controller" }, new string[] { Categories.Common, Categories.WebApi }))
		{
		}

		protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new ApiControllerWithContextScaffolder(context, base.Information);
		}
	}
}