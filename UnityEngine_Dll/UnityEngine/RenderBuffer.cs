using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public struct RenderBuffer
	{
		internal int m_RenderTextureInstanceID;

		internal IntPtr m_BufferPtr;

		internal RenderBufferLoadAction loadAction
		{
			get
			{
				return this.GetLoadAction();
			}
			set
			{
				this.SetLoadAction(value);
			}
		}

		internal RenderBufferStoreAction storeAction
		{
			get
			{
				return this.GetStoreAction();
			}
			set
			{
				this.SetStoreAction(value);
			}
		}

		[FreeFunction(Name = "RenderBufferScripting::SetLoadAction", HasExplicitThis = true)]
		internal void SetLoadAction(RenderBufferLoadAction action)
		{
			RenderBuffer.SetLoadAction_Injected(ref this, action);
		}

		[FreeFunction(Name = "RenderBufferScripting::SetStoreAction", HasExplicitThis = true)]
		internal void SetStoreAction(RenderBufferStoreAction action)
		{
			RenderBuffer.SetStoreAction_Injected(ref this, action);
		}

		[FreeFunction(Name = "RenderBufferScripting::GetLoadAction", HasExplicitThis = true)]
		internal RenderBufferLoadAction GetLoadAction()
		{
			return RenderBuffer.GetLoadAction_Injected(ref this);
		}

		[FreeFunction(Name = "RenderBufferScripting::GetStoreAction", HasExplicitThis = true)]
		internal RenderBufferStoreAction GetStoreAction()
		{
			return RenderBuffer.GetStoreAction_Injected(ref this);
		}

		[FreeFunction(Name = "RenderBufferScripting::GetNativeRenderBufferPtr", HasExplicitThis = true)]
		public IntPtr GetNativeRenderBufferPtr()
		{
			return RenderBuffer.GetNativeRenderBufferPtr_Injected(ref this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoadAction_Injected(ref RenderBuffer _unity_self, RenderBufferLoadAction action);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStoreAction_Injected(ref RenderBuffer _unity_self, RenderBufferStoreAction action);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderBufferLoadAction GetLoadAction_Injected(ref RenderBuffer _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderBufferStoreAction GetStoreAction_Injected(ref RenderBuffer _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeRenderBufferPtr_Injected(ref RenderBuffer _unity_self);
	}
}
