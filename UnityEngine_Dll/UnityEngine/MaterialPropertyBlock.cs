using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Math/SphericalHarmonicsL2.h"), NativeHeader("Runtime/Shaders/ShaderPropertySheet.h"), NativeHeader("Runtime/Shaders/ComputeShader.h"), NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public sealed class MaterialPropertyBlock
	{
		internal IntPtr m_Ptr;

		public extern bool isEmpty
		{
			[NativeName("IsEmpty"), ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Use SetFloat instead (UnityUpgradable) -> SetFloat(*)", false)]
		public void AddFloat(string name, float value)
		{
			this.SetFloat(Shader.PropertyToID(name), value);
		}

		[Obsolete("Use SetFloat instead (UnityUpgradable) -> SetFloat(*)", false)]
		public void AddFloat(int nameID, float value)
		{
			this.SetFloat(nameID, value);
		}

		[Obsolete("Use SetVector instead (UnityUpgradable) -> SetVector(*)", false)]
		public void AddVector(string name, Vector4 value)
		{
			this.SetVector(Shader.PropertyToID(name), value);
		}

		[Obsolete("Use SetVector instead (UnityUpgradable) -> SetVector(*)", false)]
		public void AddVector(int nameID, Vector4 value)
		{
			this.SetVector(nameID, value);
		}

		[Obsolete("Use SetColor instead (UnityUpgradable) -> SetColor(*)", false)]
		public void AddColor(string name, Color value)
		{
			this.SetColor(Shader.PropertyToID(name), value);
		}

		[Obsolete("Use SetColor instead (UnityUpgradable) -> SetColor(*)", false)]
		public void AddColor(int nameID, Color value)
		{
			this.SetColor(nameID, value);
		}

		[Obsolete("Use SetMatrix instead (UnityUpgradable) -> SetMatrix(*)", false)]
		public void AddMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrix(Shader.PropertyToID(name), value);
		}

		[Obsolete("Use SetMatrix instead (UnityUpgradable) -> SetMatrix(*)", false)]
		public void AddMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrix(nameID, value);
		}

		[Obsolete("Use SetTexture instead (UnityUpgradable) -> SetTexture(*)", false)]
		public void AddTexture(string name, Texture value)
		{
			this.SetTexture(Shader.PropertyToID(name), value);
		}

		[Obsolete("Use SetTexture instead (UnityUpgradable) -> SetTexture(*)", false)]
		public void AddTexture(int nameID, Texture value)
		{
			this.SetTexture(nameID, value);
		}

		[NativeName("GetFloatFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatImpl(int name);

		[NativeName("GetVectorFromScript"), ThreadSafe]
		private Vector4 GetVectorImpl(int name)
		{
			Vector4 result;
			this.GetVectorImpl_Injected(name, out result);
			return result;
		}

		[NativeName("GetColorFromScript"), ThreadSafe]
		private Color GetColorImpl(int name)
		{
			Color result;
			this.GetColorImpl_Injected(name, out result);
			return result;
		}

		[NativeName("GetMatrixFromScript"), ThreadSafe]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			Matrix4x4 result;
			this.GetMatrixImpl_Injected(name, out result);
			return result;
		}

		[NativeName("GetTextureFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture GetTextureImpl(int name);

		[NativeName("SetFloatFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatImpl(int name, float value);

		[NativeName("SetVectorFromScript"), ThreadSafe]
		private void SetVectorImpl(int name, Vector4 value)
		{
			this.SetVectorImpl_Injected(name, ref value);
		}

		[NativeName("SetColorFromScript"), ThreadSafe]
		private void SetColorImpl(int name, Color value)
		{
			this.SetColorImpl_Injected(name, ref value);
		}

		[NativeName("SetMatrixFromScript"), ThreadSafe]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			this.SetMatrixImpl_Injected(name, ref value);
		}

		[NativeName("SetTextureFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTextureImpl(int name, [NotNull("ArgumentNullException")] Texture value);

		[NativeName("SetRenderTextureFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRenderTextureImpl(int name, [NotNull("ArgumentNullException")] RenderTexture value, RenderTextureSubElement element);

		[NativeName("SetBufferFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBufferImpl(int name, ComputeBuffer value);

		[NativeName("SetGraphicsBufferFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGraphicsBufferImpl(int name, GraphicsBuffer value);

		[NativeName("SetConstantBufferFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		[NativeName("SetConstantGraphicsBufferFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		[NativeName("SetFloatArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatArrayImpl(int name, float[] values, int count);

		[NativeName("SetVectorArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVectorArrayImpl(int name, Vector4[] values, int count);

		[NativeName("SetMatrixArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		[NativeName("GetFloatArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[] GetFloatArrayImpl(int name);

		[NativeName("GetVectorArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector4[] GetVectorArrayImpl(int name);

		[NativeName("GetMatrixArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Matrix4x4[] GetMatrixArrayImpl(int name);

		[NativeName("GetFloatArrayCountFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetFloatArrayCountImpl(int name);

		[NativeName("GetVectorArrayCountFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetVectorArrayCountImpl(int name);

		[NativeName("GetMatrixArrayCountFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetMatrixArrayCountImpl(int name);

		[NativeName("ExtractFloatArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractFloatArrayImpl(int name, [Out] float[] val);

		[NativeName("ExtractVectorArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractVectorArrayImpl(int name, [Out] Vector4[] val);

		[NativeName("ExtractMatrixArrayFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		[FreeFunction("ConvertAndCopySHCoefficientArraysToPropertySheetFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_CopySHCoefficientArraysFrom(MaterialPropertyBlock properties, SphericalHarmonicsL2[] lightProbes, int sourceStart, int destStart, int count);

		[FreeFunction("CopyProbeOcclusionArrayToPropertySheetFromScript"), ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_CopyProbeOcclusionArrayFrom(MaterialPropertyBlock properties, Vector4[] occlusionProbes, int sourceStart, int destStart, int count);

		[NativeMethod(Name = "MaterialPropertyBlockScripting::Create", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateImpl();

		[NativeMethod(Name = "MaterialPropertyBlockScripting::Destroy", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyImpl(IntPtr mpb);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Clear(bool keepMemory);

		public void Clear()
		{
			this.Clear(true);
		}

		private void SetFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetFloatArrayImpl(name, values, count);
		}

		private void SetVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetVectorArrayImpl(name, values, count);
		}

		private void SetMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetMatrixArrayImpl(name, values, count);
		}

		private void ExtractFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int floatArrayCountImpl = this.GetFloatArrayCountImpl(name);
			bool flag2 = floatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, floatArrayCountImpl);
				this.ExtractFloatArrayImpl(name, (float[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		private void ExtractVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int vectorArrayCountImpl = this.GetVectorArrayCountImpl(name);
			bool flag2 = vectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, vectorArrayCountImpl);
				this.ExtractVectorArrayImpl(name, (Vector4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		private void ExtractMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int matrixArrayCountImpl = this.GetMatrixArrayCountImpl(name);
			bool flag2 = matrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, matrixArrayCountImpl);
				this.ExtractMatrixArrayImpl(name, (Matrix4x4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		public MaterialPropertyBlock()
		{
			this.m_Ptr = MaterialPropertyBlock.CreateImpl();
		}

		~MaterialPropertyBlock()
		{
			this.Dispose();
		}

		private void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				MaterialPropertyBlock.DestroyImpl(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		public void SetVector(string name, Vector4 value)
		{
			this.SetVectorImpl(Shader.PropertyToID(name), value);
		}

		public void SetVector(int nameID, Vector4 value)
		{
			this.SetVectorImpl(nameID, value);
		}

		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		public Vector4 GetVector(string name)
		{
			return this.GetVectorImpl(Shader.PropertyToID(name));
		}

		public Vector4 GetVector(int nameID)
		{
			return this.GetVectorImpl(nameID);
		}

		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		public void CopySHCoefficientArraysFrom(List<SphericalHarmonicsL2> lightProbes)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			this.CopySHCoefficientArraysFrom(NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), 0, 0, lightProbes.Count);
		}

		public void CopySHCoefficientArraysFrom(SphericalHarmonicsL2[] lightProbes)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			this.CopySHCoefficientArraysFrom(lightProbes, 0, 0, lightProbes.Length);
		}

		public void CopySHCoefficientArraysFrom(List<SphericalHarmonicsL2> lightProbes, int sourceStart, int destStart, int count)
		{
			this.CopySHCoefficientArraysFrom(NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), sourceStart, destStart, count);
		}

		public void CopySHCoefficientArraysFrom(SphericalHarmonicsL2[] lightProbes, int sourceStart, int destStart, int count)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			bool flag2 = sourceStart < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument sourceStart must not be negative.");
			}
			bool flag3 = destStart < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument destStart must not be negative.");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			bool flag5 = lightProbes.Length < sourceStart + count;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("The specified source start index or count is out of the range.");
			}
			MaterialPropertyBlock.Internal_CopySHCoefficientArraysFrom(this, lightProbes, sourceStart, destStart, count);
		}

		public void CopyProbeOcclusionArrayFrom(List<Vector4> occlusionProbes)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			this.CopyProbeOcclusionArrayFrom(NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes), 0, 0, occlusionProbes.Count);
		}

		public void CopyProbeOcclusionArrayFrom(Vector4[] occlusionProbes)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			this.CopyProbeOcclusionArrayFrom(occlusionProbes, 0, 0, occlusionProbes.Length);
		}

		public void CopyProbeOcclusionArrayFrom(List<Vector4> occlusionProbes, int sourceStart, int destStart, int count)
		{
			this.CopyProbeOcclusionArrayFrom(NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes), sourceStart, destStart, count);
		}

		public void CopyProbeOcclusionArrayFrom(Vector4[] occlusionProbes, int sourceStart, int destStart, int count)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			bool flag2 = sourceStart < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument sourceStart must not be negative.");
			}
			bool flag3 = destStart < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument destStart must not be negative.");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			bool flag5 = occlusionProbes.Length < sourceStart + count;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("The specified source start index or count is out of the range.");
			}
			MaterialPropertyBlock.Internal_CopyProbeOcclusionArrayFrom(this, occlusionProbes, sourceStart, destStart, count);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVectorImpl_Injected(int name, out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColorImpl_Injected(int name, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrixImpl_Injected(int name, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVectorImpl_Injected(int name, ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorImpl_Injected(int name, ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrixImpl_Injected(int name, ref Matrix4x4 value);
	}
}
