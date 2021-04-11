using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTextureResolver.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class Resolver : IDisposable
	{
		internal IntPtr m_Ptr;

		public int CurrentWidth
		{
			get;
			private set;
		}

		public int CurrentHeight
		{
			get;
			private set;
		}

		public Resolver()
		{
			this.<CurrentWidth>k__BackingField = 0;
			this.<CurrentHeight>k__BackingField = 0;
			base..ctor();
			bool flag = !System.enabled;
			if (flag)
			{
				throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
			}
			this.m_Ptr = Resolver.InitNative();
		}

		~Resolver()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Flush_Internal();
				Resolver.ReleaseNative(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitNative();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseNative(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Flush_Internal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init_Internal(int width, int height);

		public void UpdateSize(int width, int height)
		{
			bool flag = this.CurrentWidth < width || this.CurrentHeight < height;
			if (flag)
			{
				this.CurrentWidth = ((this.CurrentWidth < width) ? width : this.CurrentWidth);
				this.CurrentHeight = ((this.CurrentHeight < height) ? height : this.CurrentHeight);
				this.Flush_Internal();
				this.Init_Internal(this.CurrentWidth, this.CurrentHeight);
			}
		}

		public void Process(CommandBuffer cmd, RenderTargetIdentifier rt)
		{
			this.Process(cmd, rt, 0, this.CurrentWidth, 0, this.CurrentHeight, 0, 0);
		}

		public void Process(CommandBuffer cmd, RenderTargetIdentifier rt, int x, int width, int y, int height, int mip, int slice)
		{
			bool flag = cmd == null;
			if (flag)
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.ProcessVTFeedback(rt, this.m_Ptr, slice, x, width, y, height, mip);
		}
	}
}
