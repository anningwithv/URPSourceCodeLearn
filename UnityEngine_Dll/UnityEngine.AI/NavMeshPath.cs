using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/NavMeshPath.bindings.h"), MovedFrom("UnityEngine")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class NavMeshPath
	{
		internal IntPtr m_Ptr;

		internal Vector3[] m_Corners;

		public Vector3[] corners
		{
			get
			{
				this.CalculateCorners();
				return this.m_Corners;
			}
		}

		public extern NavMeshPathStatus status
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public NavMeshPath()
		{
			this.m_Ptr = NavMeshPath.InitializeNavMeshPath();
		}

		~NavMeshPath()
		{
			NavMeshPath.DestroyNavMeshPath(this.m_Ptr);
			this.m_Ptr = IntPtr.Zero;
		}

		[FreeFunction("NavMeshPathScriptBindings::InitializeNavMeshPath")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitializeNavMeshPath();

		[FreeFunction("NavMeshPathScriptBindings::DestroyNavMeshPath", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyNavMeshPath(IntPtr ptr);

		[FreeFunction("NavMeshPathScriptBindings::GetCornersNonAlloc", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetCornersNonAlloc([Out] Vector3[] results);

		[FreeFunction("NavMeshPathScriptBindings::CalculateCornersInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector3[] CalculateCornersInternal();

		[FreeFunction("NavMeshPathScriptBindings::ClearCornersInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClearCornersInternal();

		public void ClearCorners()
		{
			this.ClearCornersInternal();
			this.m_Corners = null;
		}

		private void CalculateCorners()
		{
			bool flag = this.m_Corners == null;
			if (flag)
			{
				this.m_Corners = this.CalculateCornersInternal();
			}
		}
	}
}
