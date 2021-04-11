using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	[NativeHeader("Modules/SpriteShape/Public/SpriteShapeUtility.h"), MovedFrom("UnityEngine.Experimental.U2D")]
	public class SpriteShapeUtility
	{
		[FreeFunction("SpriteShapeUtility::Generate"), NativeThrows]
		public static int[] Generate(Mesh mesh, SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners)
		{
			return SpriteShapeUtility.Generate_Injected(mesh, ref shapeParams, points, metaData, angleRange, sprites, corners);
		}

		[FreeFunction("SpriteShapeUtility::GenerateSpriteShape"), NativeThrows]
		public static void GenerateSpriteShape(SpriteShapeRenderer renderer, SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners)
		{
			SpriteShapeUtility.GenerateSpriteShape_Injected(renderer, ref shapeParams, points, metaData, angleRange, sprites, corners);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int[] Generate_Injected(Mesh mesh, ref SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateSpriteShape_Injected(SpriteShapeRenderer renderer, ref SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners);
	}
}
