using System;

namespace UnityEngine.UIElements
{
	[Serializable]
	public class VectorImage : ScriptableObject
	{
		[SerializeField]
		internal Texture2D atlas = null;

		[SerializeField]
		internal VectorImageVertex[] vertices = null;

		[SerializeField]
		internal ushort[] indices = null;

		[SerializeField]
		internal GradientSettings[] settings = null;

		[SerializeField]
		internal Vector2 size = Vector2.zero;
	}
}
