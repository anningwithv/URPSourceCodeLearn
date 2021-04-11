using System;
using UnityEngine.Scripting;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms.GameCenter
{
	[RequiredByNativeCode]
	internal struct GcAchievementData
	{
		public string m_Identifier;

		public double m_PercentCompleted;

		public int m_Completed;

		public int m_Hidden;

		public int m_LastReportedDate;

		public Achievement ToAchievement()
		{
			return new Achievement(this.m_Identifier, this.m_PercentCompleted, this.m_Completed != 0, this.m_Hidden != 0, new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((double)this.m_LastReportedDate));
		}
	}
}
