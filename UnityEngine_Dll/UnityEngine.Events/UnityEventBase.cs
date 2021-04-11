using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	[UsedByNativeCode]
	[Serializable]
	public abstract class UnityEventBase : ISerializationCallbackReceiver
	{
		private InvokableCallList m_Calls;

		[FormerlySerializedAs("m_PersistentListeners"), SerializeField]
		private PersistentCallGroup m_PersistentCalls;

		private bool m_CallsDirty = true;

		protected UnityEventBase()
		{
			this.m_Calls = new InvokableCallList();
			this.m_PersistentCalls = new PersistentCallGroup();
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.DirtyPersistentCalls();
		}

		protected MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return this.FindMethod_Impl(name, targetObj.GetType());
		}

		protected abstract MethodInfo FindMethod_Impl(string name, Type targetObjType);

		internal abstract BaseInvokableCall GetDelegate(object target, MethodInfo theFunction);

		internal MethodInfo FindMethod(PersistentCall call)
		{
			Type argumentType = typeof(UnityEngine.Object);
			bool flag = !string.IsNullOrEmpty(call.arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				argumentType = (Type.GetType(call.arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(UnityEngine.Object));
			}
			Type listenerType = (call.target != null) ? call.target.GetType() : Type.GetType(call.targetAssemblyTypeName, false);
			return this.FindMethod(call.methodName, listenerType, call.mode, argumentType);
		}

		internal MethodInfo FindMethod(string name, Type listenerType, PersistentListenerMode mode, Type argumentType)
		{
			MethodInfo result;
			switch (mode)
			{
			case PersistentListenerMode.EventDefined:
				result = this.FindMethod_Impl(name, listenerType);
				break;
			case PersistentListenerMode.Void:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[0]);
				break;
			case PersistentListenerMode.Object:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					argumentType ?? typeof(UnityEngine.Object)
				});
				break;
			case PersistentListenerMode.Int:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					typeof(int)
				});
				break;
			case PersistentListenerMode.Float:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					typeof(float)
				});
				break;
			case PersistentListenerMode.String:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					typeof(string)
				});
				break;
			case PersistentListenerMode.Bool:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					typeof(bool)
				});
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		public int GetPersistentEventCount()
		{
			return this.m_PersistentCalls.Count;
		}

		public UnityEngine.Object GetPersistentTarget(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener != null) ? listener.target : null;
		}

		public string GetPersistentMethodName(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener != null) ? listener.methodName : string.Empty;
		}

		private void DirtyPersistentCalls()
		{
			this.m_Calls.ClearPersistent();
			this.m_CallsDirty = true;
		}

		private void RebuildPersistentCallsIfNeeded()
		{
			bool callsDirty = this.m_CallsDirty;
			if (callsDirty)
			{
				this.m_PersistentCalls.Initialize(this.m_Calls, this);
				this.m_CallsDirty = false;
			}
		}

		public void SetPersistentListenerState(int index, UnityEventCallState state)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			bool flag = listener != null;
			if (flag)
			{
				listener.callState = state;
			}
			this.DirtyPersistentCalls();
		}

		protected void AddListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.AddListener(this.GetDelegate(targetObj, method));
		}

		internal void AddCall(BaseInvokableCall call)
		{
			this.m_Calls.AddListener(call);
		}

		protected void RemoveListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.RemoveListener(targetObj, method);
		}

		public void RemoveAllListeners()
		{
			this.m_Calls.Clear();
		}

		internal List<BaseInvokableCall> PrepareInvoke()
		{
			this.RebuildPersistentCallsIfNeeded();
			return this.m_Calls.PrepareInvoke();
		}

		protected void Invoke(object[] parameters)
		{
			List<BaseInvokableCall> list = this.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Invoke(parameters);
			}
		}

		public override string ToString()
		{
			return base.ToString() + " " + base.GetType().FullName;
		}

		public static MethodInfo GetValidMethodInfo(object obj, string functionName, Type[] argumentTypes)
		{
			return UnityEventBase.GetValidMethodInfo(obj.GetType(), functionName, argumentTypes);
		}

		public static MethodInfo GetValidMethodInfo(Type objectType, string functionName, Type[] argumentTypes)
		{
			MethodInfo result;
			while (objectType != typeof(object) && objectType != null)
			{
				MethodInfo method = objectType.GetMethod(functionName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, argumentTypes, null);
				bool flag = method != null;
				if (flag)
				{
					ParameterInfo[] parameters = method.GetParameters();
					bool flag2 = true;
					int num = 0;
					ParameterInfo[] array = parameters;
					for (int i = 0; i < array.Length; i++)
					{
						ParameterInfo parameterInfo = array[i];
						Type type = argumentTypes[num];
						Type parameterType = parameterInfo.ParameterType;
						flag2 = (type.IsPrimitive == parameterType.IsPrimitive);
						bool flag3 = !flag2;
						if (flag3)
						{
							break;
						}
						num++;
					}
					bool flag4 = flag2;
					if (flag4)
					{
						result = method;
						return result;
					}
				}
				objectType = objectType.BaseType;
			}
			result = null;
			return result;
		}

		protected bool ValidateRegistration(MethodInfo method, object targetObj, PersistentListenerMode mode)
		{
			return this.ValidateRegistration(method, targetObj, mode, typeof(UnityEngine.Object));
		}

		protected bool ValidateRegistration(MethodInfo method, object targetObj, PersistentListenerMode mode, Type argumentType)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method", UnityString.Format("Can not register null method on {0} for callback!", new object[]
				{
					targetObj
				}));
			}
			bool flag2 = method.DeclaringType == null;
			if (flag2)
			{
				throw new NullReferenceException(UnityString.Format("Method '{0}' declaring type is null, global methods are not supported", new object[]
				{
					method.Name
				}));
			}
			bool flag3 = !method.IsStatic;
			Type type;
			if (flag3)
			{
				UnityEngine.Object @object = targetObj as UnityEngine.Object;
				bool flag4 = @object == null || @object.GetInstanceID() == 0;
				if (flag4)
				{
					throw new ArgumentException(UnityString.Format("Could not register callback {0} on {1}. The class {2} does not derive from UnityEngine.Object", new object[]
					{
						method.Name,
						targetObj,
						(targetObj == null) ? "null" : targetObj.GetType().ToString()
					}));
				}
				type = @object.GetType();
				bool flag5 = !method.DeclaringType.IsAssignableFrom(type);
				if (flag5)
				{
					throw new ArgumentException(UnityString.Format("Method '{0}' declaring type '{1}' is not assignable from object type '{2}'", new object[]
					{
						method.Name,
						method.DeclaringType.Name,
						@object.GetType().Name
					}));
				}
			}
			else
			{
				type = method.DeclaringType;
			}
			bool flag6 = this.FindMethod(method.Name, type, mode, argumentType) == null;
			bool result;
			if (flag6)
			{
				Debug.LogWarning(UnityString.Format("Could not register listener {0}.{1} on {2} the method could not be found.", new object[]
				{
					targetObj,
					method,
					base.GetType()
				}));
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		internal void AddPersistentListener()
		{
			this.m_PersistentCalls.AddListener();
		}

		protected void RegisterPersistentListener(int index, object targetObj, MethodInfo method)
		{
			this.RegisterPersistentListener(index, targetObj, targetObj.GetType(), method);
		}

		protected void RegisterPersistentListener(int index, object targetObj, Type targetObjType, MethodInfo method)
		{
			bool flag = !this.ValidateRegistration(method, targetObj, PersistentListenerMode.EventDefined);
			if (!flag)
			{
				this.m_PersistentCalls.RegisterEventPersistentListener(index, targetObj as UnityEngine.Object, targetObjType, method.Name);
				this.DirtyPersistentCalls();
			}
		}

		internal void RemovePersistentListener(UnityEngine.Object target, MethodInfo method)
		{
			bool flag = method == null;
			if (!flag)
			{
				this.m_PersistentCalls.RemoveListeners(target, method.Name);
				this.DirtyPersistentCalls();
			}
		}

		internal void RemovePersistentListener(int index)
		{
			this.m_PersistentCalls.RemoveListener(index);
			this.DirtyPersistentCalls();
		}

		internal void UnregisterPersistentListener(int index)
		{
			this.m_PersistentCalls.UnregisterPersistentListener(index);
			this.DirtyPersistentCalls();
		}

		internal void AddVoidPersistentListener(UnityAction call)
		{
			int persistentEventCount = this.GetPersistentEventCount();
			this.AddPersistentListener();
			this.RegisterVoidPersistentListener(persistentEventCount, call);
		}

		internal void RegisterVoidPersistentListener(int index, UnityAction call)
		{
			bool flag = call == null;
			if (flag)
			{
				Debug.LogWarning("Registering a Listener requires an action");
			}
			else
			{
				bool flag2 = !this.ValidateRegistration(call.Method, call.Target, PersistentListenerMode.Void);
				if (!flag2)
				{
					this.m_PersistentCalls.RegisterVoidPersistentListener(index, call.Target as UnityEngine.Object, call.Method.DeclaringType, call.Method.Name);
					this.DirtyPersistentCalls();
				}
			}
		}

		internal void RegisterVoidPersistentListenerWithoutValidation(int index, UnityEngine.Object target, string methodName)
		{
			this.RegisterVoidPersistentListenerWithoutValidation(index, target, target.GetType(), methodName);
		}

		internal void RegisterVoidPersistentListenerWithoutValidation(int index, UnityEngine.Object target, Type targetType, string methodName)
		{
			this.m_PersistentCalls.RegisterVoidPersistentListener(index, target, targetType, methodName);
			this.DirtyPersistentCalls();
		}

		internal void AddIntPersistentListener(UnityAction<int> call, int argument)
		{
			int persistentEventCount = this.GetPersistentEventCount();
			this.AddPersistentListener();
			this.RegisterIntPersistentListener(persistentEventCount, call, argument);
		}

		internal void RegisterIntPersistentListener(int index, UnityAction<int> call, int argument)
		{
			bool flag = call == null;
			if (flag)
			{
				Debug.LogWarning("Registering a Listener requires an action");
			}
			else
			{
				bool flag2 = !this.ValidateRegistration(call.Method, call.Target, PersistentListenerMode.Int);
				if (!flag2)
				{
					this.m_PersistentCalls.RegisterIntPersistentListener(index, call.Target as UnityEngine.Object, call.Method.DeclaringType, argument, call.Method.Name);
					this.DirtyPersistentCalls();
				}
			}
		}

		internal void AddFloatPersistentListener(UnityAction<float> call, float argument)
		{
			int persistentEventCount = this.GetPersistentEventCount();
			this.AddPersistentListener();
			this.RegisterFloatPersistentListener(persistentEventCount, call, argument);
		}

		internal void RegisterFloatPersistentListener(int index, UnityAction<float> call, float argument)
		{
			bool flag = call == null;
			if (flag)
			{
				Debug.LogWarning("Registering a Listener requires an action");
			}
			else
			{
				bool flag2 = !this.ValidateRegistration(call.Method, call.Target, PersistentListenerMode.Float);
				if (!flag2)
				{
					this.m_PersistentCalls.RegisterFloatPersistentListener(index, call.Target as UnityEngine.Object, call.Method.DeclaringType, argument, call.Method.Name);
					this.DirtyPersistentCalls();
				}
			}
		}

		internal void AddBoolPersistentListener(UnityAction<bool> call, bool argument)
		{
			int persistentEventCount = this.GetPersistentEventCount();
			this.AddPersistentListener();
			this.RegisterBoolPersistentListener(persistentEventCount, call, argument);
		}

		internal void RegisterBoolPersistentListener(int index, UnityAction<bool> call, bool argument)
		{
			bool flag = call == null;
			if (flag)
			{
				Debug.LogWarning("Registering a Listener requires an action");
			}
			else
			{
				bool flag2 = !this.ValidateRegistration(call.Method, call.Target, PersistentListenerMode.Bool);
				if (!flag2)
				{
					this.m_PersistentCalls.RegisterBoolPersistentListener(index, call.Target as UnityEngine.Object, call.Method.DeclaringType, argument, call.Method.Name);
					this.DirtyPersistentCalls();
				}
			}
		}

		internal void AddStringPersistentListener(UnityAction<string> call, string argument)
		{
			int persistentEventCount = this.GetPersistentEventCount();
			this.AddPersistentListener();
			this.RegisterStringPersistentListener(persistentEventCount, call, argument);
		}

		internal void RegisterStringPersistentListener(int index, UnityAction<string> call, string argument)
		{
			bool flag = call == null;
			if (flag)
			{
				Debug.LogWarning("Registering a Listener requires an action");
			}
			else
			{
				bool flag2 = !this.ValidateRegistration(call.Method, call.Target, PersistentListenerMode.String);
				if (!flag2)
				{
					this.m_PersistentCalls.RegisterStringPersistentListener(index, call.Target as UnityEngine.Object, call.Method.DeclaringType, argument, call.Method.Name);
					this.DirtyPersistentCalls();
				}
			}
		}

		internal void AddObjectPersistentListener<T>(UnityAction<T> call, T argument) where T : UnityEngine.Object
		{
			int persistentEventCount = this.GetPersistentEventCount();
			this.AddPersistentListener();
			this.RegisterObjectPersistentListener<T>(persistentEventCount, call, argument);
		}

		internal void RegisterObjectPersistentListener<T>(int index, UnityAction<T> call, T argument) where T : UnityEngine.Object
		{
			bool flag = call == null;
			if (flag)
			{
				throw new ArgumentNullException("call", "Registering a Listener requires a non null call");
			}
			bool flag2 = !this.ValidateRegistration(call.Method, call.Target, PersistentListenerMode.Object, (argument == null) ? typeof(UnityEngine.Object) : argument.GetType());
			if (!flag2)
			{
				this.m_PersistentCalls.RegisterObjectPersistentListener(index, call.Target as UnityEngine.Object, call.Method.DeclaringType, argument, call.Method.Name);
				this.DirtyPersistentCalls();
			}
		}
	}
}
