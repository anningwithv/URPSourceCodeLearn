using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	[Serializable]
	internal class MatchDirectConnectInfo
	{
		public NodeID nodeId;

		public string publicAddress;

		public string privateAddress;

		public HostPriority hostPriority;

		public override string ToString()
		{
			return UnityString.Format("[{0}]-nodeId:{1},publicAddress:{2},privateAddress:{3},hostPriority:{4}", new object[]
			{
				base.ToString(),
				this.nodeId,
				this.publicAddress,
				this.privateAddress,
				this.hostPriority
			});
		}
	}
}
