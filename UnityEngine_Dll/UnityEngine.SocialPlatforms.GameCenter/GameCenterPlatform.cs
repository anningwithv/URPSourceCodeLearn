using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms.GameCenter
{
	[NativeHeader("Modules/GameCenter/Public/GameCenterScriptingGlue.h"), RequiredByNativeCode]
	public class GameCenterPlatform : ISocialPlatform
	{
		private static Action<bool, string> s_AuthenticateCallback;

		private static AchievementDescription[] s_adCache = new AchievementDescription[0];

		private static UserProfile[] s_friends = new UserProfile[0];

		private static UserProfile[] s_users = new UserProfile[0];

		private static Action<bool> s_ResetAchievements;

		private static LocalUser m_LocalUser;

		private static List<GcLeaderboard> m_GcBoards = new List<GcLeaderboard>();

		public ILocalUser localUser
		{
			get
			{
				bool flag = GameCenterPlatform.m_LocalUser == null;
				if (flag)
				{
					GameCenterPlatform.m_LocalUser = new LocalUser();
				}
				bool flag2 = GameCenterPlatform.GetAuthenticated() && GameCenterPlatform.m_LocalUser.id == "0";
				if (flag2)
				{
					GameCenterPlatform.PopulateLocalUser();
				}
				return GameCenterPlatform.m_LocalUser;
			}
		}

		[RequiredByNativeCode]
		private static void ClearAchievementDescriptions(int size)
		{
			bool flag = GameCenterPlatform.s_adCache == null || GameCenterPlatform.s_adCache.Length != size;
			if (flag)
			{
				GameCenterPlatform.s_adCache = new AchievementDescription[size];
			}
		}

		[RequiredByNativeCode]
		private static void SetAchievementDescription(GcAchievementDescriptionData data, int number)
		{
			GameCenterPlatform.s_adCache[number] = data.ToAchievementDescription();
		}

		[RequiredByNativeCode]
		private static void SetAchievementDescriptionImage(Texture2D texture, int number)
		{
			bool flag = GameCenterPlatform.s_adCache.Length <= number || number < 0;
			if (flag)
			{
				Debug.Log("Achievement description number out of bounds when setting image");
			}
			else
			{
				GameCenterPlatform.s_adCache[number].SetImage(texture);
			}
		}

		[RequiredByNativeCode]
		private static void TriggerAchievementDescriptionCallback(Action<IAchievementDescription[]> callback)
		{
			bool flag = callback != null && GameCenterPlatform.s_adCache != null;
			if (flag)
			{
				bool flag2 = GameCenterPlatform.s_adCache.Length == 0;
				if (flag2)
				{
					Debug.Log("No achievement descriptions returned");
				}
				IAchievementDescription[] obj = GameCenterPlatform.s_adCache;
				callback(obj);
			}
		}

		[RequiredByNativeCode]
		private static void AuthenticateCallbackWrapper(int result, string error)
		{
			GameCenterPlatform.PopulateLocalUser();
			bool flag = GameCenterPlatform.s_AuthenticateCallback != null;
			if (flag)
			{
				GameCenterPlatform.s_AuthenticateCallback(result == 1, error);
				GameCenterPlatform.s_AuthenticateCallback = null;
			}
		}

		[RequiredByNativeCode]
		private static void ClearFriends(int size)
		{
			GameCenterPlatform.SafeClearArray(ref GameCenterPlatform.s_friends, size);
		}

		[RequiredByNativeCode]
		private static void SetFriends(GcUserProfileData data, int number)
		{
			data.AddToArray(ref GameCenterPlatform.s_friends, number);
		}

		[RequiredByNativeCode]
		private static void SetFriendImage(Texture2D texture, int number)
		{
			GameCenterPlatform.SafeSetUserImage(ref GameCenterPlatform.s_friends, texture, number);
		}

		[RequiredByNativeCode]
		private static void TriggerFriendsCallbackWrapper(Action<bool> callback, int result)
		{
			bool flag = GameCenterPlatform.s_friends != null;
			if (flag)
			{
				LocalUser arg_19_0 = GameCenterPlatform.m_LocalUser;
				IUserProfile[] friends = GameCenterPlatform.s_friends;
				arg_19_0.SetFriends(friends);
			}
			bool flag2 = callback != null;
			if (flag2)
			{
				callback(result == 1);
			}
		}

		[RequiredByNativeCode]
		private static void AchievementCallbackWrapper(Action<IAchievement[]> callback, GcAchievementData[] result)
		{
			bool flag = callback != null;
			if (flag)
			{
				bool flag2 = result.Length == 0;
				if (flag2)
				{
					Debug.Log("No achievements returned");
				}
				Achievement[] array = new Achievement[result.Length];
				for (int i = 0; i < result.Length; i++)
				{
					array[i] = result[i].ToAchievement();
				}
				IAchievement[] obj = array;
				callback(obj);
			}
		}

		[RequiredByNativeCode]
		private static void ProgressCallbackWrapper(Action<bool> callback, bool success)
		{
			bool flag = callback != null;
			if (flag)
			{
				callback(success);
			}
		}

		[RequiredByNativeCode]
		private static void ScoreCallbackWrapper(Action<bool> callback, bool success)
		{
			bool flag = callback != null;
			if (flag)
			{
				callback(success);
			}
		}

		[RequiredByNativeCode]
		private static void ScoreLoaderCallbackWrapper(Action<IScore[]> callback, GcScoreData[] result)
		{
			bool flag = callback != null;
			if (flag)
			{
				Score[] array = new Score[result.Length];
				for (int i = 0; i < result.Length; i++)
				{
					array[i] = result[i].ToScore();
				}
				IScore[] obj = array;
				callback(obj);
			}
		}

		void ISocialPlatform.LoadFriends(ILocalUser user, Action<bool> callback)
		{
			bool flag = !this.VerifyAuthentication();
			if (flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					callback(false);
				}
			}
			else
			{
				GameCenterPlatform.LoadFriends(callback);
			}
		}

		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool> callback)
		{
			((ISocialPlatform)this).Authenticate(user, delegate(bool success, string error)
			{
				callback(success);
			});
		}

		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool, string> callback)
		{
			GameCenterPlatform.s_AuthenticateCallback = callback;
			GameCenterPlatform.Authenticate();
		}

		[RequiredByNativeCode]
		private static void PopulateLocalUser()
		{
			GameCenterPlatform.m_LocalUser.SetAuthenticated(GameCenterPlatform.GetAuthenticated());
			GameCenterPlatform.m_LocalUser.SetUserName(GameCenterPlatform.Internal_UserName());
			GameCenterPlatform.m_LocalUser.SetUserID(GameCenterPlatform.Internal_UserID());
			GameCenterPlatform.m_LocalUser.SetUserGameID(GameCenterPlatform.Internal_UserGameID());
			GameCenterPlatform.m_LocalUser.SetLegacyUserID(GameCenterPlatform.Internal_LegacyUserID());
			GameCenterPlatform.m_LocalUser.SetUnderage(GameCenterPlatform.GetIsUnderage());
			GameCenterPlatform.m_LocalUser.SetImage(GameCenterPlatform.GetUserImage());
		}

		public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			bool flag = !this.VerifyAuthentication();
			if (flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					IAchievementDescription[] obj = new AchievementDescription[0];
					callback(obj);
				}
			}
			else
			{
				GameCenterPlatform.InternalLoadAchievementDescriptions(callback);
			}
		}

		public void ReportProgress(string id, double progress, Action<bool> callback)
		{
			bool flag = !this.VerifyAuthentication();
			if (flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					callback(false);
				}
			}
			else
			{
				GameCenterPlatform.InternalReportProgress(id, progress, callback);
			}
		}

		public void LoadAchievements(Action<IAchievement[]> callback)
		{
			bool flag = !this.VerifyAuthentication();
			if (flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					IAchievement[] obj = new Achievement[0];
					callback(obj);
				}
			}
			else
			{
				GameCenterPlatform.InternalLoadAchievements(callback);
			}
		}

		public void ReportScore(long score, string board, Action<bool> callback)
		{
			bool flag = !this.VerifyAuthentication();
			if (flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					callback(false);
				}
			}
			else
			{
				GameCenterPlatform.InternalReportScore(score, board, callback);
			}
		}

		public void LoadScores(string category, Action<IScore[]> callback)
		{
			bool flag = !this.VerifyAuthentication();
			if (flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					IScore[] obj = new Score[0];
					callback(obj);
				}
			}
			else
			{
				GameCenterPlatform.InternalLoadScores(category, callback);
			}
		}

		public void LoadScores(ILeaderboard board, Action<bool> callback)
		{
			bool flag = !this.VerifyAuthentication();
			if (flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					callback(false);
				}
			}
			else
			{
				Leaderboard leaderboard = (Leaderboard)board;
				GcLeaderboard gcLeaderboard = new GcLeaderboard(leaderboard);
				GameCenterPlatform.m_GcBoards.Add(gcLeaderboard);
				string[] array = leaderboard.GetUserFilter();
				bool flag3 = array.Length == 0;
				if (flag3)
				{
					array = null;
				}
				gcLeaderboard.Internal_LoadScores(board.id, board.range.from, board.range.count, array, (int)board.userScope, (int)board.timeScope, callback);
			}
		}

		[RequiredByNativeCode]
		private static void LeaderboardCallbackWrapper(Action<bool> callback, bool success)
		{
			bool flag = callback != null;
			if (flag)
			{
				callback(success);
			}
		}

		public bool GetLoading(ILeaderboard board)
		{
			bool flag = !this.VerifyAuthentication();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (GcLeaderboard current in GameCenterPlatform.m_GcBoards)
				{
					bool flag2 = current.Contains((Leaderboard)board);
					if (flag2)
					{
						result = current.Loading();
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		private bool VerifyAuthentication()
		{
			bool flag = !this.localUser.authenticated;
			bool result;
			if (flag)
			{
				Debug.Log("Must authenticate first");
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		public void ShowAchievementsUI()
		{
			bool flag = !this.VerifyAuthentication();
			if (!flag)
			{
				GameCenterPlatform.Internal_ShowAchievementsUI();
			}
		}

		public void ShowLeaderboardUI()
		{
			bool flag = !this.VerifyAuthentication();
			if (!flag)
			{
				GameCenterPlatform.Internal_ShowLeaderboardUI();
			}
		}

		[RequiredByNativeCode]
		private static void ClearUsers(int size)
		{
			GameCenterPlatform.SafeClearArray(ref GameCenterPlatform.s_users, size);
		}

		[RequiredByNativeCode]
		private static void SetUser(GcUserProfileData data, int number)
		{
			data.AddToArray(ref GameCenterPlatform.s_users, number);
		}

		[RequiredByNativeCode]
		private static void SetUserImage(Texture2D texture, int number)
		{
			GameCenterPlatform.SafeSetUserImage(ref GameCenterPlatform.s_users, texture, number);
		}

		[RequiredByNativeCode]
		private static void TriggerUsersCallbackWrapper(Action<IUserProfile[]> callback)
		{
			bool flag = callback != null;
			if (flag)
			{
				IUserProfile[] obj = GameCenterPlatform.s_users;
				callback(obj);
			}
		}

		public void LoadUsers(string[] userIds, Action<IUserProfile[]> callback)
		{
			bool flag = !this.VerifyAuthentication();
			if (flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					IUserProfile[] obj = new UserProfile[0];
					callback(obj);
				}
			}
			else
			{
				GameCenterPlatform.Internal_LoadUsers(userIds, callback);
			}
		}

		[RequiredByNativeCode]
		private static void SafeSetUserImage(ref UserProfile[] array, Texture2D texture, int number)
		{
			bool flag = array.Length <= number || number < 0;
			if (flag)
			{
				Debug.Log("Invalid texture when setting user image");
				texture = new Texture2D(76, 76);
			}
			bool flag2 = array.Length > number && number >= 0;
			if (flag2)
			{
				array[number].SetImage(texture);
			}
			else
			{
				Debug.Log("User number out of bounds when setting image");
			}
		}

		private static void SafeClearArray(ref UserProfile[] array, int size)
		{
			bool flag = array == null || array.Length != size;
			if (flag)
			{
				array = new UserProfile[size];
			}
		}

		public ILeaderboard CreateLeaderboard()
		{
			return new Leaderboard();
		}

		public IAchievement CreateAchievement()
		{
			return new Achievement();
		}

		[RequiredByNativeCode]
		private static void TriggerResetAchievementCallback(bool result)
		{
			bool flag = GameCenterPlatform.s_ResetAchievements != null;
			if (flag)
			{
				GameCenterPlatform.s_ResetAchievements(result);
			}
		}

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter::GcLocalUser::GetInstance()", StaticAccessorType.Arrow)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Authenticate();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter::GcLocalUser::GetInstance()", StaticAccessorType.Arrow)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetAuthenticated();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_UserName();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_UserID();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_UserGameID();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_LegacyUserID();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter::GcLocalUser::GetInstance()", StaticAccessorType.Arrow)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetIsUnderage();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter::GcLocalUser::GetInstance()", StaticAccessorType.Arrow)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Texture2D GetUserImage();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter::GcLocalUser::GetInstance()", StaticAccessorType.Arrow)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void LoadFriends(object callback);

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("LoadAchievementDescriptions"), StaticAccessor("GameCenter::GcAchievementDescription", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalLoadAchievementDescriptions(object callback);

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("LoadAchievements"), StaticAccessor("GameCenter::GcAchievement", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalLoadAchievements(object callback);

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("ReportProgress"), StaticAccessor("GameCenter::GcAchievement", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalReportProgress(string id, double progress, object callback);

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("ReportScore"), StaticAccessor("GameCenter::GcScore", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalReportScore(long score, string category, object callback);

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("LoadScores"), StaticAccessor("GameCenter::GcScore", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalLoadScores(string category, object callback);

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("ShowAchievementsUI"), StaticAccessor("GameCenter::GcAchievementDescription", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_ShowAchievementsUI();

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("ShowLeaderboardUI"), StaticAccessor("GameCenter::GcLeaderboard", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_ShowLeaderboardUI();

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("LoadUsers"), StaticAccessor("GameCenter::GcLocalUser", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_LoadUsers(string[] userIds, object callback);

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter::GcAchievement", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetAllAchievements();

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter::GcAchievement", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ShowDefaultAchievementBanner(bool value);

		public static void ResetAllAchievements(Action<bool> callback)
		{
			GameCenterPlatform.s_ResetAchievements = callback;
			GameCenterPlatform.ResetAllAchievements();
			Debug.Log("ResetAllAchievements - no effect in editor");
			bool flag = callback != null;
			if (flag)
			{
				callback(true);
			}
		}

		public static void ShowDefaultAchievementCompletionBanner(bool value)
		{
			GameCenterPlatform.ShowDefaultAchievementBanner(value);
			Debug.Log("ShowDefaultAchievementCompletionBanner - no effect in editor");
		}

		public static void ShowLeaderboardUI(string leaderboardID, TimeScope timeScope)
		{
			GameCenterPlatform.ShowSpecificLeaderboardUI(leaderboardID, (int)timeScope);
			Debug.Log("ShowLeaderboardUI - no effect in editor");
		}

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod("ShowLeaderboardUI"), StaticAccessor("GameCenter::GcLeaderboard", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ShowSpecificLeaderboardUI(string leaderboardID, int timeScope);
	}
}
