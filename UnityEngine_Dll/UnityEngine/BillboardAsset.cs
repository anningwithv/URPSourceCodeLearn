using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Billboard/BillboardAsset.h"), NativeHeader("Runtime/Export/Graphics/BillboardRenderer.bindings.h")]
	public sealed class BillboardAsset : Object
	{
		public extern float width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float bottom
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int imageCount
		{
			[NativeMethod("GetNumImages")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int vertexCount
		{
			[NativeMethod("GetNumVertices")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int indexCount
		{
			[NativeMethod("GetNumIndices")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Material material
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public BillboardAsset()
		{
			BillboardAsset.Internal_Create(this);
		}

		[FreeFunction(Name = "BillboardRenderer_Bindings::Internal_Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] BillboardAsset obj);

		public void GetImageTexCoords(List<Vector4> imageTexCoords)
		{
			bool flag = imageTexCoords == null;
			if (flag)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			this.GetImageTexCoordsInternal(imageTexCoords);
		}

		[NativeMethod("GetBillboardDataReadonly().GetImageTexCoords")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector4[] GetImageTexCoords();

		[FreeFunction(Name = "BillboardRenderer_Bindings::GetImageTexCoordsInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetImageTexCoordsInternal(object list);

		public void SetImageTexCoords(List<Vector4> imageTexCoords)
		{
			bool flag = imageTexCoords == null;
			if (flag)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			this.SetImageTexCoordsInternalList(imageTexCoords);
		}

		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetImageTexCoords([NotNull("ArgumentNullException")] Vector4[] imageTexCoords);

		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoordsInternalList", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetImageTexCoordsInternalList(object list);

		public void GetVertices(List<Vector2> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices");
			}
			this.GetVerticesInternal(vertices);
		}

		[NativeMethod("GetBillboardDataReadonly().GetVertices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector2[] GetVertices();

		[FreeFunction(Name = "BillboardRenderer_Bindings::GetVerticesInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetVerticesInternal(object list);

		public void SetVertices(List<Vector2> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices");
			}
			this.SetVerticesInternalList(vertices);
		}

		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVertices", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVertices([NotNull("ArgumentNullException")] Vector2[] vertices);

		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVerticesInternalList", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetVerticesInternalList(object list);

		public void GetIndices(List<ushort> indices)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices");
			}
			this.GetIndicesInternal(indices);
		}

		[NativeMethod("GetBillboardDataReadonly().GetIndices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort[] GetIndices();

		[FreeFunction(Name = "BillboardRenderer_Bindings::GetIndicesInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetIndicesInternal(object list);

		public void SetIndices(List<ushort> indices)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices");
			}
			this.SetIndicesInternalList(indices);
		}

		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndices", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetIndices([NotNull("ArgumentNullException")] ushort[] indices);

		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndicesInternalList", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetIndicesInternalList(object list);

		[FreeFunction(Name = "BillboardRenderer_Bindings::MakeMaterialProperties", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void MakeMaterialProperties(MaterialPropertyBlock properties, Camera camera);
	}
}
