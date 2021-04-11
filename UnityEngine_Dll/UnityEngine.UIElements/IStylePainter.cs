using System;

namespace UnityEngine.UIElements
{
	internal interface IStylePainter
	{
		VisualElement visualElement
		{
			get;
		}

		MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags);

		void DrawText(MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint);

		void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams);

		void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams);

		void DrawImmediate(Action callback, bool cullingEnabled);
	}
}
