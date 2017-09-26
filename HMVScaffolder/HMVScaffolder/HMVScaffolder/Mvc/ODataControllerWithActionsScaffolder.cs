using EnvDTE;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using Microsoft.AspNet.Scaffolding.Mvc.Metadata;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
    public class ODataControllerWithActionsScaffolder : ControllerScaffolder<ODataFrameworkDependency>
	{
		protected internal override string TemplateFolderName
		{
			get
			{
				return "ODataControllerWithActions";
			}
		}

		public ODataControllerWithActionsScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

		protected override void AddTemplateParameters(IDictionary<string, object> templateParameters)
		{
			base.AddTemplateParameters(templateParameters);
			CodeType codeType = base.Model.ModelType.CodeType;
			ModelMetadata codeModelModelMetadatum = new CodeModelModelMetadata(codeType);
			if ((int)codeModelModelMetadatum.PrimaryKeys.Length == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The entity type '{0}' has no key defined. Define a key for this entity type.", codeType.Name));

            }
			templateParameters.Add("ModelMetadata", codeModelModelMetadatum);
			List<CodeType> codeTypes = new List<CodeType>()
			{
				codeType
			};
			templateParameters.Add("RequiredNamespaces", base.GetRequiredNamespaces(codeTypes));
			templateParameters.Add("ModelTypeNamespace", (codeType.Namespace != null ? codeType.Namespace.FullName : string.Empty));
			templateParameters.Add("ModelTypeName", codeType.Name);
			templateParameters.Add("UseAsync", base.Model.IsAsyncSelected);
			IEntityFrameworkService service = base.Context.ServiceProvider.GetService<IEntityFrameworkService>();
			string pluralizedWord = service.GetPluralizedWord(codeType.Name, CultureInfo.InvariantCulture);
			templateParameters.Add("EntitySetName", pluralizedWord);
			CodeDomProvider codeDomProvider = ValidationUtil.GenerateCodeDomProvider(ProjectExtensions.GetCodeLanguage(base.Model.ActiveProject));
			string str = codeDomProvider.CreateEscapedIdentifier(codeType.Name.ToLowerInvariantFirstChar());
			string str1 = codeDomProvider.CreateEscapedIdentifier(pluralizedWord.ToLowerInvariantFirstChar());
			templateParameters.Add("ModelVariable", str);
			templateParameters.Add("EntitySetVariable", str1);
			templateParameters.Add("ODataModificationMessage", "The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.");

            templateParameters.Add("IsLegacyOdataVersion", base.Framework.IsODataLegacy(base.Context));
		}

		protected override void OnModelCreated(ControllerScaffolderModel model)
		{
			base.OnModelCreated(model);
			model.ControllerName = null;
			model.IsAsyncSupported = true;
			model.IsModelClassSupported = true;
		}

        protected override object CreateViewModel(ControllerScaffolderModel model)
        {
            throw new NotImplementedException();
        }
    }
}