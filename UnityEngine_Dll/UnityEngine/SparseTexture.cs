using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/SparseTexture.h")]
	public sealed class SparseTexture : Texture
	{
		public extern int tileWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int tileHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isCreated
		{
			[NativeName("IsInitialized")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[FreeFunction(Name = "SparseTextureScripting::Create", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] SparseTexture mono, int width, int height, GraphicsFormat format, int mipCount);

		[FreeFunction(Name = "SparseTextureScripting::UpdateTile", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateTile(int tileX, int tileY, int miplevel, Color32[] data);

		[FreeFunction(Name = "SparseTextureScripting::UpdateTileRaw", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateTileRaw(int tileX, int tileY, int miplevel, byte[] data);

		public void UnloadTile(int tileX, int tileY, int miplevel)
		{
			this.UpdateTileRaw(tileX, tileY, miplevel, null);
		}

		internal bool ValidateSize(int width, int height, GraphicsFormat format)
		{
			bool flag = (ulong)GraphicsFormatUtility.GetBlockSize(format) * (ulong)((long)width / (long)((ulong)GraphicsFormatUtility.GetBlockWidth(format))) * (ulong)((long)height / (long)((ulong)GraphicsFormatUtility.GetBlockHeight(format))) < 65536uL;
			bool result;
			if (flag)
			{
				Debug.LogError("SparseTexture creation failed. The minimum size in bytes of a SparseTexture is 64KB.", this);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private static void ValidateIsNotCrunched(TextureFormat textureFormat)
		{
			bool flag = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
			if (flag)
			{
				throw new ArgumentException("Crunched SparseTexture is not supported.");
			}
		}

		public SparseTexture(int width, int height, DefaultFormat format, int mipCount) : this(width, height, SystemInfo.GetGraphicsFormat(format), mipCount)
		{
		}

		public SparseTexture(int width, int height, GraphicsFormat format, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Sample);
			if (!flag)
			{
				bool flag2 = !this.ValidateSize(width, height, format);
				if (!flag2)
				{
					SparseTexture.Internal_Create(this, width, height, format, mipCount);
				}
			}
		}

		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount) : this(width, height, textureFormat, mipCount, false)
		{
		}

		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount, bool linear)
		{
			bool flag = !base.ValidateFormat(textureFormat);
			if (!flag)
			{
				SparseTexture.ValidateIsNotCrunched(textureFormat);
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				bool flag2 = !this.ValidateSize(width, height, graphicsFormat);
				if (!flag2)
				{
					SparseTexture.Internal_Create(this, width, height, graphicsFormat, mipCount);
				}
			}
		}
	}
}
