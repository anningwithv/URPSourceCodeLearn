using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioHighPassFilter : Behaviour
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("AudioHighPassFilter.highpassResonaceQ is obsolete. Use highpassResonanceQ instead (UnityUpgradable) -> highpassResonanceQ", true)]
		public float highpassResonaceQ
		{
			get
			{
				return this.highpassResonanceQ;
			}
			set
			{
			}
		}

		public extern float cutoffFrequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float highpassResonanceQ
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
