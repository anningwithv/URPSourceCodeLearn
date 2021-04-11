using System;
using System.Runtime.InteropServices;

namespace UnityEngine.XR
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct XRMirrorViewBlitMode
	{
		public const int Default = 0;

		public const int LeftEye = -1;

		public const int RightEye = -2;

		public const int SideBySide = -3;

		public const int SideBySideOcclusionMesh = -4;

		public const int Distort = -5;

		public const int None = -6;
	}
}
