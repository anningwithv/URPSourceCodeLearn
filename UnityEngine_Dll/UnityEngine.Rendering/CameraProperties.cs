using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct CameraProperties : IEquatable<CameraProperties>
	{
		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <m_ShadowCullPlanes>e__FixedBuffer
		{
			public byte FixedElementField;
		}

		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <m_CameraCullPlanes>e__FixedBuffer
		{
			public byte FixedElementField;
		}

		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <layerCullDistances>e__FixedBuffer
		{
			public float FixedElementField;
		}

		private const int k_NumLayers = 32;

		private Rect screenRect;

		private Vector3 viewDir;

		private float projectionNear;

		private float projectionFar;

		private float cameraNear;

		private float cameraFar;

		private float cameraAspect;

		private Matrix4x4 cameraToWorld;

		private Matrix4x4 actualWorldToClip;

		private Matrix4x4 cameraClipToWorld;

		private Matrix4x4 cameraWorldToClip;

		private Matrix4x4 implicitProjection;

		private Matrix4x4 stereoWorldToClipLeft;

		private Matrix4x4 stereoWorldToClipRight;

		private Matrix4x4 worldToCamera;

		private Vector3 up;

		private Vector3 right;

		private Vector3 transformDirection;

		private Vector3 cameraEuler;

		private Vector3 velocity;

		private float farPlaneWorldSpaceLength;

		private uint rendererCount;

		private const int k_PlaneCount = 6;

		[FixedBuffer(typeof(byte), 96)]
		internal CameraProperties.<m_ShadowCullPlanes>e__FixedBuffer m_ShadowCullPlanes;

		[FixedBuffer(typeof(byte), 96)]
		internal CameraProperties.<m_CameraCullPlanes>e__FixedBuffer m_CameraCullPlanes;

		private float baseFarDistance;

		private Vector3 shadowCullCenter;

		[FixedBuffer(typeof(float), 32)]
		internal CameraProperties.<layerCullDistances>e__FixedBuffer layerCullDistances;

		private int layerCullSpherical;

		private CoreCameraValues coreCameraValues;

		private uint cameraType;

		private int projectionIsOblique;

		private int isImplicitProjectionMatrix;

		public unsafe Plane GetShadowCullingPlane(int index)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			byte* ptr = &this.m_ShadowCullPlanes.FixedElementField;
			Plane* ptr2 = (Plane*)ptr;
			return ptr2[index];
		}

		public unsafe void SetShadowCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_ShadowCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		public unsafe Plane GetCameraCullingPlane(int index)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			byte* ptr = &this.m_CameraCullPlanes.FixedElementField;
			Plane* ptr2 = (Plane*)ptr;
			return ptr2[index];
		}

		public unsafe void SetCameraCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_CameraCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		public unsafe bool Equals(CameraProperties other)
		{
			bool result;
			for (int i = 0; i < 6; i++)
			{
				bool flag = !this.GetShadowCullingPlane(i).Equals(other.GetShadowCullingPlane(i));
				if (flag)
				{
					result = false;
					return result;
				}
			}
			for (int j = 0; j < 6; j++)
			{
				bool flag2 = !this.GetCameraCullingPlane(j).Equals(other.GetCameraCullingPlane(j));
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			fixed (float* ptr = &this.layerCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				for (int k = 0; k < 32; k++)
				{
					bool flag3 = ptr2[k] != *(ref other.layerCullDistances.FixedElementField + (IntPtr)k * 4);
					if (flag3)
					{
						result = false;
						return result;
					}
				}
			}
			result = (this.screenRect.Equals(other.screenRect) && this.viewDir.Equals(other.viewDir) && this.projectionNear.Equals(other.projectionNear) && this.projectionFar.Equals(other.projectionFar) && this.cameraNear.Equals(other.cameraNear) && this.cameraFar.Equals(other.cameraFar) && this.cameraAspect.Equals(other.cameraAspect) && this.cameraToWorld.Equals(other.cameraToWorld) && this.actualWorldToClip.Equals(other.actualWorldToClip) && this.cameraClipToWorld.Equals(other.cameraClipToWorld) && this.cameraWorldToClip.Equals(other.cameraWorldToClip) && this.implicitProjection.Equals(other.implicitProjection) && this.stereoWorldToClipLeft.Equals(other.stereoWorldToClipLeft) && this.stereoWorldToClipRight.Equals(other.stereoWorldToClipRight) && this.worldToCamera.Equals(other.worldToCamera) && this.up.Equals(other.up) && this.right.Equals(other.right) && this.transformDirection.Equals(other.transformDirection) && this.cameraEuler.Equals(other.cameraEuler) && this.velocity.Equals(other.velocity) && this.farPlaneWorldSpaceLength.Equals(other.farPlaneWorldSpaceLength) && this.rendererCount == other.rendererCount && this.baseFarDistance.Equals(other.baseFarDistance) && this.shadowCullCenter.Equals(other.shadowCullCenter) && this.layerCullSpherical == other.layerCullSpherical && this.coreCameraValues.Equals(other.coreCameraValues) && this.cameraType == other.cameraType && this.projectionIsOblique == other.projectionIsOblique && this.isImplicitProjectionMatrix == other.isImplicitProjectionMatrix);
			return result;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CameraProperties && this.Equals((CameraProperties)obj);
		}

		public unsafe override int GetHashCode()
		{
			int num = this.screenRect.GetHashCode();
			num = (num * 397 ^ this.viewDir.GetHashCode());
			num = (num * 397 ^ this.projectionNear.GetHashCode());
			num = (num * 397 ^ this.projectionFar.GetHashCode());
			num = (num * 397 ^ this.cameraNear.GetHashCode());
			num = (num * 397 ^ this.cameraFar.GetHashCode());
			num = (num * 397 ^ this.cameraAspect.GetHashCode());
			num = (num * 397 ^ this.cameraToWorld.GetHashCode());
			num = (num * 397 ^ this.actualWorldToClip.GetHashCode());
			num = (num * 397 ^ this.cameraClipToWorld.GetHashCode());
			num = (num * 397 ^ this.cameraWorldToClip.GetHashCode());
			num = (num * 397 ^ this.implicitProjection.GetHashCode());
			num = (num * 397 ^ this.stereoWorldToClipLeft.GetHashCode());
			num = (num * 397 ^ this.stereoWorldToClipRight.GetHashCode());
			num = (num * 397 ^ this.worldToCamera.GetHashCode());
			num = (num * 397 ^ this.up.GetHashCode());
			num = (num * 397 ^ this.right.GetHashCode());
			num = (num * 397 ^ this.transformDirection.GetHashCode());
			num = (num * 397 ^ this.cameraEuler.GetHashCode());
			num = (num * 397 ^ this.velocity.GetHashCode());
			num = (num * 397 ^ this.farPlaneWorldSpaceLength.GetHashCode());
			num = (num * 397 ^ (int)this.rendererCount);
			for (int i = 0; i < 6; i++)
			{
				num = (num * 397 ^ this.GetShadowCullingPlane(i).GetHashCode());
			}
			for (int j = 0; j < 6; j++)
			{
				num = (num * 397 ^ this.GetCameraCullingPlane(j).GetHashCode());
			}
			num = (num * 397 ^ this.baseFarDistance.GetHashCode());
			num = (num * 397 ^ this.shadowCullCenter.GetHashCode());
			fixed (float* ptr = &this.layerCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				for (int k = 0; k < 32; k++)
				{
					num = (num * 397 ^ ptr2[k].GetHashCode());
				}
			}
			num = (num * 397 ^ this.layerCullSpherical);
			num = (num * 397 ^ this.coreCameraValues.GetHashCode());
			num = (num * 397 ^ (int)this.cameraType);
			num = (num * 397 ^ this.projectionIsOblique);
			return num * 397 ^ this.isImplicitProjectionMatrix;
		}

		public static bool operator ==(CameraProperties left, CameraProperties right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(CameraProperties left, CameraProperties right)
		{
			return !left.Equals(right);
		}
	}
}
