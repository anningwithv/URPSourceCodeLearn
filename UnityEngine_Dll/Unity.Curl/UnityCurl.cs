using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Curl
{
	[NativeHeader("Modules/UnityCurl/Public/UnityCurl.h"), StaticAccessor("UnityCurl", StaticAccessorType.DoubleColon)]
	internal static class UnityCurl
	{
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr CreateMultiHandle();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyMultiHandle(IntPtr handle);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateEasyHandle(byte* method, byte* url, out uint curlMethod);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetupEasyHandle(IntPtr handle, uint curlMethod, IntPtr headers, ulong contentLen, uint flags);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyEasyHandle(IntPtr handle);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void QueueRequest(IntPtr multiHandle, IntPtr easyHandle);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr AppendHeader(IntPtr headerList, byte* header);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FreeHeaderList(IntPtr headerList);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetRequestErrorCode(IntPtr request);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetRequestStatus(IntPtr request);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetRequestStatusCode(IntPtr request);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetDownloadSize(IntPtr request, out ulong downloaded, out ulong expected);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte* GetResponseHeader(IntPtr request, uint index, out uint length);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte* GetMoreBody(IntPtr handle, out int length);

		internal unsafe static void SendMoreBody(IntPtr handle, byte* chunk, uint length, BufferOwnership ownership)
		{
			UnityCurl.SendMoreBody(handle, chunk, length, (int)ownership);
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SendMoreBody(IntPtr handle, byte* chunk, uint length, int ownership);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void AbortRequest(IntPtr handle);
	}
}
