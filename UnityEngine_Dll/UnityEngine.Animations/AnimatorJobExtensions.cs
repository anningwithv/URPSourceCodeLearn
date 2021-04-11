using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorJobExtensions.bindings.h"), NativeHeader("Modules/Animation/Animator.h"), NativeHeader("Modules/Animation/Director/AnimationStream.h"), NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h"), NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h"), StaticAccessor("AnimatorJobExtensionsBindings", StaticAccessorType.DoubleColon), MovedFrom("UnityEngine.Experimental.Animations")]
	public static class AnimatorJobExtensions
	{
		public static void AddJobDependency(this Animator animator, JobHandle jobHandle)
		{
			AnimatorJobExtensions.InternalAddJobDependency(animator, jobHandle);
		}

		public static TransformStreamHandle BindStreamTransform(this Animator animator, Transform transform)
		{
			TransformStreamHandle result = default(TransformStreamHandle);
			AnimatorJobExtensions.InternalBindStreamTransform(animator, transform, out result);
			return result;
		}

		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindStreamProperty(transform, type, property, false);
		}

		public static PropertyStreamHandle BindCustomStreamProperty(this Animator animator, string property, CustomStreamPropertyType type)
		{
			PropertyStreamHandle result = default(PropertyStreamHandle);
			AnimatorJobExtensions.InternalBindCustomStreamProperty(animator, property, type, out result);
			return result;
		}

		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property, [DefaultValue("false")] bool isObjectReference)
		{
			PropertyStreamHandle result = default(PropertyStreamHandle);
			AnimatorJobExtensions.InternalBindStreamProperty(animator, transform, type, property, isObjectReference, out result);
			return result;
		}

		public static TransformSceneHandle BindSceneTransform(this Animator animator, Transform transform)
		{
			TransformSceneHandle result = default(TransformSceneHandle);
			AnimatorJobExtensions.InternalBindSceneTransform(animator, transform, out result);
			return result;
		}

		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindSceneProperty(transform, type, property, false);
		}

		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property, [DefaultValue("false")] bool isObjectReference)
		{
			PropertySceneHandle result = default(PropertySceneHandle);
			AnimatorJobExtensions.InternalBindSceneProperty(animator, transform, type, property, isObjectReference, out result);
			return result;
		}

		public static bool OpenAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			return AnimatorJobExtensions.InternalOpenAnimationStream(animator, ref stream);
		}

		public static void CloseAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			AnimatorJobExtensions.InternalCloseAnimationStream(animator, ref stream);
		}

		public static void ResolveAllStreamHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalResolveAllStreamHandles(animator);
		}

		public static void ResolveAllSceneHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalResolveAllSceneHandles(animator);
		}

		internal static void UnbindAllHandles(this Animator animator)
		{
			AnimatorJobExtensions.InternalUnbindAllHandles(animator);
		}

		private static void InternalAddJobDependency([NotNull("ArgumentNullException")] Animator animator, JobHandle jobHandle)
		{
			AnimatorJobExtensions.InternalAddJobDependency_Injected(animator, ref jobHandle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindStreamTransform([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, out TransformStreamHandle transformStreamHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindStreamProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, [NotNull("ArgumentNullException")] Type type, [NotNull("ArgumentNullException")] string property, bool isObjectReference, out PropertyStreamHandle propertyStreamHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindCustomStreamProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] string property, CustomStreamPropertyType propertyType, out PropertyStreamHandle propertyStreamHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindSceneTransform([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, out TransformSceneHandle transformSceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindSceneProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, [NotNull("ArgumentNullException")] Type type, [NotNull("ArgumentNullException")] string property, bool isObjectReference, out PropertySceneHandle propertySceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalOpenAnimationStream([NotNull("ArgumentNullException")] Animator animator, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalCloseAnimationStream([NotNull("ArgumentNullException")] Animator animator, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResolveAllStreamHandles([NotNull("ArgumentNullException")] Animator animator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResolveAllSceneHandles([NotNull("ArgumentNullException")] Animator animator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnbindAllHandles([NotNull("ArgumentNullException")] Animator animator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalAddJobDependency_Injected(Animator animator, ref JobHandle jobHandle);
	}
}
