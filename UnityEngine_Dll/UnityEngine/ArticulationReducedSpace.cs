using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public struct ArticulationReducedSpace
	{
		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		public struct <x>e__FixedBuffer
		{
			public float FixedElementField;
		}

		[FixedBuffer(typeof(float), 3)]
		private ArticulationReducedSpace.<x>e__FixedBuffer x;

		public int dofCount;

		public unsafe float this[int i]
		{
			get
			{
				bool flag = i < 0 || i >= this.dofCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return *(ref this.x.FixedElementField + (IntPtr)i * 4);
			}
			set
			{
				bool flag = i < 0 || i >= this.dofCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				*(ref this.x.FixedElementField + (IntPtr)i * 4) = value;
			}
		}

		public ArticulationReducedSpace(float a)
		{
			this.x.FixedElementField = a;
			this.dofCount = 1;
		}

		public unsafe ArticulationReducedSpace(float a, float b)
		{
			this.x.FixedElementField = a;
			*(ref this.x.FixedElementField + 4) = b;
			this.dofCount = 2;
		}

		public unsafe ArticulationReducedSpace(float a, float b, float c)
		{
			this.x.FixedElementField = a;
			*(ref this.x.FixedElementField + 4) = b;
			*(ref this.x.FixedElementField + (IntPtr)2 * 4) = c;
			this.dofCount = 3;
		}
	}
}
