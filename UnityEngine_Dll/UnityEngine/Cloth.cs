using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[NativeHeader("Modules/Cloth/Cloth.h"), NativeClass("Unity::Cloth"), RequireComponent(typeof(Transform), typeof(SkinnedMeshRenderer))]
	public sealed class Cloth : Component
	{
		public extern Vector3[] vertices
		{
			[NativeName("GetPositions")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Vector3[] normals
		{
			[NativeName("GetNormals")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ClothSkinningCoefficient[] coefficients
		{
			[NativeName("GetCoefficients")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetCoefficients")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CapsuleCollider[] capsuleColliders
		{
			[NativeName("GetCapsuleColliders")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetCapsuleColliders")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ClothSphereColliderPair[] sphereColliders
		{
			[NativeName("GetSphereColliders")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetSphereColliders")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float sleepThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float bendingStiffness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float stretchingStiffness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float damping
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 externalAcceleration
		{
			get
			{
				Vector3 result;
				this.get_externalAcceleration_Injected(out result);
				return result;
			}
			set
			{
				this.set_externalAcceleration_Injected(ref value);
			}
		}

		public Vector3 randomAcceleration
		{
			get
			{
				Vector3 result;
				this.get_randomAcceleration_Injected(out result);
				return result;
			}
			set
			{
				this.set_randomAcceleration_Injected(ref value);
			}
		}

		public extern bool useGravity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float friction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float collisionMassScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enableContinuousCollision
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float useVirtualParticles
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float worldVelocityScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float worldAccelerationScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float clothSolverFrequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Parameter solverFrequency is obsolete and no longer supported. Please use clothSolverFrequency instead.")]
		public bool solverFrequency
		{
			get
			{
				return this.clothSolverFrequency > 0f;
			}
			set
			{
				this.clothSolverFrequency = (value ? 120f : 0f);
			}
		}

		public extern bool useTethers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float stiffnessFrequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float selfCollisionDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float selfCollisionStiffness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("useContinuousCollision is no longer supported, use enableContinuousCollision instead")]
		public float useContinuousCollision
		{
			get;
			set;
		}

		[Obsolete("Deprecated.Cloth.selfCollisions is no longer supported since Unity 5.0.", true)]
		public bool selfCollision
		{
			[CompilerGenerated]
			get
			{
				return this.<selfCollision>k__BackingField;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearTransformMotion();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSelfAndInterCollisionIndices([NotNull("ArgumentNullException")] List<uint> indices);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSelfAndInterCollisionIndices([NotNull("ArgumentNullException")] List<uint> indices);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetVirtualParticleIndices([NotNull("ArgumentNullException")] List<uint> indicesOutList);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVirtualParticleIndices([NotNull("ArgumentNullException")] List<uint> indicesIn);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetVirtualParticleWeights([NotNull("ArgumentNullException")] List<Vector3> weightsOutList);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVirtualParticleWeights([NotNull("ArgumentNullException")] List<Vector3> weights);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetEnabledFading(bool enabled, float interpolationTime);

		[ExcludeFromDocs]
		public void SetEnabledFading(bool enabled)
		{
			this.SetEnabledFading(enabled, 0.5f);
		}

		private RaycastHit Raycast(Ray ray, float maxDistance, ref bool hasHit)
		{
			RaycastHit result;
			this.Raycast_Injected(ref ray, maxDistance, ref hasHit, out result);
			return result;
		}

		internal bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			bool result = false;
			hitInfo = this.Raycast(ray, maxDistance, ref result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_externalAcceleration_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_externalAcceleration_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_randomAcceleration_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_randomAcceleration_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Raycast_Injected(ref Ray ray, float maxDistance, ref bool hasHit, out RaycastHit ret);
	}
}
