using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	[NativeHeader("Modules/UIElementsNative/TextNative.bindings.h")]
	internal static class TextNative
	{
		public static Vector2 GetCursorPosition(TextNativeSettings settings, Rect rect, int cursorIndex)
		{
			bool flag = settings.font == null;
			Vector2 result;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				result = Vector2.zero;
			}
			else
			{
				result = TextNative.DoGetCursorPosition(settings, rect, cursorIndex);
			}
			return result;
		}

		public static float ComputeTextWidth(TextNativeSettings settings)
		{
			bool flag = settings.font == null;
			float result;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				result = 0f;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(settings.text);
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					result = TextNative.DoComputeTextWidth(settings);
				}
			}
			return result;
		}

		public static float ComputeTextHeight(TextNativeSettings settings)
		{
			bool flag = settings.font == null;
			float result;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				result = 0f;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(settings.text);
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					result = TextNative.DoComputeTextHeight(settings);
				}
			}
			return result;
		}

		public static NativeArray<TextVertex> GetVertices(TextNativeSettings settings)
		{
			int num = 0;
			TextNative.GetVertices(settings, IntPtr.Zero, UnsafeUtility.SizeOf<TextVertex>(), ref num);
			NativeArray<TextVertex> nativeArray = new NativeArray<TextVertex>(num, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			bool flag = num > 0;
			if (flag)
			{
				TextNative.GetVertices(settings, (IntPtr)nativeArray.GetUnsafePtr<TextVertex>(), UnsafeUtility.SizeOf<TextVertex>(), ref num);
				Debug.Assert(num == nativeArray.Length);
			}
			return nativeArray;
		}

		public static Vector2 GetOffset(TextNativeSettings settings, Rect screenRect)
		{
			bool flag = settings.font == null;
			Vector2 result;
			if (flag)
			{
				Debug.LogError("Cannot process a null font.");
				result = new Vector2(0f, 0f);
			}
			else
			{
				settings.text = (settings.text ?? "");
				result = TextNative.DoGetOffset(settings, screenRect);
			}
			return result;
		}

		public static float ComputeTextScaling(Matrix4x4 worldMatrix, float pixelsPerPoint)
		{
			Vector3 vector = new Vector3(worldMatrix.m00, worldMatrix.m10, worldMatrix.m20);
			Vector3 vector2 = new Vector3(worldMatrix.m01, worldMatrix.m11, worldMatrix.m21);
			float num = (vector.magnitude + vector2.magnitude) / 2f;
			return num * pixelsPerPoint;
		}

		[FreeFunction(Name = "TextNative::ComputeTextWidth")]
		private static float DoComputeTextWidth(TextNativeSettings settings)
		{
			return TextNative.DoComputeTextWidth_Injected(ref settings);
		}

		[FreeFunction(Name = "TextNative::ComputeTextHeight")]
		private static float DoComputeTextHeight(TextNativeSettings settings)
		{
			return TextNative.DoComputeTextHeight_Injected(ref settings);
		}

		[FreeFunction(Name = "TextNative::GetCursorPosition")]
		private static Vector2 DoGetCursorPosition(TextNativeSettings settings, Rect rect, int cursorPosition)
		{
			Vector2 result;
			TextNative.DoGetCursorPosition_Injected(ref settings, ref rect, cursorPosition, out result);
			return result;
		}

		[FreeFunction(Name = "TextNative::GetVertices")]
		private static void GetVertices(TextNativeSettings settings, IntPtr buffer, int vertexSize, ref int vertexCount)
		{
			TextNative.GetVertices_Injected(ref settings, buffer, vertexSize, ref vertexCount);
		}

		[FreeFunction(Name = "TextNative::GetOffset")]
		private static Vector2 DoGetOffset(TextNativeSettings settings, Rect rect)
		{
			Vector2 result;
			TextNative.DoGetOffset_Injected(ref settings, ref rect, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float DoComputeTextWidth_Injected(ref TextNativeSettings settings);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float DoComputeTextHeight_Injected(ref TextNativeSettings settings);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DoGetCursorPosition_Injected(ref TextNativeSettings settings, ref Rect rect, int cursorPosition, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVertices_Injected(ref TextNativeSettings settings, IntPtr buffer, int vertexSize, ref int vertexCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DoGetOffset_Injected(ref TextNativeSettings settings, ref Rect rect, out Vector2 ret);
	}
}
