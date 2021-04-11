using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms
{
	public class Local : ISocialPlatform
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly Local.<>c <>9 = new Local.<>c();

			public static Comparison<Score> <>9__20_0;

			internal int <SortScores>b__20_0(Score s1, Score s2)
			{
				return s2.value.CompareTo(s1.value);
			}
		}

		private static LocalUser m_LocalUser = null;

		private List<UserProfile> m_Friends = new List<UserProfile>();

		private List<UserProfile> m_Users = new List<UserProfile>();

		private List<AchievementDescription> m_AchievementDescriptions = new List<AchievementDescription>();

		private List<Achievement> m_Achievements = new List<Achievement>();

		private List<Leaderboard> m_Leaderboards = new List<Leaderboard>();

		private Texture2D m_DefaultTexture;

		public ILocalUser localUser
		{
			get
			{
				bool flag = Local.m_LocalUser == null;
				if (flag)
				{
					Local.m_LocalUser = new LocalUser();
				}
				return Local.m_LocalUser;
			}
		}

		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool> callback)
		{
			LocalUser localUser = (LocalUser)user;
			this.m_DefaultTexture = this.CreateDummyTexture(32, 32);
			this.PopulateStaticData();
			localUser.SetAuthenticated(true);
			localUser.SetUnderage(false);
			localUser.SetUserID("1000");
			localUser.SetUserName("Lerpz");
			localUser.SetImage(this.m_DefaultTexture);
			bool flag = callback != null;
			if (flag)
			{
				callback(true);
			}
		}

		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool, string> callback)
		{
			((ISocialPlatform)this).Authenticate(user, delegate(bool success)
			{
				callback(success, null);
			});
		}

		void ISocialPlatform.LoadFriends(ILocalUser user, Action<bool> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				LocalUser arg_23_0 = (LocalUser)user;
				IUserProfile[] friends = this.m_Friends.ToArray();
				arg_23_0.SetFriends(friends);
				bool flag2 = callback != null;
				if (flag2)
				{
					callback(true);
				}
			}
		}

		public void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			List<UserProfile> list = new List<UserProfile>();
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				for (int i = 0; i < userIDs.Length; i++)
				{
					string b = userIDs[i];
					foreach (UserProfile current in this.m_Users)
					{
						bool flag2 = current.id == b;
						if (flag2)
						{
							list.Add(current);
						}
					}
					foreach (UserProfile current2 in this.m_Friends)
					{
						bool flag3 = current2.id == b;
						if (flag3)
						{
							list.Add(current2);
						}
					}
				}
				IUserProfile[] obj = list.ToArray();
				callback(obj);
			}
		}

		public void ReportProgress(string id, double progress, Action<bool> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				foreach (Achievement current in this.m_Achievements)
				{
					bool flag2 = current.id == id && current.percentCompleted <= progress;
					if (flag2)
					{
						bool flag3 = progress >= 100.0;
						if (flag3)
						{
							current.SetCompleted(true);
						}
						current.SetHidden(false);
						current.SetLastReportedDate(DateTime.Now);
						current.percentCompleted = progress;
						bool flag4 = callback != null;
						if (flag4)
						{
							callback(true);
						}
						return;
					}
				}
				foreach (AchievementDescription current2 in this.m_AchievementDescriptions)
				{
					bool flag5 = current2.id == id;
					if (flag5)
					{
						bool completed = progress >= 100.0;
						Achievement item = new Achievement(id, progress, completed, false, DateTime.Now);
						this.m_Achievements.Add(item);
						bool flag6 = callback != null;
						if (flag6)
						{
							callback(true);
						}
						return;
					}
				}
				Debug.LogError("Achievement ID not found");
				bool flag7 = callback != null;
				if (flag7)
				{
					callback(false);
				}
			}
		}

		public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					IAchievementDescription[] obj = this.m_AchievementDescriptions.ToArray();
					callback(obj);
				}
			}
		}

		public void LoadAchievements(Action<IAchievement[]> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					IAchievement[] obj = this.m_Achievements.ToArray();
					callback(obj);
				}
			}
		}

		public void ReportScore(long score, string board, Action<bool> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				foreach (Leaderboard current in this.m_Leaderboards)
				{
					bool flag2 = current.id == board;
					if (flag2)
					{
						List<Score> list = new List<Score>((Score[])current.scores);
						list.Add(new Score(board, score, this.localUser.id, DateTime.Now, score.ToString() + " points", 0));
						Leaderboard arg_8E_0 = current;
						IScore[] scores = list.ToArray();
						arg_8E_0.SetScores(scores);
						bool flag3 = callback != null;
						if (flag3)
						{
							callback(true);
						}
						return;
					}
				}
				Debug.LogError("Leaderboard not found");
				bool flag4 = callback != null;
				if (flag4)
				{
					callback(false);
				}
			}
		}

		public void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				foreach (Leaderboard current in this.m_Leaderboards)
				{
					bool flag2 = current.id == leaderboardID;
					if (flag2)
					{
						this.SortScores(current);
						bool flag3 = callback != null;
						if (flag3)
						{
							callback(current.scores);
						}
						return;
					}
				}
				Debug.LogError("Leaderboard not found");
				bool flag4 = callback != null;
				if (flag4)
				{
					IScore[] obj = new Score[0];
					callback(obj);
				}
			}
		}

		void ISocialPlatform.LoadScores(ILeaderboard board, Action<bool> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				Leaderboard leaderboard = (Leaderboard)board;
				foreach (Leaderboard current in this.m_Leaderboards)
				{
					bool flag2 = current.id == leaderboard.id;
					if (flag2)
					{
						leaderboard.SetTitle(current.title);
						leaderboard.SetScores(current.scores);
						leaderboard.SetMaxRange((uint)current.scores.Length);
					}
				}
				this.SortScores(leaderboard);
				this.SetLocalPlayerScore(leaderboard);
				bool flag3 = callback != null;
				if (flag3)
				{
					callback(true);
				}
			}
		}

		bool ISocialPlatform.GetLoading(ILeaderboard board)
		{
			bool flag = !this.VerifyUser();
			return !flag && ((Leaderboard)board).loading;
		}

		private void SortScores(Leaderboard board)
		{
			List<Score> list = new List<Score>((Score[])board.scores);
			List<Score> arg_32_0 = list;
			Comparison<Score> arg_32_1;
			if ((arg_32_1 = Local.<>c.<>9__20_0) == null)
			{
				arg_32_1 = (Local.<>c.<>9__20_0 = new Comparison<Score>(Local.<>c.<>9.<SortScores>b__20_0));
			}
			arg_32_0.Sort(arg_32_1);
			for (int i = 0; i < list.Count; i++)
			{
				list[i].SetRank(i + 1);
			}
		}

		private void SetLocalPlayerScore(Leaderboard board)
		{
			IScore[] scores = board.scores;
			for (int i = 0; i < scores.Length; i++)
			{
				Score score = (Score)scores[i];
				bool flag = score.userID == this.localUser.id;
				if (flag)
				{
					board.SetLocalUserScore(score);
					break;
				}
			}
		}

		public void ShowAchievementsUI()
		{
			Debug.Log("ShowAchievementsUI not implemented");
		}

		public void ShowLeaderboardUI()
		{
			Debug.Log("ShowLeaderboardUI not implemented");
		}

		public ILeaderboard CreateLeaderboard()
		{
			return new Leaderboard();
		}

		public IAchievement CreateAchievement()
		{
			return new Achievement();
		}

		private bool VerifyUser()
		{
			bool flag = !this.localUser.authenticated;
			bool result;
			if (flag)
			{
				Debug.LogError("Must authenticate first");
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private void PopulateStaticData()
		{
			this.m_Friends.Add(new UserProfile("Fred", "1001", true, UserState.Online, this.m_DefaultTexture));
			this.m_Friends.Add(new UserProfile("Julia", "1002", true, UserState.Online, this.m_DefaultTexture));
			this.m_Friends.Add(new UserProfile("Jeff", "1003", true, UserState.Online, this.m_DefaultTexture));
			this.m_Users.Add(new UserProfile("Sam", "1004", false, UserState.Offline, this.m_DefaultTexture));
			this.m_Users.Add(new UserProfile("Max", "1005", false, UserState.Offline, this.m_DefaultTexture));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement01", "First achievement", this.m_DefaultTexture, "Get first achievement", "Received first achievement", false, 10));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement02", "Second achievement", this.m_DefaultTexture, "Get second achievement", "Received second achievement", false, 20));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement03", "Third achievement", this.m_DefaultTexture, "Get third achievement", "Received third achievement", false, 15));
			Leaderboard leaderboard = new Leaderboard();
			leaderboard.SetTitle("High Scores");
			leaderboard.id = "Leaderboard01";
			List<Score> list = new List<Score>();
			list.Add(new Score("Leaderboard01", 300L, "1001", DateTime.Now.AddDays(-1.0), "300 points", 1));
			list.Add(new Score("Leaderboard01", 255L, "1002", DateTime.Now.AddDays(-1.0), "255 points", 2));
			list.Add(new Score("Leaderboard01", 55L, "1003", DateTime.Now.AddDays(-1.0), "55 points", 3));
			list.Add(new Score("Leaderboard01", 10L, "1004", DateTime.Now.AddDays(-1.0), "10 points", 4));
			Leaderboard arg_241_0 = leaderboard;
			IScore[] scores = list.ToArray();
			arg_241_0.SetScores(scores);
			this.m_Leaderboards.Add(leaderboard);
		}

		private Texture2D CreateDummyTexture(int width, int height)
		{
			Texture2D texture2D = new Texture2D(width, height);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color color = ((j & i) > 0) ? Color.white : Color.gray;
					texture2D.SetPixel(j, i, color);
				}
			}
			texture2D.Apply();
			return texture2D;
		}
	}
}
