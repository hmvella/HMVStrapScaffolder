using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HMVScaffolder.Mvc
{
	internal class ProjectSettings : IProjectSettings
	{
		public string this[string key]
		{
			get
			{
				string str;
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int propertyValue = this.Storage.GetPropertyValue(key, null, 2, out str);
				if (propertyValue != -2147170504)
				{
					Marshal.ThrowExceptionForHR(propertyValue);
				}
				return str;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value != null)
				{
					int num = this.Storage.SetPropertyValue(key, null, 2, value);
					Marshal.ThrowExceptionForHR(num);
				}
				else
				{
					int num1 = this.Storage.RemoveProperty(key, null, 2);
					if (num1 != -2147170504)
					{
						Marshal.ThrowExceptionForHR(num1);
						return;
					}
				}
			}
		}

		private IVsBuildPropertyStorage Storage
		{
			get;
			set;
		}

		public ProjectSettings(IVsBuildPropertyStorage storage)
		{
			this.Storage = storage;
		}
	}
}