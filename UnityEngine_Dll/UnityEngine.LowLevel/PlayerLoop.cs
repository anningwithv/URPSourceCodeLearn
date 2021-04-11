using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public class PlayerLoop
	{
		public static PlayerLoopSystem GetDefaultPlayerLoop()
		{
			PlayerLoopSystemInternal[] defaultPlayerLoopInternal = PlayerLoop.GetDefaultPlayerLoopInternal();
			int num = 0;
			return PlayerLoop.InternalToPlayerLoopSystem(defaultPlayerLoopInternal, ref num);
		}

		public static PlayerLoopSystem GetCurrentPlayerLoop()
		{
			PlayerLoopSystemInternal[] currentPlayerLoopInternal = PlayerLoop.GetCurrentPlayerLoopInternal();
			int num = 0;
			return PlayerLoop.InternalToPlayerLoopSystem(currentPlayerLoopInternal, ref num);
		}

		public static void SetPlayerLoop(PlayerLoopSystem loop)
		{
			List<PlayerLoopSystemInternal> list = new List<PlayerLoopSystemInternal>();
			PlayerLoop.PlayerLoopSystemToInternal(loop, ref list);
			PlayerLoop.SetPlayerLoopInternal(list.ToArray());
		}

		private static int PlayerLoopSystemToInternal(PlayerLoopSystem sys, ref List<PlayerLoopSystemInternal> internalSys)
		{
			int count = internalSys.Count;
			PlayerLoopSystemInternal playerLoopSystemInternal = new PlayerLoopSystemInternal
			{
				type = sys.type,
				updateDelegate = sys.updateDelegate,
				updateFunction = sys.updateFunction,
				loopConditionFunction = sys.loopConditionFunction,
				numSubSystems = 0
			};
			internalSys.Add(playerLoopSystemInternal);
			bool flag = sys.subSystemList != null;
			if (flag)
			{
				for (int i = 0; i < sys.subSystemList.Length; i++)
				{
					playerLoopSystemInternal.numSubSystems += PlayerLoop.PlayerLoopSystemToInternal(sys.subSystemList[i], ref internalSys);
				}
			}
			internalSys[count] = playerLoopSystemInternal;
			return playerLoopSystemInternal.numSubSystems + 1;
		}

		private static PlayerLoopSystem InternalToPlayerLoopSystem(PlayerLoopSystemInternal[] internalSys, ref int offset)
		{
			PlayerLoopSystem result = new PlayerLoopSystem
			{
				type = internalSys[offset].type,
				updateDelegate = internalSys[offset].updateDelegate,
				updateFunction = internalSys[offset].updateFunction,
				loopConditionFunction = internalSys[offset].loopConditionFunction,
				subSystemList = null
			};
			int num = offset;
			offset = num + 1;
			int num2 = num;
			bool flag = internalSys[num2].numSubSystems > 0;
			if (flag)
			{
				List<PlayerLoopSystem> list = new List<PlayerLoopSystem>();
				while (offset <= num2 + internalSys[num2].numSubSystems)
				{
					list.Add(PlayerLoop.InternalToPlayerLoopSystem(internalSys, ref offset));
				}
				result.subSystemList = list.ToArray();
			}
			return result;
		}

		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayerLoopSystemInternal[] GetDefaultPlayerLoopInternal();

		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayerLoopSystemInternal[] GetCurrentPlayerLoopInternal();

		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPlayerLoopInternal(PlayerLoopSystemInternal[] loop);
	}
}
