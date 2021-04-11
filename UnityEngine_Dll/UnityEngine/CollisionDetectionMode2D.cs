using System;
using System.ComponentModel;

namespace UnityEngine
{
	public enum CollisionDetectionMode2D
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member CollisionDetectionMode2D.None has been deprecated. Use CollisionDetectionMode2D.Discrete instead (UnityUpgradable) -> Discrete", true)]
		None,
		Discrete = 0,
		Continuous
	}
}
