using System;

namespace UnityEngine
{
	public enum AssetBundleLoadResult
	{
		Success,
		Cancelled,
		NotMatchingCrc,
		FailedCache,
		NotValidAssetBundle,
		NoSerializedData,
		NotCompatible,
		AlreadyLoaded,
		FailedRead,
		FailedDecompression,
		FailedWrite,
		FailedDeleteRecompressionTarget,
		RecompressionTargetIsLoaded,
		RecompressionTargetExistsButNotArchive
	}
}
