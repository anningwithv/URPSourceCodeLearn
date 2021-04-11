using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_XR"), NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystem.h"), UsedByNativeCode]
	public class XRInputSubsystem : IntegratedSubsystem<XRInputSubsystemDescriptor>
	{
		private List<ulong> m_DeviceIdsCache;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<XRInputSubsystem> trackingOriginUpdated;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<XRInputSubsystem> boundaryChanged;

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern uint GetIndex();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryRecenter();

		public bool TryGetInputDevices(List<InputDevice> devices)
		{
			bool flag = devices == null;
			if (flag)
			{
				throw new ArgumentNullException("devices");
			}
			devices.Clear();
			bool flag2 = this.m_DeviceIdsCache == null;
			if (flag2)
			{
				this.m_DeviceIdsCache = new List<ulong>();
			}
			this.m_DeviceIdsCache.Clear();
			this.TryGetDeviceIds_AsList(this.m_DeviceIdsCache);
			for (int i = 0; i < this.m_DeviceIdsCache.Count; i++)
			{
				devices.Add(new InputDevice(this.m_DeviceIdsCache[i]));
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TrySetTrackingOriginMode(TrackingOriginModeFlags origin);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TrackingOriginModeFlags GetTrackingOriginMode();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TrackingOriginModeFlags GetSupportedTrackingOriginModes();

		public bool TryGetBoundaryPoints(List<Vector3> boundaryPoints)
		{
			bool flag = boundaryPoints == null;
			if (flag)
			{
				throw new ArgumentNullException("boundaryPoints");
			}
			return this.TryGetBoundaryPoints_AsList(boundaryPoints);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool TryGetBoundaryPoints_AsList(List<Vector3> boundaryPoints);

		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeTrackingOriginUpdatedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystemByPtr = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			XRInputSubsystem xRInputSubsystem = integratedSubsystemByPtr as XRInputSubsystem;
			bool flag = xRInputSubsystem != null && xRInputSubsystem.trackingOriginUpdated != null;
			if (flag)
			{
				xRInputSubsystem.trackingOriginUpdated(xRInputSubsystem);
			}
		}

		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeBoundaryChangedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystemByPtr = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			XRInputSubsystem xRInputSubsystem = integratedSubsystemByPtr as XRInputSubsystem;
			bool flag = xRInputSubsystem != null && xRInputSubsystem.boundaryChanged != null;
			if (flag)
			{
				xRInputSubsystem.boundaryChanged(xRInputSubsystem);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void TryGetDeviceIds_AsList(List<ulong> deviceIds);
	}
}
