using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemModulesScriptBindings.h"), NativeHeader("ParticleSystemScriptingClasses.h"), NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h"), NativeHeader("Modules/ParticleSystem/ParticleSystemGeometryJob.h"), NativeHeader("Modules/ParticleSystem/ParticleSystem.h"), NativeHeader("Modules/ParticleSystem/ParticleSystem.h"), NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h"), NativeHeader("ParticleSystemScriptingClasses.h"), RequireComponent(typeof(Transform)), UsedByNativeCode]
	public sealed class ParticleSystem : Component
	{
		[NativeType(CodegenOptions.Custom, "MonoMinMaxCurve", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		public struct MinMaxCurve
		{
			[SerializeField]
			private ParticleSystemCurveMode m_Mode;

			[SerializeField]
			private float m_CurveMultiplier;

			[SerializeField]
			private AnimationCurve m_CurveMin;

			[SerializeField]
			private AnimationCurve m_CurveMax;

			[SerializeField]
			private float m_ConstantMin;

			[SerializeField]
			private float m_ConstantMax;

			[Obsolete("Please use MinMaxCurve.curveMultiplier instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/MinMaxCurve.curveMultiplier", false)]
			public float curveScalar
			{
				get
				{
					return this.m_CurveMultiplier;
				}
				set
				{
					this.m_CurveMultiplier = value;
				}
			}

			public ParticleSystemCurveMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			public float curveMultiplier
			{
				get
				{
					return this.m_CurveMultiplier;
				}
				set
				{
					this.m_CurveMultiplier = value;
				}
			}

			public AnimationCurve curveMax
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			public AnimationCurve curveMin
			{
				get
				{
					return this.m_CurveMin;
				}
				set
				{
					this.m_CurveMin = value;
				}
			}

			public float constantMax
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			public float constantMin
			{
				get
				{
					return this.m_ConstantMin;
				}
				set
				{
					this.m_ConstantMin = value;
				}
			}

			public float constant
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			public AnimationCurve curve
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			public MinMaxCurve(float constant)
			{
				this.m_Mode = ParticleSystemCurveMode.Constant;
				this.m_CurveMultiplier = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = constant;
			}

			public MinMaxCurve(float multiplier, AnimationCurve curve)
			{
				this.m_Mode = ParticleSystemCurveMode.Curve;
				this.m_CurveMultiplier = multiplier;
				this.m_CurveMin = null;
				this.m_CurveMax = curve;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			public MinMaxCurve(float multiplier, AnimationCurve min, AnimationCurve max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoCurves;
				this.m_CurveMultiplier = multiplier;
				this.m_CurveMin = min;
				this.m_CurveMax = max;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			public MinMaxCurve(float min, float max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoConstants;
				this.m_CurveMultiplier = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = min;
				this.m_ConstantMax = max;
			}

			public float Evaluate(float time)
			{
				return this.Evaluate(time, 1f);
			}

			public float Evaluate(float time, float lerpFactor)
			{
				float result;
				switch (this.mode)
				{
				case ParticleSystemCurveMode.Constant:
					result = this.m_ConstantMax;
					return result;
				case ParticleSystemCurveMode.TwoCurves:
					result = Mathf.Lerp(this.m_CurveMin.Evaluate(time), this.m_CurveMax.Evaluate(time), lerpFactor) * this.m_CurveMultiplier;
					return result;
				case ParticleSystemCurveMode.TwoConstants:
					result = Mathf.Lerp(this.m_ConstantMin, this.m_ConstantMax, lerpFactor);
					return result;
				}
				result = this.m_CurveMax.Evaluate(time) * this.m_CurveMultiplier;
				return result;
			}

			public static implicit operator ParticleSystem.MinMaxCurve(float constant)
			{
				return new ParticleSystem.MinMaxCurve(constant);
			}
		}

		public struct MainModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("Please use flipRotation instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/MainModule.flipRotation", false)]
			public float randomizeRotationDirection
			{
				get
				{
					return this.flipRotation;
				}
				set
				{
					this.flipRotation = value;
				}
			}

			public float duration
			{
				get
				{
					return ParticleSystem.MainModule.get_duration_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_duration_Injected(ref this, value);
				}
			}

			public bool loop
			{
				get
				{
					return ParticleSystem.MainModule.get_loop_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_loop_Injected(ref this, value);
				}
			}

			public bool prewarm
			{
				get
				{
					return ParticleSystem.MainModule.get_prewarm_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_prewarm_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startDelay
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startDelay_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startDelay_Injected(ref this, ref value);
				}
			}

			public float startDelayMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startDelayMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startDelayMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startLifetime
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startLifetime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startLifetime_Injected(ref this, ref value);
				}
			}

			public float startLifetimeMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startLifetimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startLifetimeMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSpeed_Injected(ref this, ref value);
				}
			}

			public float startSpeedMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSpeedMultiplier_Injected(ref this, value);
				}
			}

			public bool startSize3D
			{
				get
				{
					return ParticleSystem.MainModule.get_startSize3D_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSize3D_Injected(ref this, value);
				}
			}

			[NativeName("StartSizeX")]
			public ParticleSystem.MinMaxCurve startSize
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSize_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSize_Injected(ref this, ref value);
				}
			}

			[NativeName("StartSizeXMultiplier")]
			public float startSizeMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startSizeX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSizeX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeX_Injected(ref this, ref value);
				}
			}

			public float startSizeXMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeXMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startSizeY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSizeY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeY_Injected(ref this, ref value);
				}
			}

			public float startSizeYMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeYMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startSizeZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startSizeZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeZ_Injected(ref this, ref value);
				}
			}

			public float startSizeZMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startSizeZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeZMultiplier_Injected(ref this, value);
				}
			}

			public bool startRotation3D
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotation3D_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotation3D_Injected(ref this, value);
				}
			}

			[NativeName("StartRotationZ")]
			public ParticleSystem.MinMaxCurve startRotation
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startRotation_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotation_Injected(ref this, ref value);
				}
			}

			[NativeName("StartRotationZMultiplier")]
			public float startRotationMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startRotationX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startRotationX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationX_Injected(ref this, ref value);
				}
			}

			public float startRotationXMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationXMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startRotationY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startRotationY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationY_Injected(ref this, ref value);
				}
			}

			public float startRotationYMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationYMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startRotationZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_startRotationZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationZ_Injected(ref this, ref value);
				}
			}

			public float startRotationZMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_startRotationZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationZMultiplier_Injected(ref this, value);
				}
			}

			public float flipRotation
			{
				get
				{
					return ParticleSystem.MainModule.get_flipRotation_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_flipRotation_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxGradient startColor
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.MainModule.get_startColor_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startColor_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve gravityModifier
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.MainModule.get_gravityModifier_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_gravityModifier_Injected(ref this, ref value);
				}
			}

			public float gravityModifierMultiplier
			{
				get
				{
					return ParticleSystem.MainModule.get_gravityModifierMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_gravityModifierMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystemSimulationSpace simulationSpace
			{
				get
				{
					return ParticleSystem.MainModule.get_simulationSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_simulationSpace_Injected(ref this, value);
				}
			}

			public Transform customSimulationSpace
			{
				get
				{
					return ParticleSystem.MainModule.get_customSimulationSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_customSimulationSpace_Injected(ref this, value);
				}
			}

			public float simulationSpeed
			{
				get
				{
					return ParticleSystem.MainModule.get_simulationSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_simulationSpeed_Injected(ref this, value);
				}
			}

			public bool useUnscaledTime
			{
				get
				{
					return ParticleSystem.MainModule.get_useUnscaledTime_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_useUnscaledTime_Injected(ref this, value);
				}
			}

			public ParticleSystemScalingMode scalingMode
			{
				get
				{
					return ParticleSystem.MainModule.get_scalingMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_scalingMode_Injected(ref this, value);
				}
			}

			public bool playOnAwake
			{
				get
				{
					return ParticleSystem.MainModule.get_playOnAwake_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_playOnAwake_Injected(ref this, value);
				}
			}

			public int maxParticles
			{
				get
				{
					return ParticleSystem.MainModule.get_maxParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_maxParticles_Injected(ref this, value);
				}
			}

			public ParticleSystemEmitterVelocityMode emitterVelocityMode
			{
				get
				{
					return ParticleSystem.MainModule.get_emitterVelocityMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_emitterVelocityMode_Injected(ref this, value);
				}
			}

			public ParticleSystemStopAction stopAction
			{
				get
				{
					return ParticleSystem.MainModule.get_stopAction_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_stopAction_Injected(ref this, value);
				}
			}

			public ParticleSystemRingBufferMode ringBufferMode
			{
				get
				{
					return ParticleSystem.MainModule.get_ringBufferMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_ringBufferMode_Injected(ref this, value);
				}
			}

			public Vector2 ringBufferLoopRange
			{
				get
				{
					Vector2 result;
					ParticleSystem.MainModule.get_ringBufferLoopRange_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_ringBufferLoopRange_Injected(ref this, ref value);
				}
			}

			public ParticleSystemCullingMode cullingMode
			{
				get
				{
					return ParticleSystem.MainModule.get_cullingMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_cullingMode_Injected(ref this, value);
				}
			}

			internal MainModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_duration_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_duration_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_loop_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_loop_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_prewarm_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_prewarm_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startDelay_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startDelay_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startDelayMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startDelayMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startLifetime_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startLifetime_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startLifetimeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startLifetimeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSpeed_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSpeed_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSpeedMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSpeedMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_startSize3D_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSize3D_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSize_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSize_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSizeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeX_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeX_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSizeXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeY_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeY_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSizeYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeZ_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeZ_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startSizeZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_startRotation3D_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotation3D_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotation_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotation_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startRotationMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationX_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationX_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startRotationXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationXMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationY_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationY_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startRotationYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationYMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationZ_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationZ_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startRotationZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationZMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_flipRotation_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_flipRotation_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startColor_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startColor_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_gravityModifier_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_gravityModifier_Injected(ref ParticleSystem.MainModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_gravityModifierMultiplier_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_gravityModifierMultiplier_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSimulationSpace get_simulationSpace_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_simulationSpace_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemSimulationSpace value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Transform get_customSimulationSpace_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_customSimulationSpace_Injected(ref ParticleSystem.MainModule _unity_self, Transform value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_simulationSpeed_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_simulationSpeed_Injected(ref ParticleSystem.MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useUnscaledTime_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useUnscaledTime_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemScalingMode get_scalingMode_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scalingMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemScalingMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_playOnAwake_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_playOnAwake_Injected(ref ParticleSystem.MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_maxParticles_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_maxParticles_Injected(ref ParticleSystem.MainModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemEmitterVelocityMode get_emitterVelocityMode_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_emitterVelocityMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemEmitterVelocityMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemStopAction get_stopAction_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_stopAction_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemStopAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemRingBufferMode get_ringBufferMode_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ringBufferMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemRingBufferMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_ringBufferLoopRange_Injected(ref ParticleSystem.MainModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ringBufferLoopRange_Injected(ref ParticleSystem.MainModule _unity_self, ref Vector2 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCullingMode get_cullingMode_Injected(ref ParticleSystem.MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_cullingMode_Injected(ref ParticleSystem.MainModule _unity_self, ParticleSystemCullingMode value);
		}

		public struct EmissionModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("ParticleSystemEmissionType no longer does anything. Time and Distance based emission are now both always active.", false)]
			public ParticleSystemEmissionType type
			{
				get
				{
					return ParticleSystemEmissionType.Time;
				}
				set
				{
				}
			}

			[Obsolete("rate property is deprecated. Use rateOverTime or rateOverDistance instead.", false)]
			public ParticleSystem.MinMaxCurve rate
			{
				get
				{
					return this.rateOverTime;
				}
				set
				{
					this.rateOverTime = value;
				}
			}

			[Obsolete("rateMultiplier property is deprecated. Use rateOverTimeMultiplier or rateOverDistanceMultiplier instead.", false)]
			public float rateMultiplier
			{
				get
				{
					return this.rateOverTimeMultiplier;
				}
				set
				{
					this.rateOverTimeMultiplier = value;
				}
			}

			public bool enabled
			{
				get
				{
					return ParticleSystem.EmissionModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve rateOverTime
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.EmissionModule.get_rateOverTime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverTime_Injected(ref this, ref value);
				}
			}

			public float rateOverTimeMultiplier
			{
				get
				{
					return ParticleSystem.EmissionModule.get_rateOverTimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverTimeMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve rateOverDistance
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.EmissionModule.get_rateOverDistance_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverDistance_Injected(ref this, ref value);
				}
			}

			public float rateOverDistanceMultiplier
			{
				get
				{
					return ParticleSystem.EmissionModule.get_rateOverDistanceMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverDistanceMultiplier_Injected(ref this, value);
				}
			}

			public int burstCount
			{
				get
				{
					return ParticleSystem.EmissionModule.get_burstCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_burstCount_Injected(ref this, value);
				}
			}

			internal EmissionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			public void SetBursts(ParticleSystem.Burst[] bursts)
			{
				this.SetBursts(bursts, bursts.Length);
			}

			public void SetBursts(ParticleSystem.Burst[] bursts, int size)
			{
				this.burstCount = size;
				for (int i = 0; i < size; i++)
				{
					this.SetBurst(i, bursts[i]);
				}
			}

			public int GetBursts(ParticleSystem.Burst[] bursts)
			{
				int burstCount = this.burstCount;
				for (int i = 0; i < burstCount; i++)
				{
					bursts[i] = this.GetBurst(i);
				}
				return burstCount;
			}

			[NativeThrows]
			public void SetBurst(int index, ParticleSystem.Burst burst)
			{
				ParticleSystem.EmissionModule.SetBurst_Injected(ref this, index, ref burst);
			}

			[NativeThrows]
			public ParticleSystem.Burst GetBurst(int index)
			{
				ParticleSystem.Burst result;
				ParticleSystem.EmissionModule.GetBurst_Injected(ref this, index, out result);
				return result;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.EmissionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.EmissionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rateOverTime_Injected(ref ParticleSystem.EmissionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverTime_Injected(ref ParticleSystem.EmissionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_rateOverTimeMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverTimeMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rateOverDistance_Injected(ref ParticleSystem.EmissionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverDistance_Injected(ref ParticleSystem.EmissionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_rateOverDistanceMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverDistanceMultiplier_Injected(ref ParticleSystem.EmissionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetBurst_Injected(ref ParticleSystem.EmissionModule _unity_self, int index, ref ParticleSystem.Burst burst);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetBurst_Injected(ref ParticleSystem.EmissionModule _unity_self, int index, out ParticleSystem.Burst ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_burstCount_Injected(ref ParticleSystem.EmissionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_burstCount_Injected(ref ParticleSystem.EmissionModule _unity_self, int value);
		}

		public struct ShapeModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("Please use scale instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/ShapeModule.scale", false)]
			public Vector3 box
			{
				get
				{
					return this.scale;
				}
				set
				{
					this.scale = value;
				}
			}

			[Obsolete("meshScale property is deprecated.Please use scale instead.", false)]
			public float meshScale
			{
				get
				{
					return this.scale.x;
				}
				set
				{
					this.scale = new Vector3(value, value, value);
				}
			}

			[Obsolete("randomDirection property is deprecated. Use randomDirectionAmount instead.", false)]
			public bool randomDirection
			{
				get
				{
					return this.randomDirectionAmount >= 0.5f;
				}
				set
				{
					this.randomDirectionAmount = (value ? 1f : 0f);
				}
			}

			public bool enabled
			{
				get
				{
					return ParticleSystem.ShapeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeType shapeType
			{
				get
				{
					return ParticleSystem.ShapeModule.get_shapeType_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_shapeType_Injected(ref this, value);
				}
			}

			public float randomDirectionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_randomDirectionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_randomDirectionAmount_Injected(ref this, value);
				}
			}

			public float sphericalDirectionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_sphericalDirectionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_sphericalDirectionAmount_Injected(ref this, value);
				}
			}

			public float randomPositionAmount
			{
				get
				{
					return ParticleSystem.ShapeModule.get_randomPositionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_randomPositionAmount_Injected(ref this, value);
				}
			}

			public bool alignToDirection
			{
				get
				{
					return ParticleSystem.ShapeModule.get_alignToDirection_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_alignToDirection_Injected(ref this, value);
				}
			}

			public float radius
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radius_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radius_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeMultiModeValue radiusMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusMode_Injected(ref this, value);
				}
			}

			public float radiusSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpread_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve radiusSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ShapeModule.get_radiusSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpeed_Injected(ref this, ref value);
				}
			}

			public float radiusSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpeedMultiplier_Injected(ref this, value);
				}
			}

			public float radiusThickness
			{
				get
				{
					return ParticleSystem.ShapeModule.get_radiusThickness_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusThickness_Injected(ref this, value);
				}
			}

			public float angle
			{
				get
				{
					return ParticleSystem.ShapeModule.get_angle_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_angle_Injected(ref this, value);
				}
			}

			public float length
			{
				get
				{
					return ParticleSystem.ShapeModule.get_length_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_length_Injected(ref this, value);
				}
			}

			public Vector3 boxThickness
			{
				get
				{
					Vector3 result;
					ParticleSystem.ShapeModule.get_boxThickness_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_boxThickness_Injected(ref this, ref value);
				}
			}

			public ParticleSystemMeshShapeType meshShapeType
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshShapeType_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshShapeType_Injected(ref this, value);
				}
			}

			public Mesh mesh
			{
				get
				{
					return ParticleSystem.ShapeModule.get_mesh_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_mesh_Injected(ref this, value);
				}
			}

			public MeshRenderer meshRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshRenderer_Injected(ref this, value);
				}
			}

			public SkinnedMeshRenderer skinnedMeshRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_skinnedMeshRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_skinnedMeshRenderer_Injected(ref this, value);
				}
			}

			public Sprite sprite
			{
				get
				{
					return ParticleSystem.ShapeModule.get_sprite_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_sprite_Injected(ref this, value);
				}
			}

			public SpriteRenderer spriteRenderer
			{
				get
				{
					return ParticleSystem.ShapeModule.get_spriteRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_spriteRenderer_Injected(ref this, value);
				}
			}

			public bool useMeshMaterialIndex
			{
				get
				{
					return ParticleSystem.ShapeModule.get_useMeshMaterialIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_useMeshMaterialIndex_Injected(ref this, value);
				}
			}

			public int meshMaterialIndex
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshMaterialIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshMaterialIndex_Injected(ref this, value);
				}
			}

			public bool useMeshColors
			{
				get
				{
					return ParticleSystem.ShapeModule.get_useMeshColors_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_useMeshColors_Injected(ref this, value);
				}
			}

			public float normalOffset
			{
				get
				{
					return ParticleSystem.ShapeModule.get_normalOffset_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_normalOffset_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeMultiModeValue meshSpawnMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnMode_Injected(ref this, value);
				}
			}

			public float meshSpawnSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpread_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve meshSpawnSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ShapeModule.get_meshSpawnSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpeed_Injected(ref this, ref value);
				}
			}

			public float meshSpawnSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_meshSpawnSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpeedMultiplier_Injected(ref this, value);
				}
			}

			public float arc
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arc_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arc_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeMultiModeValue arcMode
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcMode_Injected(ref this, value);
				}
			}

			public float arcSpread
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpread_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve arcSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ShapeModule.get_arcSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpeed_Injected(ref this, ref value);
				}
			}

			public float arcSpeedMultiplier
			{
				get
				{
					return ParticleSystem.ShapeModule.get_arcSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpeedMultiplier_Injected(ref this, value);
				}
			}

			public float donutRadius
			{
				get
				{
					return ParticleSystem.ShapeModule.get_donutRadius_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_donutRadius_Injected(ref this, value);
				}
			}

			public Vector3 position
			{
				get
				{
					Vector3 result;
					ParticleSystem.ShapeModule.get_position_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_position_Injected(ref this, ref value);
				}
			}

			public Vector3 rotation
			{
				get
				{
					Vector3 result;
					ParticleSystem.ShapeModule.get_rotation_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_rotation_Injected(ref this, ref value);
				}
			}

			public Vector3 scale
			{
				get
				{
					Vector3 result;
					ParticleSystem.ShapeModule.get_scale_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_scale_Injected(ref this, ref value);
				}
			}

			public Texture2D texture
			{
				get
				{
					return ParticleSystem.ShapeModule.get_texture_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_texture_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeTextureChannel textureClipChannel
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureClipChannel_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureClipChannel_Injected(ref this, value);
				}
			}

			public float textureClipThreshold
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureClipThreshold_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureClipThreshold_Injected(ref this, value);
				}
			}

			public bool textureColorAffectsParticles
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureColorAffectsParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureColorAffectsParticles_Injected(ref this, value);
				}
			}

			public bool textureAlphaAffectsParticles
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureAlphaAffectsParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureAlphaAffectsParticles_Injected(ref this, value);
				}
			}

			public bool textureBilinearFiltering
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureBilinearFiltering_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureBilinearFiltering_Injected(ref this, value);
				}
			}

			public int textureUVChannel
			{
				get
				{
					return ParticleSystem.ShapeModule.get_textureUVChannel_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_textureUVChannel_Injected(ref this, value);
				}
			}

			internal ShapeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeType get_shapeType_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_shapeType_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeType value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_randomDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_randomDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_sphericalDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sphericalDirectionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_randomPositionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_randomPositionAmount_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_alignToDirection_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_alignToDirection_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radius_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radius_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeMultiModeValue get_radiusMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_radiusSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusThickness_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_angle_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_angle_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_length_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_length_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_boxThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_boxThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemMeshShapeType get_meshShapeType_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshShapeType_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemMeshShapeType value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Mesh get_mesh_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mesh_Injected(ref ParticleSystem.ShapeModule _unity_self, Mesh value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern MeshRenderer get_meshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, MeshRenderer value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern SkinnedMeshRenderer get_skinnedMeshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_skinnedMeshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, SkinnedMeshRenderer value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Sprite get_sprite_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sprite_Injected(ref ParticleSystem.ShapeModule _unity_self, Sprite value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern SpriteRenderer get_spriteRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_spriteRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, SpriteRenderer value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useMeshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useMeshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_meshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshMaterialIndex_Injected(ref ParticleSystem.ShapeModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useMeshColors_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useMeshColors_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_normalOffset_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_normalOffset_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeMultiModeValue get_meshSpawnMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_meshSpawnSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_meshSpawnSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_meshSpawnSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_arc_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arc_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeMultiModeValue get_arcMode_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcMode_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_arcSpread_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcSpread_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_arcSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcSpeed_Injected(ref ParticleSystem.ShapeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_arcSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcSpeedMultiplier_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_donutRadius_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_donutRadius_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_position_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_position_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rotation_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rotation_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_scale_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scale_Injected(ref ParticleSystem.ShapeModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Texture2D get_texture_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_texture_Injected(ref ParticleSystem.ShapeModule _unity_self, Texture2D value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemShapeTextureChannel get_textureClipChannel_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureClipChannel_Injected(ref ParticleSystem.ShapeModule _unity_self, ParticleSystemShapeTextureChannel value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_textureClipThreshold_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureClipThreshold_Injected(ref ParticleSystem.ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_textureColorAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureColorAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_textureAlphaAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureAlphaAffectsParticles_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_textureBilinearFiltering_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureBilinearFiltering_Injected(ref ParticleSystem.ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_textureUVChannel_Injected(ref ParticleSystem.ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureUVChannel_Injected(ref ParticleSystem.ShapeModule _unity_self, int value);
		}

		public struct CollisionModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("The maxPlaneCount restriction has been removed. Please use planeCount instead to find out how many planes there are. (UnityUpgradable) -> UnityEngine.ParticleSystem/CollisionModule.planeCount", false)]
			public int maxPlaneCount
			{
				get
				{
					return this.planeCount;
				}
			}

			public bool enabled
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemCollisionType type
			{
				get
				{
					return ParticleSystem.CollisionModule.get_type_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_type_Injected(ref this, value);
				}
			}

			public ParticleSystemCollisionMode mode
			{
				get
				{
					return ParticleSystem.CollisionModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_mode_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve dampen
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.CollisionModule.get_dampen_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_dampen_Injected(ref this, ref value);
				}
			}

			public float dampenMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_dampenMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_dampenMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve bounce
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.CollisionModule.get_bounce_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_bounce_Injected(ref this, ref value);
				}
			}

			public float bounceMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_bounceMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_bounceMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve lifetimeLoss
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.CollisionModule.get_lifetimeLoss_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_lifetimeLoss_Injected(ref this, ref value);
				}
			}

			public float lifetimeLossMultiplier
			{
				get
				{
					return ParticleSystem.CollisionModule.get_lifetimeLossMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_lifetimeLossMultiplier_Injected(ref this, value);
				}
			}

			public float minKillSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_minKillSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_minKillSpeed_Injected(ref this, value);
				}
			}

			public float maxKillSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_maxKillSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_maxKillSpeed_Injected(ref this, value);
				}
			}

			public LayerMask collidesWith
			{
				get
				{
					LayerMask result;
					ParticleSystem.CollisionModule.get_collidesWith_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_collidesWith_Injected(ref this, ref value);
				}
			}

			public bool enableDynamicColliders
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enableDynamicColliders_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enableDynamicColliders_Injected(ref this, value);
				}
			}

			public int maxCollisionShapes
			{
				get
				{
					return ParticleSystem.CollisionModule.get_maxCollisionShapes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_maxCollisionShapes_Injected(ref this, value);
				}
			}

			public ParticleSystemCollisionQuality quality
			{
				get
				{
					return ParticleSystem.CollisionModule.get_quality_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_quality_Injected(ref this, value);
				}
			}

			public float voxelSize
			{
				get
				{
					return ParticleSystem.CollisionModule.get_voxelSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_voxelSize_Injected(ref this, value);
				}
			}

			public float radiusScale
			{
				get
				{
					return ParticleSystem.CollisionModule.get_radiusScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_radiusScale_Injected(ref this, value);
				}
			}

			public bool sendCollisionMessages
			{
				get
				{
					return ParticleSystem.CollisionModule.get_sendCollisionMessages_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_sendCollisionMessages_Injected(ref this, value);
				}
			}

			public float colliderForce
			{
				get
				{
					return ParticleSystem.CollisionModule.get_colliderForce_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_colliderForce_Injected(ref this, value);
				}
			}

			public bool multiplyColliderForceByCollisionAngle
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByCollisionAngle_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByCollisionAngle_Injected(ref this, value);
				}
			}

			public bool multiplyColliderForceByParticleSpeed
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByParticleSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByParticleSpeed_Injected(ref this, value);
				}
			}

			public bool multiplyColliderForceByParticleSize
			{
				get
				{
					return ParticleSystem.CollisionModule.get_multiplyColliderForceByParticleSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_multiplyColliderForceByParticleSize_Injected(ref this, value);
				}
			}

			public int planeCount
			{
				get
				{
					return ParticleSystem.CollisionModule.get_planeCount_Injected(ref this);
				}
			}

			[Obsolete("enableInteriorCollisions property is deprecated and is no longer required and has no effect on the particle system.", false)]
			public bool enableInteriorCollisions
			{
				get
				{
					return ParticleSystem.CollisionModule.get_enableInteriorCollisions_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_enableInteriorCollisions_Injected(ref this, value);
				}
			}

			internal CollisionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			public void AddPlane(Transform transform)
			{
				ParticleSystem.CollisionModule.AddPlane_Injected(ref this, transform);
			}

			public void RemovePlane(int index)
			{
				ParticleSystem.CollisionModule.RemovePlane_Injected(ref this, index);
			}

			public void RemovePlane(Transform transform)
			{
				this.RemovePlaneObject(transform);
			}

			private void RemovePlaneObject(Transform transform)
			{
				ParticleSystem.CollisionModule.RemovePlaneObject_Injected(ref this, transform);
			}

			public void SetPlane(int index, Transform transform)
			{
				ParticleSystem.CollisionModule.SetPlane_Injected(ref this, index, transform);
			}

			public Transform GetPlane(int index)
			{
				return ParticleSystem.CollisionModule.GetPlane_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCollisionType get_type_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_type_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionType value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCollisionMode get_mode_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mode_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_dampen_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dampen_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_dampenMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dampenMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_bounce_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_bounce_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_bounceMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_bounceMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_lifetimeLoss_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetimeLoss_Injected(ref ParticleSystem.CollisionModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_lifetimeLossMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetimeLossMultiplier_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_minKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_minKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_maxKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_maxKillSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_collidesWith_Injected(ref ParticleSystem.CollisionModule _unity_self, out LayerMask ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_collidesWith_Injected(ref ParticleSystem.CollisionModule _unity_self, ref LayerMask value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enableDynamicColliders_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enableDynamicColliders_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_maxCollisionShapes_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_maxCollisionShapes_Injected(ref ParticleSystem.CollisionModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCollisionQuality get_quality_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_quality_Injected(ref ParticleSystem.CollisionModule _unity_self, ParticleSystemCollisionQuality value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_voxelSize_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_voxelSize_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusScale_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusScale_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_sendCollisionMessages_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sendCollisionMessages_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_colliderForce_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colliderForce_Injected(ref ParticleSystem.CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyColliderForceByCollisionAngle_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyColliderForceByCollisionAngle_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyColliderForceByParticleSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyColliderForceByParticleSpeed_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyColliderForceByParticleSize_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyColliderForceByParticleSize_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, Transform transform);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemovePlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemovePlaneObject_Injected(ref ParticleSystem.CollisionModule _unity_self, Transform transform);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index, Transform transform);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Transform GetPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_planeCount_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enableInteriorCollisions_Injected(ref ParticleSystem.CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enableInteriorCollisions_Injected(ref ParticleSystem.CollisionModule _unity_self, bool value);
		}

		public struct TriggerModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("The maxColliderCount restriction has been removed. Please use colliderCount instead to find out how many colliders there are. (UnityUpgradable) -> UnityEngine.ParticleSystem/TriggerModule.colliderCount", false)]
			public int maxColliderCount
			{
				get
				{
					return this.colliderCount;
				}
			}

			public bool enabled
			{
				get
				{
					return ParticleSystem.TriggerModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemOverlapAction inside
			{
				get
				{
					return ParticleSystem.TriggerModule.get_inside_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_inside_Injected(ref this, value);
				}
			}

			public ParticleSystemOverlapAction outside
			{
				get
				{
					return ParticleSystem.TriggerModule.get_outside_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_outside_Injected(ref this, value);
				}
			}

			public ParticleSystemOverlapAction enter
			{
				get
				{
					return ParticleSystem.TriggerModule.get_enter_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_enter_Injected(ref this, value);
				}
			}

			public ParticleSystemOverlapAction exit
			{
				get
				{
					return ParticleSystem.TriggerModule.get_exit_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_exit_Injected(ref this, value);
				}
			}

			public ParticleSystemColliderQueryMode colliderQueryMode
			{
				get
				{
					return ParticleSystem.TriggerModule.get_colliderQueryMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_colliderQueryMode_Injected(ref this, value);
				}
			}

			public float radiusScale
			{
				get
				{
					return ParticleSystem.TriggerModule.get_radiusScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TriggerModule.set_radiusScale_Injected(ref this, value);
				}
			}

			public int colliderCount
			{
				get
				{
					return ParticleSystem.TriggerModule.get_colliderCount_Injected(ref this);
				}
			}

			internal TriggerModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void AddCollider(Component collider)
			{
				ParticleSystem.TriggerModule.AddCollider_Injected(ref this, collider);
			}

			[NativeThrows]
			public void RemoveCollider(int index)
			{
				ParticleSystem.TriggerModule.RemoveCollider_Injected(ref this, index);
			}

			public void RemoveCollider(Component collider)
			{
				this.RemoveColliderObject(collider);
			}

			[NativeThrows]
			private void RemoveColliderObject(Component collider)
			{
				ParticleSystem.TriggerModule.RemoveColliderObject_Injected(ref this, collider);
			}

			[NativeThrows]
			public void SetCollider(int index, Component collider)
			{
				ParticleSystem.TriggerModule.SetCollider_Injected(ref this, index, collider);
			}

			[NativeThrows]
			public Component GetCollider(int index)
			{
				return ParticleSystem.TriggerModule.GetCollider_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TriggerModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemOverlapAction get_inside_Injected(ref ParticleSystem.TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_inside_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemOverlapAction get_outside_Injected(ref ParticleSystem.TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_outside_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemOverlapAction get_enter_Injected(ref ParticleSystem.TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enter_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemOverlapAction get_exit_Injected(ref ParticleSystem.TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_exit_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemOverlapAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemColliderQueryMode get_colliderQueryMode_Injected(ref ParticleSystem.TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colliderQueryMode_Injected(ref ParticleSystem.TriggerModule _unity_self, ParticleSystemColliderQueryMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radiusScale_Injected(ref ParticleSystem.TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusScale_Injected(ref ParticleSystem.TriggerModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, Component collider);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveColliderObject_Injected(ref ParticleSystem.TriggerModule _unity_self, Component collider);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index, Component collider);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Component GetCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_colliderCount_Injected(ref ParticleSystem.TriggerModule _unity_self);
		}

		public struct SubEmittersModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("birth0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			[Obsolete("birth1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			[Obsolete("collision0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			[Obsolete("collision1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			[Obsolete("death0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			[Obsolete("death1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			public bool enabled
			{
				get
				{
					return ParticleSystem.SubEmittersModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SubEmittersModule.set_enabled_Injected(ref this, value);
				}
			}

			public int subEmittersCount
			{
				get
				{
					return ParticleSystem.SubEmittersModule.get_subEmittersCount_Injected(ref this);
				}
			}

			private static void ThrowNotImplemented()
			{
				throw new NotImplementedException();
			}

			internal SubEmittersModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability)
			{
				ParticleSystem.SubEmittersModule.AddSubEmitter_Injected(ref this, subEmitter, type, properties, emitProbability);
			}

			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties)
			{
				this.AddSubEmitter(subEmitter, type, properties, 1f);
			}

			[NativeThrows]
			public void RemoveSubEmitter(int index)
			{
				ParticleSystem.SubEmittersModule.RemoveSubEmitter_Injected(ref this, index);
			}

			public void RemoveSubEmitter(ParticleSystem subEmitter)
			{
				this.RemoveSubEmitterObject(subEmitter);
			}

			[NativeThrows]
			private void RemoveSubEmitterObject(ParticleSystem subEmitter)
			{
				ParticleSystem.SubEmittersModule.RemoveSubEmitterObject_Injected(ref this, subEmitter);
			}

			[NativeThrows]
			public void SetSubEmitterSystem(int index, ParticleSystem subEmitter)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterSystem_Injected(ref this, index, subEmitter);
			}

			[NativeThrows]
			public void SetSubEmitterType(int index, ParticleSystemSubEmitterType type)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterType_Injected(ref this, index, type);
			}

			[NativeThrows]
			public void SetSubEmitterProperties(int index, ParticleSystemSubEmitterProperties properties)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterProperties_Injected(ref this, index, properties);
			}

			[NativeThrows]
			public void SetSubEmitterEmitProbability(int index, float emitProbability)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterEmitProbability_Injected(ref this, index, emitProbability);
			}

			[NativeThrows]
			public ParticleSystem GetSubEmitterSystem(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterSystem_Injected(ref this, index);
			}

			[NativeThrows]
			public ParticleSystemSubEmitterType GetSubEmitterType(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterType_Injected(ref this, index);
			}

			[NativeThrows]
			public ParticleSystemSubEmitterProperties GetSubEmitterProperties(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterProperties_Injected(ref this, index);
			}

			[NativeThrows]
			public float GetSubEmitterEmitProbability(int index)
			{
				return ParticleSystem.SubEmittersModule.GetSubEmitterEmitProbability_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SubEmittersModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SubEmittersModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_subEmittersCount_Injected(ref ParticleSystem.SubEmittersModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddSubEmitter_Injected(ref ParticleSystem.SubEmittersModule _unity_self, ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSubEmitter_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSubEmitterObject_Injected(ref ParticleSystem.SubEmittersModule _unity_self, ParticleSystem subEmitter);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystem subEmitter);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterType_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystemSubEmitterType type);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterProperties_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, ParticleSystemSubEmitterProperties properties);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterEmitProbability_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, float emitProbability);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystem GetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSubEmitterType GetSubEmitterType_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSubEmitterProperties GetSubEmitterProperties_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetSubEmitterEmitProbability_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);
		}

		public struct TextureSheetAnimationModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("flipU property is deprecated. Use ParticleSystemRenderer.flip.x instead.", false)]
			public float flipU
			{
				get
				{
					return this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.x;
				}
				set
				{
					ParticleSystemRenderer component = this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.x = value;
					component.flip = flip;
				}
			}

			[Obsolete("flipV property is deprecated. Use ParticleSystemRenderer.flip.y instead.", false)]
			public float flipV
			{
				get
				{
					return this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.y;
				}
				set
				{
					ParticleSystemRenderer component = this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.y = value;
					component.flip = flip;
				}
			}

			[Obsolete("useRandomRow property is deprecated. Use rowMode instead.", false)]
			public bool useRandomRow
			{
				get
				{
					return this.rowMode == ParticleSystemAnimationRowMode.Random;
				}
				set
				{
					this.rowMode = (value ? ParticleSystemAnimationRowMode.Random : ParticleSystemAnimationRowMode.Custom);
				}
			}

			public bool enabled
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemAnimationMode mode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_mode_Injected(ref this, value);
				}
			}

			public ParticleSystemAnimationTimeMode timeMode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_timeMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_timeMode_Injected(ref this, value);
				}
			}

			public float fps
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_fps_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_fps_Injected(ref this, value);
				}
			}

			public int numTilesX
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_numTilesX_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_numTilesX_Injected(ref this, value);
				}
			}

			public int numTilesY
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_numTilesY_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_numTilesY_Injected(ref this, value);
				}
			}

			public ParticleSystemAnimationType animation
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_animation_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_animation_Injected(ref this, value);
				}
			}

			public ParticleSystemAnimationRowMode rowMode
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_rowMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_rowMode_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve frameOverTime
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.TextureSheetAnimationModule.get_frameOverTime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_frameOverTime_Injected(ref this, ref value);
				}
			}

			public float frameOverTimeMultiplier
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_frameOverTimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_frameOverTimeMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve startFrame
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.TextureSheetAnimationModule.get_startFrame_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_startFrame_Injected(ref this, ref value);
				}
			}

			public float startFrameMultiplier
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_startFrameMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_startFrameMultiplier_Injected(ref this, value);
				}
			}

			public int cycleCount
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_cycleCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_cycleCount_Injected(ref this, value);
				}
			}

			public int rowIndex
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_rowIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_rowIndex_Injected(ref this, value);
				}
			}

			public UVChannelFlags uvChannelMask
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_uvChannelMask_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_uvChannelMask_Injected(ref this, value);
				}
			}

			public int spriteCount
			{
				get
				{
					return ParticleSystem.TextureSheetAnimationModule.get_spriteCount_Injected(ref this);
				}
			}

			public Vector2 speedRange
			{
				get
				{
					Vector2 result;
					ParticleSystem.TextureSheetAnimationModule.get_speedRange_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_speedRange_Injected(ref this, ref value);
				}
			}

			internal TextureSheetAnimationModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void AddSprite(Sprite sprite)
			{
				ParticleSystem.TextureSheetAnimationModule.AddSprite_Injected(ref this, sprite);
			}

			[NativeThrows]
			public void RemoveSprite(int index)
			{
				ParticleSystem.TextureSheetAnimationModule.RemoveSprite_Injected(ref this, index);
			}

			[NativeThrows]
			public void SetSprite(int index, Sprite sprite)
			{
				ParticleSystem.TextureSheetAnimationModule.SetSprite_Injected(ref this, index, sprite);
			}

			[NativeThrows]
			public Sprite GetSprite(int index)
			{
				return ParticleSystem.TextureSheetAnimationModule.GetSprite_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemAnimationMode get_mode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemAnimationTimeMode get_timeMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_timeMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationTimeMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_fps_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_fps_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_numTilesX_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_numTilesX_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_numTilesY_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_numTilesY_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemAnimationType get_animation_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_animation_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationType value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemAnimationRowMode get_rowMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rowMode_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ParticleSystemAnimationRowMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_frameOverTime_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_frameOverTime_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_frameOverTimeMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_frameOverTimeMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startFrame_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startFrame_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_startFrameMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startFrameMultiplier_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_cycleCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_cycleCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_rowIndex_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rowIndex_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern UVChannelFlags get_uvChannelMask_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_uvChannelMask_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, UVChannelFlags value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_spriteCount_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_speedRange_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_speedRange_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, ref Vector2 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, Sprite sprite);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index, Sprite sprite);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Sprite GetSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index);
		}

		[RequiredByNativeCode("particleSystemParticle", Optional = true)]
		public struct Particle
		{
			[Flags]
			private enum Flags
			{
				Size3D = 1,
				Rotation3D = 2,
				MeshIndex = 4
			}

			private Vector3 m_Position;

			private Vector3 m_Velocity;

			private Vector3 m_AnimatedVelocity;

			private Vector3 m_InitialVelocity;

			private Vector3 m_AxisOfRotation;

			private Vector3 m_Rotation;

			private Vector3 m_AngularVelocity;

			private Vector3 m_StartSize;

			private Color32 m_StartColor;

			private uint m_RandomSeed;

			private uint m_ParentRandomSeed;

			private float m_Lifetime;

			private float m_StartLifetime;

			private int m_MeshIndex;

			private float m_EmitAccumulator0;

			private float m_EmitAccumulator1;

			private uint m_Flags;

			[Obsolete("Please use Particle.remainingLifetime instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/Particle.remainingLifetime", false)]
			public float lifetime
			{
				get
				{
					return this.remainingLifetime;
				}
				set
				{
					this.remainingLifetime = value;
				}
			}

			[Obsolete("randomValue property is deprecated. Use randomSeed instead to control random behavior of particles.", false)]
			public float randomValue
			{
				get
				{
					return BitConverter.ToSingle(BitConverter.GetBytes(this.m_RandomSeed), 0);
				}
				set
				{
					this.m_RandomSeed = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
				}
			}

			[Obsolete("size property is deprecated. Use startSize or GetCurrentSize() instead.", false)]
			public float size
			{
				get
				{
					return this.startSize;
				}
				set
				{
					this.startSize = value;
				}
			}

			[Obsolete("color property is deprecated. Use startColor or GetCurrentColor() instead.", false)]
			public Color32 color
			{
				get
				{
					return this.startColor;
				}
				set
				{
					this.startColor = value;
				}
			}

			public Vector3 position
			{
				get
				{
					return this.m_Position;
				}
				set
				{
					this.m_Position = value;
				}
			}

			public Vector3 velocity
			{
				get
				{
					return this.m_Velocity;
				}
				set
				{
					this.m_Velocity = value;
				}
			}

			public Vector3 animatedVelocity
			{
				get
				{
					return this.m_AnimatedVelocity;
				}
			}

			public Vector3 totalVelocity
			{
				get
				{
					return this.m_Velocity + this.m_AnimatedVelocity;
				}
			}

			public float remainingLifetime
			{
				get
				{
					return this.m_Lifetime;
				}
				set
				{
					this.m_Lifetime = value;
				}
			}

			public float startLifetime
			{
				get
				{
					return this.m_StartLifetime;
				}
				set
				{
					this.m_StartLifetime = value;
				}
			}

			public Color32 startColor
			{
				get
				{
					return this.m_StartColor;
				}
				set
				{
					this.m_StartColor = value;
				}
			}

			public uint randomSeed
			{
				get
				{
					return this.m_RandomSeed;
				}
				set
				{
					this.m_RandomSeed = value;
				}
			}

			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_AxisOfRotation;
				}
				set
				{
					this.m_AxisOfRotation = value;
				}
			}

			public float startSize
			{
				get
				{
					return this.m_StartSize.x;
				}
				set
				{
					this.m_StartSize = new Vector3(value, value, value);
				}
			}

			public Vector3 startSize3D
			{
				get
				{
					return this.m_StartSize;
				}
				set
				{
					this.m_StartSize = value;
					this.m_Flags |= 1u;
				}
			}

			public float rotation
			{
				get
				{
					return this.m_Rotation.z * 57.29578f;
				}
				set
				{
					this.m_Rotation = new Vector3(0f, 0f, value * 0.0174532924f);
				}
			}

			public Vector3 rotation3D
			{
				get
				{
					return this.m_Rotation * 57.29578f;
				}
				set
				{
					this.m_Rotation = value * 0.0174532924f;
					this.m_Flags |= 2u;
				}
			}

			public float angularVelocity
			{
				get
				{
					return this.m_AngularVelocity.z * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = new Vector3(0f, 0f, value * 0.0174532924f);
				}
			}

			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_AngularVelocity * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = value * 0.0174532924f;
					this.m_Flags |= 2u;
				}
			}

			public float GetCurrentSize(ParticleSystem system)
			{
				return system.GetParticleCurrentSize(ref this);
			}

			public Vector3 GetCurrentSize3D(ParticleSystem system)
			{
				return system.GetParticleCurrentSize3D(ref this);
			}

			public Color32 GetCurrentColor(ParticleSystem system)
			{
				return system.GetParticleCurrentColor(ref this);
			}

			public void SetMeshIndex(int index)
			{
				this.m_MeshIndex = index;
				this.m_Flags |= 4u;
			}

			public int GetMeshIndex(ParticleSystem system)
			{
				return system.GetParticleMeshIndex(ref this);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("ParticleSystem.CollisionEvent has been deprecated. Use ParticleCollisionEvent instead (UnityUpgradable)", true)]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct CollisionEvent
		{
			public Vector3 intersection
			{
				get
				{
					return default(Vector3);
				}
			}

			public Vector3 normal
			{
				get
				{
					return default(Vector3);
				}
			}

			public Vector3 velocity
			{
				get
				{
					return default(Vector3);
				}
			}

			public Component collider
			{
				get
				{
					return null;
				}
			}
		}

		[NativeType(CodegenOptions.Custom, "MonoBurst", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		public struct Burst
		{
			private float m_Time;

			private ParticleSystem.MinMaxCurve m_Count;

			private int m_RepeatCount;

			private float m_RepeatInterval;

			private float m_InvProbability;

			public float time
			{
				get
				{
					return this.m_Time;
				}
				set
				{
					this.m_Time = value;
				}
			}

			public ParticleSystem.MinMaxCurve count
			{
				get
				{
					return this.m_Count;
				}
				set
				{
					this.m_Count = value;
				}
			}

			public short minCount
			{
				get
				{
					return (short)this.m_Count.constantMin;
				}
				set
				{
					this.m_Count.constantMin = (float)value;
				}
			}

			public short maxCount
			{
				get
				{
					return (short)this.m_Count.constantMax;
				}
				set
				{
					this.m_Count.constantMax = (float)value;
				}
			}

			public int cycleCount
			{
				get
				{
					return this.m_RepeatCount + 1;
				}
				set
				{
					bool flag = value < 0;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("cycleCount", "cycleCount must be at least 0: " + value.ToString());
					}
					this.m_RepeatCount = value - 1;
				}
			}

			public float repeatInterval
			{
				get
				{
					return this.m_RepeatInterval;
				}
				set
				{
					bool flag = value <= 0f;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("repeatInterval", "repeatInterval must be greater than 0.0f: " + value.ToString());
					}
					this.m_RepeatInterval = value;
				}
			}

			public float probability
			{
				get
				{
					return 1f - this.m_InvProbability;
				}
				set
				{
					bool flag = value < 0f || value > 1f;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("probability", "probability must be between 0.0f and 1.0f: " + value.ToString());
					}
					this.m_InvProbability = 1f - value;
				}
			}

			public Burst(float _time, short _count)
			{
				this.m_Time = _time;
				this.m_Count = (float)_count;
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			public Burst(float _time, short _minCount, short _maxCount)
			{
				this.m_Time = _time;
				this.m_Count = new ParticleSystem.MinMaxCurve((float)_minCount, (float)_maxCount);
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			public Burst(float _time, short _minCount, short _maxCount, int _cycleCount, float _repeatInterval)
			{
				this.m_Time = _time;
				this.m_Count = new ParticleSystem.MinMaxCurve((float)_minCount, (float)_maxCount);
				this.m_RepeatCount = _cycleCount - 1;
				this.m_RepeatInterval = _repeatInterval;
				this.m_InvProbability = 0f;
			}

			public Burst(float _time, ParticleSystem.MinMaxCurve _count)
			{
				this.m_Time = _time;
				this.m_Count = _count;
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			public Burst(float _time, ParticleSystem.MinMaxCurve _count, int _cycleCount, float _repeatInterval)
			{
				this.m_Time = _time;
				this.m_Count = _count;
				this.m_RepeatCount = _cycleCount - 1;
				this.m_RepeatInterval = _repeatInterval;
				this.m_InvProbability = 0f;
			}
		}

		[NativeType(CodegenOptions.Custom, "MonoMinMaxGradient", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		public struct MinMaxGradient
		{
			[SerializeField]
			private ParticleSystemGradientMode m_Mode;

			[SerializeField]
			private Gradient m_GradientMin;

			[SerializeField]
			private Gradient m_GradientMax;

			[SerializeField]
			private Color m_ColorMin;

			[SerializeField]
			private Color m_ColorMax;

			public ParticleSystemGradientMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			public Gradient gradientMax
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			public Gradient gradientMin
			{
				get
				{
					return this.m_GradientMin;
				}
				set
				{
					this.m_GradientMin = value;
				}
			}

			public Color colorMax
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			public Color colorMin
			{
				get
				{
					return this.m_ColorMin;
				}
				set
				{
					this.m_ColorMin = value;
				}
			}

			public Color color
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			public Gradient gradient
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			public MinMaxGradient(Color color)
			{
				this.m_Mode = ParticleSystemGradientMode.Color;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = color;
			}

			public MinMaxGradient(Gradient gradient)
			{
				this.m_Mode = ParticleSystemGradientMode.Gradient;
				this.m_GradientMin = null;
				this.m_GradientMax = gradient;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			public MinMaxGradient(Color min, Color max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoColors;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = min;
				this.m_ColorMax = max;
			}

			public MinMaxGradient(Gradient min, Gradient max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoGradients;
				this.m_GradientMin = min;
				this.m_GradientMax = max;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			public Color Evaluate(float time)
			{
				return this.Evaluate(time, 1f);
			}

			public Color Evaluate(float time, float lerpFactor)
			{
				Color result;
				switch (this.m_Mode)
				{
				case ParticleSystemGradientMode.Color:
					result = this.m_ColorMax;
					return result;
				case ParticleSystemGradientMode.TwoColors:
					result = Color.Lerp(this.m_ColorMin, this.m_ColorMax, lerpFactor);
					return result;
				case ParticleSystemGradientMode.TwoGradients:
					result = Color.Lerp(this.m_GradientMin.Evaluate(time), this.m_GradientMax.Evaluate(time), lerpFactor);
					return result;
				case ParticleSystemGradientMode.RandomColor:
					result = this.m_GradientMax.Evaluate(lerpFactor);
					return result;
				}
				result = this.m_GradientMax.Evaluate(time);
				return result;
			}

			public static implicit operator ParticleSystem.MinMaxGradient(Color color)
			{
				return new ParticleSystem.MinMaxGradient(color);
			}

			public static implicit operator ParticleSystem.MinMaxGradient(Gradient gradient)
			{
				return new ParticleSystem.MinMaxGradient(gradient);
			}
		}

		public struct EmitParams
		{
			[NativeName("particle")]
			private ParticleSystem.Particle m_Particle;

			[NativeName("positionSet")]
			private bool m_PositionSet;

			[NativeName("velocitySet")]
			private bool m_VelocitySet;

			[NativeName("axisOfRotationSet")]
			private bool m_AxisOfRotationSet;

			[NativeName("rotationSet")]
			private bool m_RotationSet;

			[NativeName("rotationalSpeedSet")]
			private bool m_AngularVelocitySet;

			[NativeName("startSizeSet")]
			private bool m_StartSizeSet;

			[NativeName("startColorSet")]
			private bool m_StartColorSet;

			[NativeName("randomSeedSet")]
			private bool m_RandomSeedSet;

			[NativeName("startLifetimeSet")]
			private bool m_StartLifetimeSet;

			[NativeName("meshIndexSet")]
			private bool m_MeshIndexSet;

			[NativeName("applyShapeToPosition")]
			private bool m_ApplyShapeToPosition;

			public ParticleSystem.Particle particle
			{
				get
				{
					return this.m_Particle;
				}
				set
				{
					this.m_Particle = value;
					this.m_PositionSet = true;
					this.m_VelocitySet = true;
					this.m_AxisOfRotationSet = true;
					this.m_RotationSet = true;
					this.m_AngularVelocitySet = true;
					this.m_StartSizeSet = true;
					this.m_StartColorSet = true;
					this.m_RandomSeedSet = true;
					this.m_StartLifetimeSet = true;
					this.m_MeshIndexSet = true;
				}
			}

			public Vector3 position
			{
				get
				{
					return this.m_Particle.position;
				}
				set
				{
					this.m_Particle.position = value;
					this.m_PositionSet = true;
				}
			}

			public bool applyShapeToPosition
			{
				get
				{
					return this.m_ApplyShapeToPosition;
				}
				set
				{
					this.m_ApplyShapeToPosition = value;
				}
			}

			public Vector3 velocity
			{
				get
				{
					return this.m_Particle.velocity;
				}
				set
				{
					this.m_Particle.velocity = value;
					this.m_VelocitySet = true;
				}
			}

			public float startLifetime
			{
				get
				{
					return this.m_Particle.startLifetime;
				}
				set
				{
					this.m_Particle.startLifetime = value;
					this.m_StartLifetimeSet = true;
				}
			}

			public float startSize
			{
				get
				{
					return this.m_Particle.startSize;
				}
				set
				{
					this.m_Particle.startSize = value;
					this.m_StartSizeSet = true;
				}
			}

			public Vector3 startSize3D
			{
				get
				{
					return this.m_Particle.startSize3D;
				}
				set
				{
					this.m_Particle.startSize3D = value;
					this.m_StartSizeSet = true;
				}
			}

			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_Particle.axisOfRotation;
				}
				set
				{
					this.m_Particle.axisOfRotation = value;
					this.m_AxisOfRotationSet = true;
				}
			}

			public float rotation
			{
				get
				{
					return this.m_Particle.rotation;
				}
				set
				{
					this.m_Particle.rotation = value;
					this.m_RotationSet = true;
				}
			}

			public Vector3 rotation3D
			{
				get
				{
					return this.m_Particle.rotation3D;
				}
				set
				{
					this.m_Particle.rotation3D = value;
					this.m_RotationSet = true;
				}
			}

			public float angularVelocity
			{
				get
				{
					return this.m_Particle.angularVelocity;
				}
				set
				{
					this.m_Particle.angularVelocity = value;
					this.m_AngularVelocitySet = true;
				}
			}

			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_Particle.angularVelocity3D;
				}
				set
				{
					this.m_Particle.angularVelocity3D = value;
					this.m_AngularVelocitySet = true;
				}
			}

			public Color32 startColor
			{
				get
				{
					return this.m_Particle.startColor;
				}
				set
				{
					this.m_Particle.startColor = value;
					this.m_StartColorSet = true;
				}
			}

			public uint randomSeed
			{
				get
				{
					return this.m_Particle.randomSeed;
				}
				set
				{
					this.m_Particle.randomSeed = value;
					this.m_RandomSeedSet = true;
				}
			}

			public int meshIndex
			{
				set
				{
					this.m_Particle.SetMeshIndex(value);
					this.m_MeshIndexSet = true;
				}
			}

			public void ResetPosition()
			{
				this.m_PositionSet = false;
			}

			public void ResetVelocity()
			{
				this.m_VelocitySet = false;
			}

			public void ResetAxisOfRotation()
			{
				this.m_AxisOfRotationSet = false;
			}

			public void ResetRotation()
			{
				this.m_RotationSet = false;
			}

			public void ResetAngularVelocity()
			{
				this.m_AngularVelocitySet = false;
			}

			public void ResetStartSize()
			{
				this.m_StartSizeSet = false;
			}

			public void ResetStartColor()
			{
				this.m_StartColorSet = false;
			}

			public void ResetRandomSeed()
			{
				this.m_RandomSeedSet = false;
			}

			public void ResetStartLifetime()
			{
				this.m_StartLifetimeSet = false;
			}

			public void ResetMeshIndex()
			{
				this.m_MeshIndexSet = false;
			}
		}

		public struct PlaybackState
		{
			internal struct Seed
			{
				public uint x;

				public uint y;

				public uint z;

				public uint w;
			}

			internal struct Seed4
			{
				public ParticleSystem.PlaybackState.Seed x;

				public ParticleSystem.PlaybackState.Seed y;

				public ParticleSystem.PlaybackState.Seed z;

				public ParticleSystem.PlaybackState.Seed w;
			}

			internal struct Emission
			{
				public float m_ParticleSpacing;

				public float m_ToEmitAccumulator;

				public ParticleSystem.PlaybackState.Seed m_Random;
			}

			internal struct Initial
			{
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			internal struct Shape
			{
				public ParticleSystem.PlaybackState.Seed4 m_Random;

				public float m_RadiusTimer;

				public float m_RadiusTimerPrev;

				public float m_ArcTimer;

				public float m_ArcTimerPrev;

				public float m_MeshSpawnTimer;

				public float m_MeshSpawnTimerPrev;

				public int m_OrderedMeshVertexIndex;
			}

			internal struct Force
			{
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			internal struct Collision
			{
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			internal struct Noise
			{
				public float m_ScrollOffset;
			}

			internal struct Lights
			{
				public ParticleSystem.PlaybackState.Seed m_Random;

				public float m_ParticleEmissionCounter;
			}

			internal struct Trail
			{
				public float m_Timer;
			}

			internal float m_AccumulatedDt;

			internal float m_StartDelay;

			internal float m_PlaybackTime;

			internal int m_RingBufferIndex;

			internal ParticleSystem.PlaybackState.Emission m_Emission;

			internal ParticleSystem.PlaybackState.Initial m_Initial;

			internal ParticleSystem.PlaybackState.Shape m_Shape;

			internal ParticleSystem.PlaybackState.Force m_Force;

			internal ParticleSystem.PlaybackState.Collision m_Collision;

			internal ParticleSystem.PlaybackState.Noise m_Noise;

			internal ParticleSystem.PlaybackState.Lights m_Lights;

			internal ParticleSystem.PlaybackState.Trail m_Trail;
		}

		[NativeType(CodegenOptions.Custom, "MonoParticleTrails")]
		public struct Trails
		{
			internal List<Vector4> positions;

			internal List<int> frontPositions;

			internal List<int> backPositions;

			internal List<int> positionCounts;

			internal int maxTrailCount;

			internal int maxPositionsPerTrailCount;
		}

		public struct ColliderData
		{
			internal Component[] colliders;

			internal int[] colliderIndices;

			internal int[] particleStartIndices;

			public int GetColliderCount(int particleIndex)
			{
				bool flag = particleIndex < this.particleStartIndices.Length - 1;
				int result;
				if (flag)
				{
					result = this.particleStartIndices[particleIndex + 1] - this.particleStartIndices[particleIndex];
				}
				else
				{
					result = this.colliderIndices.Length - this.particleStartIndices[particleIndex];
				}
				return result;
			}

			public Component GetCollider(int particleIndex, int colliderIndex)
			{
				bool flag = colliderIndex >= this.GetColliderCount(particleIndex);
				if (flag)
				{
					throw new IndexOutOfRangeException("colliderIndex exceeded the total number of colliders for the requested particle");
				}
				int num = this.particleStartIndices[particleIndex] + colliderIndex;
				return this.colliders[this.colliderIndices[num]];
			}
		}

		public struct VelocityOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve orbitalX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalX_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve orbitalY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalY_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve orbitalZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalZ_Injected(ref this, ref value);
				}
			}

			public float orbitalXMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalXMultiplier_Injected(ref this, value);
				}
			}

			public float orbitalYMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalYMultiplier_Injected(ref this, value);
				}
			}

			public float orbitalZMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalZMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve orbitalOffsetX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetX_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve orbitalOffsetY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetY_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve orbitalOffsetZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetZ_Injected(ref this, ref value);
				}
			}

			public float orbitalOffsetXMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetXMultiplier_Injected(ref this, value);
				}
			}

			public float orbitalOffsetYMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetYMultiplier_Injected(ref this, value);
				}
			}

			public float orbitalOffsetZMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetZMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve radial
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_radial_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_radial_Injected(ref this, ref value);
				}
			}

			public float radialMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_radialMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_radialMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve speedModifier
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.VelocityOverLifetimeModule.get_speedModifier_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_speedModifier_Injected(ref this, ref value);
				}
			}

			public float speedModifierMultiplier
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_speedModifierMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_speedModifierMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.VelocityOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			internal VelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetX_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetY_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetZ_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalOffsetXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetXMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalOffsetYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetYMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_orbitalOffsetZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetZMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_radial_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radial_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_radialMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radialMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_speedModifier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_speedModifier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_speedModifierMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_speedModifierMultiplier_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_space_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);
		}

		public struct LimitVelocityOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve limitX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitX_Injected(ref this, ref value);
				}
			}

			public float limitXMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitXMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve limitY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitY_Injected(ref this, ref value);
				}
			}

			public float limitYMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitYMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve limitZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitZ_Injected(ref this, ref value);
				}
			}

			public float limitZMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitZMultiplier_Injected(ref this, value);
				}
			}

			[NativeName("Magnitude")]
			public ParticleSystem.MinMaxCurve limit
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limit_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limit_Injected(ref this, ref value);
				}
			}

			[NativeName("MagnitudeMultiplier")]
			public float limitMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_limitMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitMultiplier_Injected(ref this, value);
				}
			}

			public float dampen
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_dampen_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_dampen_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve drag
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_drag_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_drag_Injected(ref this, ref value);
				}
			}

			public float dragMultiplier
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_dragMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_dragMultiplier_Injected(ref this, value);
				}
			}

			public bool multiplyDragByParticleSize
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_multiplyDragByParticleSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_multiplyDragByParticleSize_Injected(ref this, value);
				}
			}

			public bool multiplyDragByParticleVelocity
			{
				get
				{
					return ParticleSystem.LimitVelocityOverLifetimeModule.get_multiplyDragByParticleVelocity_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_multiplyDragByParticleVelocity_Injected(ref this, value);
				}
			}

			internal LimitVelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitX_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitX_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_limitXMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitXMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitY_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitY_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_limitYMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitYMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitZ_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitZ_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_limitZMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitZMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limit_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limit_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_limitMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_dampen_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dampen_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_space_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_drag_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_drag_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_dragMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dragMultiplier_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyDragByParticleSize_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyDragByParticleSize_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_multiplyDragByParticleVelocity_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplyDragByParticleVelocity_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, bool value);
		}

		public struct InheritVelocityModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemInheritVelocityMode mode
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_mode_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.InheritVelocityModule.get_curve_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_curve_Injected(ref this, ref value);
				}
			}

			public float curveMultiplier
			{
				get
				{
					return ParticleSystem.InheritVelocityModule.get_curveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_curveMultiplier_Injected(ref this, value);
				}
			}

			internal InheritVelocityModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemInheritVelocityMode get_mode_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mode_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, ParticleSystemInheritVelocityMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_curve_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curve_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_curveMultiplier_Injected(ref ParticleSystem.InheritVelocityModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curveMultiplier_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, float value);
		}

		public struct LifetimeByEmitterSpeedModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.LifetimeByEmitterSpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LifetimeByEmitterSpeedModule.get_curve_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_curve_Injected(ref this, ref value);
				}
			}

			public float curveMultiplier
			{
				get
				{
					return ParticleSystem.LifetimeByEmitterSpeedModule.get_curveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_curveMultiplier_Injected(ref this, value);
				}
			}

			public Vector2 range
			{
				get
				{
					Vector2 result;
					ParticleSystem.LifetimeByEmitterSpeedModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			internal LifetimeByEmitterSpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_curve_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curve_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_curveMultiplier_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curveMultiplier_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, ref Vector2 value);
		}

		public struct ForceOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ForceOverLifetimeModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ForceOverLifetimeModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ForceOverLifetimeModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystemSimulationSpace space
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_space_Injected(ref this, value);
				}
			}

			public bool randomized
			{
				get
				{
					return ParticleSystem.ForceOverLifetimeModule.get_randomized_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_randomized_Injected(ref this, value);
				}
			}

			internal ForceOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_space_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_randomized_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_randomized_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, bool value);
		}

		public struct ColorOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.ColorOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.ColorOverLifetimeModule.get_color_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorOverLifetimeModule.set_color_Injected(ref this, ref value);
				}
			}

			internal ColorOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_color_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_color_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxGradient value);
		}

		public struct ColorBySpeedModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.ColorBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.ColorBySpeedModule.get_color_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_color_Injected(ref this, ref value);
				}
			}

			public Vector2 range
			{
				get
				{
					Vector2 result;
					ParticleSystem.ColorBySpeedModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			internal ColorBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_color_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_color_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, ref Vector2 value);
		}

		public struct SizeOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			[NativeName("X")]
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeOverLifetimeModule.get_size_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_size_Injected(ref this, ref value);
				}
			}

			[NativeName("XMultiplier")]
			public float sizeMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_sizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_sizeMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeOverLifetimeModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeOverLifetimeModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeOverLifetimeModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return ParticleSystem.SizeOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			internal SizeOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_size_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_size_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_sizeMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, bool value);
		}

		public struct SizeBySpeedModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			[NativeName("X")]
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeBySpeedModule.get_size_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_size_Injected(ref this, ref value);
				}
			}

			[NativeName("XMultiplier")]
			public float sizeMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_sizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_sizeMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeBySpeedModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_x_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeBySpeedModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_y_Injected(ref this, ref value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.SizeBySpeedModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_z_Injected(ref this, ref value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return ParticleSystem.SizeBySpeedModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_separateAxes_Injected(ref this, value);
				}
			}

			public Vector2 range
			{
				get
				{
					Vector2 result;
					ParticleSystem.SizeBySpeedModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			internal SizeBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_size_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_size_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_sizeMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, ref Vector2 value);
		}

		public struct RotationOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationOverLifetimeModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_x_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationOverLifetimeModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_y_Injected(ref this, ref value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationOverLifetimeModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_z_Injected(ref this, ref value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return ParticleSystem.RotationOverLifetimeModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_separateAxes_Injected(ref this, value);
				}
			}

			internal RotationOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, bool value);
		}

		public struct RotationBySpeedModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationBySpeedModule.get_x_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_x_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_xMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationBySpeedModule.get_y_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_y_Injected(ref this, ref value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_yMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.RotationBySpeedModule.get_z_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_z_Injected(ref this, ref value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_zMultiplier_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return ParticleSystem.RotationBySpeedModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_separateAxes_Injected(ref this, value);
				}
			}

			public Vector2 range
			{
				get
				{
					Vector2 result;
					ParticleSystem.RotationBySpeedModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			internal RotationBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_x_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_x_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_xMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_y_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_y_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_yMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_z_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_z_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_zMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zMultiplier_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, ref Vector2 value);
		}

		public struct ExternalForcesModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_enabled_Injected(ref this, value);
				}
			}

			public float multiplier
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_multiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_multiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve multiplierCurve
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.ExternalForcesModule.get_multiplierCurve_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_multiplierCurve_Injected(ref this, ref value);
				}
			}

			public ParticleSystemGameObjectFilter influenceFilter
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_influenceFilter_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_influenceFilter_Injected(ref this, value);
				}
			}

			public LayerMask influenceMask
			{
				get
				{
					LayerMask result;
					ParticleSystem.ExternalForcesModule.get_influenceMask_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_influenceMask_Injected(ref this, ref value);
				}
			}

			public int influenceCount
			{
				get
				{
					return ParticleSystem.ExternalForcesModule.get_influenceCount_Injected(ref this);
				}
			}

			internal ExternalForcesModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			public bool IsAffectedBy(ParticleSystemForceField field)
			{
				return ParticleSystem.ExternalForcesModule.IsAffectedBy_Injected(ref this, field);
			}

			[NativeThrows]
			public void AddInfluence([NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.AddInfluence_Injected(ref this, field);
			}

			[NativeThrows]
			private void RemoveInfluenceAtIndex(int index)
			{
				ParticleSystem.ExternalForcesModule.RemoveInfluenceAtIndex_Injected(ref this, index);
			}

			public void RemoveInfluence(int index)
			{
				this.RemoveInfluenceAtIndex(index);
			}

			[NativeThrows]
			public void RemoveInfluence([NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.RemoveInfluence_Injected(ref this, field);
			}

			public void RemoveAllInfluences()
			{
				ParticleSystem.ExternalForcesModule.RemoveAllInfluences_Injected(ref this);
			}

			[NativeThrows]
			public void SetInfluence(int index, [NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				ParticleSystem.ExternalForcesModule.SetInfluence_Injected(ref this, index, field);
			}

			[NativeThrows]
			public ParticleSystemForceField GetInfluence(int index)
			{
				return ParticleSystem.ExternalForcesModule.GetInfluence_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_multiplier_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplier_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_multiplierCurve_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplierCurve_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemGameObjectFilter get_influenceFilter_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_influenceFilter_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemGameObjectFilter value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_influenceMask_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, out LayerMask ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_influenceMask_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ref LayerMask value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_influenceCount_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool IsAffectedBy_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveInfluenceAtIndex_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, ParticleSystemForceField field);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveAllInfluences_Injected(ref ParticleSystem.ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index, ParticleSystemForceField field);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemForceField GetInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index);
		}

		public struct NoiseModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.NoiseModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_enabled_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return ParticleSystem.NoiseModule.get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_separateAxes_Injected(ref this, value);
				}
			}

			[NativeName("StrengthX")]
			public ParticleSystem.MinMaxCurve strength
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_strength_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strength_Injected(ref this, ref value);
				}
			}

			[NativeName("StrengthXMultiplier")]
			public float strengthMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve strengthX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_strengthX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthX_Injected(ref this, ref value);
				}
			}

			public float strengthXMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthXMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve strengthY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_strengthY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthY_Injected(ref this, ref value);
				}
			}

			public float strengthYMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthYMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve strengthZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_strengthZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthZ_Injected(ref this, ref value);
				}
			}

			public float strengthZMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_strengthZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthZMultiplier_Injected(ref this, value);
				}
			}

			public float frequency
			{
				get
				{
					return ParticleSystem.NoiseModule.get_frequency_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_frequency_Injected(ref this, value);
				}
			}

			public bool damping
			{
				get
				{
					return ParticleSystem.NoiseModule.get_damping_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_damping_Injected(ref this, value);
				}
			}

			public int octaveCount
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveCount_Injected(ref this, value);
				}
			}

			public float octaveMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveMultiplier_Injected(ref this, value);
				}
			}

			public float octaveScale
			{
				get
				{
					return ParticleSystem.NoiseModule.get_octaveScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_octaveScale_Injected(ref this, value);
				}
			}

			public ParticleSystemNoiseQuality quality
			{
				get
				{
					return ParticleSystem.NoiseModule.get_quality_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_quality_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve scrollSpeed
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_scrollSpeed_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_scrollSpeed_Injected(ref this, ref value);
				}
			}

			public float scrollSpeedMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_scrollSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_scrollSpeedMultiplier_Injected(ref this, value);
				}
			}

			public bool remapEnabled
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapEnabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapEnabled_Injected(ref this, value);
				}
			}

			[NativeName("RemapX")]
			public ParticleSystem.MinMaxCurve remap
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_remap_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remap_Injected(ref this, ref value);
				}
			}

			[NativeName("RemapXMultiplier")]
			public float remapMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve remapX
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_remapX_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapX_Injected(ref this, ref value);
				}
			}

			public float remapXMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapXMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve remapY
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_remapY_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapY_Injected(ref this, ref value);
				}
			}

			public float remapYMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapYMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve remapZ
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_remapZ_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapZ_Injected(ref this, ref value);
				}
			}

			public float remapZMultiplier
			{
				get
				{
					return ParticleSystem.NoiseModule.get_remapZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapZMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve positionAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_positionAmount_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_positionAmount_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve rotationAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_rotationAmount_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_rotationAmount_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve sizeAmount
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.NoiseModule.get_sizeAmount_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_sizeAmount_Injected(ref this, ref value);
				}
			}

			internal NoiseModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_separateAxes_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_separateAxes_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strength_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strength_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_strengthMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthX_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthX_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_strengthXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthY_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthY_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_strengthYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthZ_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthZ_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_strengthZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_frequency_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_frequency_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_damping_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_damping_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_octaveCount_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_octaveCount_Injected(ref ParticleSystem.NoiseModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_octaveMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_octaveMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_octaveScale_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_octaveScale_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemNoiseQuality get_quality_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_quality_Injected(ref ParticleSystem.NoiseModule _unity_self, ParticleSystemNoiseQuality value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_scrollSpeed_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scrollSpeed_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_scrollSpeedMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scrollSpeedMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_remapEnabled_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapEnabled_Injected(ref ParticleSystem.NoiseModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remap_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remap_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_remapMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapX_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapX_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_remapXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapXMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapY_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapY_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_remapYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapYMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapZ_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapZ_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_remapZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapZMultiplier_Injected(ref ParticleSystem.NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_positionAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_positionAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rotationAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rotationAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_sizeAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAmount_Injected(ref ParticleSystem.NoiseModule _unity_self, ref ParticleSystem.MinMaxCurve value);
		}

		public struct LightsModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.LightsModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_enabled_Injected(ref this, value);
				}
			}

			public float ratio
			{
				get
				{
					return ParticleSystem.LightsModule.get_ratio_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_ratio_Injected(ref this, value);
				}
			}

			public bool useRandomDistribution
			{
				get
				{
					return ParticleSystem.LightsModule.get_useRandomDistribution_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_useRandomDistribution_Injected(ref this, value);
				}
			}

			public Light light
			{
				get
				{
					return ParticleSystem.LightsModule.get_light_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_light_Injected(ref this, value);
				}
			}

			public bool useParticleColor
			{
				get
				{
					return ParticleSystem.LightsModule.get_useParticleColor_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_useParticleColor_Injected(ref this, value);
				}
			}

			public bool sizeAffectsRange
			{
				get
				{
					return ParticleSystem.LightsModule.get_sizeAffectsRange_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_sizeAffectsRange_Injected(ref this, value);
				}
			}

			public bool alphaAffectsIntensity
			{
				get
				{
					return ParticleSystem.LightsModule.get_alphaAffectsIntensity_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_alphaAffectsIntensity_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve range
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LightsModule.get_range_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_range_Injected(ref this, ref value);
				}
			}

			public float rangeMultiplier
			{
				get
				{
					return ParticleSystem.LightsModule.get_rangeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_rangeMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve intensity
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.LightsModule.get_intensity_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_intensity_Injected(ref this, ref value);
				}
			}

			public float intensityMultiplier
			{
				get
				{
					return ParticleSystem.LightsModule.get_intensityMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_intensityMultiplier_Injected(ref this, value);
				}
			}

			public int maxLights
			{
				get
				{
					return ParticleSystem.LightsModule.get_maxLights_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_maxLights_Injected(ref this, value);
				}
			}

			internal LightsModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_ratio_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ratio_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useRandomDistribution_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useRandomDistribution_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Light get_light_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_light_Injected(ref ParticleSystem.LightsModule _unity_self, Light value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_useParticleColor_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_useParticleColor_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_sizeAffectsRange_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAffectsRange_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_alphaAffectsIntensity_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_alphaAffectsIntensity_Injected(ref ParticleSystem.LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.LightsModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.LightsModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_rangeMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rangeMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_intensity_Injected(ref ParticleSystem.LightsModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_intensity_Injected(ref ParticleSystem.LightsModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_intensityMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_intensityMultiplier_Injected(ref ParticleSystem.LightsModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_maxLights_Injected(ref ParticleSystem.LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_maxLights_Injected(ref ParticleSystem.LightsModule _unity_self, int value);
		}

		public struct TrailModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.TrailModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemTrailMode mode
			{
				get
				{
					return ParticleSystem.TrailModule.get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_mode_Injected(ref this, value);
				}
			}

			public float ratio
			{
				get
				{
					return ParticleSystem.TrailModule.get_ratio_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_ratio_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxCurve lifetime
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.TrailModule.get_lifetime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_lifetime_Injected(ref this, ref value);
				}
			}

			public float lifetimeMultiplier
			{
				get
				{
					return ParticleSystem.TrailModule.get_lifetimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_lifetimeMultiplier_Injected(ref this, value);
				}
			}

			public float minVertexDistance
			{
				get
				{
					return ParticleSystem.TrailModule.get_minVertexDistance_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_minVertexDistance_Injected(ref this, value);
				}
			}

			public ParticleSystemTrailTextureMode textureMode
			{
				get
				{
					return ParticleSystem.TrailModule.get_textureMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_textureMode_Injected(ref this, value);
				}
			}

			public bool worldSpace
			{
				get
				{
					return ParticleSystem.TrailModule.get_worldSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_worldSpace_Injected(ref this, value);
				}
			}

			public bool dieWithParticles
			{
				get
				{
					return ParticleSystem.TrailModule.get_dieWithParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_dieWithParticles_Injected(ref this, value);
				}
			}

			public bool sizeAffectsWidth
			{
				get
				{
					return ParticleSystem.TrailModule.get_sizeAffectsWidth_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_sizeAffectsWidth_Injected(ref this, value);
				}
			}

			public bool sizeAffectsLifetime
			{
				get
				{
					return ParticleSystem.TrailModule.get_sizeAffectsLifetime_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_sizeAffectsLifetime_Injected(ref this, value);
				}
			}

			public bool inheritParticleColor
			{
				get
				{
					return ParticleSystem.TrailModule.get_inheritParticleColor_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_inheritParticleColor_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxGradient colorOverLifetime
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.TrailModule.get_colorOverLifetime_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_colorOverLifetime_Injected(ref this, ref value);
				}
			}

			public ParticleSystem.MinMaxCurve widthOverTrail
			{
				get
				{
					ParticleSystem.MinMaxCurve result;
					ParticleSystem.TrailModule.get_widthOverTrail_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_widthOverTrail_Injected(ref this, ref value);
				}
			}

			public float widthOverTrailMultiplier
			{
				get
				{
					return ParticleSystem.TrailModule.get_widthOverTrailMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_widthOverTrailMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystem.MinMaxGradient colorOverTrail
			{
				get
				{
					ParticleSystem.MinMaxGradient result;
					ParticleSystem.TrailModule.get_colorOverTrail_Injected(ref this, out result);
					return result;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_colorOverTrail_Injected(ref this, ref value);
				}
			}

			public bool generateLightingData
			{
				get
				{
					return ParticleSystem.TrailModule.get_generateLightingData_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_generateLightingData_Injected(ref this, value);
				}
			}

			public int ribbonCount
			{
				get
				{
					return ParticleSystem.TrailModule.get_ribbonCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_ribbonCount_Injected(ref this, value);
				}
			}

			public float shadowBias
			{
				get
				{
					return ParticleSystem.TrailModule.get_shadowBias_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_shadowBias_Injected(ref this, value);
				}
			}

			public bool splitSubEmitterRibbons
			{
				get
				{
					return ParticleSystem.TrailModule.get_splitSubEmitterRibbons_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_splitSubEmitterRibbons_Injected(ref this, value);
				}
			}

			public bool attachRibbonsToTransform
			{
				get
				{
					return ParticleSystem.TrailModule.get_attachRibbonsToTransform_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_attachRibbonsToTransform_Injected(ref this, value);
				}
			}

			internal TrailModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemTrailMode get_mode_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mode_Injected(ref ParticleSystem.TrailModule _unity_self, ParticleSystemTrailMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_ratio_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ratio_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_lifetime_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetime_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_lifetimeMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetimeMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_minVertexDistance_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_minVertexDistance_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemTrailTextureMode get_textureMode_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureMode_Injected(ref ParticleSystem.TrailModule _unity_self, ParticleSystemTrailTextureMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_worldSpace_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_worldSpace_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_dieWithParticles_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dieWithParticles_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_sizeAffectsWidth_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAffectsWidth_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_sizeAffectsLifetime_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAffectsLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_inheritParticleColor_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_inheritParticleColor_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_colorOverLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colorOverLifetime_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_widthOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_widthOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_widthOverTrailMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_widthOverTrailMultiplier_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_colorOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colorOverTrail_Injected(ref ParticleSystem.TrailModule _unity_self, ref ParticleSystem.MinMaxGradient value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_generateLightingData_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_generateLightingData_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int get_ribbonCount_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ribbonCount_Injected(ref ParticleSystem.TrailModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float get_shadowBias_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_shadowBias_Injected(ref ParticleSystem.TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_splitSubEmitterRibbons_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_splitSubEmitterRibbons_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_attachRibbonsToTransform_Injected(ref ParticleSystem.TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_attachRibbonsToTransform_Injected(ref ParticleSystem.TrailModule _unity_self, bool value);
		}

		public struct CustomDataModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return ParticleSystem.CustomDataModule.get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CustomDataModule.set_enabled_Injected(ref this, value);
				}
			}

			internal CustomDataModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void SetMode(ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode)
			{
				ParticleSystem.CustomDataModule.SetMode_Injected(ref this, stream, mode);
			}

			[NativeThrows]
			public ParticleSystemCustomDataMode GetMode(ParticleSystemCustomData stream)
			{
				return ParticleSystem.CustomDataModule.GetMode_Injected(ref this, stream);
			}

			[NativeThrows]
			public void SetVectorComponentCount(ParticleSystemCustomData stream, int count)
			{
				ParticleSystem.CustomDataModule.SetVectorComponentCount_Injected(ref this, stream, count);
			}

			[NativeThrows]
			public int GetVectorComponentCount(ParticleSystemCustomData stream)
			{
				return ParticleSystem.CustomDataModule.GetVectorComponentCount_Injected(ref this, stream);
			}

			[NativeThrows]
			public void SetVector(ParticleSystemCustomData stream, int component, ParticleSystem.MinMaxCurve curve)
			{
				ParticleSystem.CustomDataModule.SetVector_Injected(ref this, stream, component, ref curve);
			}

			[NativeThrows]
			public ParticleSystem.MinMaxCurve GetVector(ParticleSystemCustomData stream, int component)
			{
				ParticleSystem.MinMaxCurve result;
				ParticleSystem.CustomDataModule.GetVector_Injected(ref this, stream, component, out result);
				return result;
			}

			[NativeThrows]
			public void SetColor(ParticleSystemCustomData stream, ParticleSystem.MinMaxGradient gradient)
			{
				ParticleSystem.CustomDataModule.SetColor_Injected(ref this, stream, ref gradient);
			}

			[NativeThrows]
			public ParticleSystem.MinMaxGradient GetColor(ParticleSystemCustomData stream)
			{
				ParticleSystem.MinMaxGradient result;
				ParticleSystem.CustomDataModule.GetColor_Injected(ref this, stream, out result);
				return result;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool get_enabled_Injected(ref ParticleSystem.CustomDataModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_enabled_Injected(ref ParticleSystem.CustomDataModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMode_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCustomDataMode GetMode_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVectorComponentCount_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int count);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVectorComponentCount_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVector_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, ref ParticleSystem.MinMaxCurve curve);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetVector_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, out ParticleSystem.MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetColor_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, ref ParticleSystem.MinMaxGradient gradient);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetColor_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, out ParticleSystem.MinMaxGradient ret);
		}

		[Obsolete("safeCollisionEventSize has been deprecated. Use GetSafeCollisionEventSize() instead (UnityUpgradable) -> ParticlePhysicsExtensions.GetSafeCollisionEventSize(UnityEngine.ParticleSystem)", false)]
		public int safeCollisionEventSize
		{
			get
			{
				return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize(this);
			}
		}

		[Obsolete("startDelay property is deprecated. Use main.startDelay or main.startDelayMultiplier instead.", false)]
		public float startDelay
		{
			get
			{
				return this.main.startDelayMultiplier;
			}
			set
			{
				this.main.startDelayMultiplier = value;
			}
		}

		[Obsolete("loop property is deprecated. Use main.loop instead.", false)]
		public bool loop
		{
			get
			{
				return this.main.loop;
			}
			set
			{
				this.main.loop = value;
			}
		}

		[Obsolete("playOnAwake property is deprecated. Use main.playOnAwake instead.", false)]
		public bool playOnAwake
		{
			get
			{
				return this.main.playOnAwake;
			}
			set
			{
				this.main.playOnAwake = value;
			}
		}

		[Obsolete("duration property is deprecated. Use main.duration instead.", false)]
		public float duration
		{
			get
			{
				return this.main.duration;
			}
		}

		[Obsolete("playbackSpeed property is deprecated. Use main.simulationSpeed instead.", false)]
		public float playbackSpeed
		{
			get
			{
				return this.main.simulationSpeed;
			}
			set
			{
				this.main.simulationSpeed = value;
			}
		}

		[Obsolete("enableEmission property is deprecated. Use emission.enabled instead.", false)]
		public bool enableEmission
		{
			get
			{
				return this.emission.enabled;
			}
			set
			{
				this.emission.enabled = value;
			}
		}

		[Obsolete("emissionRate property is deprecated. Use emission.rateOverTime, emission.rateOverDistance, emission.rateOverTimeMultiplier or emission.rateOverDistanceMultiplier instead.", false)]
		public float emissionRate
		{
			get
			{
				return this.emission.rateOverTimeMultiplier;
			}
			set
			{
				this.emission.rateOverTime = value;
			}
		}

		[Obsolete("startSpeed property is deprecated. Use main.startSpeed or main.startSpeedMultiplier instead.", false)]
		public float startSpeed
		{
			get
			{
				return this.main.startSpeedMultiplier;
			}
			set
			{
				this.main.startSpeedMultiplier = value;
			}
		}

		[Obsolete("startSize property is deprecated. Use main.startSize or main.startSizeMultiplier instead.", false)]
		public float startSize
		{
			get
			{
				return this.main.startSizeMultiplier;
			}
			set
			{
				this.main.startSizeMultiplier = value;
			}
		}

		[Obsolete("startColor property is deprecated. Use main.startColor instead.", false)]
		public Color startColor
		{
			get
			{
				return this.main.startColor.color;
			}
			set
			{
				this.main.startColor = value;
			}
		}

		[Obsolete("startRotation property is deprecated. Use main.startRotation or main.startRotationMultiplier instead.", false)]
		public float startRotation
		{
			get
			{
				return this.main.startRotationMultiplier;
			}
			set
			{
				this.main.startRotationMultiplier = value;
			}
		}

		[Obsolete("startRotation3D property is deprecated. Use main.startRotationX, main.startRotationY and main.startRotationZ instead. (Or main.startRotationXMultiplier, main.startRotationYMultiplier and main.startRotationZMultiplier).", false)]
		public Vector3 startRotation3D
		{
			get
			{
				return new Vector3(this.main.startRotationXMultiplier, this.main.startRotationYMultiplier, this.main.startRotationZMultiplier);
			}
			set
			{
				ParticleSystem.MainModule main = this.main;
				main.startRotationXMultiplier = value.x;
				main.startRotationYMultiplier = value.y;
				main.startRotationZMultiplier = value.z;
			}
		}

		[Obsolete("startLifetime property is deprecated. Use main.startLifetime or main.startLifetimeMultiplier instead.", false)]
		public float startLifetime
		{
			get
			{
				return this.main.startLifetimeMultiplier;
			}
			set
			{
				this.main.startLifetimeMultiplier = value;
			}
		}

		[Obsolete("gravityModifier property is deprecated. Use main.gravityModifier or main.gravityModifierMultiplier instead.", false)]
		public float gravityModifier
		{
			get
			{
				return this.main.gravityModifierMultiplier;
			}
			set
			{
				this.main.gravityModifierMultiplier = value;
			}
		}

		[Obsolete("maxParticles property is deprecated. Use main.maxParticles instead.", false)]
		public int maxParticles
		{
			get
			{
				return this.main.maxParticles;
			}
			set
			{
				this.main.maxParticles = value;
			}
		}

		[Obsolete("simulationSpace property is deprecated. Use main.simulationSpace instead.", false)]
		public ParticleSystemSimulationSpace simulationSpace
		{
			get
			{
				return this.main.simulationSpace;
			}
			set
			{
				this.main.simulationSpace = value;
			}
		}

		[Obsolete("scalingMode property is deprecated. Use main.scalingMode instead.", false)]
		public ParticleSystemScalingMode scalingMode
		{
			get
			{
				return this.main.scalingMode;
			}
			set
			{
				this.main.scalingMode = value;
			}
		}

		[Obsolete("automaticCullingEnabled property is deprecated. Use proceduralSimulationSupported instead (UnityUpgradable) -> proceduralSimulationSupported", true)]
		public bool automaticCullingEnabled
		{
			get
			{
				return this.proceduralSimulationSupported;
			}
		}

		public extern bool isPlaying
		{
			[NativeName("SyncJobs(false)->IsPlaying")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isEmitting
		{
			[NativeName("SyncJobs(false)->IsEmitting")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isStopped
		{
			[NativeName("SyncJobs(false)->IsStopped")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isPaused
		{
			[NativeName("SyncJobs(false)->IsPaused")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int particleCount
		{
			[NativeName("SyncJobs(false)->GetParticleCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float time
		{
			[NativeName("SyncJobs(false)->GetSecPosition")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SyncJobs(false)->SetSecPosition")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern uint randomSeed
		{
			[NativeName("GetRandomSeed")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SyncJobs(false)->SetRandomSeed")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useAutoRandomSeed
		{
			[NativeName("GetAutoRandomSeed")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SyncJobs(false)->SetAutoRandomSeed")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool proceduralSimulationSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("GetState().localToWorld", TargetType = TargetType.Field)]
		internal Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_localToWorldMatrix_Injected(out result);
				return result;
			}
		}

		public ParticleSystem.MainModule main
		{
			get
			{
				return new ParticleSystem.MainModule(this);
			}
		}

		public ParticleSystem.EmissionModule emission
		{
			get
			{
				return new ParticleSystem.EmissionModule(this);
			}
		}

		public ParticleSystem.ShapeModule shape
		{
			get
			{
				return new ParticleSystem.ShapeModule(this);
			}
		}

		public ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime
		{
			get
			{
				return new ParticleSystem.VelocityOverLifetimeModule(this);
			}
		}

		public ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime
		{
			get
			{
				return new ParticleSystem.LimitVelocityOverLifetimeModule(this);
			}
		}

		public ParticleSystem.InheritVelocityModule inheritVelocity
		{
			get
			{
				return new ParticleSystem.InheritVelocityModule(this);
			}
		}

		public ParticleSystem.LifetimeByEmitterSpeedModule lifetimeByEmitterSpeed
		{
			get
			{
				return new ParticleSystem.LifetimeByEmitterSpeedModule(this);
			}
		}

		public ParticleSystem.ForceOverLifetimeModule forceOverLifetime
		{
			get
			{
				return new ParticleSystem.ForceOverLifetimeModule(this);
			}
		}

		public ParticleSystem.ColorOverLifetimeModule colorOverLifetime
		{
			get
			{
				return new ParticleSystem.ColorOverLifetimeModule(this);
			}
		}

		public ParticleSystem.ColorBySpeedModule colorBySpeed
		{
			get
			{
				return new ParticleSystem.ColorBySpeedModule(this);
			}
		}

		public ParticleSystem.SizeOverLifetimeModule sizeOverLifetime
		{
			get
			{
				return new ParticleSystem.SizeOverLifetimeModule(this);
			}
		}

		public ParticleSystem.SizeBySpeedModule sizeBySpeed
		{
			get
			{
				return new ParticleSystem.SizeBySpeedModule(this);
			}
		}

		public ParticleSystem.RotationOverLifetimeModule rotationOverLifetime
		{
			get
			{
				return new ParticleSystem.RotationOverLifetimeModule(this);
			}
		}

		public ParticleSystem.RotationBySpeedModule rotationBySpeed
		{
			get
			{
				return new ParticleSystem.RotationBySpeedModule(this);
			}
		}

		public ParticleSystem.ExternalForcesModule externalForces
		{
			get
			{
				return new ParticleSystem.ExternalForcesModule(this);
			}
		}

		public ParticleSystem.NoiseModule noise
		{
			get
			{
				return new ParticleSystem.NoiseModule(this);
			}
		}

		public ParticleSystem.CollisionModule collision
		{
			get
			{
				return new ParticleSystem.CollisionModule(this);
			}
		}

		public ParticleSystem.TriggerModule trigger
		{
			get
			{
				return new ParticleSystem.TriggerModule(this);
			}
		}

		public ParticleSystem.SubEmittersModule subEmitters
		{
			get
			{
				return new ParticleSystem.SubEmittersModule(this);
			}
		}

		public ParticleSystem.TextureSheetAnimationModule textureSheetAnimation
		{
			get
			{
				return new ParticleSystem.TextureSheetAnimationModule(this);
			}
		}

		public ParticleSystem.LightsModule lights
		{
			get
			{
				return new ParticleSystem.LightsModule(this);
			}
		}

		public ParticleSystem.TrailModule trails
		{
			get
			{
				return new ParticleSystem.TrailModule(this);
			}
		}

		public ParticleSystem.CustomDataModule customData
		{
			get
			{
				return new ParticleSystem.CustomDataModule(this);
			}
		}

		[Obsolete("Emit with specific parameters is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
		{
			ParticleSystem.Particle particle = default(ParticleSystem.Particle);
			particle.position = position;
			particle.velocity = velocity;
			particle.lifetime = lifetime;
			particle.startLifetime = lifetime;
			particle.startSize = size;
			particle.rotation3D = Vector3.zero;
			particle.angularVelocity3D = Vector3.zero;
			particle.startColor = color;
			particle.randomSeed = 5u;
			this.EmitOld_Internal(ref particle);
		}

		[Obsolete("Emit with a single particle structure is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(ParticleSystem.Particle particle)
		{
			this.EmitOld_Internal(ref particle);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetParticleCurrentSize(ref ParticleSystem.Particle particle);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize3D", HasExplicitThis = true)]
		internal Vector3 GetParticleCurrentSize3D(ref ParticleSystem.Particle particle)
		{
			Vector3 result;
			this.GetParticleCurrentSize3D_Injected(ref particle, out result);
			return result;
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentColor", HasExplicitThis = true)]
		internal Color32 GetParticleCurrentColor(ref ParticleSystem.Particle particle)
		{
			Color32 result;
			this.GetParticleCurrentColor_Injected(ref particle, out result);
			return result;
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleMeshIndex", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetParticleMeshIndex(ref ParticleSystem.Particle particle);

		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticles", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetParticles([Out] ParticleSystem.Particle[] particles, int size, int offset);

		public void SetParticles([Out] ParticleSystem.Particle[] particles, int size)
		{
			this.SetParticles(particles, size, 0);
		}

		public void SetParticles([Out] ParticleSystem.Particle[] particles)
		{
			this.SetParticles(particles, -1);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset);

		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size, int offset)
		{
			this.SetParticlesWithNativeArray((IntPtr)particles.GetUnsafeReadOnlyPtr<ParticleSystem.Particle>(), particles.Length, size, 0);
		}

		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size)
		{
			this.SetParticles(particles, size, 0);
		}

		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles)
		{
			this.SetParticles(particles, -1);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticles", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetParticles([NotNull("ArgumentNullException")] [Out] ParticleSystem.Particle[] particles, int size, int offset);

		public int GetParticles([Out] ParticleSystem.Particle[] particles, int size)
		{
			return this.GetParticles(particles, size, 0);
		}

		public int GetParticles([Out] ParticleSystem.Particle[] particles)
		{
			return this.GetParticles(particles, -1);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset);

		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size, int offset)
		{
			return this.GetParticlesWithNativeArray((IntPtr)particles.GetUnsafePtr<ParticleSystem.Particle>(), particles.Length, size, 0);
		}

		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size)
		{
			return this.GetParticles(particles, size, 0);
		}

		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles)
		{
			return this.GetParticles(particles, -1);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::SetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCustomParticleData([NotNull("ArgumentNullException")] List<Vector4> customData, ParticleSystemCustomData streamIndex);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetCustomParticleData([NotNull("ArgumentNullException")] List<Vector4> customData, ParticleSystemCustomData streamIndex);

		public ParticleSystem.PlaybackState GetPlaybackState()
		{
			ParticleSystem.PlaybackState result;
			this.GetPlaybackState_Injected(out result);
			return result;
		}

		public void SetPlaybackState(ParticleSystem.PlaybackState playbackState)
		{
			this.SetPlaybackState_Injected(ref playbackState);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTrailData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTrailDataInternal(ref ParticleSystem.Trails trailData);

		public ParticleSystem.Trails GetTrails()
		{
			ParticleSystem.Trails result = new ParticleSystem.Trails
			{
				positions = new List<Vector4>(),
				frontPositions = new List<int>(),
				backPositions = new List<int>(),
				positionCounts = new List<int>()
			};
			this.GetTrailDataInternal(ref result);
			return result;
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTrailData", HasExplicitThis = true)]
		public void SetTrails(ParticleSystem.Trails trailData)
		{
			this.SetTrails_Injected(ref trailData);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::Simulate", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Simulate(float t, [UnityEngine.Internal.DefaultValue("true")] bool withChildren, [UnityEngine.Internal.DefaultValue("true")] bool restart, [UnityEngine.Internal.DefaultValue("true")] bool fixedTimeStep);

		public void Simulate(float t, [UnityEngine.Internal.DefaultValue("true")] bool withChildren, [UnityEngine.Internal.DefaultValue("true")] bool restart)
		{
			this.Simulate(t, withChildren, restart, true);
		}

		public void Simulate(float t, [UnityEngine.Internal.DefaultValue("true")] bool withChildren)
		{
			this.Simulate(t, withChildren, true);
		}

		public void Simulate(float t)
		{
			this.Simulate(t, true);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::Play", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play([UnityEngine.Internal.DefaultValue("true")] bool withChildren);

		public void Play()
		{
			this.Play(true);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::Pause", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause([UnityEngine.Internal.DefaultValue("true")] bool withChildren);

		public void Pause()
		{
			this.Pause(true);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::Stop", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop([UnityEngine.Internal.DefaultValue("true")] bool withChildren, [UnityEngine.Internal.DefaultValue("ParticleSystemStopBehavior.StopEmitting")] ParticleSystemStopBehavior stopBehavior);

		public void Stop([UnityEngine.Internal.DefaultValue("true")] bool withChildren)
		{
			this.Stop(withChildren, ParticleSystemStopBehavior.StopEmitting);
		}

		public void Stop()
		{
			this.Stop(true);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::Clear", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear([UnityEngine.Internal.DefaultValue("true")] bool withChildren);

		public void Clear()
		{
			this.Clear(true);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::IsAlive", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsAlive([UnityEngine.Internal.DefaultValue("true")] bool withChildren);

		public bool IsAlive()
		{
			return this.IsAlive(true);
		}

		[RequiredByNativeCode]
		public void Emit(int count)
		{
			this.Emit_Internal(count);
		}

		[NativeName("SyncJobs()->Emit")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit_Internal(int count);

		[NativeName("SyncJobs()->EmitParticlesExternal")]
		public void Emit(ParticleSystem.EmitParams emitParams, int count)
		{
			this.Emit_Injected(ref emitParams, count);
		}

		[NativeName("SyncJobs()->EmitParticleExternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EmitOld_Internal(ref ParticleSystem.Particle particle);

		public void TriggerSubEmitter(int subEmitterIndex)
		{
			this.TriggerSubEmitter(subEmitterIndex, null);
		}

		public void TriggerSubEmitter(int subEmitterIndex, ref ParticleSystem.Particle particle)
		{
			this.TriggerSubEmitterForParticle(subEmitterIndex, particle);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitterForParticle", HasExplicitThis = true)]
		internal void TriggerSubEmitterForParticle(int subEmitterIndex, ParticleSystem.Particle particle)
		{
			this.TriggerSubEmitterForParticle_Injected(subEmitterIndex, ref particle);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitter", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void TriggerSubEmitter(int subEmitterIndex, List<ParticleSystem.Particle> particles);

		[FreeFunction(Name = "ParticleSystemGeometryJob::ResetPreMappedBufferMemory")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetPreMappedBufferMemory();

		[FreeFunction(Name = "ParticleSystemGeometryJob::SetMaximumPreMappedBufferCounts")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMaximumPreMappedBufferCounts(int vertexBuffersCount, int indexBuffersCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe extern void* GetManagedJobData();

		internal JobHandle GetManagedJobHandle()
		{
			JobHandle result;
			this.GetManagedJobHandle_Injected(out result);
			return result;
		}

		internal void SetManagedJobHandle(JobHandle handle)
		{
			this.SetManagedJobHandle_Injected(ref handle);
		}

		[FreeFunction("ScheduleManagedJob", ThrowsException = true)]
		internal unsafe static JobHandle ScheduleManagedJob(ref JobsUtility.JobScheduleParameters parameters, void* additionalData)
		{
			JobHandle result;
			ParticleSystem.ScheduleManagedJob_Injected(ref parameters, additionalData, out result);
			return result;
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void CopyManagedJobData(void* systemPtr, out NativeParticleData particleData);

		[FreeFunction(Name = "ParticleSystemEditor::SetupDefaultParticleSystemType", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetupDefaultType(ParticleSystemSubEmitterType type);

		[NativeName("GetNoiseModule().GeneratePreviewTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GenerateNoisePreviewTexture(Texture2D dst);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void CalculateEffectUIData(ref int particleCount, ref float fastestParticle, ref float slowestParticle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GenerateRandomSeed();

		[FreeFunction(Name = "ParticleSystemScriptBindings::CalculateEffectUISubEmitterData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool CalculateEffectUISubEmitterData(ref int particleCount, ref float fastestParticle, ref float slowestParticle);

		[FreeFunction(Name = "ParticleSystemScriptBindings::CheckVertexStreamsMatchShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CheckVertexStreamsMatchShader(bool hasTangent, bool hasColor, int texCoordChannelCount, Material material, ref bool tangentError, ref bool colorError, ref bool uvError);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetMaxTexCoordStreams")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMaxTexCoordStreams();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleCurrentSize3D_Injected(ref ParticleSystem.Particle particle, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleCurrentColor_Injected(ref ParticleSystem.Particle particle, out Color32 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPlaybackState_Injected(out ParticleSystem.PlaybackState ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPlaybackState_Injected(ref ParticleSystem.PlaybackState playbackState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTrails_Injected(ref ParticleSystem.Trails trailData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit_Injected(ref ParticleSystem.EmitParams emitParams, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TriggerSubEmitterForParticle_Injected(int subEmitterIndex, ref ParticleSystem.Particle particle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetManagedJobHandle_Injected(out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetManagedJobHandle_Injected(ref JobHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleManagedJob_Injected(ref JobsUtility.JobScheduleParameters parameters, void* additionalData, out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localToWorldMatrix_Injected(out Matrix4x4 ret);
	}
}
