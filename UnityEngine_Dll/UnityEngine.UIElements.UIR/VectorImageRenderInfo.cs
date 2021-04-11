using System;

namespace UnityEngine.UIElements.UIR
{
	internal class VectorImageRenderInfo : LinkedPoolItem<VectorImageRenderInfo>
	{
		public int useCount;

		public GradientRemap firstGradientRemap;

		public Alloc gradientSettingsAlloc;

		public void Reset()
		{
			this.useCount = 0;
			this.firstGradientRemap = null;
			this.gradientSettingsAlloc = default(Alloc);
		}
	}
}
