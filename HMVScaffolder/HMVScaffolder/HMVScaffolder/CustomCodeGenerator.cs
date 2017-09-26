using HMVScaffolder.Mvc;
using Microsoft.AspNet.Scaffolding;

using HMVScaffolder.Mvc;

using System;
using System.Collections.Generic;
using HMVScaffolder.UI;

namespace HMVScaffolder.Mvc
{
    public class CustomCodeGenerator : CodeGenerator
    {
        private CustomViewModel _viewModel;

        /// <summary>
        /// Constructor for the custom code generator
        /// </summary>
        /// <param name="context">Context of the current code generation operation based on how scaffolder was invoked(such as selected project/folder) </param>
        /// <param name="information">Code generation information that is defined in the factory class.</param>
        public CustomCodeGenerator(
            CodeGenerationContext context,
            CodeGeneratorInformation information)
            : base(context, information)
        {
            try
            {
                _viewModel = new CustomViewModel(Context);
            }
            catch (Exception exc)
            {
            }
        }

        /// <summary>
        /// Any UI to be displayed after the scaffolder has been selected from the Add Scaffold dialog.
        /// Any validation on the input for values in the UI should be completed before returning from this method.
        /// </summary>
        /// <returns></returns>
        public override bool ShowUIAndValidate()
        {
            // Bring up the selection dialog and allow user to select a model type
            SelectModelWindow window = new SelectModelWindow(_viewModel);
            bool? showDialog = window.ShowDialog();
            return showDialog ?? false;
        }

        /// <summary>
        /// This method is executed after the ShowUIAndValidate method, and this is where the actual code generation should occur.
        /// In this example, we are generating a new file from t4 template based on the ModelType selected in our UI.
        /// </summary>
        public override void GenerateCode()
        {
            // Get the selected code type
            var codeType = _viewModel.SelectedModelType.CodeType;

            Dictionary<string, object> dsd = new Dictionary<string, object>();
            dsd.Add("test", "uhu");

            // Add the custom scaffolding item from T4 template.
            this.AddFileFromTemplate(Context.ActiveProject,
                "MVCBootstrapServerTable",
                "CustomTextTemplate",
                dsd,
                skipIfExists: false);
        }
    }
}