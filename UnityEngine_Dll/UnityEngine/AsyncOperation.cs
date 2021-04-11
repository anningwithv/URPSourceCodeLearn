using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Misc/AsyncOperation.h"), NativeHeader("Runtime/Export/Scripting/AsyncOperation.bindings.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncOperation : YieldInstruction
	{
		internal IntPtr m_Ptr;

		private Action<AsyncOperation> m_completeCallback;

		public event Action<AsyncOperation> completed
		{
			add
			{
				bool isDone = this.isDone;
				if (isDone)
				{
					value(this);
				}
				else
				{
					this.m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(this.m_completeCallback, value);
				}
			}
			remove
			{
				this.m_completeCallback = (Action<AsyncOperation>)Delegate.Remove(this.m_completeCallback, value);
			}
		}

		public extern bool isDone
		{
			[NativeMethod("IsDone")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float progress
		{
			[NativeMethod("GetProgress")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int priority
		{
			[NativeMethod("GetPriority")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetPriority")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowSceneActivation
		{
			[NativeMethod("GetAllowSceneActivation")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetAllowSceneActivation")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeMethod(IsThreadSafe = true), StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		~AsyncOperation()
		{
			AsyncOperation.InternalDestroy(this.m_Ptr);
		}

		[RequiredByNativeCode]
		internal void InvokeCompletionEvent()
		{
			bool flag = this.m_completeCallback != null;
			if (flag)
			{
				this.m_completeCallback(this);
				this.m_completeCallback = null;
			}
		}
	}
}
