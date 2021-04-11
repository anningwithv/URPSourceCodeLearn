using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Graphics/LineUtility.bindings.h")]
	public sealed class LineUtility
	{
		public static void Simplify(List<Vector3> points, float tolerance, List<int> pointsToKeep)
		{
			bool flag = points == null;
			if (flag)
			{
				throw new ArgumentNullException("points");
			}
			bool flag2 = pointsToKeep == null;
			if (flag2)
			{
				throw new ArgumentNullException("pointsToKeep");
			}
			LineUtility.GeneratePointsToKeep3D(points, tolerance, pointsToKeep);
		}

		public static void Simplify(List<Vector3> points, float tolerance, List<Vector3> simplifiedPoints)
		{
			bool flag = points == null;
			if (flag)
			{
				throw new ArgumentNullException("points");
			}
			bool flag2 = simplifiedPoints == null;
			if (flag2)
			{
				throw new ArgumentNullException("simplifiedPoints");
			}
			LineUtility.GenerateSimplifiedPoints3D(points, tolerance, simplifiedPoints);
		}

		public static void Simplify(List<Vector2> points, float tolerance, List<int> pointsToKeep)
		{
			bool flag = points == null;
			if (flag)
			{
				throw new ArgumentNullException("points");
			}
			bool flag2 = pointsToKeep == null;
			if (flag2)
			{
				throw new ArgumentNullException("pointsToKeep");
			}
			LineUtility.GeneratePointsToKeep2D(points, tolerance, pointsToKeep);
		}

		public static void Simplify(List<Vector2> points, float tolerance, List<Vector2> simplifiedPoints)
		{
			bool flag = points == null;
			if (flag)
			{
				throw new ArgumentNullException("points");
			}
			bool flag2 = simplifiedPoints == null;
			if (flag2)
			{
				throw new ArgumentNullException("simplifiedPoints");
			}
			LineUtility.GenerateSimplifiedPoints2D(points, tolerance, simplifiedPoints);
		}

		[FreeFunction("LineUtility_Bindings::GeneratePointsToKeep3D", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GeneratePointsToKeep3D(object pointsList, float tolerance, object pointsToKeepList);

		[FreeFunction("LineUtility_Bindings::GeneratePointsToKeep2D", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GeneratePointsToKeep2D(object pointsList, float tolerance, object pointsToKeepList);

		[FreeFunction("LineUtility_Bindings::GenerateSimplifiedPoints3D", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GenerateSimplifiedPoints3D(object pointsList, float tolerance, object simplifiedPoints);

		[FreeFunction("LineUtility_Bindings::GenerateSimplifiedPoints2D", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GenerateSimplifiedPoints2D(object pointsList, float tolerance, object simplifiedPoints);
	}
}
