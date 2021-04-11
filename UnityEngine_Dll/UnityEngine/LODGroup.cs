using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/LOD/LODUtility.h"), NativeHeader("Runtime/Graphics/LOD/LODGroupManager.h"), NativeHeader("Runtime/Graphics/LOD/LODGroup.h"), StaticAccessor("GetLODGroupManager()", StaticAccessorType.Dot)]
	public class LODGroup : Component
	{
		public Vector3 localReferencePoint
		{
			get
			{
				Vector3 result;
				this.get_localReferencePoint_Injected(out result);
				return result;
			}
			set
			{
				this.set_localReferencePoint_Injected(ref value);
			}
		}

		public extern float size
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int lodCount
		{
			[NativeMethod("GetLODCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern LODFadeMode fadeMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool animateCrossFading
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetLODGroupManager()")]
		public static extern float crossFadeAnimationDuration
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal Vector3 worldReferencePoint
		{
			get
			{
				Vector3 result;
				this.get_worldReferencePoint_Injected(out result);
				return result;
			}
		}

		[FreeFunction("UpdateLODGroupBoundingBox", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateBounds();

		[FreeFunction("GetLODs_Binding", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern LOD[] GetLODs();

		[Obsolete("Use SetLODs instead.")]
		public void SetLODS(LOD[] lods)
		{
			this.SetLODs(lods);
		}

		[FreeFunction("SetLODs_Binding", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLODs(LOD[] lods);

		[FreeFunction("ForceLODLevel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ForceLOD(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localReferencePoint_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localReferencePoint_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldReferencePoint_Injected(out Vector3 ret);
	}
}
