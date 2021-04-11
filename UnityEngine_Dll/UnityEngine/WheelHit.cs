using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Vehicles/WheelCollider.h")]
	public struct WheelHit
	{
		[NativeName("point")]
		private Vector3 m_Point;

		[NativeName("normal")]
		private Vector3 m_Normal;

		[NativeName("forwardDir")]
		private Vector3 m_ForwardDir;

		[NativeName("sidewaysDir")]
		private Vector3 m_SidewaysDir;

		[NativeName("force")]
		private float m_Force;

		[NativeName("forwardSlip")]
		private float m_ForwardSlip;

		[NativeName("sidewaysSlip")]
		private float m_SidewaysSlip;

		[NativeName("collider")]
		private Collider m_Collider;

		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
			set
			{
				this.m_Collider = value;
			}
		}

		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		public Vector3 forwardDir
		{
			get
			{
				return this.m_ForwardDir;
			}
			set
			{
				this.m_ForwardDir = value;
			}
		}

		public Vector3 sidewaysDir
		{
			get
			{
				return this.m_SidewaysDir;
			}
			set
			{
				this.m_SidewaysDir = value;
			}
		}

		public float force
		{
			get
			{
				return this.m_Force;
			}
			set
			{
				this.m_Force = value;
			}
		}

		public float forwardSlip
		{
			get
			{
				return this.m_ForwardSlip;
			}
			set
			{
				this.m_ForwardSlip = value;
			}
		}

		public float sidewaysSlip
		{
			get
			{
				return this.m_SidewaysSlip;
			}
			set
			{
				this.m_SidewaysSlip = value;
			}
		}
	}
}
