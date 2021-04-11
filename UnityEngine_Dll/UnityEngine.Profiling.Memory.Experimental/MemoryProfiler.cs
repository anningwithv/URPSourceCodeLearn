using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Profiling.Experimental;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling.Memory.Experimental
{
	[NativeHeader("Modules/Profiler/Runtime/MemorySnapshotManager.h")]
	public sealed class MemoryProfiler
	{
		private static bool isCompiling = false;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		private static event Action<string, bool> m_SnapshotFinished;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		private static event Action<string, bool, DebugScreenCapture> m_SaveScreenshotToDisk;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<MetaData> createMetaData;

		internal static void StartedCompilationCallback(object msg)
		{
			MemoryProfiler.isCompiling = true;
		}

		internal static void FinishedCompilationCallback(object msg)
		{
			MemoryProfiler.isCompiling = false;
		}

		[NativeConditional("ENABLE_PROFILER"), NativeMethod("StartOperation"), StaticAccessor("profiling::memory::GetMemorySnapshotManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartOperation(uint captureFlag, bool requestScreenshot, string path, bool isRemote);

		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			MemoryProfiler.TakeSnapshot(path, finishCallback, null, captureFlags);
		}

		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, Action<string, bool, DebugScreenCapture> screenshotCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			bool flag = MemoryProfiler.isCompiling;
			if (flag)
			{
				UnityEngine.Debug.LogError("Canceling snapshot, there is a compilation in progress.");
			}
			else
			{
				bool flag2 = MemoryProfiler.m_SnapshotFinished != null;
				if (flag2)
				{
					UnityEngine.Debug.LogWarning("Canceling snapshot, there is another snapshot in progress.");
					finishCallback(path, false);
				}
				else
				{
					MemoryProfiler.m_SnapshotFinished += finishCallback;
					MemoryProfiler.m_SaveScreenshotToDisk += screenshotCallback;
					MemoryProfiler.StartOperation((uint)captureFlags, MemoryProfiler.m_SaveScreenshotToDisk != null, path, false);
				}
			}
		}

		public static void TakeTempSnapshot(Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			string[] array = Application.dataPath.Split(new char[]
			{
				'/'
			});
			string str = array[array.Length - 2];
			string path = Application.temporaryCachePath + "/" + str + ".snap";
			MemoryProfiler.TakeSnapshot(path, finishCallback, captureFlags);
		}

		[RequiredByNativeCode]
		private static byte[] PrepareMetadata()
		{
			bool flag = MemoryProfiler.createMetaData == null;
			byte[] result;
			if (flag)
			{
				result = new byte[0];
			}
			else
			{
				MetaData metaData = new MetaData();
				MemoryProfiler.createMetaData(metaData);
				bool flag2 = metaData.content == null;
				if (flag2)
				{
					metaData.content = "";
				}
				bool flag3 = metaData.platform == null;
				if (flag3)
				{
					metaData.platform = "";
				}
				int num = 2 * metaData.content.Length;
				int num2 = 2 * metaData.platform.Length;
				int num3 = num + num2 + 12;
				byte[] array = new byte[num3];
				int offset = 0;
				offset = MemoryProfiler.WriteIntToByteArray(array, offset, metaData.content.Length);
				offset = MemoryProfiler.WriteStringToByteArray(array, offset, metaData.content);
				offset = MemoryProfiler.WriteIntToByteArray(array, offset, metaData.platform.Length);
				offset = MemoryProfiler.WriteStringToByteArray(array, offset, metaData.platform);
				result = array;
			}
			return result;
		}

		internal unsafe static int WriteIntToByteArray(byte[] array, int offset, int value)
		{
			byte* ptr = (byte*)(&value);
			array[offset++] = *ptr;
			array[offset++] = ptr[1];
			array[offset++] = ptr[2];
			array[offset++] = ptr[3];
			return offset;
		}

		internal unsafe static int WriteStringToByteArray(byte[] array, int offset, string value)
		{
			bool flag = value.Length != 0;
			if (flag)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr2 = ptr;
					char* ptr3 = ptr + value.Length;
					while (ptr2 != ptr3)
					{
						for (int i = 0; i < 2; i++)
						{
							array[offset++] = *(byte*)(ptr2 + i / 2);
						}
						ptr2++;
					}
				}
			}
			return offset;
		}

		[RequiredByNativeCode]
		private static void FinalizeSnapshot(string path, bool result)
		{
			bool flag = MemoryProfiler.m_SnapshotFinished != null;
			if (flag)
			{
				Action<string, bool> snapshotFinished = MemoryProfiler.m_SnapshotFinished;
				MemoryProfiler.m_SnapshotFinished = null;
				snapshotFinished(path, result);
			}
		}

		[RequiredByNativeCode]
		private static void SaveScreenshotToDisk(string path, bool result, IntPtr pixelsPtr, int pixelsCount, TextureFormat format, int width, int height)
		{
			bool flag = MemoryProfiler.m_SaveScreenshotToDisk != null;
			if (flag)
			{
				Action<string, bool, DebugScreenCapture> saveScreenshotToDisk = MemoryProfiler.m_SaveScreenshotToDisk;
				MemoryProfiler.m_SaveScreenshotToDisk = null;
				DebugScreenCapture arg = default(DebugScreenCapture);
				if (result)
				{
					NativeArray<byte> rawImageDataReference = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(pixelsPtr.ToPointer(), pixelsCount, Allocator.Persistent);
					arg.rawImageDataReference = rawImageDataReference;
					arg.height = height;
					arg.width = width;
					arg.imageFormat = format;
				}
				saveScreenshotToDisk(path, result, arg);
			}
		}
	}
}
