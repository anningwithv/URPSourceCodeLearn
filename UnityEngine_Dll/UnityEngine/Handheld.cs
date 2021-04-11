using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.iOS;

namespace UnityEngine
{
	[NativeHeader("Runtime/Video/MoviePlayback.h"), NativeHeader("Runtime/Export/Handheld/Handheld.bindings.h"), NativeHeader("Runtime/Input/GetInput.h")]
	public class Handheld
	{
		[Obsolete("Property Handheld.use32BitDisplayBuffer has been deprecated. Modifying it has no effect, use PlayerSettings instead.")]
		public static bool use32BitDisplayBuffer
		{
			get
			{
				return Handheld.GetUse32BitDisplayBuffer_Bindings();
			}
			set
			{
			}
		}

		public static bool PlayFullScreenMovie(string path, [DefaultValue("Color.black")] Color bgColor, [DefaultValue("FullScreenMovieControlMode.Full")] FullScreenMovieControlMode controlMode, [DefaultValue("FullScreenMovieScalingMode.AspectFit")] FullScreenMovieScalingMode scalingMode)
		{
			return Handheld.PlayFullScreenMovie_Bindings(path, bgColor, controlMode, scalingMode);
		}

		[ExcludeFromDocs]
		public static bool PlayFullScreenMovie(string path, Color bgColor, FullScreenMovieControlMode controlMode)
		{
			FullScreenMovieScalingMode scalingMode = FullScreenMovieScalingMode.AspectFit;
			return Handheld.PlayFullScreenMovie_Bindings(path, bgColor, controlMode, scalingMode);
		}

		[ExcludeFromDocs]
		public static bool PlayFullScreenMovie(string path, Color bgColor)
		{
			FullScreenMovieScalingMode scalingMode = FullScreenMovieScalingMode.AspectFit;
			FullScreenMovieControlMode controlMode = FullScreenMovieControlMode.Full;
			return Handheld.PlayFullScreenMovie_Bindings(path, bgColor, controlMode, scalingMode);
		}

		[ExcludeFromDocs]
		public static bool PlayFullScreenMovie(string path)
		{
			FullScreenMovieScalingMode scalingMode = FullScreenMovieScalingMode.AspectFit;
			FullScreenMovieControlMode controlMode = FullScreenMovieControlMode.Full;
			Color black = Color.black;
			return Handheld.PlayFullScreenMovie_Bindings(path, black, controlMode, scalingMode);
		}

		[FreeFunction("PlayFullScreenMovie")]
		private static bool PlayFullScreenMovie_Bindings(string path, Color bgColor, FullScreenMovieControlMode controlMode, FullScreenMovieScalingMode scalingMode)
		{
			return Handheld.PlayFullScreenMovie_Bindings_Injected(path, ref bgColor, controlMode, scalingMode);
		}

		[FreeFunction("Vibrate")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Vibrate();

		[FreeFunction("GetUse32BitDisplayBuffer_Bindings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetUse32BitDisplayBuffer_Bindings();

		[FreeFunction("SetActivityIndicatorStyle_Bindings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActivityIndicatorStyleImpl_Bindings(int style);

		public static void SetActivityIndicatorStyle(ActivityIndicatorStyle style)
		{
			Handheld.SetActivityIndicatorStyleImpl_Bindings((int)style);
		}

		public static void SetActivityIndicatorStyle(AndroidActivityIndicatorStyle style)
		{
			Handheld.SetActivityIndicatorStyleImpl_Bindings((int)style);
		}

		[FreeFunction("GetActivityIndicatorStyle_Bindings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetActivityIndicatorStyle();

		[FreeFunction("StartActivityIndicator_Bindings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StartActivityIndicator();

		[FreeFunction("StopActivityIndicator_Bindings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopActivityIndicator();

		[FreeFunction("ClearShaderCache_Bindings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearShaderCache();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool PlayFullScreenMovie_Bindings_Injected(string path, ref Color bgColor, FullScreenMovieControlMode controlMode, FullScreenMovieScalingMode scalingMode);
	}
}
