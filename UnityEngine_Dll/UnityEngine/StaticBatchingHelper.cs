using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct StaticBatchingHelper
	{
		[FreeFunction("MeshScripting::CombineMeshVerticesForStaticBatching")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Mesh InternalCombineVertices(MeshSubsetCombineUtility.MeshInstance[] meshes, string meshName);

		[FreeFunction("MeshScripting::CombineMeshIndicesForStaticBatching")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalCombineIndices(MeshSubsetCombineUtility.SubMeshInstance[] submeshes, Mesh combinedMesh);
	}
}
