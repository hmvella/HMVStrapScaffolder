using System;
using System.Reflection;

namespace HMVScaffolder.Mvc
{
	public interface IProjectSettings
	{
		string this[string key]
		{
			get;
			set;
		}
	}
}