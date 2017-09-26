using Microsoft.AspNet.Scaffolding;
using System;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	public class ApiControllerWithContextScaffolder : ControllerWithContextScaffolder<WebApiFrameworkDependency>
	{
		protected internal override string TemplateFolderName
		{
			get
			{
				return "ApiControllerWithContext";
			}
		}

		public ApiControllerWithContextScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

        protected override object CreateViewModel(ControllerScaffolderModel model)
        {
            throw new NotImplementedException();
        }
    }
}