using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.IO
{
	[NativeConditional("ENABLE_PROFILER"), NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h"), StaticAccessor("FileAccessor", StaticAccessorType.DoubleColon)]
	internal static class File
	{
		internal static ulong totalOpenCalls
		{
			get
			{
				return File.GetTotalOpenCalls();
			}
		}

		internal static ulong totalCloseCalls
		{
			get
			{
				return File.GetTotalCloseCalls();
			}
		}

		internal static ulong totalReadCalls
		{
			get
			{
				return File.GetTotalReadCalls();
			}
		}

		internal static ulong totalWriteCalls
		{
			get
			{
				return File.GetTotalWriteCalls();
			}
		}

		internal static ulong totalSeekCalls
		{
			get
			{
				return File.GetTotalSeekCalls();
			}
		}

		internal static ulong totalZeroSeekCalls
		{
			get
			{
				return File.GetTotalZeroSeekCalls();
			}
		}

		internal static ulong totalFilesOpened
		{
			get
			{
				return File.GetTotalFilesOpened();
			}
		}

		internal static ulong totalFilesClosed
		{
			get
			{
				return File.GetTotalFilesClosed();
			}
		}

		internal static ulong totalBytesRead
		{
			get
			{
				return File.GetTotalBytesRead();
			}
		}

		internal static ulong totalBytesWritten
		{
			get
			{
				return File.GetTotalBytesWritten();
			}
		}

		internal static bool recordZeroSeeks
		{
			get
			{
				return File.GetRecordZeroSeeks();
			}
			set
			{
				File.SetRecordZeroSeeks(value);
			}
		}

		internal static ThreadIORestrictionMode MainThreadIORestrictionMode
		{
			get
			{
				return File.GetMainThreadFileIORestriction();
			}
			set
			{
				File.SetMainThreadFileIORestriction(value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetRecordZeroSeeks(bool enable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetRecordZeroSeeks();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalOpenCalls();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalCloseCalls();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalReadCalls();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalWriteCalls();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalSeekCalls();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalZeroSeekCalls();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalFilesOpened();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalFilesClosed();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalBytesRead();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalBytesWritten();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMainThreadFileIORestriction(ThreadIORestrictionMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ThreadIORestrictionMode GetMainThreadFileIORestriction();
	}
}
