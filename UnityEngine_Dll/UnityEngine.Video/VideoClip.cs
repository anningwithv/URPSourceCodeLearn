using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Video
{
	[NativeHeader("Modules/Video/Public/VideoClip.h"), RequiredByNativeCode]
	public sealed class VideoClip : UnityEngine.Object
	{
		public extern string originalPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ulong frameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern double frameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeName("Duration")]
		public extern double length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint pixelAspectRatioNumerator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint pixelAspectRatioDenominator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool sRGB
		{
			[NativeName("IssRGB")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ushort audioTrackCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private VideoClip()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort GetAudioChannelCount(ushort audioTrackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetAudioSampleRate(ushort audioTrackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetAudioLanguage(ushort audioTrackIdx);
	}
}
