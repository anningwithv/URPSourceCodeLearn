using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct MeshGraphicsTestHelper
	{
		[FreeFunction("MeshGraphicsTestHelper::InternalEnableComputeBufferBindings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalEnableComputeBufferBindings([NotNull("ArgumentNullException")] Mesh mesh);

		[FreeFunction("MeshGraphicsTestHelper::InternalEnableComputeBufferBindingsSkinned")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalEnableComputeBufferBindingsSkinned([NotNull("ArgumentNullException")] SkinnedMeshRenderer mesh);

		[FreeFunction("MeshGraphicsTestHelper::InternalAssignComputeBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalAssignComputeBuffer([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] ComputeShader shader, int kernelIndex, int iboTargetID, int vboTargetID);

		[FreeFunction("MeshGraphicsTestHelper::InternalAssignComputeBufferSkinned")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalAssignComputeBufferSkinned([NotNull("ArgumentNullException")] SkinnedMeshRenderer mesh, [NotNull("ArgumentNullException")] ComputeShader shader, int kernelIndex, int vboTargetID);

		[FreeFunction("MeshGraphicsTestHelper::InternalSetChannelInfo")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalSetChannelInfo([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] ComputeShader shader, int kernelIndex, int vertexStrideID, int positionOffsetID, int normalOffsetID, int uvOffsetID, int colorOffsetID);
	}
}
