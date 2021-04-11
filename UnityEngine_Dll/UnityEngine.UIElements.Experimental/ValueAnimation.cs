using System;

namespace UnityEngine.UIElements.Experimental
{
	public sealed class ValueAnimation<T> : IValueAnimationUpdate, IValueAnimation
	{
		private const int k_DefaultDurationMs = 400;

		private const int k_DefaultMaxPoolSize = 100;

		private long m_StartTimeMs;

		private int m_DurationMs;

		private static ObjectPool<ValueAnimation<T>> sObjectPool = new ObjectPool<ValueAnimation<T>>(100);

		private T _from;

		private bool fromValueSet = false;

		public int durationMs
		{
			get
			{
				return this.m_DurationMs;
			}
			set
			{
				bool flag = value < 1;
				if (flag)
				{
					value = 1;
				}
				this.m_DurationMs = value;
			}
		}

		public Func<float, float> easingCurve
		{
			get;
			set;
		}

		public bool isRunning
		{
			get;
			private set;
		}

		public Action onAnimationCompleted
		{
			get;
			set;
		}

		public bool autoRecycle
		{
			get;
			set;
		}

		private bool recycled
		{
			get;
			set;
		}

		private VisualElement owner
		{
			get;
			set;
		}

		public Action<VisualElement, T> valueUpdated
		{
			get;
			set;
		}

		public Func<VisualElement, T> initialValue
		{
			get;
			set;
		}

		public Func<T, T, float, T> interpolator
		{
			get;
			set;
		}

		public T from
		{
			get
			{
				bool flag = !this.fromValueSet;
				if (flag)
				{
					bool flag2 = this.initialValue != null;
					if (flag2)
					{
						this.from = this.initialValue(this.owner);
					}
				}
				return this._from;
			}
			set
			{
				this.fromValueSet = true;
				this._from = value;
			}
		}

		public T to
		{
			get;
			set;
		}

		public ValueAnimation()
		{
			this.SetDefaultValues();
		}

		public void Start()
		{
			this.CheckNotRecycled();
			bool flag = this.owner != null;
			if (flag)
			{
				this.m_StartTimeMs = Panel.TimeSinceStartupMs();
				this.Register();
				this.isRunning = true;
			}
		}

		public void Stop()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
				this.isRunning = false;
				Action expr_28 = this.onAnimationCompleted;
				if (expr_28 != null)
				{
					expr_28();
				}
				bool autoRecycle = this.autoRecycle;
				if (autoRecycle)
				{
					bool flag = !this.recycled;
					if (flag)
					{
						this.Recycle();
					}
				}
			}
		}

		public void Recycle()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				bool flag = !this.autoRecycle;
				if (!flag)
				{
					this.Stop();
					return;
				}
				this.Stop();
			}
			this.SetDefaultValues();
			this.recycled = true;
			ValueAnimation<T>.sObjectPool.Release(this);
		}

		void IValueAnimationUpdate.Tick(long currentTimeMs)
		{
			this.CheckNotRecycled();
			long num = currentTimeMs - this.m_StartTimeMs;
			float num2 = (float)num / (float)this.durationMs;
			bool flag = false;
			bool flag2 = num2 >= 1f;
			if (flag2)
			{
				num2 = 1f;
				flag = true;
			}
			Func<float, float> expr_3D = this.easingCurve;
			num2 = ((expr_3D != null) ? expr_3D(num2) : num2);
			bool flag3 = this.interpolator != null;
			if (flag3)
			{
				T arg = this.interpolator(this.from, this.to, num2);
				Action<VisualElement, T> expr_7B = this.valueUpdated;
				if (expr_7B != null)
				{
					expr_7B(this.owner, arg);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				this.Stop();
			}
		}

		private void SetDefaultValues()
		{
			this.m_DurationMs = 400;
			this.autoRecycle = true;
			this.owner = null;
			this.m_StartTimeMs = 0L;
			this.onAnimationCompleted = null;
			this.valueUpdated = null;
			this.initialValue = null;
			this.interpolator = null;
			this.to = default(T);
			this.from = default(T);
			this.fromValueSet = false;
			this.easingCurve = new Func<float, float>(Easing.OutQuad);
		}

		private void Unregister()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.UnregisterAnimation(this);
			}
		}

		private void Register()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.RegisterAnimation(this);
			}
		}

		internal void SetOwner(VisualElement e)
		{
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
			}
			this.owner = e;
			bool isRunning2 = this.isRunning;
			if (isRunning2)
			{
				this.Register();
			}
		}

		private void CheckNotRecycled()
		{
			bool recycled = this.recycled;
			if (recycled)
			{
				throw new InvalidOperationException("Animation object has been recycled. Use KeepAlive() to keep a reference to an animation after it has been stopped.");
			}
		}

		public static ValueAnimation<T> Create(VisualElement e, Func<T, T, float, T> interpolator)
		{
			ValueAnimation<T> valueAnimation = ValueAnimation<T>.sObjectPool.Get();
			valueAnimation.recycled = false;
			valueAnimation.SetOwner(e);
			valueAnimation.interpolator = interpolator;
			return valueAnimation;
		}

		public ValueAnimation<T> Ease(Func<float, float> easing)
		{
			this.easingCurve = easing;
			return this;
		}

		public ValueAnimation<T> OnCompleted(Action callback)
		{
			this.onAnimationCompleted = callback;
			return this;
		}

		public ValueAnimation<T> KeepAlive()
		{
			this.autoRecycle = false;
			return this;
		}
	}
}
