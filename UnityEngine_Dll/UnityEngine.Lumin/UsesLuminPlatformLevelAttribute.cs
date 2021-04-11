using System;

namespace UnityEngine.Lumin
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class UsesLuminPlatformLevelAttribute : Attribute
	{
		private readonly uint m_PlatformLevel;

		public uint platformLevel
		{
			get
			{
				return this.m_PlatformLevel;
			}
		}

		public UsesLuminPlatformLevelAttribute(uint platformLevel)
		{
			this.m_PlatformLevel = platformLevel;
		}
	}
}
