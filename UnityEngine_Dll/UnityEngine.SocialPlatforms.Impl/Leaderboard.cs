using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	public class Leaderboard : ILeaderboard
	{
		private bool m_Loading;

		private IScore m_LocalUserScore;

		private uint m_MaxRange;

		private IScore[] m_Scores;

		private string m_Title;

		private string[] m_UserIDs;

		public bool loading
		{
			get
			{
				return ActivePlatform.Instance.GetLoading(this);
			}
		}

		public string id
		{
			get;
			set;
		}

		public UserScope userScope
		{
			get;
			set;
		}

		public Range range
		{
			get;
			set;
		}

		public TimeScope timeScope
		{
			get;
			set;
		}

		public IScore localUserScore
		{
			get
			{
				return this.m_LocalUserScore;
			}
		}

		public uint maxRange
		{
			get
			{
				return this.m_MaxRange;
			}
		}

		public IScore[] scores
		{
			get
			{
				return this.m_Scores;
			}
		}

		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		public Leaderboard()
		{
			this.id = "Invalid";
			this.range = new Range(1, 10);
			this.userScope = UserScope.Global;
			this.timeScope = TimeScope.AllTime;
			this.m_Loading = false;
			this.m_LocalUserScore = new Score("Invalid", 0L);
			this.m_MaxRange = 0u;
			IScore[] scores = new Score[0];
			this.m_Scores = scores;
			this.m_Title = "Invalid";
			this.m_UserIDs = new string[0];
		}

		public void SetUserFilter(string[] userIDs)
		{
			this.m_UserIDs = userIDs;
		}

		public override string ToString()
		{
			string[] expr_08 = new string[20];
			expr_08[0] = "ID: '";
			expr_08[1] = this.id;
			expr_08[2] = "' Title: '";
			expr_08[3] = this.m_Title;
			expr_08[4] = "' Loading: '";
			expr_08[5] = this.m_Loading.ToString();
			expr_08[6] = "' Range: [";
			int arg_5D_1 = 7;
			Range range = this.range;
			expr_08[arg_5D_1] = range.from.ToString();
			expr_08[8] = ",";
			int arg_7C_1 = 9;
			range = this.range;
			expr_08[arg_7C_1] = range.count.ToString();
			expr_08[10] = "] MaxRange: '";
			expr_08[11] = this.m_MaxRange.ToString();
			expr_08[12] = "' Scores: '";
			expr_08[13] = this.m_Scores.Length.ToString();
			expr_08[14] = "' UserScope: '";
			expr_08[15] = this.userScope.ToString();
			expr_08[16] = "' TimeScope: '";
			expr_08[17] = this.timeScope.ToString();
			expr_08[18] = "' UserFilter: '";
			expr_08[19] = this.m_UserIDs.Length.ToString();
			return string.Concat(expr_08);
		}

		public void LoadScores(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadScores(this, callback);
		}

		public void SetLocalUserScore(IScore score)
		{
			this.m_LocalUserScore = score;
		}

		public void SetMaxRange(uint maxRange)
		{
			this.m_MaxRange = maxRange;
		}

		public void SetScores(IScore[] scores)
		{
			this.m_Scores = scores;
		}

		public void SetTitle(string title)
		{
			this.m_Title = title;
		}

		public string[] GetUserFilter()
		{
			return this.m_UserIDs;
		}
	}
}
