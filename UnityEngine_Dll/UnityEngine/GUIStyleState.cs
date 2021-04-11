using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyleState
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		private readonly GUIStyle m_SourceStyle;

		[NativeProperty("Background", false, TargetType.Function)]
		public extern Texture2D background
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("textColor", false, TargetType.Field)]
		public Color textColor
		{
			get
			{
				Color result;
				this.get_textColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_textColor_Injected(ref value);
			}
		}

		[NativeProperty("scaledBackgrounds", false, TargetType.Field)]
		public extern Texture2D[] scaledBackgrounds
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[FreeFunction(Name = "GUIStyleState_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init();

		[FreeFunction(Name = "GUIStyleState_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		public GUIStyleState()
		{
			this.m_Ptr = GUIStyleState.Init();
		}

		private GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		internal static GUIStyleState ProduceGUIStyleStateFromDeserialization(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		internal static GUIStyleState GetGUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Cleanup();
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_textColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_textColor_Injected(ref Color value);
	}
}
