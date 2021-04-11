using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
	internal static class BeforeRenderHelper
	{
		private struct OrderBlock
		{
			internal int order;

			internal UnityAction callback;
		}

		private static List<BeforeRenderHelper.OrderBlock> s_OrderBlocks = new List<BeforeRenderHelper.OrderBlock>();

		private static int GetUpdateOrder(UnityAction callback)
		{
			object[] customAttributes = callback.Method.GetCustomAttributes(typeof(BeforeRenderOrderAttribute), true);
			BeforeRenderOrderAttribute beforeRenderOrderAttribute = (customAttributes != null && customAttributes.Length != 0) ? (customAttributes[0] as BeforeRenderOrderAttribute) : null;
			return (beforeRenderOrderAttribute != null) ? beforeRenderOrderAttribute.order : 0;
		}

		public static void RegisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> obj = BeforeRenderHelper.s_OrderBlocks;
			lock (obj)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag)
					{
						BeforeRenderHelper.OrderBlock value = BeforeRenderHelper.s_OrderBlocks[num];
						value.callback = (UnityAction)Delegate.Combine(value.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = value;
						return;
					}
					num++;
				}
				BeforeRenderHelper.OrderBlock item = default(BeforeRenderHelper.OrderBlock);
				item.order = updateOrder;
				item.callback = (UnityAction)Delegate.Combine(item.callback, callback);
				BeforeRenderHelper.s_OrderBlocks.Insert(num, item);
			}
		}

		public static void UnregisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> obj = BeforeRenderHelper.s_OrderBlocks;
			lock (obj)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag)
					{
						BeforeRenderHelper.OrderBlock orderBlock = BeforeRenderHelper.s_OrderBlocks[num];
						orderBlock.callback = (UnityAction)Delegate.Remove(orderBlock.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = orderBlock;
						bool flag2 = orderBlock.callback == null;
						if (flag2)
						{
							BeforeRenderHelper.s_OrderBlocks.RemoveAt(num);
						}
						break;
					}
					num++;
				}
			}
		}

		public static void Invoke()
		{
			List<BeforeRenderHelper.OrderBlock> obj = BeforeRenderHelper.s_OrderBlocks;
			lock (obj)
			{
				for (int i = 0; i < BeforeRenderHelper.s_OrderBlocks.Count; i++)
				{
					UnityAction callback = BeforeRenderHelper.s_OrderBlocks[i].callback;
					bool flag = callback != null;
					if (flag)
					{
						callback();
					}
				}
			}
		}
	}
}
