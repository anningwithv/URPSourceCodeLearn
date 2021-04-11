using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/SceneManager/SceneManager.h"), NativeHeader("Runtime/GameCode/CloneObject.h"), NativeHeader("Runtime/Export/Scripting/UnityEngineObject.bindings.h"), RequiredByNativeCode(GenerateProxy = true)]
	[StructLayout(LayoutKind.Sequential)]
	public class Object
	{
		private IntPtr m_CachedPtr;

		private int m_InstanceID;

		private string m_UnityRuntimeErrorString;

		internal static int OffsetOfInstanceIDInCPlusPlusObject = -1;

		private const string objectIsNullMessage = "The Object you want to instantiate is null.";

		private const string cloneDestroyedMessage = "Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.";

		public string name
		{
			get
			{
				return Object.GetName(this);
			}
			set
			{
				Object.SetName(this, value);
			}
		}

		public extern HideFlags hideFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[SecuritySafeCritical]
		public int GetInstanceID()
		{
			this.EnsureRunningOnMainThread();
			return this.m_InstanceID;
		}

		public override int GetHashCode()
		{
			return this.m_InstanceID;
		}

		public override bool Equals(object other)
		{
			Object @object = other as Object;
			bool flag = @object == null && other != null && !(other is Object);
			return !flag && Object.CompareBaseObjects(this, @object);
		}

		public static implicit operator bool(Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			bool flag = lhs == null;
			bool flag2 = rhs == null;
			bool flag3 = flag2 & flag;
			bool result;
			if (flag3)
			{
				result = true;
			}
			else
			{
				bool flag4 = flag2;
				if (flag4)
				{
					result = !Object.IsNativeObjectAlive(lhs);
				}
				else
				{
					bool flag5 = flag;
					if (flag5)
					{
						result = !Object.IsNativeObjectAlive(rhs);
					}
					else
					{
						result = (lhs.m_InstanceID == rhs.m_InstanceID);
					}
				}
			}
			return result;
		}

		private void EnsureRunningOnMainThread()
		{
			bool flag = !Object.CurrentThreadIsMainThread();
			if (flag)
			{
				throw new InvalidOperationException("EnsureRunningOnMainThread can only be called from the main thread");
			}
		}

		private static bool IsNativeObjectAlive(Object o)
		{
			bool flag = o.GetCachedPtr() != IntPtr.Zero;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = o is MonoBehaviour || o is ScriptableObject;
				result = (!flag2 && Object.DoesObjectWithInstanceIDExist(o.GetInstanceID()));
			}
			return result;
		}

		private IntPtr GetCachedPtr()
		{
			return this.m_CachedPtr;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			bool flag = original is ScriptableObject;
			if (flag)
			{
				throw new ArgumentException("Cannot instantiate a ScriptableObject with a position and rotation");
			}
			Object @object = Object.Internal_InstantiateSingle(original, position, rotation);
			bool flag2 = @object == null;
			if (flag2)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
		{
			bool flag = parent == null;
			Object result;
			if (flag)
			{
				result = Object.Instantiate(original, position, rotation);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object @object = Object.Internal_InstantiateSingleWithParent(original, parent, position, rotation);
				bool flag2 = @object == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				result = @object;
			}
			return result;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object @object = Object.Internal_CloneSingle(original);
			bool flag = @object == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent)
		{
			return Object.Instantiate(original, parent, false);
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace)
		{
			bool flag = parent == null;
			Object result;
			if (flag)
			{
				result = Object.Instantiate(original);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object @object = Object.Internal_CloneSingleWithParent(original, parent, instantiateInWorldSpace);
				bool flag2 = @object == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				result = @object;
			}
			return result;
		}

		public static T Instantiate<T>(T original) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			T t = (T)((object)Object.Internal_CloneSingle(original));
			bool flag = t == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return t;
		}

		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation));
		}

		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation, parent));
		}

		public static T Instantiate<T>(T original, Transform parent) where T : Object
		{
			return Object.Instantiate<T>(original, parent, false);
		}

		public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
		{
			return (T)((object)Object.Instantiate(original, parent, worldPositionStays));
		}

		[NativeMethod(Name = "Scripting::DestroyObjectFromScripting", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);

		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float t = 0f;
			Object.Destroy(obj, t);
		}

		[NativeMethod(Name = "Scripting::DestroyObjectFromScriptingImmediate", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);

		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool allowDestroyingAssets = false;
			Object.DestroyImmediate(obj, allowDestroyingAssets);
		}

		public static Object[] FindObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type, false);
		}

		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfType"), TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfType(Type type, bool includeInactive);

		[FreeFunction("GetSceneManager().DontDestroyOnLoad", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DontDestroyOnLoad([NotNull("NullExceptionObject")] Object target);

		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj, [DefaultValue("0.0F")] float t)
		{
			Object.Destroy(obj, t);
		}

		[Obsolete("use Object.Destroy instead."), ExcludeFromDocs]
		public static void DestroyObject(Object obj)
		{
			float t = 0f;
			Object.Destroy(obj, t);
		}

		[Obsolete("warning use Object.FindObjectsOfType instead.")]
		public static Object[] FindSceneObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type);
		}

		[Obsolete("use Resources.FindObjectsOfTypeAll instead."), FreeFunction("UnityEngineObjectBindings::FindObjectsOfTypeIncludingAssets")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), false));
		}

		public static T[] FindObjectsOfType<T>(bool includeInactive) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), includeInactive));
		}

		public static T FindObjectOfType<T>() where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), false));
		}

		public static T FindObjectOfType<T>(bool includeInactive) where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), includeInactive));
		}

		[Obsolete("Please use Resources.FindObjectsOfTypeAll instead")]
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		private static void CheckNullArgument(object arg, string message)
		{
			bool flag = arg == null;
			if (flag)
			{
				throw new ArgumentException(message);
			}
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = Object.FindObjectsOfType(type, false);
			bool flag = array.Length != 0;
			Object result;
			if (flag)
			{
				result = array[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type, bool includeInactive)
		{
			Object[] array = Object.FindObjectsOfType(type, includeInactive);
			bool flag = array.Length != 0;
			Object result;
			if (flag)
			{
				result = array[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public override string ToString()
		{
			return Object.ToString(this);
		}

		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		[NativeMethod(Name = "Object::GetOffsetOfInstanceIdMember", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetOffsetOfInstanceIDInCPlusPlusObject();

		[NativeMethod(Name = "CurrentThreadIsMainThread", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CurrentThreadIsMainThread();

		[FreeFunction("CloneObject")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_CloneSingle([NotNull("NullExceptionObject")] Object data);

		[FreeFunction("CloneObject")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_CloneSingleWithParent([NotNull("NullExceptionObject")] Object data, [NotNull("NullExceptionObject")] Transform parent, bool worldPositionStays);

		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingle([NotNull("NullExceptionObject")] Object data, Vector3 pos, Quaternion rot)
		{
			return Object.Internal_InstantiateSingle_Injected(data, ref pos, ref rot);
		}

		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingleWithParent([NotNull("NullExceptionObject")] Object data, [NotNull("NullExceptionObject")] Transform parent, Vector3 pos, Quaternion rot)
		{
			return Object.Internal_InstantiateSingleWithParent_Injected(data, parent, ref pos, ref rot);
		}

		[FreeFunction("UnityEngineObjectBindings::ToString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ToString(Object obj);

		[FreeFunction("UnityEngineObjectBindings::GetName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetName([NotNull("NullExceptionObject")] Object obj);

		[FreeFunction("UnityEngineObjectBindings::IsPersistent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsPersistent([NotNull("NullExceptionObject")] Object obj);

		[FreeFunction("UnityEngineObjectBindings::SetName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName([NotNull("NullExceptionObject")] Object obj, string name);

		[NativeMethod(Name = "UnityEngineObjectBindings::DoesObjectWithInstanceIDExist", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool DoesObjectWithInstanceIDExist(int instanceID);

		[FreeFunction("UnityEngineObjectBindings::FindObjectFromInstanceID"), VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object FindObjectFromInstanceID(int instanceID);

		[FreeFunction("UnityEngineObjectBindings::ForceLoadFromInstanceID"), VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object ForceLoadFromInstanceID(int instanceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_InstantiateSingle_Injected(Object data, ref Vector3 pos, ref Quaternion rot);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_InstantiateSingleWithParent_Injected(Object data, Transform parent, ref Vector3 pos, ref Quaternion rot);
	}
}
