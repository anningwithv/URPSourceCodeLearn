using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false), VisibleToOtherModules]
	internal class PreventReadOnlyInstanceModificationAttribute : Attribute
	{
	}
}
