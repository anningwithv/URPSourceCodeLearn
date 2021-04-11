using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Networking/Ping.bindings.h")]
	public sealed class Ping
	{
		internal IntPtr m_Ptr;

		public bool isDone
		{
			get
			{
				bool flag = this.m_Ptr == IntPtr.Zero;
				return !flag && this.Internal_IsDone();
			}
		}

		public extern int time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string ip
		{
			[NativeName("GetIP")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Ping(string address)
		{
			this.m_Ptr = Ping.Internal_Create(address);
		}

		~Ping()
		{
			this.DestroyPing();
		}

		[ThreadAndSerializationSafe]
		public void DestroyPing()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (!flag)
			{
				Ping.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		[FreeFunction("DestroyPing", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		[FreeFunction("CreatePing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(string address);

		[NativeName("GetIsDone")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_IsDone();
	}
}
