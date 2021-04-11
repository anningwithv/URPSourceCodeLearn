using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false), VisibleToOtherModules]
	internal class VisibleToOtherModulesAttribute : Attribute
	{
		public VisibleToOtherModulesAttribute()
		{
		}

		public VisibleToOtherModulesAttribute(params string[] modules)
		{
		}
	}
}
