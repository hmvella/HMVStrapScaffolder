using System.ComponentModel.Composition;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class ODataControllerWithActionsScaffolderFactory : ScaffolderFactory<ODataFrameworkDependency>
	{
		public ODataControllerWithActionsScaffolderFactory() : base(new CodeGeneratorInformation("Web API 2 OData v3 Controller with read/write actions", "A Web API 2 OData v3 controller with CRUD actions to create, read, update, delete, and list entities.", "Justin Farrugia", ScaffolderVersions.WebApiScaffolderVersion, "ODataControllerWithActionsScaffolder", ScaffolderIcons.Controller, new string[] { "Controller" }, new string[] { Categories.WebApi }))
		{
		}

		protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new ODataControllerWithActionsScaffolder(context, base.Information);
		}
	}
}