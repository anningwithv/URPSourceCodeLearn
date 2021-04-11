using System;

namespace UnityEngine.VFX
{
	internal enum VFXTaskType
	{
		None,
		Spawner = 268435456,
		Initialize = 536870912,
		Update = 805306368,
		Output = 1073741824,
		CameraSort = 805306369,
		PerCameraUpdate,
		PerCameraSort,
		ParticlePointOutput = 1073741824,
		ParticleLineOutput,
		ParticleQuadOutput,
		ParticleHexahedronOutput,
		ParticleMeshOutput,
		ParticleTriangleOutput,
		ParticleOctagonOutput,
		ConstantRateSpawner = 268435456,
		BurstSpawner,
		PeriodicBurstSpawner,
		VariableRateSpawner,
		CustomCallbackSpawner,
		SetAttributeSpawner,
		EvaluateExpressionsSpawner
	}
}
