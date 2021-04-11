using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public struct ArticulationDrive
	{
		public float lowerLimit;

		public float upperLimit;

		public float stiffness;

		public float damping;

		public float forceLimit;

		public float target;

		public float targetVelocity;
	}
}
