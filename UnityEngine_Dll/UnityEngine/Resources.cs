using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h"), NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	public sealed class Resources
	{
		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			bool flag = rawObjects == null;
			T[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				T[] array = new T[rawObjects.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (T)((object)rawObjects[i]);
				}
				result = array;
			}
			return result;
		}

		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return ResourcesAPI.ActiveAPI.FindObjectsOfTypeAll(type);
		}

		public static T[] FindObjectsOfTypeAll<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.FindObjectsOfTypeAll(typeof(T)));
		}

		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		public static T Load<T>(string path) where T : Object
		{
			return (T)((object)Resources.Load(path, typeof(T)));
		}

		public static Object Load(string path, Type systemTypeInstance)
		{
			return ResourcesAPI.ActiveAPI.Load(path, systemTypeInstance);
		}

		public static ResourceRequest LoadAsync(string path)
		{
			return Resources.LoadAsync(path, typeof(Object));
		}

		public static ResourceRequest LoadAsync<T>(string path) where T : Object
		{
			return Resources.LoadAsync(path, typeof(T));
		}

		public static ResourceRequest LoadAsync(string path, Type type)
		{
			return ResourcesAPI.ActiveAPI.LoadAsync(path, type);
		}

		public static Object[] LoadAll(string path, Type systemTypeInstance)
		{
			return ResourcesAPI.ActiveAPI.LoadAll(path, systemTypeInstance);
		}

		public static Object[] LoadAll(string path)
		{
			return Resources.LoadAll(path, typeof(Object));
		}

		public static T[] LoadAll<T>(string path) where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.LoadAll(path, typeof(T)));
		}

		[FreeFunction("GetScriptingBuiltinResource", ThrowsException = true), TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object GetBuiltinResource([NotNull("ArgumentNullException")] Type type, string path);

		public static T GetBuiltinResource<T>(string path) where T : Object
		{
			return (T)((object)Resources.GetBuiltinResource(typeof(T), path));
		}

		public static void UnloadAsset(Object assetToUnload)
		{
			ResourcesAPI.ActiveAPI.UnloadAsset(assetToUnload);
		}

		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnloadAssetImplResourceManager(Object assetToUnload);

		[FreeFunction("Resources_Bindings::UnloadUnusedAssets")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation UnloadUnusedAssets();

		[FreeFunction("Resources_Bindings::InstanceIDToObject")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object InstanceIDToObject(int instanceID);

		[FreeFunction("Resources_Bindings::InstanceIDToObjectList")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InstanceIDToObjectList(IntPtr instanceIDs, int instanceCount, List<Object> objects);

		public static void InstanceIDToObjectList(NativeArray<int> instanceIDs, List<Object> objects)
		{
			bool flag = !instanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "instanceIDs");
			}
			bool flag2 = objects == null;
			if (flag2)
			{
				throw new ArgumentNullException("objects");
			}
			bool flag3 = instanceIDs.Length == 0;
			if (flag3)
			{
				objects.Clear();
			}
			else
			{
				Resources.InstanceIDToObjectList((IntPtr)instanceIDs.GetUnsafeReadOnlyPtr<int>(), instanceIDs.Length, objects);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use AssetDatabase.LoadAssetAtPath instead (UnityUpgradable) -> * [UnityEditor] UnityEditor.AssetDatabase.LoadAssetAtPath(*)", true), TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		public static Object LoadAssetAtPath(string assetPath, Type type)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use AssetDatabase.LoadAssetAtPath<T>() instead (UnityUpgradable) -> * [UnityEditor] UnityEditor.AssetDatabase.LoadAssetAtPath<T>(*)", true)]
		public static T LoadAssetAtPath<T>(string assetPath) where T : Object
		{
			return default(T);
		}
	}
}
