using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	[Serializable]
	internal class ListMatchResponse : BasicResponse
	{
		public List<MatchDesc> matches;

		public ListMatchResponse()
		{
			this.matches = new List<MatchDesc>();
		}

		public ListMatchResponse(List<MatchDesc> otherMatches)
		{
			this.matches = otherMatches;
		}

		public override string ToString()
		{
			return UnityString.Format("[{0}]-matches.Count:{1}", new object[]
			{
				base.ToString(),
				(this.matches == null) ? 0 : this.matches.Count
			});
		}
	}
}
