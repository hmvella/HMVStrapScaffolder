using System.ComponentModel.Composition;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class MvcAreaScaffolderFactory : ScaffolderFactory<MvcFrameworkDependency>
	{
		public MvcAreaScaffolderFactory() : base(new CodeGeneratorInformation("MVC 5 Area", "An MVC area that can have its own models, views, controllers, and routes.", "Justin Farrugia", ScaffolderVersions.MvcScaffolderVersion, "MvcAreaScaffolder", ScaffolderIcons.Area, new string[] { "Area" }, new string[] { Categories.Mvc, Categories.MvcArea }))
		{
		}

		protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new MvcAreaScaffolder(context, base.Information);
		}
	}
}