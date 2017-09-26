using System;

namespace HMVScaffolder.Mvc
{
	internal static class MvcViewTemplates
	{
		public static string Create;

		public static string Delete;

		public static string Details;

		public static string Edit;

		public static string Empty;

		public static string List;

		public static string Index;

        public static string SelectItemsData;

        static MvcViewTemplates()
		{
			Create = "Create";
			Delete = "Delete";
            Details = "Details";
			Edit = "Edit";
			Empty = "Empty";
			List = "List";
			Index = "Index";
            SelectItemsData = "SelectItemsData";
		}
	}
}