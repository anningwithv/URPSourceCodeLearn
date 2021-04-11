using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/BaseClasses/BitField.h"), NativeHeader("Runtime/BaseClasses/TagManager.h"), NativeClass("BitField", "struct BitField;"), RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct LayerMask
	{
		[NativeName("m_Bits")]
		private int m_Mask;

		public int value
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		public static implicit operator LayerMask(int intVal)
		{
			LayerMask result;
			result.m_Mask = intVal;
			return result;
		}

		[NativeMethod("LayerToString"), StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string LayerToName(int layer);

		[NativeMethod("StringToLayer"), StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NameToLayer(string layerName);

		public static int GetMask(params string[] layerNames)
		{
			bool flag = layerNames == null;
			if (flag)
			{
				throw new ArgumentNullException("layerNames");
			}
			int num = 0;
			for (int i = 0; i < layerNames.Length; i++)
			{
				string layerName = layerNames[i];
				int num2 = LayerMask.NameToLayer(layerName);
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}
	}
}
