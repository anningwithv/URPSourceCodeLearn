using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready."), NativeConditional("ENABLE_NETWORK && ENABLE_UNET", true), NativeHeader("Modules/UNET/UNETManager.h"), NativeHeader("Modules/UNET/UNetTypes.h"), NativeHeader("Modules/UNET/UNETConfiguration.h")]
	public sealed class NetworkTransport
	{
		private static int s_nextSceneId = 1;

		public static bool IsStarted
		{
			get
			{
				return NetworkTransport.IsStartedInternal();
			}
		}

		public static bool DoesEndPointUsePlatformProtocols(EndPoint endPoint)
		{
			bool flag = endPoint.GetType().FullName == "UnityEngine.PS4.SceEndPoint";
			bool result;
			if (flag)
			{
				SocketAddress socketAddress = endPoint.Serialize();
				bool flag2 = socketAddress[8] != 0 || socketAddress[9] > 0;
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static int ConnectEndPoint(int hostId, EndPoint endPoint, int exceptionConnectionId, out byte error)
		{
			error = 0;
			byte[] array = new byte[]
			{
				95,
				36,
				19,
				246
			};
			bool flag = endPoint == null;
			if (flag)
			{
				throw new NullReferenceException("Null EndPoint provided");
			}
			bool flag2 = endPoint.GetType().FullName != "UnityEngine.XboxOne.XboxOneEndPoint" && endPoint.GetType().FullName != "UnityEngine.PS4.SceEndPoint" && endPoint.GetType().FullName != "UnityEngine.PSVita.SceEndPoint";
			if (flag2)
			{
				throw new ArgumentException("Endpoint of type XboxOneEndPoint or SceEndPoint  required");
			}
			bool flag3 = endPoint.GetType().FullName == "UnityEngine.XboxOne.XboxOneEndPoint";
			int result;
			if (flag3)
			{
				bool flag4 = endPoint.AddressFamily != AddressFamily.InterNetworkV6;
				if (flag4)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid family");
				}
				SocketAddress socketAddress = endPoint.Serialize();
				bool flag5 = socketAddress.Size != 14;
				if (flag5)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid size");
				}
				bool flag6 = socketAddress[0] != 0 || socketAddress[1] > 0;
				if (flag6)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid family signature");
				}
				bool flag7 = socketAddress[2] != array[0] || socketAddress[3] != array[1] || socketAddress[4] != array[2] || socketAddress[5] != array[3];
				if (flag7)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid signature");
				}
				byte[] array2 = new byte[8];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = socketAddress[6 + i];
				}
				IntPtr intPtr = new IntPtr(BitConverter.ToInt64(array2, 0));
				bool flag8 = intPtr == IntPtr.Zero;
				if (flag8)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid SOCKET_STORAGE pointer");
				}
				byte[] array3 = new byte[2];
				Marshal.Copy(intPtr, array3, 0, array3.Length);
				AddressFamily addressFamily = (AddressFamily)(((int)array3[1] << 8) + (int)array3[0]);
				bool flag9 = addressFamily != AddressFamily.InterNetworkV6;
				if (flag9)
				{
					throw new ArgumentException("XboxOneEndPoint has corrupt or invalid SOCKET_STORAGE pointer");
				}
				result = NetworkTransport.Internal_ConnectEndPoint(hostId, array2, 128, exceptionConnectionId, out error);
			}
			else
			{
				SocketAddress socketAddress2 = endPoint.Serialize();
				bool flag10 = socketAddress2.Size != 16;
				if (flag10)
				{
					throw new ArgumentException("EndPoint has an invalid size");
				}
				bool flag11 = (int)socketAddress2[0] != socketAddress2.Size;
				if (flag11)
				{
					throw new ArgumentException("EndPoint has an invalid size value");
				}
				bool flag12 = socketAddress2[1] != 2;
				if (flag12)
				{
					throw new ArgumentException("EndPoint has an invalid family value");
				}
				byte[] array4 = new byte[16];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = socketAddress2[j];
				}
				int num = NetworkTransport.Internal_ConnectEndPoint(hostId, array4, 16, exceptionConnectionId, out error);
				result = num;
			}
			return result;
		}

		private NetworkTransport()
		{
		}

		public static void Init()
		{
			NetworkTransport.InitializeClass();
		}

		public static void Init(GlobalConfig config)
		{
			bool flag = config.NetworkEventAvailable != null;
			if (flag)
			{
				NetworkTransport.SetNetworkEventAvailableCallback(config.NetworkEventAvailable);
			}
			bool flag2 = config.ConnectionReadyForSend != null;
			if (flag2)
			{
				NetworkTransport.SetConnectionReadyForSendCallback(config.ConnectionReadyForSend);
			}
			NetworkTransport.InitializeClassWithConfig(new GlobalConfigInternal(config));
		}

		[FreeFunction("UNETManager::InitializeClass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeClass();

		[FreeFunction("UNETManager::InitializeClassWithConfig")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeClassWithConfig(GlobalConfigInternal config);

		public static void Shutdown()
		{
			NetworkTransport.Cleanup();
		}

		[Obsolete("This function has been deprecated. Use AssetDatabase utilities instead.")]
		public static string GetAssetId(GameObject go)
		{
			return "";
		}

		public static void AddSceneId(int id)
		{
			bool flag = id > NetworkTransport.s_nextSceneId;
			if (flag)
			{
				NetworkTransport.s_nextSceneId = id + 1;
			}
		}

		public static int GetNextSceneId()
		{
			return NetworkTransport.s_nextSceneId++;
		}

		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, int port, string ip)
		{
			bool flag = topology == null;
			if (flag)
			{
				throw new NullReferenceException("topology is not defined");
			}
			NetworkTransport.CheckTopology(topology);
			return NetworkTransport.AddHostInternal(new HostTopologyInternal(topology), ip, port, minTimeout, maxTimeout);
		}

		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, int port)
		{
			return NetworkTransport.AddHostWithSimulator(topology, minTimeout, maxTimeout, port, null);
		}

		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout)
		{
			return NetworkTransport.AddHostWithSimulator(topology, minTimeout, maxTimeout, 0, null);
		}

		public static int AddHost(HostTopology topology, int port, string ip)
		{
			return NetworkTransport.AddHostWithSimulator(topology, 0, 0, port, ip);
		}

		public static int AddHost(HostTopology topology, int port)
		{
			return NetworkTransport.AddHost(topology, port, null);
		}

		public static int AddHost(HostTopology topology)
		{
			return NetworkTransport.AddHost(topology, 0, null);
		}

		[FreeFunction("UNETManager::Get()->AddHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddHostInternal(HostTopologyInternal topologyInt, string ip, int port, int minTimeout, int maxTimeout);

		public static int AddWebsocketHost(HostTopology topology, int port, string ip)
		{
			bool flag = port != 0;
			if (flag)
			{
				bool flag2 = NetworkTransport.IsPortOpen(ip, port);
				if (flag2)
				{
					throw new InvalidOperationException("Cannot open web socket on port " + port.ToString() + " It has been already occupied.");
				}
			}
			bool flag3 = topology == null;
			if (flag3)
			{
				throw new NullReferenceException("topology is not defined");
			}
			NetworkTransport.CheckTopology(topology);
			return NetworkTransport.AddWsHostInternal(new HostTopologyInternal(topology), ip, port);
		}

		public static int AddWebsocketHost(HostTopology topology, int port)
		{
			return NetworkTransport.AddWebsocketHost(topology, port, null);
		}

		[FreeFunction("UNETManager::Get()->AddWsHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddWsHostInternal(HostTopologyInternal topologyInt, string ip, int port);

		private static bool IsPortOpen(string ip, int port)
		{
			TimeSpan timeout = TimeSpan.FromMilliseconds(500.0);
			string host = (ip == null) ? "127.0.0.1" : ip;
			bool result;
			try
			{
				using (TcpClient tcpClient = new TcpClient())
				{
					IAsyncResult asyncResult = tcpClient.BeginConnect(host, port, null, null);
					bool flag = asyncResult.AsyncWaitHandle.WaitOne(timeout);
					bool flag2 = !flag;
					if (flag2)
					{
						result = false;
						return result;
					}
					tcpClient.EndConnect(asyncResult);
				}
			}
			catch
			{
				result = false;
				return result;
			}
			result = true;
			return result;
		}

		public static void ConnectAsNetworkHost(int hostId, string address, int port, NetworkID network, SourceID source, NodeID node, out byte error)
		{
			NetworkTransport.ConnectAsNetworkHostInternal(hostId, address, port, (ulong)network, (ulong)source, (ushort)node, out error);
		}

		[FreeFunction("UNETManager::Get()->ConnectAsNetworkHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConnectAsNetworkHostInternal(int hostId, string address, int port, ulong network, ulong source, ushort node, out byte error);

		[FreeFunction("UNETManager::Get()->DisconnectNetworkHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisconnectNetworkHost(int hostId, out byte error);

		public static NetworkEventType ReceiveRelayEventFromHost(int hostId, out byte error)
		{
			return (NetworkEventType)NetworkTransport.ReceiveRelayEventFromHostInternal(hostId, out error);
		}

		[FreeFunction("UNETManager::Get()->PopRelayHostData", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReceiveRelayEventFromHostInternal(int hostId, out byte error);

		public static int ConnectToNetworkPeer(int hostId, string address, int port, int exceptionConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, int bytesPerSec, float bucketSizeFactor, out byte error)
		{
			return NetworkTransport.ConnectToNetworkPeerInternal(hostId, address, port, exceptionConnectionId, relaySlotId, (ulong)network, (ulong)source, (ushort)node, bytesPerSec, bucketSizeFactor, out error);
		}

		public static int ConnectToNetworkPeer(int hostId, string address, int port, int exceptionConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, out byte error)
		{
			return NetworkTransport.ConnectToNetworkPeer(hostId, address, port, exceptionConnectionId, relaySlotId, network, source, node, 0, 0f, out error);
		}

		[FreeFunction("UNETManager::Get()->ConnectToNetworkPeer", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ConnectToNetworkPeerInternal(int hostId, string address, int port, int exceptionConnectionId, int relaySlotId, ulong network, ulong source, ushort node, int bytesPerSec, float bucketSizeFactor, out byte error);

		[Obsolete("GetCurrentIncomingMessageAmount has been deprecated.")]
		public static int GetCurrentIncomingMessageAmount()
		{
			return 0;
		}

		[Obsolete("GetCurrentOutgoingMessageAmount has been deprecated.")]
		public static int GetCurrentOutgoingMessageAmount()
		{
			return 0;
		}

		[FreeFunction("UNETManager::Get()->GetIncomingMessageQueueSize", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingMessageQueueSize(int hostId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingMessageQueueSize", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingMessageQueueSize(int hostId, out byte error);

		[FreeFunction("UNETManager::Get()->GetCurrentRTT", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentRTT(int hostId, int connectionId, out byte error);

		[Obsolete("GetCurrentRtt() has been deprecated.")]
		public static int GetCurrentRtt(int hostId, int connectionId, out byte error)
		{
			return NetworkTransport.GetCurrentRTT(hostId, connectionId, out error);
		}

		[FreeFunction("UNETManager::Get()->GetIncomingPacketLossCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingPacketLossCount(int hostId, int connectionId, out byte error);

		[Obsolete("GetNetworkLostPacketNum() has been deprecated.")]
		public static int GetNetworkLostPacketNum(int hostId, int connectionId, out byte error)
		{
			return NetworkTransport.GetIncomingPacketLossCount(hostId, connectionId, out error);
		}

		[FreeFunction("UNETManager::Get()->GetIncomingPacketCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingPacketCount(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingPacketNetworkLossPercent", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketNetworkLossPercent(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingPacketOverflowLossPercent", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketOverflowLossPercent(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetMaxAllowedBandwidth", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetMaxAllowedBandwidth(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetAckBufferCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAckBufferCount(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetIncomingPacketDropCountForAllHosts", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingPacketDropCountForAllHosts();

		[FreeFunction("UNETManager::Get()->GetIncomingPacketCountForAllHosts", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingPacketCountForAllHosts();

		[FreeFunction("UNETManager::Get()->GetOutgoingPacketCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketCount();

		[FreeFunction("UNETManager::Get()->GetOutgoingPacketCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketCountForHost(int hostId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingPacketCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketCountForConnection(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingMessageCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingMessageCount();

		[FreeFunction("UNETManager::Get()->GetOutgoingMessageCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingMessageCountForHost(int hostId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingMessageCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingMessageCountForConnection(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingUserBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingUserBytesCount();

		[FreeFunction("UNETManager::Get()->GetOutgoingUserBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingUserBytesCountForHost(int hostId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingUserBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingUserBytesCountForConnection(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingSystemBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingSystemBytesCount();

		[FreeFunction("UNETManager::Get()->GetOutgoingSystemBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingSystemBytesCountForHost(int hostId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingSystemBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingSystemBytesCountForConnection(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingFullBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingFullBytesCount();

		[FreeFunction("UNETManager::Get()->GetOutgoingFullBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingFullBytesCountForHost(int hostId, out byte error);

		[FreeFunction("UNETManager::Get()->GetOutgoingFullBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingFullBytesCountForConnection(int hostId, int connectionId, out byte error);

		[Obsolete("GetPacketSentRate has been deprecated.")]
		public static int GetPacketSentRate(int hostId, int connectionId, out byte error)
		{
			error = 0;
			return 0;
		}

		[Obsolete("GetPacketReceivedRate has been deprecated.")]
		public static int GetPacketReceivedRate(int hostId, int connectionId, out byte error)
		{
			error = 0;
			return 0;
		}

		[Obsolete("GetRemotePacketReceivedRate has been deprecated.")]
		public static int GetRemotePacketReceivedRate(int hostId, int connectionId, out byte error)
		{
			error = 0;
			return 0;
		}

		[Obsolete("GetNetIOTimeuS has been deprecated.")]
		public static int GetNetIOTimeuS()
		{
			return 0;
		}

		[FreeFunction("UNETManager::Get()->GetConnectionInfo", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetConnectionInfo(int hostId, int connectionId, out int port, out ulong network, out ushort dstNode, out byte error);

		public static void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
		{
			ulong num;
			ushort num2;
			address = NetworkTransport.GetConnectionInfo(hostId, connectionId, out port, out num, out num2, out error);
			network = (NetworkID)num;
			dstNode = (NodeID)num2;
		}

		[FreeFunction("UNETManager::Get()->GetNetworkTimestamp", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNetworkTimestamp();

		[FreeFunction("UNETManager::Get()->GetRemoteDelayTimeMS", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetRemoteDelayTimeMS(int hostId, int connectionId, int remoteTime, out byte error);

		public static bool StartSendMulticast(int hostId, int channelId, byte[] buffer, int size, out byte error)
		{
			return NetworkTransport.StartSendMulticastInternal(hostId, channelId, buffer, size, out error);
		}

		[FreeFunction("UNETManager::Get()->StartSendMulticast", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool StartSendMulticastInternal(int hostId, int channelId, [Out] byte[] buffer, int size, out byte error);

		[FreeFunction("UNETManager::Get()->SendMulticast", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SendMulticast(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->FinishSendMulticast", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FinishSendMulticast(int hostId, out byte error);

		[FreeFunction("UNETManager::Get()->GetMaxPacketSize", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxPacketSize();

		[FreeFunction("UNETManager::Get()->RemoveHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RemoveHost(int hostId);

		[FreeFunction("UNETManager::IsStarted")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsStartedInternal();

		[FreeFunction("UNETManager::Get()->Connect", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Connect(int hostId, string address, int port, int exeptionConnectionId, out byte error);

		[FreeFunction("UNETManager::Get()->ConnectWithSimulator", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ConnectWithSimulatorInternal(int hostId, string address, int port, int exeptionConnectionId, out byte error, ConnectionSimulatorConfigInternal conf);

		public static int ConnectWithSimulator(int hostId, string address, int port, int exeptionConnectionId, out byte error, ConnectionSimulatorConfig conf)
		{
			return NetworkTransport.ConnectWithSimulatorInternal(hostId, address, port, exeptionConnectionId, out error, new ConnectionSimulatorConfigInternal(conf));
		}

		[FreeFunction("UNETManager::Get()->Disconnect", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Disconnect(int hostId, int connectionId, out byte error);

		[FreeFunction("UNETManager::Get()->ConnectSockAddr", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_ConnectEndPoint(int hostId, [Out] byte[] sockAddrStorage, int sockAddrStorageLen, int exceptionConnectionId, out byte error);

		public static bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int size, out byte error)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new NullReferenceException("send buffer is not initialized");
			}
			return NetworkTransport.SendWrapper(hostId, connectionId, channelId, buffer, size, out error);
		}

		[FreeFunction("UNETManager::Get()->Send", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SendWrapper(int hostId, int connectionId, int channelId, [Out] byte[] buffer, int size, out byte error);

		public static bool QueueMessageForSending(int hostId, int connectionId, int channelId, byte[] buffer, int size, out byte error)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new NullReferenceException("send buffer is not initialized");
			}
			return NetworkTransport.QueueMessageForSendingWrapper(hostId, connectionId, channelId, buffer, size, out error);
		}

		[FreeFunction("UNETManager::Get()->QueueMessageForSending", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool QueueMessageForSendingWrapper(int hostId, int connectionId, int channelId, [Out] byte[] buffer, int size, out byte error);

		[FreeFunction("UNETManager::Get()->SendQueuedMessages", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SendQueuedMessages(int hostId, int connectionId, out byte error);

		public static NetworkEventType Receive(out int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
		{
			return (NetworkEventType)NetworkTransport.PopData(out hostId, out connectionId, out channelId, buffer, bufferSize, out receivedSize, out error);
		}

		[FreeFunction("UNETManager::Get()->PopData", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopData(out int hostId, out int connectionId, out int channelId, [Out] byte[] buffer, int bufferSize, out int receivedSize, out byte error);

		public static NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
		{
			return (NetworkEventType)NetworkTransport.PopDataFromHost(hostId, out connectionId, out channelId, buffer, bufferSize, out receivedSize, out error);
		}

		[FreeFunction("UNETManager::Get()->PopDataFromHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopDataFromHost(int hostId, out int connectionId, out int channelId, [Out] byte[] buffer, int bufferSize, out int receivedSize, out byte error);

		[FreeFunction("UNETManager::Get()->SetPacketStat", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPacketStat(int direction, int packetStatId, int numMsgs, int numBytes);

		[FreeFunction("UNETManager::SetNetworkEventAvailableCallback"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNetworkEventAvailableCallback(Action<int> callback);

		[FreeFunction("UNETManager::Cleanup")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cleanup();

		[FreeFunction("UNETManager::SetConnectionReadyForSendCallback"), NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConnectionReadyForSendCallback(Action<int, int> callback);

		[FreeFunction("UNETManager::Get()->NotifyWhenConnectionReadyForSend", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool NotifyWhenConnectionReadyForSend(int hostId, int connectionId, int notificationLevel, out byte error);

		[FreeFunction("UNETManager::Get()->GetHostPort", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetHostPort(int hostId);

		[FreeFunction("UNETManager::Get()->StartBroadcastDiscoveryWithData", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool StartBroadcastDiscoveryWithData(int hostId, int broadcastPort, int key, int version, int subversion, [Out] byte[] buffer, int size, int timeout, out byte error);

		[FreeFunction("UNETManager::Get()->StartBroadcastDiscoveryWithoutData", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool StartBroadcastDiscoveryWithoutData(int hostId, int broadcastPort, int key, int version, int subversion, int timeout, out byte error);

		public static bool StartBroadcastDiscovery(int hostId, int broadcastPort, int key, int version, int subversion, byte[] buffer, int size, int timeout, out byte error)
		{
			bool flag = buffer != null;
			if (flag)
			{
				bool flag2 = buffer.Length < size;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Size: " + size.ToString() + " > buffer.Length " + buffer.Length.ToString());
				}
				bool flag3 = size == 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("Size is zero while buffer exists, please pass null and 0 as buffer and size parameters");
				}
			}
			bool flag4 = buffer == null;
			bool result;
			if (flag4)
			{
				result = NetworkTransport.StartBroadcastDiscoveryWithoutData(hostId, broadcastPort, key, version, subversion, timeout, out error);
			}
			else
			{
				result = NetworkTransport.StartBroadcastDiscoveryWithData(hostId, broadcastPort, key, version, subversion, buffer, size, timeout, out error);
			}
			return result;
		}

		[FreeFunction("UNETManager::Get()->StopBroadcastDiscovery", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopBroadcastDiscovery();

		[FreeFunction("UNETManager::Get()->IsBroadcastDiscoveryRunning", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBroadcastDiscoveryRunning();

		[FreeFunction("UNETManager::Get()->SetBroadcastCredentials", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBroadcastCredentials(int hostId, int key, int version, int subversion, out byte error);

		[FreeFunction("UNETManager::Get()->GetBroadcastConnectionInfoInternal", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetBroadcastConnectionInfo(int hostId, out int port, out byte error);

		public static void GetBroadcastConnectionInfo(int hostId, out string address, out int port, out byte error)
		{
			address = NetworkTransport.GetBroadcastConnectionInfo(hostId, out port, out error);
		}

		public static void GetBroadcastConnectionMessage(int hostId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
		{
			NetworkTransport.GetBroadcastConnectionMessageInternal(hostId, buffer, bufferSize, out receivedSize, out error);
		}

		[FreeFunction("UNETManager::SetMulticastLock")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMulticastLock(bool enabled);

		[FreeFunction("UNETManager::Get()->GetBroadcastConnectionMessage", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBroadcastConnectionMessageInternal(int hostId, [Out] byte[] buffer, int bufferSize, out int receivedSize, out byte error);

		private static void CheckTopology(HostTopology topology)
		{
			int maxPacketSize = NetworkTransport.GetMaxPacketSize();
			bool flag = (int)topology.DefaultConfig.PacketSize > maxPacketSize;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("Default config: packet size should be less than packet size defined in global config: " + maxPacketSize.ToString());
			}
			for (int i = 0; i < topology.SpecialConnectionConfigs.Count; i++)
			{
				bool flag2 = (int)topology.SpecialConnectionConfigs[i].PacketSize > maxPacketSize;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Special config " + i.ToString() + ": packet size should be less than packet size defined in global config: " + maxPacketSize.ToString());
				}
			}
		}

		[FreeFunction("UNETManager::Get()->LoadEncryptionLibrary", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadEncryptionLibraryInternal(string libraryName);

		public static bool LoadEncryptionLibrary(string libraryName)
		{
			return NetworkTransport.LoadEncryptionLibraryInternal(libraryName);
		}

		[FreeFunction("UNETManager::Get()->UnloadEncryptionLibrary", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnloadEncryptionLibraryInternal();

		public static void UnloadEncryptionLibrary()
		{
			NetworkTransport.UnloadEncryptionLibraryInternal();
		}

		[FreeFunction("UNETManager::Get()->IsEncryptionActive", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEncryptionActiveInternal();

		public static bool IsEncryptionActive()
		{
			return NetworkTransport.IsEncryptionActiveInternal();
		}

		[FreeFunction("UNETManager::Get()->GetEncryptionSafeMaxPacketSize", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern short GetEncryptionSafeMaxPacketSizeInternal(short maxPacketSize);

		public static short GetEncryptionSafeMaxPacketSize(short maxPacketSize)
		{
			return NetworkTransport.GetEncryptionSafeMaxPacketSizeInternal(maxPacketSize);
		}
	}
}
