using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Mono/MonoBehaviour.h"), ExtensionOfNativeClass, NativeClass(null), RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ScriptableObject : Object
	{
		public ScriptableObject()
		{
			ScriptableObject.CreateScriptableObject(this);
		}

		[Obsolete("Use EditorUtility.SetDirty instead"), NativeConditional("ENABLE_MONO")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDirty();

		public static ScriptableObject CreateInstance(string className)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromName(className);
		}

		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromType(type, true);
		}

		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateScriptableObject([Writable] ScriptableObject self);

		[FreeFunction("Scripting::CreateScriptableObject")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject CreateScriptableObjectInstanceFromName(string className);

		[FreeFunction("Scripting::CreateScriptableObjectWithType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ScriptableObject CreateScriptableObjectInstanceFromType(Type type, bool applyDefaultsAndReset);

		[FreeFunction("Scripting::ResetAndApplyDefaultInstances")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetAndApplyDefaultInstances([NotNull("NullExceptionObject")] Object obj);
	}
}
