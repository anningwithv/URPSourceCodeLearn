using System;

namespace UnityEngine
{
	public enum ParticleSystemShapeType
	{
		Sphere,
		[Obsolete("SphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		SphereShell,
		Hemisphere,
		[Obsolete("HemisphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		HemisphereShell,
		Cone,
		Box,
		Mesh,
		[Obsolete("ConeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeShell,
		ConeVolume,
		[Obsolete("ConeVolumeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeVolumeShell,
		Circle,
		[Obsolete("CircleEdge is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		CircleEdge,
		SingleSidedEdge,
		MeshRenderer,
		SkinnedMeshRenderer,
		BoxShell,
		BoxEdge,
		Donut,
		Rectangle,
		Sprite,
		SpriteRenderer
	}
}
