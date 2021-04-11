using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Scripting/Component.bindings.h"), NativeClass("Unity::Component"), RequiredByNativeCode]
	public class Component : Object
	{
		public extern Transform transform
		{
			[FreeFunction("GetTransform", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern GameObject gameObject
		{
			[FreeFunction("GetGameObject", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public string tag
		{
			get
			{
				return this.gameObject.tag;
			}
			set
			{
				this.gameObject.tag = value;
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

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			return this.gameObject.GetComponent(type);
		}

		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public bool TryGetComponent(Type type, out Component component)
		{
			return this.gameObject.TryGetComponent(type, out component);
		}

		[SecuritySafeCritical]
		public bool TryGetComponent<T>(out T component)
		{
			return this.gameObject.TryGetComponent<T>(out component);
		}

		[FreeFunction(HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponent(string type);

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentInChildren(t, includeInactive);
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t)
		{
			return this.GetComponentInChildren(t, false);
		}

		public T GetComponentInChildren<T>([UnityEngine.Internal.DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), false));
		}

		public Component[] GetComponentsInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren(t, includeInactive);
		}

		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type t)
		{
			return this.gameObject.GetComponentsInChildren(t, false);
		}

		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
		{
			this.gameObject.GetComponentsInChildren<T>(includeInactive, result);
		}

		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t)
		{
			return this.gameObject.GetComponentInParent(t);
		}

		public T GetComponentInParent<T>()
		{
			return (T)((object)this.GetComponentInParent(typeof(T)));
		}

		public Component[] GetComponentsInParent(Type t, [UnityEngine.Internal.DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent(t, includeInactive);
		}

		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type t)
		{
			return this.GetComponentsInParent(t, false);
		}

		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent<T>(includeInactive);
		}

		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.gameObject.GetComponentsInParent<T>(includeInactive, results);
		}

		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		public Component[] GetComponents(Type type)
		{
			return this.gameObject.GetComponents(type);
		}

		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetComponentsForListInternal(Type searchType, object resultList);

		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, results);
		}

		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsForListInternal(typeof(T), results);
		}

		public T[] GetComponents<T>()
		{
			return this.gameObject.GetComponents<T>();
		}

		public bool CompareTag(string tag)
		{
			return this.gameObject.CompareTag(tag);
		}

		[FreeFunction(HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Component GetCoupledComponent();

		[FreeFunction(HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsCoupledComponent();

		[FreeFunction(HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessageUpwards(string methodName, [UnityEngine.Internal.DefaultValue("null")] object value, [UnityEngine.Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			this.SendMessageUpwards(methodName, value, SendMessageOptions.RequireReceiver);
		}

		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			this.SendMessageUpwards(methodName, null, SendMessageOptions.RequireReceiver);
		}

		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		public void SendMessage(string methodName, object value)
		{
			this.SendMessage(methodName, value, SendMessageOptions.RequireReceiver);
		}

		public void SendMessage(string methodName)
		{
			this.SendMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		[FreeFunction("SendMessage", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessage(string methodName, object value, SendMessageOptions options);

		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		[FreeFunction("BroadcastMessage", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BroadcastMessage(string methodName, [UnityEngine.Internal.DefaultValue("null")] object parameter, [UnityEngine.Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			this.BroadcastMessage(methodName, parameter, SendMessageOptions.RequireReceiver);
		}

		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			this.BroadcastMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}
	}
}
