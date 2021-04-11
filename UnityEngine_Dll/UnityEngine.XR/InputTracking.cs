using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR"), NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTrackingFacade.h"), StaticAccessor("XRInputTrackingFacade::Get()", StaticAccessorType.Dot), RequiredByNativeCode]
	public static class InputTracking
	{
		private enum TrackingStateEventType
		{
			NodeAdded,
			NodeRemoved,
			TrackingAcquired,
			TrackingLost
		}

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<XRNodeState> trackingAcquired;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<XRNodeState> trackingLost;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<XRNodeState> nodeAdded;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public static event Action<XRNodeState> nodeRemoved;

		[Obsolete("This API is obsolete, and should no longer be used. Please use the TrackedPoseDriver in the Legacy Input Helpers package for controlling a camera in XR."), NativeConditional("ENABLE_VR")]
		public static extern bool disablePositionalTracking
		{
			[NativeName("GetPositionalTrackingDisabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetPositionalTrackingDisabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		private static void InvokeTrackingEvent(InputTracking.TrackingStateEventType eventType, XRNode nodeType, long uniqueID, bool tracked)
		{
			XRNodeState obj = default(XRNodeState);
			obj.uniqueID = (ulong)uniqueID;
			obj.nodeType = nodeType;
			obj.tracked = tracked;
			Action<XRNodeState> action;
			switch (eventType)
			{
			case InputTracking.TrackingStateEventType.NodeAdded:
				action = InputTracking.nodeAdded;
				break;
			case InputTracking.TrackingStateEventType.NodeRemoved:
				action = InputTracking.nodeRemoved;
				break;
			case InputTracking.TrackingStateEventType.TrackingAcquired:
				action = InputTracking.trackingAcquired;
				break;
			case InputTracking.TrackingStateEventType.TrackingLost:
				action = InputTracking.trackingLost;
				break;
			default:
				throw new ArgumentException("TrackingEventHandler - Invalid EventType: " + eventType.ToString());
			}
			bool flag = action != null;
			if (flag)
			{
				action(obj);
			}
		}

		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.TryGetFeatureValue with the CommonUsages.devicePosition usage instead."), NativeConditional("ENABLE_VR", "Vector3f::zero")]
		public static Vector3 GetLocalPosition(XRNode node)
		{
			Vector3 result;
			InputTracking.GetLocalPosition_Injected(node, out result);
			return result;
		}

		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.TryGetFeatureValue with the CommonUsages.deviceRotation usage instead."), NativeConditional("ENABLE_VR", "Quaternionf::identity()")]
		public static Quaternion GetLocalRotation(XRNode node)
		{
			Quaternion result;
			InputTracking.GetLocalRotation_Injected(node, out result);
			return result;
		}

		[Obsolete("This API is obsolete, and should no longer be used. Please use XRInputSubsystem.TryRecenter() instead."), NativeConditional("ENABLE_VR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Recenter();

		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.name with the device associated with that tracking data instead."), NativeConditional("ENABLE_VR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetNodeName(ulong uniqueId);

		public static void GetNodeStates(List<XRNodeState> nodeStates)
		{
			bool flag = nodeStates == null;
			if (flag)
			{
				throw new ArgumentNullException("nodeStates");
			}
			nodeStates.Clear();
			InputTracking.GetNodeStates_Internal(nodeStates);
		}

		[NativeConditional("ENABLE_VR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNodeStates_Internal([NotNull("ArgumentNullException")] List<XRNodeState> nodeStates);

		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h"), StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetDeviceIdAtXRNode(XRNode node);

		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h"), StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetDeviceIdsAtXRNode_Internal(XRNode node, [NotNull("ArgumentNullException")] List<ulong> deviceIds);

		static InputTracking()
		{
			// Note: this type is marked as 'beforefieldinit'.
			InputTracking.trackingAcquired = null;
			InputTracking.trackingLost = null;
			InputTracking.nodeAdded = null;
			InputTracking.nodeRemoved = null;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPosition_Injected(XRNode node, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotation_Injected(XRNode node, out Quaternion ret);
	}
}
