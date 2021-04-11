using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("iPhoneNetworkReachability enumeration is deprecated. Please use NetworkReachability instead (UnityUpgradable) -> NetworkReachability", true)]
	public enum iPhoneNetworkReachability
	{
		NotReachable,
		ReachableViaCarrierDataNetwork,
		[Obsolete]
		ReachableViaWiFiNetwork
	}
}
