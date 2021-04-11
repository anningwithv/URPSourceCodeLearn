using System;

namespace UnityEngine.XR
{
	[Flags]
	public enum InputTrackingState : uint
	{
		None = 0u,
		Position = 1u,
		Rotation = 2u,
		Velocity = 4u,
		AngularVelocity = 8u,
		Acceleration = 16u,
		AngularAcceleration = 32u,
		All = 63u
	}
}
