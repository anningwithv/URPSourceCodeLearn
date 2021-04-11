using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_XR"), NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshingSubsystem.h"), NativeHeader("Modules/XR/XRPrefix.h"), UsedByNativeCode]
	public class XRMeshSubsystem : IntegratedSubsystem<XRMeshSubsystemDescriptor>
	{
		public extern float meshDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool TryGetMeshInfos(List<MeshInfo> meshInfosOut)
		{
			bool flag = meshInfosOut == null;
			if (flag)
			{
				throw new ArgumentNullException("meshInfosOut");
			}
			return this.GetMeshInfosAsList(meshInfosOut);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetMeshInfosAsList(List<MeshInfo> meshInfos);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MeshInfo[] GetMeshInfosAsFixedArray();

		public void GenerateMeshAsync(MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			this.GenerateMeshAsync_Injected(ref meshId, mesh, meshCollider, attributes, onMeshGenerationComplete);
		}

		[RequiredByNativeCode]
		private void InvokeMeshReadyDelegate(MeshGenerationResult result, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			bool flag = onMeshGenerationComplete != null;
			if (flag)
			{
				onMeshGenerationComplete(result);
			}
		}

		public bool SetBoundingVolume(Vector3 origin, Vector3 extents)
		{
			return this.SetBoundingVolume_Injected(ref origin, ref extents);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GenerateMeshAsync_Injected(ref MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetBoundingVolume_Injected(ref Vector3 origin, ref Vector3 extents);
	}
}
