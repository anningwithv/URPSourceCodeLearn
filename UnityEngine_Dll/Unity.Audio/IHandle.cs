using System;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[VisibleToOtherModules]
	internal interface IHandle<HandleType> : IValidatable, IEquatable<HandleType> where HandleType : struct, IHandle<HandleType>
	{
	}
}
