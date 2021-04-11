using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.U2D
{
	[NativeHeader("Runtime/Graphics/SpriteFrame.h"), NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	public static class SpriteDataAccessExtensions
	{
		private static void CheckAttributeTypeMatchesAndThrow<T>(VertexAttribute channel)
		{
			bool flag;
			switch (channel)
			{
			case VertexAttribute.Position:
			case VertexAttribute.Normal:
				flag = (typeof(T) == typeof(Vector3));
				break;
			case VertexAttribute.Tangent:
				flag = (typeof(T) == typeof(Vector4));
				break;
			case VertexAttribute.Color:
				flag = (typeof(T) == typeof(Color32));
				break;
			case VertexAttribute.TexCoord0:
			case VertexAttribute.TexCoord1:
			case VertexAttribute.TexCoord2:
			case VertexAttribute.TexCoord3:
			case VertexAttribute.TexCoord4:
			case VertexAttribute.TexCoord5:
			case VertexAttribute.TexCoord6:
			case VertexAttribute.TexCoord7:
				flag = (typeof(T) == typeof(Vector2));
				break;
			case VertexAttribute.BlendWeight:
				flag = (typeof(T) == typeof(BoneWeight));
				break;
			default:
				throw new InvalidOperationException(string.Format("The requested channel '{0}' is unknown.", channel));
			}
			bool flag2 = !flag;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The requested channel '{0}' does not match the return type {1}.", channel, typeof(T).Name));
			}
		}

		public unsafe static NativeSlice<T> GetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteChannelInfo channelInfo = SpriteDataAccessExtensions.GetChannelInfo(sprite, channel);
			byte* dataPointer = (byte*)channelInfo.buffer + channelInfo.offset;
			NativeSlice<T> result = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>((void*)dataPointer, channelInfo.stride, channelInfo.count);
			NativeSliceUnsafeUtility.SetAtomicSafetyHandle<T>(ref result, sprite.GetSafetyHandle());
			return result;
		}

		public static void SetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel, NativeArray<T> src) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteDataAccessExtensions.SetChannelData(sprite, channel, src.GetUnsafeReadOnlyPtr<T>());
		}

		public static NativeArray<Matrix4x4> GetBindPoses(this Sprite sprite)
		{
			SpriteChannelInfo bindPoseInfo = SpriteDataAccessExtensions.GetBindPoseInfo(sprite);
			NativeArray<Matrix4x4> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(bindPoseInfo.buffer, bindPoseInfo.count, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<Matrix4x4>(ref result, sprite.GetSafetyHandle());
			return result;
		}

		public static void SetBindPoses(this Sprite sprite, NativeArray<Matrix4x4> src)
		{
			SpriteDataAccessExtensions.SetBindPoseData(sprite, src.GetUnsafeReadOnlyPtr<Matrix4x4>(), src.Length);
		}

		public static NativeArray<ushort> GetIndices(this Sprite sprite)
		{
			SpriteChannelInfo indicesInfo = SpriteDataAccessExtensions.GetIndicesInfo(sprite);
			NativeArray<ushort> result = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ushort>(indicesInfo.buffer, indicesInfo.count, Allocator.Invalid);
			NativeArrayUnsafeUtility.SetAtomicSafetyHandle<ushort>(ref result, sprite.GetSafetyHandle());
			return result;
		}

		public static void SetIndices(this Sprite sprite, NativeArray<ushort> src)
		{
			SpriteDataAccessExtensions.SetIndicesData(sprite, src.GetUnsafeReadOnlyPtr<ushort>(), src.Length);
		}

		public static SpriteBone[] GetBones(this Sprite sprite)
		{
			return SpriteDataAccessExtensions.GetBoneInfo(sprite);
		}

		public static void SetBones(this Sprite sprite, SpriteBone[] src)
		{
			SpriteDataAccessExtensions.SetBoneData(sprite, src);
		}

		[NativeName("HasChannel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasVertexAttribute([NotNull("ArgumentNullException")] this Sprite sprite, VertexAttribute channel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetVertexCount([NotNull("ArgumentNullException")] this Sprite sprite, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetVertexCount([NotNull("ArgumentNullException")] this Sprite sprite);

		private static SpriteChannelInfo GetBindPoseInfo([NotNull("ArgumentNullException")] Sprite sprite)
		{
			SpriteChannelInfo result;
			SpriteDataAccessExtensions.GetBindPoseInfo_Injected(sprite, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBindPoseData([NotNull("ArgumentNullException")] Sprite sprite, void* src, int count);

		private static SpriteChannelInfo GetIndicesInfo([NotNull("ArgumentNullException")] Sprite sprite)
		{
			SpriteChannelInfo result;
			SpriteDataAccessExtensions.GetIndicesInfo_Injected(sprite, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetIndicesData([NotNull("ArgumentNullException")] Sprite sprite, void* src, int count);

		private static SpriteChannelInfo GetChannelInfo([NotNull("ArgumentNullException")] Sprite sprite, VertexAttribute channel)
		{
			SpriteChannelInfo result;
			SpriteDataAccessExtensions.GetChannelInfo_Injected(sprite, channel, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetChannelData([NotNull("ArgumentNullException")] Sprite sprite, VertexAttribute channel, void* src);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteBone[] GetBoneInfo([NotNull("ArgumentNullException")] Sprite sprite);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoneData([NotNull("ArgumentNullException")] Sprite sprite, SpriteBone[] src);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetPrimaryVertexStreamSize(Sprite sprite);

		internal static AtomicSafetyHandle GetSafetyHandle([NotNull("ArgumentNullException")] this Sprite sprite)
		{
			AtomicSafetyHandle result;
			SpriteDataAccessExtensions.GetSafetyHandle_Injected(sprite, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBindPoseInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetChannelInfo_Injected(Sprite sprite, VertexAttribute channel, out SpriteChannelInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSafetyHandle_Injected(Sprite sprite, out AtomicSafetyHandle ret);
	}
}
