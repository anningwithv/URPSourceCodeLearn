using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	[NativeConditional("ENABLE_NETWORK && ENABLE_UNET", true), NativeHeader("Modules/UNET/UNETConfiguration.h")]
	internal class ConnectionSimulatorConfigInternal : IDisposable
	{
		public IntPtr m_Ptr;

		public ConnectionSimulatorConfigInternal(ConnectionSimulatorConfig config)
		{
			this.m_Ptr = ConnectionSimulatorConfigInternal.InternalCreate(config.m_OutMinDelay, config.m_OutAvgDelay, config.m_InMinDelay, config.m_InAvgDelay, config.m_PacketLossPercentage);
		}

		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ConnectionSimulatorConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		~ConnectionSimulatorConfigInternal()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ConnectionSimulatorConfigInternal.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalCreate(int outMinDelay, int outAvgDelay, int inMinDelay, int inAvgDelay, float packetLossPercentage);

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);
	}
}
