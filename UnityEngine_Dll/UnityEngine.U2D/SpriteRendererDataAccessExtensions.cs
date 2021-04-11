using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	[NativeHeader("Runtime/Graphics/Mesh/SpriteRenderer.h"), NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	public static class SpriteRendererDataAccessExtensions
	{
		internal static void SetDeformableBuffer(this SpriteRenderer spriteRenderer, NativeArray<byte> src)
		{
			bool flag = spriteRenderer.sprite == null;
			if (flag)
			{
				throw new ArgumentException(string.Format("spriteRenderer does not have a valid sprite set.", new object[0]));
			}
			bool flag2 = src.Length != SpriteDataAccessExtensions.GetPrimaryVertexStreamSize(spriteRenderer.sprite);
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("custom sprite vertex data size must match sprite asset's vertex data size {0} {1}", src.Length, SpriteDataAccessExtensions.GetPrimaryVertexStreamSize(spriteRenderer.sprite)));
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer(spriteRenderer, src.GetUnsafeReadOnlyPtr<byte>(), src.Length);
		}

		internal static void SetDeformableBuffer(this SpriteRenderer spriteRenderer, NativeArray<Vector3> src)
		{
			bool flag = spriteRenderer.sprite == null;
			if (flag)
			{
				throw new InvalidOperationException("spriteRenderer does not have a valid sprite set.");
			}
			bool flag2 = src.Length != spriteRenderer.sprite.GetVertexCount();
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The src length {0} must match the vertex count of source Sprite {1}.", src.Length, spriteRenderer.sprite.GetVertexCount()));
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer(spriteRenderer, src.GetUnsafeReadOnlyPtr<Vector3>(), src.Length);
		}

		internal static void SetBatchDeformableBufferAndLocalAABBArray(SpriteRenderer[] spriteRenderers, NativeArray<IntPtr> buffers, NativeArray<int> bufferSizes, NativeArray<Bounds> bounds)
		{
			int num = spriteRenderers.Length;
			bool flag = num != buffers.Length || num != bufferSizes.Length || num != bounds.Length;
			if (flag)
			{
				throw new ArgumentException("Input array sizes are not the same.");
			}
			SpriteRendererDataAccessExtensions.SetBatchDeformableBufferAndLocalAABBArray(spriteRenderers, buffers.GetUnsafeReadOnlyPtr<IntPtr>(), bufferSizes.GetUnsafeReadOnlyPtr<int>(), bounds.GetUnsafeReadOnlyPtr<Bounds>(), num);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeactivateDeformableBuffer([NotNull("ArgumentNullException")] this SpriteRenderer renderer);

		internal static void SetLocalAABB([NotNull("ArgumentNullException")] this SpriteRenderer renderer, Bounds aabb)
		{
			SpriteRendererDataAccessExtensions.SetLocalAABB_Injected(renderer, ref aabb);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetDeformableBuffer([NotNull("ArgumentNullException")] SpriteRenderer spriteRenderer, void* src, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBatchDeformableBufferAndLocalAABBArray(SpriteRenderer[] spriteRenderers, void* buffers, void* bufferSizes, void* bounds, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalAABB_Injected(SpriteRenderer renderer, ref Bounds aabb);
	}
}
