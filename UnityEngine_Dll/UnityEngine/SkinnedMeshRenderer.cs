using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Mesh/SkinnedMeshRenderer.h")]
	public class SkinnedMeshRenderer : Renderer
	{
		public extern SkinQuality quality
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool updateWhenOffscreen
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool forceMatrixRecalculationPerRender
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Transform rootBone
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern Transform actualRootBone
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Transform[] bones
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Mesh")]
		public extern Mesh sharedMesh
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("SkinnedMeshMotionVectors")]
		public extern bool skinnedMotionVectors
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Bounds localBounds
		{
			get
			{
				return this.GetLocalAABB();
			}
			set
			{
				this.SetLocalAABB(value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetBlendShapeWeight(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBlendShapeWeight(int index, float value);

		public void BakeMesh(Mesh mesh)
		{
			this.BakeMesh(mesh, false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh([NotNull("NullExceptionObject")] Mesh mesh, bool useScale);

		[FreeFunction(Name = "SkinnedMeshRendererScripting::GetLocalAABB", HasExplicitThis = true)]
		private Bounds GetLocalAABB()
		{
			Bounds result;
			this.GetLocalAABB_Injected(out result);
			return result;
		}

		private void SetLocalAABB(Bounds b)
		{
			this.SetLocalAABB_Injected(ref b);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalAABB_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLocalAABB_Injected(ref Bounds b);
	}
}
