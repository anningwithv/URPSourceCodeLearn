using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	public static class HashUtilities
	{
		public unsafe static void AppendHash(ref Hash128 inHash, ref Hash128 outHash)
		{
			fixed (Hash128* ptr = &outHash)
			{
				Hash128* hash = ptr;
				fixed (Hash128* ptr2 = &inHash)
				{
					Hash128* data = ptr2;
					HashUnsafeUtilities.ComputeHash128((void*)data, (ulong)((long)sizeof(Hash128)), hash);
				}
			}
		}

		public unsafe static void QuantisedMatrixHash(ref Matrix4x4 value, ref Hash128 hash)
		{
			fixed (Hash128* ptr = &hash)
			{
				Hash128* hash2 = ptr;
				int* ptr2 = stackalloc int[16];
				for (int i = 0; i < 16; i++)
				{
					ptr2[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128((void*)ptr2, 64uL, hash2);
			}
		}

		public unsafe static void QuantisedVectorHash(ref Vector3 value, ref Hash128 hash)
		{
			fixed (Hash128* ptr = &hash)
			{
				Hash128* hash2 = ptr;
				int* ptr2 = stackalloc int[3];
				for (int i = 0; i < 3; i++)
				{
					ptr2[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128((void*)ptr2, 12uL, hash2);
			}
		}

		public unsafe static void ComputeHash128<T>(ref T value, ref Hash128 hash) where T : struct
		{
			void* data = UnsafeUtility.AddressOf<T>(ref value);
			ulong dataSize = (ulong)((long)UnsafeUtility.SizeOf<T>());
			Hash128* hash2 = (Hash128*)UnsafeUtility.AddressOf<Hash128>(ref hash);
			HashUnsafeUtilities.ComputeHash128(data, dataSize, hash2);
		}

		public unsafe static void ComputeHash128(byte[] value, ref Hash128 hash)
		{
			fixed (byte* ptr = &value[0])
			{
				byte* data = ptr;
				ulong dataSize = (ulong)((long)value.Length);
				Hash128* hash2 = (Hash128*)UnsafeUtility.AddressOf<Hash128>(ref hash);
				HashUnsafeUtilities.ComputeHash128((void*)data, dataSize, hash2);
			}
		}
	}
}
