using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Struct), VisibleToOtherModules]
	internal class IL2CPPStructAlignmentAttribute : Attribute
	{
		public int Align;

		public IL2CPPStructAlignmentAttribute()
		{
			this.Align = 1;
		}
	}
}
