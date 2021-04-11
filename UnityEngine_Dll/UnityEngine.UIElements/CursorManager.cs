using System;

namespace UnityEngine.UIElements
{
	internal class CursorManager : ICursorManager
	{
		public void SetCursor(Cursor cursor)
		{
			bool flag = cursor.texture != null;
			if (flag)
			{
				UnityEngine.Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
			}
			else
			{
				bool flag2 = cursor.defaultCursorId != 0;
				if (flag2)
				{
					Debug.LogWarning("Runtime cursors other than the default cursor need to be defined using a texture.");
				}
				this.ResetCursor();
			}
		}

		public void ResetCursor()
		{
			UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
	}
}
