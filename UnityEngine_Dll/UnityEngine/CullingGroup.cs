using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Camera/CullingGroup.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class CullingGroup : IDisposable
	{
		public delegate void StateChanged(CullingGroupEvent sphere);

		internal IntPtr m_Ptr;

		private CullingGroup.StateChanged m_OnStateChanged = null;

		public CullingGroup.StateChanged onStateChanged
		{
			get
			{
				return this.m_OnStateChanged;
			}
			set
			{
				this.m_OnStateChanged = value;
			}
		}

		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Camera targetCamera
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public CullingGroup()
		{
			this.m_Ptr = CullingGroup.Init(this);
		}

		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					this.FinalizerFailure();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		[FreeFunction("CullingGroup_Bindings::Dispose", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DisposeInternal();

		public void Dispose()
		{
			this.DisposeInternal();
			this.m_Ptr = IntPtr.Zero;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBoundingSpheres(BoundingSphere[] array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBoundingSphereCount(int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EraseSwapBack(int index);

		public static void EraseSwapBack<T>(int index, T[] myArray, ref int size)
		{
			size--;
			myArray[index] = myArray[size];
		}

		public int QueryIndices(bool visible, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, -1, CullingQueryOptions.IgnoreDistance, result, firstIndex);
		}

		public int QueryIndices(int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(false, distanceIndex, CullingQueryOptions.IgnoreVisibility, result, firstIndex);
		}

		public int QueryIndices(bool visible, int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, distanceIndex, CullingQueryOptions.Normal, result, firstIndex);
		}

		[FreeFunction("CullingGroup_Bindings::QueryIndices", HasExplicitThis = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int QueryIndices(bool visible, int distanceIndex, CullingQueryOptions options, int[] result, int firstIndex);

		[FreeFunction("CullingGroup_Bindings::IsVisible", HasExplicitThis = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsVisible(int index);

		[FreeFunction("CullingGroup_Bindings::GetDistance", HasExplicitThis = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetDistance(int index);

		[FreeFunction("CullingGroup_Bindings::SetBoundingDistances", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBoundingDistances(float[] distances);

		[FreeFunction("CullingGroup_Bindings::SetDistanceReferencePoint", HasExplicitThis = true)]
		private void SetDistanceReferencePoint_InternalVector3(Vector3 point)
		{
			this.SetDistanceReferencePoint_InternalVector3_Injected(ref point);
		}

		[NativeMethod("SetDistanceReferenceTransform")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetDistanceReferencePoint_InternalTransform(Transform transform);

		public void SetDistanceReferencePoint(Vector3 point)
		{
			this.SetDistanceReferencePoint_InternalVector3(point);
		}

		public void SetDistanceReferencePoint(Transform transform)
		{
			this.SetDistanceReferencePoint_InternalTransform(transform);
		}

		[SecuritySafeCritical, RequiredByNativeCode]
		private unsafe static void SendEvents(CullingGroup cullingGroup, IntPtr eventsPtr, int count)
		{
			CullingGroupEvent* ptr = (CullingGroupEvent*)eventsPtr.ToPointer();
			bool flag = cullingGroup.m_OnStateChanged == null;
			if (!flag)
			{
				for (int i = 0; i < count; i++)
				{
					cullingGroup.m_OnStateChanged(ptr[i]);
				}
			}
		}

		[FreeFunction("CullingGroup_Bindings::Init")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init(object scripting);

		[FreeFunction("CullingGroup_Bindings::FinalizerFailure", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FinalizerFailure();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetDistanceReferencePoint_InternalVector3_Injected(ref Vector3 point);
	}
}
