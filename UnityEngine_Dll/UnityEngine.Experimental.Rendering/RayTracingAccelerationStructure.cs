using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Export/Graphics/RayTracingAccelerationStructure.bindings.h"), NativeHeader("Runtime/Shaders/RayTracingAccelerationStructure.h"), UsedByNativeCode]
	public sealed class RayTracingAccelerationStructure : IDisposable
	{
		[Flags]
		public enum RayTracingModeMask
		{
			Nothing = 0,
			Static = 2,
			DynamicTransform = 4,
			DynamicGeometry = 8,
			Everything = 14
		}

		public enum ManagementMode
		{
			Manual,
			Automatic
		}

		public struct RASSettings
		{
			public RayTracingAccelerationStructure.ManagementMode managementMode;

			public RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask;

			public int layerMask;

			public RASSettings(RayTracingAccelerationStructure.ManagementMode sceneManagementMode, RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask, int layerMask)
			{
				this.managementMode = sceneManagementMode;
				this.rayTracingModeMask = rayTracingModeMask;
				this.layerMask = layerMask;
			}
		}

		internal IntPtr m_Ptr;

		~RayTracingAccelerationStructure()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				RayTracingAccelerationStructure.Destroy(this);
			}
			this.m_Ptr = IntPtr.Zero;
		}

		public RayTracingAccelerationStructure(RayTracingAccelerationStructure.RASSettings settings)
		{
			this.m_Ptr = RayTracingAccelerationStructure.Create(settings);
		}

		public RayTracingAccelerationStructure()
		{
			this.m_Ptr = RayTracingAccelerationStructure.Create(new RayTracingAccelerationStructure.RASSettings
			{
				rayTracingModeMask = RayTracingAccelerationStructure.RayTracingModeMask.Everything,
				managementMode = RayTracingAccelerationStructure.ManagementMode.Manual,
				layerMask = -1
			});
		}

		[FreeFunction("RayTracingAccelerationStructure_Bindings::Create")]
		private static IntPtr Create(RayTracingAccelerationStructure.RASSettings desc)
		{
			return RayTracingAccelerationStructure.Create_Injected(ref desc);
		}

		[FreeFunction("RayTracingAccelerationStructure_Bindings::Destroy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(RayTracingAccelerationStructure accelStruct);

		public void Release()
		{
			this.Dispose();
		}

		public void Build()
		{
			this.Build(Vector3.zero);
		}

		[Obsolete("Method Update has been deprecated. Use Build instead (UnityUpgradable) -> Build()", true)]
		public void Update()
		{
			this.Build(Vector3.zero);
		}

		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::Build", HasExplicitThis = true)]
		public void Build(Vector3 relativeOrigin)
		{
			this.Build_Injected(ref relativeOrigin);
		}

		[Obsolete("Method Update has been deprecated. Use Build instead (UnityUpgradable) -> Build(*)", true), FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::Update", HasExplicitThis = true)]
		public void Update(Vector3 relativeOrigin)
		{
			this.Update_Injected(ref relativeOrigin);
		}

		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::AddInstance", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddInstance([NotNull("ArgumentNullException")] Renderer targetRenderer, bool[] subMeshMask = null, bool[] subMeshTransparencyFlags = null, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255u);

		public void AddInstance(GraphicsBuffer aabbBuffer, uint numElements, Material material, bool isCutOff, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255u, bool reuseBounds = false)
		{
			this.AddInstance_Procedural(aabbBuffer, numElements, material, Matrix4x4.identity, isCutOff, enableTriangleCulling, frontTriangleCounterClockwise, mask, reuseBounds);
		}

		public void AddInstance(GraphicsBuffer aabbBuffer, uint numElements, Material material, Matrix4x4 instanceTransform, bool isCutOff, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255u, bool reuseBounds = false)
		{
			this.AddInstance_Procedural(aabbBuffer, numElements, material, instanceTransform, isCutOff, enableTriangleCulling, frontTriangleCounterClockwise, mask, reuseBounds);
		}

		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::AddInstance", HasExplicitThis = true)]
		private void AddInstance_Procedural([NotNull("ArgumentNullException")] GraphicsBuffer aabbBuffer, uint numElements, [NotNull("ArgumentNullException")] Material material, Matrix4x4 instanceTransform, bool isCutOff, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255u, bool reuseBounds = false)
		{
			this.AddInstance_Procedural_Injected(aabbBuffer, numElements, material, ref instanceTransform, isCutOff, enableTriangleCulling, frontTriangleCounterClockwise, mask, reuseBounds);
		}

		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstanceTransform", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateInstanceTransform([NotNull("ArgumentNullException")] Renderer renderer);

		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::GetSize", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ulong GetSize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(ref RayTracingAccelerationStructure.RASSettings desc);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Build_Injected(ref Vector3 relativeOrigin);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Update_Injected(ref Vector3 relativeOrigin);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddInstance_Procedural_Injected(GraphicsBuffer aabbBuffer, uint numElements, Material material, ref Matrix4x4 instanceTransform, bool isCutOff, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255u, bool reuseBounds = false);
	}
}
