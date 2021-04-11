using System;
using System.IO;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal static class ManagedStreamHelpers
	{
		internal static void ValidateLoadFromStream(Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("ManagedStream object must be non-null", "stream");
			}
			bool flag2 = !stream.CanRead;
			if (flag2)
			{
				throw new ArgumentException("ManagedStream object must be readable (stream.CanRead must return true)", "stream");
			}
			bool flag3 = !stream.CanSeek;
			if (flag3)
			{
				throw new ArgumentException("ManagedStream object must be seekable (stream.CanSeek must return true)", "stream");
			}
		}

		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamRead(byte[] buffer, int offset, int count, Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(int*)((void*)returnValueAddress) = stream.Read(buffer, offset, count);
		}

		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamSeek(long offset, uint origin, Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(long*)((void*)returnValueAddress) = stream.Seek(offset, (SeekOrigin)origin);
		}

		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamLength(Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(long*)((void*)returnValueAddress) = stream.Length;
		}
	}
}
