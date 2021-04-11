using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h"), NativeHeader("Runtime/Shaders/Material.h")]
	public class Material : Object
	{
		public extern Shader shader
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color color
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Color color;
				if (flag)
				{
					color = this.GetColor(firstPropertyNameIdByAttribute);
				}
				else
				{
					color = this.GetColor("_Color");
				}
				return color;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetColor(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetColor("_Color", value);
				}
			}
		}

		public Texture mainTexture
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Texture texture;
				if (flag)
				{
					texture = this.GetTexture(firstPropertyNameIdByAttribute);
				}
				else
				{
					texture = this.GetTexture("_MainTex");
				}
				return texture;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTexture(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTexture("_MainTex", value);
				}
			}
		}

		public Vector2 mainTextureOffset
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 textureOffset;
				if (flag)
				{
					textureOffset = this.GetTextureOffset(firstPropertyNameIdByAttribute);
				}
				else
				{
					textureOffset = this.GetTextureOffset("_MainTex");
				}
				return textureOffset;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureOffset(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureOffset("_MainTex", value);
				}
			}
		}

		public Vector2 mainTextureScale
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 textureScale;
				if (flag)
				{
					textureScale = this.GetTextureScale(firstPropertyNameIdByAttribute);
				}
				else
				{
					textureScale = this.GetTextureScale("_MainTex");
				}
				return textureScale;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureScale(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureScale("_MainTex", value);
				}
			}
		}

		public extern int renderQueue
		{
			[NativeName("GetActualRenderQueue")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetCustomRenderQueue")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern int rawRenderQueue
		{
			[NativeName("GetCustomRenderQueue")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern MaterialGlobalIlluminationFlags globalIlluminationFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool doubleSidedGI
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("EnableInstancingVariants")]
		public extern bool enableInstancing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int passCount
		{
			[NativeName("GetShader()->GetPassCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

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

		[Obsolete("Creating materials from shader source string will be removed in the future. Use Shader assets instead.", false)]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		[FreeFunction("MaterialScripting::CreateWithShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithShader([Writable] Material self, [NotNull("ArgumentNullException")] Shader shader);

		[FreeFunction("MaterialScripting::CreateWithMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithMaterial([Writable] Material self, [NotNull("ArgumentNullException")] Material source);

		[FreeFunction("MaterialScripting::CreateWithString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithString([Writable] Material self);

		public Material(Shader shader)
		{
			Material.CreateWithShader(this, shader);
		}

		[RequiredByNativeCode]
		public Material(Material source)
		{
			Material.CreateWithMaterial(this, source);
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Creating materials from shader source string is no longer supported. Use Shader assets instead.", false)]
		public Material(string contents)
		{
			Material.CreateWithString(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Material GetDefaultMaterial();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Material GetDefaultParticleMaterial();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Material GetDefaultLineMaterial();

		[NativeName("GetFirstPropertyNameIdByAttributeFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags attributeFlag);

		[NativeName("HasPropertyFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasProperty(int nameID);

		public bool HasProperty(string name)
		{
			return this.HasProperty(Shader.PropertyToID(name));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableKeyword(string keyword);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableKeyword(string keyword);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsKeywordEnabled(string keyword);

		[FreeFunction("MaterialScripting::SetShaderPassEnabled", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetShaderPassEnabled(string passName, bool enabled);

		[FreeFunction("MaterialScripting::GetShaderPassEnabled", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetShaderPassEnabled(string passName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetPassName(int pass);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int FindPass(string passName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetOverrideTag(string tag, string val);

		[NativeName("GetTag")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetTagImpl(string tag, bool currentSubShaderOnly, string defaultValue);

		public string GetTag(string tag, bool searchFallbacks, string defaultValue)
		{
			return this.GetTagImpl(tag, !searchFallbacks, defaultValue);
		}

		public string GetTag(string tag, bool searchFallbacks)
		{
			return this.GetTagImpl(tag, !searchFallbacks, "");
		}

		[FreeFunction("MaterialScripting::Lerp", HasExplicitThis = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Lerp(Material start, Material end, float t);

		[FreeFunction("MaterialScripting::SetPass", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPass(int pass);

		[FreeFunction("MaterialScripting::CopyPropertiesFrom", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyPropertiesFromMaterial(Material mat);

		[FreeFunction("MaterialScripting::GetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string[] GetShaderKeywords();

		[FreeFunction("MaterialScripting::SetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetShaderKeywords(string[] names);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int ComputeCRC();

		[FreeFunction("MaterialScripting::GetTexturePropertyNames", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetTexturePropertyNames();

		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDs", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetTexturePropertyNameIDs();

		[FreeFunction("MaterialScripting::GetTexturePropertyNamesInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTexturePropertyNamesInternal(object outNames);

		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDsInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTexturePropertyNameIDsInternal(object outNames);

		public void GetTexturePropertyNames(List<string> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNamesInternal(outNames);
		}

		public void GetTexturePropertyNameIDs(List<int> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNameIDsInternal(outNames);
		}

		[NativeName("SetFloatFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatImpl(int name, float value);

		[NativeName("SetColorFromScript")]
		private void SetColorImpl(int name, Color value)
		{
			this.SetColorImpl_Injected(name, ref value);
		}

		[NativeName("SetMatrixFromScript")]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			this.SetMatrixImpl_Injected(name, ref value);
		}

		[NativeName("SetTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTextureImpl(int name, Texture value);

		[NativeName("SetRenderTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element);

		[NativeName("SetBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBufferImpl(int name, ComputeBuffer value);

		[NativeName("SetGraphicsBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGraphicsBufferImpl(int name, GraphicsBuffer value);

		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		[NativeName("SetConstantGraphicsBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		[NativeName("GetFloatFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatImpl(int name);

		[NativeName("GetColorFromScript")]
		private Color GetColorImpl(int name)
		{
			Color result;
			this.GetColorImpl_Injected(name, out result);
			return result;
		}

		[NativeName("GetMatrixFromScript")]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			Matrix4x4 result;
			this.GetMatrixImpl_Injected(name, out result);
			return result;
		}

		[NativeName("GetTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture GetTextureImpl(int name);

		[FreeFunction(Name = "MaterialScripting::SetFloatArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatArrayImpl(int name, float[] values, int count);

		[FreeFunction(Name = "MaterialScripting::SetVectorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVectorArrayImpl(int name, Vector4[] values, int count);

		[FreeFunction(Name = "MaterialScripting::SetColorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorArrayImpl(int name, Color[] values, int count);

		[FreeFunction(Name = "MaterialScripting::SetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		[FreeFunction(Name = "MaterialScripting::GetFloatArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[] GetFloatArrayImpl(int name);

		[FreeFunction(Name = "MaterialScripting::GetVectorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector4[] GetVectorArrayImpl(int name);

		[FreeFunction(Name = "MaterialScripting::GetColorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Color[] GetColorArrayImpl(int name);

		[FreeFunction(Name = "MaterialScripting::GetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Matrix4x4[] GetMatrixArrayImpl(int name);

		[FreeFunction(Name = "MaterialScripting::GetFloatArrayCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetFloatArrayCountImpl(int name);

		[FreeFunction(Name = "MaterialScripting::GetVectorArrayCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetVectorArrayCountImpl(int name);

		[FreeFunction(Name = "MaterialScripting::GetColorArrayCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetColorArrayCountImpl(int name);

		[FreeFunction(Name = "MaterialScripting::GetMatrixArrayCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetMatrixArrayCountImpl(int name);

		[FreeFunction(Name = "MaterialScripting::ExtractFloatArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractFloatArrayImpl(int name, [Out] float[] val);

		[FreeFunction(Name = "MaterialScripting::ExtractVectorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractVectorArrayImpl(int name, [Out] Vector4[] val);

		[FreeFunction(Name = "MaterialScripting::ExtractColorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractColorArrayImpl(int name, [Out] Color[] val);

		[FreeFunction(Name = "MaterialScripting::ExtractMatrixArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		[NativeName("GetTextureScaleAndOffsetFromScript")]
		private Vector4 GetTextureScaleAndOffsetImpl(int name)
		{
			Vector4 result;
			this.GetTextureScaleAndOffsetImpl_Injected(name, out result);
			return result;
		}

		[NativeName("SetTextureOffsetFromScript")]
		private void SetTextureOffsetImpl(int name, Vector2 offset)
		{
			this.SetTextureOffsetImpl_Injected(name, ref offset);
		}

		[NativeName("SetTextureScaleFromScript")]
		private void SetTextureScaleImpl(int name, Vector2 scale)
		{
			this.SetTextureScaleImpl_Injected(name, ref scale);
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

		private void SetColorArray(int name, Color[] values, int count)
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
			this.SetColorArrayImpl(name, values, count);
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

		private void ExtractColorArray(int name, List<Color> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int colorArrayCountImpl = this.GetColorArrayCountImpl(name);
			bool flag2 = colorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Color>(values, colorArrayCountImpl);
				this.ExtractColorArrayImpl(name, (Color[])NoAllocHelpers.ExtractArrayFromList(values));
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

		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		public void SetVector(string name, Vector4 value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		public void SetVector(int nameID, Vector4 value)
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

		public void SetColorArray(string name, List<Color> values)
		{
			this.SetColorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Color>(values), values.Count);
		}

		public void SetColorArray(int nameID, List<Color> values)
		{
			this.SetColorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Color>(values), values.Count);
		}

		public void SetColorArray(string name, Color[] values)
		{
			this.SetColorArray(Shader.PropertyToID(name), values, values.Length);
		}

		public void SetColorArray(int nameID, Color[] values)
		{
			this.SetColorArray(nameID, values, values.Length);
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

		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		public Vector4 GetVector(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		public Vector4 GetVector(int nameID)
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

		public Color[] GetColorArray(string name)
		{
			return this.GetColorArray(Shader.PropertyToID(name));
		}

		public Color[] GetColorArray(int nameID)
		{
			return (this.GetColorArrayCountImpl(nameID) != 0) ? this.GetColorArrayImpl(nameID) : null;
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

		public void GetColorArray(string name, List<Color> values)
		{
			this.ExtractColorArray(Shader.PropertyToID(name), values);
		}

		public void GetColorArray(int nameID, List<Color> values)
		{
			this.ExtractColorArray(nameID, values);
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

		public void SetTextureOffset(string name, Vector2 value)
		{
			this.SetTextureOffsetImpl(Shader.PropertyToID(name), value);
		}

		public void SetTextureOffset(int nameID, Vector2 value)
		{
			this.SetTextureOffsetImpl(nameID, value);
		}

		public void SetTextureScale(string name, Vector2 value)
		{
			this.SetTextureScaleImpl(Shader.PropertyToID(name), value);
		}

		public void SetTextureScale(int nameID, Vector2 value)
		{
			this.SetTextureScaleImpl(nameID, value);
		}

		public Vector2 GetTextureOffset(string name)
		{
			return this.GetTextureOffset(Shader.PropertyToID(name));
		}

		public Vector2 GetTextureOffset(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.z, textureScaleAndOffsetImpl.w);
		}

		public Vector2 GetTextureScale(string name)
		{
			return this.GetTextureScale(Shader.PropertyToID(name));
		}

		public Vector2 GetTextureScale(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.x, textureScaleAndOffsetImpl.y);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorImpl_Injected(int name, ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrixImpl_Injected(int name, ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColorImpl_Injected(int name, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrixImpl_Injected(int name, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTextureScaleAndOffsetImpl_Injected(int name, out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTextureOffsetImpl_Injected(int name, ref Vector2 offset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTextureScaleImpl_Injected(int name, ref Vector2 scale);
	}
}
