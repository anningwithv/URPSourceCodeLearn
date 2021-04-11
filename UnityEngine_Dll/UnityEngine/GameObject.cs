using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Scripting/GameObject.bindings.h"), ExcludeFromPreset, UsedByNativeCode]
	public sealed class GameObject : Object
	{
		public extern Transform transform
		{
			[FreeFunction("GameObjectBindings::GetTransform", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int layer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public extern bool active
		{
			[NativeMethod(Name = "IsActive")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod(Name = "SetSelfActive")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool activeSelf
		{
			[NativeMethod(Name = "IsSelfActive")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool activeInHierarchy
		{
			[NativeMethod(Name = "IsActive")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isStatic
		{
			[NativeMethod(Name = "GetIsStaticDeprecated")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod(Name = "SetIsStaticDeprecated")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern bool isStaticBatchable
		{
			[NativeMethod(Name = "IsStaticBatchable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string tag
		{
			[FreeFunction("GameObjectBindings::GetTag", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("GameObjectBindings::SetTag", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Scene scene
		{
			[FreeFunction("GameObjectBindings::GetScene", HasExplicitThis = true)]
			get
			{
				Scene result;
				this.get_scene_Injected(out result);
				return result;
			}
		}

		public extern ulong sceneCullingMask
		{
			[FreeFunction(Name = "GameObjectBindings::GetSceneCullingMask", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property rigidbody has been deprecated. Use GetComponent<Rigidbody>() instead. (UnityUpgradable)", true)]
		public Component rigidbody
		{
			get
			{
				throw new NotSupportedException("rigidbody property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property rigidbody2D has been deprecated. Use GetComponent<Rigidbody2D>() instead. (UnityUpgradable)", true)]
		public Component rigidbody2D
		{
			get
			{
				throw new NotSupportedException("rigidbody2D property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property camera has been deprecated. Use GetComponent<Camera>() instead. (UnityUpgradable)", true)]
		public Component camera
		{
			get
			{
				throw new NotSupportedException("camera property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property light has been deprecated. Use GetComponent<Light>() instead. (UnityUpgradable)", true)]
		public Component light
		{
			get
			{
				throw new NotSupportedException("light property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property animation has been deprecated. Use GetComponent<Animation>() instead. (UnityUpgradable)", true)]
		public Component animation
		{
			get
			{
				throw new NotSupportedException("animation property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property constantForce has been deprecated. Use GetComponent<ConstantForce>() instead. (UnityUpgradable)", true)]
		public Component constantForce
		{
			get
			{
				throw new NotSupportedException("constantForce property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property renderer has been deprecated. Use GetComponent<Renderer>() instead. (UnityUpgradable)", true)]
		public Component renderer
		{
			get
			{
				throw new NotSupportedException("renderer property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property audio has been deprecated. Use GetComponent<AudioSource>() instead. (UnityUpgradable)", true)]
		public Component audio
		{
			get
			{
				throw new NotSupportedException("audio property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property networkView has been deprecated. Use GetComponent<NetworkView>() instead. (UnityUpgradable)", true)]
		public Component networkView
		{
			get
			{
				throw new NotSupportedException("networkView property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property collider has been deprecated. Use GetComponent<Collider>() instead. (UnityUpgradable)", true)]
		public Component collider
		{
			get
			{
				throw new NotSupportedException("collider property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property collider2D has been deprecated. Use GetComponent<Collider2D>() instead. (UnityUpgradable)", true)]
		public Component collider2D
		{
			get
			{
				throw new NotSupportedException("collider2D property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property hingeJoint has been deprecated. Use GetComponent<HingeJoint>() instead. (UnityUpgradable)", true)]
		public Component hingeJoint
		{
			get
			{
				throw new NotSupportedException("hingeJoint property has been deprecated");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Property particleSystem has been deprecated. Use GetComponent<ParticleSystem>() instead. (UnityUpgradable)", true)]
		public Component particleSystem
		{
			get
			{
				throw new NotSupportedException("particleSystem property has been deprecated");
			}
		}

		[FreeFunction("GameObjectBindings::CreatePrimitive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		[FreeFunction(Name = "GameObjectBindings::GetComponentFromType", HasExplicitThis = true, ThrowsException = true), TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponent(Type type);

		[FreeFunction(Name = "GameObjectBindings::GetComponentFastPath", HasExplicitThis = true, ThrowsException = true), NativeWritableSelf]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		[FreeFunction(Name = "Scripting::GetScriptingWrapperOfComponentOfGameObject", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Component GetComponentByName(string type);

		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		[FreeFunction(Name = "GameObjectBindings::GetComponentInChildren", HasExplicitThis = true, ThrowsException = true), TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponentInChildren(Type type, bool includeInactive);

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			return this.GetComponentInChildren(type, false);
		}

		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			bool includeInactive = false;
			return this.GetComponentInChildren<T>(includeInactive);
		}

		public T GetComponentInChildren<T>([UnityEngine.Internal.DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		[FreeFunction(Name = "GameObjectBindings::GetComponentInParent", HasExplicitThis = true, ThrowsException = true), TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponentInParent(Type type, bool includeInactive);

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type type)
		{
			return this.GetComponentInParent(type, false);
		}

		[ExcludeFromDocs]
		public T GetComponentInParent<T>()
		{
			bool includeInactive = false;
			return this.GetComponentInParent<T>(includeInactive);
		}

		public T GetComponentInParent<T>([UnityEngine.Internal.DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInParent(typeof(T), includeInactive));
		}

		[FreeFunction(Name = "GameObjectBindings::GetComponentsInternal", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Array GetComponentsInternal(Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList);

		public Component[] GetComponents(Type type)
		{
			return (Component[])this.GetComponentsInternal(type, false, false, true, false, null);
		}

		public T[] GetComponents<T>()
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false, null);
		}

		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsInternal(type, false, false, true, false, results);
		}

		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, false, true, false, results);
		}

		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentsInChildren(type, includeInactive);
		}

		public Component[] GetComponentsInChildren(Type type, [UnityEngine.Internal.DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, false, null);
		}

		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, null);
		}

		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, results);
		}

		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool includeInactive = false;
			return this.GetComponentsInParent(type, includeInactive);
		}

		public Component[] GetComponentsInParent(Type type, [UnityEngine.Internal.DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, true, null);
		}

		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, results);
		}

		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, null);
		}

		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		[SecuritySafeCritical]
		public unsafe bool TryGetComponent<T>(out T component)
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.TryGetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			component = castHelper.t;
			return castHelper.t != null;
		}

		public bool TryGetComponent(Type type, out Component component)
		{
			component = this.TryGetComponentInternal(type);
			return component != null;
		}

		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFromType", HasExplicitThis = true, ThrowsException = true), TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Component TryGetComponentInternal(Type type);

		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFastPath", HasExplicitThis = true, ThrowsException = true), NativeWritableSelf]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void TryGetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		[FreeFunction(Name = "MonoAddComponent", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Component AddComponentInternal(string className);

		[FreeFunction(Name = "MonoAddComponentWithType", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component Internal_AddComponentWithType(Type componentType);

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		[NativeMethod(Name = "SetSelfActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetActive(bool value);

		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children."), NativeMethod(Name = "SetActiveRecursivelyDeprecated")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetActiveRecursively(bool state);

		[FreeFunction(Name = "GameObjectBindings::CompareTag", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CompareTag(string tag);

		[FreeFunction(Name = "GameObjectBindings::FindGameObjectWithTag", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		[FreeFunction(Name = "GameObjectBindings::FindGameObjectsWithTag", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

		[FreeFunction(Name = "Scripting::SendScriptingMessageUpwards", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessageUpwards(string methodName, [UnityEngine.Internal.DefaultValue("null")] object value, [UnityEngine.Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, options);
		}

		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessageUpwards(methodName, value, options);
		}

		[FreeFunction(Name = "Scripting::SendScriptingMessage", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessage(string methodName, [UnityEngine.Internal.DefaultValue("null")] object value, [UnityEngine.Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, options);
		}

		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			this.SendMessage(methodName, value, options);
		}

		[FreeFunction(Name = "Scripting::BroadcastScriptingMessage", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BroadcastMessage(string methodName, [UnityEngine.Internal.DefaultValue("null")] object parameter, [UnityEngine.Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, options);
		}

		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object parameter = null;
			this.BroadcastMessage(methodName, parameter, options);
		}

		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			for (int i = 0; i < components.Length; i++)
			{
				Type componentType = components[i];
				this.AddComponent(componentType);
			}
		}

		[FreeFunction(Name = "GameObjectBindings::Internal_CreateGameObject")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateGameObject([Writable] GameObject self, string name);

		[FreeFunction(Name = "GameObjectBindings::Find")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject Find(string name);

		[FreeFunction(Name = "GameObjectBindings::CalculateBounds", HasExplicitThis = true)]
		internal Bounds CalculateBounds()
		{
			Bounds result;
			this.CalculateBounds_Injected(out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int IsMarkedVisible();

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("GameObject.SampleAnimation(AnimationClip, float) has been deprecated. Use AnimationClip.SampleAnimation(GameObject, float) instead (UnityUpgradable).", true)]
		public void SampleAnimation(Object clip, float time)
		{
			throw new NotSupportedException("GameObject.SampleAnimation is deprecated");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("GameObject.AddComponent with string argument has been deprecated. Use GameObject.AddComponent<T>() instead. (UnityUpgradable).", true)]
		public Component AddComponent(string className)
		{
			throw new NotSupportedException("AddComponent(string) is deprecated");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("gameObject.PlayAnimation is not supported anymore. Use animation.Play()", true)]
		public void PlayAnimation(Object animation)
		{
			throw new NotSupportedException("gameObject.PlayAnimation is not supported anymore. Use animation.Play();");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("gameObject.StopAnimation is not supported anymore. Use animation.Stop()", true)]
		public void StopAnimation()
		{
			throw new NotSupportedException("gameObject.StopAnimation(); is not supported anymore. Use animation.Stop();");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_scene_Injected(out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CalculateBounds_Injected(out Bounds ret);
	}
}
