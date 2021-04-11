using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	[Serializable]
	public class HostTopology
	{
		[SerializeField]
		private ConnectionConfig m_DefConfig = null;

		[SerializeField]
		private int m_MaxDefConnections = 0;

		[SerializeField]
		private List<ConnectionConfig> m_SpecialConnections = new List<ConnectionConfig>();

		[SerializeField]
		private ushort m_ReceivedMessagePoolSize = 1024;

		[SerializeField]
		private ushort m_SentMessagePoolSize = 1024;

		[SerializeField]
		private float m_MessagePoolSizeGrowthFactor = 0.75f;

		public ConnectionConfig DefaultConfig
		{
			get
			{
				return this.m_DefConfig;
			}
		}

		public int MaxDefaultConnections
		{
			get
			{
				return this.m_MaxDefConnections;
			}
		}

		public int SpecialConnectionConfigsCount
		{
			get
			{
				return this.m_SpecialConnections.Count;
			}
		}

		public List<ConnectionConfig> SpecialConnectionConfigs
		{
			get
			{
				return this.m_SpecialConnections;
			}
		}

		public ushort ReceivedMessagePoolSize
		{
			get
			{
				return this.m_ReceivedMessagePoolSize;
			}
			set
			{
				this.m_ReceivedMessagePoolSize = value;
			}
		}

		public ushort SentMessagePoolSize
		{
			get
			{
				return this.m_SentMessagePoolSize;
			}
			set
			{
				this.m_SentMessagePoolSize = value;
			}
		}

		public float MessagePoolSizeGrowthFactor
		{
			get
			{
				return this.m_MessagePoolSizeGrowthFactor;
			}
			set
			{
				bool flag = (double)value <= 0.5 || (double)value > 1.0;
				if (flag)
				{
					throw new ArgumentException("pool growth factor should be varied between 0.5 and 1.0");
				}
				this.m_MessagePoolSizeGrowthFactor = value;
			}
		}

		public HostTopology(ConnectionConfig defaultConfig, int maxDefaultConnections)
		{
			bool flag = defaultConfig == null;
			if (flag)
			{
				throw new NullReferenceException("config is not defined");
			}
			bool flag2 = maxDefaultConnections <= 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("maxConnections", "Number of connections should be > 0");
			}
			bool flag3 = maxDefaultConnections >= 65535;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("maxConnections", "Number of connections should be < 65535");
			}
			ConnectionConfig.Validate(defaultConfig);
			this.m_DefConfig = new ConnectionConfig(defaultConfig);
			this.m_MaxDefConnections = maxDefaultConnections;
		}

		private HostTopology()
		{
		}

		public ConnectionConfig GetSpecialConnectionConfig(int i)
		{
			bool flag = i > this.m_SpecialConnections.Count || i == 0;
			if (flag)
			{
				throw new ArgumentException("special configuration index is out of valid range");
			}
			return this.m_SpecialConnections[i - 1];
		}

		public int AddSpecialConnectionConfig(ConnectionConfig config)
		{
			bool flag = this.m_MaxDefConnections + this.m_SpecialConnections.Count + 1 >= 65535;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("maxConnections", "Number of connections should be < 65535");
			}
			this.m_SpecialConnections.Add(new ConnectionConfig(config));
			return this.m_SpecialConnections.Count;
		}
	}
}
