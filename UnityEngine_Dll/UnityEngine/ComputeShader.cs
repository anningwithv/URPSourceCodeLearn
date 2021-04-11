using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h"), NativeHeader("Runtime/Shaders/ComputeShader.h"), UsedByNativeCode]
	public sealed class ComputeShader : Object
	{
		public string[] shaderKeywords
		{
			get
			{
				return this.GetShaderKeywords();
			}
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		[NativeMethod(Name = "ComputeShaderScripting::FindKernel", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true), RequiredByNativeCode]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int FindKernel(string name);

		[FreeFunction(Name = "ComputeShaderScripting::HasKernel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasKernel(string name);

		[FreeFunction(Name = "ComputeShaderScripting::SetValue<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float val);

		[FreeFunction(Name = "ComputeShaderScripting::SetValue<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(int nameID, int val);

		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Vector4f>", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			this.SetVector_Injected(nameID, ref val);
		}

		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			this.SetMatrix_Injected(nameID, ref val);
		}

		[FreeFunction(Name = "ComputeShaderScripting::SetArray<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatArray(int nameID, float[] values);

		[FreeFunction(Name = "ComputeShaderScripting::SetArray<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntArray(int nameID, int[] values);

		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		[NativeMethod(Name = "ComputeShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] Texture texture, int mipLevel);

		[NativeMethod(Name = "ComputeShaderScripting::SetRenderTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRenderTexture(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] RenderTexture texture, int mipLevel, RenderTextureSubElement element);

		[NativeMethod(Name = "ComputeShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTextureFromGlobal(int kernelIndex, int nameID, int globalTextureNameID);

		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetBuffer(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer);

		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetGraphicsBuffer(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer);

		public void SetBuffer(int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetBuffer(kernelIndex, nameID, buffer);
		}

		public void SetBuffer(int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetGraphicsBuffer(kernelIndex, nameID, buffer);
		}

		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantComputeBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer, int offset, int size);

		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer, int offset, int size);

		[NativeMethod(Name = "ComputeShaderScripting::GetKernelThreadGroupSizes", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetKernelThreadGroupSizes(int kernelIndex, out uint x, out uint y, out uint z);

		[NativeName("DispatchComputeShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DispatchIndirect(int kernelIndex, [NotNull("ArgumentNullException")] ComputeBuffer argsBuffer, uint argsOffset);

		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DispatchIndirectGraphicsBuffer(int kernelIndex, [NotNull("ArgumentNullException")] GraphicsBuffer argsBuffer, uint argsOffset);

		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableKeyword(string keyword);

		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableKeyword(string keyword);

		[FreeFunction("ComputeShaderScripting::IsKeywordEnabled", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsKeywordEnabled(string keyword);

		[FreeFunction("ComputeShaderScripting::GetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string[] GetShaderKeywords();

		[FreeFunction("ComputeShaderScripting::SetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetShaderKeywords(string[] names);

		private ComputeShader()
		{
		}

		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		public void SetTexture(int kernelIndex, int nameID, Texture texture)
		{
			this.SetTexture(kernelIndex, nameID, texture, 0);
		}

		public void SetTexture(int kernelIndex, string name, Texture texture)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, 0);
		}

		public void SetTexture(int kernelIndex, string name, Texture texture, int mipLevel)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel);
		}

		public void SetTexture(int kernelIndex, int nameID, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, nameID, texture, mipLevel, element);
		}

		public void SetTexture(int kernelIndex, string name, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel, element);
		}

		public void SetTextureFromGlobal(int kernelIndex, string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(kernelIndex, Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		public void SetBuffer(int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		public void SetBuffer(int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(nameID, buffer, offset, size);
		}

		public void SetConstantBuffer(string name, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		public void SetConstantBuffer(int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(nameID, buffer, offset, size);
		}

		public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			this.Internal_DispatchIndirect(kernelIndex, argsBuffer, argsOffset);
		}

		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0u);
		}

		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			this.Internal_DispatchIndirectGraphicsBuffer(kernelIndex, argsBuffer, argsOffset);
		}

		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0u);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);
	}
}
