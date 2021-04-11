using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Parameter), VisibleToOtherModules]
	internal class UnmarshalledAttribute : Attribute, IBindingsAttribute
	{
	}
}
