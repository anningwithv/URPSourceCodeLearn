using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics2D/Public/CompositeCollider2D.h"), RequireComponent(typeof(Rigidbody2D))]
	public sealed class CompositeCollider2D : Collider2D
	{
		public enum GeometryType
		{
			Outlines,
			Polygons
		}

		public enum GenerationType
		{
			Synchronous,
			Manual
		}

		public extern CompositeCollider2D.GeometryType geometryType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CompositeCollider2D.GenerationType generationType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float vertexDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float edgeRadius
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float offsetDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int pathCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int pointCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GenerateGeometry();

		public int GetPathPointCount(int index)
		{
			int num = this.pathCount - 1;
			bool flag = index < 0 || index > num;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, num));
			}
			return this.GetPathPointCount_Internal(index);
		}

		[NativeMethod("GetPathPointCount_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPathPointCount_Internal(int index);

		public int GetPath(int index, Vector2[] points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathArray_Internal(index, points);
		}

		[NativeMethod("GetPathArray_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPathArray_Internal(int index, [NotNull("ArgumentNullException")] Vector2[] points);

		public int GetPath(int index, List<Vector2> points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathList_Internal(index, points);
		}

		[NativeMethod("GetPathList_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPathList_Internal(int index, [NotNull("ArgumentNullException")] List<Vector2> points);
	}
}
