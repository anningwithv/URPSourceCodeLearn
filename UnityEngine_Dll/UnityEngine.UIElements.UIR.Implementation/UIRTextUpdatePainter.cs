using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR.Implementation
{
	internal class UIRTextUpdatePainter : IStylePainter, IDisposable
	{
		private VisualElement m_CurrentElement;

		private int m_TextEntryIndex;

		private NativeArray<Vertex> m_DudVerts;

		private NativeArray<ushort> m_DudIndices;

		private NativeSlice<Vertex> m_MeshDataVerts;

		private Color32 m_XFormClipPages;

		private Color32 m_IDsFlags;

		private Color32 m_OpacityPagesSettingsIndex;

		public MeshGenerationContext meshGenerationContext
		{
			[CompilerGenerated]
			get
			{
				return this.<meshGenerationContext>k__BackingField;
			}
		}

		public VisualElement visualElement
		{
			get
			{
				return this.m_CurrentElement;
			}
		}

		public UIRTextUpdatePainter()
		{
			this.<meshGenerationContext>k__BackingField = new MeshGenerationContext(this);
		}

		public void Begin(VisualElement ve, UIRenderDevice device)
		{
			Debug.Assert(ve.renderChainData.usesLegacyText && ve.renderChainData.textEntries.Count > 0);
			this.m_CurrentElement = ve;
			this.m_TextEntryIndex = 0;
			Alloc allocVerts = ve.renderChainData.data.allocVerts;
			NativeSlice<Vertex> slice = ve.renderChainData.data.allocPage.vertices.cpuData.Slice((int)allocVerts.start, (int)allocVerts.size);
			device.Update(ve.renderChainData.data, ve.renderChainData.data.allocVerts.size, out this.m_MeshDataVerts);
			RenderChainTextEntry renderChainTextEntry = ve.renderChainData.textEntries[0];
			bool flag = ve.renderChainData.textEntries.Count > 1 || renderChainTextEntry.vertexCount != this.m_MeshDataVerts.Length;
			if (flag)
			{
				this.m_MeshDataVerts.CopyFrom(slice);
			}
			int firstVertex = renderChainTextEntry.firstVertex;
			this.m_XFormClipPages = slice[firstVertex].xformClipPages;
			this.m_IDsFlags = slice[firstVertex].idsFlags;
			this.m_OpacityPagesSettingsIndex = slice[firstVertex].opacityPageSVGSettingIndex;
		}

		public void End()
		{
			Debug.Assert(this.m_TextEntryIndex == this.m_CurrentElement.renderChainData.textEntries.Count);
			this.m_CurrentElement = null;
		}

		public void Dispose()
		{
			bool isCreated = this.m_DudVerts.IsCreated;
			if (isCreated)
			{
				this.m_DudVerts.Dispose();
			}
			bool isCreated2 = this.m_DudIndices.IsCreated;
			if (isCreated2)
			{
				this.m_DudIndices.Dispose();
			}
		}

		public void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams)
		{
		}

		public void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams)
		{
		}

		public void DrawImmediate(Action callback, bool cullingEnabled)
		{
		}

		public MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			bool flag = this.m_DudVerts.Length < vertexCount;
			if (flag)
			{
				bool isCreated = this.m_DudVerts.IsCreated;
				if (isCreated)
				{
					this.m_DudVerts.Dispose();
				}
				this.m_DudVerts = new NativeArray<Vertex>(vertexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			bool flag2 = this.m_DudIndices.Length < indexCount;
			if (flag2)
			{
				bool isCreated2 = this.m_DudIndices.IsCreated;
				if (isCreated2)
				{
					this.m_DudIndices.Dispose();
				}
				this.m_DudIndices = new NativeArray<ushort>(indexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			return new MeshWriteData
			{
				m_Vertices = this.m_DudVerts.Slice(0, vertexCount),
				m_Indices = this.m_DudIndices.Slice(0, indexCount)
			};
		}

		public void DrawText(MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint)
		{
			bool flag = textParams.font == null;
			if (!flag)
			{
				bool flag2 = this.m_CurrentElement.panel.contextType == ContextType.Editor;
				if (flag2)
				{
					textParams.fontColor *= textParams.playmodeTintColor;
				}
				float scaling = TextNative.ComputeTextScaling(this.m_CurrentElement.worldTransform, pixelsPerPoint);
				TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(textParams, scaling);
				using (NativeArray<TextVertex> vertices = TextNative.GetVertices(textNativeSettings))
				{
					List<RenderChainTextEntry> arg_91_0 = this.m_CurrentElement.renderChainData.textEntries;
					int textEntryIndex = this.m_TextEntryIndex;
					this.m_TextEntryIndex = textEntryIndex + 1;
					RenderChainTextEntry renderChainTextEntry = arg_91_0[textEntryIndex];
					Vector2 offset = TextNative.GetOffset(textNativeSettings, textParams.rect);
					MeshBuilder.UpdateText(vertices, offset, this.m_CurrentElement.renderChainData.verticesSpace, this.m_XFormClipPages, this.m_IDsFlags, this.m_OpacityPagesSettingsIndex, this.m_MeshDataVerts.Slice(renderChainTextEntry.firstVertex, renderChainTextEntry.vertexCount));
					renderChainTextEntry.command.state.font = textParams.font.material.mainTexture;
				}
			}
		}
	}
}
