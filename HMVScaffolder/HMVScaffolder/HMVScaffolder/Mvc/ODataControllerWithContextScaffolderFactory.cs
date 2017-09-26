using System.ComponentModel.Composition;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class ODataControllerWithContextScaffolderFactory : ScaffolderFactory<ODataFrameworkDependency>
	{
		public ODataControllerWithContextScaffolderFactory() : base(new CodeGeneratorInformation("Web API 2 OData v3 Controller with actions, using Entity Framework", "A Web API 2 OData v3 controller with CRUD actions to create, read, update, delete, and list entities from an Entity Framework data context.", "Justin Farrugia", ScaffolderVersions.WebApiScaffolderVersion, "ODataControllerWithContextScaffolder", ScaffolderIcons.Controller, new string[] { "Controller" }, new string[] { Categories.WebApi }))
		{
		}

		protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new ODataControllerWithContextScaffolder(context, base.Information);
		}
	}
}