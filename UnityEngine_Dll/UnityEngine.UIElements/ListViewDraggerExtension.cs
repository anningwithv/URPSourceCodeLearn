using System;

namespace UnityEngine.UIElements
{
	internal static class ListViewDraggerExtension
	{
		public static ListView.RecycledItem GetRecycledItemFromIndex(this ListView listView, int index)
		{
			ListView.RecycledItem result;
			foreach (ListView.RecycledItem current in listView.Pool)
			{
				bool flag = current.index.Equals(index);
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
