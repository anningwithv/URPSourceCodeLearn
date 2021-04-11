using System;

namespace UnityEngine.Lumin
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class UsesLuminPrivilegeAttribute : Attribute
	{
		private readonly string m_Privilege;

		public string privilege
		{
			get
			{
				return this.m_Privilege;
			}
		}

		public UsesLuminPrivilegeAttribute(string privilege)
		{
			this.m_Privilege = privilege;
		}
	}
}
