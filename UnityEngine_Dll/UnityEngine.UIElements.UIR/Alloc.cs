using System;

namespace UnityEngine.UIElements.UIR
{
	internal struct Alloc
	{
		public uint start;

		public uint size;

		internal object handle;

		internal bool shortLived;
	}
}
