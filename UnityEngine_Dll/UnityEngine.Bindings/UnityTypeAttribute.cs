using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Parameter), VisibleToOtherModules]
	internal class UnityTypeAttribute : Attribute, IBindingsAttribute
	{
	}
}
