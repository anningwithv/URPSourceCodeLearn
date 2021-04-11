using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/SpriteUtility.h"), NativeHeader("Runtime/2D/Common/SpriteDataAccess.h"), NativeHeader("Runtime/2D/Common/ScriptBindings/SpritesMarshalling.h"), NativeType("Runtime/Graphics/SpriteFrame.h"), ExcludeFromPreset]
	public sealed class Sprite : Object
	{
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		public Rect rect
		{
			get
			{
				Rect result;
				this.get_rect_Injected(out result);
				return result;
			}
		}

		public Vector4 border
		{
			get
			{
				Vector4 result;
				this.get_border_Injected(out result);
				return result;
			}
		}

		public extern Texture2D texture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float pixelsPerUnit
		{
			[NativeMethod("GetPixelsToUnits")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float spriteAtlasTextureScale
		{
			[NativeMethod("GetSpriteAtlasTextureScale")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Texture2D associatedAlphaSplitTexture
		{
			[NativeMethod("GetAlphaTexture")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector2 pivot
		{
			[NativeMethod("GetPivotInPixels")]
			get
			{
				Vector2 result;
				this.get_pivot_Injected(out result);
				return result;
			}
		}

		public bool packed
		{
			get
			{
				return this.GetPacked() == 1;
			}
		}

		public SpritePackingMode packingMode
		{
			get
			{
				return (SpritePackingMode)this.GetPackingMode();
			}
		}

		public SpritePackingRotation packingRotation
		{
			get
			{
				return (SpritePackingRotation)this.GetPackingRotation();
			}
		}

		public Rect textureRect
		{
			get
			{
				bool flag = this.packed && this.packingMode != SpritePackingMode.Rectangle;
				Rect result;
				if (flag)
				{
					result = Rect.zero;
				}
				else
				{
					result = this.GetTextureRect();
				}
				return result;
			}
		}

		public Vector2 textureRectOffset
		{
			get
			{
				bool flag = this.packed && this.packingMode != SpritePackingMode.Rectangle;
				Vector2 result;
				if (flag)
				{
					result = Vector2.zero;
				}
				else
				{
					result = this.GetTextureRectOffset();
				}
				return result;
			}
		}

		public extern Vector2[] vertices
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteVertices", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ushort[] triangles
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteIndices", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Vector2[] uv
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteUVs", HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[RequiredByNativeCode]
		private Sprite()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetPackingMode();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetPackingRotation();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetPacked();

		internal Rect GetTextureRect()
		{
			Rect result;
			this.GetTextureRect_Injected(out result);
			return result;
		}

		internal Vector2 GetTextureRectOffset()
		{
			Vector2 result;
			this.GetTextureRectOffset_Injected(out result);
			return result;
		}

		internal Vector4 GetInnerUVs()
		{
			Vector4 result;
			this.GetInnerUVs_Injected(out result);
			return result;
		}

		internal Vector4 GetOuterUVs()
		{
			Vector4 result;
			this.GetOuterUVs_Injected(out result);
			return result;
		}

		internal Vector4 GetPadding()
		{
			Vector4 result;
			this.GetPadding_Injected(out result);
			return result;
		}

		[FreeFunction("SpritesBindings::CreateSpriteWithoutTextureScripting")]
		internal static Sprite CreateSpriteWithoutTextureScripting(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting_Injected(ref rect, ref pivot, pixelsToUnits, texture);
		}

		[FreeFunction("SpritesBindings::CreateSprite")]
		internal static Sprite CreateSprite(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			return Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border, generateFallbackPhysicsShape);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Texture2D GetSecondaryTexture(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPhysicsShapeCount();

		public int GetPhysicsShapePointCount(int shapeIdx)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			return this.Internal_GetPhysicsShapePointCount(shapeIdx);
		}

		[NativeMethod("GetPhysicsShapePointCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetPhysicsShapePointCount(int shapeIdx);

		public int GetPhysicsShape(int shapeIdx, List<Vector2> physicsShape)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			Sprite.GetPhysicsShapeImpl(this, shapeIdx, physicsShape);
			return physicsShape.Count;
		}

		[FreeFunction("SpritesBindings::GetPhysicsShape", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsShapeImpl(Sprite sprite, int shapeIdx, List<Vector2> physicsShape);

		public void OverridePhysicsShape(IList<Vector2[]> physicsShapes)
		{
			for (int i = 0; i < physicsShapes.Count; i++)
			{
				Vector2[] array = physicsShapes[i];
				bool flag = array == null;
				if (flag)
				{
					throw new ArgumentNullException(string.Format("Physics Shape at {0} is null.", i));
				}
				bool flag2 = array.Length < 3;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Physics Shape at {0} has less than 3 vertices ({1}).", i, array.Length));
				}
			}
			Sprite.OverridePhysicsShapeCount(this, physicsShapes.Count);
			for (int j = 0; j < physicsShapes.Count; j++)
			{
				Sprite.OverridePhysicsShape(this, physicsShapes[j], j);
			}
		}

		[FreeFunction("SpritesBindings::OverridePhysicsShapeCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverridePhysicsShapeCount(Sprite sprite, int physicsShapeCount);

		[FreeFunction("SpritesBindings::OverridePhysicsShape", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverridePhysicsShape(Sprite sprite, Vector2[] physicsShape, int idx);

		[FreeFunction("SpritesBindings::OverrideGeometry", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void OverrideGeometry(Vector2[] vertices, ushort[] triangles);

		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, texture);
		}

		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, null);
		}

		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			bool flag = texture == null;
			Sprite result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = rect.xMax > (float)texture.width || rect.yMax > (float)texture.height;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Could not create sprite ({0}, {1}, {2}, {3}) from a {4}x{5} texture.", new object[]
					{
						rect.x,
						rect.y,
						rect.width,
						rect.height,
						texture.width,
						texture.height
					}));
				}
				bool flag3 = pixelsPerUnit <= 0f;
				if (flag3)
				{
					throw new ArgumentException("pixelsPerUnit must be set to a positive non-zero value.");
				}
				result = Sprite.CreateSprite(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, generateFallbackPhysicsShape);
			}
			return result;
		}

		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, false);
		}

		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, Vector4.zero);
		}

		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, SpriteMeshType.Tight);
		}

		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, 0u);
		}

		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			return Sprite.Create(texture, rect, pivot, 100f);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTextureRect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTextureRectOffset_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetInnerUVs_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetOuterUVs_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPadding_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Sprite CreateSpriteWithoutTextureScripting_Injected(ref Rect rect, ref Vector2 pivot, float pixelsToUnits, Texture2D texture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Sprite CreateSprite_Injected(Texture2D texture, ref Rect rect, ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, ref Vector4 border, bool generateFallbackPhysicsShape);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_border_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pivot_Injected(out Vector2 ret);
	}
}
