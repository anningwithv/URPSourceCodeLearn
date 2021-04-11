using System;

namespace UnityEngine
{
	[Serializable]
	public struct Pose : IEquatable<Pose>
	{
		public Vector3 position;

		public Quaternion rotation;

		private static readonly Pose k_Identity = new Pose(Vector3.zero, Quaternion.identity);

		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
		}

		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
		}

		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
		}

		public static Pose identity
		{
			get
			{
				return Pose.k_Identity;
			}
		}

		public Pose(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

		public override string ToString()
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(),
				this.rotation.ToString()
			});
		}

		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(format),
				this.rotation.ToString(format)
			});
		}

		public Pose GetTransformedBy(Pose lhs)
		{
			return new Pose
			{
				position = lhs.position + lhs.rotation * this.position,
				rotation = lhs.rotation * this.rotation
			};
		}

		public Pose GetTransformedBy(Transform lhs)
		{
			return new Pose
			{
				position = lhs.TransformPoint(this.position),
				rotation = lhs.rotation * this.rotation
			};
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is Pose);
			return !flag && this.Equals((Pose)obj);
		}

		public bool Equals(Pose other)
		{
			return this.position == other.position && this.rotation == other.rotation;
		}

		public override int GetHashCode()
		{
			return this.position.GetHashCode() ^ this.rotation.GetHashCode() << 1;
		}

		public static bool operator ==(Pose a, Pose b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Pose a, Pose b)
		{
			return !(a == b);
		}
	}
}
