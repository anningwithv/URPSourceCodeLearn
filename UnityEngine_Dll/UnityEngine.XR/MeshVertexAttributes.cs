using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[Flags, NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h"), UsedByNativeCode]
	public enum MeshVertexAttributes
	{
		None = 0,
		Normals = 1,
		Tangents = 2,
		UVs = 4,
		Colors = 8
	}
}
