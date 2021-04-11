using System;

namespace UnityEngine
{
	public class Compass
	{
		public float magneticHeading
		{
			get
			{
				return LocationService.GetLastHeading().magneticHeading;
			}
		}

		public float trueHeading
		{
			get
			{
				return LocationService.GetLastHeading().trueHeading;
			}
		}

		public float headingAccuracy
		{
			get
			{
				return LocationService.GetLastHeading().headingAccuracy;
			}
		}

		public Vector3 rawVector
		{
			get
			{
				return LocationService.GetLastHeading().raw;
			}
		}

		public double timestamp
		{
			get
			{
				return LocationService.GetLastHeading().timestamp;
			}
		}

		public bool enabled
		{
			get
			{
				return LocationService.IsHeadingUpdatesEnabled();
			}
			set
			{
				LocationService.SetHeadingUpdatesEnabled(value);
			}
		}
	}
}
