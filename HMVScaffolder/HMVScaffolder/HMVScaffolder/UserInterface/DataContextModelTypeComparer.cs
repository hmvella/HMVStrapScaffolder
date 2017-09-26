using System;
using System.Collections.Generic;

namespace HMVScaffolder.Mvc
{
	internal class DataContextModelTypeComparer : Comparer<ModelType>
	{
		public DataContextModelTypeComparer()
		{
		}

		public override int Compare(ModelType x, ModelType y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return StringComparer.CurrentCulture.Compare(x.ShortTypeName, y.ShortTypeName);
		}
	}
}