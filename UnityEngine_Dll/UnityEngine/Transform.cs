using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformScriptBindings.h"), NativeHeader("Configuration/UnityConfigure.h"), NativeHeader("Runtime/Transform/Transform.h"), RequiredByNativeCode]
	public class Transform : Component, IEnumerable
	{
		private class Enumerator : IEnumerator
		{
			private Transform outer;

			private int currentIndex = -1;

			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < childCount;
			}

			public void Reset()
			{
				this.currentIndex = -1;
			}
		}

		public Vector3 position
		{
			get
			{
				Vector3 result;
				this.get_position_Injected(out result);
				return result;
			}
			set
			{
				this.set_position_Injected(ref value);
			}
		}

		public Vector3 localPosition
		{
			get
			{
				Vector3 result;
				this.get_localPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_localPosition_Injected(ref value);
			}
		}

		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		public Vector3 localEulerAngles
		{
			get
			{
				return this.localRotation.eulerAngles;
			}
			set
			{
				this.localRotation = Quaternion.Euler(value);
			}
		}

		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		public Quaternion rotation
		{
			get
			{
				Quaternion result;
				this.get_rotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotation_Injected(ref value);
			}
		}

		public Quaternion localRotation
		{
			get
			{
				Quaternion result;
				this.get_localRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_localRotation_Injected(ref value);
			}
		}

		[NativeConditional("UNITY_EDITOR")]
		internal RotationOrder rotationOrder
		{
			get
			{
				return (RotationOrder)this.GetRotationOrderInternal();
			}
			set
			{
				this.SetRotationOrderInternal(value);
			}
		}

		public Vector3 localScale
		{
			get
			{
				Vector3 result;
				this.get_localScale_Injected(out result);
				return result;
			}
			set
			{
				this.set_localScale_Injected(ref value);
			}
		}

		public Transform parent
		{
			get
			{
				return this.parentInternal;
			}
			set
			{
				bool flag = this is RectTransform;
				if (flag)
				{
					Debug.LogWarning("Parent of RectTransform is being set with parent property. Consider using the SetParent method instead, with the worldPositionStays argument set to false. This will retain local orientation and scale rather than world orientation and scale, which can prevent common UI scaling issues.", this);
				}
				this.parentInternal = value;
			}
		}

		internal Transform parentInternal
		{
			get
			{
				return this.GetParent();
			}
			set
			{
				this.SetParent(value);
			}
		}

		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_worldToLocalMatrix_Injected(out result);
				return result;
			}
		}

		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.get_localToWorldMatrix_Injected(out result);
				return result;
			}
		}

		public Transform root
		{
			get
			{
				return this.GetRoot();
			}
		}

		public extern int childCount
		{
			[NativeMethod("GetChildrenCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector3 lossyScale
		{
			[NativeMethod("GetWorldScaleLossy")]
			get
			{
				Vector3 result;
				this.get_lossyScale_Injected(out result);
				return result;
			}
		}

		[NativeProperty("HasChangedDeprecated")]
		public extern bool hasChanged
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int hierarchyCapacity
		{
			get
			{
				return this.internal_getHierarchyCapacity();
			}
			set
			{
				this.internal_setHierarchyCapacity(value);
			}
		}

		public int hierarchyCount
		{
			get
			{
				return this.internal_getHierarchyCount();
			}
		}

		protected Transform()
		{
		}

		internal Vector3 GetLocalEulerAngles(RotationOrder order)
		{
			Vector3 result;
			this.GetLocalEulerAngles_Injected(order, out result);
			return result;
		}

		internal void SetLocalEulerAngles(Vector3 euler, RotationOrder order)
		{
			this.SetLocalEulerAngles_Injected(ref euler, order);
		}

		[NativeConditional("UNITY_EDITOR")]
		internal void SetLocalEulerHint(Vector3 euler)
		{
			this.SetLocalEulerHint_Injected(ref euler);
		}

		[NativeConditional("UNITY_EDITOR"), NativeMethod("GetRotationOrder")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetRotationOrderInternal();

		[NativeConditional("UNITY_EDITOR"), NativeMethod("SetRotationOrder")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetRotationOrderInternal(RotationOrder rotationOrder);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Transform GetParent();

		public void SetParent(Transform p)
		{
			this.SetParent(p, true);
		}

		[FreeFunction("SetParent", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetParent(Transform parent, bool worldPositionStays);

		public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			this.SetPositionAndRotation_Injected(ref position, ref rotation);
		}

		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.World;
			if (flag)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		public void Translate(Vector3 translation)
		{
			this.Translate(translation, Space.Self);
		}

		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		public void Translate(float x, float y, float z)
		{
			this.Translate(new Vector3(x, y, z), Space.Self);
		}

		public void Translate(Vector3 translation, Transform relativeTo)
		{
			bool flag = relativeTo;
			if (flag)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		public void Rotate(Vector3 eulers, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion rhs = Quaternion.Euler(eulers.x, eulers.y, eulers.z);
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.localRotation *= rhs;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * rhs * this.rotation;
			}
		}

		public void Rotate(Vector3 eulers)
		{
			this.Rotate(eulers, Space.Self);
		}

		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), Space.Self);
		}

		[NativeMethod("RotateAround")]
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			this.RotateAroundInternal_Injected(ref axis, angle);
		}

		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.0174532924f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.0174532924f);
			}
		}

		public void Rotate(Vector3 axis, float angle)
		{
			this.Rotate(axis, angle, Space.Self);
		}

		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = this.position;
			Quaternion rotation = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = rotation * vector2;
			vector = point + vector2;
			this.position = vector;
			this.RotateAroundInternal(axis, angle * 0.0174532924f);
		}

		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		public void LookAt(Transform target)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, Vector3.up);
			}
		}

		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			this.Internal_LookAt(worldPosition, worldUp);
		}

		public void LookAt(Vector3 worldPosition)
		{
			this.Internal_LookAt(worldPosition, Vector3.up);
		}

		[FreeFunction("Internal_LookAt", HasExplicitThis = true)]
		private void Internal_LookAt(Vector3 worldPosition, Vector3 worldUp)
		{
			this.Internal_LookAt_Injected(ref worldPosition, ref worldUp);
		}

		public Vector3 TransformDirection(Vector3 direction)
		{
			Vector3 result;
			this.TransformDirection_Injected(ref direction, out result);
			return result;
		}

		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			Vector3 result;
			this.InverseTransformDirection_Injected(ref direction, out result);
			return result;
		}

		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		public Vector3 TransformVector(Vector3 vector)
		{
			Vector3 result;
			this.TransformVector_Injected(ref vector, out result);
			return result;
		}

		public Vector3 TransformVector(float x, float y, float z)
		{
			return this.TransformVector(new Vector3(x, y, z));
		}

		public Vector3 InverseTransformVector(Vector3 vector)
		{
			Vector3 result;
			this.InverseTransformVector_Injected(ref vector, out result);
			return result;
		}

		public Vector3 InverseTransformVector(float x, float y, float z)
		{
			return this.InverseTransformVector(new Vector3(x, y, z));
		}

		public Vector3 TransformPoint(Vector3 position)
		{
			Vector3 result;
			this.TransformPoint_Injected(ref position, out result);
			return result;
		}

		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		public Vector3 InverseTransformPoint(Vector3 position)
		{
			Vector3 result;
			this.InverseTransformPoint_Injected(ref position, out result);
			return result;
		}

		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Transform GetRoot();

		[FreeFunction("DetachChildren", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DetachChildren();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAsFirstSibling();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAsLastSibling();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSiblingIndex(int index);

		[NativeMethod("MoveAfterSiblingInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void MoveAfterSibling(Transform transform, bool notifyEditorAndMarkDirty);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetSiblingIndex();

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Transform FindRelativeTransformWithPath([NotNull("NullExceptionObject")] Transform transform, string path, [DefaultValue("false")] bool isActiveOnly);

		public Transform Find(string n)
		{
			bool flag = n == null;
			if (flag)
			{
				throw new ArgumentNullException("Name cannot be null");
			}
			return Transform.FindRelativeTransformWithPath(this, n, false);
		}

		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SendTransformChangedScale();

		[FreeFunction("Internal_IsChildOrSameTransform", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsChildOf([NotNull("ArgumentNullException")] Transform parent);

		[Obsolete("FindChild has been deprecated. Use Find instead (UnityUpgradable) -> Find([mscorlib] System.String)", false)]
		public Transform FindChild(string n)
		{
			return this.Find(n);
		}

		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			this.RotateAround_Injected(ref axis, angle);
		}

		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			this.RotateAroundLocal_Injected(ref axis, angle);
		}

		[FreeFunction("GetChild", HasExplicitThis = true), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform GetChild(int index);

		[Obsolete("warning use Transform.childCount instead (UnityUpgradable) -> Transform.childCount", false), NativeMethod("GetChildrenCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetChildCount();

		[FreeFunction("GetHierarchyCapacity", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_getHierarchyCapacity();

		[FreeFunction("SetHierarchyCapacity", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void internal_setHierarchyCapacity(int value);

		[FreeFunction("GetHierarchyCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_getHierarchyCount();

		[FreeFunction("IsNonUniformScaleTransform", HasExplicitThis = true), NativeConditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsNonUniformScaleTransform();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_position_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_position_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localPosition_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalEulerAngles_Injected(RotationOrder order, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLocalEulerAngles_Injected(ref Vector3 euler, RotationOrder order);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLocalEulerHint_Injected(ref Vector3 euler);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localRotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localScale_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localScale_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldToLocalMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localToWorldMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPositionAndRotation_Injected(ref Vector3 position, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RotateAroundInternal_Injected(ref Vector3 axis, float angle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_LookAt_Injected(ref Vector3 worldPosition, ref Vector3 worldUp);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TransformDirection_Injected(ref Vector3 direction, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InverseTransformDirection_Injected(ref Vector3 direction, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TransformVector_Injected(ref Vector3 vector, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InverseTransformVector_Injected(ref Vector3 vector, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TransformPoint_Injected(ref Vector3 position, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InverseTransformPoint_Injected(ref Vector3 position, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_lossyScale_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RotateAround_Injected(ref Vector3 axis, float angle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RotateAroundLocal_Injected(ref Vector3 axis, float angle);
	}
}
