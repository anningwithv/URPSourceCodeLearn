using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	internal class DrawParams
	{
		internal static readonly Rect k_UnlimitedRect = new Rect(-100000f, -100000f, 200000f, 200000f);

		internal static readonly Rect k_FullNormalizedRect = new Rect(-1f, -1f, 2f, 2f);

		internal readonly Stack<ViewTransform> view = new Stack<ViewTransform>(8);

		internal readonly Stack<Rect> scissor = new Stack<Rect>(8);

		public void Reset()
		{
			this.view.Clear();
			this.view.Push(new ViewTransform
			{
				transform = Matrix4x4.identity,
				clipRect = UIRUtility.ToVector4(DrawParams.k_FullNormalizedRect)
			});
			this.scissor.Clear();
			this.scissor.Push(DrawParams.k_UnlimitedRect);
		}
	}
}
