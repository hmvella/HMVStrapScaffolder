using System;
using System.Runtime.CompilerServices;

namespace HMVScaffolder.Mvc
{
	public class FrameworkDependencyStatus
	{
		public readonly static FrameworkDependencyStatus InstallSuccessful;

		public readonly static FrameworkDependencyStatus InstallNotNeeded;

		public bool IsNewDependencyInstall
		{
			get;
			private set;
		}

		public bool IsReadmeRequired
		{
			get;
			private set;
		}

		public string ReadmeText
		{
			get;
			private set;
		}

		static FrameworkDependencyStatus()
		{
			FrameworkDependencyStatus.InstallSuccessful = new FrameworkDependencyStatus()
			{
				IsNewDependencyInstall = true
			};
			FrameworkDependencyStatus.InstallNotNeeded = new FrameworkDependencyStatus();
		}

		private FrameworkDependencyStatus()
		{
		}

		public static FrameworkDependencyStatus FromReadme(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			FrameworkDependencyStatus frameworkDependencyStatu = new FrameworkDependencyStatus()
			{
				IsNewDependencyInstall = true,
				IsReadmeRequired = true,
				ReadmeText = text
			};
			return frameworkDependencyStatu;
		}
	}
}