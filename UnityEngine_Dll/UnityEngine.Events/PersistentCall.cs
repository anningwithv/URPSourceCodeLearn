using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	[Serializable]
	internal class PersistentCall : ISerializationCallbackReceiver
	{
		[FormerlySerializedAs("instance"), SerializeField]
		private UnityEngine.Object m_Target;

		[SerializeField]
		private string m_TargetAssemblyTypeName;

		[FormerlySerializedAs("methodName"), SerializeField]
		private string m_MethodName;

		[FormerlySerializedAs("mode"), SerializeField]
		private PersistentListenerMode m_Mode = PersistentListenerMode.EventDefined;

		[FormerlySerializedAs("arguments"), SerializeField]
		private ArgumentCache m_Arguments = new ArgumentCache();

		[FormerlySerializedAs("m_Enabled"), FormerlySerializedAs("enabled"), SerializeField]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;

		public UnityEngine.Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		public string targetAssemblyTypeName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_TargetAssemblyTypeName) && this.m_Target != null;
				if (flag)
				{
					this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_Target.GetType().AssemblyQualifiedName);
				}
				return this.m_TargetAssemblyTypeName;
			}
		}

		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		public UnityEventCallState callState
		{
			get
			{
				return this.m_CallState;
			}
			set
			{
				this.m_CallState = value;
			}
		}

		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.targetAssemblyTypeName) && !string.IsNullOrEmpty(this.methodName);
		}

		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			bool flag = this.m_CallState == UnityEventCallState.RuntimeOnly && !Application.isPlaying;
			BaseInvokableCall result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_CallState == UnityEventCallState.Off || theEvent == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					MethodInfo methodInfo = theEvent.FindMethod(this);
					bool flag3 = methodInfo == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						UnityEngine.Object target = methodInfo.IsStatic ? null : this.target;
						switch (this.m_Mode)
						{
						case PersistentListenerMode.EventDefined:
							result = theEvent.GetDelegate(target, methodInfo);
							break;
						case PersistentListenerMode.Void:
							result = new InvokableCall(target, methodInfo);
							break;
						case PersistentListenerMode.Object:
							result = PersistentCall.GetObjectCall(target, methodInfo, this.m_Arguments);
							break;
						case PersistentListenerMode.Int:
							result = new CachedInvokableCall<int>(target, methodInfo, this.m_Arguments.intArgument);
							break;
						case PersistentListenerMode.Float:
							result = new CachedInvokableCall<float>(target, methodInfo, this.m_Arguments.floatArgument);
							break;
						case PersistentListenerMode.String:
							result = new CachedInvokableCall<string>(target, methodInfo, this.m_Arguments.stringArgument);
							break;
						case PersistentListenerMode.Bool:
							result = new CachedInvokableCall<bool>(target, methodInfo, this.m_Arguments.boolArgument);
							break;
						default:
							result = null;
							break;
						}
					}
				}
			}
			return result;
		}

		private static BaseInvokableCall GetObjectCall(UnityEngine.Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(UnityEngine.Object);
			bool flag = !string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = (Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(UnityEngine.Object));
			}
			Type typeFromHandle = typeof(CachedInvokableCall<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[]
			{
				type
			});
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(UnityEngine.Object),
				typeof(MethodInfo),
				type
			});
			UnityEngine.Object @object = arguments.unityObjectArgument;
			bool flag2 = @object != null && !type.IsAssignableFrom(@object.GetType());
			if (flag2)
			{
				@object = null;
			}
			return constructor.Invoke(new object[]
			{
				target,
				method,
				@object
			}) as BaseInvokableCall;
		}

		public void RegisterPersistentListener(UnityEngine.Object ttarget, Type targetType, string mmethodName)
		{
			this.m_Target = ttarget;
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(targetType.AssemblyQualifiedName);
			this.m_MethodName = mmethodName;
		}

		public void UnregisterPersistentListener()
		{
			this.m_MethodName = string.Empty;
			this.m_Target = null;
			this.m_TargetAssemblyTypeName = string.Empty;
		}

		public void OnBeforeSerialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		public void OnAfterDeserialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}
	}
}
