using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/PhysicMaterial.h")]
	public class PhysicMaterial : Object
	{
		public extern float bounciness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float dynamicFriction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float staticFriction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern PhysicMaterialCombine frictionCombine
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern PhysicMaterialCombine bounceCombine
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use PhysicMaterial.bounciness instead (UnityUpgradable) -> bounciness")]
		public float bouncyness
		{
			get
			{
				return this.bounciness;
			}
			set
			{
				this.bounciness = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public Vector3 frictionDirection2
		{
			get
			{
				return Vector3.zero;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public float dynamicFriction2
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public float staticFriction2
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public Vector3 frictionDirection
		{
			get
			{
				return Vector3.zero;
			}
			set
			{
			}
		}

		public PhysicMaterial()
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, "DynamicMaterial");
		}

		public PhysicMaterial(string name)
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, name);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicsMaterial([Writable] PhysicMaterial mat, string name);
	}
}
