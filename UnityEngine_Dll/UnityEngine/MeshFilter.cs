using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Mesh/MeshFilter.h"), RequireComponent(typeof(Transform))]
	public sealed class MeshFilter : Component
	{
		public extern Mesh sharedMesh
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Mesh mesh
		{
			[NativeName("GetInstantiatedMeshFromScript")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetInstantiatedMesh")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		private void DontStripMeshFilter()
		{
		}
	}
}
