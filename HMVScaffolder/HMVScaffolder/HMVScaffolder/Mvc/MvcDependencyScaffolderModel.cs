using Microsoft.AspNet.Scaffolding;
using System;

namespace Microsoft.AspNet.Scaffolding.Mvc
{
	public class MvcDependencyScaffolderModel : ScaffolderModel
	{
		public override string OutputFileFullPath
		{
			get
			{
				return null;
			}
		}

		public MvcDependencyScaffolderModel(CodeGenerationContext context) : base(context)
		{
		}
	}
}