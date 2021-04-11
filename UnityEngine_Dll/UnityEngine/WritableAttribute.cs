using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false), VisibleToOtherModules]
	internal class WritableAttribute : Attribute
	{
	}
}
