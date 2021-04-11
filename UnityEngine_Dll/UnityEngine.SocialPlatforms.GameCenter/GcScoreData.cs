using System;
using UnityEngine.Scripting;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms.GameCenter
{
	[RequiredByNativeCode]
	internal struct GcScoreData
	{
		public string m_Category;

		public uint m_ValueLow;

		public int m_ValueHigh;

		public int m_Date;

		public string m_FormattedValue;

		public string m_PlayerID;

		public int m_Rank;

		public Score ToScore()
		{
			return new Score(this.m_Category, ((long)this.m_ValueHigh << 32) + (long)((ulong)this.m_ValueLow), this.m_PlayerID, new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((double)this.m_Date), this.m_FormattedValue, this.m_Rank);
		}
	}
}
