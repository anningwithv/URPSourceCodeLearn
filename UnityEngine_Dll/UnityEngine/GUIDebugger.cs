using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/IMGUI/GUIDebugger.bindings.h")]
	internal class GUIDebugger
	{
		[NativeConditional("UNITY_EDITOR")]
		public static extern bool active
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeConditional("UNITY_EDITOR")]
		public static void LogLayoutEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style)
		{
			GUIDebugger.LogLayoutEntry_Injected(ref rect, left, right, top, bottom, style);
		}

		[NativeConditional("UNITY_EDITOR")]
		public static void LogLayoutGroupEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical)
		{
			GUIDebugger.LogLayoutGroupEntry_Injected(ref rect, left, right, top, bottom, style, isVertical);
		}

		[NativeConditional("UNITY_EDITOR"), NativeMethod("LogEndGroup"), StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LogLayoutEndGroup();

		[NativeConditional("UNITY_EDITOR"), StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		public static void LogBeginProperty(string targetTypeAssemblyQualifiedName, string path, Rect position)
		{
			GUIDebugger.LogBeginProperty_Injected(targetTypeAssemblyQualifiedName, path, ref position);
		}

		[NativeConditional("UNITY_EDITOR"), StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LogEndProperty();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogLayoutEntry_Injected(ref Rect rect, int left, int right, int top, int bottom, GUIStyle style);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogLayoutGroupEntry_Injected(ref Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogBeginProperty_Injected(string targetTypeAssemblyQualifiedName, string path, ref Rect position);
	}
}
