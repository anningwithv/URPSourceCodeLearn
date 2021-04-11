using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Mono/MonoBehaviour.h"), NativeHeader("Runtime/Scripting/DelayedCallUtility.h"), ExtensionOfNativeClass, RequiredByNativeCode]
	public class MonoBehaviour : Behaviour
	{
		public extern bool useGUILayout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool runInEditMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern bool allowPrefabModeInPlayMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public MonoBehaviour()
		{
			MonoBehaviour.ConstructorCheck(this);
		}

		public bool IsInvoking()
		{
			return MonoBehaviour.Internal_IsInvokingAll(this);
		}

		public void CancelInvoke()
		{
			MonoBehaviour.Internal_CancelInvokeAll(this);
		}

		public void Invoke(string methodName, float time)
		{
			MonoBehaviour.InvokeDelayed(this, methodName, time, 0f);
		}

		public void InvokeRepeating(string methodName, float time, float repeatRate)
		{
			bool flag = repeatRate <= 1E-05f && repeatRate != 0f;
			if (flag)
			{
				throw new UnityException("Invoke repeat rate has to be larger than 0.00001F)");
			}
			MonoBehaviour.InvokeDelayed(this, methodName, time, repeatRate);
		}

		public void CancelInvoke(string methodName)
		{
			MonoBehaviour.CancelInvoke(this, methodName);
		}

		public bool IsInvoking(string methodName)
		{
			return MonoBehaviour.IsInvoking(this, methodName);
		}

		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object value = null;
			return this.StartCoroutine(methodName, value);
		}

		public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
		{
			bool flag = string.IsNullOrEmpty(methodName);
			if (flag)
			{
				throw new NullReferenceException("methodName is null or empty");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged(methodName, value);
		}

		public Coroutine StartCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged2(routine);
		}

		[Obsolete("StartCoroutine_Auto has been deprecated. Use StartCoroutine instead (UnityUpgradable) -> StartCoroutine([mscorlib] System.Collections.IEnumerator)", false)]
		public Coroutine StartCoroutine_Auto(IEnumerator routine)
		{
			return this.StartCoroutine(routine);
		}

		public void StopCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineFromEnumeratorManaged(routine);
		}

		public void StopCoroutine(Coroutine routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineManaged(routine);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopCoroutine(string methodName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopAllCoroutines();

		public static void print(object message)
		{
			Debug.Log(message);
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConstructorCheck([Writable] Object self);

		[FreeFunction("CancelInvoke")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CancelInvokeAll([NotNull("NullExceptionObject")] MonoBehaviour self);

		[FreeFunction("IsInvoking")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsInvokingAll([NotNull("NullExceptionObject")] MonoBehaviour self);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvokeDelayed([NotNull("NullExceptionObject")] MonoBehaviour self, string methodName, float time, float repeatRate);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelInvoke([NotNull("NullExceptionObject")] MonoBehaviour self, string methodName);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInvoking([NotNull("NullExceptionObject")] MonoBehaviour self, string methodName);

		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsObjectMonoBehaviour([NotNull("NullExceptionObject")] Object obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Coroutine StartCoroutineManaged(string methodName, object value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Coroutine StartCoroutineManaged2(IEnumerator enumerator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StopCoroutineManaged(Coroutine routine);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StopCoroutineFromEnumeratorManaged(IEnumerator routine);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetScriptClassName();
	}
}
