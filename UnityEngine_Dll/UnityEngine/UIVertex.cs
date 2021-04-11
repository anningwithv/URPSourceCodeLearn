using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct UIVertex
	{
		public Vector3 position;

		public Vector3 normal;

		public Vector4 tangent;

		public Color32 color;

		public Vector4 uv0;

		public Vector4 uv1;

		public Vector4 uv2;

		public Vector4 uv3;

		private static readonly Color32 s_DefaultColor = new Color32(255, 255, 255, 255);

		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		public static UIVertex simpleVert = new UIVertex
		{
			position = Vector3.zero,
			normal = Vector3.back,
			tangent = UIVertex.s_DefaultTangent,
			color = UIVertex.s_DefaultColor,
			uv0 = Vector4.zero,
			uv1 = Vector4.zero,
			uv2 = Vector4.zero,
			uv3 = Vector4.zero
		};
	}
}
