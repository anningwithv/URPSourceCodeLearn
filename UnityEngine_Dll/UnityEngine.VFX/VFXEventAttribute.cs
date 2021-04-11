using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	[NativeType(Header = "Modules/VFX/Public/VFXEventAttribute.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class VFXEventAttribute : IDisposable
	{
		private IntPtr m_Ptr;

		private bool m_Owner;

		private VisualEffectAsset m_VfxAsset;

		internal VisualEffectAsset vfxAsset
		{
			get
			{
				return this.m_VfxAsset;
			}
		}

		private VFXEventAttribute(IntPtr ptr, bool owner, VisualEffectAsset vfxAsset)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
			this.m_VfxAsset = vfxAsset;
		}

		private VFXEventAttribute() : this(IntPtr.Zero, false, null)
		{
		}

		public VFXEventAttribute(VFXEventAttribute original)
		{
			bool flag = original == null;
			if (flag)
			{
				throw new ArgumentNullException("VFXEventAttribute expect a non null attribute");
			}
			this.m_Ptr = VFXEventAttribute.Internal_Create();
			this.m_VfxAsset = original.m_VfxAsset;
			this.Internal_InitFromEventAttribute(original);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		internal static VFXEventAttribute Internal_InstanciateVFXEventAttribute(VisualEffectAsset vfxAsset)
		{
			VFXEventAttribute vFXEventAttribute = new VFXEventAttribute(VFXEventAttribute.Internal_Create(), true, vfxAsset);
			vFXEventAttribute.Internal_InitFromAsset(vfxAsset);
			return vFXEventAttribute;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_InitFromAsset(VisualEffectAsset vfxAsset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_InitFromEventAttribute(VFXEventAttribute vfxEventAttribute);

		private void Release()
		{
			bool flag = this.m_Owner && this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				VFXEventAttribute.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
			this.m_VfxAsset = null;
		}

		~VFXEventAttribute()
		{
			this.Release();
		}

		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		[NativeName("HasValueFromScript<bool>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasBool(int nameID);

		[NativeName("HasValueFromScript<int>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasInt(int nameID);

		[NativeName("HasValueFromScript<UInt32>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasUint(int nameID);

		[NativeName("HasValueFromScript<float>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasFloat(int nameID);

		[NativeName("HasValueFromScript<Vector2f>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector2(int nameID);

		[NativeName("HasValueFromScript<Vector3f>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector3(int nameID);

		[NativeName("HasValueFromScript<Vector4f>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector4(int nameID);

		[NativeName("HasValueFromScript<Matrix4x4f>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasMatrix4x4(int nameID);

		[NativeName("SetValueFromScript<bool>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBool(int nameID, bool b);

		[NativeName("SetValueFromScript<int>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(int nameID, int i);

		[NativeName("SetValueFromScript<UInt32>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetUint(int nameID, uint i);

		[NativeName("SetValueFromScript<float>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float f);

		[NativeName("SetValueFromScript<Vector2f>")]
		public void SetVector2(int nameID, Vector2 v)
		{
			this.SetVector2_Injected(nameID, ref v);
		}

		[NativeName("SetValueFromScript<Vector3f>")]
		public void SetVector3(int nameID, Vector3 v)
		{
			this.SetVector3_Injected(nameID, ref v);
		}

		[NativeName("SetValueFromScript<Vector4f>")]
		public void SetVector4(int nameID, Vector4 v)
		{
			this.SetVector4_Injected(nameID, ref v);
		}

		[NativeName("SetValueFromScript<Matrix4x4f>")]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			this.SetMatrix4x4_Injected(nameID, ref v);
		}

		[NativeName("GetValueFromScript<bool>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool(int nameID);

		[NativeName("GetValueFromScript<int>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetInt(int nameID);

		[NativeName("GetValueFromScript<UInt32>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetUint(int nameID);

		[NativeName("GetValueFromScript<float>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		[NativeName("GetValueFromScript<Vector2f>")]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 result;
			this.GetVector2_Injected(nameID, out result);
			return result;
		}

		[NativeName("GetValueFromScript<Vector3f>")]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 result;
			this.GetVector3_Injected(nameID, out result);
			return result;
		}

		[NativeName("GetValueFromScript<Vector4f>")]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 result;
			this.GetVector4_Injected(nameID, out result);
			return result;
		}

		[NativeName("GetValueFromScript<Matrix4x4f>")]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 result;
			this.GetMatrix4x4_Injected(nameID, out result);
			return result;
		}

		public bool HasBool(string name)
		{
			return this.HasBool(Shader.PropertyToID(name));
		}

		public bool HasInt(string name)
		{
			return this.HasInt(Shader.PropertyToID(name));
		}

		public bool HasUint(string name)
		{
			return this.HasUint(Shader.PropertyToID(name));
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

		public void SetBool(string name, bool b)
		{
			this.SetBool(Shader.PropertyToID(name), b);
		}

		public void SetInt(string name, int i)
		{
			this.SetInt(Shader.PropertyToID(name), i);
		}

		public void SetUint(string name, uint i)
		{
			this.SetUint(Shader.PropertyToID(name), i);
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

		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		public uint GetUint(string name)
		{
			return this.GetUint(Shader.PropertyToID(name));
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

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyValuesFrom([NotNull("ArgumentNullException")] VFXEventAttribute eventAttibute);

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
	}
}
