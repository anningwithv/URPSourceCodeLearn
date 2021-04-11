using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	internal class VectorImageRenderInfoPool : LinkedPool<VectorImageRenderInfo>
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly VectorImageRenderInfoPool.<>c <>9 = new VectorImageRenderInfoPool.<>c();

			public static Func<VectorImageRenderInfo> <>9__0_0;

			public static Action<VectorImageRenderInfo> <>9__0_1;

			internal VectorImageRenderInfo ctor>b__0_0()
			{
				return new VectorImageRenderInfo();
			}

			internal void ctor>b__0_1(VectorImageRenderInfo vectorImageInfo)
			{
				vectorImageInfo.Reset();
			}
		}

		public VectorImageRenderInfoPool()
		{
			Func<VectorImageRenderInfo> arg_44_1;
			if ((arg_44_1 = VectorImageRenderInfoPool.<>c.<>9__0_0) == null)
			{
				arg_44_1 = (VectorImageRenderInfoPool.<>c.<>9__0_0 = new Func<VectorImageRenderInfo>(VectorImageRenderInfoPool.<>c.<>9.<.ctor>b__0_0));
			}
			Action<VectorImageRenderInfo> arg_44_2;
			if ((arg_44_2 = VectorImageRenderInfoPool.<>c.<>9__0_1) == null)
			{
				arg_44_2 = (VectorImageRenderInfoPool.<>c.<>9__0_1 = new Action<VectorImageRenderInfo>(VectorImageRenderInfoPool.<>c.<>9.<.ctor>b__0_1));
			}
			base..ctor(arg_44_1, arg_44_2, 10000);
		}
	}
}
