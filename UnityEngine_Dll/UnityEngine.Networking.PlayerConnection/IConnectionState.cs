using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Networking.PlayerConnection
{
	[MovedFrom("UnityEngine.Experimental.Networking.PlayerConnection")]
	public interface IConnectionState : IDisposable
	{
		ConnectionTarget connectedToTarget
		{
			get;
		}

		string connectionName
		{
			get;
		}
	}
}
