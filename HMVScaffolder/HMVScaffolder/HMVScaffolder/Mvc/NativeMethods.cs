using System;

namespace HMVScaffolder.Mvc
{
	internal static class NativeMethods
	{
		public const int E_FAIL = -2147467259;

		public const int E_XML_ATTRIBUTE_NOT_FOUND = -2147170504;

		public const int S_OK = 0;

		public const int CLSCTX_INPROC_SERVER = 1;

		public readonly static Guid IID_IUnknown;

		static NativeMethods()
		{
			NativeMethods.IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
		}

		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}
	}
}