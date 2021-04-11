using System;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Video
{
	[UsedByNativeCode]
	internal enum VideoError
	{
		NoErr,
		OutOfMemoryErr,
		CantReadFile,
		CantWriteFile,
		BadParams,
		NoData,
		BadPermissions,
		DeviceNotAvailable,
		ResourceNotAvailable,
		NetworkErr
	}
}
