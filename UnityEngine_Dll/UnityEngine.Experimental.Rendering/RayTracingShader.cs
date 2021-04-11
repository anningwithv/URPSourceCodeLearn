using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h"), NativeHeader("Runtime/Shaders/RayTracingShader.h"), NativeHeader("Runtime/Shaders/RayTracingAccelerationStructure.h")]
	public sealed class RayTracingShader : UnityEngine.Object
	{
		public extern float maxRecursionDepth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction(Name = "RayTracingShaderScripting::SetValue<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float val);

		[FreeFunction(Name = "RayTracingShaderScripting::SetValue<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(int nameID, int val);

		[FreeFunction(Name = "RayTracingShaderScripting::SetValue<Vector4f>", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			this.SetVector_Injected(nameID, ref val);
		}

		[FreeFunction(Name = "RayTracingShaderScripting::SetValue<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			this.SetMatrix_Injected(nameID, ref val);
		}

		[FreeFunction(Name = "RayTracingShaderScripting::SetArray<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatArray(int nameID, float[] values);

		[FreeFunction(Name = "RayTracingShaderScripting::SetArray<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntArray(int nameID, int[] values);

		[FreeFunction(Name = "RayTracingShaderScripting::SetArray<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		[FreeFunction(Name = "RayTracingShaderScripting::SetArray<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		[NativeMethod(Name = "RayTracingShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int nameID, [NotNull("ArgumentNullException")] Texture texture);

		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer);

		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer);

		[FreeFunction(Name = "RayTracingShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantComputeBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer, int offset, int size);

		[FreeFunction(Name = "RayTracingShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer, int offset, int size);

		[NativeMethod(Name = "RayTracingShaderScripting::SetAccelerationStructure", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAccelerationStructure(int nameID, [NotNull("ArgumentNullException")] RayTracingAccelerationStructure accelerationStructure);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetShaderPass(string passName);

		[NativeMethod(Name = "RayTracingShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTextureFromGlobal(int nameID, int globalTextureNameID);

		[NativeName("DispatchRays")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispatch(string rayGenFunctionName, int width, int height, int depth, Camera camera = null);

		public void SetBuffer(int nameID, GraphicsBuffer buffer)
		{
			this.SetGraphicsBuffer(nameID, buffer);
		}

		private RayTracingShader()
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

		public void SetTexture(string name, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(name), texture);
		}

		public void SetBuffer(string name, ComputeBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(name), buffer);
		}

		public void SetBuffer(string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(name), buffer);
		}

		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(nameID, buffer, offset, size);
		}

		public void SetConstantBuffer(string name, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		public void SetConstantBuffer(int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(nameID, buffer, offset, size);
		}

		public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		public void SetAccelerationStructure(string name, RayTracingAccelerationStructure accelerationStructure)
		{
			this.SetAccelerationStructure(Shader.PropertyToID(name), accelerationStructure);
		}

		public void SetTextureFromGlobal(string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);
	}
}
