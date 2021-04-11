using System;

namespace UnityEngine.UIElements
{
	[Serializable]
	internal struct VectorImageVertex
	{
		public Vector3 position;

		public Color32 tint;

		public Vector2 uv;

		public uint settingIndex;
	}
}
