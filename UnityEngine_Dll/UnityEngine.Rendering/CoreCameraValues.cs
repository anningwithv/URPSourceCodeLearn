using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	internal struct CoreCameraValues : IEquatable<CoreCameraValues>
	{
		private int filterMode;

		private uint cullingMask;

		private int instanceID;

		public bool Equals(CoreCameraValues other)
		{
			return this.filterMode == other.filterMode && this.cullingMask == other.cullingMask && this.instanceID == other.instanceID;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CoreCameraValues && this.Equals((CoreCameraValues)obj);
		}

		public override int GetHashCode()
		{
			int num = this.filterMode;
			num = (num * 397 ^ (int)this.cullingMask);
			return num * 397 ^ this.instanceID;
		}

		public static bool operator ==(CoreCameraValues left, CoreCameraValues right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(CoreCameraValues left, CoreCameraValues right)
		{
			return !left.Equals(right);
		}
	}
}
