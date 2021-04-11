using System;

namespace UnityEngine.UIElements.UIR
{
	internal class RenderChainCommand : PoolItem
	{
		internal VisualElement owner;

		internal RenderChainCommand prev;

		internal RenderChainCommand next;

		internal bool closing;

		internal CommandType type;

		internal State state;

		internal MeshHandle mesh;

		internal int indexOffset;

		internal int indexCount;

		internal Action callback;

		internal void Reset()
		{
			this.owner = null;
			this.prev = (this.next = null);
			this.closing = false;
			this.type = CommandType.Draw;
			this.state = default(State);
			this.mesh = null;
			this.indexOffset = (this.indexCount = 0);
			this.callback = null;
		}

		internal void ExecuteNonDrawMesh(DrawParams drawParams, float pixelsPerPoint, ref Exception immediateException)
		{
			switch (this.type)
			{
			case CommandType.ImmediateCull:
			{
				bool flag = !RenderChainCommand.RectPointsToPixelsAndFlipYAxis(this.owner.worldBound, pixelsPerPoint).Overlaps(Utility.GetActiveViewport());
				if (flag)
				{
					return;
				}
				break;
			}
			case CommandType.Immediate:
				break;
			case CommandType.PushView:
			{
				ViewTransform viewTransform = new ViewTransform
				{
					transform = this.owner.worldTransform,
					clipRect = RenderChainCommand.RectToClipSpace(this.owner.worldClip)
				};
				drawParams.view.Push(viewTransform);
				GL.modelview = viewTransform.transform;
				return;
			}
			case CommandType.PopView:
				drawParams.view.Pop();
				GL.modelview = drawParams.view.Peek().transform;
				return;
			case CommandType.PushScissor:
			{
				Rect rect = RenderChainCommand.CombineScissorRects(this.owner.worldClip, drawParams.scissor.Peek());
				drawParams.scissor.Push(rect);
				Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect, pixelsPerPoint));
				return;
			}
			case CommandType.PopScissor:
			{
				drawParams.scissor.Pop();
				Rect rect2 = drawParams.scissor.Peek();
				bool flag2 = rect2.x == DrawParams.k_UnlimitedRect.x;
				if (flag2)
				{
					Utility.DisableScissor();
				}
				else
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect2, pixelsPerPoint));
				}
				return;
			}
			default:
				return;
			}
			bool flag3 = immediateException != null;
			if (!flag3)
			{
				Matrix4x4 unityProjectionMatrix = Utility.GetUnityProjectionMatrix();
				bool flag4 = drawParams.scissor.Count > 1;
				bool flag5 = flag4;
				if (flag5)
				{
					Utility.DisableScissor();
				}
				Utility.ProfileImmediateRendererBegin();
				try
				{
					using (new GUIClip.ParentClipScope(this.owner.worldTransform, this.owner.worldClip))
					{
						this.callback();
					}
				}
				catch (Exception ex)
				{
					immediateException = ex;
				}
				GL.modelview = drawParams.view.Peek().transform;
				GL.LoadProjectionMatrix(unityProjectionMatrix);
				Utility.ProfileImmediateRendererEnd();
				bool flag6 = flag4;
				if (flag6)
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(drawParams.scissor.Peek(), pixelsPerPoint));
				}
			}
		}

		private static Vector4 RectToClipSpace(Rect rc)
		{
			Matrix4x4 deviceProjectionMatrix = Utility.GetDeviceProjectionMatrix();
			Vector3 vector = deviceProjectionMatrix.MultiplyPoint(new Vector3(rc.xMin, rc.yMin, 0f));
			Vector3 vector2 = deviceProjectionMatrix.MultiplyPoint(new Vector3(rc.xMax, rc.yMax, 0f));
			return new Vector4(Mathf.Min(vector.x, vector2.x), Mathf.Min(vector.y, vector2.y), Mathf.Max(vector.x, vector2.x), Mathf.Max(vector.y, vector2.y));
		}

		private static Rect CombineScissorRects(Rect r0, Rect r1)
		{
			Rect result = new Rect(0f, 0f, 0f, 0f);
			result.x = Math.Max(r0.x, r1.x);
			result.y = Math.Max(r0.y, r1.y);
			result.xMax = Math.Max(result.x, Math.Min(r0.xMax, r1.xMax));
			result.yMax = Math.Max(result.y, Math.Min(r0.yMax, r1.yMax));
			return result;
		}

		private static RectInt RectPointsToPixelsAndFlipYAxis(Rect rect, float pixelsPerPoint)
		{
			float num = (float)Utility.GetActiveViewport().height;
			return new RectInt(0, 0, 0, 0)
			{
				x = Mathf.RoundToInt(rect.x * pixelsPerPoint),
				y = Mathf.RoundToInt(num - rect.yMax * pixelsPerPoint),
				width = Mathf.RoundToInt(rect.width * pixelsPerPoint),
				height = Mathf.RoundToInt(rect.height * pixelsPerPoint)
			};
		}
	}
}
