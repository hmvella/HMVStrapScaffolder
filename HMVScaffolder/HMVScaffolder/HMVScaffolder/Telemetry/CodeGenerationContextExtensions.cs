using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc.Telemetry
{
	internal static class CodeGenerationContextExtensions
	{
		public static void AddTelemetryData(this CodeGenerationContext context, string key, object value)
		{
			Dictionary<string, object> strs = null;
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!context.Items.TryGetProperty<Dictionary<string, object>>("MSInternal_MvcInfo", out strs))
			{
				strs = new Dictionary<string, object>();
				context.Items.AddProperty("MSInternal_MvcInfo", strs);
			}
			strs[key] = value;
		}
	}
}