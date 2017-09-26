using Microsoft.AspNet.Scaffolding;
using System.ComponentModel.Composition;

namespace HMVScaffolder.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	internal class MvcControllerWithContextScaffolderFactory : ScaffolderFactory<MvcFrameworkDependency>
	{


        /////// <summary>
        ///////  Information about the code generator goes here.
        /////// </summary>
        ////private static CodeGeneratorInformation _info = new CodeGeneratorInformation(
        ////    displayName: "",
        ////    description: "This is a custom scaffolder.",
        ////    author: "Justin Name",
        ////    version: new Version(1, 0, 0, 0),
        ////    id: "MvcControllerWithContextScaffolder",
        ////    icon: ToImageSource(Resources._TemplateIconSample),
        ////    gestures: new[] { "Controller", "View", "Area" },
        ////    categories: new[] { Categories.Common, Categories.MvcController, Categories.Other });


        public MvcControllerWithContextScaffolderFactory() : base(new CodeGeneratorInformation("Custom Scaffolder test 2345678", "This is a custom scaffolder.", "Justin Farrugia", ScaffolderVersions.MvcScaffolderVersion, "MvcControllerWithContextScaffolder", ScaffolderIcons.ControllerWithViews, new string[] { "Controller" }, new string[] { Categories.Common, Categories.Mvc, Categories.MvcController }))
        {
		}


        protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
		{
			return new MvcControllerWithContextScaffolder(context, base.Information);
		}

    }
}