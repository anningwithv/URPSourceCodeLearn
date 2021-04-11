using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	[NativeType(Header = "Modules/VFX/Public/VFXExpressionValues.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class VFXExpressionValues
	{
		internal IntPtr m_Ptr;

		private VFXExpressionValues()
		{
		}

		[RequiredByNativeCode]
		internal static VFXExpressionValues CreateExpressionValuesWrapper(IntPtr ptr)
		{
			return new VFXExpressionValues
			{
				m_Ptr = ptr
			};
		}

		[NativeName("GetValueFromScript<bool>"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool(int nameID);

		[NativeName("GetValueFromScript<int>"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetInt(int nameID);

		[NativeName("GetValueFromScript<UInt32>"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetUInt(int nameID);

		[NativeName("GetValueFromScript<float>"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		[NativeName("GetValueFromScript<Vector2f>"), NativeThrows]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 result;
			this.GetVector2_Injected(nameID, out result);
			return result;
		}

		[NativeName("GetValueFromScript<Vector3f>"), NativeThrows]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 result;
			this.GetVector3_Injected(nameID, out result);
			return result;
		}

		[NativeName("GetValueFromScript<Vector4f>"), NativeThrows]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 result;
			this.GetVector4_Injected(nameID, out result);
			return result;
		}

		[NativeName("GetValueFromScript<Matrix4x4f>"), NativeThrows]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 result;
			this.GetMatrix4x4_Injected(nameID, out result);
			return result;
		}

		[NativeName("GetValueFromScript<Texture*>"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(int nameID);

		[NativeName("GetValueFromScript<Mesh*>"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Mesh GetMesh(int nameID);

		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.Internal_GetAnimationCurveFromScript(nameID, animationCurve);
			return animationCurve;
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_GetAnimationCurveFromScript(int nameID, AnimationCurve curve);

		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			this.Internal_GetGradientFromScript(nameID, gradient);
			return gradient;
		}

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_GetGradientFromScript(int nameID, Gradient gradient);

		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
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

		public AnimationCurve GetAnimationCurve(string name)
		{
			return this.GetAnimationCurve(Shader.PropertyToID(name));
		}

		public Gradient GetGradient(string name)
		{
			return this.GetGradient(Shader.PropertyToID(name));
		}

		public Mesh GetMesh(string name)
		{
			return this.GetMesh(Shader.PropertyToID(name));
		}

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
