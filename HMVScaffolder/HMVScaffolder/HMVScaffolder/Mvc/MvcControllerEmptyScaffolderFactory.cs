using HMVScaffolder.Properties;
using Microsoft.AspNet.Scaffolding;
using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HMVScaffolder.Mvc
{
    [Export(typeof(CodeGeneratorFactory))]
	public class MvcControllerEmptyScaffolderFactory : ScaffolderFactory<MvcFrameworkDependency>
	{

        public MvcControllerEmptyScaffolderFactory() : base(new CodeGeneratorInformation("test mvc decompile empty test2", "test test", "Justin Farrugia", ScaffolderVersions.MvcScaffolderVersion, "MvcControllerEmptyScaffolder", ScaffolderIcons.ControllerWithViews, new string[] { "Controller" }, new string[] { Categories.Common, Categories.Mvc, Categories.MvcController }))
        {
		}

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new MvcControllerEmptyScaffolder(context, base.Information);
        }

        protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
        {
            return new MvcControllerEmptyScaffolder(context, base.Information);
        }

        /// <summary>
        /// Provides a way to check if the custom scaffolder is valid under this context
        /// </summary>
        /// <param name="codeGenerationContext">The code generation context</param>
        /// <returns>True if valid, False otherwise</returns>
        public override bool IsSupported(CodeGenerationContext codeGenerationContext)
        {
            if (codeGenerationContext.ActiveProject.CodeModel.Language != EnvDTE.CodeModelLanguageConstants.vsCMLanguageCSharp)
            {
                return false;
            }

            return true;
        }



        /// <summary>
        /// Helper method to convert Icon to Imagesource.
        /// </summary>
        /// <param name="icon">Icon</param>
        /// <returns>Imagesource</returns>
        public static ImageSource ToImageSource(Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
    }
}