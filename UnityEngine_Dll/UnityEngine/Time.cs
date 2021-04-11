using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Input/TimeManager.h"), StaticAccessor("GetTimeManager()", StaticAccessorType.Dot)]
	public class Time
	{
		[NativeProperty("CurTime")]
		public static extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("CurTime")]
		public static extern double timeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("TimeSinceSceneLoad")]
		public static extern float timeSinceLevelLoad
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("TimeSinceSceneLoad")]
		public static extern double timeSinceLevelLoadAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float deltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float fixedTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("FixedTime")]
		public static extern double fixedTimeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float unscaledTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("UnscaledTime")]
		public static extern double unscaledTimeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float fixedUnscaledTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("FixedUnscaledTime")]
		public static extern double fixedUnscaledTimeAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float unscaledDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float fixedUnscaledDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float fixedDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float maximumDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float smoothDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float maximumParticleDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float timeScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int frameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("RenderFrameCount")]
		public static extern int renderedFrameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("Realtime")]
		public static extern float realtimeSinceStartup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("Realtime")]
		public static extern double realtimeSinceStartupAsDouble
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float captureDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static int captureFramerate
		{
			get
			{
				return (Time.captureDeltaTime == 0f) ? 0 : ((int)Mathf.Round(1f / Time.captureDeltaTime));
			}
			set
			{
				Time.captureDeltaTime = ((value == 0) ? 0f : (1f / (float)value));
			}
		}

		public static extern bool inFixedTimeStep
		{
			[NativeName("IsUsingFixedTimeStep")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
