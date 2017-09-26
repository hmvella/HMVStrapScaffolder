using EnvDTE;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    public class ODataControllerWithContextScaffolder : ControllerWithContextScaffolder<ODataFrameworkDependency>
	{
		protected internal override string TemplateFolderName
		{
			get
			{
				return "ODataControllerWithContext";
			}
		}

		public ODataControllerWithContextScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

		protected override IDictionary<string, object> AddTemplateParameters(CodeType dbContextType, ModelMetadata modelMetadata)
		{
			IDictionary<string, object> strs = base.AddTemplateParameters(dbContextType, modelMetadata);
			strs.Add("ODataModificationMessage", "The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.");

            strs.Add("IsLegacyOdataVersion", base.Framework.IsODataLegacy(base.Context));
			return strs;
		}

        protected override object CreateViewModel(ControllerScaffolderModel model)
        {
            throw new NotImplementedException();
        }
    }
}