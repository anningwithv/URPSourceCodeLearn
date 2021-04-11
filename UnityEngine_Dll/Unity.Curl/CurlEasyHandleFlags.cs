using System;

namespace Unity.Curl
{
	[Flags]
	internal enum CurlEasyHandleFlags : uint
	{
		kSendBody = 1u,
		kReceiveHeaders = 2u,
		kReceiveBody = 4u,
		kFollowRedirects = 8u
	}
}
