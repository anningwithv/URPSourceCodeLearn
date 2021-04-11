using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class TrackedReference
	{
		internal IntPtr m_Ptr;

		protected TrackedReference()
		{
		}

		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			bool flag = y == null && x == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = y == null;
				if (flag2)
				{
					result = (x.m_Ptr == IntPtr.Zero);
				}
				else
				{
					bool flag3 = x == null;
					if (flag3)
					{
						result = (y.m_Ptr == IntPtr.Zero);
					}
					else
					{
						result = (x.m_Ptr == y.m_Ptr);
					}
				}
			}
			return result;
		}

		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}
	}
}
