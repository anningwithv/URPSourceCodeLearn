using System;

namespace UnityEngine.XR
{
	public static class CommonUsages
	{
		public static InputFeatureUsage<bool> isTracked = new InputFeatureUsage<bool>("IsTracked");

		public static InputFeatureUsage<bool> primaryButton = new InputFeatureUsage<bool>("PrimaryButton");

		public static InputFeatureUsage<bool> primaryTouch = new InputFeatureUsage<bool>("PrimaryTouch");

		public static InputFeatureUsage<bool> secondaryButton = new InputFeatureUsage<bool>("SecondaryButton");

		public static InputFeatureUsage<bool> secondaryTouch = new InputFeatureUsage<bool>("SecondaryTouch");

		public static InputFeatureUsage<bool> gripButton = new InputFeatureUsage<bool>("GripButton");

		public static InputFeatureUsage<bool> triggerButton = new InputFeatureUsage<bool>("TriggerButton");

		public static InputFeatureUsage<bool> menuButton = new InputFeatureUsage<bool>("MenuButton");

		public static InputFeatureUsage<bool> primary2DAxisClick = new InputFeatureUsage<bool>("Primary2DAxisClick");

		public static InputFeatureUsage<bool> primary2DAxisTouch = new InputFeatureUsage<bool>("Primary2DAxisTouch");

		public static InputFeatureUsage<bool> secondary2DAxisClick = new InputFeatureUsage<bool>("Secondary2DAxisClick");

		public static InputFeatureUsage<bool> secondary2DAxisTouch = new InputFeatureUsage<bool>("Secondary2DAxisTouch");

		public static InputFeatureUsage<bool> userPresence = new InputFeatureUsage<bool>("UserPresence");

		public static InputFeatureUsage<InputTrackingState> trackingState = new InputFeatureUsage<InputTrackingState>("TrackingState");

		public static InputFeatureUsage<float> batteryLevel = new InputFeatureUsage<float>("BatteryLevel");

		public static InputFeatureUsage<float> trigger = new InputFeatureUsage<float>("Trigger");

		public static InputFeatureUsage<float> grip = new InputFeatureUsage<float>("Grip");

		public static InputFeatureUsage<Vector2> primary2DAxis = new InputFeatureUsage<Vector2>("Primary2DAxis");

		public static InputFeatureUsage<Vector2> secondary2DAxis = new InputFeatureUsage<Vector2>("Secondary2DAxis");

		public static InputFeatureUsage<Vector3> devicePosition = new InputFeatureUsage<Vector3>("DevicePosition");

		public static InputFeatureUsage<Vector3> leftEyePosition = new InputFeatureUsage<Vector3>("LeftEyePosition");

		public static InputFeatureUsage<Vector3> rightEyePosition = new InputFeatureUsage<Vector3>("RightEyePosition");

		public static InputFeatureUsage<Vector3> centerEyePosition = new InputFeatureUsage<Vector3>("CenterEyePosition");

		public static InputFeatureUsage<Vector3> colorCameraPosition = new InputFeatureUsage<Vector3>("CameraPosition");

		public static InputFeatureUsage<Vector3> deviceVelocity = new InputFeatureUsage<Vector3>("DeviceVelocity");

		public static InputFeatureUsage<Vector3> deviceAngularVelocity = new InputFeatureUsage<Vector3>("DeviceAngularVelocity");

		public static InputFeatureUsage<Vector3> leftEyeVelocity = new InputFeatureUsage<Vector3>("LeftEyeVelocity");

		public static InputFeatureUsage<Vector3> leftEyeAngularVelocity = new InputFeatureUsage<Vector3>("LeftEyeAngularVelocity");

		public static InputFeatureUsage<Vector3> rightEyeVelocity = new InputFeatureUsage<Vector3>("RightEyeVelocity");

		public static InputFeatureUsage<Vector3> rightEyeAngularVelocity = new InputFeatureUsage<Vector3>("RightEyeAngularVelocity");

		public static InputFeatureUsage<Vector3> centerEyeVelocity = new InputFeatureUsage<Vector3>("CenterEyeVelocity");

		public static InputFeatureUsage<Vector3> centerEyeAngularVelocity = new InputFeatureUsage<Vector3>("CenterEyeAngularVelocity");

		public static InputFeatureUsage<Vector3> colorCameraVelocity = new InputFeatureUsage<Vector3>("CameraVelocity");

		public static InputFeatureUsage<Vector3> colorCameraAngularVelocity = new InputFeatureUsage<Vector3>("CameraAngularVelocity");

		public static InputFeatureUsage<Vector3> deviceAcceleration = new InputFeatureUsage<Vector3>("DeviceAcceleration");

		public static InputFeatureUsage<Vector3> deviceAngularAcceleration = new InputFeatureUsage<Vector3>("DeviceAngularAcceleration");

		public static InputFeatureUsage<Vector3> leftEyeAcceleration = new InputFeatureUsage<Vector3>("LeftEyeAcceleration");

		public static InputFeatureUsage<Vector3> leftEyeAngularAcceleration = new InputFeatureUsage<Vector3>("LeftEyeAngularAcceleration");

		public static InputFeatureUsage<Vector3> rightEyeAcceleration = new InputFeatureUsage<Vector3>("RightEyeAcceleration");

		public static InputFeatureUsage<Vector3> rightEyeAngularAcceleration = new InputFeatureUsage<Vector3>("RightEyeAngularAcceleration");

		public static InputFeatureUsage<Vector3> centerEyeAcceleration = new InputFeatureUsage<Vector3>("CenterEyeAcceleration");

		public static InputFeatureUsage<Vector3> centerEyeAngularAcceleration = new InputFeatureUsage<Vector3>("CenterEyeAngularAcceleration");

		public static InputFeatureUsage<Vector3> colorCameraAcceleration = new InputFeatureUsage<Vector3>("CameraAcceleration");

		public static InputFeatureUsage<Vector3> colorCameraAngularAcceleration = new InputFeatureUsage<Vector3>("CameraAngularAcceleration");

		public static InputFeatureUsage<Quaternion> deviceRotation = new InputFeatureUsage<Quaternion>("DeviceRotation");

		public static InputFeatureUsage<Quaternion> leftEyeRotation = new InputFeatureUsage<Quaternion>("LeftEyeRotation");

		public static InputFeatureUsage<Quaternion> rightEyeRotation = new InputFeatureUsage<Quaternion>("RightEyeRotation");

		public static InputFeatureUsage<Quaternion> centerEyeRotation = new InputFeatureUsage<Quaternion>("CenterEyeRotation");

		public static InputFeatureUsage<Quaternion> colorCameraRotation = new InputFeatureUsage<Quaternion>("CameraRotation");

		public static InputFeatureUsage<Hand> handData = new InputFeatureUsage<Hand>("HandData");

		public static InputFeatureUsage<Eyes> eyesData = new InputFeatureUsage<Eyes>("EyesData");

		[Obsolete("CommonUsages.dPad is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<Vector2> dPad = new InputFeatureUsage<Vector2>("DPad");

		[Obsolete("CommonUsages.indexFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> indexFinger = new InputFeatureUsage<float>("IndexFinger");

		[Obsolete("CommonUsages.MiddleFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> middleFinger = new InputFeatureUsage<float>("MiddleFinger");

		[Obsolete("CommonUsages.RingFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> ringFinger = new InputFeatureUsage<float>("RingFinger");

		[Obsolete("CommonUsages.PinkyFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> pinkyFinger = new InputFeatureUsage<float>("PinkyFinger");

		[Obsolete("CommonUsages.thumbrest is Oculus only, and is being moved to their package. Please use OculusUsages.thumbrest. These will still function until removed.")]
		public static InputFeatureUsage<bool> thumbrest = new InputFeatureUsage<bool>("Thumbrest");

		[Obsolete("CommonUsages.indexTouch is Oculus only, and is being moved to their package.  Please use OculusUsages.indexTouch. These will still function until removed.")]
		public static InputFeatureUsage<float> indexTouch = new InputFeatureUsage<float>("IndexTouch");

		[Obsolete("CommonUsages.thumbTouch is Oculus only, and is being moved to their package.  Please use OculusUsages.thumbTouch. These will still function until removed.")]
		public static InputFeatureUsage<float> thumbTouch = new InputFeatureUsage<float>("ThumbTouch");
	}
}
