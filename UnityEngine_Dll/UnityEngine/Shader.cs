using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h"), NativeHeader("Runtime/Shaders/ShaderNameRegistry.h"), NativeHeader("Runtime/Shaders/ComputeShader.h"), NativeHeader("Runtime/Shaders/Shader.h"), NativeHeader("Runtime/Graphics/ShaderScriptBindings.h"), NativeHeader("Runtime/Misc/ResourceManager.h"), NativeHeader("Runtime/Shaders/GpuPrograms/ShaderVariantCollection.h")]
	public sealed class Shader : Object
	{
		[Obsolete("Use Graphics.activeTier instead (UnityUpgradable) -> UnityEngine.Graphics.activeTier", false)]
		public static ShaderHardwareTier globalShaderHardwareTier
		{
			get
			{
				return (ShaderHardwareTier)Graphics.activeTier;
			}
			set
			{
				Graphics.activeTier = (GraphicsTier)value;
			}
		}

		[NativeProperty("MaximumShaderLOD")]
		public extern int maximumLOD
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("GlobalMaximumShaderLOD")]
		public static extern int globalMaximumLOD
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isSupported
		{
			[NativeMethod("IsSupported")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string globalRenderPipeline
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int renderQueue
		{
			[FreeFunction("ShaderScripting::GetRenderQueue", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern DisableBatchingType disableBatching
		{
			[FreeFunction("ShaderScripting::GetDisableBatchingType", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int passCount
		{
			[FreeFunction(Name = "ShaderScripting::GetPassCount", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("CustomEditorName")]
		internal extern string customEditor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("SetGlobalTexGenMode is not supported anymore. Use programmable shaders to achieve the same effect.", true)]
		public static void SetGlobalTexGenMode(string propertyName, TexGenMode mode)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("SetGlobalTextureMatrixName is not supported anymore. Use programmable shaders to achieve the same effect.", true)]
		public static void SetGlobalTextureMatrixName(string propertyName, string matrixName)
		{
		}

		public static Shader Find(string name)
		{
			return ResourcesAPI.ActiveAPI.FindShaderByName(name);
		}

		[FreeFunction("GetBuiltinResource<Shader>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Shader FindBuiltin(string name);

		[FreeFunction("ShaderScripting::EnableKeyword")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnableKeyword(string keyword);

		[FreeFunction("ShaderScripting::DisableKeyword")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableKeyword(string keyword);

		[FreeFunction("ShaderScripting::IsKeywordEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsKeywordEnabled(string keyword);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WarmupAllShaders();

		[FreeFunction("ShaderScripting::TagToID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int TagToID(string name);

		[FreeFunction("ShaderScripting::IDToTag")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string IDToTag(int name);

		[FreeFunction(Name = "ShaderScripting::PropertyToID", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PropertyToID(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Shader GetDependency(string name);

		public ShaderTagId FindPassTagValue(int passIndex, ShaderTagId tagName)
		{
			bool flag = passIndex < 0 || passIndex >= this.passCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("passIndex");
			}
			int id = this.Internal_FindPassTagValue(passIndex, tagName.id);
			return new ShaderTagId
			{
				id = id
			};
		}

		[FreeFunction(Name = "ShaderScripting::FindPassTagValue", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_FindPassTagValue(int passIndex, int tagName);

		[FreeFunction("ShaderScripting::SetGlobalFloat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatImpl(int name, float value);

		[FreeFunction("ShaderScripting::SetGlobalVector")]
		private static void SetGlobalVectorImpl(int name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl_Injected(name, ref value);
		}

		[FreeFunction("ShaderScripting::SetGlobalMatrix")]
		private static void SetGlobalMatrixImpl(int name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl_Injected(name, ref value);
		}

		[FreeFunction("ShaderScripting::SetGlobalTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTextureImpl(int name, Texture value);

		[FreeFunction("ShaderScripting::SetGlobalRenderTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element);

		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalBufferImpl(int name, ComputeBuffer value);

		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalGraphicsBufferImpl(int name, GraphicsBuffer value);

		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		[FreeFunction("ShaderScripting::GetGlobalFloat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGlobalFloatImpl(int name);

		[FreeFunction("ShaderScripting::GetGlobalVector")]
		private static Vector4 GetGlobalVectorImpl(int name)
		{
			Vector4 result;
			Shader.GetGlobalVectorImpl_Injected(name, out result);
			return result;
		}

		[FreeFunction("ShaderScripting::GetGlobalMatrix")]
		private static Matrix4x4 GetGlobalMatrixImpl(int name)
		{
			Matrix4x4 result;
			Shader.GetGlobalMatrixImpl_Injected(name, out result);
			return result;
		}

		[FreeFunction("ShaderScripting::GetGlobalTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Texture GetGlobalTextureImpl(int name);

		[FreeFunction("ShaderScripting::SetGlobalFloatArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatArrayImpl(int name, float[] values, int count);

		[FreeFunction("ShaderScripting::SetGlobalVectorArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorArrayImpl(int name, Vector4[] values, int count);

		[FreeFunction("ShaderScripting::SetGlobalMatrixArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		[FreeFunction("ShaderScripting::GetGlobalFloatArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float[] GetGlobalFloatArrayImpl(int name);

		[FreeFunction("ShaderScripting::GetGlobalVectorArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector4[] GetGlobalVectorArrayImpl(int name);

		[FreeFunction("ShaderScripting::GetGlobalMatrixArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Matrix4x4[] GetGlobalMatrixArrayImpl(int name);

		[FreeFunction("ShaderScripting::GetGlobalFloatArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalFloatArrayCountImpl(int name);

		[FreeFunction("ShaderScripting::GetGlobalVectorArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalVectorArrayCountImpl(int name);

		[FreeFunction("ShaderScripting::GetGlobalMatrixArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalMatrixArrayCountImpl(int name);

		[FreeFunction("ShaderScripting::ExtractGlobalFloatArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalFloatArrayImpl(int name, [Out] float[] val);

		[FreeFunction("ShaderScripting::ExtractGlobalVectorArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalVectorArrayImpl(int name, [Out] Vector4[] val);

		[FreeFunction("ShaderScripting::ExtractGlobalMatrixArray")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		private static void SetGlobalFloatArray(int name, float[] values, int count)
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
			Shader.SetGlobalFloatArrayImpl(name, values, count);
		}

		private static void SetGlobalVectorArray(int name, Vector4[] values, int count)
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
			Shader.SetGlobalVectorArrayImpl(name, values, count);
		}

		private static void SetGlobalMatrixArray(int name, Matrix4x4[] values, int count)
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
			Shader.SetGlobalMatrixArrayImpl(name, values, count);
		}

		private static void ExtractGlobalFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalFloatArrayCountImpl = Shader.GetGlobalFloatArrayCountImpl(name);
			bool flag2 = globalFloatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, globalFloatArrayCountImpl);
				Shader.ExtractGlobalFloatArrayImpl(name, (float[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		private static void ExtractGlobalVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalVectorArrayCountImpl = Shader.GetGlobalVectorArrayCountImpl(name);
			bool flag2 = globalVectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, globalVectorArrayCountImpl);
				Shader.ExtractGlobalVectorArrayImpl(name, (Vector4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		private static void ExtractGlobalMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalMatrixArrayCountImpl = Shader.GetGlobalMatrixArrayCountImpl(name);
			bool flag2 = globalMatrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, globalMatrixArrayCountImpl);
				Shader.ExtractGlobalMatrixArrayImpl(name, (Matrix4x4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		public static void SetGlobalFloat(string name, float value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), value);
		}

		public static void SetGlobalFloat(int nameID, float value)
		{
			Shader.SetGlobalFloatImpl(nameID, value);
		}

		public static void SetGlobalInt(string name, int value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		public static void SetGlobalInt(int nameID, int value)
		{
			Shader.SetGlobalFloatImpl(nameID, (float)value);
		}

		public static void SetGlobalVector(string name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		public static void SetGlobalVector(int nameID, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		public static void SetGlobalColor(string name, Color value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		public static void SetGlobalColor(int nameID, Color value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		public static void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(Shader.PropertyToID(name), value);
		}

		public static void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(nameID, value);
		}

		public static void SetGlobalTexture(string name, Texture value)
		{
			Shader.SetGlobalTextureImpl(Shader.PropertyToID(name), value);
		}

		public static void SetGlobalTexture(int nameID, Texture value)
		{
			Shader.SetGlobalTextureImpl(nameID, value);
		}

		public static void SetGlobalTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		public static void SetGlobalTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(nameID, value, element);
		}

		public static void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(Shader.PropertyToID(name), value);
		}

		public static void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(nameID, value);
		}

		public static void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		public static void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(nameID, value);
		}

		public static void SetGlobalConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		public static void SetGlobalConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(nameID, value, offset, size);
		}

		public static void SetGlobalConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		public static void SetGlobalConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		public static void SetGlobalFloatArray(string name, List<float> values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		public static void SetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.SetGlobalFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		public static void SetGlobalFloatArray(string name, float[] values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		public static void SetGlobalFloatArray(int nameID, float[] values)
		{
			Shader.SetGlobalFloatArray(nameID, values, values.Length);
		}

		public static void SetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		public static void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		public static void SetGlobalVectorArray(string name, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		public static void SetGlobalVectorArray(int nameID, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(nameID, values, values.Length);
		}

		public static void SetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		public static void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		public static void SetGlobalMatrixArray(string name, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		public static void SetGlobalMatrixArray(int nameID, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(nameID, values, values.Length);
		}

		public static float GetGlobalFloat(string name)
		{
			return Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		public static float GetGlobalFloat(int nameID)
		{
			return Shader.GetGlobalFloatImpl(nameID);
		}

		public static int GetGlobalInt(string name)
		{
			return (int)Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		public static int GetGlobalInt(int nameID)
		{
			return (int)Shader.GetGlobalFloatImpl(nameID);
		}

		public static Vector4 GetGlobalVector(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		public static Vector4 GetGlobalVector(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		public static Color GetGlobalColor(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		public static Color GetGlobalColor(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		public static Matrix4x4 GetGlobalMatrix(string name)
		{
			return Shader.GetGlobalMatrixImpl(Shader.PropertyToID(name));
		}

		public static Matrix4x4 GetGlobalMatrix(int nameID)
		{
			return Shader.GetGlobalMatrixImpl(nameID);
		}

		public static Texture GetGlobalTexture(string name)
		{
			return Shader.GetGlobalTextureImpl(Shader.PropertyToID(name));
		}

		public static Texture GetGlobalTexture(int nameID)
		{
			return Shader.GetGlobalTextureImpl(nameID);
		}

		public static float[] GetGlobalFloatArray(string name)
		{
			return Shader.GetGlobalFloatArray(Shader.PropertyToID(name));
		}

		public static float[] GetGlobalFloatArray(int nameID)
		{
			return (Shader.GetGlobalFloatArrayCountImpl(nameID) != 0) ? Shader.GetGlobalFloatArrayImpl(nameID) : null;
		}

		public static Vector4[] GetGlobalVectorArray(string name)
		{
			return Shader.GetGlobalVectorArray(Shader.PropertyToID(name));
		}

		public static Vector4[] GetGlobalVectorArray(int nameID)
		{
			return (Shader.GetGlobalVectorArrayCountImpl(nameID) != 0) ? Shader.GetGlobalVectorArrayImpl(nameID) : null;
		}

		public static Matrix4x4[] GetGlobalMatrixArray(string name)
		{
			return Shader.GetGlobalMatrixArray(Shader.PropertyToID(name));
		}

		public static Matrix4x4[] GetGlobalMatrixArray(int nameID)
		{
			return (Shader.GetGlobalMatrixArrayCountImpl(nameID) != 0) ? Shader.GetGlobalMatrixArrayImpl(nameID) : null;
		}

		public static void GetGlobalFloatArray(string name, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(Shader.PropertyToID(name), values);
		}

		public static void GetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(nameID, values);
		}

		public static void GetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(Shader.PropertyToID(name), values);
		}

		public static void GetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(nameID, values);
		}

		public static void GetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(Shader.PropertyToID(name), values);
		}

		public static void GetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(nameID, values);
		}

		private Shader()
		{
		}

		[FreeFunction("ShaderScripting::GetPropertyName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetPropertyName([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		[FreeFunction("ShaderScripting::GetPropertyNameId")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPropertyNameId([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		[FreeFunction("ShaderScripting::GetPropertyType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderPropertyType GetPropertyType([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		[FreeFunction("ShaderScripting::GetPropertyDescription")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetPropertyDescription([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		[FreeFunction("ShaderScripting::GetPropertyFlags")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderPropertyFlags GetPropertyFlags([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		[FreeFunction("ShaderScripting::GetPropertyAttributes")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetPropertyAttributes([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		[FreeFunction("ShaderScripting::GetPropertyDefaultValue")]
		private static Vector4 GetPropertyDefaultValue([NotNull("ArgumentNullException")] Shader shader, int propertyIndex)
		{
			Vector4 result;
			Shader.GetPropertyDefaultValue_Injected(shader, propertyIndex, out result);
			return result;
		}

		[FreeFunction("ShaderScripting::GetPropertyTextureDimension")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension GetPropertyTextureDimension([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		[FreeFunction("ShaderScripting::GetPropertyTextureDefaultName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetPropertyTextureDefaultName([NotNull("ArgumentNullException")] Shader shader, int propertyIndex);

		[FreeFunction("ShaderScripting::FindTextureStack")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindTextureStackImpl([NotNull("ArgumentNullException")] Shader s, int propertyIdx, out string stackName, out int layerIndex);

		private static void CheckPropertyIndex(Shader s, int propertyIndex)
		{
			bool flag = propertyIndex < 0 || propertyIndex >= s.GetPropertyCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("propertyIndex");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPropertyCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int FindPropertyIndex(string propertyName);

		public string GetPropertyName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyName(this, propertyIndex);
		}

		public int GetPropertyNameId(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyNameId(this, propertyIndex);
		}

		public ShaderPropertyType GetPropertyType(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyType(this, propertyIndex);
		}

		public string GetPropertyDescription(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyDescription(this, propertyIndex);
		}

		public ShaderPropertyFlags GetPropertyFlags(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyFlags(this, propertyIndex);
		}

		public string[] GetPropertyAttributes(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyAttributes(this, propertyIndex);
		}

		public float GetPropertyDefaultFloatValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Float && propertyType != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Float or Range.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex)[0];
		}

		public Vector4 GetPropertyDefaultVectorValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Color && propertyType != ShaderPropertyType.Vector;
			if (flag)
			{
				throw new ArgumentException("Property type is not Color or Vector.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex);
		}

		public Vector2 GetPropertyRangeLimits(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Range.");
			}
			Vector4 propertyDefaultValue = Shader.GetPropertyDefaultValue(this, propertyIndex);
			return new Vector2(propertyDefaultValue[1], propertyDefaultValue[2]);
		}

		public TextureDimension GetPropertyTextureDimension(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not TexEnv.");
			}
			return Shader.GetPropertyTextureDimension(this, propertyIndex);
		}

		public string GetPropertyTextureDefaultName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.GetPropertyTextureDefaultName(this, propertyIndex);
		}

		public bool FindTextureStack(int propertyIndex, out string stackName, out int layerIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.FindTextureStackImpl(this, propertyIndex, out stackName, out layerIndex);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorImpl_Injected(int name, ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixImpl_Injected(int name, ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalVectorImpl_Injected(int name, out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalMatrixImpl_Injected(int name, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyDefaultValue_Injected(Shader shader, int propertyIndex, out Vector4 ret);
	}
}
