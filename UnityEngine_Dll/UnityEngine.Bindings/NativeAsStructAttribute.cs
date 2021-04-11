using System;

namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Class), VisibleToOtherModules]
	internal class NativeAsStructAttribute : Attribute, IBindingsAttribute
	{
	}
}
