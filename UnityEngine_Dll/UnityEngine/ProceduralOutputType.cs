using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	public enum ProceduralOutputType
	{
		Unknown,
		Diffuse,
		Normal,
		Height,
		Emissive,
		Specular,
		Opacity,
		Smoothness,
		AmbientOcclusion,
		DetailMask,
		Metallic,
		Roughness
	}
}
