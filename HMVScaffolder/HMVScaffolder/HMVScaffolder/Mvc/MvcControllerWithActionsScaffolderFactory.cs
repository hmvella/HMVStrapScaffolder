using System.ComponentModel.Composition;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class MvcControllerWithActionsScaffolderFactory : ScaffolderFactory<MvcFrameworkDependency>
	{
		public MvcControllerWithActionsScaffolderFactory() : base(new CodeGeneratorInformation("MVC 5 Controller with read/write actions", "An MVC controller with actions to create, read, update, delete, and list entities.", "Justin Farrugia", ScaffolderVersions.MvcScaffolderVersion, "MvcControllerWithActionsScaffolder", ScaffolderIcons.Controller, new string[] { "Controller" }, new string[] { Categories.Mvc, Categories.MvcController }))
		{
		}

		protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new MvcControllerWithActionsScaffolder(context, base.Information);
		}
	}
}