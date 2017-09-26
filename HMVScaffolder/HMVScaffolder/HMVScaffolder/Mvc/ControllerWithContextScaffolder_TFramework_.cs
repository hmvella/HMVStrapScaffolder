using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using Microsoft.AspNet.Scaffolding.NuGet;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace HMVScaffolder.Mvc
{
    public abstract class ControllerWithContextScaffolder<TFramework> : ControllerScaffolder<TFramework>
	where TFramework : IFrameworkDependency
	{
		protected ControllerWithContextScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

		protected internal override void AddScaffoldDependencies(List<NuGetPackage> packages)
		{
			base.AddScaffoldDependencies(packages);
			IEntityFrameworkService service = base.Context.ServiceProvider.GetService<IEntityFrameworkService>();
			packages.AddRange(service.Dependencies);
		}

		protected virtual IDictionary<string, object> AddTemplateParameters(CodeType dbContextType, ModelMetadata modelMetadata)
		{
			if (dbContextType == null)
			{
				throw new ArgumentNullException("dbContextType");
			}
			if (modelMetadata == null)
			{
				throw new ArgumentNullException("modelMetadata");
			}
			if (string.IsNullOrEmpty(base.Model.ControllerName))
			{
				throw new InvalidOperationException("The controller name is invalid.");
            }
			IDictionary<string, object> strs = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			CodeType codeType = base.Model.ModelType.CodeType;
			strs.Add("ModelMetadata", modelMetadata);
			strs.Add("ModelTypeNamespace", (codeType.Namespace != null ? codeType.Namespace.FullName : string.Empty));
			List<CodeType> codeTypes = new List<CodeType>()
			{
				codeType,
				dbContextType
			};
			strs.Add("RequiredNamespaces", base.GetRequiredNamespaces(codeTypes));
			strs.Add("ModelTypeName", codeType.Name);
			strs.Add("ContextTypeName", dbContextType.Name);
			strs.Add("UseAsync", base.Model.IsAsyncSelected);
			CodeDomProvider codeDomProvider = ValidationUtil.GenerateCodeDomProvider(ProjectExtensions.GetCodeLanguage(base.Model.ActiveProject));
			string str = codeDomProvider.CreateEscapedIdentifier(base.Model.ModelType.ShortTypeName.ToLowerInvariantFirstChar());
			strs.Add("ModelVariable", str);
			strs.Add("EntitySetVariable", modelMetadata.EntitySetName.ToLowerInvariantFirstChar());
			if (base.Model.IsViewGenerationSupported)
			{
				bool flag = OverpostingProtection.IsOverpostingProtectionRequired(codeType);
				strs.Add("IsOverpostingProtectionRequired", flag);
				if (flag)
				{
					strs.Add("OverpostingWarningMessage", OverpostingProtection.WarningMessage);
					strs.Add("BindAttributeIncludeText", OverpostingProtection.GetBindAttributeIncludeText(modelMetadata));
				}
			}
			return strs;
		}

		protected ModelMetadata GenerateContextAndController()
		{
			string typeName = base.Model.ModelType.TypeName;
			string str = base.Model.DataContextType.TypeName;
			IEntityFrameworkService service = base.Context.ServiceProvider.GetService<IEntityFrameworkService>();
			ModelMetadata modelMetadatum = service.AddRequiredEntity(base.Context, str, typeName);
			CodeType codeType = base.Context.ServiceProvider.GetService<ICodeTypeService>().GetCodeType(base.Context.ActiveProject, str);
			base.GenerateController(this.AddTemplateParameters(codeType, modelMetadatum));
			return modelMetadatum;
		}

		protected override void OnModelCreated(ControllerScaffolderModel model)
		{
			base.OnModelCreated(model);
			model.ControllerName = null;
			model.IsModelClassSupported = true;
			model.IsDataContextSupported = true;
		}

		protected internal override void Scaffold()
		{
			try
			{
				this.GenerateContextAndController();
			}
			finally
			{
				base.Framework.RecordControllerTelemetryOptions(base.Context, base.Model);
			}
		}
	}
}