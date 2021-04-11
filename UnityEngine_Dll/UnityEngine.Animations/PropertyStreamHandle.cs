using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h"), MovedFrom("UnityEngine.Experimental.Animations")]
	public struct PropertyStreamHandle
	{
		private uint m_AnimatorBindingsVersion;

		private int handleIndex;

		private int valueArrayIndex;

		private int bindType;

		private bool createdByNative
		{
			get
			{
				return this.animatorBindingsVersion > 0u;
			}
		}

		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		private bool hasValueArrayIndex
		{
			get
			{
				return this.valueArrayIndex != -1;
			}
		}

		private bool hasBindType
		{
			get
			{
				return this.bindType != 0;
			}
		}

		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
			private set
			{
				this.m_AnimatorBindingsVersion = value;
			}
		}

		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex && this.hasBindType;
		}

		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return this.animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
		}

		public bool IsResolved(AnimationStream stream)
		{
			return this.IsResolvedInternal(ref stream);
		}

		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsSameVersionAsStream(ref stream) && this.hasValueArrayIndex;
		}

		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = this.IsResolvedInternal(ref stream);
			if (!flag)
			{
				bool flag2 = !this.createdByNative || !this.hasHandleIndex || !this.hasBindType;
				if (flag2)
				{
					throw new InvalidOperationException("The PropertyStreamHandle is invalid. Please use proper function to create the handle.");
				}
				bool flag3 = !this.IsSameVersionAsStream(ref stream) || (this.hasHandleIndex && !this.hasValueArrayIndex);
				if (flag3)
				{
					this.ResolveInternal(ref stream);
				}
				bool flag4 = this.hasHandleIndex && !this.hasValueArrayIndex;
				if (flag4)
				{
					throw new InvalidOperationException("The PropertyStreamHandle cannot be resolved.");
				}
			}
		}

		public float GetFloat(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 5;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetFloatInternal(ref stream);
		}

		public void SetFloat(AnimationStream stream, float value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 5;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetFloatInternal(ref stream, value);
		}

		public int GetInt(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 10 && this.bindType != 11 && this.bindType != 9;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetIntInternal(ref stream);
		}

		public void SetInt(AnimationStream stream, int value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 10 && this.bindType != 11 && this.bindType != 9;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetIntInternal(ref stream, value);
		}

		public bool GetBool(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 6 && this.bindType != 7;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetBoolInternal(ref stream);
		}

		public void SetBool(AnimationStream stream, bool value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 6 && this.bindType != 7;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetBoolInternal(ref stream, value);
		}

		public bool GetReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetReadMaskInternal(ref stream);
		}

		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			PropertyStreamHandle.ResolveInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		private float GetFloatInternal(ref AnimationStream stream)
		{
			return PropertyStreamHandle.GetFloatInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "SetFloat", IsThreadSafe = true)]
		private void SetFloatInternal(ref AnimationStream stream, float value)
		{
			PropertyStreamHandle.SetFloatInternal_Injected(ref this, ref stream, value);
		}

		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		private int GetIntInternal(ref AnimationStream stream)
		{
			return PropertyStreamHandle.GetIntInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "SetInt", IsThreadSafe = true)]
		private void SetIntInternal(ref AnimationStream stream, int value)
		{
			PropertyStreamHandle.SetIntInternal_Injected(ref this, ref stream, value);
		}

		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		private bool GetBoolInternal(ref AnimationStream stream)
		{
			return PropertyStreamHandle.GetBoolInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "SetBool", IsThreadSafe = true)]
		private void SetBoolInternal(ref AnimationStream stream, bool value)
		{
			PropertyStreamHandle.SetBoolInternal_Injected(ref this, ref stream, value);
		}

		[NativeMethod(Name = "GetReadMask", IsThreadSafe = true)]
		private bool GetReadMaskInternal(ref AnimationStream stream)
		{
			return PropertyStreamHandle.GetReadMaskInternal_Injected(ref this, ref stream);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetReadMaskInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);
	}
}
