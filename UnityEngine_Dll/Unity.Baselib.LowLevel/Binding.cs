using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.Baselib.LowLevel
{
	[NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_SourceLocation.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_Timer.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_ThreadLocalStorage.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_Socket.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_DynamicLibrary.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_Thread.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_NetworkAddress.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_ErrorState.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_RegisteredNetwork.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_Memory.gen.binding.h"), NativeHeader("External/baselib/builds/CSharp/BindingsUnity/Baselib_ErrorCode.gen.binding.h")]
	internal static class Binding
	{
		public struct Baselib_DynamicLibrary_Handle
		{
			public IntPtr handle;
		}

		public enum Baselib_ErrorCode
		{
			Success,
			OutOfMemory = 16777216,
			OutOfSystemResources,
			InvalidAddressRange,
			InvalidArgument,
			InvalidBufferSize,
			InvalidState,
			NotSupported,
			Timeout,
			UnsupportedAlignment = 33554432,
			InvalidPageSize,
			InvalidPageCount,
			UnsupportedPageState,
			ThreadCannotJoinSelf = 50331648,
			NetworkInitializationError = 67108864,
			AddressInUse,
			AddressUnreachable,
			AddressFamilyNotSupported,
			Disconnected,
			InvalidPathname = 83886080,
			RequestedAccessIsNotAllowed,
			IOError,
			FailedToOpenDynamicLibrary = 100663296,
			FunctionNotFound,
			UnexpectedError = -1
		}

		public enum Baselib_ErrorState_NativeErrorCodeType : byte
		{
			None,
			PlatformDefined
		}

		public enum Baselib_ErrorState_ExtraInformationType : byte
		{
			None,
			StaticString,
			GenerationCounter
		}

		public struct Baselib_ErrorState
		{
			public Binding.Baselib_SourceLocation sourceLocation;

			public ulong nativeErrorCode;

			public ulong extraInformation;

			public Binding.Baselib_ErrorCode code;

			public Binding.Baselib_ErrorState_NativeErrorCodeType nativeErrorCodeType;

			public Binding.Baselib_ErrorState_ExtraInformationType extraInformationType;
		}

		public enum Baselib_ErrorState_ExplainVerbosity
		{
			ErrorType,
			ErrorType_SourceLocation_Explanation
		}

		public struct Baselib_Memory_PageSizeInfo
		{
			public ulong defaultPageSize;

			public ulong pageSizes0;

			public ulong pageSizes1;

			public ulong pageSizes2;

			public ulong pageSizes3;

			public ulong pageSizes4;

			public ulong pageSizes5;

			public ulong pageSizesLen;
		}

		public struct Baselib_Memory_PageAllocation
		{
			public IntPtr ptr;

			public ulong pageSize;

			public ulong pageCount;
		}

		public enum Baselib_Memory_PageState
		{
			Reserved,
			NoAccess,
			ReadOnly,
			ReadWrite = 4,
			ReadOnly_Executable = 18,
			ReadWrite_Executable = 20
		}

		public enum Baselib_NetworkAddress_Family
		{
			Invalid,
			IPv4,
			IPv6
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct Baselib_NetworkAddress
		{
			[FieldOffset(0)]
			public byte data0;

			[FieldOffset(1)]
			public byte data1;

			[FieldOffset(2)]
			public byte data2;

			[FieldOffset(3)]
			public byte data3;

			[FieldOffset(4)]
			public byte data4;

			[FieldOffset(5)]
			public byte data5;

			[FieldOffset(6)]
			public byte data6;

			[FieldOffset(7)]
			public byte data7;

			[FieldOffset(8)]
			public byte data8;

			[FieldOffset(9)]
			public byte data9;

			[FieldOffset(10)]
			public byte data10;

			[FieldOffset(11)]
			public byte data11;

			[FieldOffset(12)]
			public byte data12;

			[FieldOffset(13)]
			public byte data13;

			[FieldOffset(14)]
			public byte data14;

			[FieldOffset(15)]
			public byte data15;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(0)]
			public byte ipv6_0;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(1)]
			public byte ipv6_1;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(2)]
			public byte ipv6_2;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(3)]
			public byte ipv6_3;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(4)]
			public byte ipv6_4;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(5)]
			public byte ipv6_5;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(6)]
			public byte ipv6_6;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(7)]
			public byte ipv6_7;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(8)]
			public byte ipv6_8;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(9)]
			public byte ipv6_9;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(10)]
			public byte ipv6_10;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(11)]
			public byte ipv6_11;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(12)]
			public byte ipv6_12;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(13)]
			public byte ipv6_13;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(14)]
			public byte ipv6_14;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(15)]
			public byte ipv6_15;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(0)]
			public byte ipv4_0;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(1)]
			public byte ipv4_1;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(2)]
			public byte ipv4_2;

			[Ignore(DoesNotContributeToSize = true)]
			[FieldOffset(3)]
			public byte ipv4_3;

			[FieldOffset(16)]
			public byte port0;

			[FieldOffset(17)]
			public byte port1;

			[FieldOffset(18)]
			public byte family;

			[FieldOffset(19)]
			public byte _padding;
		}

		public enum Baselib_NetworkAddress_AddressReuse
		{
			DoNotAllow,
			Allow
		}

		public struct Baselib_RegisteredNetwork_Buffer
		{
			public IntPtr id;

			public Binding.Baselib_Memory_PageAllocation allocation;
		}

		public struct Baselib_RegisteredNetwork_BufferSlice
		{
			public IntPtr id;

			public IntPtr data;

			public uint size;

			public uint offset;
		}

		public struct Baselib_RegisteredNetwork_Endpoint
		{
			public Binding.Baselib_RegisteredNetwork_BufferSlice slice;
		}

		public struct Baselib_RegisteredNetwork_Request
		{
			public Binding.Baselib_RegisteredNetwork_BufferSlice payload;

			public Binding.Baselib_RegisteredNetwork_Endpoint remoteEndpoint;

			public IntPtr requestUserdata;
		}

		public enum Baselib_RegisteredNetwork_CompletionStatus
		{
			Failed,
			Success
		}

		public struct Baselib_RegisteredNetwork_CompletionResult
		{
			public Binding.Baselib_RegisteredNetwork_CompletionStatus status;

			public uint bytesTransferred;

			public IntPtr requestUserdata;
		}

		public struct Baselib_RegisteredNetwork_Socket_UDP
		{
			public IntPtr handle;
		}

		public enum Baselib_RegisteredNetwork_ProcessStatus
		{
			NonePendingImmediately,
			Done = 0,
			Pending
		}

		public enum Baselib_RegisteredNetwork_CompletionQueueStatus
		{
			NoResultsAvailable,
			ResultsAvailable
		}

		public struct Baselib_Socket_Handle
		{
			public IntPtr handle;
		}

		public enum Baselib_Socket_Protocol
		{
			UDP = 1,
			TCP
		}

		public struct Baselib_Socket_Message
		{
			public unsafe Binding.Baselib_NetworkAddress* address;

			public IntPtr data;

			public uint dataLen;
		}

		public enum Baselib_Socket_PollEvents
		{
			Readable = 1,
			Writable,
			Connected = 4
		}

		public struct Baselib_Socket_PollFd
		{
			public Binding.Baselib_Socket_Handle handle;

			public Binding.Baselib_Socket_PollEvents requestedEvents;

			public Binding.Baselib_Socket_PollEvents resultEvents;

			public unsafe Binding.Baselib_ErrorState* errorState;
		}

		public struct Baselib_SourceLocation
		{
			public unsafe byte* file;

			public unsafe byte* function;

			public uint lineNumber;
		}

		public struct Baselib_Timer_TickToNanosecondConversionRatio
		{
			public ulong ticksToNanosecondsNumerator;

			public ulong ticksToNanosecondsDenominator;
		}

		public static readonly UIntPtr Baselib_Memory_MaxAlignment = new UIntPtr(65536u);

		public static readonly UIntPtr Baselib_Memory_MinGuaranteedAlignment = new UIntPtr(8u);

		public const uint Baselib_NetworkAddress_IpMaxStringLength = 46u;

		public static readonly IntPtr Baselib_RegisteredNetwork_Buffer_Id_Invalid = IntPtr.Zero;

		public const uint Baselib_RegisteredNetwork_Endpoint_MaxSize = 28u;

		public static readonly IntPtr Baselib_Thread_InvalidId = IntPtr.Zero;

		public const uint Baselib_TLS_MinimumGuaranteedSlots = 100u;

		public const ulong Baselib_SecondsPerMinute = 60uL;

		public const ulong Baselib_MillisecondsPerSecond = 1000uL;

		public const ulong Baselib_MillisecondsPerMinute = 60000uL;

		public const ulong Baselib_MicrosecondsPerMillisecond = 1000uL;

		public const ulong Baselib_MicrosecondsPerSecond = 1000000uL;

		public const ulong Baselib_MicrosecondsPerMinute = 60000000uL;

		public const ulong Baselib_NanosecondsPerMicrosecond = 1000uL;

		public const ulong Baselib_NanosecondsPerMillisecond = 1000000uL;

		public const ulong Baselib_NanosecondsPerSecond = 1000000000uL;

		public const ulong Baselib_NanosecondsPerMinute = 60000000000uL;

		public const ulong Baselib_Timer_MaxNumberOfNanosecondsPerTick = 1000uL;

		public const double Baselib_Timer_MinNumberOfNanosecondsPerTick = 0.01;

		public static readonly Binding.Baselib_Memory_PageAllocation Baselib_Memory_PageAllocation_Invalid = default(Binding.Baselib_Memory_PageAllocation);

		public static readonly Binding.Baselib_RegisteredNetwork_Socket_UDP Baselib_RegisteredNetwork_Socket_UDP_Invalid = default(Binding.Baselib_RegisteredNetwork_Socket_UDP);

		public static readonly Binding.Baselib_Socket_Handle Baselib_Socket_Handle_Invalid = new Binding.Baselib_Socket_Handle
		{
			handle = (IntPtr)(-1)
		};

		public static readonly Binding.Baselib_DynamicLibrary_Handle Baselib_DynamicLibrary_Handle_Invalid = new Binding.Baselib_DynamicLibrary_Handle
		{
			handle = (IntPtr)(-1)
		};

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_DynamicLibrary_Handle Baselib_DynamicLibrary_Open(byte* pathname, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_DynamicLibrary_Handle result;
			Binding.Baselib_DynamicLibrary_Open_Injected(pathname, errorState, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static IntPtr Baselib_DynamicLibrary_GetFunction(Binding.Baselib_DynamicLibrary_Handle handle, byte* functionName, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_DynamicLibrary_GetFunction_Injected(ref handle, functionName, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_DynamicLibrary_Close(Binding.Baselib_DynamicLibrary_Handle handle)
		{
			Binding.Baselib_DynamicLibrary_Close_Injected(ref handle);
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint Baselib_ErrorState_Explain(Binding.Baselib_ErrorState* errorState, byte* buffer, uint bufferLen, Binding.Baselib_ErrorState_ExplainVerbosity verbosity);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_Memory_GetPageSizeInfo(Binding.Baselib_Memory_PageSizeInfo* outPagesSizeInfo);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Memory_Allocate(UIntPtr size);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Memory_Reallocate(IntPtr ptr, UIntPtr newSize);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_Memory_Free(IntPtr ptr);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Memory_AlignedAllocate(UIntPtr size, UIntPtr alignment);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Memory_AlignedReallocate(IntPtr ptr, UIntPtr newSize, UIntPtr alignment);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_Memory_AlignedFree(IntPtr ptr);

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_Memory_PageAllocation Baselib_Memory_AllocatePages(ulong pageSize, ulong pageCount, ulong alignmentInMultipleOfPageSize, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Memory_PageAllocation result;
			Binding.Baselib_Memory_AllocatePages_Injected(pageSize, pageCount, alignmentInMultipleOfPageSize, pageState, errorState, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Memory_ReleasePages(Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Memory_ReleasePages_Injected(ref pageAllocation, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_Memory_SetPageState(IntPtr addressOfFirstPage, ulong pageSize, ulong pageCount, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_NetworkAddress_Encode(Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_NetworkAddress_Family family, byte* ip, ushort port, Binding.Baselib_ErrorState* errorState);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_NetworkAddress_Decode(Binding.Baselib_NetworkAddress* srcAddress, Binding.Baselib_NetworkAddress_Family* family, byte* ipAddressBuffer, uint ipAddressBufferLen, ushort* port, Binding.Baselib_ErrorState* errorState);

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Buffer Baselib_RegisteredNetwork_Buffer_Register(Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Buffer result;
			Binding.Baselib_RegisteredNetwork_Buffer_Register_Injected(ref pageAllocation, errorState, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_RegisteredNetwork_Buffer_Deregister(Binding.Baselib_RegisteredNetwork_Buffer buffer)
		{
			Binding.Baselib_RegisteredNetwork_Buffer_Deregister_Injected(ref buffer);
		}

		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_BufferSlice Baselib_RegisteredNetwork_BufferSlice_Create(Binding.Baselib_RegisteredNetwork_Buffer buffer, uint offset, uint size)
		{
			Binding.Baselib_RegisteredNetwork_BufferSlice result;
			Binding.Baselib_RegisteredNetwork_BufferSlice_Create_Injected(ref buffer, offset, size, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_BufferSlice Baselib_RegisteredNetwork_BufferSlice_Empty()
		{
			Binding.Baselib_RegisteredNetwork_BufferSlice result;
			Binding.Baselib_RegisteredNetwork_BufferSlice_Empty_Injected(out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Endpoint Baselib_RegisteredNetwork_Endpoint_Create(Binding.Baselib_NetworkAddress* srcAddress, Binding.Baselib_RegisteredNetwork_BufferSlice dstSlice, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Endpoint result;
			Binding.Baselib_RegisteredNetwork_Endpoint_Create_Injected(srcAddress, ref dstSlice, errorState, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_Endpoint Baselib_RegisteredNetwork_Endpoint_Empty()
		{
			Binding.Baselib_RegisteredNetwork_Endpoint result;
			Binding.Baselib_RegisteredNetwork_Endpoint_Empty_Injected(out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress(Binding.Baselib_RegisteredNetwork_Endpoint endpoint, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress_Injected(ref endpoint, dstAddress, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Socket_UDP Baselib_RegisteredNetwork_Socket_UDP_Create(Binding.Baselib_NetworkAddress* bindAddress, Binding.Baselib_NetworkAddress_AddressReuse endpointReuse, uint sendQueueSize, uint recvQueueSize, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP result;
			Binding.Baselib_RegisteredNetwork_Socket_UDP_Create_Injected(bindAddress, endpointReuse, sendQueueSize, recvQueueSize, errorState, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv_Injected(ref socket, requests, requestsCount, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend_Injected(ref socket, requests, requestsCount, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv_Injected(ref socket, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ProcessSend_Injected(ref socket, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv_Injected(ref socket, timeoutInMilliseconds, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend_Injected(ref socket, timeoutInMilliseconds, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv_Injected(ref socket, results, resultsCount, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_DequeueSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_DequeueSend_Injected(ref socket, results, resultsCount, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress_Injected(ref socket, dstAddress, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_RegisteredNetwork_Socket_UDP_Close(Binding.Baselib_RegisteredNetwork_Socket_UDP socket)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP_Close_Injected(ref socket);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_Socket_Handle Baselib_Socket_Create(Binding.Baselib_NetworkAddress_Family family, Binding.Baselib_Socket_Protocol protocol, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_Handle result;
			Binding.Baselib_Socket_Create_Injected(family, protocol, errorState, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Socket_Bind(Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_NetworkAddress_AddressReuse addressReuse, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_Bind_Injected(ref socket, address, addressReuse, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Socket_TCP_Connect(Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_NetworkAddress_AddressReuse addressReuse, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_TCP_Connect_Injected(ref socket, address, addressReuse, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Baselib_Socket_Poll(Binding.Baselib_Socket_PollFd* sockets, uint socketsCount, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState);

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Socket_GetAddress(Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_GetAddress_Injected(ref socket, address, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Socket_TCP_Listen(Binding.Baselib_Socket_Handle socket, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_TCP_Listen_Injected(ref socket, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_Socket_Handle Baselib_Socket_TCP_Accept(Binding.Baselib_Socket_Handle socket, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Socket_Handle result;
			Binding.Baselib_Socket_TCP_Accept_Injected(ref socket, errorState, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_Socket_UDP_Send(Binding.Baselib_Socket_Handle socket, Binding.Baselib_Socket_Message* messages, uint messagesCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_Socket_UDP_Send_Injected(ref socket, messages, messagesCount, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_Socket_TCP_Send(Binding.Baselib_Socket_Handle socket, IntPtr data, uint dataLen, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_Socket_TCP_Send_Injected(ref socket, data, dataLen, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_Socket_UDP_Recv(Binding.Baselib_Socket_Handle socket, Binding.Baselib_Socket_Message* messages, uint messagesCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_Socket_UDP_Recv_Injected(ref socket, messages, messagesCount, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_Socket_TCP_Recv(Binding.Baselib_Socket_Handle socket, IntPtr data, uint dataLen, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_Socket_TCP_Recv_Injected(ref socket, data, dataLen, errorState);
		}

		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_Socket_Close(Binding.Baselib_Socket_Handle socket)
		{
			Binding.Baselib_Socket_Close_Injected(ref socket);
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_Thread_YieldExecution();

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Baselib_Thread_GetCurrentThreadId();

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr Baselib_TLS_Alloc();

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_TLS_Free(UIntPtr handle);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_TLS_Set(UIntPtr handle, UIntPtr value);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr Baselib_TLS_Get(UIntPtr handle);

		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_Timer_TickToNanosecondConversionRatio Baselib_Timer_GetTicksToNanosecondsConversionRatio()
		{
			Binding.Baselib_Timer_TickToNanosecondConversionRatio result;
			Binding.Baselib_Timer_GetTicksToNanosecondsConversionRatio_Injected(out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong Baselib_Timer_GetHighPrecisionTimerTicks();

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Baselib_Timer_WaitForAtLeast(uint timeInMilliseconds);

		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Baselib_Timer_GetTimeSinceStartupInSeconds();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_DynamicLibrary_Open_Injected(byte* pathname, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_DynamicLibrary_Handle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Baselib_DynamicLibrary_GetFunction_Injected(ref Binding.Baselib_DynamicLibrary_Handle handle, byte* functionName, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_DynamicLibrary_Close_Injected(ref Binding.Baselib_DynamicLibrary_Handle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Memory_AllocatePages_Injected(ulong pageSize, ulong pageCount, ulong alignmentInMultipleOfPageSize, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_Memory_PageAllocation ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Memory_ReleasePages_Injected(ref Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Buffer_Register_Injected(ref Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Buffer ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_Buffer_Deregister_Injected(ref Binding.Baselib_RegisteredNetwork_Buffer buffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_BufferSlice_Create_Injected(ref Binding.Baselib_RegisteredNetwork_Buffer buffer, uint offset, uint size, out Binding.Baselib_RegisteredNetwork_BufferSlice ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_BufferSlice_Empty_Injected(out Binding.Baselib_RegisteredNetwork_BufferSlice ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Endpoint_Create_Injected(Binding.Baselib_NetworkAddress* srcAddress, ref Binding.Baselib_RegisteredNetwork_BufferSlice dstSlice, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Endpoint ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_Endpoint_Empty_Injected(out Binding.Baselib_RegisteredNetwork_Endpoint ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress_Injected(ref Binding.Baselib_RegisteredNetwork_Endpoint endpoint, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Socket_UDP_Create_Injected(Binding.Baselib_NetworkAddress* bindAddress, Binding.Baselib_NetworkAddress_AddressReuse endpointReuse, uint sendQueueSize, uint recvQueueSize, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Socket_UDP ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_DequeueSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_RegisteredNetwork_Socket_UDP_Close_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_Create_Injected(Binding.Baselib_NetworkAddress_Family family, Binding.Baselib_Socket_Protocol protocol, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_Socket_Handle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_Bind_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_NetworkAddress_AddressReuse addressReuse, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_TCP_Connect_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_NetworkAddress_AddressReuse addressReuse, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_GetAddress_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_NetworkAddress* address, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_TCP_Listen_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Baselib_Socket_TCP_Accept_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_Socket_Handle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_Socket_UDP_Send_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_Socket_Message* messages, uint messagesCount, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_Socket_TCP_Send_Injected(ref Binding.Baselib_Socket_Handle socket, IntPtr data, uint dataLen, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_Socket_UDP_Recv_Injected(ref Binding.Baselib_Socket_Handle socket, Binding.Baselib_Socket_Message* messages, uint messagesCount, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern uint Baselib_Socket_TCP_Recv_Injected(ref Binding.Baselib_Socket_Handle socket, IntPtr data, uint dataLen, Binding.Baselib_ErrorState* errorState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_Socket_Close_Injected(ref Binding.Baselib_Socket_Handle socket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Baselib_Timer_GetTicksToNanosecondsConversionRatio_Injected(out Binding.Baselib_Timer_TickToNanosecondConversionRatio ret);
	}
}
