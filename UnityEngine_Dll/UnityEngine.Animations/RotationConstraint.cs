using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h"), NativeHeader("Modules/Animation/Constraints/RotationConstraint.h"), RequireComponent(typeof(Transform)), UsedByNativeCode]
	public sealed class RotationConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		public extern float weight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 rotationAtRest
		{
			get
			{
				Vector3 result;
				this.get_rotationAtRest_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationAtRest_Injected(ref value);
			}
		}

		public Vector3 rotationOffset
		{
			get
			{
				Vector3 result;
				this.get_rotationOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationOffset_Injected(ref value);
			}
		}

		public extern Axis rotationAxis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool constraintActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool locked
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int sourceCount
		{
			get
			{
				return RotationConstraint.GetSourceCountInternal(this);
			}
		}

		Transform IConstraintInternal.transform
		{
			get
			{
				return base.transform;
			}
		}

		private RotationConstraint()
		{
			RotationConstraint.Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] RotationConstraint self);

		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] RotationConstraint self);

		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			RotationConstraint.SetSourcesInternal(this, sources);
		}

		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] RotationConstraint self, List<ConstraintSource> sources);

		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		[NativeName("RemoveSource")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveSourceInternal(int index);

		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource result;
			this.GetSourceInternal_Injected(index, out result);
			return result;
		}

		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The RotationConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ActivateAndPreserveOffset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ActivateWithZeroOffset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void UserUpdateOffset();

		void IConstraintInternal.ActivateAndPreserveOffset()
		{
			this.ActivateAndPreserveOffset();
		}

		void IConstraintInternal.ActivateWithZeroOffset()
		{
			this.ActivateWithZeroOffset();
		}

		void IConstraintInternal.UserUpdateOffset()
		{
			this.UserUpdateOffset();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
