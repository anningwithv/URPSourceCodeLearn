using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	[Serializable]
	internal class DropConnectionResponse : Response
	{
		public ulong networkId;

		public NodeID nodeId;

		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:{1}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X")
			});
		}
	}
}
