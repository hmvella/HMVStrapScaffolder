using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HMVScaffolder.Mvc
{
	internal static class BitmapExtensions
	{
		public static ImageSource ToImageSource(this Bitmap bitmap)
		{
			ImageSource imageSource;
			if (bitmap == null)
			{
				throw new ArgumentNullException("bitmap");
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, ImageFormat.Png);
				memoryStream.Position = (long)0;
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.StreamSource = memoryStream;
				bitmapImage.EndInit();
				if (bitmapImage.CanFreeze)
				{
					bitmapImage.Freeze();
				}
				imageSource = bitmapImage;
			}
			return imageSource;
		}
	}
}