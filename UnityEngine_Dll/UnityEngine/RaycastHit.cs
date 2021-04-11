using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/Physics/RaycastHit.h"), NativeHeader("PhysicsScriptingClasses.h"), NativeHeader("Runtime/Interfaces/IRaycast.h"), UsedByNativeCode]
	public struct RaycastHit
	{
		[NativeName("point")]
		internal Vector3 m_Point;

		[NativeName("normal")]
		internal Vector3 m_Normal;

		[NativeName("faceID")]
		internal uint m_FaceID;

		[NativeName("distance")]
		internal float m_Distance;

		[NativeName("uv")]
		internal Vector2 m_UV;

		[NativeName("collider")]
		internal int m_Collider;

		public Collider collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider;
			}
		}

		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		public Vector3 barycentricCoordinate
		{
			get
			{
				return new Vector3(1f - (this.m_UV.y + this.m_UV.x), this.m_UV.x, this.m_UV.y);
			}
			set
			{
				this.m_UV = value;
			}
		}

		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		public int triangleIndex
		{
			get
			{
				return (int)this.m_FaceID;
			}
		}

		public Vector2 textureCoord
		{
			get
			{
				return RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 0);
			}
		}

		public Vector2 textureCoord2
		{
			get
			{
				return RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
			}
		}

		[Obsolete("Use textureCoord2 instead. (UnityUpgradable) -> textureCoord2")]
		public Vector2 textureCoord1
		{
			get
			{
				return this.textureCoord2;
			}
		}

		public Transform transform
		{
			get
			{
				Rigidbody rigidbody = this.rigidbody;
				bool flag = rigidbody != null;
				Transform result;
				if (flag)
				{
					result = rigidbody.transform;
				}
				else
				{
					bool flag2 = this.collider != null;
					if (flag2)
					{
						result = this.collider.transform;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		public Rigidbody rigidbody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedRigidbody : null;
			}
		}

		public ArticulationBody articulationBody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedArticulationBody : null;
			}
		}

		public Vector2 lightmapCoord
		{
			get
			{
				Vector2 vector = RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				bool flag = this.collider.GetComponent<Renderer>() != null;
				if (flag)
				{
					Vector4 lightmapScaleOffset = this.collider.GetComponent<Renderer>().lightmapScaleOffset;
					vector.x = vector.x * lightmapScaleOffset.x + lightmapScaleOffset.z;
					vector.y = vector.y * lightmapScaleOffset.y + lightmapScaleOffset.w;
				}
				return vector;
			}
		}

		[FreeFunction]
		private static Vector2 CalculateRaycastTexCoord(Collider collider, Vector2 uv, Vector3 pos, uint face, int textcoord)
		{
			Vector2 result;
			RaycastHit.CalculateRaycastTexCoord_Injected(collider, ref uv, ref pos, face, textcoord, out result);
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateRaycastTexCoord_Injected(Collider collider, ref Vector2 uv, ref Vector3 pos, uint face, int textcoord, out Vector2 ret);
	}
}
