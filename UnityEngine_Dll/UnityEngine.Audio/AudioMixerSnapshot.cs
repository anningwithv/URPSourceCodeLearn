using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Audio
{
	[NativeHeader("Modules/Audio/Public/AudioMixerSnapshot.h")]
	public class AudioMixerSnapshot : UnityEngine.Object, ISubAssetNotDuplicatable
	{
		[NativeProperty]
		public extern AudioMixer audioMixer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal AudioMixerSnapshot()
		{
		}

		public void TransitionTo(float timeToReach)
		{
			this.audioMixer.TransitionToSnapshot(this, timeToReach);
		}
	}
}
