using System;

namespace HMVScaffolder.Mvc
{
	internal static class NuGetPackages
	{
		public readonly static string WebApiNuGetPackageId;

		public readonly static string WebApiClientNuGetPackageId;

		public readonly static string WebApiCoreNuGetPackageId;

		public readonly static string WebApiWebHostNuGetPackageId;

		public readonly static string NewtonsoftJsonNuGetPackageId;

		public readonly static string MvcNuGetPackageId;

		public readonly static string AntlrNuGetPackageId;

		public readonly static string RazorNuGetPackageId;

		public readonly static string WebPagesNuGetPackageId;

		public readonly static string OptimizationNuGetPackageId;

		public readonly static string InfrastructureNuGetPackageId;

		public readonly static string JQueryNuGetPackageId;

		public readonly static string BootstrapNuGetPackageId;

		public readonly static string ModernizrNuGetPackageId;

		public readonly static string WebGreaseNuGetPackageId;

		public readonly static string JQueryValidationNuGetPackageId;

		public readonly static string JQueryUnobtrusiveNuGetPackageId;

		public readonly static string ODataNuGetPackageId;

		public readonly static string EdmNuGetPackageId;

		public readonly static string MicrosoftODataNuGetPackageId;

		public readonly static string SpatialNuGetPackageId;

		public readonly static string[] MvcMinimalPackageSet;

		public readonly static string[] MvcFullPackageSet;

		public readonly static string[] WebApiPackageSet;

		public readonly static string[] ODataPackageSet;

		public readonly static string[] JQueryPackageSet;

		public readonly static string[] LayoutPageDependencyPackageSet;

		static NuGetPackages()
		{
			NuGetPackages.WebApiNuGetPackageId = "Microsoft.AspNet.WebApi";
			NuGetPackages.WebApiClientNuGetPackageId = "Microsoft.AspNet.WebApi.Client";
			NuGetPackages.WebApiCoreNuGetPackageId = "Microsoft.AspNet.WebApi.Core";
			NuGetPackages.WebApiWebHostNuGetPackageId = "Microsoft.AspNet.WebApi.WebHost";
			NuGetPackages.NewtonsoftJsonNuGetPackageId = "Newtonsoft.Json";
			NuGetPackages.MvcNuGetPackageId = "Microsoft.AspNet.Mvc";
			NuGetPackages.AntlrNuGetPackageId = "Antlr";
			NuGetPackages.RazorNuGetPackageId = "Microsoft.AspNet.Razor";
			NuGetPackages.WebPagesNuGetPackageId = "Microsoft.AspNet.WebPages";
			NuGetPackages.OptimizationNuGetPackageId = "Microsoft.AspNet.Web.Optimization";
			NuGetPackages.InfrastructureNuGetPackageId = "Microsoft.Web.Infrastructure";
			NuGetPackages.JQueryNuGetPackageId = "jQuery";
			NuGetPackages.BootstrapNuGetPackageId = "bootstrap";
			NuGetPackages.ModernizrNuGetPackageId = "Modernizr";
			NuGetPackages.WebGreaseNuGetPackageId = "WebGrease";
			NuGetPackages.JQueryValidationNuGetPackageId = "jQuery.Validation";
			NuGetPackages.JQueryUnobtrusiveNuGetPackageId = "Microsoft.jQuery.Unobtrusive.Validation";
			NuGetPackages.ODataNuGetPackageId = "Microsoft.AspNet.WebApi.OData";
			NuGetPackages.EdmNuGetPackageId = "Microsoft.Data.Edm";
			NuGetPackages.MicrosoftODataNuGetPackageId = "Microsoft.Data.OData";
			NuGetPackages.SpatialNuGetPackageId = "System.Spatial";
			string[] razorNuGetPackageId = new string[] { NuGetPackages.RazorNuGetPackageId, NuGetPackages.WebPagesNuGetPackageId, NuGetPackages.InfrastructureNuGetPackageId, NuGetPackages.MvcNuGetPackageId };
			NuGetPackages.MvcMinimalPackageSet = razorNuGetPackageId;
			string[] bootstrapNuGetPackageId = new string[] { NuGetPackages.BootstrapNuGetPackageId, NuGetPackages.JQueryNuGetPackageId, NuGetPackages.JQueryValidationNuGetPackageId, NuGetPackages.JQueryUnobtrusiveNuGetPackageId, NuGetPackages.RazorNuGetPackageId, NuGetPackages.OptimizationNuGetPackageId, NuGetPackages.WebPagesNuGetPackageId, NuGetPackages.InfrastructureNuGetPackageId, NuGetPackages.MvcNuGetPackageId, NuGetPackages.ModernizrNuGetPackageId, NuGetPackages.NewtonsoftJsonNuGetPackageId, NuGetPackages.AntlrNuGetPackageId, NuGetPackages.WebGreaseNuGetPackageId };
			NuGetPackages.MvcFullPackageSet = bootstrapNuGetPackageId;
			string[] newtonsoftJsonNuGetPackageId = new string[] { NuGetPackages.NewtonsoftJsonNuGetPackageId, NuGetPackages.WebApiClientNuGetPackageId, NuGetPackages.WebApiCoreNuGetPackageId, NuGetPackages.WebApiWebHostNuGetPackageId, NuGetPackages.WebApiNuGetPackageId };
			NuGetPackages.WebApiPackageSet = newtonsoftJsonNuGetPackageId;
			string[] edmNuGetPackageId = new string[] { NuGetPackages.EdmNuGetPackageId, NuGetPackages.SpatialNuGetPackageId, NuGetPackages.MicrosoftODataNuGetPackageId, NuGetPackages.ODataNuGetPackageId };
			NuGetPackages.ODataPackageSet = edmNuGetPackageId;
			string[] jQueryNuGetPackageId = new string[] { NuGetPackages.JQueryNuGetPackageId, NuGetPackages.JQueryValidationNuGetPackageId, NuGetPackages.JQueryUnobtrusiveNuGetPackageId };
			NuGetPackages.JQueryPackageSet = jQueryNuGetPackageId;
			string[] strArrays = new string[] { NuGetPackages.BootstrapNuGetPackageId, NuGetPackages.JQueryNuGetPackageId, NuGetPackages.ModernizrNuGetPackageId };
			NuGetPackages.LayoutPageDependencyPackageSet = strArrays;
		}
	}
}