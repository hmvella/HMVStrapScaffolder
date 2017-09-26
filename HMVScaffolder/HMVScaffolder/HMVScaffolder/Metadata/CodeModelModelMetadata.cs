using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	[Serializable]
	public sealed class CodeModelModelMetadata : ModelMetadata
	{
		private static Type[] _bindableNonPrimitiveTypes;

		static CodeModelModelMetadata()
		{
			Type[] typeArray = new Type[] { typeof(string), typeof(decimal), typeof(Guid), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan) };
			CodeModelModelMetadata._bindableNonPrimitiveTypes = typeArray;
		}

		public CodeModelModelMetadata(CodeType model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			base.Properties = CodeModelModelMetadata.GetModelProperties(model).ToArray<PropertyMetadata>();
			base.PrimaryKeys = (
				from mp in CodeModelModelMetadata.GetModelProperties(model)
				where mp.IsPrimaryKey
				select mp).ToArray<PropertyMetadata>();
		}

		public CodeModelModelMetadata()
		{
		}

		private static IList<PropertyMetadata> GetModelProperties(CodeType codeType)
		{
			IList<PropertyMetadata> propertyMetadatas = new List<PropertyMetadata>();
			foreach (CodeProperty codeProperty in CodeTypeExtensions.GetPublicMembers(codeType).OfType<CodeProperty>())
			{
				if (!CodePropertyExtensions.HasPublicGetter(codeProperty) || CodePropertyExtensions.IsIndexerProperty(codeProperty) || !CodeModelModelMetadata.IsBindableType(codeProperty.Type))
				{
					continue;
				}
				propertyMetadatas.Add(new CodeModelPropertyMetadata(codeProperty));
			}
			return propertyMetadatas;
		}

		private static bool IsBindableType(CodeTypeRef type)
		{
			if (CodeTypeRefExtensions.IsPrimitiveType(type))
			{
				return true;
			}
			return CodeModelModelMetadata._bindableNonPrimitiveTypes.Any<Type>((Type x) => CodeTypeRefExtensions.IsMatchForReflectionType(type, x));
		}
	}
}