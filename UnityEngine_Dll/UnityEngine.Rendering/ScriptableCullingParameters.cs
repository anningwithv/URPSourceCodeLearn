using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct ScriptableCullingParameters : IEquatable<ScriptableCullingParameters>
	{
		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			public byte FixedElementField;
		}

		[CompilerGenerated, UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <m_LayerFarCullDistances>e__FixedBuffer
		{
			public float FixedElementField;
		}

		private int m_IsOrthographic;

		private LODParameters m_LODParameters;

		private const int k_MaximumCullingPlaneCount = 10;

		public static readonly int maximumCullingPlaneCount = 10;

		[FixedBuffer(typeof(byte), 160)]
		internal ScriptableCullingParameters.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		private int m_CullingPlaneCount;

		private uint m_CullingMask;

		private ulong m_SceneMask;

		private const int k_LayerCount = 32;

		public static readonly int layerCount = 32;

		[FixedBuffer(typeof(float), 32)]
		internal ScriptableCullingParameters.<m_LayerFarCullDistances>e__FixedBuffer m_LayerFarCullDistances;

		private int m_LayerCull;

		private Matrix4x4 m_CullingMatrix;

		private Vector3 m_Origin;

		private float m_ShadowDistance;

		private CullingOptions m_CullingOptions;

		private ReflectionProbeSortingCriteria m_ReflectionProbeSortingCriteria;

		private CameraProperties m_CameraProperties;

		private float m_AccurateOcclusionThreshold;

		private int m_MaximumPortalCullingJobs;

		private const int k_CullingJobCountLowerLimit = 1;

		private const int k_CullingJobCountUpperLimit = 16;

		private Matrix4x4 m_StereoViewMatrix;

		private Matrix4x4 m_StereoProjectionMatrix;

		private float m_StereoSeparationDistance;

		private int m_maximumVisibleLights;

		public int maximumVisibleLights
		{
			get
			{
				return this.m_maximumVisibleLights;
			}
			set
			{
				this.m_maximumVisibleLights = value;
			}
		}

		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "value", value, 10));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(this.m_IsOrthographic);
			}
			set
			{
				this.m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		public LODParameters lodParameters
		{
			get
			{
				return this.m_LODParameters;
			}
			set
			{
				this.m_LODParameters = value;
			}
		}

		public uint cullingMask
		{
			get
			{
				return this.m_CullingMask;
			}
			set
			{
				this.m_CullingMask = value;
			}
		}

		public Matrix4x4 cullingMatrix
		{
			get
			{
				return this.m_CullingMatrix;
			}
			set
			{
				this.m_CullingMatrix = value;
			}
		}

		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		public float shadowDistance
		{
			get
			{
				return this.m_ShadowDistance;
			}
			set
			{
				this.m_ShadowDistance = value;
			}
		}

		public CullingOptions cullingOptions
		{
			get
			{
				return this.m_CullingOptions;
			}
			set
			{
				this.m_CullingOptions = value;
			}
		}

		public ReflectionProbeSortingCriteria reflectionProbeSortingCriteria
		{
			get
			{
				return this.m_ReflectionProbeSortingCriteria;
			}
			set
			{
				this.m_ReflectionProbeSortingCriteria = value;
			}
		}

		public CameraProperties cameraProperties
		{
			get
			{
				return this.m_CameraProperties;
			}
			set
			{
				this.m_CameraProperties = value;
			}
		}

		public Matrix4x4 stereoViewMatrix
		{
			get
			{
				return this.m_StereoViewMatrix;
			}
			set
			{
				this.m_StereoViewMatrix = value;
			}
		}

		public Matrix4x4 stereoProjectionMatrix
		{
			get
			{
				return this.m_StereoProjectionMatrix;
			}
			set
			{
				this.m_StereoProjectionMatrix = value;
			}
		}

		public float stereoSeparationDistance
		{
			get
			{
				return this.m_StereoSeparationDistance;
			}
			set
			{
				this.m_StereoSeparationDistance = value;
			}
		}

		public float accurateOcclusionThreshold
		{
			get
			{
				return this.m_AccurateOcclusionThreshold;
			}
			set
			{
				this.m_AccurateOcclusionThreshold = Mathf.Max(-1f, value);
			}
		}

		public int maximumPortalCullingJobs
		{
			get
			{
				return this.m_MaximumPortalCullingJobs;
			}
			set
			{
				bool flag = value < 1 || value > 16;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be in range {2} to {3}", new object[]
					{
						"maximumPortalCullingJobs",
						this.maximumPortalCullingJobs,
						1,
						16
					}));
				}
				this.m_MaximumPortalCullingJobs = value;
			}
		}

		public static int cullingJobsLowerLimit
		{
			get
			{
				return 1;
			}
		}

		public static int cullingJobsUpperLimit
		{
			get
			{
				return 16;
			}
		}

		public unsafe float GetLayerCullingDistance(int layerIndex)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			float* ptr = &this.m_LayerFarCullDistances.FixedElementField;
			return ptr[layerIndex];
		}

		public unsafe void SetLayerCullingDistance(int layerIndex, float distance)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				ptr2[layerIndex] = distance;
			}
		}

		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			byte* ptr = &this.m_CullingPlanes.FixedElementField;
			Plane* ptr2 = (Plane*)ptr;
			return ptr2[index];
		}

		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		public bool Equals(ScriptableCullingParameters other)
		{
			bool result;
			for (int i = 0; i < 32; i++)
			{
				bool flag = !this.GetLayerCullingDistance(i).Equals(other.GetLayerCullingDistance(i));
				if (flag)
				{
					result = false;
					return result;
				}
			}
			for (int j = 0; j < this.cullingPlaneCount; j++)
			{
				bool flag2 = !this.GetCullingPlane(j).Equals(other.GetCullingPlane(j));
				if (flag2)
				{
					result = false;
					return result;
				}
			}
			result = (this.m_IsOrthographic == other.m_IsOrthographic && this.m_LODParameters.Equals(other.m_LODParameters) && this.m_CullingPlaneCount == other.m_CullingPlaneCount && this.m_CullingMask == other.m_CullingMask && this.m_SceneMask == other.m_SceneMask && this.m_LayerCull == other.m_LayerCull && this.m_CullingMatrix.Equals(other.m_CullingMatrix) && this.m_Origin.Equals(other.m_Origin) && this.m_ShadowDistance.Equals(other.m_ShadowDistance) && this.m_CullingOptions == other.m_CullingOptions && this.m_ReflectionProbeSortingCriteria == other.m_ReflectionProbeSortingCriteria && this.m_CameraProperties.Equals(other.m_CameraProperties) && this.m_AccurateOcclusionThreshold.Equals(other.m_AccurateOcclusionThreshold) && this.m_StereoViewMatrix.Equals(other.m_StereoViewMatrix) && this.m_StereoProjectionMatrix.Equals(other.m_StereoProjectionMatrix) && this.m_StereoSeparationDistance.Equals(other.m_StereoSeparationDistance) && this.m_maximumVisibleLights == other.m_maximumVisibleLights);
			return result;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableCullingParameters && this.Equals((ScriptableCullingParameters)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397 ^ this.m_LODParameters.GetHashCode());
			num = (num * 397 ^ this.m_CullingPlaneCount);
			num = (num * 397 ^ (int)this.m_CullingMask);
			num = (num * 397 ^ this.m_SceneMask.GetHashCode());
			num = (num * 397 ^ this.m_LayerCull);
			num = (num * 397 ^ this.m_CullingMatrix.GetHashCode());
			num = (num * 397 ^ this.m_Origin.GetHashCode());
			num = (num * 397 ^ this.m_ShadowDistance.GetHashCode());
			num = (num * 397 ^ (int)this.m_CullingOptions);
			num = (num * 397 ^ (int)this.m_ReflectionProbeSortingCriteria);
			num = (num * 397 ^ this.m_CameraProperties.GetHashCode());
			num = (num * 397 ^ this.m_AccurateOcclusionThreshold.GetHashCode());
			num = (num * 397 ^ this.m_MaximumPortalCullingJobs.GetHashCode());
			num = (num * 397 ^ this.m_StereoViewMatrix.GetHashCode());
			num = (num * 397 ^ this.m_StereoProjectionMatrix.GetHashCode());
			num = (num * 397 ^ this.m_StereoSeparationDistance.GetHashCode());
			return num * 397 ^ this.m_maximumVisibleLights;
		}

		public static bool operator ==(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return !left.Equals(right);
		}
	}
}
