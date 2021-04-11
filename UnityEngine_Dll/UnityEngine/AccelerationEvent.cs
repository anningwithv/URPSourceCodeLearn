using System;

namespace UnityEngine
{
	public struct AccelerationEvent
	{
		internal float x;

		internal float y;

		internal float z;

		internal float m_TimeDelta;

		public Vector3 acceleration
		{
			get
			{
				return new Vector3(this.x, this.y, this.z);
			}
		}

		public float deltaTime
		{
			get
			{
				return this.m_TimeDelta;
			}
		}
	}
}
