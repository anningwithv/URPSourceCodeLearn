using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VisualEffectBindings.h"), NativeHeader("Modules/VFX/Public/VisualEffect.h"), RequireComponent(typeof(Transform))]
	public class VisualEffect : Behaviour
	{
		private VFXEventAttribute m_cachedEventAttribute;

		public Action<VFXOutputEventArgs> outputEventReceived;

		public extern bool pause
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float playRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern uint startSeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool resetSeedOnPlay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int initialEventID
		{
			[FreeFunction(Name = "VisualEffectBindings::GetInitialEventID", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "VisualEffectBindings::SetInitialEventID", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string initialEventName
		{
			[FreeFunction(Name = "VisualEffectBindings::GetInitialEventName", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(Name = "VisualEffectBindings::SetInitialEventName", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool culled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern VisualEffectAsset visualEffectAsset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int aliveParticleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public VFXEventAttribute CreateVFXEventAttribute()
		{
			bool flag = this.visualEffectAsset == null;
			VFXEventAttribute result;
			if (flag)
			{
				result = null;
			}
			else
			{
				VFXEventAttribute vFXEventAttribute = VFXEventAttribute.Internal_InstanciateVFXEventAttribute(this.visualEffectAsset);
				result = vFXEventAttribute;
			}
			return result;
		}

		private void CheckValidVFXEventAttribute(VFXEventAttribute eventAttribute)
		{
			bool flag = eventAttribute != null && eventAttribute.vfxAsset != this.visualEffectAsset;
			if (flag)
			{
				throw new InvalidOperationException("Invalid VFXEventAttribute provided to VisualEffect. It has been created with another VisualEffectAsset. Use CreateVFXEventAttribute.");
			}
		}

		[FreeFunction(Name = "VisualEffectBindings::SendEventFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SendEventFromScript(int eventNameID, VFXEventAttribute eventAttribute);

		public void SendEvent(int eventNameID, VFXEventAttribute eventAttribute)
		{
			this.CheckValidVFXEventAttribute(eventAttribute);
			this.SendEventFromScript(eventNameID, eventAttribute);
		}

		public void SendEvent(string eventName, VFXEventAttribute eventAttribute)
		{
			this.SendEvent(Shader.PropertyToID(eventName), eventAttribute);
		}

		public void SendEvent(int eventNameID)
		{
			this.SendEventFromScript(eventNameID, null);
		}

		public void SendEvent(string eventName)
		{
			this.SendEvent(Shader.PropertyToID(eventName), null);
		}

		public void Play(VFXEventAttribute eventAttribute)
		{
			this.SendEvent(VisualEffectAsset.PlayEventID, eventAttribute);
		}

		public void Play()
		{
			this.SendEvent(VisualEffectAsset.PlayEventID);
		}

		public void Stop(VFXEventAttribute eventAttribute)
		{
			this.SendEvent(VisualEffectAsset.StopEventID, eventAttribute);
		}

		public void Stop()
		{
			this.SendEvent(VisualEffectAsset.StopEventID);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reinit();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AdvanceOneFrame();

		[FreeFunction(Name = "VisualEffectBindings::ResetOverrideFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetOverride(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::GetTextureDimensionFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TextureDimension GetTextureDimension(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasBool(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasInt(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasUInt(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasFloat(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector2f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector2(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector3f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector3(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector4(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasMatrix4x4(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasTexture(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasAnimationCurve(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Gradient*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasGradient(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasMesh(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBool(int nameID, bool b);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(int nameID, int i);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetUInt(int nameID, uint i);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float f);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public void SetVector2(int nameID, Vector2 v)
		{
			this.SetVector2_Injected(nameID, ref v);
		}

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public void SetVector3(int nameID, Vector3 v)
		{
			this.SetVector3_Injected(nameID, ref v);
		}

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public void SetVector4(int nameID, Vector4 v)
		{
			this.SetVector4_Injected(nameID, ref v);
		}

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			this.SetMatrix4x4_Injected(nameID, ref v);
		}

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int nameID, [NotNull("ArgumentNullException")] Texture t);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAnimationCurve(int nameID, [NotNull("ArgumentNullException")] AnimationCurve c);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Gradient*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetGradient(int nameID, [NotNull("ArgumentNullException")] Gradient g);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMesh(int nameID, [NotNull("ArgumentNullException")] Mesh m);

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<bool>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetInt(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<UInt32>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetUInt(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 result;
			this.GetVector2_Injected(nameID, out result);
			return result;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 result;
			this.GetVector3_Injected(nameID, out result);
			return result;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 result;
			this.GetVector4_Injected(nameID, out result);
			return result;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 result;
			this.GetMatrix4x4_Injected(nameID, out result);
			return result;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Texture*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Mesh*>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Mesh GetMesh(int nameID);

		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			this.Internal_GetGradient(nameID, gradient);
			return gradient;
		}

		[FreeFunction(Name = "VisualEffectBindings::Internal_GetGradientFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetGradient(int nameID, Gradient gradient);

		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.Internal_GetAnimationCurve(nameID, animationCurve);
			return animationCurve;
		}

		[FreeFunction(Name = "VisualEffectBindings::Internal_GetAnimationCurveFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetAnimationCurve(int nameID, AnimationCurve curve);

		[FreeFunction(Name = "VisualEffectBindings::HasSystemFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasSystem(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::GetParticleSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		public VFXParticleSystemInfo GetParticleSystemInfo(int nameID)
		{
			VFXParticleSystemInfo result;
			this.GetParticleSystemInfo_Injected(nameID, out result);
			return result;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetSpawnSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSpawnSystemInfo(int nameID, IntPtr spawnerState);

		public void GetSpawnSystemInfo(int nameID, VFXSpawnerState spawnState)
		{
			bool flag = spawnState == null;
			if (flag)
			{
				throw new NullReferenceException("GetSpawnSystemInfo expects a non null VFXSpawnerState.");
			}
			IntPtr ptr = spawnState.GetPtr();
			bool flag2 = ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new NullReferenceException("GetSpawnSystemInfo use an unexpected not owned VFXSpawnerState.");
			}
			this.GetSpawnSystemInfo(nameID, ptr);
		}

		public VFXSpawnerState GetSpawnSystemInfo(int nameID)
		{
			VFXSpawnerState vFXSpawnerState = new VFXSpawnerState();
			this.GetSpawnSystemInfo(nameID, vFXSpawnerState);
			return vFXSpawnerState;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetSystemNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSystemNames([NotNull("ArgumentNullException")] List<string> names);

		[FreeFunction(Name = "VisualEffectBindings::GetParticleSystemNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetParticleSystemNames([NotNull("ArgumentNullException")] List<string> names);

		[FreeFunction(Name = "VisualEffectBindings::GetOutputEventNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetOutputEventNames([NotNull("ArgumentNullException")] List<string> names);

		[FreeFunction(Name = "VisualEffectBindings::GetSpawnSystemNamesFromScript", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSpawnSystemNames([NotNull("ArgumentNullException")] List<string> names);

		public void ResetOverride(string name)
		{
			this.ResetOverride(Shader.PropertyToID(name));
		}

		public bool HasInt(string name)
		{
			return this.HasInt(Shader.PropertyToID(name));
		}

		public bool HasUInt(string name)
		{
			return this.HasUInt(Shader.PropertyToID(name));
		}

		public bool HasFloat(string name)
		{
			return this.HasFloat(Shader.PropertyToID(name));
		}

		public bool HasVector2(string name)
		{
			return this.HasVector2(Shader.PropertyToID(name));
		}

		public bool HasVector3(string name)
		{
			return this.HasVector3(Shader.PropertyToID(name));
		}

		public bool HasVector4(string name)
		{
			return this.HasVector4(Shader.PropertyToID(name));
		}

		public bool HasMatrix4x4(string name)
		{
			return this.HasMatrix4x4(Shader.PropertyToID(name));
		}

		public bool HasTexture(string name)
		{
			return this.HasTexture(Shader.PropertyToID(name));
		}

		public TextureDimension GetTextureDimension(string name)
		{
			return this.GetTextureDimension(Shader.PropertyToID(name));
		}

		public bool HasAnimationCurve(string name)
		{
			return this.HasAnimationCurve(Shader.PropertyToID(name));
		}

		public bool HasGradient(string name)
		{
			return this.HasGradient(Shader.PropertyToID(name));
		}

		public bool HasMesh(string name)
		{
			return this.HasMesh(Shader.PropertyToID(name));
		}

		public bool HasBool(string name)
		{
			return this.HasBool(Shader.PropertyToID(name));
		}

		public void SetInt(string name, int i)
		{
			this.SetInt(Shader.PropertyToID(name), i);
		}

		public void SetUInt(string name, uint i)
		{
			this.SetUInt(Shader.PropertyToID(name), i);
		}

		public void SetFloat(string name, float f)
		{
			this.SetFloat(Shader.PropertyToID(name), f);
		}

		public void SetVector2(string name, Vector2 v)
		{
			this.SetVector2(Shader.PropertyToID(name), v);
		}

		public void SetVector3(string name, Vector3 v)
		{
			this.SetVector3(Shader.PropertyToID(name), v);
		}

		public void SetVector4(string name, Vector4 v)
		{
			this.SetVector4(Shader.PropertyToID(name), v);
		}

		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			this.SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		public void SetTexture(string name, Texture t)
		{
			this.SetTexture(Shader.PropertyToID(name), t);
		}

		public void SetAnimationCurve(string name, AnimationCurve c)
		{
			this.SetAnimationCurve(Shader.PropertyToID(name), c);
		}

		public void SetGradient(string name, Gradient g)
		{
			this.SetGradient(Shader.PropertyToID(name), g);
		}

		public void SetMesh(string name, Mesh m)
		{
			this.SetMesh(Shader.PropertyToID(name), m);
		}

		public void SetBool(string name, bool b)
		{
			this.SetBool(Shader.PropertyToID(name), b);
		}

		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		public uint GetUInt(string name)
		{
			return this.GetUInt(Shader.PropertyToID(name));
		}

		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		public Mesh GetMesh(string name)
		{
			return this.GetMesh(Shader.PropertyToID(name));
		}

		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		public AnimationCurve GetAnimationCurve(string name)
		{
			return this.GetAnimationCurve(Shader.PropertyToID(name));
		}

		public Gradient GetGradient(string name)
		{
			return this.GetGradient(Shader.PropertyToID(name));
		}

		public bool HasSystem(string name)
		{
			return this.HasSystem(Shader.PropertyToID(name));
		}

		public VFXParticleSystemInfo GetParticleSystemInfo(string name)
		{
			return this.GetParticleSystemInfo(Shader.PropertyToID(name));
		}

		public VFXSpawnerState GetSpawnSystemInfo(string name)
		{
			return this.GetSpawnSystemInfo(Shader.PropertyToID(name));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Simulate(float stepDeltaTime, uint stepCount = 1u);

		[RequiredByNativeCode]
		private static VFXEventAttribute InvokeGetCachedEventAttributeForOutputEvent_Internal(VisualEffect source)
		{
			bool flag = source.outputEventReceived == null;
			VFXEventAttribute result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = source.m_cachedEventAttribute == null;
				if (flag2)
				{
					source.m_cachedEventAttribute = source.CreateVFXEventAttribute();
				}
				result = source.m_cachedEventAttribute;
			}
			return result;
		}

		[RequiredByNativeCode]
		private static void InvokeOutputEventReceived_Internal(VisualEffect source, int eventNameId)
		{
			VFXOutputEventArgs obj = new VFXOutputEventArgs(eventNameId, source.m_cachedEventAttribute);
			source.outputEventReceived(obj);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector2_Injected(int nameID, ref Vector2 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector3_Injected(int nameID, ref Vector3 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector4_Injected(int nameID, ref Vector4 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix4x4_Injected(int nameID, ref Matrix4x4 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleSystemInfo_Injected(int nameID, out VFXParticleSystemInfo ret);
	}
}
