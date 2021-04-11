using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Modules/IMGUI/GUI.bindings.h"), NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	public class GUI
	{
		public enum ToolbarButtonSize
		{
			Fixed,
			FitToContents
		}

		public delegate void WindowFunction(int id);

		public abstract class Scope : IDisposable
		{
			private bool m_Disposed;

			internal virtual void Dispose(bool disposing)
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					bool flag = disposing && !GUIUtility.guiIsExiting;
					if (flag)
					{
						this.CloseScope();
					}
					this.m_Disposed = true;
				}
			}

			protected override void Finalize()
			{
				try
				{
					bool flag = !this.m_Disposed && !GUIUtility.guiIsExiting;
					if (flag)
					{
						Console.WriteLine(base.GetType().Name + " was not disposed! You should use the 'using' keyword or manually call Dispose.");
					}
					this.Dispose(false);
				}
				finally
				{
					base.Finalize();
				}
			}

			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected abstract void CloseScope();
		}

		public class GroupScope : GUI.Scope
		{
			public GroupScope(Rect position)
			{
				GUI.BeginGroup(position);
			}

			public GroupScope(Rect position, string text)
			{
				GUI.BeginGroup(position, text);
			}

			public GroupScope(Rect position, Texture image)
			{
				GUI.BeginGroup(position, image);
			}

			public GroupScope(Rect position, GUIContent content)
			{
				GUI.BeginGroup(position, content);
			}

			public GroupScope(Rect position, GUIStyle style)
			{
				GUI.BeginGroup(position, style);
			}

			public GroupScope(Rect position, string text, GUIStyle style)
			{
				GUI.BeginGroup(position, text, style);
			}

			public GroupScope(Rect position, Texture image, GUIStyle style)
			{
				GUI.BeginGroup(position, image, style);
			}

			protected override void CloseScope()
			{
				GUI.EndGroup();
			}
		}

		public class ScrollViewScope : GUI.Scope
		{
			public Vector2 scrollPosition
			{
				get;
				private set;
			}

			public bool handleScrollWheel
			{
				get;
				set;
			}

			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect);
			}

			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical);
			}

			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar);
			}

			public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar);
			}

			internal ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
			{
				this.handleScrollWheel = true;
				this.scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
			}

			protected override void CloseScope()
			{
				GUI.EndScrollView(this.handleScrollWheel);
			}
		}

		public class ClipScope : GUI.Scope
		{
			public ClipScope(Rect position)
			{
				GUI.BeginClip(position);
			}

			internal ClipScope(Rect position, Vector2 scrollOffset)
			{
				GUI.BeginClip(position, scrollOffset, default(Vector2), false);
			}

			protected override void CloseScope()
			{
				GUI.EndClip();
			}
		}

		internal struct ColorScope : IDisposable
		{
			private bool m_Disposed;

			private Color m_PreviousColor;

			public ColorScope(Color newColor)
			{
				this.m_Disposed = false;
				this.m_PreviousColor = GUI.color;
				GUI.color = newColor;
			}

			public ColorScope(float r, float g, float b, float a = 1f)
			{
				this = new GUI.ColorScope(new Color(r, g, b, a));
			}

			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUI.color = this.m_PreviousColor;
				}
			}
		}

		internal struct BackgroundColorScope : IDisposable
		{
			private bool m_Disposed;

			private Color m_PreviousColor;

			public BackgroundColorScope(Color newColor)
			{
				this.m_Disposed = false;
				this.m_PreviousColor = GUI.backgroundColor;
				GUI.backgroundColor = newColor;
			}

			public BackgroundColorScope(float r, float g, float b, float a = 1f)
			{
				this = new GUI.BackgroundColorScope(new Color(r, g, b, a));
			}

			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUI.backgroundColor = this.m_PreviousColor;
				}
			}
		}

		private const float s_ScrollStepSize = 10f;

		private static int s_ScrollControlId;

		private static int s_HotTextField;

		private static readonly int s_BoxHash;

		private static readonly int s_ButonHash;

		private static readonly int s_RepeatButtonHash;

		private static readonly int s_ToggleHash;

		private static readonly int s_ButtonGridHash;

		private static readonly int s_SliderHash;

		private static readonly int s_BeginGroupHash;

		private static readonly int s_ScrollviewHash;

		private static GUISkin s_Skin;

		internal static Rect s_ToolTipRect;

		public static Color color
		{
			get
			{
				Color result;
				GUI.get_color_Injected(out result);
				return result;
			}
			set
			{
				GUI.set_color_Injected(ref value);
			}
		}

		public static Color backgroundColor
		{
			get
			{
				Color result;
				GUI.get_backgroundColor_Injected(out result);
				return result;
			}
			set
			{
				GUI.set_backgroundColor_Injected(ref value);
			}
		}

		public static Color contentColor
		{
			get
			{
				Color result;
				GUI.get_contentColor_Injected(out result);
				return result;
			}
			set
			{
				GUI.set_contentColor_Injected(ref value);
			}
		}

		public static extern bool changed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static extern bool usePageScrollbars
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern Material blendMaterial
		{
			[FreeFunction("GetGUIBlendMaterial")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern Material blitMaterial
		{
			[FreeFunction("GetGUIBlitMaterial")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern Material roundedRectMaterial
		{
			[FreeFunction("GetGUIRoundedRectMaterial")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern Material roundedRectWithColorPerBorderMaterial
		{
			[FreeFunction("GetGUIRoundedRectWithColorPerBorderMaterial")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static int scrollTroughSide
		{
			get;
			set;
		}

		internal static DateTime nextScrollStepTime
		{
			get;
			set;
		}

		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return GUI.s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				GUI.DoSetSkin(value);
			}
		}

		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.GetMatrix();
			}
			set
			{
				GUIClip.SetMatrix(value);
			}
		}

		public static string tooltip
		{
			get
			{
				string text = GUI.Internal_GetTooltip();
				bool flag = text != null;
				string result;
				if (flag)
				{
					result = text;
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				GUI.Internal_SetTooltip(value);
			}
		}

		protected static string mouseTooltip
		{
			get
			{
				return GUI.Internal_GetMouseTooltip();
			}
		}

		protected static Rect tooltipRect
		{
			get
			{
				return GUI.s_ToolTipRect;
			}
			set
			{
				GUI.s_ToolTipRect = value;
			}
		}

		internal static GenericStack scrollViewStates
		{
			get;
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GrabMouseControl(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasMouseControl(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseMouseControl();

		[FreeFunction("GetGUIState().SetNameOfNextControl")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetNextControlName(string name);

		[FreeFunction("GetGUIState().GetNameOfFocusedControl")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetNameOfFocusedControl();

		[FreeFunction("GetGUIState().FocusKeyboardControl")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusControl(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRepaintEditorWindow();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetTooltip();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetTooltip(string value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetMouseTooltip();

		private static Rect Internal_DoModalWindow(int id, int instanceID, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, object skin)
		{
			Rect result;
			GUI.Internal_DoModalWindow_Injected(id, instanceID, ref clientRect, func, content, style, skin, out result);
			return result;
		}

		private static Rect Internal_DoWindow(int id, int instanceID, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, object skin, bool forceRectOnLayout)
		{
			Rect result;
			GUI.Internal_DoWindow_Injected(id, instanceID, ref clientRect, func, title, style, skin, forceRectOnLayout, out result);
			return result;
		}

		public static void DragWindow(Rect position)
		{
			GUI.DragWindow_Injected(ref position);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToFront(int windowID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToBack(int windowID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusWindow(int windowID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnfocusWindow();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BeginWindows();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_EndWindows();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_Concatenate(GUIContent first, GUIContent second);

		static GUI()
		{
			GUI.s_HotTextField = -1;
			GUI.s_BoxHash = "Box".GetHashCode();
			GUI.s_ButonHash = "Button".GetHashCode();
			GUI.s_RepeatButtonHash = "repeatButton".GetHashCode();
			GUI.s_ToggleHash = "Toggle".GetHashCode();
			GUI.s_ButtonGridHash = "ButtonGrid".GetHashCode();
			GUI.s_SliderHash = "Slider".GetHashCode();
			GUI.s_BeginGroupHash = "BeginGroup".GetHashCode();
			GUI.s_ScrollviewHash = "scrollView".GetHashCode();
			GUI.<scrollViewStates>k__BackingField = new GenericStack();
			GUI.nextScrollStepTime = DateTime.Now;
		}

		internal static void DoSetSkin(GUISkin newSkin)
		{
			bool flag = !newSkin;
			if (flag)
			{
				newSkin = GUIUtility.GetDefaultSkin();
			}
			GUI.s_Skin = newSkin;
			newSkin.MakeCurrent();
		}

		internal static void CleanupRoots()
		{
			GUI.s_Skin = null;
			GUIUtility.CleanupRoots();
			GUILayoutUtility.CleanupRoots();
			GUISkin.CleanupRoots();
			GUIStyle.CleanupRoots();
		}

		public static void Label(Rect position, string text)
		{
			GUI.Label(position, GUIContent.Temp(text), GUI.s_Skin.label);
		}

		public static void Label(Rect position, Texture image)
		{
			GUI.Label(position, GUIContent.Temp(image), GUI.s_Skin.label);
		}

		public static void Label(Rect position, GUIContent content)
		{
			GUI.Label(position, content, GUI.s_Skin.label);
		}

		public static void Label(Rect position, string text, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(text), style);
		}

		public static void Label(Rect position, Texture image, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(image), style);
		}

		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUI.DoLabel(position, content, style);
		}

		public static void DrawTexture(Rect position, Texture image)
		{
			GUI.DrawTexture(position, image, ScaleMode.StretchToFill);
		}

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
		{
			GUI.DrawTexture(position, image, scaleMode, true);
		}

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, 0f);
		}

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, GUI.color, 0f, 0f);
		}

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, float borderWidth, float borderRadius)
		{
			Vector4 borderWidths = Vector4.one * borderWidth;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadius);
		}

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, float borderRadius)
		{
			Vector4 borderRadiuses = Vector4.one * borderRadius;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadiuses);
		}

		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, Vector4 borderRadiuses)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadiuses, true);
		}

		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, Vector4 borderRadiuses, bool drawSmoothCorners)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, color, color, color, borderWidths, borderRadiuses, drawSmoothCorners);
		}

		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color leftColor, Color topColor, Color rightColor, Color bottomColor, Vector4 borderWidths, Vector4 borderRadiuses)
		{
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, leftColor, topColor, rightColor, bottomColor, borderWidths, borderRadiuses, true);
		}

		internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color leftColor, Color topColor, Color rightColor, Color bottomColor, Vector4 borderWidths, Vector4 borderRadiuses, bool drawSmoothCorners)
		{
			GUIUtility.CheckOnGUI();
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				bool flag2 = image == null;
				if (flag2)
				{
					Debug.LogWarning("null texture passed to GUI.DrawTexture");
				}
				else
				{
					bool flag3 = imageAspect == 0f;
					if (flag3)
					{
						imageAspect = (float)image.width / (float)image.height;
					}
					bool flag4 = borderWidths != Vector4.zero;
					Material mat;
					if (flag4)
					{
						bool flag5 = leftColor != topColor || leftColor != rightColor || leftColor != bottomColor;
						if (flag5)
						{
							mat = GUI.roundedRectWithColorPerBorderMaterial;
						}
						else
						{
							mat = GUI.roundedRectMaterial;
						}
					}
					else
					{
						bool flag6 = borderRadiuses != Vector4.zero;
						if (flag6)
						{
							mat = GUI.roundedRectMaterial;
						}
						else
						{
							mat = (alphaBlend ? GUI.blendMaterial : GUI.blitMaterial);
						}
					}
					Internal_DrawTextureArguments internal_DrawTextureArguments = new Internal_DrawTextureArguments
					{
						leftBorder = 0,
						rightBorder = 0,
						topBorder = 0,
						bottomBorder = 0,
						color = leftColor,
						leftBorderColor = leftColor,
						topBorderColor = topColor,
						rightBorderColor = rightColor,
						bottomBorderColor = bottomColor,
						borderWidths = borderWidths,
						cornerRadiuses = borderRadiuses,
						texture = image,
						smoothCorners = drawSmoothCorners,
						mat = mat
					};
					GUI.CalculateScaledTextureRects(position, scaleMode, imageAspect, ref internal_DrawTextureArguments.screenRect, ref internal_DrawTextureArguments.sourceRect);
					Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
				}
			}
		}

		internal static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
		{
			float num = position.width / position.height;
			bool result = false;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				outScreenRect = position;
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				result = true;
				break;
			case ScaleMode.ScaleAndCrop:
			{
				bool flag = num > imageAspect;
				if (flag)
				{
					float num2 = imageAspect / num;
					outScreenRect = position;
					outSourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
					result = true;
				}
				else
				{
					float num3 = num / imageAspect;
					outScreenRect = position;
					outSourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
					result = true;
				}
				break;
			}
			case ScaleMode.ScaleToFit:
			{
				bool flag2 = num > imageAspect;
				if (flag2)
				{
					float num4 = imageAspect / num;
					outScreenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					result = true;
				}
				else
				{
					float num5 = num / imageAspect;
					outScreenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					result = true;
				}
				break;
			}
			}
			return result;
		}

		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)
		{
			GUI.DrawTextureWithTexCoords(position, image, texCoords, true);
		}

		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, bool alphaBlend)
		{
			GUIUtility.CheckOnGUI();
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				Material mat = alphaBlend ? GUI.blendMaterial : GUI.blitMaterial;
				Internal_DrawTextureArguments internal_DrawTextureArguments = default(Internal_DrawTextureArguments);
				internal_DrawTextureArguments.texture = image;
				internal_DrawTextureArguments.mat = mat;
				internal_DrawTextureArguments.leftBorder = 0;
				internal_DrawTextureArguments.rightBorder = 0;
				internal_DrawTextureArguments.topBorder = 0;
				internal_DrawTextureArguments.bottomBorder = 0;
				internal_DrawTextureArguments.color = GUI.color;
				internal_DrawTextureArguments.leftBorderColor = GUI.color;
				internal_DrawTextureArguments.topBorderColor = GUI.color;
				internal_DrawTextureArguments.rightBorderColor = GUI.color;
				internal_DrawTextureArguments.bottomBorderColor = GUI.color;
				internal_DrawTextureArguments.screenRect = position;
				internal_DrawTextureArguments.sourceRect = texCoords;
				Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
			}
		}

		public static void Box(Rect position, string text)
		{
			GUI.Box(position, GUIContent.Temp(text), GUI.s_Skin.box);
		}

		public static void Box(Rect position, Texture image)
		{
			GUI.Box(position, GUIContent.Temp(image), GUI.s_Skin.box);
		}

		public static void Box(Rect position, GUIContent content)
		{
			GUI.Box(position, content, GUI.s_Skin.box);
		}

		public static void Box(Rect position, string text, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(text), style);
		}

		public static void Box(Rect position, Texture image, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(image), style);
		}

		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_BoxHash, FocusType.Passive);
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				style.Draw(position, content, controlID, false, position.Contains(Event.current.mousePosition));
			}
		}

		public static bool Button(Rect position, string text)
		{
			return GUI.Button(position, GUIContent.Temp(text), GUI.s_Skin.button);
		}

		public static bool Button(Rect position, Texture image)
		{
			return GUI.Button(position, GUIContent.Temp(image), GUI.s_Skin.button);
		}

		public static bool Button(Rect position, GUIContent content)
		{
			return GUI.Button(position, content, GUI.s_Skin.button);
		}

		public static bool Button(Rect position, string text, GUIStyle style)
		{
			return GUI.Button(position, GUIContent.Temp(text), style);
		}

		public static bool Button(Rect position, Texture image, GUIStyle style)
		{
			return GUI.Button(position, GUIContent.Temp(image), style);
		}

		public static bool Button(Rect position, GUIContent content, GUIStyle style)
		{
			int controlID = GUIUtility.GetControlID(GUI.s_ButonHash, FocusType.Passive, position);
			return GUI.Button(position, controlID, content, style);
		}

		internal static bool Button(Rect position, int id, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoButton(position, id, content, style);
		}

		public static bool RepeatButton(Rect position, string text)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), GUI.s_Skin.button, FocusType.Passive);
		}

		public static bool RepeatButton(Rect position, Texture image)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), GUI.s_Skin.button, FocusType.Passive);
		}

		public static bool RepeatButton(Rect position, GUIContent content)
		{
			return GUI.DoRepeatButton(position, content, GUI.s_Skin.button, FocusType.Passive);
		}

		public static bool RepeatButton(Rect position, string text, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Passive);
		}

		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Passive);
		}

		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, content, style, FocusType.Passive);
		}

		private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_RepeatButtonHash, focusType, position);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			EventType eventType = typeForControl;
			bool result;
			if (eventType != EventType.MouseDown)
			{
				if (eventType != EventType.MouseUp)
				{
					if (eventType != EventType.Repaint)
					{
						result = false;
					}
					else
					{
						style.Draw(position, content, controlID, false, position.Contains(Event.current.mousePosition));
						result = (controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition));
					}
				}
				else
				{
					bool flag = GUIUtility.hotControl == controlID;
					if (flag)
					{
						GUIUtility.hotControl = 0;
						Event.current.Use();
						result = position.Contains(Event.current.mousePosition);
					}
					else
					{
						result = false;
					}
				}
			}
			else
			{
				bool flag2 = position.Contains(Event.current.mousePosition);
				if (flag2)
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				result = false;
			}
			return result;
		}

		public static string TextField(Rect position, string text)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, -1, GUI.skin.textField);
			return gUIContent.text;
		}

		public static string TextField(Rect position, string text, int maxLength)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, maxLength, GUI.skin.textField);
			return gUIContent.text;
		}

		public static string TextField(Rect position, string text, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, -1, style);
			return gUIContent.text;
		}

		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, maxLength, style);
			return gUIContent.text;
		}

		public static string PasswordField(Rect position, string password, char maskChar)
		{
			return GUI.PasswordField(position, password, maskChar, -1, GUI.skin.textField);
		}

		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
		{
			return GUI.PasswordField(position, password, maskChar, maxLength, GUI.skin.textField);
		}

		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
		{
			return GUI.PasswordField(position, password, maskChar, -1, style);
		}

		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			string text = GUI.PasswordFieldGetStrToShow(password, maskChar);
			GUIContent gUIContent = GUIContent.Temp(text);
			bool changed = GUI.changed;
			GUI.changed = false;
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard), gUIContent, false, maxLength, style, password, maskChar);
			}
			else
			{
				GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, false, maxLength, style);
			}
			text = (GUI.changed ? gUIContent.text : password);
			GUI.changed |= changed;
			return text;
		}

		internal static string PasswordFieldGetStrToShow(string password, char maskChar)
		{
			return (Event.current.type == EventType.Repaint || Event.current.type == EventType.MouseDown) ? "".PadRight(password.Length, maskChar) : password;
		}

		public static string TextArea(Rect position, string text)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, true, -1, GUI.skin.textArea);
			return gUIContent.text;
		}

		public static string TextArea(Rect position, string text, int maxLength)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, true, maxLength, GUI.skin.textArea);
			return gUIContent.text;
		}

		public static string TextArea(Rect position, string text, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, true, -1, style);
			return gUIContent.text;
		}

		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent gUIContent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, true, maxLength, style);
			return gUIContent.text;
		}

		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
		{
			GUI.DoTextField(position, id, content, multiline, maxLength, style, null);
		}

		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText)
		{
			GUI.DoTextField(position, id, content, multiline, maxLength, style, secureText, '\0');
		}

		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar)
		{
			GUIUtility.CheckOnGUI();
			bool flag = maxLength >= 0 && content.text.Length > maxLength;
			if (flag)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
			textEditor.text = content.text;
			textEditor.SaveBackup();
			textEditor.position = position;
			textEditor.style = style;
			textEditor.multiline = multiline;
			textEditor.controlID = id;
			textEditor.DetectFocusChange();
			bool flag2 = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag2)
			{
				GUI.HandleTextFieldEventForTouchscreen(position, id, content, multiline, maxLength, style, secureText, maskChar, textEditor);
			}
			else
			{
				GUI.HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, textEditor);
			}
			textEditor.UpdateScrollOffsetIfNeeded(Event.current);
		}

		private static void HandleTextFieldEventForTouchscreen(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar, TextEditor editor)
		{
			Event current = Event.current;
			EventType type = current.type;
			EventType eventType = type;
			if (eventType != EventType.MouseDown)
			{
				if (eventType == EventType.Repaint)
				{
					bool flag = editor.keyboardOnScreen != null;
					if (flag)
					{
						content.text = editor.keyboardOnScreen.text;
						bool flag2 = maxLength >= 0 && content.text.Length > maxLength;
						if (flag2)
						{
							content.text = content.text.Substring(0, maxLength);
						}
						bool flag3 = editor.keyboardOnScreen.status > TouchScreenKeyboard.Status.Visible;
						if (flag3)
						{
							editor.keyboardOnScreen = null;
							GUI.changed = true;
						}
					}
					string text = content.text;
					bool flag4 = secureText != null;
					if (flag4)
					{
						content.text = GUI.PasswordFieldGetStrToShow(text, maskChar);
					}
					style.Draw(position, content, id, false);
					content.text = text;
				}
			}
			else
			{
				bool flag5 = position.Contains(current.mousePosition);
				if (flag5)
				{
					GUIUtility.hotControl = id;
					bool flag6 = GUI.s_HotTextField != -1 && GUI.s_HotTextField != id;
					if (flag6)
					{
						TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUI.s_HotTextField);
						textEditor.keyboardOnScreen = null;
					}
					GUI.s_HotTextField = id;
					bool flag7 = GUIUtility.keyboardControl != id;
					if (flag7)
					{
						GUIUtility.keyboardControl = id;
					}
					editor.keyboardOnScreen = TouchScreenKeyboard.Open(secureText ?? content.text, TouchScreenKeyboardType.Default, true, multiline, secureText != null);
					current.Use();
				}
			}
		}

		private static void HandleTextFieldEventForDesktop(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, TextEditor editor)
		{
			Event current = Event.current;
			bool flag = false;
			switch (current.type)
			{
			case EventType.MouseDown:
			{
				bool flag2 = position.Contains(current.mousePosition);
				if (flag2)
				{
					GUIUtility.hotControl = id;
					GUIUtility.keyboardControl = id;
					editor.m_HasFocus = true;
					editor.MoveCursorToPosition(Event.current.mousePosition);
					bool flag3 = Event.current.clickCount == 2 && GUI.skin.settings.doubleClickSelectsWord;
					if (flag3)
					{
						editor.SelectCurrentWord();
						editor.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
						editor.MouseDragSelectsWholeWords(true);
					}
					bool flag4 = Event.current.clickCount == 3 && GUI.skin.settings.tripleClickSelectsLine;
					if (flag4)
					{
						editor.SelectCurrentParagraph();
						editor.MouseDragSelectsWholeWords(true);
						editor.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
					}
					current.Use();
				}
				break;
			}
			case EventType.MouseUp:
			{
				bool flag5 = GUIUtility.hotControl == id;
				if (flag5)
				{
					editor.MouseDragSelectsWholeWords(false);
					GUIUtility.hotControl = 0;
					current.Use();
				}
				break;
			}
			case EventType.MouseDrag:
			{
				bool flag6 = GUIUtility.hotControl == id;
				if (flag6)
				{
					bool shift = current.shift;
					if (shift)
					{
						editor.MoveCursorToPosition(Event.current.mousePosition);
					}
					else
					{
						editor.SelectToPosition(Event.current.mousePosition);
					}
					current.Use();
				}
				break;
			}
			case EventType.KeyDown:
			{
				bool flag7 = GUIUtility.keyboardControl != id;
				if (flag7)
				{
					return;
				}
				bool flag8 = editor.HandleKeyEvent(current);
				if (flag8)
				{
					current.Use();
					flag = true;
					content.text = editor.text;
				}
				else
				{
					bool flag9 = current.keyCode == KeyCode.Tab || current.character == '\t';
					if (flag9)
					{
						return;
					}
					char character = current.character;
					bool flag10 = character == '\n' && !multiline && !current.alt;
					if (flag10)
					{
						return;
					}
					Font font = style.font;
					bool flag11 = !font;
					if (flag11)
					{
						font = GUI.skin.font;
					}
					bool flag12 = font.HasCharacter(character) || character == '\n';
					if (flag12)
					{
						editor.Insert(character);
						flag = true;
					}
					else
					{
						bool flag13 = character == '\0';
						if (flag13)
						{
							bool flag14 = GUIUtility.compositionString.Length > 0;
							if (flag14)
							{
								editor.ReplaceSelection("");
								flag = true;
							}
							current.Use();
						}
					}
				}
				break;
			}
			case EventType.Repaint:
			{
				bool flag15 = GUIUtility.keyboardControl != id;
				if (flag15)
				{
					style.Draw(position, content, id, false);
				}
				else
				{
					editor.DrawCursor(content.text);
				}
				break;
			}
			}
			bool flag16 = GUIUtility.keyboardControl == id;
			if (flag16)
			{
				GUIUtility.textFieldInput = true;
			}
			bool flag17 = flag;
			if (flag17)
			{
				GUI.changed = true;
				content.text = editor.text;
				bool flag18 = maxLength >= 0 && content.text.Length > maxLength;
				if (flag18)
				{
					content.text = content.text.Substring(0, maxLength);
				}
				current.Use();
			}
		}

		public static bool Toggle(Rect position, bool value, string text)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), GUI.s_Skin.toggle);
		}

		public static bool Toggle(Rect position, bool value, Texture image)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), GUI.s_Skin.toggle);
		}

		public static bool Toggle(Rect position, bool value, GUIContent content)
		{
			return GUI.Toggle(position, value, content, GUI.s_Skin.toggle);
		}

		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), style);
		}

		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), style);
		}

		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoToggle(position, GUIUtility.GetControlID(GUI.s_ToggleHash, FocusType.Passive, position), value, content, style);
		}

		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoToggle(position, id, value, content, style);
		}

		public static int Toolbar(Rect position, int selected, string[] texts)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), GUI.s_Skin.button);
		}

		public static int Toolbar(Rect position, int selected, Texture[] images)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), GUI.s_Skin.button);
		}

		public static int Toolbar(Rect position, int selected, GUIContent[] contents)
		{
			return GUI.Toolbar(position, selected, contents, GUI.s_Skin.button);
		}

		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), style);
		}

		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), style);
		}

		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, contents, null, style, GUI.ToolbarButtonSize.Fixed, null);
		}

		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style, GUI.ToolbarButtonSize buttonSize)
		{
			return GUI.Toolbar(position, selected, contents, null, style, buttonSize, null);
		}

		internal static int Toolbar(Rect position, int selected, GUIContent[] contents, string[] controlNames, GUIStyle style, GUI.ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
		{
			GUIUtility.CheckOnGUI();
			GUIStyle firstStyle;
			GUIStyle midStyle;
			GUIStyle lastStyle;
			GUI.FindStyles(ref style, out firstStyle, out midStyle, out lastStyle, "left", "mid", "right");
			return GUI.DoButtonGrid(position, selected, contents, controlNames, contents.Length, style, firstStyle, midStyle, lastStyle, buttonSize, contentsEnabled);
		}

		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
		}

		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
		}

		public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
		{
			return GUI.SelectionGrid(position, selected, content, xCount, null);
		}

		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
		}

		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
		}

		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
		{
			bool flag = style == null;
			if (flag)
			{
				style = GUI.s_Skin.button;
			}
			return GUI.DoButtonGrid(position, selected, contents, null, xCount, style, style, style, style, GUI.ToolbarButtonSize.Fixed, null);
		}

		internal static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
		{
			bool flag = style == null;
			if (flag)
			{
				style = GUI.skin.button;
			}
			string name = style.name;
			midStyle = (GUI.skin.FindStyle(name + mid) ?? style);
			firstStyle = (GUI.skin.FindStyle(name + first) ?? midStyle);
			lastStyle = (GUI.skin.FindStyle(name + last) ?? midStyle);
		}

		internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			bool flag = xCount < 2;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = xCount == 2;
				if (flag2)
				{
					result = Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
				}
				else
				{
					int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
					result = Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
				}
			}
			return result;
		}

		internal static bool DoControl(Rect position, int id, bool on, bool hover, GUIContent content, GUIStyle style)
		{
			Event current = Event.current;
			bool result;
			switch (current.type)
			{
			case EventType.MouseDown:
			{
				bool flag = GUIUtility.HitTest(position, current);
				if (flag)
				{
					GUI.GrabMouseControl(id);
					current.Use();
				}
				break;
			}
			case EventType.MouseUp:
			{
				bool flag2 = GUI.HasMouseControl(id);
				if (flag2)
				{
					GUI.ReleaseMouseControl();
					current.Use();
					bool flag3 = GUIUtility.HitTest(position, current);
					if (flag3)
					{
						GUI.changed = true;
						result = !on;
						return result;
					}
				}
				break;
			}
			case EventType.MouseDrag:
			{
				bool flag4 = GUI.HasMouseControl(id);
				if (flag4)
				{
					current.Use();
				}
				break;
			}
			case EventType.KeyDown:
			{
				bool flag5 = current.alt || current.shift || current.command || current.control;
				bool flag6 = (current.keyCode == KeyCode.Space || current.keyCode == KeyCode.Return || current.keyCode == KeyCode.KeypadEnter) && !flag5 && GUIUtility.keyboardControl == id;
				if (flag6)
				{
					current.Use();
					GUI.changed = true;
					result = !on;
					return result;
				}
				break;
			}
			case EventType.Repaint:
				style.Draw(position, content, id, on, hover);
				break;
			}
			result = on;
			return result;
		}

		private static void DoLabel(Rect position, GUIContent content, GUIStyle style)
		{
			Event current = Event.current;
			bool flag = current.type != EventType.Repaint;
			if (!flag)
			{
				style.Draw(position, content, false, false, false, false);
				bool flag2 = !string.IsNullOrEmpty(content.tooltip) && position.Contains(current.mousePosition) && GUIClip.visibleRect.Contains(current.mousePosition);
				if (flag2)
				{
					GUIStyle.SetMouseTooltip(content.tooltip, position);
				}
			}
		}

		internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoControl(position, id, value, position.Contains(Event.current.mousePosition), content, style);
		}

		internal static bool DoButton(Rect position, int id, GUIContent content, GUIStyle style)
		{
			return GUI.DoControl(position, id, false, position.Contains(Event.current.mousePosition), content, style);
		}

		private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, string[] controlNames, int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, GUI.ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
		{
			GUIUtility.CheckOnGUI();
			int num = contents.Length;
			bool flag = num == 0;
			int result;
			if (flag)
			{
				result = selected;
			}
			else
			{
				bool flag2 = xCount <= 0;
				if (flag2)
				{
					Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set xCount to a positive value.");
					result = selected;
				}
				else
				{
					bool flag3 = contentsEnabled != null && contentsEnabled.Length != num;
					if (flag3)
					{
						throw new ArgumentException("contentsEnabled");
					}
					int num2 = num / xCount;
					bool flag4 = num % xCount != 0;
					if (flag4)
					{
						num2++;
					}
					float num3 = (float)GUI.CalcTotalHorizSpacing(xCount, style, firstStyle, midStyle, lastStyle);
					float num4 = (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1));
					float elemWidth = (position.width - num3) / (float)xCount;
					float elemHeight = (position.height - num4) / (float)num2;
					bool flag5 = style.fixedWidth != 0f;
					if (flag5)
					{
						elemWidth = style.fixedWidth;
					}
					bool flag6 = style.fixedHeight != 0f;
					if (flag6)
					{
						elemHeight = style.fixedHeight;
					}
					Rect[] array = GUI.CalcMouseRects(position, contents, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false, buttonSize);
					GUIStyle gUIStyle = null;
					int num5 = 0;
					for (int i = 0; i < num; i++)
					{
						bool enabled = GUI.enabled;
						GUI.enabled &= (contentsEnabled == null || contentsEnabled[i]);
						Rect rect = array[i];
						GUIContent gUIContent = contents[i];
						bool flag7 = controlNames != null;
						if (flag7)
						{
							GUI.SetNextControlName(controlNames[i]);
						}
						int controlID = GUIUtility.GetControlID(GUI.s_ButtonGridHash, FocusType.Passive, rect);
						bool flag8 = i == selected;
						if (flag8)
						{
							num5 = controlID;
						}
						EventType typeForControl = Event.current.GetTypeForControl(controlID);
						EventType eventType = typeForControl;
						switch (eventType)
						{
						case EventType.MouseDown:
						{
							bool flag9 = GUIUtility.HitTest(rect, Event.current);
							if (flag9)
							{
								GUIUtility.hotControl = controlID;
								Event.current.Use();
							}
							break;
						}
						case EventType.MouseUp:
						{
							bool flag10 = GUIUtility.hotControl == controlID;
							if (flag10)
							{
								GUIUtility.hotControl = 0;
								Event.current.Use();
								GUI.changed = true;
								result = i;
								return result;
							}
							break;
						}
						case EventType.MouseMove:
							break;
						case EventType.MouseDrag:
						{
							bool flag11 = GUIUtility.hotControl == controlID;
							if (flag11)
							{
								Event.current.Use();
							}
							break;
						}
						default:
							if (eventType == EventType.Repaint)
							{
								GUIStyle gUIStyle2 = (num == 1) ? style : ((i == 0) ? firstStyle : ((i == num - 1) ? lastStyle : midStyle));
								bool flag12 = rect.Contains(Event.current.mousePosition);
								bool flag13 = GUIUtility.hotControl == controlID;
								bool flag14 = selected == i;
								bool flag15 = !flag14;
								if (flag15)
								{
									gUIStyle2.Draw(rect, gUIContent, flag12 && (GUI.enabled | flag13) && (flag13 || GUIUtility.hotControl == 0), GUI.enabled & flag13, false, false);
								}
								else
								{
									gUIStyle = gUIStyle2;
								}
								bool flag16 = flag12;
								if (flag16)
								{
									GUIUtility.mouseUsed = true;
									bool flag17 = !string.IsNullOrEmpty(gUIContent.tooltip);
									if (flag17)
									{
										GUIStyle.SetMouseTooltip(gUIContent.tooltip, rect);
									}
								}
							}
							break;
						}
						GUI.enabled = enabled;
					}
					bool flag18 = gUIStyle != null;
					if (flag18)
					{
						Rect position2 = array[selected];
						GUIContent content = contents[selected];
						bool flag19 = position2.Contains(Event.current.mousePosition);
						bool flag20 = GUIUtility.hotControl == num5;
						bool enabled2 = GUI.enabled;
						GUI.enabled &= (contentsEnabled == null || contentsEnabled[selected]);
						gUIStyle.Draw(position2, content, flag19 && (GUI.enabled | flag20) && (flag20 || GUIUtility.hotControl == 0), GUI.enabled & flag20, true, false);
						GUI.enabled = enabled2;
					}
					result = selected;
				}
			}
			return result;
		}

		private static Rect[] CalcMouseRects(Rect position, GUIContent[] contents, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, bool addBorders, GUI.ToolbarButtonSize buttonSize)
		{
			int num = contents.Length;
			int num2 = 0;
			float x = position.xMin;
			float num3 = position.yMin;
			GUIStyle gUIStyle = style;
			Rect[] array = new Rect[num];
			bool flag = num > 1;
			if (flag)
			{
				gUIStyle = firstStyle;
			}
			for (int i = 0; i < num; i++)
			{
				float width = 0f;
				if (buttonSize != GUI.ToolbarButtonSize.Fixed)
				{
					if (buttonSize == GUI.ToolbarButtonSize.FitToContents)
					{
						width = gUIStyle.CalcSize(contents[i]).x;
					}
				}
				else
				{
					width = elemWidth;
				}
				bool flag2 = !addBorders;
				if (flag2)
				{
					array[i] = new Rect(x, num3, width, elemHeight);
				}
				else
				{
					array[i] = gUIStyle.margin.Add(new Rect(x, num3, width, elemHeight));
				}
				array[i] = GUIUtility.AlignRectToDevice(array[i]);
				GUIStyle gUIStyle2 = midStyle;
				bool flag3 = i == num - 2 || i == xCount - 2;
				if (flag3)
				{
					gUIStyle2 = lastStyle;
				}
				x = array[i].xMax + (float)Mathf.Max(gUIStyle.margin.right, gUIStyle2.margin.left);
				num2++;
				bool flag4 = num2 >= xCount;
				if (flag4)
				{
					num2 = 0;
					num3 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
					x = position.xMin;
					gUIStyle2 = firstStyle;
				}
				gUIStyle = gUIStyle2;
			}
			return array;
		}

		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, true, 0, GUI.skin.horizontalSliderThumbExtent);
		}

		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, 0, null);
		}

		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle thumbExtent)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, 0, (thumbExtent == null && thumb == GUI.skin.horizontalSliderThumb) ? GUI.skin.horizontalSliderThumbExtent : thumbExtent);
		}

		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, false, 0, GUI.skin.verticalSliderThumbExtent);
		}

		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, 0, null);
		}

		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb, GUIStyle thumbExtent)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, 0, (thumbExtent == null && thumb == GUI.skin.verticalSliderThumb) ? GUI.skin.verticalSliderThumbExtent : thumbExtent);
		}

		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
		{
			GUIUtility.CheckOnGUI();
			bool flag = id == 0;
			if (flag)
			{
				id = GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive, position);
			}
			return new SliderHandler(position, value, size, start, end, slider, thumb, horiz, id, thumbExtent).Handle();
		}

		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, GUI.skin.horizontalScrollbarThumb, GUI.skin.horizontalScrollbarLeftButton, GUI.skin.horizontalScrollbarRightButton, true);
		}

		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "leftbutton"), GUI.skin.GetStyle(style.name + "rightbutton"), true);
		}

		internal static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
		{
			bool result = false;
			bool flag = GUI.DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive);
			if (flag)
			{
				bool flag2 = GUI.s_ScrollControlId != scrollerID;
				GUI.s_ScrollControlId = scrollerID;
				bool flag3 = flag2;
				if (flag3)
				{
					result = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(250.0);
				}
				else
				{
					bool flag4 = DateTime.Now >= GUI.nextScrollStepTime;
					if (flag4)
					{
						result = true;
						GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(30.0);
					}
				}
				bool flag5 = Event.current.type == EventType.Repaint;
				if (flag5)
				{
					GUI.InternalRepaintEditorWindow();
				}
			}
			return result;
		}

		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, GUI.skin.verticalScrollbarThumb, GUI.skin.verticalScrollbarUpButton, GUI.skin.verticalScrollbarDownButton, false);
		}

		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "upbutton"), GUI.skin.GetStyle(style.name + "downbutton"), false);
		}

		internal static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive, position);
			Rect position2;
			Rect rect;
			Rect rect2;
			if (horiz)
			{
				position2 = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
				rect = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
				rect2 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
			}
			else
			{
				position2 = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
				rect = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
				rect2 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
			}
			value = GUI.Slider(position2, value, size, leftValue, rightValue, slider, thumb, horiz, controlID, null);
			bool flag = Event.current.type == EventType.MouseUp;
			bool flag2 = GUI.ScrollerRepeatButton(controlID, rect, leftButton);
			if (flag2)
			{
				value -= 10f * ((leftValue < rightValue) ? 1f : -1f);
			}
			bool flag3 = GUI.ScrollerRepeatButton(controlID, rect2, rightButton);
			if (flag3)
			{
				value += 10f * ((leftValue < rightValue) ? 1f : -1f);
			}
			bool flag4 = flag && Event.current.type == EventType.Used;
			if (flag4)
			{
				GUI.s_ScrollControlId = 0;
			}
			bool flag5 = leftValue < rightValue;
			if (flag5)
			{
				value = Mathf.Clamp(value, leftValue, rightValue - size);
			}
			else
			{
				value = Mathf.Clamp(value, rightValue, leftValue - size);
			}
			return value;
		}

		public static void BeginClip(Rect position, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Push(position, scrollOffset, renderOffset, resetOffset);
		}

		public static void BeginGroup(Rect position)
		{
			GUI.BeginGroup(position, GUIContent.none, GUIStyle.none);
		}

		public static void BeginGroup(Rect position, string text)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
		}

		public static void BeginGroup(Rect position, Texture image)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
		}

		public static void BeginGroup(Rect position, GUIContent content)
		{
			GUI.BeginGroup(position, content, GUIStyle.none);
		}

		public static void BeginGroup(Rect position, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.none, style);
		}

		public static void BeginGroup(Rect position, string text, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), style);
		}

		public static void BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), style);
		}

		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			GUI.BeginGroup(position, content, style, Vector2.zero);
		}

		internal static void BeginGroup(Rect position, GUIContent content, GUIStyle style, Vector2 scrollOffset)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.s_BeginGroupHash, FocusType.Passive);
			bool flag = content != GUIContent.none || style != GUIStyle.none;
			if (flag)
			{
				EventType type = Event.current.type;
				EventType eventType = type;
				if (eventType != EventType.Repaint)
				{
					bool flag2 = position.Contains(Event.current.mousePosition);
					if (flag2)
					{
						GUIUtility.mouseUsed = true;
					}
				}
				else
				{
					style.Draw(position, content, controlID);
				}
			}
			GUIClip.Push(position, scrollOffset, Vector2.zero, false);
		}

		public static void EndGroup()
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Internal_Pop();
		}

		public static void BeginClip(Rect position)
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
		}

		public static void EndClip()
		{
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
		}

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			GUIUtility.CheckOnGUI();
			bool flag = Event.current.type == EventType.DragUpdated && position.Contains(Event.current.mousePosition);
			if (flag)
			{
				bool flag2 = Mathf.Abs(Event.current.mousePosition.y - position.y) < 8f;
				if (flag2)
				{
					scrollPosition.y -= 16f;
					GUI.InternalRepaintEditorWindow();
				}
				else
				{
					bool flag3 = Mathf.Abs(Event.current.mousePosition.y - position.yMax) < 8f;
					if (flag3)
					{
						scrollPosition.y += 16f;
						GUI.InternalRepaintEditorWindow();
					}
				}
			}
			int controlID = GUIUtility.GetControlID(GUI.s_ScrollviewHash, FocusType.Passive);
			ScrollViewState scrollViewState = (ScrollViewState)GUIUtility.GetStateObject(typeof(ScrollViewState), controlID);
			bool apply = scrollViewState.apply;
			if (apply)
			{
				scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.apply = false;
			}
			scrollViewState.position = position;
			scrollViewState.scrollPosition = scrollPosition;
			scrollViewState.visibleRect = (scrollViewState.viewRect = viewRect);
			scrollViewState.visibleRect.width = position.width;
			scrollViewState.visibleRect.height = position.height;
			GUI.scrollViewStates.Push(scrollViewState);
			Rect screenRect = new Rect(position);
			EventType type = Event.current.type;
			EventType eventType = type;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					bool flag4 = alwaysShowVertical;
					bool flag5 = alwaysShowHorizontal;
					bool flag6 = flag5 || viewRect.width > screenRect.width;
					if (flag6)
					{
						scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						flag5 = true;
					}
					bool flag7 = flag4 || viewRect.height > screenRect.height;
					if (flag7)
					{
						scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						screenRect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						flag4 = true;
						bool flag8 = !flag5 && viewRect.width > screenRect.width;
						if (flag8)
						{
							scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							flag5 = true;
						}
					}
					bool flag9 = Event.current.type == EventType.Repaint && background != GUIStyle.none;
					if (flag9)
					{
						background.Draw(position, position.Contains(Event.current.mousePosition), false, flag5 & flag4, false);
					}
					bool flag10 = flag5 && horizontalScrollbar != GUIStyle.none;
					if (flag10)
					{
						scrollPosition.x = GUI.HorizontalScrollbar(new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, screenRect.width, horizontalScrollbar.fixedHeight), scrollPosition.x, Mathf.Min(screenRect.width, viewRect.width), 0f, viewRect.width, horizontalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						scrollPosition.x = ((horizontalScrollbar != GUIStyle.none) ? 0f : Mathf.Clamp(scrollPosition.x, 0f, Mathf.Max(viewRect.width - position.width, 0f)));
					}
					bool flag11 = flag4 && verticalScrollbar != GUIStyle.none;
					if (flag11)
					{
						scrollPosition.y = GUI.VerticalScrollbar(new Rect(screenRect.xMax + (float)verticalScrollbar.margin.left, screenRect.y, verticalScrollbar.fixedWidth, screenRect.height), scrollPosition.y, Mathf.Min(screenRect.height, viewRect.height), 0f, viewRect.height, verticalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
						scrollPosition.y = ((verticalScrollbar != GUIStyle.none) ? 0f : Mathf.Clamp(scrollPosition.y, 0f, Mathf.Max(viewRect.height - position.height, 0f)));
					}
				}
			}
			else
			{
				GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.s_RepeatButtonHash, FocusType.Passive);
			}
			GUIClip.Push(screenRect, new Vector2(Mathf.Round(-scrollPosition.x - viewRect.x), Mathf.Round(-scrollPosition.y - viewRect.y)), Vector2.zero, false);
			return scrollPosition;
		}

		public static void EndScrollView()
		{
			GUI.EndScrollView(true);
		}

		public static void EndScrollView(bool handleScrollWheel)
		{
			GUIUtility.CheckOnGUI();
			bool flag = GUI.scrollViewStates.Count == 0;
			if (!flag)
			{
				ScrollViewState scrollViewState = (ScrollViewState)GUI.scrollViewStates.Peek();
				GUIClip.Pop();
				GUI.scrollViewStates.Pop();
				bool flag2 = false;
				float num = Time.realtimeSinceStartup - scrollViewState.previousTimeSinceStartup;
				scrollViewState.previousTimeSinceStartup = Time.realtimeSinceStartup;
				bool flag3 = Event.current.type == EventType.Repaint && scrollViewState.velocity != Vector2.zero;
				if (flag3)
				{
					for (int i = 0; i < 2; i++)
					{
						ScrollViewState var_7_91_cp_0 = scrollViewState;
						int index = i;
						var_7_91_cp_0.velocity[index] = var_7_91_cp_0.velocity[index] * Mathf.Pow(0.1f, num);
						float num2 = 0.1f / num;
						bool flag4 = Mathf.Abs(scrollViewState.velocity[i]) < num2;
						if (flag4)
						{
							scrollViewState.velocity[i] = 0f;
						}
						else
						{
							ScrollViewState var_7_FA_cp_0 = scrollViewState;
							index = i;
							var_7_FA_cp_0.velocity[index] = var_7_FA_cp_0.velocity[index] + ((scrollViewState.velocity[i] < 0f) ? num2 : (-num2));
							ScrollViewState var_7_135_cp_0 = scrollViewState;
							index = i;
							var_7_135_cp_0.scrollPosition[index] = var_7_135_cp_0.scrollPosition[index] + scrollViewState.velocity[i] * num;
							flag2 = true;
							scrollViewState.touchScrollStartMousePosition = Event.current.mousePosition;
							scrollViewState.touchScrollStartPosition = scrollViewState.scrollPosition;
						}
					}
					bool flag5 = scrollViewState.velocity != Vector2.zero;
					if (flag5)
					{
						GUI.InternalRepaintEditorWindow();
					}
				}
				bool flag6 = handleScrollWheel && (Event.current.type == EventType.ScrollWheel || Event.current.type == EventType.TouchDown || Event.current.type == EventType.TouchUp || Event.current.type == EventType.TouchMove) && (scrollViewState.viewRect.width > scrollViewState.visibleRect.width || scrollViewState.viewRect.height > scrollViewState.visibleRect.height);
				if (flag6)
				{
					bool flag7 = Event.current.type == EventType.ScrollWheel && ((scrollViewState.viewRect.width > scrollViewState.visibleRect.width && !Mathf.Approximately(0f, Event.current.delta.x)) || (scrollViewState.viewRect.height > scrollViewState.visibleRect.height && !Mathf.Approximately(0f, Event.current.delta.y))) && scrollViewState.position.Contains(Event.current.mousePosition);
					if (flag7)
					{
						scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.scrollPosition.x + Event.current.delta.x * 20f, 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
						scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.scrollPosition.y + Event.current.delta.y * 20f, 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
						Event.current.Use();
						flag2 = true;
					}
					else
					{
						bool flag8 = Event.current.type == EventType.TouchDown && (Event.current.modifiers & EventModifiers.Alt) == EventModifiers.Alt && scrollViewState.position.Contains(Event.current.mousePosition);
						if (flag8)
						{
							scrollViewState.isDuringTouchScroll = true;
							scrollViewState.touchScrollStartMousePosition = Event.current.mousePosition;
							scrollViewState.touchScrollStartPosition = scrollViewState.scrollPosition;
							GUIUtility.hotControl = GUIUtility.GetControlID(GUI.s_ScrollviewHash, FocusType.Passive, scrollViewState.position);
							Event.current.Use();
						}
						else
						{
							bool flag9 = scrollViewState.isDuringTouchScroll && Event.current.type == EventType.TouchUp;
							if (flag9)
							{
								scrollViewState.isDuringTouchScroll = false;
							}
							else
							{
								bool flag10 = scrollViewState.isDuringTouchScroll && Event.current.type == EventType.TouchMove;
								if (flag10)
								{
									Vector2 scrollPosition = scrollViewState.scrollPosition;
									scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.touchScrollStartPosition.x - (Event.current.mousePosition.x - scrollViewState.touchScrollStartMousePosition.x), 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
									scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.touchScrollStartPosition.y - (Event.current.mousePosition.y - scrollViewState.touchScrollStartMousePosition.y), 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
									Event.current.Use();
									Vector2 b = (scrollViewState.scrollPosition - scrollPosition) / num;
									scrollViewState.velocity = Vector2.Lerp(scrollViewState.velocity, b, num * 10f);
									flag2 = true;
								}
							}
						}
					}
				}
				bool flag11 = flag2;
				if (flag11)
				{
					bool flag12 = scrollViewState.scrollPosition.x < 0f;
					if (flag12)
					{
						scrollViewState.scrollPosition.x = 0f;
					}
					bool flag13 = scrollViewState.scrollPosition.y < 0f;
					if (flag13)
					{
						scrollViewState.scrollPosition.y = 0f;
					}
					scrollViewState.apply = true;
				}
			}
		}

		internal static ScrollViewState GetTopScrollView()
		{
			bool flag = GUI.scrollViewStates.Count != 0;
			ScrollViewState result;
			if (flag)
			{
				result = (ScrollViewState)GUI.scrollViewStates.Peek();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static void ScrollTo(Rect position)
		{
			ScrollViewState topScrollView = GUI.GetTopScrollView();
			if (topScrollView != null)
			{
				topScrollView.ScrollTo(position);
			}
		}

		public static bool ScrollTowards(Rect position, float maxDelta)
		{
			ScrollViewState topScrollView = GUI.GetTopScrollView();
			bool flag = topScrollView == null;
			return !flag && topScrollView.ScrollTowards(position, maxDelta);
		}

		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin, true);
		}

		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin, true);
		}

		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin, true);
		}

		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin, true);
		}

		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin, true);
		}

		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoWindow(id, clientRect, func, title, style, GUI.skin, true);
		}

		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin);
		}

		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin);
		}

		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin);
		}

		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin);
		}

		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin);
		}

		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			return GUI.DoModalWindow(id, clientRect, func, content, style, GUI.skin);
		}

		private static Rect DoWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
		{
			return GUI.Internal_DoWindow(id, GUIUtility.s_OriginalID, clientRect, func, title, style, skin, forceRectOnLayout);
		}

		private static Rect DoModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
		{
			return GUI.Internal_DoModalWindow(id, GUIUtility.s_OriginalID, clientRect, func, content, style, skin);
		}

		[RequiredByNativeCode]
		internal static void CallWindowDelegate(GUI.WindowFunction func, int id, int instanceID, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
		{
			GUILayoutUtility.SelectIDList(id, true);
			GUISkin skin = GUI.skin;
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				bool flag2 = forceRect != 0;
				if (flag2)
				{
					GUILayoutOption[] options = new GUILayoutOption[]
					{
						GUILayout.Width(width),
						GUILayout.Height(height)
					};
					GUILayoutUtility.BeginWindow(id, style, options);
				}
				else
				{
					GUILayoutUtility.BeginWindow(id, style, null);
				}
			}
			else
			{
				GUILayoutUtility.BeginWindow(id, GUIStyle.none, null);
			}
			GUI.skin = _skin;
			if (func != null)
			{
				func(id);
			}
			bool flag3 = Event.current.type == EventType.Layout;
			if (flag3)
			{
				GUILayoutUtility.Layout();
			}
			GUI.skin = skin;
		}

		public static void DragWindow()
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			Matrix4x4 matrix = GUI.matrix;
			GUI.Internal_BeginWindows();
			GUI.matrix = matrix;
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		internal static void EndWindows()
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			GUI.Internal_EndWindows();
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_backgroundColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backgroundColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_contentColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_contentColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DoModalWindow_Injected(int id, int instanceID, ref Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, object skin, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DoWindow_Injected(int id, int instanceID, ref Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, object skin, bool forceRectOnLayout, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DragWindow_Injected(ref Rect position);
	}
}
