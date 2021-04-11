using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Hashing/Hash128.bindings.h"), NativeHeader("Runtime/Utilities/Hash128.h"), UsedByNativeCode]
	[Serializable]
	public struct Hash128 : IComparable, IComparable<Hash128>, IEquatable<Hash128>
	{
		private uint m_u32_0;

		private uint m_u32_1;

		private uint m_u32_2;

		private uint m_u32_3;

		private const ulong kConst = 16045690984833335023uL;

		internal unsafe ulong u64_0
		{
			get
			{
				uint* ptr = &this.m_u32_0;
				return (ulong)(*(long*)ptr);
			}
		}

		internal unsafe ulong u64_1
		{
			get
			{
				uint* ptr = &this.m_u32_2;
				return (ulong)(*(long*)ptr);
			}
		}

		public bool isValid
		{
			get
			{
				return this.m_u32_0 != 0u || this.m_u32_1 != 0u || this.m_u32_2 != 0u || this.m_u32_3 > 0u;
			}
		}

		public Hash128(uint u32_0, uint u32_1, uint u32_2, uint u32_3)
		{
			this.m_u32_0 = u32_0;
			this.m_u32_1 = u32_1;
			this.m_u32_2 = u32_2;
			this.m_u32_3 = u32_3;
		}

		public unsafe Hash128(ulong u64_0, ulong u64_1)
		{
			uint* ptr = (uint*)(&u64_0);
			uint* ptr2 = (uint*)(&u64_1);
			this.m_u32_0 = *ptr;
			this.m_u32_1 = ptr[1];
			this.m_u32_2 = *ptr2;
			this.m_u32_3 = ptr2[1];
		}

		public int CompareTo(Hash128 rhs)
		{
			bool flag = this < rhs;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = this > rhs;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		public override string ToString()
		{
			return Hash128.Hash128ToStringImpl(this);
		}

		[FreeFunction("StringToHash128", IsThreadSafe = true)]
		public static Hash128 Parse(string hashString)
		{
			Hash128 result;
			Hash128.Parse_Injected(hashString, out result);
			return result;
		}

		[FreeFunction("Hash128ToString", IsThreadSafe = true)]
		private static string Hash128ToStringImpl(Hash128 hash)
		{
			return Hash128.Hash128ToStringImpl_Injected(ref hash);
		}

		[FreeFunction("ComputeHash128FromScriptString", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ComputeFromString(string data, ref Hash128 hash);

		[FreeFunction("ComputeHash128FromScriptPointer", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ComputeFromPtr(IntPtr data, int start, int count, int elemSize, ref Hash128 hash);

		[FreeFunction("ComputeHash128FromScriptArray", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ComputeFromArray(Array data, int start, int count, int elemSize, ref Hash128 hash);

		public static Hash128 Compute(string data)
		{
			Hash128 result = default(Hash128);
			Hash128.ComputeFromString(data, ref result);
			return result;
		}

		public static Hash128 Compute<T>(NativeArray<T> data) where T : struct
		{
			Hash128 result = default(Hash128);
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, data.Length, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		public static Hash128 Compute<T>(NativeArray<T> data, int start, int count) where T : struct
		{
			bool flag = start < 0 || count < 0 || start + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), start, count, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		public static Hash128 Compute<T>(T[] data) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Compute must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromArray(data, 0, data.Length, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		public static Hash128 Compute<T>(T[] data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Compute must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromArray(data, start, count, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		public static Hash128 Compute<T>(List<T> data) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Compute", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), 0, data.Count, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		public static Hash128 Compute<T>(List<T> data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Compute", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), start, count, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		public unsafe static Hash128 Compute<[IsUnmanaged] T>(ref T val) where T : struct, ValueType
		{
			void* value = (void*)(&val);
			Hash128 result = default(Hash128);
			Hash128.ComputeFromPtr((IntPtr)value, 0, 1, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		public static Hash128 Compute(int val)
		{
			Hash128 result = default(Hash128);
			result.Append(val);
			return result;
		}

		public static Hash128 Compute(float val)
		{
			Hash128 result = default(Hash128);
			result.Append(val);
			return result;
		}

		public unsafe static Hash128 Compute(void* data, ulong size)
		{
			Hash128 result = default(Hash128);
			Hash128.ComputeFromPtr(new IntPtr(data), 0, (int)size, 1, ref result);
			return result;
		}

		public void Append(string data)
		{
			Hash128.ComputeFromString(data, ref this);
		}

		public void Append<T>(NativeArray<T> data) where T : struct
		{
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, data.Length, UnsafeUtility.SizeOf<T>(), ref this);
		}

		public void Append<T>(NativeArray<T> data, int start, int count) where T : struct
		{
			bool flag = start < 0 || count < 0 || start + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		public void Append<T>(T[] data) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Append must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			Hash128.ComputeFromArray(data, 0, data.Length, UnsafeUtility.SizeOf<T>(), ref this);
		}

		public void Append<T>(T[] data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Append must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromArray(data, start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		public void Append<T>(List<T> data) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Append", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), 0, data.Count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		public void Append<T>(List<T> data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Append", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		public unsafe void Append<[IsUnmanaged] T>(ref T val) where T : struct, ValueType
		{
			fixed (T* ptr = &val)
			{
				void* value = (void*)ptr;
				Hash128.ComputeFromPtr((IntPtr)value, 0, 1, UnsafeUtility.SizeOf<T>(), ref this);
			}
		}

		public void Append(int val)
		{
			this.ShortHash4((uint)val);
		}

		public unsafe void Append(float val)
		{
			this.ShortHash4(*(uint*)(&val));
		}

		public unsafe void Append(void* data, ulong size)
		{
			Hash128.ComputeFromPtr(new IntPtr(data), 0, (int)size, 1, ref this);
		}

		public override bool Equals(object obj)
		{
			return obj is Hash128 && this == (Hash128)obj;
		}

		public bool Equals(Hash128 obj)
		{
			return this == obj;
		}

		public override int GetHashCode()
		{
			return this.m_u32_0.GetHashCode() ^ this.m_u32_1.GetHashCode() ^ this.m_u32_2.GetHashCode() ^ this.m_u32_3.GetHashCode();
		}

		public int CompareTo(object obj)
		{
			bool flag = obj == null || !(obj is Hash128);
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				Hash128 rhs = (Hash128)obj;
				result = this.CompareTo(rhs);
			}
			return result;
		}

		public static bool operator ==(Hash128 hash1, Hash128 hash2)
		{
			return hash1.m_u32_0 == hash2.m_u32_0 && hash1.m_u32_1 == hash2.m_u32_1 && hash1.m_u32_2 == hash2.m_u32_2 && hash1.m_u32_3 == hash2.m_u32_3;
		}

		public static bool operator !=(Hash128 hash1, Hash128 hash2)
		{
			return !(hash1 == hash2);
		}

		public static bool operator <(Hash128 x, Hash128 y)
		{
			bool flag = x.m_u32_0 != y.m_u32_0;
			bool result;
			if (flag)
			{
				result = (x.m_u32_0 < y.m_u32_0);
			}
			else
			{
				bool flag2 = x.m_u32_1 != y.m_u32_1;
				if (flag2)
				{
					result = (x.m_u32_1 < y.m_u32_1);
				}
				else
				{
					bool flag3 = x.m_u32_2 != y.m_u32_2;
					if (flag3)
					{
						result = (x.m_u32_2 < y.m_u32_2);
					}
					else
					{
						result = (x.m_u32_3 < y.m_u32_3);
					}
				}
			}
			return result;
		}

		public static bool operator >(Hash128 x, Hash128 y)
		{
			bool flag = x < y;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = x == y;
				result = !flag2;
			}
			return result;
		}

		private void ShortHash4(uint data)
		{
			ulong u64_ = this.u64_0;
			ulong u64_2 = this.u64_1;
			ulong num = 16045690984833335023uL;
			ulong num2 = 16045690984833335023uL;
			num2 += 288230376151711744uL;
			num += (ulong)data;
			Hash128.ShortEnd(ref u64_, ref u64_2, ref num, ref num2);
			this.m_u32_0 = (uint)u64_;
			this.m_u32_1 = (uint)(u64_ >> 32);
			this.m_u32_2 = (uint)u64_2;
			this.m_u32_3 = (uint)(u64_2 >> 32);
		}

		private static void ShortEnd(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3)
		{
			h3 ^= h2;
			Hash128.Rot64(ref h2, 15);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 52);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 26);
			h1 += h0;
			h2 ^= h1;
			Hash128.Rot64(ref h1, 51);
			h2 += h1;
			h3 ^= h2;
			Hash128.Rot64(ref h2, 28);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 9);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 47);
			h1 += h0;
			h2 ^= h1;
			Hash128.Rot64(ref h1, 54);
			h2 += h1;
			h3 ^= h2;
			Hash128.Rot64(ref h2, 32);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 25);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 63);
			h1 += h0;
		}

		private static void Rot64(ref ulong x, int k)
		{
			x = (x << k | x >> 64 - k);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Parse_Injected(string hashString, out Hash128 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Hash128ToStringImpl_Injected(ref Hash128 hash);
	}
}
