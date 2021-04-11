using System;
using UnityEngine.Scripting;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms.GameCenter
{
	[RequiredByNativeCode]
	internal struct GcUserProfileData
	{
		public string userName;

		public string teamID;

		public string gameID;

		public int isFriend;

		public Texture2D image;

		public UserProfile ToUserProfile()
		{
			return new UserProfile(this.userName, this.teamID, this.gameID, this.isFriend == 1, UserState.Offline, this.image);
		}

		public void AddToArray(ref UserProfile[] array, int number)
		{
			bool flag = array.Length > number && number >= 0;
			if (flag)
			{
				array[number] = this.ToUserProfile();
			}
			else
			{
				Debug.Log("Index number out of bounds when setting user data");
			}
		}
	}
}
