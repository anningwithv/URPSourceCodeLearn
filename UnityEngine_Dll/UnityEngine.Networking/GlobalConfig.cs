using System;

namespace UnityEngine.Networking
{
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	[Serializable]
	public class GlobalConfig
	{
		private const uint g_MaxTimerTimeout = 12000u;

		private const uint g_MaxNetSimulatorTimeout = 12000u;

		private const ushort g_MaxHosts = 128;

		[SerializeField]
		private uint m_ThreadAwakeTimeout;

		[SerializeField]
		private ReactorModel m_ReactorModel;

		[SerializeField]
		private ushort m_ReactorMaximumReceivedMessages;

		[SerializeField]
		private ushort m_ReactorMaximumSentMessages;

		[SerializeField]
		private ushort m_MaxPacketSize;

		[SerializeField]
		private ushort m_MaxHosts;

		[SerializeField]
		private byte m_ThreadPoolSize;

		[SerializeField]
		private uint m_MinTimerTimeout;

		[SerializeField]
		private uint m_MaxTimerTimeout;

		[SerializeField]
		private uint m_MinNetSimulatorTimeout;

		[SerializeField]
		private uint m_MaxNetSimulatorTimeout;

		[SerializeField]
		private Action<int, int> m_ConnectionReadyForSend;

		[SerializeField]
		private Action<int> m_NetworkEventAvailable;

		public uint ThreadAwakeTimeout
		{
			get
			{
				return this.m_ThreadAwakeTimeout;
			}
			set
			{
				bool flag = value == 0u;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Minimal thread awake timeout should be > 0");
				}
				this.m_ThreadAwakeTimeout = value;
			}
		}

		public ReactorModel ReactorModel
		{
			get
			{
				return this.m_ReactorModel;
			}
			set
			{
				this.m_ReactorModel = value;
			}
		}

		public ushort ReactorMaximumReceivedMessages
		{
			get
			{
				return this.m_ReactorMaximumReceivedMessages;
			}
			set
			{
				this.m_ReactorMaximumReceivedMessages = value;
			}
		}

		public ushort ReactorMaximumSentMessages
		{
			get
			{
				return this.m_ReactorMaximumSentMessages;
			}
			set
			{
				this.m_ReactorMaximumSentMessages = value;
			}
		}

		public ushort MaxPacketSize
		{
			get
			{
				return this.m_MaxPacketSize;
			}
			set
			{
				this.m_MaxPacketSize = value;
			}
		}

		public ushort MaxHosts
		{
			get
			{
				return this.m_MaxHosts;
			}
			set
			{
				bool flag = value == 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MaxHosts", "Maximum hosts number should be > 0");
				}
				bool flag2 = value > 128;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MaxHosts", "Maximum hosts number should be <= " + 128.ToString());
				}
				this.m_MaxHosts = value;
			}
		}

		public byte ThreadPoolSize
		{
			get
			{
				return this.m_ThreadPoolSize;
			}
			set
			{
				this.m_ThreadPoolSize = value;
			}
		}

		public uint MinTimerTimeout
		{
			get
			{
				return this.m_MinTimerTimeout;
			}
			set
			{
				bool flag = value > this.MaxTimerTimeout;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MinTimerTimeout should be < MaxTimerTimeout");
				}
				bool flag2 = value == 0u;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MinTimerTimeout should be > 0");
				}
				this.m_MinTimerTimeout = value;
			}
		}

		public uint MaxTimerTimeout
		{
			get
			{
				return this.m_MaxTimerTimeout;
			}
			set
			{
				bool flag = value == 0u;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MaxTimerTimeout should be > 0");
				}
				bool flag2 = value > 12000u;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MaxTimerTimeout should be <=" + 12000u.ToString());
				}
				this.m_MaxTimerTimeout = value;
			}
		}

		public uint MinNetSimulatorTimeout
		{
			get
			{
				return this.m_MinNetSimulatorTimeout;
			}
			set
			{
				bool flag = value > this.MaxNetSimulatorTimeout;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MinNetSimulatorTimeout should be < MaxTimerTimeout");
				}
				bool flag2 = value == 0u;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MinNetSimulatorTimeout should be > 0");
				}
				this.m_MinNetSimulatorTimeout = value;
			}
		}

		public uint MaxNetSimulatorTimeout
		{
			get
			{
				return this.m_MaxNetSimulatorTimeout;
			}
			set
			{
				bool flag = value == 0u;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("MaxNetSimulatorTimeout should be > 0");
				}
				bool flag2 = value > 12000u;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("MaxNetSimulatorTimeout should be <=" + 12000u.ToString());
				}
				this.m_MaxNetSimulatorTimeout = value;
			}
		}

		public Action<int> NetworkEventAvailable
		{
			get
			{
				return this.m_NetworkEventAvailable;
			}
			set
			{
				this.m_NetworkEventAvailable = value;
			}
		}

		public Action<int, int> ConnectionReadyForSend
		{
			get
			{
				return this.m_ConnectionReadyForSend;
			}
			set
			{
				this.m_ConnectionReadyForSend = value;
			}
		}

		public GlobalConfig()
		{
			this.m_ThreadAwakeTimeout = 1u;
			this.m_ReactorModel = ReactorModel.SelectReactor;
			this.m_ReactorMaximumReceivedMessages = 1024;
			this.m_ReactorMaximumSentMessages = 1024;
			this.m_MaxPacketSize = 2000;
			this.m_MaxHosts = 16;
			this.m_ThreadPoolSize = 1;
			this.m_MinTimerTimeout = 1u;
			this.m_MaxTimerTimeout = 12000u;
			this.m_MinNetSimulatorTimeout = 1u;
			this.m_MaxNetSimulatorTimeout = 12000u;
			this.m_ConnectionReadyForSend = null;
			this.m_NetworkEventAvailable = null;
		}
	}
}
