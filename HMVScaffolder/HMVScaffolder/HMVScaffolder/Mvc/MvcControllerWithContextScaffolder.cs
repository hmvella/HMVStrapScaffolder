using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using Microsoft.AspNet.Scaffolding.NuGet;
using System;
using System.Collections.Generic;

namespace HMVScaffolder.Mvc
{
	public class MvcControllerWithContextScaffolder : ControllerWithContextScaffolder<MvcFrameworkDependency>
	{
		private readonly string[] _viewNames = new string[] { MvcViewTemplates.Create, MvcViewTemplates.Delete, MvcViewTemplates.Details, MvcViewTemplates.Edit, MvcViewTemplates.Index, MvcViewTemplates.SelectItemsData };

		protected internal override string TemplateFolderName
		{
			get
			{
				return "MvcControllerWithContext";
			}
		}

		public MvcControllerWithContextScaffolder(CodeGenerationContext context, CodeGeneratorInformation information) : base(context, information)
		{
		}

		protected internal override void AddRuntimePackages(List<NuGetPackage> packages)
		{
			base.AddRuntimePackages(packages);
			MvcViewScaffolderFactory mvcViewScaffolderFactory = new MvcViewScaffolderFactory();
			((MvcViewScaffolder)mvcViewScaffolderFactory.CreateInstance(base.Context)).AddRuntimePackages(packages);
		}

		protected internal override void AddScaffoldDependencies(List<NuGetPackage> packages)
		{
			base.AddScaffoldDependencies(packages);
			MvcViewScaffolderFactory mvcViewScaffolderFactory = new MvcViewScaffolderFactory();
			((MvcViewScaffolder)mvcViewScaffolderFactory.CreateInstance(base.Context)).AddScaffoldDependencies(packages);
		}

		protected override void OnModelCreated(ControllerScaffolderModel model)
		{
			base.OnModelCreated(model);
			model.IsViewFolderRequired = true;
			model.IsViewGenerationSupported = true;
		}

		protected internal override void Scaffold()
		{
			try
			{
				ModelMetadata modelMetadatum = base.GenerateContextAndController();
				if (base.Model.IsViewGenerationSelected)
				{
					string[] strArrays = this._viewNames;
					for (int i = 0; i < (int)strArrays.Length; i++)
					{
						string str = strArrays[i];
						MvcViewScaffolder mvcViewScaffolder = (MvcViewScaffolder)(new MvcViewScaffolderFactory()).CreateInstance(base.Context);
						mvcViewScaffolder.GenerateViewForController(base.Model, modelMetadatum, str);
					}
				}
			}
			finally
			{
				base.Framework.RecordControllerTelemetryOptions(base.Context, base.Model);
			}
		}


    }
}