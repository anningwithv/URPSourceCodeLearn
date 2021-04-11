using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Editor/Src/Undo/Undo.h")]
	internal class RuntimeUndo
	{
		internal void SetTransformParentUndo(Transform transform, Transform newParent, string name)
		{
			RuntimeUndo.SetTransformParent(transform, newParent, true, name);
		}

		[FreeFunction("SetTransformParentUndo")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTransformParent([NotNull("NullExceptionObject")] Transform transform, Transform newParent, bool worldPositionStays, string name);

		[FreeFunction("RecordUndoDiff")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RecordObject(Object objectToUndo, string name);

		[FreeFunction("RecordUndoDiff")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void RecordObjects(Object[] objectsToUndo, string name);
	}
}
