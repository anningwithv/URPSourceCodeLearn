using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.AI
{
	public struct NavMeshLocation
	{
		public PolygonId polygon
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<polygon>k__BackingField;
			}
		}

		public Vector3 position
		{
			[CompilerGenerated, IsReadOnly]
			get
			{
				return this.<position>k__BackingField;
			}
		}

		internal NavMeshLocation(Vector3 position, PolygonId polygon)
		{
			this.<position>k__BackingField = position;
			this.<polygon>k__BackingField = polygon;
		}
	}
}
