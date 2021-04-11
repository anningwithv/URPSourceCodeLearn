using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	[NativeType(Header = "Modules/DSPGraph/Public/DSPGraphHandles.h")]
	internal struct Handle : IHandle<Handle>, IValidatable, IEquatable<Handle>
	{
		internal struct Node
		{
			public long Next;

			public int Id;

			public int Version;

			public int DidAllocate;

			public const int InvalidId = -1;
		}

		[NativeDisableUnsafePtrRestriction]
		private IntPtr m_Node;

		public int Version;

		public unsafe Handle.Node* AtomicNode
		{
			get
			{
				return (Handle.Node*)((void*)this.m_Node);
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException();
				}
				this.m_Node = (IntPtr)((void*)value);
				this.Version = value->Version;
			}
		}

		public unsafe int Id
		{
			get
			{
				return this.Valid ? this.AtomicNode->Id : -1;
			}
			set
			{
				bool flag = value == -1;
				if (flag)
				{
					throw new ArgumentException("Invalid ID");
				}
				bool flag2 = !this.Valid;
				if (flag2)
				{
					throw new InvalidOperationException("Handle is invalid or has been destroyed");
				}
				bool flag3 = this.AtomicNode->Id != -1;
				if (flag3)
				{
					throw new InvalidOperationException(string.Format("Trying to overwrite id on live node {0}", this.AtomicNode->Id));
				}
				this.AtomicNode->Id = value;
			}
		}

		public unsafe bool Valid
		{
			get
			{
				return this.m_Node != IntPtr.Zero && this.AtomicNode->Version == this.Version;
			}
		}

		public unsafe bool Alive
		{
			get
			{
				return this.Valid && this.AtomicNode->Id != -1;
			}
		}

		public unsafe Handle(Handle.Node* node)
		{
			bool flag = node == null;
			if (flag)
			{
				throw new ArgumentNullException("node");
			}
			bool flag2 = node->Id != -1;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Reusing unflushed node {0}", node->Id));
			}
			this.Version = node->Version;
			this.m_Node = (IntPtr)((void*)node);
		}

		public unsafe void FlushNode()
		{
			bool flag = !this.Valid;
			if (flag)
			{
				throw new InvalidOperationException("Attempting to flush invalid audio handle");
			}
			this.AtomicNode->Id = -1;
			this.AtomicNode->Version++;
		}

		public bool Equals(Handle other)
		{
			return this.m_Node == other.m_Node && this.Version == other.Version;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is Handle && this.Equals((Handle)obj);
		}

		public override int GetHashCode()
		{
			return (int)this.m_Node * 397 ^ this.Version;
		}
	}
}
