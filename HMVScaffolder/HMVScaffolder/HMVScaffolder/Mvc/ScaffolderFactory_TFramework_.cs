using Microsoft.AspNet.Scaffolding;
using HMVScaffolder.Mvc.VisualStudio;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	public abstract class ScaffolderFactory<TFramework> : CodeGeneratorFactory
	where TFramework : IFrameworkDependency
	{
        [Import]
        private TFramework Framework
        {
            get;
            set;
        }

        [Import]
        private INuGetRepository Repository
        {
            get;
            set;
        }

        [Import]
        private IVisualStudioIntegration VisualStudioIntegration
        {
            get;
            set;
        }

        protected ScaffolderFactory(CodeGeneratorInformation information) : base(information)
		{
		}

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (!context.Items.ContainsProperty(typeof(TFramework)))
            {
                context.Items.AddProperty(typeof(TFramework), this.Framework);
            }
            if (!context.Items.ContainsProperty(typeof(INuGetRepository)))
            {
                context.Items.AddProperty(typeof(INuGetRepository), this.Repository);
            }
            if (!context.Items.ContainsProperty(typeof(IVisualStudioIntegration)))
            {
                context.Items.AddProperty(typeof(IVisualStudioIntegration), this.VisualStudioIntegration);
            }
            return this.CreateInstanceInternal(context);
        }

        protected abstract ICodeGenerator CreateInstanceInternal(CodeGenerationContext context);

        ////////public override bool IsSupported(CodeGenerationContext context)
        ////////{
        ////////    if (context == null)
        ////////    {
        ////////        throw new ArgumentNullException("context");
        ////////    }
        ////////    return this.Framework.IsSupported(context);
        ////////}
    }
}