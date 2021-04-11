using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Random/Random.bindings.h")]
	public static class Random
	{
		[Serializable]
		public struct State
		{
			[SerializeField]
			private int s0;

			[SerializeField]
			private int s1;

			[SerializeField]
			private int s2;

			[SerializeField]
			private int s3;
		}

		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static Random.State state
		{
			get
			{
				Random.State result;
				Random.get_state_Injected(out result);
				return result;
			}
			set
			{
				Random.set_state_Injected(ref value);
			}
		}

		public static extern float value
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Vector3 insideUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 result;
				Random.get_insideUnitSphere_Injected(out result);
				return result;
			}
		}

		public static Vector2 insideUnitCircle
		{
			get
			{
				Vector2 result;
				Random.GetRandomUnitCircle(out result);
				return result;
			}
		}

		public static Vector3 onUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 result;
				Random.get_onUnitSphere_Injected(out result);
				return result;
			}
		}

		public static Quaternion rotation
		{
			[FreeFunction]
			get
			{
				Quaternion result;
				Random.get_rotation_Injected(out result);
				return result;
			}
		}

		public static Quaternion rotationUniform
		{
			[FreeFunction]
			get
			{
				Quaternion result;
				Random.get_rotationUniform_Injected(out result);
				return result;
			}
		}

		[Obsolete("Deprecated. Use InitState() function or Random.state property instead."), StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static extern int seed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeMethod("SetSeed"), StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitState(int seed);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Range(float minInclusive, float maxInclusive);

		public static int Range(int minInclusive, int maxExclusive)
		{
			return Random.RandomRangeInt(minInclusive, maxExclusive);
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RandomRangeInt(int minInclusive, int maxExclusive);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRandomUnitCircle(out Vector2 output);

		[Obsolete("Use Random.Range instead")]
		public static float RandomRange(float min, float max)
		{
			return Random.Range(min, max);
		}

		[Obsolete("Use Random.Range instead")]
		public static int RandomRange(int min, int max)
		{
			return Random.Range(min, max);
		}

		public static Color ColorHSV()
		{
			return Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		public static Color ColorHSV(float hueMin, float hueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 0f, 1f, 1f, 1f);
		}

		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
		}

		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
		{
			float h = Mathf.Lerp(hueMin, hueMax, Random.value);
			float s = Mathf.Lerp(saturationMin, saturationMax, Random.value);
			float v = Mathf.Lerp(valueMin, valueMax, Random.value);
			Color result = Color.HSVToRGB(h, s, v, true);
			result.a = Mathf.Lerp(alphaMin, alphaMax, Random.value);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_state_Injected(out Random.State ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_state_Injected(ref Random.State value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_insideUnitSphere_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_onUnitSphere_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationUniform_Injected(out Quaternion ret);
	}
}
