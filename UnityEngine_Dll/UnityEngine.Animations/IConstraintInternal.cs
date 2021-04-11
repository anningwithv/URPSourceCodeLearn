using System;

namespace UnityEngine.Animations
{
	internal interface IConstraintInternal
	{
		Transform transform
		{
			get;
		}

		void ActivateAndPreserveOffset();

		void ActivateWithZeroOffset();

		void UserUpdateOffset();
	}
}
