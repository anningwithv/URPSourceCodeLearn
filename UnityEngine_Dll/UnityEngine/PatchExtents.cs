using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct PatchExtents
	{
		internal float m_min;

		internal float m_max;

		public float min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		public float max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}
	}
}
