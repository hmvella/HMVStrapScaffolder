using System;

namespace HMVScaffolder.Mvc
{
	internal static class TypeNames
	{
		public readonly static string KeyAttributeTypeName;

		public readonly static string ColumnAttributeTypeName;

		public readonly static string EdmScalarPropertyAttributeTypeName;

		public readonly static string ScaffoldColumnAttributeTypeName;

		static TypeNames()
		{
			TypeNames.KeyAttributeTypeName = "System.ComponentModel.DataAnnotations.KeyAttribute";
			TypeNames.ColumnAttributeTypeName = "System.Data.Linq.Mapping.ColumnAttribute";
			TypeNames.EdmScalarPropertyAttributeTypeName = "System.Data.Entity.Core.Objects.DataClasses.EdmScalarPropertyAttribute";
			TypeNames.ScaffoldColumnAttributeTypeName = "System.ComponentModel.DataAnnotations.ScaffoldColumnAttribute";
		}
	}
}