using System;

namespace UnityEngine.Networking
{
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	public class ConnectionSimulatorConfig : IDisposable
	{
		internal int m_OutMinDelay;

		internal int m_OutAvgDelay;

		internal int m_InMinDelay;

		internal int m_InAvgDelay;

		internal float m_PacketLossPercentage;

		public ConnectionSimulatorConfig(int outMinDelay, int outAvgDelay, int inMinDelay, int inAvgDelay, float packetLossPercentage)
		{
			this.m_OutMinDelay = outMinDelay;
			this.m_OutAvgDelay = outAvgDelay;
			this.m_InMinDelay = inMinDelay;
			this.m_InAvgDelay = inAvgDelay;
			this.m_PacketLossPercentage = packetLossPercentage;
		}

		[ThreadAndSerializationSafe]
		public void Dispose()
		{
		}

		~ConnectionSimulatorConfig()
		{
			this.Dispose();
		}
	}
}
