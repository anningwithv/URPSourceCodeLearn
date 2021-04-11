using System;

namespace UnityEngine.XR
{
	[Flags]
	public enum InputDeviceCharacteristics : uint
	{
		None = 0u,
		HeadMounted = 1u,
		Camera = 2u,
		HeldInHand = 4u,
		HandTracking = 8u,
		EyeTracking = 16u,
		TrackedDevice = 32u,
		Controller = 64u,
		TrackingReference = 128u,
		Left = 256u,
		Right = 512u,
		Simulated6DOF = 1024u
	}
}
