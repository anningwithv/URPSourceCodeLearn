using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Audio
{
	[NativeHeader("Modules/Audio/Public/AudioMixerGroup.h")]
	public class AudioMixerGroup : UnityEngine.Object, ISubAssetNotDuplicatable
	{
		[NativeProperty]
		public extern AudioMixer audioMixer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal AudioMixerGroup()
		{
		}
	}
}
