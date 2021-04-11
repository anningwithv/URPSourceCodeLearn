using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	[NativeHeader("Runtime/Export/SceneManager/SceneUtility.bindings.h")]
	public static class SceneUtility
	{
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetScenePathByBuildIndex(int buildIndex);

		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetBuildIndexByScenePath(string scenePath);
	}
}
