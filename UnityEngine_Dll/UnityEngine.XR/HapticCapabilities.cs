using System;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR")]
	public struct HapticCapabilities : IEquatable<HapticCapabilities>
	{
		private uint m_NumChannels;

		private bool m_SupportsImpulse;

		private bool m_SupportsBuffer;

		private uint m_BufferFrequencyHz;

		private uint m_BufferMaxSize;

		private uint m_BufferOptimalSize;

		public uint numChannels
		{
			get
			{
				return this.m_NumChannels;
			}
			internal set
			{
				this.m_NumChannels = value;
			}
		}

		public bool supportsImpulse
		{
			get
			{
				return this.m_SupportsImpulse;
			}
			internal set
			{
				this.m_SupportsImpulse = value;
			}
		}

		public bool supportsBuffer
		{
			get
			{
				return this.m_SupportsBuffer;
			}
			internal set
			{
				this.m_SupportsBuffer = value;
			}
		}

		public uint bufferFrequencyHz
		{
			get
			{
				return this.m_BufferFrequencyHz;
			}
			internal set
			{
				this.m_BufferFrequencyHz = value;
			}
		}

		public uint bufferMaxSize
		{
			get
			{
				return this.m_BufferMaxSize;
			}
			internal set
			{
				this.m_BufferMaxSize = value;
			}
		}

		public uint bufferOptimalSize
		{
			get
			{
				return this.m_BufferOptimalSize;
			}
			internal set
			{
				this.m_BufferOptimalSize = value;
			}
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is HapticCapabilities);
			return !flag && this.Equals((HapticCapabilities)obj);
		}

		public bool Equals(HapticCapabilities other)
		{
			return this.numChannels == other.numChannels && this.supportsImpulse == other.supportsImpulse && this.supportsBuffer == other.supportsBuffer && this.bufferFrequencyHz == other.bufferFrequencyHz && this.bufferMaxSize == other.bufferMaxSize && this.bufferOptimalSize == other.bufferOptimalSize;
		}

		public override int GetHashCode()
		{
			return this.numChannels.GetHashCode() ^ this.supportsImpulse.GetHashCode() << 1 ^ this.supportsBuffer.GetHashCode() >> 1 ^ this.bufferFrequencyHz.GetHashCode() << 2 ^ this.bufferMaxSize.GetHashCode() >> 2 ^ this.bufferOptimalSize.GetHashCode() << 3;
		}

		public static bool operator ==(HapticCapabilities a, HapticCapabilities b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(HapticCapabilities a, HapticCapabilities b)
		{
			return !(a == b);
		}
	}
}
