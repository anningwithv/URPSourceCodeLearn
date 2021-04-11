using System;

namespace UnityEngine.iOS
{
	[Flags]
	public enum SystemGestureDeferMode : uint
	{
		None = 0u,
		TopEdge = 1u,
		LeftEdge = 2u,
		BottomEdge = 4u,
		RightEdge = 8u,
		All = 15u
	}
}
