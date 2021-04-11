using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h"), NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	internal static class ResourcesAPIInternal
	{
		[FreeFunction("Resources_Bindings::FindObjectsOfTypeAll"), TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		[FreeFunction("GetScriptMapper().FindShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Shader FindShaderByName(string name);

		[FreeFunction("Resources_Bindings::Load"), NativeThrows, TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object Load(string path, [NotNull("ArgumentNullException")] Type systemTypeInstance);

		[FreeFunction("Resources_Bindings::LoadAll"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] LoadAll([NotNull("ArgumentNullException")] string path, [NotNull("ArgumentNullException")] Type systemTypeInstance);

		[FreeFunction("Resources_Bindings::LoadAsyncInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ResourceRequest LoadAsyncInternal(string path, Type type);

		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnloadAsset(Object assetToUnload);
	}
}
