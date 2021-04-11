using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
	public static class PhysicsSceneExtensions2D
	{
		public static PhysicsScene2D GetPhysicsScene2D(this Scene scene)
		{
			bool flag = !scene.IsValid();
			if (flag)
			{
				throw new ArgumentException("Cannot get physics scene; Unity scene is invalid.", "scene");
			}
			PhysicsScene2D physicsScene_Internal = PhysicsSceneExtensions2D.GetPhysicsScene_Internal(scene);
			bool flag2 = physicsScene_Internal.IsValid();
			if (flag2)
			{
				return physicsScene_Internal;
			}
			throw new Exception("The physics scene associated with the Unity scene is invalid.");
		}

		[NativeMethod("GetPhysicsSceneFromUnityScene"), StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		private static PhysicsScene2D GetPhysicsScene_Internal(Scene scene)
		{
			PhysicsScene2D result;
			PhysicsSceneExtensions2D.GetPhysicsScene_Internal_Injected(ref scene, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsScene_Internal_Injected(ref Scene scene, out PhysicsScene2D ret);
	}
}
