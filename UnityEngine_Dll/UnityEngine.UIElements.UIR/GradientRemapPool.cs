using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	internal class GradientRemapPool : LinkedPool<GradientRemap>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly GradientRemapPool.<>c <>9 = new GradientRemapPool.<>c();

			public static Func<GradientRemap> <>9__0_0;

			public static Action<GradientRemap> <>9__0_1;

			internal GradientRemap ctor>b__0_0()
			{
				return new GradientRemap();
			}

			internal void ctor>b__0_1(GradientRemap gradientRemap)
			{
				gradientRemap.Reset();
			}
		}

		public GradientRemapPool()
		{
			Func<GradientRemap> arg_44_1;
			if ((arg_44_1 = GradientRemapPool.<>c.<>9__0_0) == null)
			{
				arg_44_1 = (GradientRemapPool.<>c.<>9__0_0 = new Func<GradientRemap>(GradientRemapPool.<>c.<>9.<.ctor>b__0_0));
			}
			Action<GradientRemap> arg_44_2;
			if ((arg_44_2 = GradientRemapPool.<>c.<>9__0_1) == null)
			{
				arg_44_2 = (GradientRemapPool.<>c.<>9__0_1 = new Action<GradientRemap>(GradientRemapPool.<>c.<>9.<.ctor>b__0_1));
			}
			base..ctor(arg_44_1, arg_44_2, 10000);
		}
	}
}
