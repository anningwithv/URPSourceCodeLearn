using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal sealed class UnitySynchronizationContext : SynchronizationContext
	{
		private struct WorkRequest
		{
			private readonly SendOrPostCallback m_DelagateCallback;

			private readonly object m_DelagateState;

			private readonly ManualResetEvent m_WaitHandle;

			public WorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
			{
				this.m_DelagateCallback = callback;
				this.m_DelagateState = state;
				this.m_WaitHandle = waitHandle;
			}

			public void Invoke()
			{
				try
				{
					this.m_DelagateCallback(this.m_DelagateState);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
				bool flag = this.m_WaitHandle != null;
				if (flag)
				{
					this.m_WaitHandle.Set();
				}
			}
		}

		private const int kAwqInitialCapacity = 20;

		private readonly List<UnitySynchronizationContext.WorkRequest> m_AsyncWorkQueue;

		private readonly List<UnitySynchronizationContext.WorkRequest> m_CurrentFrameWork = new List<UnitySynchronizationContext.WorkRequest>(20);

		private readonly int m_MainThreadID;

		private int m_TrackedCount = 0;

		private UnitySynchronizationContext(int mainThreadID)
		{
			this.m_AsyncWorkQueue = new List<UnitySynchronizationContext.WorkRequest>(20);
			this.m_MainThreadID = mainThreadID;
		}

		private UnitySynchronizationContext(List<UnitySynchronizationContext.WorkRequest> queue, int mainThreadID)
		{
			this.m_AsyncWorkQueue = queue;
			this.m_MainThreadID = mainThreadID;
		}

		public override void Send(SendOrPostCallback callback, object state)
		{
			bool flag = this.m_MainThreadID == Thread.CurrentThread.ManagedThreadId;
			if (flag)
			{
				callback(state);
			}
			else
			{
				using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
				{
					List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
					lock (asyncWorkQueue)
					{
						this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, manualResetEvent));
					}
					manualResetEvent.WaitOne();
				}
			}
		}

		public override void OperationStarted()
		{
			Interlocked.Increment(ref this.m_TrackedCount);
		}

		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref this.m_TrackedCount);
		}

		public override void Post(SendOrPostCallback callback, object state)
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, null));
			}
		}

		public override SynchronizationContext CreateCopy()
		{
			return new UnitySynchronizationContext(this.m_AsyncWorkQueue, this.m_MainThreadID);
		}

		private void Exec()
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_CurrentFrameWork.AddRange(this.m_AsyncWorkQueue);
				this.m_AsyncWorkQueue.Clear();
			}
			while (this.m_CurrentFrameWork.Count > 0)
			{
				UnitySynchronizationContext.WorkRequest item = this.m_CurrentFrameWork[0];
				this.m_CurrentFrameWork.Remove(item);
				item.Invoke();
			}
		}

		private bool HasPendingTasks()
		{
			return this.m_AsyncWorkQueue.Count != 0 || this.m_TrackedCount != 0;
		}

		[RequiredByNativeCode]
		private static void InitializeSynchronizationContext()
		{
			SynchronizationContext.SetSynchronizationContext(new UnitySynchronizationContext(Thread.CurrentThread.ManagedThreadId));
		}

		[RequiredByNativeCode]
		private static void ExecuteTasks()
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext != null;
			if (flag)
			{
				unitySynchronizationContext.Exec();
			}
		}

		[RequiredByNativeCode]
		private static bool ExecutePendingTasks(long millisecondsTimeout)
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				while (unitySynchronizationContext.HasPendingTasks())
				{
					bool flag2 = stopwatch.ElapsedMilliseconds > millisecondsTimeout;
					if (flag2)
					{
						break;
					}
					unitySynchronizationContext.Exec();
					Thread.Sleep(1);
				}
				result = !unitySynchronizationContext.HasPendingTasks();
			}
			return result;
		}
	}
}
