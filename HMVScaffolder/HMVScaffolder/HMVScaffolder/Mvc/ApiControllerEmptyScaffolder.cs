using Microsoft.AspNet.Scaffolding;
using System;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	public class ApiControllerEmptyScaffolder : ControllerScaffolder<WebApiFrameworkDependency>
	{
		protected internal override string TemplateFolderName
		{
			get
			{
				return "ApiControllerEmpty";
			}
		}

		public ApiControllerEmptyScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

        protected override object CreateViewModel(ControllerScaffolderModel model)
        {
            throw new NotImplementedException();
        }
    }
}