using Microsoft.AspNet.Scaffolding;
using System;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	public class MvcControllerWithActionsScaffolder : ControllerScaffolder<MvcFrameworkDependency>
	{
		protected internal override string TemplateFolderName
		{
			get
			{
				return "MvcControllerWithActions";
			}
		}

		public MvcControllerWithActionsScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

		protected override void OnModelCreated(ControllerScaffolderModel model)
		{
			base.OnModelCreated(model);
			model.IsViewFolderRequired = true;
		}

        protected override object CreateViewModel(ControllerScaffolderModel model)
        {
            throw new NotImplementedException();
        }
    }
}