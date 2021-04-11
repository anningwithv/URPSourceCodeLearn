using System;

namespace UnityEngine.Networking
{
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	[Serializable]
	public class ChannelQOS
	{
		[SerializeField]
		internal QosType m_Type;

		[SerializeField]
		internal bool m_BelongsSharedOrderChannel;

		public QosType QOS
		{
			get
			{
				return this.m_Type;
			}
		}

		public bool BelongsToSharedOrderChannel
		{
			get
			{
				return this.m_BelongsSharedOrderChannel;
			}
		}

		public ChannelQOS(QosType value)
		{
			this.m_Type = value;
			this.m_BelongsSharedOrderChannel = false;
		}

		public ChannelQOS()
		{
			this.m_Type = QosType.Unreliable;
			this.m_BelongsSharedOrderChannel = false;
		}

		public ChannelQOS(ChannelQOS channel)
		{
			bool flag = channel == null;
			if (flag)
			{
				throw new NullReferenceException("channel is not defined");
			}
			this.m_Type = channel.m_Type;
			this.m_BelongsSharedOrderChannel = channel.m_BelongsSharedOrderChannel;
		}
	}
}
