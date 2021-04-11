using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Audio
{
	[NativeHeader("Modules/Audio/Public/AudioMixer.h"), NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixer.bindings.h"), ExcludeFromObjectFactory, ExcludeFromPreset]
	public class AudioMixer : UnityEngine.Object
	{
		[NativeProperty]
		public extern AudioMixerGroup outputAudioMixerGroup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty]
		public extern AudioMixerUpdateMode updateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal AudioMixer()
		{
		}

		[NativeMethod("FindSnapshotFromName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AudioMixerSnapshot FindSnapshot(string name);

		[NativeMethod("AudioMixerBindings::FindMatchingGroups", IsFreeFunction = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AudioMixerGroup[] FindMatchingGroups(string subPath);

		internal void TransitionToSnapshot(AudioMixerSnapshot snapshot, float timeToReach)
		{
			bool flag = snapshot == null;
			if (flag)
			{
				throw new ArgumentException("null Snapshot passed to AudioMixer.TransitionToSnapshot of AudioMixer '" + base.name + "'");
			}
			bool flag2 = snapshot.audioMixer != this;
			if (flag2)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Snapshot '",
					snapshot.name,
					"' passed to AudioMixer.TransitionToSnapshot is not a snapshot from AudioMixer '",
					base.name,
					"'"
				}));
			}
			this.TransitionToSnapshotInternal(snapshot, timeToReach);
		}

		[NativeMethod("TransitionToSnapshot")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TransitionToSnapshotInternal(AudioMixerSnapshot snapshot, float timeToReach);

		[NativeMethod("AudioMixerBindings::TransitionToSnapshots", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void TransitionToSnapshots(AudioMixerSnapshot[] snapshots, float[] weights, float timeToReach);

		[NativeMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetFloat(string name, float value);

		[NativeMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool ClearFloat(string name);

		[NativeMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetFloat(string name, out float value);
	}
}
