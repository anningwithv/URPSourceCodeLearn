using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms.GameCenter
{
	[NativeHeader("Modules/GameCenter/Public/GameCenterScriptingGlue.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class GcLeaderboard
	{
		private IntPtr m_InternalLeaderboard;

		private Leaderboard m_GenericLeaderboard;

		internal GcLeaderboard(Leaderboard board)
		{
			this.m_GenericLeaderboard = board;
		}

		~GcLeaderboard()
		{
			this.Dispose();
		}

		internal bool Contains(Leaderboard board)
		{
			return this.m_GenericLeaderboard == board;
		}

		internal void SetScores(GcScoreData[] scoreDatas)
		{
			bool flag = this.m_GenericLeaderboard != null;
			if (flag)
			{
				Score[] array = new Score[scoreDatas.Length];
				for (int i = 0; i < scoreDatas.Length; i++)
				{
					array[i] = scoreDatas[i].ToScore();
				}
				Leaderboard arg_44_0 = this.m_GenericLeaderboard;
				IScore[] scores = array;
				arg_44_0.SetScores(scores);
			}
		}

		internal void SetLocalScore(GcScoreData scoreData)
		{
			bool flag = this.m_GenericLeaderboard != null;
			if (flag)
			{
				this.m_GenericLeaderboard.SetLocalUserScore(scoreData.ToScore());
			}
		}

		internal void SetMaxRange(uint maxRange)
		{
			bool flag = this.m_GenericLeaderboard != null;
			if (flag)
			{
				this.m_GenericLeaderboard.SetMaxRange(maxRange);
			}
		}

		internal void SetTitle(string title)
		{
			bool flag = this.m_GenericLeaderboard != null;
			if (flag)
			{
				this.m_GenericLeaderboard.SetTitle(title);
			}
		}

		internal void Internal_LoadScores(string category, int from, int count, string[] userIDs, int playerScope, int timeScope, object callback)
		{
			this.m_InternalLeaderboard = GcLeaderboard.GcLeaderboard_LoadScores(this, category, from, count, userIDs, playerScope, timeScope, callback);
		}

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GcLeaderboard_LoadScores(object self, string category, int from, int count, string[] userIDs, int playerScope, int timeScope, object callback);

		internal bool Loading()
		{
			return GcLeaderboard.GcLeaderboard_Loading(this.m_InternalLeaderboard);
		}

		[NativeConditional("ENABLE_GAMECENTER"), StaticAccessor("GameCenter", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GcLeaderboard_Loading(IntPtr leaderboard);

		internal void Dispose()
		{
			GcLeaderboard.GcLeaderboard_Dispose(this.m_InternalLeaderboard);
			this.m_InternalLeaderboard = IntPtr.Zero;
		}

		[NativeConditional("ENABLE_GAMECENTER"), NativeMethod(IsThreadSafe = true), StaticAccessor("GameCenter", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GcLeaderboard_Dispose(IntPtr leaderboard);
	}
}
