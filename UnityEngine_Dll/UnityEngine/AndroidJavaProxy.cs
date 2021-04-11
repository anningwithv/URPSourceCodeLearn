using System;
using System.Reflection;

namespace UnityEngine
{
	public class AndroidJavaProxy
	{
		public readonly AndroidJavaClass javaInterface;

		internal IntPtr proxyObject = IntPtr.Zero;

		private static readonly GlobalJavaObjectRef s_JavaLangSystemClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("java/lang/System"));

		private static readonly IntPtr s_HashCodeMethodID = AndroidJNIHelper.GetMethodID(AndroidJavaProxy.s_JavaLangSystemClass, "identityHashCode", "(Ljava/lang/Object;)I", true);

		public AndroidJavaProxy(string javaInterface) : this(new AndroidJavaClass(javaInterface))
		{
		}

		public AndroidJavaProxy(AndroidJavaClass javaInterface)
		{
			this.javaInterface = javaInterface;
		}

		~AndroidJavaProxy()
		{
			AndroidJNISafe.DeleteWeakGlobalRef(this.proxyObject);
		}

		public virtual AndroidJavaObject Invoke(string methodName, object[] args)
		{
			Exception ex = null;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			Type[] array = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				array[i] = ((args[i] == null) ? typeof(AndroidJavaObject) : args[i].GetType());
			}
			AndroidJavaObject result;
			try
			{
				MethodInfo method = base.GetType().GetMethod(methodName, bindingAttr, null, array, null);
				bool flag = method != null;
				if (flag)
				{
					result = _AndroidJNIHelper.Box(method.Invoke(this, args));
					return result;
				}
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2.InnerException;
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			string[] array2 = new string[args.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = array[j].ToString();
			}
			bool flag2 = ex != null;
			if (flag2)
			{
				string[] expr_D2 = new string[6];
				int arg_E6_1 = 0;
				Type expr_DA = base.GetType();
				expr_D2[arg_E6_1] = ((expr_DA != null) ? expr_DA.ToString() : null);
				expr_D2[1] = ".";
				expr_D2[2] = methodName;
				expr_D2[3] = "(";
				expr_D2[4] = string.Join(",", array2);
				expr_D2[5] = ")";
				throw new TargetInvocationException(string.Concat(expr_D2), ex);
			}
			IntPtr arg_17B_0 = this.GetRawProxy();
			string[] expr_129 = new string[7];
			expr_129[0] = "No such proxy method: ";
			int arg_145_1 = 1;
			Type expr_139 = base.GetType();
			expr_129[arg_145_1] = ((expr_139 != null) ? expr_139.ToString() : null);
			expr_129[2] = ".";
			expr_129[3] = methodName;
			expr_129[4] = "(";
			expr_129[5] = string.Join(",", array2);
			expr_129[6] = ")";
			AndroidReflection.SetNativeExceptionOnProxy(arg_17B_0, new Exception(string.Concat(expr_129)), true);
			result = null;
			return result;
		}

		public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			object[] array = new object[javaArgs.Length];
			for (int i = 0; i < javaArgs.Length; i++)
			{
				array[i] = _AndroidJNIHelper.Unbox(javaArgs[i]);
				bool flag = !(array[i] is AndroidJavaObject);
				if (flag)
				{
					bool flag2 = javaArgs[i] != null;
					if (flag2)
					{
						javaArgs[i].Dispose();
					}
				}
			}
			return this.Invoke(methodName, array);
		}

		public virtual bool equals(AndroidJavaObject obj)
		{
			IntPtr obj2 = (obj == null) ? IntPtr.Zero : obj.GetRawObject();
			return AndroidJNI.IsSameObject(this.proxyObject, obj2);
		}

		public virtual int hashCode()
		{
			jvalue[] array = new jvalue[1];
			array[0].l = this.GetRawProxy();
			return AndroidJNISafe.CallStaticIntMethod(AndroidJavaProxy.s_JavaLangSystemClass, AndroidJavaProxy.s_HashCodeMethodID, array);
		}

		public virtual string toString()
		{
			return ((this != null) ? this.ToString() : null) + " <c# proxy java object>";
		}

		internal AndroidJavaObject GetProxyObject()
		{
			return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(this.GetRawProxy());
		}

		internal IntPtr GetRawProxy()
		{
			IntPtr intPtr = IntPtr.Zero;
			bool flag = this.proxyObject != IntPtr.Zero;
			if (flag)
			{
				intPtr = AndroidJNI.NewLocalRef(this.proxyObject);
				bool flag2 = intPtr == IntPtr.Zero;
				if (flag2)
				{
					AndroidJNI.DeleteWeakGlobalRef(this.proxyObject);
					this.proxyObject = IntPtr.Zero;
				}
			}
			bool flag3 = intPtr == IntPtr.Zero;
			if (flag3)
			{
				intPtr = AndroidJNIHelper.CreateJavaProxy(this);
				this.proxyObject = AndroidJNI.NewWeakGlobalRef(intPtr);
			}
			return intPtr;
		}
	}
}
