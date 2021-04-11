using System;

namespace UnityEngine.Yoga
{
	internal class YogaConfig
	{
		internal static readonly YogaConfig Default = new YogaConfig(Native.YGConfigGetDefault());

		private IntPtr _ygConfig;

		private Logger _logger;

		internal IntPtr Handle
		{
			get
			{
				return this._ygConfig;
			}
		}

		public Logger Logger
		{
			get
			{
				return this._logger;
			}
			set
			{
				this._logger = value;
			}
		}

		public bool UseWebDefaults
		{
			get
			{
				return Native.YGConfigGetUseWebDefaults(this._ygConfig);
			}
			set
			{
				Native.YGConfigSetUseWebDefaults(this._ygConfig, value);
			}
		}

		public float PointScaleFactor
		{
			get
			{
				return Native.YGConfigGetPointScaleFactor(this._ygConfig);
			}
			set
			{
				Native.YGConfigSetPointScaleFactor(this._ygConfig, value);
			}
		}

		private YogaConfig(IntPtr ygConfig)
		{
			this._ygConfig = ygConfig;
			bool flag = this._ygConfig == IntPtr.Zero;
			if (flag)
			{
				throw new InvalidOperationException("Failed to allocate native memory");
			}
		}

		public YogaConfig() : this(Native.YGConfigNew())
		{
		}

		protected override void Finalize()
		{
			try
			{
				bool flag = this.Handle != YogaConfig.Default.Handle;
				if (flag)
				{
					Native.YGConfigFree(this.Handle);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		public void SetExperimentalFeatureEnabled(YogaExperimentalFeature feature, bool enabled)
		{
			Native.YGConfigSetExperimentalFeatureEnabled(this._ygConfig, feature, enabled);
		}

		public bool IsExperimentalFeatureEnabled(YogaExperimentalFeature feature)
		{
			return Native.YGConfigIsExperimentalFeatureEnabled(this._ygConfig, feature);
		}

		public static int GetInstanceCount()
		{
			return Native.YGConfigGetInstanceCount();
		}

		public static void SetDefaultLogger(Logger logger)
		{
			YogaConfig.Default.Logger = logger;
		}
	}
}
