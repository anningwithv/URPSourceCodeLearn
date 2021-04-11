using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	[MovedFrom("UnityEngine.Experimental.U2D")]
	public struct ShapeControlPoint
	{
		public Vector3 position;

		public Vector3 leftTangent;

		public Vector3 rightTangent;

		public int mode;
	}
}
