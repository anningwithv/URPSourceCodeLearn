using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.iOS
{
	[NativeHeader("Runtime/Export/iOS/OnDemandResources.h"), UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class OnDemandResourcesRequest : AsyncOperation, IDisposable
	{
		public extern string error
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float loadingPriority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetResourcePath(string resourceName);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyFromScript(IntPtr ptr);

		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				OnDemandResourcesRequest.DestroyFromScript(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		internal OnDemandResourcesRequest()
		{
		}

		~OnDemandResourcesRequest()
		{
			this.Dispose();
		}
	}
}
