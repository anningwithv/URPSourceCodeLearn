using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/IMGUI/GUIClip.h"), NativeHeader("Modules/IMGUI/GUIState.h")]
	internal sealed class GUIClip
	{
		internal struct ParentClipScope : IDisposable
		{
			private bool m_Disposed;

			public ParentClipScope(Matrix4x4 objectTransform, Rect clipRect)
			{
				this.m_Disposed = false;
				GUIClip.Internal_PushParentClip(objectTransform, clipRect);
			}

			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUIClip.Internal_PopParentClip();
				}
			}
		}

		internal static extern bool enabled
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static Rect visibleRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetVisibleRect")]
			get
			{
				Rect result;
				GUIClip.get_visibleRect_Injected(out result);
				return result;
			}
		}

		internal static Rect topmostRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopMostPhysicalRect")]
			get
			{
				Rect result;
				GUIClip.get_topmostRect_Injected(out result);
				return result;
			}
		}

		internal static void Internal_Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.Internal_Push_Injected(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Pop();

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetCount();

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopRect")]
		internal static Rect GetTopRect()
		{
			Rect result;
			GUIClip.GetTopRect_Injected(out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
		private static Vector2 Unclip_Vector2(Vector2 pos)
		{
			Vector2 result;
			GUIClip.Unclip_Vector2_Injected(ref pos, out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
		private static Rect Unclip_Rect(Rect rect)
		{
			Rect result;
			GUIClip.Unclip_Rect_Injected(ref rect, out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
		private static Vector2 Clip_Vector2(Vector2 absolutePos)
		{
			Vector2 result;
			GUIClip.Clip_Vector2_Injected(ref absolutePos, out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
		private static Rect Internal_Clip_Rect(Rect absoluteRect)
		{
			Rect result;
			GUIClip.Internal_Clip_Rect_Injected(ref absoluteRect, out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
		private static Vector2 UnclipToWindow_Vector2(Vector2 pos)
		{
			Vector2 result;
			GUIClip.UnclipToWindow_Vector2_Injected(ref pos, out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
		private static Rect UnclipToWindow_Rect(Rect rect)
		{
			Rect result;
			GUIClip.UnclipToWindow_Rect_Injected(ref rect, out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
		private static Vector2 ClipToWindow_Vector2(Vector2 absolutePos)
		{
			Vector2 result;
			GUIClip.ClipToWindow_Vector2_Injected(ref absolutePos, out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
		private static Rect ClipToWindow_Rect(Rect absoluteRect)
		{
			Rect result;
			GUIClip.ClipToWindow_Rect_Injected(ref absoluteRect, out result);
			return result;
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetAbsoluteMousePosition")]
		private static Vector2 Internal_GetAbsoluteMousePosition()
		{
			Vector2 result;
			GUIClip.Internal_GetAbsoluteMousePosition_Injected(out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reapply();

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetUserMatrix")]
		internal static Matrix4x4 GetMatrix()
		{
			Matrix4x4 result;
			GUIClip.GetMatrix_Injected(out result);
			return result;
		}

		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.SetMatrix_Injected(ref m);
		}

		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetParentTransform")]
		internal static Matrix4x4 GetParentMatrix()
		{
			Matrix4x4 result;
			GUIClip.GetParentMatrix_Injected(out result);
			return result;
		}

		internal static void Internal_PushParentClip(Matrix4x4 objectTransform, Rect clipRect)
		{
			GUIClip.Internal_PushParentClip_Injected(ref objectTransform, ref clipRect);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_PopParentClip();

		internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.Internal_Push(screenRect, scrollOffset, renderOffset, resetOffset);
		}

		internal static void Pop()
		{
			GUIClip.Internal_Pop();
		}

		public static Vector2 Unclip(Vector2 pos)
		{
			return GUIClip.Unclip_Vector2(pos);
		}

		public static Rect Unclip(Rect rect)
		{
			return GUIClip.Unclip_Rect(rect);
		}

		public static Vector2 Clip(Vector2 absolutePos)
		{
			return GUIClip.Clip_Vector2(absolutePos);
		}

		public static Rect Clip(Rect absoluteRect)
		{
			return GUIClip.Internal_Clip_Rect(absoluteRect);
		}

		public static Vector2 UnclipToWindow(Vector2 pos)
		{
			return GUIClip.UnclipToWindow_Vector2(pos);
		}

		public static Rect UnclipToWindow(Rect rect)
		{
			return GUIClip.UnclipToWindow_Rect(rect);
		}

		public static Vector2 ClipToWindow(Vector2 absolutePos)
		{
			return GUIClip.ClipToWindow_Vector2(absolutePos);
		}

		public static Rect ClipToWindow(Rect absoluteRect)
		{
			return GUIClip.ClipToWindow_Rect(absoluteRect);
		}

		public static Vector2 GetAbsoluteMousePosition()
		{
			return GUIClip.Internal_GetAbsoluteMousePosition();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_visibleRect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_topmostRect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Push_Injected(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTopRect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unclip_Vector2_Injected(ref Vector2 pos, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unclip_Rect_Injected(ref Rect rect, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clip_Vector2_Injected(ref Vector2 absolutePos, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Clip_Rect_Injected(ref Rect absoluteRect, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnclipToWindow_Vector2_Injected(ref Vector2 pos, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnclipToWindow_Rect_Injected(ref Rect rect, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClipToWindow_Vector2_Injected(ref Vector2 absolutePos, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClipToWindow_Rect_Injected(ref Rect absoluteRect, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetAbsoluteMousePosition_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrix_Injected(ref Matrix4x4 m);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParentMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_PushParentClip_Injected(ref Matrix4x4 objectTransform, ref Rect clipRect);
	}
}
