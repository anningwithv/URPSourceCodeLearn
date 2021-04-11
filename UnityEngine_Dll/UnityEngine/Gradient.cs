using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Math/Gradient.bindings.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Gradient : IEquatable<Gradient>
	{
		internal IntPtr m_Ptr;

		public extern GradientColorKey[] colorKeys
		{
			[FreeFunction("Gradient_Bindings::GetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Gradient_Bindings::SetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern GradientAlphaKey[] alphaKeys
		{
			[FreeFunction("Gradient_Bindings::GetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Gradient_Bindings::SetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern GradientMode mode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal Color constantColor
		{
			get
			{
				Color result;
				this.get_constantColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_constantColor_Injected(ref value);
			}
		}

		[FreeFunction(Name = "Gradient_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init();

		[FreeFunction(Name = "Gradient_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		[FreeFunction("Gradient_Bindings::Internal_Equals", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_Equals(IntPtr other);

		[RequiredByNativeCode]
		public Gradient()
		{
			this.m_Ptr = Gradient.Init();
		}

		~Gradient()
		{
			this.Cleanup();
		}

		[FreeFunction(Name = "Gradient_Bindings::Evaluate", IsThreadSafe = true, HasExplicitThis = true)]
		public Color Evaluate(float time)
		{
			Color result;
			this.Evaluate_Injected(time, out result);
			return result;
		}

		[FreeFunction(Name = "Gradient_Bindings::SetKeys", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

		public override bool Equals(object o)
		{
			bool flag = o == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == o;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = o.GetType() != base.GetType();
					result = (!flag3 && this.Equals((Gradient)o));
				}
			}
			return result;
		}

		public bool Equals(Gradient other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == other;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = this.m_Ptr.Equals(other.m_Ptr);
					result = (flag3 || this.Internal_Equals(other.m_Ptr));
				}
			}
			return result;
		}

		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Evaluate_Injected(float time, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_constantColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_constantColor_Injected(ref Color value);
	}
}
