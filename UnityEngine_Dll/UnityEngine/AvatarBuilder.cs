using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public class AvatarBuilder
	{
		public static Avatar BuildHumanAvatar(GameObject go, HumanDescription humanDescription)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return AvatarBuilder.BuildHumanAvatarInternal(go, humanDescription);
		}

		[FreeFunction("AvatarBuilderBindings::BuildHumanAvatar")]
		private static Avatar BuildHumanAvatarInternal(GameObject go, HumanDescription humanDescription)
		{
			return AvatarBuilder.BuildHumanAvatarInternal_Injected(go, ref humanDescription);
		}

		[FreeFunction("AvatarBuilderBindings::BuildGenericAvatar")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Avatar BuildGenericAvatar([NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] string rootMotionTransformName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Avatar BuildHumanAvatarInternal_Injected(GameObject go, ref HumanDescription humanDescription);
	}
}
