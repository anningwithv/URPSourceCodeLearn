using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioDistortionFilter : Behaviour
	{
		public extern float distortionLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
