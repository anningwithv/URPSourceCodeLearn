using System;
using System.Reflection;

namespace UnityEngine.Events
{
	internal abstract class BaseInvokableCall
	{
		protected BaseInvokableCall()
		{
		}

		protected BaseInvokableCall(object target, MethodInfo function)
		{
			bool isStatic = function.IsStatic;
			if (isStatic)
			{
				bool flag = target != null;
				if (flag)
				{
					throw new ArgumentException("target must be null");
				}
			}
			else
			{
				bool flag2 = target == null;
				if (flag2)
				{
					throw new ArgumentNullException("target");
				}
			}
			bool flag3 = function == null;
			if (flag3)
			{
				throw new ArgumentNullException("function");
			}
		}

		public abstract void Invoke(object[] args);

		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			bool flag = arg != null && !(arg is T);
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		protected static bool AllowInvoke(Delegate @delegate)
		{
			object target = @delegate.Target;
			bool flag = target == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				UnityEngine.Object @object = target as UnityEngine.Object;
				bool flag2 = @object != null;
				result = (!flag2 || @object != null);
			}
			return result;
		}

		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
