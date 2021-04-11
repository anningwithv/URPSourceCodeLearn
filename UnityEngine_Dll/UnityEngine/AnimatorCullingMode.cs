using System;
using System.ComponentModel;

namespace UnityEngine
{
	public enum AnimatorCullingMode
	{
		AlwaysAnimate,
		CullUpdateTransforms,
		CullCompletely,
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Enum member AnimatorCullingMode.BasedOnRenderers has been deprecated. Use AnimatorCullingMode.CullUpdateTransforms instead. (UnityUpgradable) -> CullUpdateTransforms", true)]
		BasedOnRenderers = 1
	}
}
