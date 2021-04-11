using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR.Tango
{
	[NativeHeader("ARScriptingClasses.h"), UsedByNativeCode]
	internal struct PoseData
	{
		public double orientation_x;

		public double orientation_y;

		public double orientation_z;

		public double orientation_w;

		public double translation_x;

		public double translation_y;

		public double translation_z;

		public PoseStatus statusCode;

		public Quaternion rotation
		{
			get
			{
				return new Quaternion((float)this.orientation_x, (float)this.orientation_y, (float)this.orientation_z, (float)this.orientation_w);
			}
		}

		public Vector3 position
		{
			get
			{
				return new Vector3((float)this.translation_x, (float)this.translation_y, (float)this.translation_z);
			}
		}
	}
}
