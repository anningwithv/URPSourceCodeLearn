using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	public struct AttachmentDescriptor : IEquatable<AttachmentDescriptor>
	{
		private RenderBufferLoadAction m_LoadAction;

		private RenderBufferStoreAction m_StoreAction;

		private GraphicsFormat m_Format;

		private RenderTargetIdentifier m_LoadStoreTarget;

		private RenderTargetIdentifier m_ResolveTarget;

		private Color m_ClearColor;

		private float m_ClearDepth;

		private uint m_ClearStencil;

		public RenderBufferLoadAction loadAction
		{
			get
			{
				return this.m_LoadAction;
			}
			set
			{
				this.m_LoadAction = value;
			}
		}

		public RenderBufferStoreAction storeAction
		{
			get
			{
				return this.m_StoreAction;
			}
			set
			{
				this.m_StoreAction = value;
			}
		}

		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this.m_Format;
			}
			set
			{
				this.m_Format = value;
			}
		}

		public RenderTextureFormat format
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(this.m_Format);
			}
			set
			{
				this.m_Format = GraphicsFormatUtility.GetGraphicsFormat(value, RenderTextureReadWrite.Default);
			}
		}

		public RenderTargetIdentifier loadStoreTarget
		{
			get
			{
				return this.m_LoadStoreTarget;
			}
			set
			{
				this.m_LoadStoreTarget = value;
			}
		}

		public RenderTargetIdentifier resolveTarget
		{
			get
			{
				return this.m_ResolveTarget;
			}
			set
			{
				this.m_ResolveTarget = value;
			}
		}

		public Color clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
			set
			{
				this.m_ClearColor = value;
			}
		}

		public float clearDepth
		{
			get
			{
				return this.m_ClearDepth;
			}
			set
			{
				this.m_ClearDepth = value;
			}
		}

		public uint clearStencil
		{
			get
			{
				return this.m_ClearStencil;
			}
			set
			{
				this.m_ClearStencil = value;
			}
		}

		public void ConfigureTarget(RenderTargetIdentifier target, bool loadExistingContents, bool storeResults)
		{
			this.m_LoadStoreTarget = target;
			bool flag = loadExistingContents && this.m_LoadAction != RenderBufferLoadAction.Clear;
			if (flag)
			{
				this.m_LoadAction = RenderBufferLoadAction.Load;
			}
			if (storeResults)
			{
				bool flag2 = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Resolve;
				if (flag2)
				{
					this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
				}
				else
				{
					this.m_StoreAction = RenderBufferStoreAction.Store;
				}
			}
		}

		public void ConfigureResolveTarget(RenderTargetIdentifier target)
		{
			this.m_ResolveTarget = target;
			bool flag = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Store;
			if (flag)
			{
				this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
			}
			else
			{
				this.m_StoreAction = RenderBufferStoreAction.Resolve;
			}
		}

		public void ConfigureClear(Color clearColor, float clearDepth = 1f, uint clearStencil = 0u)
		{
			this.m_ClearColor = clearColor;
			this.m_ClearDepth = clearDepth;
			this.m_ClearStencil = clearStencil;
			this.m_LoadAction = RenderBufferLoadAction.Clear;
		}

		public AttachmentDescriptor(GraphicsFormat format)
		{
			this = default(AttachmentDescriptor);
			this.m_LoadAction = RenderBufferLoadAction.DontCare;
			this.m_StoreAction = RenderBufferStoreAction.DontCare;
			this.m_Format = format;
			this.m_LoadStoreTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ResolveTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ClearColor = new Color(0f, 0f, 0f, 0f);
			this.m_ClearDepth = 1f;
		}

		public AttachmentDescriptor(RenderTextureFormat format)
		{
			this = new AttachmentDescriptor(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		public AttachmentDescriptor(RenderTextureFormat format, RenderTargetIdentifier target, bool loadExistingContents = false, bool storeResults = false, bool resolve = false)
		{
			this = new AttachmentDescriptor(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		public bool Equals(AttachmentDescriptor other)
		{
			return this.m_LoadAction == other.m_LoadAction && this.m_StoreAction == other.m_StoreAction && this.m_Format == other.m_Format && this.m_LoadStoreTarget.Equals(other.m_LoadStoreTarget) && this.m_ResolveTarget.Equals(other.m_ResolveTarget) && this.m_ClearColor.Equals(other.m_ClearColor) && this.m_ClearDepth.Equals(other.m_ClearDepth) && this.m_ClearStencil == other.m_ClearStencil;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is AttachmentDescriptor && this.Equals((AttachmentDescriptor)obj);
		}

		public override int GetHashCode()
		{
			int num = (int)this.m_LoadAction;
			num = (num * 397 ^ (int)this.m_StoreAction);
			num = (num * 397 ^ (int)this.m_Format);
			num = (num * 397 ^ this.m_LoadStoreTarget.GetHashCode());
			num = (num * 397 ^ this.m_ResolveTarget.GetHashCode());
			num = (num * 397 ^ this.m_ClearColor.GetHashCode());
			num = (num * 397 ^ this.m_ClearDepth.GetHashCode());
			return num * 397 ^ (int)this.m_ClearStencil;
		}

		public static bool operator ==(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return !left.Equals(right);
		}
	}
}
