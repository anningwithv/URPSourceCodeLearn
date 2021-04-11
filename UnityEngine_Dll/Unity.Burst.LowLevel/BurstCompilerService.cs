using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Burst.LowLevel
{
	[NativeHeader("Runtime/Burst/Burst.h"), NativeHeader("Runtime/Burst/BurstDelegateCache.h"), StaticAccessor("BurstCompilerService::Get()", StaticAccessorType.Arrow)]
	internal static class BurstCompilerService
	{
		public delegate bool ExtractCompilerFlags(Type jobType, out string flags);

		public enum BurstLogType
		{
			Info,
			Warning,
			Error
		}

		public static extern bool IsInitialized
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeMethod("Initialize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InitializeInternal(string path, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetDisassembly(MethodInfo m, string compilerOptions);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CompileAsyncDelegateMethod(object delegateMethod, string compilerOptions);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* GetAsyncCompiledAsyncDelegateMethod(int userID);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* GetOrCreateSharedMemory(ref Hash128 key, uint size_of, uint alignment);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetMethodSignature(MethodInfo method);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCurrentExecutionMode(uint environment);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetCurrentExecutionMode();

		[FreeFunction("DefaultBurstLogCallback", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Log(void* userData, BurstCompilerService.BurstLogType logType, byte* message, byte* filename, int lineNumber);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool LoadBurstLibrary(string fullPathToLibBurstGenerated);

		public static void Initialize(string folderRuntime, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags)
		{
			bool flag = folderRuntime == null;
			if (flag)
			{
				throw new ArgumentNullException("folderRuntime");
			}
			bool flag2 = extractCompilerFlags == null;
			if (flag2)
			{
				throw new ArgumentNullException("extractCompilerFlags");
			}
			bool flag3 = !Directory.Exists(folderRuntime);
			if (flag3)
			{
				Debug.LogError("Unable to initialize the burst JIT compiler. The folder `" + folderRuntime + "` does not exist");
			}
			else
			{
				string text = BurstCompilerService.InitializeInternal(folderRuntime, extractCompilerFlags);
				bool flag4 = !string.IsNullOrEmpty(text);
				if (flag4)
				{
					Debug.LogError("Unexpected error while trying to initialize the burst JIT compiler: " + text);
				}
			}
		}
	}
}
