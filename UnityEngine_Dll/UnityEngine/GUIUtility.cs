using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Input/InputManager.h"), NativeHeader("Runtime/Input/InputBindings.h"), NativeHeader("Runtime/Utilities/CopyPaste.h"), NativeHeader("Modules/IMGUI/GUIManager.h"), NativeHeader("Modules/IMGUI/GUIUtility.h"), NativeHeader("Runtime/Camera/RenderLayers/GUITexture.h")]
	public class GUIUtility
	{
		internal static int s_SkinMode;

		internal static int s_OriginalID;

		internal static Action takeCapture;

		internal static Action releaseCapture;

		internal static Func<int, IntPtr, bool> processEvent;

		internal static Action cleanupRoots;

		internal static Func<Exception, bool> endContainerGUIFromException;

		internal static Action guiChanged;

		internal static Func<bool> s_HasCurrentWindowKeyFocusFunc;

		public static extern bool hasModalWindow
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("GetGUIState().m_PixelsPerPoint", true, TargetType.Field)]
		internal static extern float pixelsPerPoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("GetGUIState().m_OnGUIDepth", true, TargetType.Field)]
		internal static extern int guiDepth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static Vector2 s_EditorScreenPointOffset
		{
			[NativeMethod("GetGUIState().GetGUIPixelOffset", true)]
			get
			{
				Vector2 result;
				GUIUtility.get_s_EditorScreenPointOffset_Injected(out result);
				return result;
			}
			[NativeMethod("GetGUIState().SetGUIPixelOffset", true)]
			set
			{
				GUIUtility.set_s_EditorScreenPointOffset_Injected(ref value);
			}
		}

		[NativeProperty("GetGUIState().m_CanvasGUIState.m_IsMouseUsed", true, TargetType.Field)]
		internal static extern bool mouseUsed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetInputManager()", StaticAccessorType.Dot)]
		internal static extern bool textFieldInput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static extern bool manualTex2SRGBEnabled
		{
			[FreeFunction("GUITexture::IsManualTex2SRGBEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("GUITexture::SetManualTex2SRGBEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string systemCopyBuffer
		{
			[FreeFunction("GetCopyBuffer")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetCopyBuffer")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static extern string compositionString
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static extern IMECompositionMode imeCompositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 result;
				GUIUtility.get_compositionCursorPos_Injected(out result);
				return result;
			}
			set
			{
				GUIUtility.set_compositionCursorPos_Injected(ref value);
			}
		}

		internal static bool guiIsExiting
		{
			get;
			set;
		}

		public static int hotControl
		{
			get
			{
				return GUIUtility.Internal_GetHotControl();
			}
			set
			{
				GUIUtility.Internal_SetHotControl(value);
			}
		}

		public static int keyboardControl
		{
			get
			{
				return GUIUtility.Internal_GetKeyboardControl();
			}
			set
			{
				GUIUtility.Internal_SetKeyboardControl(value);
			}
		}

		[StaticAccessor("GetGUIState()", StaticAccessorType.Dot)]
		public static int GetControlID(int hint, FocusType focusType, Rect rect)
		{
			return GUIUtility.GetControlID_Injected(hint, focusType, ref rect);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void BeginContainerFromOwner(ScriptableObject owner);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void BeginContainer(ObjectGUIState objectGUIState);

		[NativeMethod("EndContainer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_EndContainer();

		[FreeFunction("GetSpecificGUIState(0).m_EternalGUIState->GetNextUniqueID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetPermanentControlID();

		[StaticAccessor("GetUndoManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UpdateUndoName();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CheckForTabEvent(Event evt);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetKeyboardControlToFirstControlId();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetKeyboardControlToLastControlId();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasFocusableControls();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool OwnsId(int id);

		public static Rect AlignRectToDevice(Rect rect, out int widthInPixels, out int heightInPixels)
		{
			Rect result;
			GUIUtility.AlignRectToDevice_Injected(ref rect, out widthInPixels, out heightInPixels, out result);
			return result;
		}

		internal static Vector3 Internal_MultiplyPoint(Vector3 point, Matrix4x4 transform)
		{
			Vector3 result;
			GUIUtility.Internal_MultiplyPoint_Injected(ref point, ref transform, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetChanged();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetChanged(bool changed);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHotControl();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetKeyboardControl();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHotControl(int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetKeyboardControl(int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object Internal_GetDefaultSkin(int skinMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_GetBuiltinSkin(int skin);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExitGUI();

		private static Vector2 InternalWindowToScreenPoint(Vector2 windowPoint)
		{
			Vector2 result;
			GUIUtility.InternalWindowToScreenPoint_Injected(ref windowPoint, out result);
			return result;
		}

		private static Vector2 InternalScreenToWindowPoint(Vector2 screenPoint)
		{
			Vector2 result;
			GUIUtility.InternalScreenToWindowPoint_Injected(ref screenPoint, out result);
			return result;
		}

		[RequiredByNativeCode]
		private static void MarkGUIChanged()
		{
			Action expr_06 = GUIUtility.guiChanged;
			if (expr_06 != null)
			{
				expr_06();
			}
		}

		public static int GetControlID(FocusType focus)
		{
			return GUIUtility.GetControlID(0, focus);
		}

		public static int GetControlID(GUIContent contents, FocusType focus)
		{
			return GUIUtility.GetControlID(contents.hash, focus);
		}

		public static int GetControlID(FocusType focus, Rect position)
		{
			return GUIUtility.GetControlID(0, focus, position);
		}

		public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
		{
			return GUIUtility.GetControlID(contents.hash, focus, position);
		}

		public static int GetControlID(int hint, FocusType focus)
		{
			return GUIUtility.GetControlID(hint, focus, Rect.zero);
		}

		public static object GetStateObject(Type t, int controlID)
		{
			return GUIStateObjects.GetStateObject(t, controlID);
		}

		public static object QueryStateObject(Type t, int controlID)
		{
			return GUIStateObjects.QueryStateObject(t, controlID);
		}

		[RequiredByNativeCode]
		internal static void TakeCapture()
		{
			Action expr_06 = GUIUtility.takeCapture;
			if (expr_06 != null)
			{
				expr_06();
			}
		}

		[RequiredByNativeCode]
		internal static void RemoveCapture()
		{
			Action expr_06 = GUIUtility.releaseCapture;
			if (expr_06 != null)
			{
				expr_06();
			}
		}

		internal static bool HasKeyFocus(int controlID)
		{
			return controlID == GUIUtility.keyboardControl && (GUIUtility.s_HasCurrentWindowKeyFocusFunc == null || GUIUtility.s_HasCurrentWindowKeyFocusFunc());
		}

		public static void ExitGUI()
		{
			throw new ExitGUIException();
		}

		internal static GUISkin GetDefaultSkin(int skinMode)
		{
			return GUIUtility.Internal_GetDefaultSkin(skinMode) as GUISkin;
		}

		internal static GUISkin GetDefaultSkin()
		{
			return GUIUtility.Internal_GetDefaultSkin(GUIUtility.s_SkinMode) as GUISkin;
		}

		internal static GUISkin GetBuiltinSkin(int skin)
		{
			return GUIUtility.Internal_GetBuiltinSkin(skin) as GUISkin;
		}

		[RequiredByNativeCode]
		internal static void ProcessEvent(int instanceID, IntPtr nativeEventPtr, out bool result)
		{
			result = false;
			bool flag = GUIUtility.processEvent != null;
			if (flag)
			{
				result = GUIUtility.processEvent(instanceID, nativeEventPtr);
			}
		}

		internal static void EndContainer()
		{
			GUIUtility.Internal_EndContainer();
			GUIUtility.Internal_ExitGUI();
		}

		internal static void CleanupRoots()
		{
			Action expr_06 = GUIUtility.cleanupRoots;
			if (expr_06 != null)
			{
				expr_06();
			}
		}

		[RequiredByNativeCode]
		internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUIUtility.s_OriginalID = instanceID;
			GUIUtility.ResetGlobalState();
			bool flag = useGUILayout != 0;
			if (flag)
			{
				GUILayoutUtility.Begin(instanceID);
			}
		}

		[RequiredByNativeCode]
		internal static void SetSkin(int skinMode)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUI.DoSetSkin(null);
		}

		[RequiredByNativeCode]
		internal static void EndGUI(int layoutType)
		{
			try
			{
				bool flag = Event.current.type == EventType.Layout;
				if (flag)
				{
					switch (layoutType)
					{
					case 1:
						GUILayoutUtility.Layout();
						break;
					case 2:
						GUILayoutUtility.LayoutFromEditorWindow();
						break;
					}
				}
				GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
				GUIContent.ClearStaticCache();
			}
			finally
			{
				GUIUtility.Internal_ExitGUI();
			}
		}

		[RequiredByNativeCode]
		internal static bool EndGUIFromException(Exception exception)
		{
			GUIUtility.Internal_ExitGUI();
			return GUIUtility.ShouldRethrowException(exception);
		}

		[RequiredByNativeCode]
		internal static bool EndContainerGUIFromException(Exception exception)
		{
			bool flag = GUIUtility.endContainerGUIFromException != null;
			return flag && GUIUtility.endContainerGUIFromException(exception);
		}

		internal static void ResetGlobalState()
		{
			GUI.skin = null;
			GUIUtility.guiIsExiting = false;
			GUI.changed = false;
			GUI.scrollViewStates.Clear();
		}

		internal static bool IsExitGUIException(Exception exception)
		{
			while (exception is TargetInvocationException && exception.InnerException != null)
			{
				exception = exception.InnerException;
			}
			return exception is ExitGUIException;
		}

		internal static bool ShouldRethrowException(Exception exception)
		{
			return GUIUtility.IsExitGUIException(exception);
		}

		internal static void CheckOnGUI()
		{
			bool flag = GUIUtility.guiDepth <= 0;
			if (flag)
			{
				throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
			}
		}

		internal static float RoundToPixelGrid(float v)
		{
			return Mathf.Floor(v * GUIUtility.pixelsPerPoint + 0.48f) / GUIUtility.pixelsPerPoint;
		}

		public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
		{
			return GUIUtility.InternalWindowToScreenPoint(GUIClip.UnclipToWindow(guiPoint));
		}

		public static Rect GUIToScreenRect(Rect guiRect)
		{
			Vector2 vector = GUIUtility.GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
			guiRect.x = vector.x;
			guiRect.y = vector.y;
			return guiRect;
		}

		public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
		{
			return GUIClip.ClipToWindow(GUIUtility.InternalScreenToWindowPoint(screenPoint));
		}

		public static Rect ScreenToGUIRect(Rect screenRect)
		{
			Vector2 vector = GUIUtility.ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
			screenRect.x = vector.x;
			screenRect.y = vector.y;
			return screenRect;
		}

		public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.identity;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 lhs = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = lhs * matrix;
		}

		public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 lhs = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = lhs * matrix;
		}

		public static Rect AlignRectToDevice(Rect rect)
		{
			int num;
			int num2;
			return GUIUtility.AlignRectToDevice(rect, out num, out num2);
		}

		internal static bool HitTest(Rect rect, Vector2 point, int offset)
		{
			return point.x >= rect.xMin - (float)offset && point.x < rect.xMax + (float)offset && point.y >= rect.yMin - (float)offset && point.y < rect.yMax + (float)offset;
		}

		internal static bool HitTest(Rect rect, Vector2 point, bool isDirectManipulationDevice)
		{
			int offset = isDirectManipulationDevice ? 3 : 0;
			return GUIUtility.HitTest(rect, point, offset);
		}

		internal static bool HitTest(Rect rect, Event evt)
		{
			return GUIUtility.HitTest(rect, evt.mousePosition, evt.isDirectManipulationDevice);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_s_EditorScreenPointOffset_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_s_EditorScreenPointOffset_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetControlID_Injected(int hint, FocusType focusType, ref Rect rect);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AlignRectToDevice_Injected(ref Rect rect, out int widthInPixels, out int heightInPixels, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositionCursorPos_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_MultiplyPoint_Injected(ref Vector3 point, ref Matrix4x4 transform, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalWindowToScreenPoint_Injected(ref Vector2 windowPoint, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalScreenToWindowPoint_Injected(ref Vector2 screenPoint, out Vector2 ret);
	}
}
