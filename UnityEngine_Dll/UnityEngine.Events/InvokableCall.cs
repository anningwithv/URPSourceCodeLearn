using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Events
{
	internal class InvokableCall : BaseInvokableCall
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		private event UnityAction Delegate;

		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate += (UnityAction)System.Delegate.CreateDelegate(typeof(UnityAction), target, theFunction);
		}

		public InvokableCall(UnityAction action)
		{
			this.Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		public void Invoke()
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		protected event UnityAction<T1> Delegate;

		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate += (UnityAction<T1>)System.Delegate.CreateDelegate(typeof(UnityAction<T1>), target, theFunction);
		}

		public InvokableCall(UnityAction<T1> action)
		{
			this.Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 1;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		public virtual void Invoke(T1 args0)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0);
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		protected event UnityAction<T1, T2> Delegate;

		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2>)System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2>), target, theFunction);
		}

		public InvokableCall(UnityAction<T1, T2> action)
		{
			this.Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]));
			}
		}

		public void Invoke(T1 args0, T2 args1)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1);
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		protected event UnityAction<T1, T2, T3> Delegate;

		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3>), target, theFunction);
		}

		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			this.Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 3;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			}
		}

		public void Invoke(T1 args0, T2 args1, T3 args2)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2);
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		protected event UnityAction<T1, T2, T3, T4> Delegate;

		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3, T4>), target, theFunction);
		}

		public InvokableCall(UnityAction<T1, T2, T3, T4> action)
		{
			this.Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 4;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			}
		}

		public void Invoke(T1 args0, T2 args1, T3 args2, T4 args3)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2, args3);
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
