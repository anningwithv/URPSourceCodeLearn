using System;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public class UnityAPICompatibilityVersionAttribute : Attribute
	{
		private string _version;

		private string[] _configurationAssembliesHashes;

		public string version
		{
			get
			{
				return this._version;
			}
		}

		internal string[] configurationAssembliesHashes
		{
			get
			{
				return this._configurationAssembliesHashes;
			}
		}

		[Obsolete("This overload of the attribute has been deprecated. Use the constructor that takes the version and a boolean", true)]
		public UnityAPICompatibilityVersionAttribute(string version)
		{
			this._version = version;
		}

		public UnityAPICompatibilityVersionAttribute(string version, bool checkOnlyUnityVersion)
		{
			bool flag = !checkOnlyUnityVersion;
			if (flag)
			{
				throw new ArgumentException("You must pass 'true' to checkOnlyUnityVersion parameter.");
			}
			this._version = version;
		}

		public UnityAPICompatibilityVersionAttribute(string version, string[] configurationAssembliesHashes)
		{
			this._version = version;
			this._configurationAssembliesHashes = configurationAssembliesHashes;
		}
	}
}
