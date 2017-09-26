using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HMVScaffolder.Mvc
{
	internal static class OleServiceProviderExtensions
	{
		internal static InterfaceType CreateInstance<InterfaceType>(System.IServiceProvider serviceProvider, Guid clsid)
		where InterfaceType : class
		{
			InterfaceType objectForIUnknown = default(InterfaceType);
			if (clsid != Guid.Empty)
			{
				ILocalRegistry service = serviceProvider.GetService<ILocalRegistry>();
				if (service != null)
				{
					IntPtr zero = IntPtr.Zero;
					Guid iDIUnknown = NativeMethods.IID_IUnknown;
					if (!NativeMethods.Succeeded(service.CreateInstance(clsid, null, ref iDIUnknown, 1, out zero)) || zero == IntPtr.Zero)
					{
						return default(InterfaceType);
					}
					objectForIUnknown = (InterfaceType)(Marshal.GetObjectForIUnknown(zero) as InterfaceType);
					Marshal.ThrowExceptionForHR(Marshal.Release(zero));
				}
			}
			return objectForIUnknown;
		}

		public static InterfaceType CreateSitedInstance<InterfaceType>(this Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider, Guid clsid)
		where InterfaceType : class
		{
			InterfaceType interfaceType;
			using (ServiceProvider serviceProvider1 = new ServiceProvider(serviceProvider))
			{
				interfaceType = OleServiceProviderExtensions.CreateSitedInstance<InterfaceType>(serviceProvider1, clsid);
			}
			return interfaceType;
		}

		internal static InterfaceType CreateSitedInstance<InterfaceType>(System.IServiceProvider serviceProvider, Guid clsid)
		where InterfaceType : class
		{
			InterfaceType interfaceType = OleServiceProviderExtensions.CreateInstance<InterfaceType>(serviceProvider, clsid);
			if (interfaceType != null)
			{
				IObjectWithSite objectWithSite = (object)interfaceType as IObjectWithSite;
				Microsoft.VisualStudio.OLE.Interop.IServiceProvider service = serviceProvider.GetService<Microsoft.VisualStudio.OLE.Interop.IServiceProvider>();
				if (objectWithSite == null || service == null)
				{
					interfaceType = default(InterfaceType);
				}
				else
				{
					objectWithSite.SetSite(service);
				}
			}
			return interfaceType;
		}
	}
}