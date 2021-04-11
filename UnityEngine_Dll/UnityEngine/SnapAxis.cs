using System;

namespace UnityEngine
{
	[Flags]
	public enum SnapAxis : byte
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4,
		All = 7
	}
}
