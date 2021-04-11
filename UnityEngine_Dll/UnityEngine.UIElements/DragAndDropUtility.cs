using System;

namespace UnityEngine.UIElements
{
	internal static class DragAndDropUtility
	{
		private static Func<IDragAndDrop> s_MakeClientFunc;

		private static IDragAndDrop s_DragAndDrop;

		public static IDragAndDrop dragAndDrop
		{
			get
			{
				bool flag = DragAndDropUtility.s_DragAndDrop == null;
				if (flag)
				{
					bool flag2 = DragAndDropUtility.s_MakeClientFunc != null;
					if (flag2)
					{
						DragAndDropUtility.s_DragAndDrop = DragAndDropUtility.s_MakeClientFunc();
					}
					else
					{
						DragAndDropUtility.s_DragAndDrop = new DefaultDragAndDropClient();
					}
				}
				return DragAndDropUtility.s_DragAndDrop;
			}
		}

		internal static void RegisterMakeClientFunc(Func<IDragAndDrop> makeClient)
		{
			bool flag = DragAndDropUtility.s_MakeClientFunc != null;
			if (flag)
			{
				throw new UnityException("The MakeClientFunc has already been registered. Registration denied.");
			}
			DragAndDropUtility.s_MakeClientFunc = makeClient;
		}
	}
}
