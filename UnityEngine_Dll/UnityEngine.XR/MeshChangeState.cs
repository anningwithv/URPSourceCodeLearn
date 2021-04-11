using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h"), UsedByNativeCode]
	public enum MeshChangeState
	{
		Added,
		Updated,
		Removed,
		Unchanged
	}
}
