using Microsoft.AspNet.Scaffolding;
using System;

namespace HMVScaffolder.Mvc
{
	public class MvcControllerEmptyScaffolder : ControllerScaffolder<MvcFrameworkDependency>
	{
		protected internal override string TemplateFolderName
		{
			get
			{
				return "MvcControllerEmpty";
			}
		}

		public MvcControllerEmptyScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

		protected override void OnModelCreated(ControllerScaffolderModel model)
		{
			base.OnModelCreated(model);
			model.IsViewFolderRequired = true;
		}

        
    }
}