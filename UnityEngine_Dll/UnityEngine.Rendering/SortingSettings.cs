using System;

namespace UnityEngine.Rendering
{
	public struct SortingSettings : IEquatable<SortingSettings>
	{
		private Matrix4x4 m_WorldToCameraMatrix;

		private Vector3 m_CameraPosition;

		private Vector3 m_CustomAxis;

		private SortingCriteria m_Criteria;

		private DistanceMetric m_DistanceMetric;

		private Matrix4x4 m_PreviousVPMatrix;

		private Matrix4x4 m_NonJitteredVPMatrix;

		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				return this.m_WorldToCameraMatrix;
			}
			set
			{
				this.m_WorldToCameraMatrix = value;
			}
		}

		public Vector3 cameraPosition
		{
			get
			{
				return this.m_CameraPosition;
			}
			set
			{
				this.m_CameraPosition = value;
			}
		}

		public Vector3 customAxis
		{
			get
			{
				return this.m_CustomAxis;
			}
			set
			{
				this.m_CustomAxis = value;
			}
		}

		public SortingCriteria criteria
		{
			get
			{
				return this.m_Criteria;
			}
			set
			{
				this.m_Criteria = value;
			}
		}

		public DistanceMetric distanceMetric
		{
			get
			{
				return this.m_DistanceMetric;
			}
			set
			{
				this.m_DistanceMetric = value;
			}
		}

		public SortingSettings(Camera camera)
		{
			ScriptableRenderContext.InitializeSortSettings(camera, out this);
			this.m_Criteria = this.criteria;
		}

		public bool Equals(SortingSettings other)
		{
			return this.m_WorldToCameraMatrix.Equals(other.m_WorldToCameraMatrix) && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_CustomAxis.Equals(other.m_CustomAxis) && this.m_Criteria == other.m_Criteria && this.m_DistanceMetric == other.m_DistanceMetric && this.m_PreviousVPMatrix.Equals(other.m_PreviousVPMatrix) && this.m_NonJitteredVPMatrix.Equals(other.m_NonJitteredVPMatrix);
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SortingSettings && this.Equals((SortingSettings)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_WorldToCameraMatrix.GetHashCode();
			num = (num * 397 ^ this.m_CameraPosition.GetHashCode());
			num = (num * 397 ^ this.m_CustomAxis.GetHashCode());
			num = (num * 397 ^ (int)this.m_Criteria);
			num = (num * 397 ^ (int)this.m_DistanceMetric);
			num = (num * 397 ^ this.m_PreviousVPMatrix.GetHashCode());
			return num * 397 ^ this.m_NonJitteredVPMatrix.GetHashCode();
		}

		public static bool operator ==(SortingSettings left, SortingSettings right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(SortingSettings left, SortingSettings right)
		{
			return !left.Equals(right);
		}
	}
}
