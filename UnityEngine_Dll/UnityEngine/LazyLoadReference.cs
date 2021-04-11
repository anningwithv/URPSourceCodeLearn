using System;

namespace UnityEngine
{
	[Serializable]
	public struct LazyLoadReference<T> where T : Object
	{
		private const int kInstanceID_None = 0;

		[SerializeField]
		private int m_InstanceID;

		public bool isSet
		{
			get
			{
				return this.m_InstanceID != 0;
			}
		}

		public bool isBroken
		{
			get
			{
				return this.m_InstanceID != 0 && !Object.DoesObjectWithInstanceIDExist(this.m_InstanceID);
			}
		}

		public T asset
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				T result;
				if (flag)
				{
					result = default(T);
				}
				else
				{
					result = (T)((object)Object.ForceLoadFromInstanceID(this.m_InstanceID));
				}
				return result;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.m_InstanceID = 0;
				}
				else
				{
					bool flag2 = !Object.IsPersistent(value);
					if (flag2)
					{
						throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
					}
					this.m_InstanceID = value.GetInstanceID();
				}
			}
		}

		public int instanceID
		{
			get
			{
				return this.m_InstanceID;
			}
			set
			{
				this.m_InstanceID = value;
			}
		}

		public LazyLoadReference(T asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				this.m_InstanceID = 0;
			}
			else
			{
				bool flag2 = !Object.IsPersistent(asset);
				if (flag2)
				{
					throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
				}
				this.m_InstanceID = asset.GetInstanceID();
			}
		}

		public LazyLoadReference(int instanceID)
		{
			this.m_InstanceID = instanceID;
		}

		public static implicit operator LazyLoadReference<T>(T asset)
		{
			return new LazyLoadReference<T>
			{
				asset = asset
			};
		}

		public static implicit operator LazyLoadReference<T>(int instanceID)
		{
			return new LazyLoadReference<T>
			{
				instanceID = instanceID
			};
		}
	}
}
