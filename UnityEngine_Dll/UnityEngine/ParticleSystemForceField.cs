using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h"), NativeHeader("Modules/ParticleSystem/ParticleSystemForceFieldManager.h"), NativeHeader("Modules/ParticleSystem/ParticleSystemForceField.h"), NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h"), NativeHeader("ParticleSystemScriptingClasses.h"), RequireComponent(typeof(Transform))]
	public class ParticleSystemForceField : Component
	{
		[NativeName("ForceShape")]
		public extern ParticleSystemForceFieldShape shape
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float startRange
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float endRange
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float gravityFocus
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 rotationRandomness
		{
			get
			{
				Vector2 result;
				this.get_rotationRandomness_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationRandomness_Injected(ref value);
			}
		}

		public extern bool multiplyDragByParticleSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool multiplyDragByParticleVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture3D vectorField
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public ParticleSystem.MinMaxCurve directionX
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_directionX_Injected(out result);
				return result;
			}
			set
			{
				this.set_directionX_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve directionY
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_directionY_Injected(out result);
				return result;
			}
			set
			{
				this.set_directionY_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve directionZ
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_directionZ_Injected(out result);
				return result;
			}
			set
			{
				this.set_directionZ_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve gravity
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_gravity_Injected(out result);
				return result;
			}
			set
			{
				this.set_gravity_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve rotationSpeed
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_rotationSpeed_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationSpeed_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve rotationAttraction
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_rotationAttraction_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationAttraction_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve drag
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_drag_Injected(out result);
				return result;
			}
			set
			{
				this.set_drag_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve vectorFieldSpeed
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_vectorFieldSpeed_Injected(out result);
				return result;
			}
			set
			{
				this.set_vectorFieldSpeed_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve vectorFieldAttraction
		{
			get
			{
				ParticleSystem.MinMaxCurve result;
				this.get_vectorFieldAttraction_Injected(out result);
				return result;
			}
			set
			{
				this.set_vectorFieldAttraction_Injected(ref value);
			}
		}

		[NativeMethod("GetForceFields"), StaticAccessor("GetParticleSystemForceFieldManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ParticleSystemForceField[] FindAll();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationRandomness_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationRandomness_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_directionX_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_directionX_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_directionY_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_directionY_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_directionZ_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_directionZ_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_gravity_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_gravity_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationSpeed_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationSpeed_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationAttraction_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationAttraction_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_drag_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_drag_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_vectorFieldSpeed_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_vectorFieldSpeed_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_vectorFieldAttraction_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_vectorFieldAttraction_Injected(ref ParticleSystem.MinMaxCurve value);
	}
}
