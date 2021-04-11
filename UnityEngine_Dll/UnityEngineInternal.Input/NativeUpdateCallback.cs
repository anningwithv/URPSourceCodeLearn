using System;

namespace UnityEngineInternal.Input
{
	internal unsafe delegate void NativeUpdateCallback(NativeInputUpdateType updateType, NativeInputEventBuffer* buffer);
}
