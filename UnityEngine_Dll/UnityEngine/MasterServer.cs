using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
	public class MasterServer
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static string ipAddress
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
		public static int port
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
		public static int updateRate
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
		public static bool dedicatedServer
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
		public static void RequestHostList(string gameTypeName)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static HostData[] PollHostList()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void RegisterHost(string gameTypeName, string gameName)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void RegisterHost(string gameTypeName, string gameName, string comment)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void UnregisterHost()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void ClearHostList()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}
	}
}
