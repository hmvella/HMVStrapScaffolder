using HMVScaffolder.Properties;
using System.Windows.Media;

namespace HMVScaffolder.Mvc
{
    internal static class ScaffolderIcons
	{
		public readonly static ImageSource Area;

		public readonly static ImageSource Controller;

		public readonly static ImageSource ControllerWithViews;

		public readonly static ImageSource Views;

		static ScaffolderIcons()
		{
			ScaffolderIcons.Area = Resources.AreaIcon.ToImageSource();
			ScaffolderIcons.Controller = Resources.ControllerIcon.ToImageSource();
			ScaffolderIcons.ControllerWithViews = Resources.ControllerWithViewsIcon.ToImageSource();
			ScaffolderIcons.Views = Resources.ViewsIcon.ToImageSource();
		}
	}
}