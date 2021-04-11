using System;

namespace UnityEngine.UIElements
{
	public class MeshGenerationContext
	{
		[Flags]
		internal enum MeshFlags
		{
			None = 0,
			UVisDisplacement = 1,
			IsSVGGradients = 2,
			IsCustomSVGGradients = 3
		}

		internal IStylePainter painter;

		public VisualElement visualElement
		{
			get
			{
				return this.painter.visualElement;
			}
		}

		internal MeshGenerationContext(IStylePainter painter)
		{
			this.painter = painter;
		}

		public MeshWriteData Allocate(int vertexCount, int indexCount, Texture texture = null)
		{
			return this.painter.DrawMesh(vertexCount, indexCount, texture, null, MeshGenerationContext.MeshFlags.None);
		}

		internal MeshWriteData Allocate(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			return this.painter.DrawMesh(vertexCount, indexCount, texture, material, flags);
		}
	}
}
