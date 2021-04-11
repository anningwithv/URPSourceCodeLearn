using System;
using System.Collections.Generic;

namespace UnityEngine
{
	public static class ParticlePhysicsExtensions
	{
		[Obsolete("GetCollisionEvents function using ParticleCollisionEvent[] is deprecated. Use List<ParticleCollisionEvent> instead.", false)]
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, ParticleCollisionEvent[] collisionEvents)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new ArgumentNullException("go");
			}
			bool flag2 = collisionEvents == null;
			if (flag2)
			{
				throw new ArgumentNullException("collisionEvents");
			}
			return ParticleSystemExtensionsImpl.GetCollisionEventsDeprecated(ps, go, collisionEvents);
		}

		public static int GetSafeCollisionEventSize(this ParticleSystem ps)
		{
			return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize(ps);
		}

		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, List<ParticleCollisionEvent> collisionEvents)
		{
			return ParticleSystemExtensionsImpl.GetCollisionEvents(ps, go, collisionEvents);
		}

		public static int GetSafeTriggerParticlesSize(this ParticleSystem ps, ParticleSystemTriggerEventType type)
		{
			return ParticleSystemExtensionsImpl.GetSafeTriggerParticlesSize(ps, (int)type);
		}

		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			return ParticleSystemExtensionsImpl.GetTriggerParticles(ps, (int)type, particles);
		}

		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, out ParticleSystem.ColliderData colliderData)
		{
			bool flag = type == ParticleSystemTriggerEventType.Exit;
			if (flag)
			{
				throw new InvalidOperationException("Querying the collider data for the Exit event is not currently supported.");
			}
			bool flag2 = type == ParticleSystemTriggerEventType.Outside;
			if (flag2)
			{
				throw new InvalidOperationException("Querying the collider data for the Outside event is not supported, because when a particle is outside the collision volume, it is always outside every collider.");
			}
			colliderData = default(ParticleSystem.ColliderData);
			return ParticleSystemExtensionsImpl.GetTriggerParticlesWithData(ps, (int)type, particles, ref colliderData);
		}

		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, int offset, int count)
		{
			bool flag = particles == null;
			if (flag)
			{
				throw new ArgumentNullException("particles");
			}
			bool flag2 = offset >= particles.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("offset", "offset should be smaller than the size of the particles list.");
			}
			bool flag3 = offset + count >= particles.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count", "offset+count should be smaller than the size of the particles list.");
			}
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, offset, count);
		}

		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, 0, particles.Count);
		}
	}
}
