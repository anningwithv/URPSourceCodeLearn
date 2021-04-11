using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.SceneManagement
{
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h"), RequiredByNativeCode]
	public class SceneManager
	{
		internal static bool s_AllowLoadScene = true;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event UnityAction<Scene, LoadSceneMode> sceneLoaded;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event UnityAction<Scene> sceneUnloaded;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event UnityAction<Scene, Scene> activeSceneChanged;

		public static extern int sceneCount
		{
			[NativeHeader("Runtime/SceneManager/SceneManager.h"), NativeMethod("GetSceneCount"), StaticAccessor("GetSceneManager()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static int sceneCountInBuildSettings
		{
			get
			{
				return SceneManagerAPI.ActiveAPI.GetNumScenesInBuildSettings();
			}
		}

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetActiveScene()
		{
			Scene result;
			SceneManager.GetActiveScene_Injected(out result);
			return result;
		}

		[NativeThrows, StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static bool SetActiveScene(Scene scene)
		{
			return SceneManager.SetActiveScene_Injected(ref scene);
		}

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByPath(string scenePath)
		{
			Scene result;
			SceneManager.GetSceneByPath_Injected(scenePath, out result);
			return result;
		}

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByName(string name)
		{
			Scene result;
			SceneManager.GetSceneByName_Injected(name, out result);
			return result;
		}

		public static Scene GetSceneByBuildIndex(int buildIndex)
		{
			return SceneManagerAPI.ActiveAPI.GetSceneByBuildIndex(buildIndex);
		}

		[NativeThrows, StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneAt(int index)
		{
			Scene result;
			SceneManager.GetSceneAt_Injected(index, out result);
			return result;
		}

		[NativeThrows, StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene CreateScene([NotNull("ArgumentNullException")] string sceneName, CreateSceneParameters parameters)
		{
			Scene result;
			SceneManager.CreateScene_Injected(sceneName, ref parameters, out result);
			return result;
		}

		[NativeThrows, StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static bool UnloadSceneInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneInternal_Injected(ref scene, options);
		}

		[NativeThrows, StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static AsyncOperation UnloadSceneAsyncInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal_Injected(ref scene, options);
		}

		private static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = SceneManagerAPI.ActiveAPI.LoadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
			}
			return result;
		}

		private static AsyncOperation UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation result;
			if (flag)
			{
				outSuccess = false;
				result = null;
			}
			else
			{
				result = SceneManagerAPI.ActiveAPI.UnloadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, immediately, options, out outSuccess);
			}
			return result;
		}

		[NativeThrows, StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static void MergeScenes(Scene sourceScene, Scene destinationScene)
		{
			SceneManager.MergeScenes_Injected(ref sourceScene, ref destinationScene);
		}

		[NativeThrows, StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static void MoveGameObjectToScene([NotNull("ArgumentNullException")] GameObject go, Scene scene)
		{
			SceneManager.MoveGameObjectToScene_Injected(go, ref scene);
		}

		[RequiredByNativeCode]
		internal static AsyncOperation LoadFirstScene_Internal(bool async)
		{
			return SceneManagerAPI.ActiveAPI.LoadFirstScene(async);
		}

		[Obsolete("Use SceneManager.sceneCount and SceneManager.GetSceneAt(int index) to loop the all scenes instead.")]
		public static Scene[] GetAllScenes()
		{
			Scene[] array = new Scene[SceneManager.sceneCount];
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				array[i] = SceneManager.GetSceneAt(i);
			}
			return array;
		}

		public static Scene CreateScene(string sceneName)
		{
			CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.None);
			return SceneManager.CreateScene(sceneName, parameters);
		}

		public static void LoadScene(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneName, parameters);
		}

		[ExcludeFromDocs]
		public static void LoadScene(string sceneName)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneName, parameters);
		}

		public static Scene LoadScene(string sceneName, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		public static void LoadScene(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneBuildIndex, parameters);
		}

		[ExcludeFromDocs]
		public static void LoadScene(int sceneBuildIndex)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneBuildIndex, parameters);
		}

		public static Scene LoadScene(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, parameters);
		}

		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, parameters);
		}

		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, false);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneName, parameters);
		}

		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(string sceneName)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneName, parameters);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, false);
		}

		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(Scene scene)
		{
			return SceneManager.UnloadSceneInternal(scene, UnloadSceneOptions.None);
		}

		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(int sceneBuildIndex)
		{
			bool result;
			SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, true, UnloadSceneOptions.None, out result);
			return result;
		}

		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(string sceneName)
		{
			bool result;
			SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, true, UnloadSceneOptions.None, out result);
			return result;
		}

		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, UnloadSceneOptions.None, out flag);
		}

		public static AsyncOperation UnloadSceneAsync(string sceneName)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, UnloadSceneOptions.None, out flag);
		}

		public static AsyncOperation UnloadSceneAsync(Scene scene)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, UnloadSceneOptions.None);
		}

		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, options, out flag);
		}

		public static AsyncOperation UnloadSceneAsync(string sceneName, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, options, out flag);
		}

		public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, options);
		}

		[RequiredByNativeCode]
		private static void Internal_SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			bool flag = SceneManager.sceneLoaded != null;
			if (flag)
			{
				SceneManager.sceneLoaded(scene, mode);
			}
		}

		[RequiredByNativeCode]
		private static void Internal_SceneUnloaded(Scene scene)
		{
			bool flag = SceneManager.sceneUnloaded != null;
			if (flag)
			{
				SceneManager.sceneUnloaded(scene);
			}
		}

		[RequiredByNativeCode]
		private static void Internal_ActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
		{
			bool flag = SceneManager.activeSceneChanged != null;
			if (flag)
			{
				SceneManager.activeSceneChanged(previousActiveScene, newActiveScene);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveScene_Injected(out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetActiveScene_Injected(ref Scene scene);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByPath_Injected(string scenePath, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByName_Injected(string name, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneAt_Injected(int index, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateScene_Injected(string sceneName, ref CreateSceneParameters parameters, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnloadSceneInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation UnloadSceneAsyncInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MergeScenes_Injected(ref Scene sourceScene, ref Scene destinationScene);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveGameObjectToScene_Injected(GameObject go, ref Scene scene);
	}
}
