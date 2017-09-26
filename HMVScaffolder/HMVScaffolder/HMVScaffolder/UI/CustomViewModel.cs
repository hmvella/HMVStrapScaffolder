using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace HMVScaffolder.Mvc
{
    /// <summary>
    /// View model for code types so that it can be displayed on the UI.
    /// </summary>
    public class CustomViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The code generation context</param>
        public CustomViewModel(CodeGenerationContext context)
        {
            Context = context;
        }

        /// <summary>
        /// This gets all the Model types from the active project.
        /// </summary>
        public IEnumerable<ModelType> ModelTypes
        {
            get
            {
                ICodeTypeService codeTypeService = (ICodeTypeService)Context
                    .ServiceProvider.GetService(typeof(ICodeTypeService));

                var _code = codeTypeService
                            .GetAllCodeTypes(Context.ActiveProject)
                            .Where(codeType => codeType.IsValidWebProjectEntityType())
                            .Select(codeType => new ModelType(codeType));

                return _code;
            }
        }

        public ModelType SelectedModelType { get; set; }

        public CodeGenerationContext Context { get; private set; }
    }
}