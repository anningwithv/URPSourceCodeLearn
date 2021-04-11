using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Editor/Src/Undo/PropertyDiffUndoRecorder.h"), NativeHeader("Editor/Src/Animation/AnimationModeSnapshot.h")]
	public struct DrivenRectTransformTracker
	{
		private List<RectTransform> m_Tracked;

		private static bool s_BlockUndo;

		internal static bool CanRecordModifications()
		{
			return !DrivenRectTransformTracker.IsInAnimationMode() && (DrivenRectTransformTracker.IsUndoingOrRedoing() || DrivenRectTransformTracker.HasUndoRecordObjects());
		}

		[FreeFunction("GetAnimationModeSnapshot().IsInAnimationMode")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInAnimationMode();

		[FreeFunction("GetPropertyDiffUndoRecorder().HasRecordings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasUndoRecordObjects();

		[FreeFunction("GetPropertyDiffUndoRecorder().IsUndoingOrRedoing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsUndoingOrRedoing();

		public static void StopRecordingUndo()
		{
			DrivenRectTransformTracker.s_BlockUndo = true;
		}

		public static void StartRecordingUndo()
		{
			DrivenRectTransformTracker.s_BlockUndo = false;
		}

		public void Add(Object driver, RectTransform rectTransform, DrivenTransformProperties drivenProperties)
		{
			bool flag = this.m_Tracked == null;
			if (flag)
			{
				this.m_Tracked = new List<RectTransform>();
			}
			bool flag2 = !Application.isPlaying && DrivenRectTransformTracker.CanRecordModifications() && !DrivenRectTransformTracker.s_BlockUndo;
			if (flag2)
			{
				RuntimeUndo.RecordObject(rectTransform, "Driving RectTransform");
			}
			rectTransform.drivenByObject = driver;
			rectTransform.drivenProperties |= drivenProperties;
			this.m_Tracked.Add(rectTransform);
		}

		[Obsolete("revertValues parameter is ignored. Please use Clear() instead.")]
		public void Clear(bool revertValues)
		{
			this.Clear();
		}

		public void Clear()
		{
			bool flag = this.m_Tracked != null;
			if (flag)
			{
				for (int i = 0; i < this.m_Tracked.Count; i++)
				{
					bool flag2 = this.m_Tracked[i] != null;
					if (flag2)
					{
						bool flag3 = !Application.isPlaying && DrivenRectTransformTracker.CanRecordModifications() && !DrivenRectTransformTracker.s_BlockUndo;
						if (flag3)
						{
							RuntimeUndo.RecordObject(this.m_Tracked[i], "Driving RectTransform");
						}
						this.m_Tracked[i].drivenByObject = null;
						this.m_Tracked[i].drivenProperties = DrivenTransformProperties.None;
					}
				}
				this.m_Tracked.Clear();
			}
		}
	}
}
