using System.ComponentModel.Composition;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class ApiControllerWithActionsScaffolderFactory : ScaffolderFactory<WebApiFrameworkDependency>
	{
		public ApiControllerWithActionsScaffolderFactory() : base(new CodeGeneratorInformation("Web API 2 Controller with read/write actions", "A Web API controller with REST actions to create, read, update, delete, and list entities.", "Justin Farrugia", ScaffolderVersions.WebApiScaffolderVersion, "ApiControllerWithActionsScaffolder", ScaffolderIcons.Controller, new string[] { "Controller" }, new string[] { Categories.WebApi }))
		{
		}

		protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new ApiControllerWithActionsScaffolder(context, base.Information);
		}
	}
}