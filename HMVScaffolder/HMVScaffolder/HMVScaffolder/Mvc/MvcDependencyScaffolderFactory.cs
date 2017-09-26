using System.ComponentModel.Composition;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	public class MvcDependencyScaffolderFactory : ScaffolderFactory<MvcFrameworkDependency>
	{
		public MvcDependencyScaffolderFactory() : base(new CodeGeneratorInformation("MVC 5 Dependencies", "Add MVC Dependencies to the project. Includes packages, configuration, script libraries, and a default layout.", "Justin Farrugia", ScaffolderVersions.MvcScaffolderVersion, "MvcDependencyScaffolder", ScaffolderIcons.Controller, new string[0], new string[] { Categories.Mvc }))
		{
		}

		protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new MvcDependencyScaffolder(context, base.Information);
		}
	}
}