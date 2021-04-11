using System;

namespace UnityEngine.UIElements
{
	internal class RepaintData
	{
		public Matrix4x4 currentOffset
		{
			get;
			set;
		}

		public Vector2 mousePosition
		{
			get;
			set;
		}

		public Rect currentWorldClip
		{
			get;
			set;
		}

		public Event repaintEvent
		{
			get;
			set;
		}

		public RepaintData()
		{
			this.<currentOffset>k__BackingField = Matrix4x4.identity;
			base..ctor();
		}
	}
}
