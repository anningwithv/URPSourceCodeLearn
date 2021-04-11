using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	[NativeType(Header = "Modules/SpriteShape/Public/SpriteShapeRenderer.h"), MovedFrom("UnityEngine.Experimental.U2D")]
	public class SpriteShapeRenderer : Renderer
	{
		public Color color
		{
			get
			{
				Color result;
				this.get_color_Injected(out result);
				return result;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetVertexCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetIndexCount();

		internal Bounds GetLocalAABB()
		{
			Bounds result;
			this.GetLocalAABB_Injected(out result);
			return result;
		}

		public void Prepare(JobHandle handle, SpriteShapeParameters shapeParams, Sprite[] sprites)
		{
			this.Prepare_Injected(ref handle, ref shapeParams, sprites);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RefreshSafetyHandle(SpriteShapeDataType arrayType);

		private AtomicSafetyHandle GetSafetyHandle(SpriteShapeDataType arrayType)
		{
			AtomicSafetyHandle result;
			this.GetSafetyHandle_Injected(arrayType, out result);
			return result;
		}

		private NativeArray<T> GetNativeDataArray<T>(SpriteShapeDataType dataType) where T : struct
		{
			this.RefreshSafetyHandle(dataType);
			SpriteChannelInfo dataInfo = this.GetDataInfo(dataType);
			NativeArray<T> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataInfo.buffer, dataInfo.count, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.GetSafetyHandle(dataType));
			return result;
		}

		private unsafe NativeSlice<T> GetChannelDataArray<T>(SpriteShapeDataType dataType, VertexAttribute channel) where T : struct
		{
			this.RefreshSafetyHandle(dataType);
			SpriteChannelInfo channelInfo = this.GetChannelInfo(channel);
			byte* dataPointer = (byte*)channelInfo.buffer + channelInfo.offset;
			NativeSlice<T> result = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>((void*)dataPointer, channelInfo.stride, channelInfo.count);
			NativeSliceUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, this.GetSafetyHandle(dataType));
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSegmentCount(int geomCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMeshDataCount(int vertexCount, int indexCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMeshChannelInfo(int vertexCount, int indexCount, int hotChannelMask);

		private SpriteChannelInfo GetDataInfo(SpriteShapeDataType arrayType)
		{
			SpriteChannelInfo result;
			this.GetDataInfo_Injected(arrayType, out result);
			return result;
		}

		private SpriteChannelInfo GetChannelInfo(VertexAttribute channel)
		{
			SpriteChannelInfo result;
			this.GetChannelInfo_Injected(channel, out result);
			return result;
		}

		public NativeArray<Bounds> GetBounds()
		{
			return this.GetNativeDataArray<Bounds>(SpriteShapeDataType.BoundingBox);
		}

		public NativeArray<SpriteShapeSegment> GetSegments(int dataSize)
		{
			this.SetSegmentCount(dataSize);
			return this.GetNativeDataArray<SpriteShapeSegment>(SpriteShapeDataType.Segment);
		}

		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords)
		{
			this.SetMeshDataCount(dataSize, dataSize);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
		}

		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Vector4> tangents)
		{
			this.SetMeshChannelInfo(dataSize, dataSize, 4);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
			tangents = this.GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
		}

		public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Vector4> tangents, out NativeSlice<Vector3> normals)
		{
			this.SetMeshChannelInfo(dataSize, dataSize, 6);
			indices = this.GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
			vertices = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
			texcoords = this.GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
			tangents = this.GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
			normals = this.GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelNormal, VertexAttribute.Normal);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_color_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_color_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalAABB_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Prepare_Injected(ref JobHandle handle, ref SpriteShapeParameters shapeParams, Sprite[] sprites);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSafetyHandle_Injected(SpriteShapeDataType arrayType, out AtomicSafetyHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetDataInfo_Injected(SpriteShapeDataType arrayType, out SpriteChannelInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetChannelInfo_Injected(VertexAttribute channel, out SpriteChannelInfo ret);
	}
}
