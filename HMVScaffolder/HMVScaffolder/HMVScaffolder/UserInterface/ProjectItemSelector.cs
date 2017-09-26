using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HMVScaffolder.Mvc
{
    internal static class ProjectItemSelector
    {
        [Flags]
        internal enum ProjectItemSelectorFlags
        {
            PISF_ReturnAppRelativeUrls,
            PSIF_ReturnDocRelativeUrls
        }


        [Guid("EDDE1B36-C493-4cbe-B75C-762947CFE068")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IProjectItemSelector
        {
            [DispId(1)]
            [MethodImpl(MethodImplOptions.PreserveSig)]
            int SelectItem([In] IVsHierarchy hierarchy, [In] uint itemID, [In] string filters, [In] string dlgTitle, [In] ProjectItemSelectorFlags flags, [In] string relUrlToAnchor, [In] string relUrlToSelect, [In] string baseUrl, out string relUrlOfSelectedItem, out bool canceled);
        }

        private static int SelectItem(IVsHierarchy hierarchy, string filter, string title, string preselectedItem, out string appRelUrlOfSelectedItem, out bool canceled)
        {
            appRelUrlOfSelectedItem = null;
            canceled = false;
            int site = -2147467259;
            if (hierarchy != null)
            {
                Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider = null;
                site = hierarchy.GetSite(out serviceProvider);
                if (NativeMethods.Succeeded(site) && serviceProvider != null)
                {
                    IProjectItemSelector projectItemSelector = serviceProvider.CreateSitedInstance<IProjectItemSelector>(typeof(IProjectItemSelector).GUID);
                    if (projectItemSelector != null)
                    {
                        site = projectItemSelector.SelectItem(hierarchy, Convert.ToUInt32(-1), filter, title, ProjectItemSelectorFlags.PISF_ReturnAppRelativeUrls, null, preselectedItem, null, out appRelUrlOfSelectedItem, out canceled);
                    }
                }
            }
            return site;
        }

        public static bool TrySelectItem(IVsHierarchy hierarchy, string title, string filter, string preselectedItem, out string relativePath)
        {
            bool flag;
            if (hierarchy == null)
            {
                throw new ArgumentNullException("hierarchy");
            }
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (!NativeMethods.Succeeded(ProjectItemSelector.SelectItem(hierarchy, filter, title, preselectedItem, out relativePath, out flag)))
            {
                return false;
            }
            return !flag;
        }
    }
}