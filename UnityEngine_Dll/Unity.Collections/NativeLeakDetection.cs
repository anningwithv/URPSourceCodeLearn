using System;
using UnityEngine;

namespace Unity.Collections
{
	public static class NativeLeakDetection
	{
		private static int s_NativeLeakDetectionMode;

		private const string kNativeLeakDetectionModePrefsString = "Unity.Colletions.NativeLeakDetection.Mode";

		public static NativeLeakDetectionMode Mode
		{
			get
			{
				bool flag = NativeLeakDetection.s_NativeLeakDetectionMode == 0;
				if (flag)
				{
					NativeLeakDetection.Initialize();
				}
				return (NativeLeakDetectionMode)NativeLeakDetection.s_NativeLeakDetectionMode;
			}
			set
			{
				bool flag = NativeLeakDetection.s_NativeLeakDetectionMode != (int)value;
				if (flag)
				{
					NativeLeakDetection.s_NativeLeakDetectionMode = (int)value;
					PlayerPrefs.EditorPrefsSetInt("Unity.Colletions.NativeLeakDetection.Mode", (int)value);
				}
			}
		}

		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			NativeLeakDetection.s_NativeLeakDetectionMode = PlayerPrefs.EditorPrefsGetInt("Unity.Colletions.NativeLeakDetection.Mode", 2);
			bool flag = NativeLeakDetection.s_NativeLeakDetectionMode < 1 || NativeLeakDetection.s_NativeLeakDetectionMode > 3;
			if (flag)
			{
				NativeLeakDetection.s_NativeLeakDetectionMode = 2;
			}
		}
	}
}
