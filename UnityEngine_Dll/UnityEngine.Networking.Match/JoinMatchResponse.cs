using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	[Serializable]
	internal class JoinMatchResponse : BasicResponse
	{
		public string address;

		public int port;

		public int domain = 0;

		public ulong networkId;

		public string accessTokenString;

		public NodeID nodeId;

		public bool usingRelay;

		public override string ToString()
		{
			return UnityString.Format("[{0}]-address:{1},port:{2},networkId:0x{3},accessTokenString.IsEmpty:{4},nodeId:0x{5},usingRelay:{6}", new object[]
			{
				base.ToString(),
				this.address,
				this.port,
				this.networkId.ToString("X"),
				string.IsNullOrEmpty(this.accessTokenString),
				this.nodeId.ToString("X"),
				this.usingRelay
			});
		}
	}
}
