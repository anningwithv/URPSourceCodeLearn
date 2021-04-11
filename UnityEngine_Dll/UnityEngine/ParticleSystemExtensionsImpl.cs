using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	internal class ParticleSystemExtensionsImpl
	{
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeCollisionEventSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetSafeCollisionEventSize([NotNull("ArgumentNullException")] ParticleSystem ps);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEventsDeprecated")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCollisionEventsDeprecated([NotNull("ArgumentNullException")] ParticleSystem ps, GameObject go, [Out] ParticleCollisionEvent[] collisionEvents);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeTriggerParticlesSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetSafeTriggerParticlesSize([NotNull("ArgumentNullException")] ParticleSystem ps, int type);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEvents")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCollisionEvents([NotNull("ArgumentNullException")] ParticleSystem ps, [NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] List<ParticleCollisionEvent> collisionEvents);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetTriggerParticles([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticlesWithData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetTriggerParticlesWithData([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles, ref ParticleSystem.ColliderData colliderData);

		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTriggerParticles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTriggerParticles([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles, int offset, int count);
	}
}
