using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Audio
{
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.h"), StaticAccessor("AudioSampleProviderBindings", StaticAccessorType.DoubleColon)]
	public class AudioSampleProvider : IDisposable
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ConsumeSampleFramesNativeFunction(uint providerId, IntPtr interleavedSampleFrames, uint sampleFrameCount);

		public delegate void SampleFramesHandler(AudioSampleProvider provider, uint sampleFrameCount);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SampleFramesEventNativeFunction(IntPtr userData, uint providerId, uint sampleFrameCount);

		private AudioSampleProvider.ConsumeSampleFramesNativeFunction m_ConsumeSampleFramesNativeFunction;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event AudioSampleProvider.SampleFramesHandler sampleFramesAvailable;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event AudioSampleProvider.SampleFramesHandler sampleFramesOverflow;

		public uint id
		{
			get;
			private set;
		}

		public ushort trackIndex
		{
			get;
			private set;
		}

		public UnityEngine.Object owner
		{
			get;
			private set;
		}

		public bool valid
		{
			get
			{
				return AudioSampleProvider.InternalIsValid(this.id);
			}
		}

		public ushort channelCount
		{
			get;
			private set;
		}

		public uint sampleRate
		{
			get;
			private set;
		}

		public uint maxSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetMaxSampleFrameCount(this.id);
			}
		}

		public uint availableSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetAvailableSampleFrameCount(this.id);
			}
		}

		public uint freeSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetFreeSampleFrameCount(this.id);
			}
		}

		public uint freeSampleFrameCountLowThreshold
		{
			get
			{
				return AudioSampleProvider.InternalGetFreeSampleFrameCountLowThreshold(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetFreeSampleFrameCountLowThreshold(this.id, value);
			}
		}

		public bool enableSampleFramesAvailableEvents
		{
			get
			{
				return AudioSampleProvider.InternalGetEnableSampleFramesAvailableEvents(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetEnableSampleFramesAvailableEvents(this.id, value);
			}
		}

		public bool enableSilencePadding
		{
			get
			{
				return AudioSampleProvider.InternalGetEnableSilencePadding(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetEnableSilencePadding(this.id, value);
			}
		}

		public static AudioSampleProvider.ConsumeSampleFramesNativeFunction consumeSampleFramesNativeFunction
		{
			get
			{
				return (AudioSampleProvider.ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(AudioSampleProvider.InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(AudioSampleProvider.ConsumeSampleFramesNativeFunction));
			}
		}

		[VisibleToOtherModules]
		internal static AudioSampleProvider Lookup(uint providerId, UnityEngine.Object ownerObj, ushort trackIndex)
		{
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.InternalGetScriptingPtr(providerId);
			bool flag = audioSampleProvider != null || !AudioSampleProvider.InternalIsValid(providerId);
			AudioSampleProvider result;
			if (flag)
			{
				result = audioSampleProvider;
			}
			else
			{
				result = new AudioSampleProvider(providerId, ownerObj, trackIndex);
			}
			return result;
		}

		internal static AudioSampleProvider Create(ushort channelCount, uint sampleRate)
		{
			uint providerId = AudioSampleProvider.InternalCreateSampleProvider(channelCount, sampleRate);
			bool flag = !AudioSampleProvider.InternalIsValid(providerId);
			AudioSampleProvider result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new AudioSampleProvider(providerId, null, 0);
			}
			return result;
		}

		private AudioSampleProvider(uint providerId, UnityEngine.Object ownerObj, ushort trackIdx)
		{
			this.owner = ownerObj;
			this.id = providerId;
			this.trackIndex = trackIdx;
			this.m_ConsumeSampleFramesNativeFunction = (AudioSampleProvider.ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(AudioSampleProvider.InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(AudioSampleProvider.ConsumeSampleFramesNativeFunction));
			ushort channelCount = 0;
			uint sampleRate = 0u;
			AudioSampleProvider.InternalGetFormatInfo(providerId, out channelCount, out sampleRate);
			this.channelCount = channelCount;
			this.sampleRate = sampleRate;
			AudioSampleProvider.InternalSetScriptingPtr(providerId, this);
		}

		~AudioSampleProvider()
		{
			this.owner = null;
			this.Dispose();
		}

		public void Dispose()
		{
			bool flag = this.id > 0u;
			if (flag)
			{
				AudioSampleProvider.InternalSetScriptingPtr(this.id, null);
				bool flag2 = this.owner == null;
				if (flag2)
				{
					AudioSampleProvider.InternalRemove(this.id);
				}
				this.id = 0u;
			}
			GC.SuppressFinalize(this);
		}

		public uint ConsumeSampleFrames(NativeArray<float> sampleFrames)
		{
			bool flag = this.channelCount == 0;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				result = this.m_ConsumeSampleFramesNativeFunction(this.id, (IntPtr)sampleFrames.GetUnsafePtr<float>(), (uint)(sampleFrames.Length / (int)this.channelCount));
			}
			return result;
		}

		internal uint QueueSampleFrames(NativeArray<float> sampleFrames)
		{
			bool flag = this.channelCount == 0;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				result = AudioSampleProvider.InternalQueueSampleFrames(this.id, (IntPtr)sampleFrames.GetUnsafeReadOnlyPtr<float>(), (uint)(sampleFrames.Length / (int)this.channelCount));
			}
			return result;
		}

		public void SetSampleFramesAvailableNativeHandler(AudioSampleProvider.SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			AudioSampleProvider.InternalSetSampleFramesAvailableNativeHandler(this.id, Marshal.GetFunctionPointerForDelegate(handler), userData);
		}

		public void ClearSampleFramesAvailableNativeHandler()
		{
			AudioSampleProvider.InternalClearSampleFramesAvailableNativeHandler(this.id);
		}

		public void SetSampleFramesOverflowNativeHandler(AudioSampleProvider.SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			AudioSampleProvider.InternalSetSampleFramesOverflowNativeHandler(this.id, Marshal.GetFunctionPointerForDelegate(handler), userData);
		}

		public void ClearSampleFramesOverflowNativeHandler()
		{
			AudioSampleProvider.InternalClearSampleFramesOverflowNativeHandler(this.id);
		}

		[RequiredByNativeCode]
		private void InvokeSampleFramesAvailable(int sampleFrameCount)
		{
			bool flag = this.sampleFramesAvailable != null;
			if (flag)
			{
				this.sampleFramesAvailable(this, (uint)sampleFrameCount);
			}
		}

		[RequiredByNativeCode]
		private void InvokeSampleFramesOverflow(int droppedSampleFrameCount)
		{
			bool flag = this.sampleFramesOverflow != null;
			if (flag)
			{
				this.sampleFramesOverflow(this, (uint)droppedSampleFrameCount);
			}
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalCreateSampleProvider(ushort channelCount, uint sampleRate);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRemove(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetFormatInfo(uint providerId, out ushort chCount, out uint sRate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSampleProvider InternalGetScriptingPtr(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetScriptingPtr(uint providerId, AudioSampleProvider provider);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalIsValid(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetMaxSampleFrameCount(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetAvailableSampleFrameCount(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetFreeSampleFrameCount(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetFreeSampleFrameCountLowThreshold(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetFreeSampleFrameCountLowThreshold(uint providerId, uint sampleFrameCount);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEnableSampleFramesAvailableEvents(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEnableSampleFramesAvailableEvents(uint providerId, bool enable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSampleFramesAvailableNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearSampleFramesAvailableNativeHandler(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSampleFramesOverflowNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearSampleFramesOverflowNativeHandler(uint providerId);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEnableSilencePadding(uint id);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEnableSilencePadding(uint id, bool enabled);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetConsumeSampleFramesNativeFunctionPtr();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalQueueSampleFrames(uint id, IntPtr interleavedSampleFrames, uint sampleFrameCount);
	}
}
