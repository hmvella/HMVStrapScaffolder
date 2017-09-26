using Microsoft.AspNet.Scaffolding;
using System.ComponentModel.Composition;

namespace HMVScaffolder.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class MvcViewScaffolderFactory : ScaffolderFactory<MvcFrameworkDependency>
	{
		public MvcViewScaffolderFactory() : base(new CodeGeneratorInformation("MVC 5 View test 4", "An MVC view with or without a strongly-typed Model", "Justin Farrugia", ScaffolderVersions.MvcScaffolderVersion, "MvcViewScaffolder", ScaffolderIcons.Views, new string[] { "View" }, new string[] { Categories.Common, Categories.Mvc, Categories.MvcView }))
		{
		}

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new MvcViewScaffolder(context, base.Information);
        }

        protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new MvcViewScaffolder(context, base.Information);
		}
	}
}