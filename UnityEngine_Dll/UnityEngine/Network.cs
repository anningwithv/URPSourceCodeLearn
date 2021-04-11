using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
	public class Network
	{
		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static string incomingPassword
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
		public static NetworkLogLevel logLevel
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
		public static NetworkPlayer[] connections
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkPlayer player
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static bool isClient
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static bool isServer
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkPeerType peerType
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static float sendRate
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
		public static bool isMessageQueueRunning
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
		public static double time
		{
			get
			{
				throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static int minimumAllocatableViewIDs
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
		public static bool useNat
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
		public static string natFacilitatorIP
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
		public static int natFacilitatorPort
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
		public static string connectionTesterIP
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
		public static int connectionTesterPort
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
		public static int maxConnections
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
		public static string proxyIP
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
		public static int proxyPort
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
		public static bool useProxy
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
		public static string proxyPassword
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
		public static NetworkConnectionError InitializeServer(int connections, int listenPort, bool useNat)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError InitializeServer(int connections, int listenPort)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void InitializeSecurity()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError Connect(string IP, int remotePort)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError Connect(string IP, int remotePort, string password)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError Connect(string[] IPs, int remotePort)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError Connect(string[] IPs, int remotePort, string password)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError Connect(string GUID)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError Connect(string GUID, string password)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError Connect(HostData hostData)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkConnectionError Connect(HostData hostData, string password)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void Disconnect()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void Disconnect(int timeout)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void CloseConnection(NetworkPlayer target, bool sendDisconnectionNotification)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static NetworkViewID AllocateViewID()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static Object Instantiate(Object prefab, Vector3 position, Quaternion rotation, int group)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void Destroy(NetworkViewID viewID)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void Destroy(GameObject gameObject)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void DestroyPlayerObjects(NetworkPlayer playerID)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void RemoveRPCs(NetworkPlayer playerID)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void RemoveRPCs(NetworkPlayer playerID, int group)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void RemoveRPCs(NetworkViewID viewID)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void RemoveRPCsInGroup(int group)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void SetLevelPrefix(int prefix)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static int GetLastPing(NetworkPlayer player)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static int GetAveragePing(NetworkPlayer player)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void SetReceivingEnabled(NetworkPlayer player, int group, bool enabled)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void SetSendingEnabled(int group, bool enabled)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static void SetSendingEnabled(NetworkPlayer player, int group, bool enabled)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static ConnectionTesterStatus TestConnection()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static ConnectionTesterStatus TestConnection(bool forceTest)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static ConnectionTesterStatus TestConnectionNAT()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static ConnectionTesterStatus TestConnectionNAT(bool forceTest)
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never), Obsolete("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.", true)]
		public static bool HavePublicAddress()
		{
			throw new NotSupportedException("The legacy networking system has been removed in Unity 2018.2. Use Unity Multiplayer and NetworkIdentity instead.");
		}
	}
}
