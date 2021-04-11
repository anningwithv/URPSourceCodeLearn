using System;

namespace UnityEngine.UIElements
{
	[Flags]
	internal enum RenderHints
	{
		None = 0,
		GroupTransform = 1,
		BoneTransform = 2,
		ClipWithScissors = 4
	}
}
