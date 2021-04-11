using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	[NativeHeader("Runtime/File/AsyncReadManagerManagedApi.h")]
	public static class AsyncReadManager
	{
		[FreeFunction("AsyncReadManagerManaged::Read", IsThreadSafe = true), ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadInternal(string filename, void* cmds, uint cmdCount, string assetName, ulong typeID, AssetLoadingSubsystem subsystem)
		{
			ReadHandle result;
			AsyncReadManager.ReadInternal_Injected(filename, cmds, cmdCount, assetName, typeID, subsystem, out result);
			return result;
		}

		public unsafe static ReadHandle Read(string filename, ReadCommand* readCmds, uint readCmdCount, string assetName = "", ulong typeID = 0uL, AssetLoadingSubsystem subsystem = AssetLoadingSubsystem.Scripts)
		{
			return AsyncReadManager.ReadInternal(filename, (void*)readCmds, readCmdCount, assetName, typeID, subsystem);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadInternal_Injected(string filename, void* cmds, uint cmdCount, string assetName, ulong typeID, AssetLoadingSubsystem subsystem, out ReadHandle ret);
	}
}
