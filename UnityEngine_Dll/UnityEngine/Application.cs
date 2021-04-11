using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Logging/LogSystem.h"), NativeHeader("Runtime/Application/ApplicationInfo.h"), NativeHeader("Runtime/PreloadManager/PreloadManager.h"), NativeHeader("Runtime/Utilities/Argv.h"), NativeHeader("Runtime/Utilities/URLUtility.h"), NativeHeader("Runtime/Misc/SystemInfo.h"), NativeHeader("Runtime/Misc/PlayerSettings.h"), NativeHeader("Runtime/Misc/Player.h"), NativeHeader("Runtime/Export/Application/Application.bindings.h"), NativeHeader("Runtime/Application/AdsIdHandler.h"), NativeHeader("Runtime/Input/TargetFrameRate.h"), NativeHeader("Runtime/Input/InputManager.h"), NativeHeader("Runtime/PreloadManager/LoadSceneOperation.h"), NativeHeader("Runtime/Input/GetInput.h"), NativeHeader("Runtime/File/ApplicationSpecificPersistentDataPath.h"), NativeHeader("Runtime/Network/NetworkUtility.h"), NativeHeader("Runtime/BaseClasses/IsPlaying.h"), NativeHeader("Runtime/Misc/BuildSettings.h")]
	public class Application
	{
		public delegate void AdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled, string errorMsg);

		public delegate void LowMemoryCallback();

		public delegate void LogCallback(string condition, string stackTrace, LogType type);

		private static Application.LogCallback s_LogCallbackHandler;

		private static Application.LogCallback s_LogCallbackHandlerThreaded;

		internal static Application.AdvertisingIdentifierCallback OnAdvertisingIdentifierCallback;

		private static volatile Application.LogCallback s_RegisterLogCallbackDeprecated;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Application.LowMemoryCallback lowMemory;

		public static event Application.LogCallback logMessageReceived
		{
			add
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandler, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandler, value);
			}
		}

		public static event Application.LogCallback logMessageReceivedThreaded
		{
			add
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandlerThreaded, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandlerThreaded, value);
			}
		}

		public static event UnityAction onBeforeRender
		{
			add
			{
				BeforeRenderHelper.RegisterCallback(value);
			}
			remove
			{
				BeforeRenderHelper.UnregisterCallback(value);
			}
		}

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<bool> focusChanged;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<string> deepLinkActivated;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Func<bool> wantsToQuit;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action quitting;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action unloading;

		[Obsolete("This property is deprecated, please use LoadLevelAsync to detect if a specific scene is currently loading.")]
		public static extern bool isLoadingLevel
		{
			[FreeFunction("GetPreloadManager().IsLoadingOrQueued")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This property is deprecated and always returns 0.")]
		public static int streamedBytes
		{
			get
			{
				return 0;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Application.webSecurityEnabled is no longer supported, since the Unity Web Player is no longer supported by Unity", true)]
		public static bool webSecurityEnabled
		{
			get
			{
				return false;
			}
		}

		public static extern bool isPlaying
		{
			[FreeFunction("IsWorldPlaying")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool isFocused
		{
			[FreeFunction("IsPlayerFocused")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string buildGUID
		{
			[FreeFunction("Application_Bindings::GetBuildGUID")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool runInBackground
		{
			[FreeFunction("GetPlayerSettingsRunInBackground")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetPlayerSettingsRunInBackground")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool isBatchMode
		{
			[FreeFunction("::IsBatchmode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern bool isTestRun
		{
			[FreeFunction("::IsTestRun")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern bool isHumanControllingUs
		{
			[FreeFunction("::IsHumanControllingUs")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string dataPath
		{
			[FreeFunction("GetAppDataPath")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string streamingAssetsPath
		{
			[FreeFunction("GetStreamingAssetsPath", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string persistentDataPath
		{
			[FreeFunction("GetPersistentDataPathApplicationSpecific")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string temporaryCachePath
		{
			[FreeFunction("GetTemporaryCachePathApplicationSpecific")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string absoluteURL
		{
			[FreeFunction("GetPlayerSettings().GetAbsoluteURL")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string unityVersion
		{
			[FreeFunction("Application_Bindings::GetUnityVersion", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern int unityVersionVer
		{
			[FreeFunction("Application_Bindings::GetUnityVersionVer", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern int unityVersionMaj
		{
			[FreeFunction("Application_Bindings::GetUnityVersionMaj", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern int unityVersionMin
		{
			[FreeFunction("Application_Bindings::GetUnityVersionMin", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string version
		{
			[FreeFunction("GetApplicationInfo().GetVersion")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string installerName
		{
			[FreeFunction("GetApplicationInfo().GetInstallerName")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string identifier
		{
			[FreeFunction("GetApplicationInfo().GetApplicationIdentifier")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ApplicationInstallMode installMode
		{
			[FreeFunction("GetApplicationInfo().GetInstallMode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ApplicationSandboxType sandboxType
		{
			[FreeFunction("GetApplicationInfo().GetSandboxType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string productName
		{
			[FreeFunction("GetPlayerSettings().GetProductName")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string companyName
		{
			[FreeFunction("GetPlayerSettings().GetCompanyName")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string cloudProjectId
		{
			[FreeFunction("GetPlayerSettings().GetCloudProjectId")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern int targetFrameRate
		{
			[FreeFunction("GetTargetFrameRate")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetTargetFrameRate")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use SetStackTraceLogType/GetStackTraceLogType instead")]
		public static extern StackTraceLogType stackTraceLogType
		{
			[FreeFunction("Application_Bindings::GetStackTraceLogType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("Application_Bindings::SetStackTraceLogType")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string consoleLogPath
		{
			[FreeFunction("GetConsoleLogPath")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ThreadPriority backgroundLoadingPriority
		{
			[FreeFunction("GetPreloadManager().GetThreadPriority")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("GetPreloadManager().SetThreadPriority")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool genuine
		{
			[FreeFunction("IsApplicationGenuine")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool genuineCheckAvailable
		{
			[FreeFunction("IsApplicationGenuineAvailable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern bool submitAnalytics
		{
			[FreeFunction("GetPlayerSettings().GetSubmitAnalytics")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("This property is deprecated, please use SplashScreen.isFinished instead")]
		public static bool isShowingSplashScreen
		{
			get
			{
				return !SplashScreen.isFinished;
			}
		}

		public static RuntimePlatform platform
		{
			get
			{
				return ShimManager.applicationShim.platform;
			}
		}

		public static bool isMobilePlatform
		{
			get
			{
				return ShimManager.applicationShim.isMobilePlatform;
			}
		}

		public static bool isConsolePlatform
		{
			get
			{
				return ShimManager.applicationShim.isConsolePlatform;
			}
		}

		public static SystemLanguage systemLanguage
		{
			get
			{
				return ShimManager.applicationShim.systemLanguage;
			}
		}

		public static NetworkReachability internetReachability
		{
			get
			{
				return ShimManager.applicationShim.internetReachability;
			}
		}

		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !Application.isEditor;
			}
		}

		[Obsolete("Use SceneManager.sceneCountInBuildSettings")]
		public static int levelCount
		{
			get
			{
				return SceneManager.sceneCountInBuildSettings;
			}
		}

		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static int loadedLevel
		{
			get
			{
				return SceneManager.GetActiveScene().buildIndex;
			}
		}

		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static string loadedLevelName
		{
			get
			{
				return SceneManager.GetActiveScene().name;
			}
		}

		public static bool isEditor
		{
			get
			{
				return ShimManager.applicationShim.isEditor;
			}
		}

		[FreeFunction("GetInputManager().QuitApplication")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Quit(int exitCode);

		public static void Quit()
		{
			Application.Quit(0);
		}

		[Obsolete("CancelQuit is deprecated. Use the wantsToQuit event instead."), FreeFunction("GetInputManager().CancelQuitApplication")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CancelQuit();

		[FreeFunction("Application_Bindings::Unload")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Unload();

		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This function is deprecated and always returns 1.0 for valid level indices.")]
		public static float GetStreamProgressForLevel(int levelIndex)
		{
			bool flag = levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings;
			float result;
			if (flag)
			{
				result = 1f;
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This function is deprecated and always returns 1.0.")]
		public static float GetStreamProgressForLevel(string levelName)
		{
			return 1f;
		}

		public static bool CanStreamedLevelBeLoaded(int levelIndex)
		{
			return levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings;
		}

		[FreeFunction("Application_Bindings::CanStreamedLevelBeLoaded")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CanStreamedLevelBeLoaded(string levelName);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPlaying([NotNull("NullExceptionObject")] Object obj);

		[FreeFunction("GetBuildSettings().GetBuildTags")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetBuildTags();

		[FreeFunction("GetBuildSettings().SetBuildTags")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBuildTags(string[] buildTags);

		[FreeFunction("GetBuildSettings().GetHasPROVersion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasProLicense();

		[FreeFunction("HasARGV")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasARGV(string name);

		[FreeFunction("GetFirstValueForARGV")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetValueForARGV(string name);

		[Obsolete("Application.ExternalEval is deprecated. See https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html for alternatives.")]
		public static void ExternalEval(string script)
		{
			bool flag = script.Length > 0 && script[script.Length - 1] != ';';
			if (flag)
			{
				script += ";";
			}
			Application.Internal_ExternalCall(script);
		}

		[FreeFunction("Application_Bindings::ExternalCall")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExternalCall(string script);

		[FreeFunction("GetAdsIdHandler().RequestAdsIdAsync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RequestAdvertisingIdentifierAsync(Application.AdvertisingIdentifierCallback delegateMethod);

		[FreeFunction("OpenURL")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OpenURL(string url);

		[Obsolete("Use UnityEngine.Diagnostics.Utils.ForceCrash")]
		public static void ForceCrash(int mode)
		{
			Utils.ForceCrash((ForcedCrashCategory)mode);
		}

		[FreeFunction("Application_Bindings::SetLogCallbackDefined")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLogCallbackDefined(bool defined);

		[FreeFunction("GetStackTraceLogType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern StackTraceLogType GetStackTraceLogType(LogType logType);

		[FreeFunction("SetStackTraceLogType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStackTraceLogType(LogType logType, StackTraceLogType stackTraceType);

		[FreeFunction("Application_Bindings::RequestUserAuthorization")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation RequestUserAuthorization(UserAuthorization mode);

		[FreeFunction("Application_Bindings::HasUserAuthorization")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasUserAuthorization(UserAuthorization mode);

		[RequiredByNativeCode]
		internal static void CallLowMemory()
		{
			Application.LowMemoryCallback lowMemoryCallback = Application.lowMemory;
			bool flag = lowMemoryCallback != null;
			if (flag)
			{
				lowMemoryCallback();
			}
		}

		[RequiredByNativeCode]
		private static void CallLogCallback(string logString, string stackTrace, LogType type, bool invokedOnMainThread)
		{
			if (invokedOnMainThread)
			{
				Application.LogCallback logCallback = Application.s_LogCallbackHandler;
				bool flag = logCallback != null;
				if (flag)
				{
					logCallback(logString, stackTrace, type);
				}
			}
			Application.LogCallback logCallback2 = Application.s_LogCallbackHandlerThreaded;
			bool flag2 = logCallback2 != null;
			if (flag2)
			{
				logCallback2(logString, stackTrace, type);
			}
		}

		internal static void InvokeOnAdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled)
		{
			bool flag = Application.OnAdvertisingIdentifierCallback != null;
			if (flag)
			{
				Application.OnAdvertisingIdentifierCallback(advertisingId, trackingEnabled, string.Empty);
			}
		}

		private static string ObjectToJSString(object o)
		{
			bool flag = o == null;
			string result;
			if (flag)
			{
				result = "null";
			}
			else
			{
				bool flag2 = o is string;
				if (flag2)
				{
					string text = o.ToString().Replace("\\", "\\\\");
					text = text.Replace("\"", "\\\"");
					text = text.Replace("\n", "\\n");
					text = text.Replace("\r", "\\r");
					text = text.Replace("\0", "");
					text = text.Replace("\u2028", "");
					text = text.Replace("\u2029", "");
					result = "\"" + text + "\"";
				}
				else
				{
					bool flag3 = o is int || o is short || o is uint || o is ushort || o is byte;
					if (flag3)
					{
						result = o.ToString();
					}
					else
					{
						bool flag4 = o is float;
						if (flag4)
						{
							NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
							result = ((float)o).ToString(numberFormat);
						}
						else
						{
							bool flag5 = o is double;
							if (flag5)
							{
								NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
								result = ((double)o).ToString(numberFormat2);
							}
							else
							{
								bool flag6 = o is char;
								if (flag6)
								{
									bool flag7 = (char)o == '"';
									if (flag7)
									{
										result = "\"\\\"\"";
									}
									else
									{
										result = "\"" + o.ToString() + "\"";
									}
								}
								else
								{
									bool flag8 = o is IList;
									if (flag8)
									{
										IList list = (IList)o;
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append("new Array(");
										int count = list.Count;
										for (int i = 0; i < count; i++)
										{
											bool flag9 = i != 0;
											if (flag9)
											{
												stringBuilder.Append(", ");
											}
											stringBuilder.Append(Application.ObjectToJSString(list[i]));
										}
										stringBuilder.Append(")");
										result = stringBuilder.ToString();
									}
									else
									{
										result = Application.ObjectToJSString(o.ToString());
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		[Obsolete("Application.ExternalCall is deprecated. See https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html for alternatives.")]
		public static void ExternalCall(string functionName, params object[] args)
		{
			Application.Internal_ExternalCall(Application.BuildInvocationForArguments(functionName, args));
		}

		private static string BuildInvocationForArguments(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = i != 0;
				if (flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(Application.ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		public static void DontDestroyOnLoad(Object o)
		{
			bool flag = o != null;
			if (flag)
			{
				Object.DontDestroyOnLoad(o);
			}
		}

		[Obsolete("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead (UnityUpgradable) -> [UnityEngine] UnityEngine.ScreenCapture.CaptureScreenshot(*)", true)]
		public static void CaptureScreenshot(string filename, int superSize)
		{
			throw new NotSupportedException("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead.");
		}

		[Obsolete("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead (UnityUpgradable) -> [UnityEngine] UnityEngine.ScreenCapture.CaptureScreenshot(*)", true)]
		public static void CaptureScreenshot(string filename)
		{
			throw new NotSupportedException("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead.");
		}

		[RequiredByNativeCode]
		private static bool Internal_ApplicationWantsToQuit()
		{
			bool flag = Application.wantsToQuit != null;
			bool result;
			if (flag)
			{
				Delegate[] invocationList = Application.wantsToQuit.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Func<bool> func = (Func<bool>)invocationList[i];
					try
					{
						bool flag2 = !func();
						if (flag2)
						{
							result = false;
							return result;
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
				}
			}
			result = true;
			return result;
		}

		[RequiredByNativeCode]
		private static void Internal_ApplicationQuit()
		{
			bool flag = Application.quitting != null;
			if (flag)
			{
				Application.quitting();
			}
		}

		[RequiredByNativeCode]
		private static void Internal_ApplicationUnload()
		{
			bool flag = Application.unloading != null;
			if (flag)
			{
				Application.unloading();
			}
		}

		[RequiredByNativeCode]
		internal static void InvokeOnBeforeRender()
		{
			BeforeRenderHelper.Invoke();
		}

		[RequiredByNativeCode]
		internal static void InvokeFocusChanged(bool focus)
		{
			bool flag = Application.focusChanged != null;
			if (flag)
			{
				Application.focusChanged(focus);
			}
		}

		[RequiredByNativeCode]
		internal static void InvokeDeepLinkActivated(string url)
		{
			bool flag = Application.deepLinkActivated != null;
			if (flag)
			{
				Application.deepLinkActivated(url);
			}
		}

		[Obsolete("Application.RegisterLogCallback is deprecated. Use Application.logMessageReceived instead.")]
		public static void RegisterLogCallback(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, false);
		}

		[Obsolete("Application.RegisterLogCallbackThreaded is deprecated. Use Application.logMessageReceivedThreaded instead.")]
		public static void RegisterLogCallbackThreaded(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, true);
		}

		private static void RegisterLogCallback(Application.LogCallback handler, bool threaded)
		{
			bool flag = Application.s_RegisterLogCallbackDeprecated != null;
			if (flag)
			{
				Application.logMessageReceived -= Application.s_RegisterLogCallbackDeprecated;
				Application.logMessageReceivedThreaded -= Application.s_RegisterLogCallbackDeprecated;
			}
			Application.s_RegisterLogCallbackDeprecated = handler;
			bool flag2 = handler != null;
			if (flag2)
			{
				if (threaded)
				{
					Application.logMessageReceivedThreaded += handler;
				}
				else
				{
					Application.logMessageReceived += handler;
				}
			}
		}

		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Single);
		}

		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Single);
		}

		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Additive);
		}

		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Additive);
		}

		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
		}

		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
		}

		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
		}

		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
		}

		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(int index)
		{
			return SceneManager.UnloadScene(index);
		}

		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(string scenePath)
		{
			return SceneManager.UnloadScene(scenePath);
		}
	}
}
