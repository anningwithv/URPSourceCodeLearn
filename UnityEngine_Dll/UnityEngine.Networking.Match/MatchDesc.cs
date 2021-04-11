using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	[Serializable]
	internal class MatchDesc
	{
		public ulong networkId;

		public string name;

		public int averageEloScore;

		public int maxSize;

		public int currentSize;

		public bool isPrivate;

		public Dictionary<string, long> matchAttributes;

		public NodeID hostNodeId;

		public List<MatchDirectConnectInfo> directConnectInfos;

		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1},name:{2},averageEloScore:{3},maxSize:{4},currentSize:{5},isPrivate:{6},matchAttributes.Count:{7},hostNodeId:{8},directConnectInfos.Count:{9}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				this.name,
				this.averageEloScore,
				this.maxSize,
				this.currentSize,
				this.isPrivate,
				(this.matchAttributes == null) ? 0 : this.matchAttributes.Count,
				this.hostNodeId,
				this.directConnectInfos.Count
			});
		}
	}
}
