using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
	public static class PhysicsSceneExtensions
	{
		public static PhysicsScene GetPhysicsScene(this Scene scene)
		{
			bool flag = !scene.IsValid();
			if (flag)
			{
				throw new ArgumentException("Cannot get physics scene; Unity scene is invalid.", "scene");
			}
			PhysicsScene physicsScene_Internal = PhysicsSceneExtensions.GetPhysicsScene_Internal(scene);
			bool flag2 = physicsScene_Internal.IsValid();
			if (flag2)
			{
				return physicsScene_Internal;
			}
			throw new Exception("The physics scene associated with the Unity scene is invalid.");
		}

		[NativeMethod("GetPhysicsSceneFromUnityScene"), StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		private static PhysicsScene GetPhysicsScene_Internal(Scene scene)
		{
			PhysicsScene result;
			PhysicsSceneExtensions.GetPhysicsScene_Internal_Injected(ref scene, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsScene_Internal_Injected(ref Scene scene, out PhysicsScene ret);
	}
}
