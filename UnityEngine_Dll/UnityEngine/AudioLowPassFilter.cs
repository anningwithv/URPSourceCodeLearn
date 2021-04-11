using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioLowPassFilter : Behaviour
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("AudioLowPassFilter.lowpassResonaceQ is obsolete. Use lowpassResonanceQ instead (UnityUpgradable) -> lowpassResonanceQ", true)]
		public float lowpassResonaceQ
		{
			get
			{
				return this.lowpassResonanceQ;
			}
			set
			{
			}
		}

		public AnimationCurve customCutoffCurve
		{
			get
			{
				return this.GetCustomLowpassLevelCurveCopy();
			}
			set
			{
				AudioLowPassFilter.SetCustomLowpassLevelCurveHelper(this, value);
			}
		}

		public extern float cutoffFrequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float lowpassResonanceQ
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationCurve GetCustomLowpassLevelCurveCopy();

		[NativeMethod(Name = "AudioLowPassFilterBindings::SetCustomLowpassLevelCurveHelper", IsFreeFunction = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomLowpassLevelCurveHelper([NotNull("NullExceptionObject")] AudioLowPassFilter source, AnimationCurve curve);
	}
}
