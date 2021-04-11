using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	[NativeHeader("Runtime/Profiler/ScriptBindings/Recorder.bindings.h"), NativeHeader("Runtime/Profiler/Recorder.h"), UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Recorder
	{
		internal IntPtr m_Ptr;

		internal static Recorder s_InvalidRecorder = new Recorder();

		public bool isValid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		public bool enabled
		{
			get
			{
				return this.isValid && this.IsEnabled();
			}
			set
			{
				bool isValid = this.isValid;
				if (isValid)
				{
					this.SetEnabled(value);
				}
			}
		}

		public long elapsedNanoseconds
		{
			get
			{
				return this.isValid ? this.GetElapsedNanoseconds() : 0L;
			}
		}

		public long gpuElapsedNanoseconds
		{
			get
			{
				return this.isValid ? this.GetGpuElapsedNanoseconds() : 0L;
			}
		}

		public int sampleBlockCount
		{
			get
			{
				return this.isValid ? this.GetSampleBlockCount() : 0;
			}
		}

		public int gpuSampleBlockCount
		{
			get
			{
				return this.isValid ? this.GetGpuSampleBlockCount() : 0;
			}
		}

		internal Recorder()
		{
		}

		internal Recorder(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Recorder.DisposeNative(this.m_Ptr);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		public static Recorder Get(string samplerName)
		{
			IntPtr @internal = Recorder.GetInternal(samplerName);
			bool flag = @internal == IntPtr.Zero;
			Recorder result;
			if (flag)
			{
				result = Recorder.s_InvalidRecorder;
			}
			else
			{
				result = new Recorder(@internal);
			}
			return result;
		}

		[NativeMethod(Name = "ProfilerBindings::GetRecorderInternal", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetInternal(string samplerName);

		[NativeMethod(Name = "ProfilerBindings::DisposeNativeRecorder", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisposeNative(IntPtr ptr);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsEnabled();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetEnabled(bool enabled);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern long GetElapsedNanoseconds();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern long GetGpuElapsedNanoseconds();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetSampleBlockCount();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetGpuSampleBlockCount();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void FilterToCurrentThread();

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CollectFromAllThreads();
	}
}
