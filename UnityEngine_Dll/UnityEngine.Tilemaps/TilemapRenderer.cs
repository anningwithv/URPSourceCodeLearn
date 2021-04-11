using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.U2D;

namespace UnityEngine.Tilemaps
{
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h"), NativeHeader("Modules/Tilemap/TilemapRendererJobs.h"), NativeHeader("Modules/Grid/Public/GridMarshalling.h"), NativeType(Header = "Modules/Tilemap/Public/TilemapRenderer.h"), RequireComponent(typeof(Tilemap))]
	public sealed class TilemapRenderer : Renderer
	{
		public enum SortOrder
		{
			BottomLeft,
			BottomRight,
			TopLeft,
			TopRight
		}

		public enum Mode
		{
			Chunk,
			Individual
		}

		public enum DetectChunkCullingBounds
		{
			Auto,
			Manual
		}

		public Vector3Int chunkSize
		{
			get
			{
				Vector3Int result;
				this.get_chunkSize_Injected(out result);
				return result;
			}
			set
			{
				this.set_chunkSize_Injected(ref value);
			}
		}

		public Vector3 chunkCullingBounds
		{
			[FreeFunction("TilemapRendererBindings::GetChunkCullingBounds", HasExplicitThis = true)]
			get
			{
				Vector3 result;
				this.get_chunkCullingBounds_Injected(out result);
				return result;
			}
			[FreeFunction("TilemapRendererBindings::SetChunkCullingBounds", HasExplicitThis = true)]
			set
			{
				this.set_chunkCullingBounds_Injected(ref value);
			}
		}

		public extern int maxChunkCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int maxFrameAge
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TilemapRenderer.SortOrder sortOrder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("RenderMode")]
		public extern TilemapRenderer.Mode mode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TilemapRenderer.DetectChunkCullingBounds detectChunkCullingBounds
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		internal void RegisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered += new Action<SpriteAtlas>(this.OnSpriteAtlasRegistered);
		}

		[RequiredByNativeCode]
		internal void UnregisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered -= new Action<SpriteAtlas>(this.OnSpriteAtlasRegistered);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void OnSpriteAtlasRegistered(SpriteAtlas atlas);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_chunkSize_Injected(out Vector3Int ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_chunkSize_Injected(ref Vector3Int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_chunkCullingBounds_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_chunkCullingBounds_Injected(ref Vector3 value);
	}
}
