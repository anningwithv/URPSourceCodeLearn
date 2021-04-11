using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Collision
	{
		internal Vector3 m_Impulse;

		internal Vector3 m_RelativeVelocity;

		internal Rigidbody m_Rigidbody;

		internal Collider m_Collider;

		internal int m_ContactCount;

		internal ContactPoint[] m_ReusedContacts;

		internal ContactPoint[] m_LegacyContacts;

		public Vector3 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Rigidbody;
			}
		}

		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		public Transform transform
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.transform : this.collider.transform;
			}
		}

		public GameObject gameObject
		{
			get
			{
				return (this.m_Rigidbody != null) ? this.m_Rigidbody.gameObject : this.m_Collider.gameObject;
			}
		}

		public int contactCount
		{
			get
			{
				return this.m_ContactCount;
			}
		}

		public ContactPoint[] contacts
		{
			get
			{
				bool flag = this.m_LegacyContacts == null;
				if (flag)
				{
					this.m_LegacyContacts = new ContactPoint[this.m_ContactCount];
					Array.Copy(this.m_ReusedContacts, this.m_LegacyContacts, this.m_ContactCount);
				}
				return this.m_LegacyContacts;
			}
		}

		public Vector3 impulse
		{
			get
			{
				return this.m_Impulse;
			}
		}

		[Obsolete("Use Collision.relativeVelocity instead.", false)]
		public Vector3 impactForceSum
		{
			get
			{
				return this.relativeVelocity;
			}
		}

		[Obsolete("Will always return zero.", false)]
		public Vector3 frictionForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		[Obsolete("Please use Collision.rigidbody, Collision.transform or Collision.collider instead", false)]
		public Component other
		{
			get
			{
				return (this.m_Rigidbody != null) ? this.m_Rigidbody : this.m_Collider;
			}
		}

		private ContactPoint[] GetContacts_Internal()
		{
			return (this.m_LegacyContacts == null) ? this.m_ReusedContacts : this.m_LegacyContacts;
		}

		public ContactPoint GetContact(int index)
		{
			bool flag = index < 0 || index >= this.m_ContactCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get contact at index {0}. There are {1} contact(s).", index, this.m_ContactCount));
			}
			return this.GetContacts_Internal()[index];
		}

		public int GetContacts(ContactPoint[] contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided array is NULL.");
			}
			int num = Mathf.Min(this.m_ContactCount, contacts.Length);
			Array.Copy(this.GetContacts_Internal(), contacts, num);
			return num;
		}

		public int GetContacts(List<ContactPoint> contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided list is NULL.");
			}
			contacts.Clear();
			contacts.AddRange(this.GetContacts_Internal());
			return this.contactCount;
		}

		[Obsolete("Do not use Collision.GetEnumerator(), enumerate using non-allocating array returned by Collision.GetContacts() or enumerate using Collision.GetContact(index) instead.", false)]
		public virtual IEnumerator GetEnumerator()
		{
			return this.contacts.GetEnumerator();
		}
	}
}
