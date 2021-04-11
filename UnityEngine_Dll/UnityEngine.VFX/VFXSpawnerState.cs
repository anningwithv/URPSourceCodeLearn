using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	[NativeType(Header = "Modules/VFX/Public/VFXSpawnerState.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class VFXSpawnerState : IDisposable
	{
		private IntPtr m_Ptr;

		private bool m_Owner;

		public bool playing
		{
			get
			{
				return this.loopState == VFXSpawnerLoopState.Looping;
			}
			set
			{
				this.loopState = (value ? VFXSpawnerLoopState.Looping : VFXSpawnerLoopState.Finished);
			}
		}

		public extern bool newLoop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern VFXSpawnerLoopState loopState
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float spawnCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float deltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float totalTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float delayBeforeLoop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float loopDuration
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float delayAfterLoop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int loopIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int loopCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern VFXEventAttribute vfxEventAttribute
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public VFXSpawnerState() : this(VFXSpawnerState.Internal_Create(), true)
		{
		}

		internal VFXSpawnerState(IntPtr ptr, bool owner)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		[RequiredByNativeCode]
		internal static VFXSpawnerState CreateSpawnerStateWrapper()
		{
			return new VFXSpawnerState(IntPtr.Zero, false);
		}

		[RequiredByNativeCode]
		internal void SetWrapValue(IntPtr ptr)
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			this.m_Ptr = ptr;
		}

		internal IntPtr GetPtr()
		{
			return this.m_Ptr;
		}

		private void Release()
		{
			bool flag = this.m_Ptr != IntPtr.Zero && this.m_Owner;
			if (flag)
			{
				VFXSpawnerState.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
		}

		~VFXSpawnerState()
		{
			this.Release();
		}

		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);
	}
}
