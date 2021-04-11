using System;

namespace UnityEngine.Rendering
{
	public struct LODParameters : IEquatable<LODParameters>
	{
		private int m_IsOrthographic;

		private Vector3 m_CameraPosition;

		private float m_FieldOfView;

		private float m_OrthoSize;

		private int m_CameraPixelHeight;

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

		public float fieldOfView
		{
			get
			{
				return this.m_FieldOfView;
			}
			set
			{
				this.m_FieldOfView = value;
			}
		}

		public float orthoSize
		{
			get
			{
				return this.m_OrthoSize;
			}
			set
			{
				this.m_OrthoSize = value;
			}
		}

		public int cameraPixelHeight
		{
			get
			{
				return this.m_CameraPixelHeight;
			}
			set
			{
				this.m_CameraPixelHeight = value;
			}
		}

		public bool Equals(LODParameters other)
		{
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_FieldOfView.Equals(other.m_FieldOfView) && this.m_OrthoSize.Equals(other.m_OrthoSize) && this.m_CameraPixelHeight == other.m_CameraPixelHeight;
		}

		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LODParameters && this.Equals((LODParameters)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397 ^ this.m_CameraPosition.GetHashCode());
			num = (num * 397 ^ this.m_FieldOfView.GetHashCode());
			num = (num * 397 ^ this.m_OrthoSize.GetHashCode());
			return num * 397 ^ this.m_CameraPixelHeight;
		}

		public static bool operator ==(LODParameters left, LODParameters right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(LODParameters left, LODParameters right)
		{
			return !left.Equals(right);
		}
	}
}
