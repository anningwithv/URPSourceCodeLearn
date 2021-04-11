using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h"), NativeClass("ContactFilter", "struct ContactFilter;"), RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[Serializable]
	public struct ContactFilter2D
	{
		[NativeName("m_UseTriggers")]
		public bool useTriggers;

		[NativeName("m_UseLayerMask")]
		public bool useLayerMask;

		[NativeName("m_UseDepth")]
		public bool useDepth;

		[NativeName("m_UseOutsideDepth")]
		public bool useOutsideDepth;

		[NativeName("m_UseNormalAngle")]
		public bool useNormalAngle;

		[NativeName("m_UseOutsideNormalAngle")]
		public bool useOutsideNormalAngle;

		[NativeName("m_LayerMask")]
		public LayerMask layerMask;

		[NativeName("m_MinDepth")]
		public float minDepth;

		[NativeName("m_MaxDepth")]
		public float maxDepth;

		[NativeName("m_MinNormalAngle")]
		public float minNormalAngle;

		[NativeName("m_MaxNormalAngle")]
		public float maxNormalAngle;

		public const float NormalAngleUpperLimit = 359.9999f;

		public bool isFiltering
		{
			get
			{
				return !this.useTriggers || this.useLayerMask || this.useDepth || this.useNormalAngle;
			}
		}

		public ContactFilter2D NoFilter()
		{
			this.useTriggers = true;
			this.useLayerMask = false;
			this.layerMask = -1;
			this.useDepth = false;
			this.useOutsideDepth = false;
			this.minDepth = float.NegativeInfinity;
			this.maxDepth = float.PositiveInfinity;
			this.useNormalAngle = false;
			this.useOutsideNormalAngle = false;
			this.minNormalAngle = 0f;
			this.maxNormalAngle = 359.9999f;
			return this;
		}

		private void CheckConsistency()
		{
			ContactFilter2D.CheckConsistency_Injected(ref this);
		}

		public void ClearLayerMask()
		{
			this.useLayerMask = false;
		}

		public void SetLayerMask(LayerMask layerMask)
		{
			this.layerMask = layerMask;
			this.useLayerMask = true;
		}

		public void ClearDepth()
		{
			this.useDepth = false;
		}

		public void SetDepth(float minDepth, float maxDepth)
		{
			this.minDepth = minDepth;
			this.maxDepth = maxDepth;
			this.useDepth = true;
			this.CheckConsistency();
		}

		public void ClearNormalAngle()
		{
			this.useNormalAngle = false;
		}

		public void SetNormalAngle(float minNormalAngle, float maxNormalAngle)
		{
			this.minNormalAngle = minNormalAngle;
			this.maxNormalAngle = maxNormalAngle;
			this.useNormalAngle = true;
			this.CheckConsistency();
		}

		public bool IsFilteringTrigger([Writable] Collider2D collider)
		{
			return !this.useTriggers && collider.isTrigger;
		}

		public bool IsFilteringLayerMask(GameObject obj)
		{
			return this.useLayerMask && (this.layerMask & 1 << obj.layer) == 0;
		}

		public bool IsFilteringDepth(GameObject obj)
		{
			bool flag = !this.useDepth;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.minDepth > this.maxDepth;
				if (flag2)
				{
					float num = this.minDepth;
					this.minDepth = this.maxDepth;
					this.maxDepth = num;
				}
				float z = obj.transform.position.z;
				bool flag3 = z < this.minDepth || z > this.maxDepth;
				bool flag4 = this.useOutsideDepth;
				if (flag4)
				{
					result = !flag3;
				}
				else
				{
					result = flag3;
				}
			}
			return result;
		}

		public bool IsFilteringNormalAngle(Vector2 normal)
		{
			return ContactFilter2D.IsFilteringNormalAngle_Injected(ref this, ref normal);
		}

		public bool IsFilteringNormalAngle(float angle)
		{
			return this.IsFilteringNormalAngleUsingAngle(angle);
		}

		private bool IsFilteringNormalAngleUsingAngle(float angle)
		{
			return ContactFilter2D.IsFilteringNormalAngleUsingAngle_Injected(ref this, angle);
		}

		internal static ContactFilter2D CreateLegacyFilter(int layerMask, float minDepth, float maxDepth)
		{
			ContactFilter2D result = default(ContactFilter2D);
			result.useTriggers = Physics2D.queriesHitTriggers;
			result.SetLayerMask(layerMask);
			result.SetDepth(minDepth, maxDepth);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CheckConsistency_Injected(ref ContactFilter2D _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFilteringNormalAngle_Injected(ref ContactFilter2D _unity_self, ref Vector2 normal);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFilteringNormalAngleUsingAngle_Injected(ref ContactFilter2D _unity_self, float angle);
	}
}
