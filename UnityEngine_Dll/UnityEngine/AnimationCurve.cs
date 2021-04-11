using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Math/AnimationCurve.bindings.h"), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimationCurve : IEquatable<AnimationCurve>
	{
		internal IntPtr m_Ptr;

		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey(index);
			}
		}

		public extern int length
		{
			[NativeMethod("GetKeyCount", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern WrapMode preWrapMode
		{
			[NativeMethod("GetPreInfinity", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetPreInfinity", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern WrapMode postWrapMode
		{
			[NativeMethod("GetPostInfinity", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetPostInfinity", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[FreeFunction("AnimationCurveBindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		[FreeFunction("AnimationCurveBindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(Keyframe[] keys);

		[FreeFunction("AnimationCurveBindings::Internal_Equals", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_Equals(IntPtr other);

		~AnimationCurve()
		{
			AnimationCurve.Internal_Destroy(this.m_Ptr);
		}

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float Evaluate(float time);

		[FreeFunction("AnimationCurveBindings::AddKeySmoothTangents", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int AddKey(float time, float value);

		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		[NativeMethod("AddKey", IsThreadSafe = true)]
		private int AddKey_Internal(Keyframe key)
		{
			return this.AddKey_Internal_Injected(ref key);
		}

		[FreeFunction("AnimationCurveBindings::MoveKey", HasExplicitThis = true, IsThreadSafe = true), NativeThrows]
		public int MoveKey(int index, Keyframe key)
		{
			return this.MoveKey_Injected(index, ref key);
		}

		[FreeFunction("AnimationCurveBindings::RemoveKey", HasExplicitThis = true, IsThreadSafe = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveKey(int index);

		[FreeFunction("AnimationCurveBindings::SetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetKeys(Keyframe[] keys);

		[FreeFunction("AnimationCurveBindings::GetKey", HasExplicitThis = true, IsThreadSafe = true), NativeThrows]
		private Keyframe GetKey(int index)
		{
			Keyframe result;
			this.GetKey_Injected(index, out result);
			return result;
		}

		[FreeFunction("AnimationCurveBindings::GetKeys", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Keyframe[] GetKeys();

		[FreeFunction("AnimationCurveBindings::SmoothTangents", HasExplicitThis = true, IsThreadSafe = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothTangents(int index, float weight);

		public static AnimationCurve Constant(float timeStart, float timeEnd, float value)
		{
			return AnimationCurve.Linear(timeStart, value, timeEnd, value);
		}

		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve result;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				result = new AnimationCurve(new Keyframe[]
				{
					keyframe
				});
			}
			else
			{
				float num = (valueEnd - valueStart) / (timeEnd - timeStart);
				Keyframe[] keys = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, num),
					new Keyframe(timeEnd, valueEnd, num, 0f)
				};
				result = new AnimationCurve(keys);
			}
			return result;
		}

		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			bool flag = timeStart == timeEnd;
			AnimationCurve result;
			if (flag)
			{
				Keyframe keyframe = new Keyframe(timeStart, valueStart);
				result = new AnimationCurve(new Keyframe[]
				{
					keyframe
				});
			}
			else
			{
				Keyframe[] keys = new Keyframe[]
				{
					new Keyframe(timeStart, valueStart, 0f, 0f),
					new Keyframe(timeEnd, valueEnd, 0f, 0f)
				};
				result = new AnimationCurve(keys);
			}
			return result;
		}

		public AnimationCurve(params Keyframe[] keys)
		{
			this.m_Ptr = AnimationCurve.Internal_Create(keys);
		}

		[RequiredByNativeCode]
		public AnimationCurve()
		{
			this.m_Ptr = AnimationCurve.Internal_Create(null);
		}

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
					result = (!flag3 && this.Equals((AnimationCurve)o));
				}
			}
			return result;
		}

		public bool Equals(AnimationCurve other)
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
		private extern int AddKey_Internal_Injected(ref Keyframe key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int MoveKey_Injected(int index, ref Keyframe key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetKey_Injected(int index, out Keyframe ret);
	}
}
