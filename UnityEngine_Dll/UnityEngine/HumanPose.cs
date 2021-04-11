using System;

namespace UnityEngine
{
	public struct HumanPose
	{
		public Vector3 bodyPosition;

		public Quaternion bodyRotation;

		public float[] muscles;

		internal void Init()
		{
			bool flag = this.muscles != null;
			if (flag)
			{
				bool flag2 = this.muscles.Length != HumanTrait.MuscleCount;
				if (flag2)
				{
					throw new InvalidOperationException("Bad array size for HumanPose.muscles. Size must equal HumanTrait.MuscleCount");
				}
			}
			bool flag3 = this.muscles == null;
			if (flag3)
			{
				this.muscles = new float[HumanTrait.MuscleCount];
				bool flag4 = this.bodyRotation.x == 0f && this.bodyRotation.y == 0f && this.bodyRotation.z == 0f && this.bodyRotation.w == 0f;
				if (flag4)
				{
					this.bodyRotation.w = 1f;
				}
			}
		}
	}
}
