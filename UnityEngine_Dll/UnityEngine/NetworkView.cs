using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true), NativeClass(null)]
	public class NetworkView : Behaviour
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public Component observed
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
			set
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public NetworkStateSynchronization stateSynchronization
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
			set
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public NetworkViewID viewID
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
			set
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public int group
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
			set
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public bool isMine
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public NetworkPlayer owner
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void RPC(string name, RPCMode mode, params object[] args)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public void RPC(string name, NetworkPlayer target, params object[] args)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		private bool SetScope(NetworkPlayer player, bool relevancy)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		private static NetworkView Find(NetworkViewID viewID)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}
	}
}
