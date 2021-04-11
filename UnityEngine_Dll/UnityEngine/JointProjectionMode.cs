using System;
using System.ComponentModel;

namespace UnityEngine
{
	public enum JointProjectionMode
	{
		None,
		PositionAndRotation,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("JointProjectionMode.PositionOnly is no longer supported", true)]
		PositionOnly
	}
}
