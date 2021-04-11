using System;

namespace UnityEngine
{
	public class AndroidJavaClass : AndroidJavaObject
	{
		public AndroidJavaClass(string className)
		{
			this._AndroidJavaClass(className);
		}

		private void _AndroidJavaClass(string className)
		{
			base.DebugPrint("Creating AndroidJavaClass from " + className);
			IntPtr jobject = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(jobject);
			this.m_jobject = new GlobalJavaObjectRef(IntPtr.Zero);
		}

		internal AndroidJavaClass(IntPtr jclass)
		{
			bool flag = jclass == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			this.m_jclass = new GlobalJavaObjectRef(jclass);
			this.m_jobject = new GlobalJavaObjectRef(IntPtr.Zero);
		}
	}
}
