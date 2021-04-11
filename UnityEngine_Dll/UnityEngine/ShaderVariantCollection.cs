using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	public sealed class ShaderVariantCollection : Object
	{
		public struct ShaderVariant
		{
			public Shader shader;

			public PassType passType;

			public string[] keywords;

			[FreeFunction, NativeConditional("UNITY_EDITOR")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern string CheckShaderVariant(Shader shader, PassType passType, string[] keywords);

			public ShaderVariant(Shader shader, PassType passType, params string[] keywords)
			{
				this.shader = shader;
				this.passType = passType;
				this.keywords = keywords;
				string text = ShaderVariantCollection.ShaderVariant.CheckShaderVariant(shader, passType, keywords);
				bool flag = !string.IsNullOrEmpty(text);
				if (flag)
				{
					throw new ArgumentException(text);
				}
			}
		}

		public extern int shaderCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int variantCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isWarmedUp
		{
			[NativeName("IsWarmedUp")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool AddVariant(Shader shader, PassType passType, string[] keywords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool RemoveVariant(Shader shader, PassType passType, string[] keywords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ContainsVariant(Shader shader, PassType passType, string[] keywords);

		[NativeName("ClearVariants")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		[NativeName("WarmupShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WarmUp();

		[NativeName("CreateFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ShaderVariantCollection svc);

		public ShaderVariantCollection()
		{
			ShaderVariantCollection.Internal_Create(this);
		}

		public bool Add(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.AddVariant(variant.shader, variant.passType, variant.keywords);
		}

		public bool Remove(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.RemoveVariant(variant.shader, variant.passType, variant.keywords);
		}

		public bool Contains(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.ContainsVariant(variant.shader, variant.passType, variant.keywords);
		}
	}
}
