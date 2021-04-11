using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	[NativeConditional("ENABLE_NETWORK && ENABLE_UNET", true), NativeHeader("Modules/UNET/UNETConfiguration.h")]
	internal class GlobalConfigInternal : IDisposable
	{
		public IntPtr m_Ptr;

		[NativeProperty("m_ThreadAwakeTimeout", TargetType.Field)]
		private extern uint ThreadAwakeTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_ReactorModel", TargetType.Field)]
		private extern byte ReactorModel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_ReactorMaximumReceivedMessages", TargetType.Field)]
		private extern ushort ReactorMaximumReceivedMessages
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_ReactorMaximumSentMessages", TargetType.Field)]
		private extern ushort ReactorMaximumSentMessages
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_MaxPacketSize", TargetType.Field)]
		private extern ushort MaxPacketSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_MaxHosts", TargetType.Field)]
		private extern ushort MaxHosts
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_ThreadPoolSize", TargetType.Field)]
		private extern byte ThreadPoolSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_MinTimerTimeout", TargetType.Field)]
		private extern uint MinTimerTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_MaxTimerTimeout", TargetType.Field)]
		private extern uint MaxTimerTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_MinNetSimulatorTimeout", TargetType.Field)]
		private extern uint MinNetSimulatorTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_MaxNetSimulatorTimeout", TargetType.Field)]
		private extern uint MaxNetSimulatorTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public GlobalConfigInternal(GlobalConfig config)
		{
			bool flag = config == null;
			if (flag)
			{
				throw new NullReferenceException("config is not defined");
			}
			this.m_Ptr = GlobalConfigInternal.InternalCreate();
			this.ThreadAwakeTimeout = config.ThreadAwakeTimeout;
			this.ReactorModel = (byte)config.ReactorModel;
			this.ReactorMaximumReceivedMessages = config.ReactorMaximumReceivedMessages;
			this.ReactorMaximumSentMessages = config.ReactorMaximumSentMessages;
			this.MaxPacketSize = config.MaxPacketSize;
			this.MaxHosts = config.MaxHosts;
			bool flag2 = config.ThreadPoolSize == 0 || config.ThreadPoolSize > 254;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("Worker thread pool size should be >= 1 && < 254 (for server only)");
			}
			byte threadPoolSize = config.ThreadPoolSize;
			bool flag3 = config.ThreadPoolSize > 1;
			if (flag3)
			{
				Debug.LogWarning("Worker thread pool size can be > 1 only for server platforms: Win, OSX or Linux");
				threadPoolSize = 1;
			}
			this.ThreadPoolSize = threadPoolSize;
			this.MinTimerTimeout = config.MinTimerTimeout;
			this.MaxTimerTimeout = config.MaxTimerTimeout;
			this.MinNetSimulatorTimeout = config.MinNetSimulatorTimeout;
			this.MaxNetSimulatorTimeout = config.MaxNetSimulatorTimeout;
		}

		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				GlobalConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		~GlobalConfigInternal()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				GlobalConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalCreate();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);
	}
}
