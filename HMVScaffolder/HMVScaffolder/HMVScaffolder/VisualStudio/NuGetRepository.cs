using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace HMVScaffolder.Mvc
{
    [Export(typeof(INuGetRepository))]
	internal class NuGetRepository : INuGetRepository
	{
		private const string WSRNuGetPackagesRegistryKeyName = "WebStackVS15";

		private readonly static NuGetRegistryRepository _repository;

		static NuGetRepository()
		{
			NuGetRepository._repository = new NuGetRegistryRepository("WebStackVS15", true);
		}

		public NuGetRepository()
		{
		}

		public NuGetPackage GetPackage(CodeGenerationContext context, string id)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			string packageVersion = this.GetPackageVersion(context, id);
			return new NuGetPackage(id, packageVersion, NuGetRepository._repository);
		}

		public string GetPackageVersion(CodeGenerationContext context, string id)
		{
			string str;
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			IDictionary<string, string> packageVersions = PackageVersions.GetPackageVersions(context);
			packageVersions.TryGetValue(id, out str);
			return str;
		}
	}
}