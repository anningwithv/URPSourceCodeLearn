using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Method), VisibleToOtherModules]
	internal class ThreadAndSerializationSafeAttribute : Attribute
	{
	}
}
