using System;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	[RequiredByNativeCode]
	[Serializable]
	public abstract class VFXSpawnerCallbacks : ScriptableObject
	{
		public abstract void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		public abstract void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		public abstract void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);
	}
}
