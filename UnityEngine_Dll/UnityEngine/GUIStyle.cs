using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h"), NativeHeader("IMGUIScriptingClasses.h"), RequiredByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyle
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		[NonSerialized]
		private GUIStyleState m_Normal;

		[NonSerialized]
		private GUIStyleState m_Hover;

		[NonSerialized]
		private GUIStyleState m_Active;

		[NonSerialized]
		private GUIStyleState m_Focused;

		[NonSerialized]
		private GUIStyleState m_OnNormal;

		[NonSerialized]
		private GUIStyleState m_OnHover;

		[NonSerialized]
		private GUIStyleState m_OnActive;

		[NonSerialized]
		private GUIStyleState m_OnFocused;

		[NonSerialized]
		private RectOffset m_Border;

		[NonSerialized]
		private RectOffset m_Padding;

		[NonSerialized]
		private RectOffset m_Margin;

		[NonSerialized]
		private RectOffset m_Overflow;

		[NonSerialized]
		private string m_Name;

		internal static DrawHandler onDraw;

		internal int blockId;

		internal static bool showKeyboardFocus = true;

		private static GUIStyle s_None;

		[NativeProperty("Name", false, TargetType.Function)]
		internal extern string rawName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Font", false, TargetType.Function)]
		public extern Font font
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_ImagePosition", false, TargetType.Field)]
		public extern ImagePosition imagePosition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_Alignment", false, TargetType.Field)]
		public extern TextAnchor alignment
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_WordWrap", false, TargetType.Field)]
		public extern bool wordWrap
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_Clipping", false, TargetType.Field)]
		public extern TextClipping clipping
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_ContentOffset", false, TargetType.Field)]
		public Vector2 contentOffset
		{
			get
			{
				Vector2 result;
				this.get_contentOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_contentOffset_Injected(ref value);
			}
		}

		[NativeProperty("m_FixedWidth", false, TargetType.Field)]
		public extern float fixedWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_FixedHeight", false, TargetType.Field)]
		public extern float fixedHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_StretchWidth", false, TargetType.Field)]
		public extern bool stretchWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_StretchHeight", false, TargetType.Field)]
		public extern bool stretchHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_FontSize", false, TargetType.Field)]
		public extern int fontSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_FontStyle", false, TargetType.Field)]
		public extern FontStyle fontStyle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("m_RichText", false, TargetType.Field)]
		public extern bool richText
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Don't use clipOffset - put things inside BeginGroup instead. This functionality will be removed in a later version.", false), NativeProperty("m_ClipOffset", false, TargetType.Field)]
		public Vector2 clipOffset
		{
			get
			{
				Vector2 result;
				this.get_clipOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_clipOffset_Injected(ref value);
			}
		}

		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		internal Vector2 Internal_clipOffset
		{
			get
			{
				Vector2 result;
				this.get_Internal_clipOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_Internal_clipOffset_Injected(ref value);
			}
		}

		public string name
		{
			get
			{
				string arg_1A_0;
				if ((arg_1A_0 = this.m_Name) == null)
				{
					arg_1A_0 = (this.m_Name = this.rawName);
				}
				return arg_1A_0;
			}
			set
			{
				this.m_Name = value;
				this.rawName = value;
			}
		}

		public GUIStyleState normal
		{
			get
			{
				GUIStyleState arg_21_0;
				if ((arg_21_0 = this.m_Normal) == null)
				{
					arg_21_0 = (this.m_Normal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(0)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignStyleState(0, value.m_Ptr);
			}
		}

		public GUIStyleState hover
		{
			get
			{
				GUIStyleState arg_21_0;
				if ((arg_21_0 = this.m_Hover) == null)
				{
					arg_21_0 = (this.m_Hover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(1)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignStyleState(1, value.m_Ptr);
			}
		}

		public GUIStyleState active
		{
			get
			{
				GUIStyleState arg_21_0;
				if ((arg_21_0 = this.m_Active) == null)
				{
					arg_21_0 = (this.m_Active = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(2)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignStyleState(2, value.m_Ptr);
			}
		}

		public GUIStyleState onNormal
		{
			get
			{
				GUIStyleState arg_21_0;
				if ((arg_21_0 = this.m_OnNormal) == null)
				{
					arg_21_0 = (this.m_OnNormal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(4)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignStyleState(4, value.m_Ptr);
			}
		}

		public GUIStyleState onHover
		{
			get
			{
				GUIStyleState arg_21_0;
				if ((arg_21_0 = this.m_OnHover) == null)
				{
					arg_21_0 = (this.m_OnHover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(5)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignStyleState(5, value.m_Ptr);
			}
		}

		public GUIStyleState onActive
		{
			get
			{
				GUIStyleState arg_21_0;
				if ((arg_21_0 = this.m_OnActive) == null)
				{
					arg_21_0 = (this.m_OnActive = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(6)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignStyleState(6, value.m_Ptr);
			}
		}

		public GUIStyleState focused
		{
			get
			{
				GUIStyleState arg_21_0;
				if ((arg_21_0 = this.m_Focused) == null)
				{
					arg_21_0 = (this.m_Focused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(3)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignStyleState(3, value.m_Ptr);
			}
		}

		public GUIStyleState onFocused
		{
			get
			{
				GUIStyleState arg_21_0;
				if ((arg_21_0 = this.m_OnFocused) == null)
				{
					arg_21_0 = (this.m_OnFocused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(7)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignStyleState(7, value.m_Ptr);
			}
		}

		public RectOffset border
		{
			get
			{
				RectOffset arg_21_0;
				if ((arg_21_0 = this.m_Border) == null)
				{
					arg_21_0 = (this.m_Border = new RectOffset(this, this.GetRectOffsetPtr(0)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignRectOffset(0, value.m_Ptr);
			}
		}

		public RectOffset margin
		{
			get
			{
				RectOffset arg_21_0;
				if ((arg_21_0 = this.m_Margin) == null)
				{
					arg_21_0 = (this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignRectOffset(1, value.m_Ptr);
			}
		}

		public RectOffset padding
		{
			get
			{
				RectOffset arg_21_0;
				if ((arg_21_0 = this.m_Padding) == null)
				{
					arg_21_0 = (this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignRectOffset(2, value.m_Ptr);
			}
		}

		public RectOffset overflow
		{
			get
			{
				RectOffset arg_21_0;
				if ((arg_21_0 = this.m_Overflow) == null)
				{
					arg_21_0 = (this.m_Overflow = new RectOffset(this, this.GetRectOffsetPtr(3)));
				}
				return arg_21_0;
			}
			set
			{
				this.AssignRectOffset(3, value.m_Ptr);
			}
		}

		public float lineHeight
		{
			get
			{
				return Mathf.Round(GUIStyle.Internal_GetLineHeight(this.m_Ptr));
			}
		}

		public static GUIStyle none
		{
			get
			{
				GUIStyle arg_14_0;
				if ((arg_14_0 = GUIStyle.s_None) == null)
				{
					arg_14_0 = (GUIStyle.s_None = new GUIStyle());
				}
				return arg_14_0;
			}
		}

		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(GUIStyle self);

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Copy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Copy(GUIStyle self, GUIStyle other);

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr self);

		[FreeFunction(Name = "GUIStyle_Bindings::GetStyleStatePtr", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetStyleStatePtr(int idx);

		[FreeFunction(Name = "GUIStyle_Bindings::AssignStyleState", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AssignStyleState(int idx, IntPtr srcStyleState);

		[FreeFunction(Name = "GUIStyle_Bindings::GetRectOffsetPtr", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetRectOffsetPtr(int idx);

		[FreeFunction(Name = "GUIStyle_Bindings::AssignRectOffset", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AssignRectOffset(int idx, IntPtr srcRectOffset);

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetLineHeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw", HasExplicitThis = true)]
		private void Internal_Draw(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Internal_Draw_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw2", HasExplicitThis = true)]
		private void Internal_Draw2(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Internal_Draw2_Injected(ref position, content, controlID, on);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawCursor", HasExplicitThis = true)]
		private void Internal_DrawCursor(Rect position, GUIContent content, int pos, Color cursorColor)
		{
			this.Internal_DrawCursor_Injected(ref position, content, pos, ref cursorColor);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawWithTextSelection", HasExplicitThis = true)]
		private void Internal_DrawWithTextSelection(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, Color cursorColor, Color selectionColor)
		{
			this.Internal_DrawWithTextSelection_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus, drawSelectionAsComposition, cursorFirst, cursorLast, ref cursorColor, ref selectionColor);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorPixelPosition", HasExplicitThis = true)]
		internal Vector2 Internal_GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Vector2 result;
			this.Internal_GetCursorPixelPosition_Injected(ref position, content, cursorStringIndex, out result);
			return result;
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorStringIndex", HasExplicitThis = true)]
		internal int Internal_GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return this.Internal_GetCursorStringIndex_Injected(ref position, content, ref cursorPixelPosition);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetSelectedRenderedText", HasExplicitThis = true)]
		internal string Internal_GetSelectedRenderedText(Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex)
		{
			return this.Internal_GetSelectedRenderedText_Injected(ref localPosition, mContent, selectIndex, cursorIndex);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetHyperlinksRect", HasExplicitThis = true)]
		internal Rect[] Internal_GetHyperlinksRect(Rect localPosition, GUIContent mContent)
		{
			return this.Internal_GetHyperlinksRect_Injected(ref localPosition, mContent);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetNumCharactersThatFitWithinWidth", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int Internal_GetNumCharactersThatFitWithinWidth(string text, float width);

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSize", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSize(GUIContent content)
		{
			Vector2 result;
			this.Internal_CalcSize_Injected(content, out result);
			return result;
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSizeWithConstraints", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSizeWithConstraints(GUIContent content, Vector2 maxSize)
		{
			Vector2 result;
			this.Internal_CalcSizeWithConstraints_Injected(content, ref maxSize, out result);
			return result;
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcHeight", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float Internal_CalcHeight(GUIContent content, float width);

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcMinMaxWidth", HasExplicitThis = true)]
		private Vector2 Internal_CalcMinMaxWidth(GUIContent content)
		{
			Vector2 result;
			this.Internal_CalcMinMaxWidth_Injected(content, out result);
			return result;
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawPrefixLabel", HasExplicitThis = true)]
		private void Internal_DrawPrefixLabel(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Internal_DrawPrefixLabel_Injected(ref position, content, controlID, on);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawContent", HasExplicitThis = true)]
		internal void Internal_DrawContent(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool hasTextInput, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, Color cursorColor, Color selectionColor, Color imageColor, float textOffsetX, float textOffsetY, float imageTopOffset, float imageLeftOffset, bool overflowX, bool overflowY)
		{
			this.Internal_DrawContent_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus, hasTextInput, drawSelectionAsComposition, cursorFirst, cursorLast, ref cursorColor, ref selectionColor, ref imageColor, textOffsetX, textOffsetY, imageTopOffset, imageLeftOffset, overflowX, overflowY);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::SetMouseTooltip")]
		internal static void SetMouseTooltip(string tooltip, Rect screenRect)
		{
			GUIStyle.SetMouseTooltip_Injected(tooltip, ref screenRect);
		}

		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorFlashOffset")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashOffset();

		[FreeFunction(Name = "GUIStyle::SetDefaultFont")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDefaultFont(Font font);

		public GUIStyle()
		{
			this.m_Ptr = GUIStyle.Internal_Create(this);
		}

		public GUIStyle(GUIStyle other)
		{
			bool flag = other == null;
			if (flag)
			{
				Debug.LogError("Copied style is null. Using StyleNotFound instead.");
				other = GUISkin.error;
			}
			this.m_Ptr = GUIStyle.Internal_Copy(this, other);
		}

		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					GUIStyle.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		internal static void CleanupRoots()
		{
			GUIStyle.s_None = null;
		}

		internal void InternalOnAfterDeserialize()
		{
			this.m_Normal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(0));
			this.m_Hover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(1));
			this.m_Active = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(2));
			this.m_Focused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(3));
			this.m_OnNormal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(4));
			this.m_OnHover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(5));
			this.m_OnActive = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(6));
			this.m_OnFocused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(7));
		}

		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.none, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(text), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(image), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, content, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		public void Draw(Rect position, GUIContent content, int controlID)
		{
			this.Draw(position, content, controlID, false, false, false, false);
		}

		public void Draw(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Draw(position, content, controlID, false, false, on, false);
		}

		public void Draw(Rect position, GUIContent content, int controlID, bool on, bool hover)
		{
			this.Draw(position, content, controlID, hover, GUIUtility.hotControl == controlID, on, GUIUtility.HasKeyFocus(controlID));
		}

		private void Draw(Rect position, GUIContent content, int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			bool flag = Event.current.type != EventType.Repaint;
			if (flag)
			{
				throw new Exception("Style.Draw may not be called if it is not a repaint event");
			}
			bool flag2 = content == null;
			if (flag2)
			{
				throw new Exception("Style.Draw may not be called with GUIContent that is null.");
			}
			DrawStates states = new DrawStates(controlId, isHover, isActive, on, hasKeyboardFocus);
			bool flag3 = GUIStyle.onDraw == null || !GUIStyle.onDraw(this, position, content, states);
			if (flag3)
			{
				bool flag4 = controlId == -1;
				if (flag4)
				{
					this.Internal_Draw(position, content, isHover, isActive, on, hasKeyboardFocus);
				}
				else
				{
					this.Internal_Draw2(position, content, controlId, on);
				}
			}
		}

		internal void DrawPrefixLabel(Rect position, GUIContent content, int controlID)
		{
			bool flag = content != null;
			if (flag)
			{
				DrawStates states = new DrawStates(controlID, position.Contains(Event.current.mousePosition), false, false, GUIUtility.HasKeyFocus(controlID));
				bool flag2 = GUIStyle.onDraw == null || !GUIStyle.onDraw(this, position, content, states);
				if (flag2)
				{
					this.Internal_DrawPrefixLabel(position, content, controlID, false);
				}
			}
			else
			{
				Debug.LogError("Style.DrawPrefixLabel may not be called with GUIContent that is null.");
			}
		}

		public void DrawCursor(Rect position, GUIContent content, int controlID, int character)
		{
			Event current = Event.current;
			bool flag = current.type == EventType.Repaint;
			if (flag)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				bool flag2 = cursorFlashSpeed == 0f || num < 0.5f;
				if (flag2)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				this.Internal_DrawCursor(position, content, character, cursorColor);
			}
		}

		internal void DrawWithTextSelection(Rect position, GUIContent content, bool isActive, bool hasKeyboardFocus, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition, Color selectionColor)
		{
			bool flag = Event.current.type != EventType.Repaint;
			if (flag)
			{
				Debug.LogError("Style.Draw may not be called if it is not a repaint event");
			}
			else
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				bool flag2 = cursorFlashSpeed == 0f || num < 0.5f;
				if (flag2)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				bool isHover = position.Contains(Event.current.mousePosition);
				DrawStates states = new DrawStates(-1, isHover, isActive, false, hasKeyboardFocus, drawSelectionAsComposition, firstSelectedCharacter, lastSelectedCharacter, cursorColor, selectionColor);
				bool flag3 = GUIStyle.onDraw == null || !GUIStyle.onDraw(this, position, content, states);
				if (flag3)
				{
					this.Internal_DrawWithTextSelection(position, content, isHover, isActive, false, hasKeyboardFocus, drawSelectionAsComposition, firstSelectedCharacter, lastSelectedCharacter, cursorColor, selectionColor);
				}
			}
		}

		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			this.DrawWithTextSelection(position, content, controlID == GUIUtility.hotControl, controlID == GUIUtility.keyboardControl && GUIStyle.showKeyboardFocus, firstSelectedCharacter, lastSelectedCharacter, drawSelectionAsComposition, GUI.skin.settings.selectionColor);
		}

		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		public static implicit operator GUIStyle(string str)
		{
			bool flag = GUISkin.current == null;
			GUIStyle result;
			if (flag)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				result = GUISkin.error;
			}
			else
			{
				result = GUISkin.current.GetStyle(str);
			}
			return result;
		}

		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			return this.Internal_GetCursorPixelPosition(position, content, cursorStringIndex);
		}

		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return this.Internal_GetCursorStringIndex(position, content, cursorPixelPosition);
		}

		internal int GetNumCharactersThatFitWithinWidth(string text, float width)
		{
			return this.Internal_GetNumCharactersThatFitWithinWidth(text, width);
		}

		public Vector2 CalcSize(GUIContent content)
		{
			return this.Internal_CalcSize(content);
		}

		internal Vector2 CalcSizeWithConstraints(GUIContent content, Vector2 constraints)
		{
			return this.Internal_CalcSizeWithConstraints(content, constraints);
		}

		public Vector2 CalcScreenSize(Vector2 contentSize)
		{
			return new Vector2((this.fixedWidth != 0f) ? this.fixedWidth : Mathf.Ceil(contentSize.x + (float)this.padding.left + (float)this.padding.right), (this.fixedHeight != 0f) ? this.fixedHeight : Mathf.Ceil(contentSize.y + (float)this.padding.top + (float)this.padding.bottom));
		}

		public float CalcHeight(GUIContent content, float width)
		{
			return this.Internal_CalcHeight(content, width);
		}

		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			Vector2 vector = this.Internal_CalcMinMaxWidth(content);
			minWidth = vector.x;
			maxWidth = vector.y;
		}

		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[]
			{
				this.name
			});
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_contentOffset_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_contentOffset_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_clipOffset_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_clipOffset_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_Internal_clipOffset_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_Internal_clipOffset_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Draw_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_Draw2_Injected(ref Rect position, GUIContent content, int controlID, bool on);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DrawCursor_Injected(ref Rect position, GUIContent content, int pos, ref Color cursorColor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DrawWithTextSelection_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, ref Color cursorColor, ref Color selectionColor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetCursorPixelPosition_Injected(ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetCursorStringIndex_Injected(ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string Internal_GetSelectedRenderedText_Injected(ref Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Rect[] Internal_GetHyperlinksRect_Injected(ref Rect localPosition, GUIContent mContent);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CalcSize_Injected(GUIContent content, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CalcSizeWithConstraints_Injected(GUIContent content, ref Vector2 maxSize, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CalcMinMaxWidth_Injected(GUIContent content, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DrawPrefixLabel_Injected(ref Rect position, GUIContent content, int controlID, bool on);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DrawContent_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool hasTextInput, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, ref Color cursorColor, ref Color selectionColor, ref Color imageColor, float textOffsetX, float textOffsetY, float imageTopOffset, float imageLeftOffset, bool overflowX, bool overflowY);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMouseTooltip_Injected(string tooltip, ref Rect screenRect);
	}
}
