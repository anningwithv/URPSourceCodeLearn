using System;

namespace UnityEngine.XR
{
	internal enum InputFeatureType : uint
	{
		Custom,
		Binary,
		DiscreteStates,
		Axis1D,
		Axis2D,
		Axis3D,
		Rotation,
		Hand,
		Bone,
		Eyes,
		kUnityXRInputFeatureTypeInvalid = 4294967295u
	}
}
