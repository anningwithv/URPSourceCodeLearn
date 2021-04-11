using System;

namespace UnityEngine
{
	public struct LocationInfo
	{
		internal double m_Timestamp;

		internal float m_Latitude;

		internal float m_Longitude;

		internal float m_Altitude;

		internal float m_HorizontalAccuracy;

		internal float m_VerticalAccuracy;

		public float latitude
		{
			get
			{
				return this.m_Latitude;
			}
		}

		public float longitude
		{
			get
			{
				return this.m_Longitude;
			}
		}

		public float altitude
		{
			get
			{
				return this.m_Altitude;
			}
		}

		public float horizontalAccuracy
		{
			get
			{
				return this.m_HorizontalAccuracy;
			}
		}

		public float verticalAccuracy
		{
			get
			{
				return this.m_VerticalAccuracy;
			}
		}

		public double timestamp
		{
			get
			{
				return this.m_Timestamp;
			}
		}
	}
}
