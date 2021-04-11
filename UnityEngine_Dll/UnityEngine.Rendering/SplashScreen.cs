using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Graphics/DrawSplashScreenAndWatermarks.h")]
	public class SplashScreen
	{
		public enum StopBehavior
		{
			StopImmediate,
			FadeOut
		}

		public static extern bool isFinished
		{
			[FreeFunction("IsSplashScreenFinished")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelSplashScreen();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSplashScreenFade();

		[FreeFunction("BeginSplashScreen_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin();

		public static void Stop(SplashScreen.StopBehavior stopBehavior)
		{
			bool flag = stopBehavior == SplashScreen.StopBehavior.FadeOut;
			if (flag)
			{
				SplashScreen.BeginSplashScreenFade();
			}
			else
			{
				SplashScreen.CancelSplashScreen();
			}
		}

		[FreeFunction("DrawSplashScreen_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Draw();

		[FreeFunction("SetSplashScreenTime")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTime(float time);
	}
}
