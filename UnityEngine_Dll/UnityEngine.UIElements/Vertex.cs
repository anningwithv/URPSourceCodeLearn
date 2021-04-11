using System;

namespace UnityEngine.UIElements
{
	public struct Vertex
	{
		public static readonly float nearZ = 0f;

		public Vector3 position;

		public Color32 tint;

		public Vector2 uv;

		internal Color32 xformClipPages;

		internal Color32 idsFlags;

		internal Color32 opacityPageSVGSettingIndex;
	}
}
