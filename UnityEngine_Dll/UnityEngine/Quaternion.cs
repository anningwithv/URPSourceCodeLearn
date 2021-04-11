using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[Il2CppEagerStaticClassConstruction, NativeHeader("Runtime/Math/MathScripting.h"), NativeType(Header = "Runtime/Math/Quaternion.h"), UsedByNativeCode]
	public struct Quaternion : IEquatable<Quaternion>, IFormattable
	{
		public float x;

		public float y;

		public float z;

		public float w;

		private static readonly Quaternion identityQuaternion = new Quaternion(0f, 0f, 0f, 1f);

		public const float kEpsilon = 1E-06f;

		public float this[int index]
		{
			get
			{
				float result;
				switch (index)
				{
				case 0:
					result = this.x;
					break;
				case 1:
					result = this.y;
					break;
				case 2:
					result = this.z;
					break;
				case 3:
					result = this.w;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
				return result;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		public static Quaternion identity
		{
			get
			{
				return Quaternion.identityQuaternion;
			}
		}

		public Vector3 eulerAngles
		{
			get
			{
				return Quaternion.Internal_MakePositive(Quaternion.Internal_ToEulerRad(this) * 57.29578f);
			}
			set
			{
				this = Quaternion.Internal_FromEulerRad(value * 0.0174532924f);
			}
		}

		public Quaternion normalized
		{
			get
			{
				return Quaternion.Normalize(this);
			}
		}

		[FreeFunction("FromToQuaternionSafe", IsThreadSafe = true)]
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			Quaternion result;
			Quaternion.FromToRotation_Injected(ref fromDirection, ref toDirection, out result);
			return result;
		}

		[FreeFunction(IsThreadSafe = true)]
		public static Quaternion Inverse(Quaternion rotation)
		{
			Quaternion result;
			Quaternion.Inverse_Injected(ref rotation, out result);
			return result;
		}

		[FreeFunction("QuaternionScripting::Slerp", IsThreadSafe = true)]
		public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.Slerp_Injected(ref a, ref b, t, out result);
			return result;
		}

		[FreeFunction("QuaternionScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.SlerpUnclamped_Injected(ref a, ref b, t, out result);
			return result;
		}

		[FreeFunction("QuaternionScripting::Lerp", IsThreadSafe = true)]
		public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.Lerp_Injected(ref a, ref b, t, out result);
			return result;
		}

		[FreeFunction("QuaternionScripting::LerpUnclamped", IsThreadSafe = true)]
		public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.LerpUnclamped_Injected(ref a, ref b, t, out result);
			return result;
		}

		[FreeFunction("EulerToQuaternion", IsThreadSafe = true)]
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			Quaternion result;
			Quaternion.Internal_FromEulerRad_Injected(ref euler, out result);
			return result;
		}

		[FreeFunction("QuaternionScripting::ToEuler", IsThreadSafe = true)]
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			Vector3 result;
			Quaternion.Internal_ToEulerRad_Injected(ref rotation, out result);
			return result;
		}

		[FreeFunction("QuaternionScripting::ToAxisAngle", IsThreadSafe = true)]
		private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad_Injected(ref q, out axis, out angle);
		}

		[FreeFunction("QuaternionScripting::AngleAxis", IsThreadSafe = true)]
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			Quaternion result;
			Quaternion.AngleAxis_Injected(angle, ref axis, out result);
			return result;
		}

		[FreeFunction("QuaternionScripting::LookRotation", IsThreadSafe = true)]
		public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
		{
			Quaternion result;
			Quaternion.LookRotation_Injected(ref forward, ref upwards, out result);
			return result;
		}

		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			return Quaternion.LookRotation(forward, Vector3.up);
		}

		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 result;
			result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return result;
		}

		private static bool IsEqualUsingDot(float dot)
		{
			return dot > 0.999999f;
		}

		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
		}

		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return !(lhs == rhs);
		}

		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		[ExcludeFromDocs]
		public void SetLookRotation(Vector3 view)
		{
			Vector3 up = Vector3.up;
			this.SetLookRotation(view, up);
		}

		public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
		{
			this = Quaternion.LookRotation(view, up);
		}

		[MethodImpl((MethodImplOptions)256)]
		public static float Angle(Quaternion a, Quaternion b)
		{
			float num = Quaternion.Dot(a, b);
			return Quaternion.IsEqualUsingDot(num) ? 0f : (Mathf.Acos(Mathf.Min(Mathf.Abs(num), 1f)) * 2f * 57.29578f);
		}

		private static Vector3 Internal_MakePositive(Vector3 euler)
		{
			float num = -0.005729578f;
			float num2 = 360f + num;
			bool flag = euler.x < num;
			if (flag)
			{
				euler.x += 360f;
			}
			else
			{
				bool flag2 = euler.x > num2;
				if (flag2)
				{
					euler.x -= 360f;
				}
			}
			bool flag3 = euler.y < num;
			if (flag3)
			{
				euler.y += 360f;
			}
			else
			{
				bool flag4 = euler.y > num2;
				if (flag4)
				{
					euler.y -= 360f;
				}
			}
			bool flag5 = euler.z < num;
			if (flag5)
			{
				euler.z += 360f;
			}
			else
			{
				bool flag6 = euler.z > num2;
				if (flag6)
				{
					euler.z -= 360f;
				}
			}
			return euler;
		}

		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.0174532924f);
		}

		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.0174532924f);
		}

		[MethodImpl((MethodImplOptions)256)]
		public void ToAngleAxis(out float angle, out Vector3 axis)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
			angle *= 57.29578f;
		}

		public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			this = Quaternion.FromToRotation(fromDirection, toDirection);
		}

		public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			float num = Quaternion.Angle(from, to);
			bool flag = num == 0f;
			Quaternion result;
			if (flag)
			{
				result = to;
			}
			else
			{
				result = Quaternion.SlerpUnclamped(from, to, Mathf.Min(1f, maxDegreesDelta / num));
			}
			return result;
		}

		public static Quaternion Normalize(Quaternion q)
		{
			float num = Mathf.Sqrt(Quaternion.Dot(q, q));
			bool flag = num < Mathf.Epsilon;
			Quaternion result;
			if (flag)
			{
				result = Quaternion.identity;
			}
			else
			{
				result = new Quaternion(q.x / num, q.y / num, q.z / num, q.w / num);
			}
			return result;
		}

		public void Normalize()
		{
			this = Quaternion.Normalize(this);
		}

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		public override bool Equals(object other)
		{
			bool flag = !(other is Quaternion);
			return !flag && this.Equals((Quaternion)other);
		}

		public bool Equals(Quaternion other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
		}

		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F1";
			}
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Quaternion EulerRotation(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Quaternion EulerRotation(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetEulerRotation(float x, float y, float z)
		{
			this = Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetEulerRotation(Vector3 euler)
		{
			this = Quaternion.Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		public Vector3 ToEuler()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Quaternion EulerAngles(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Quaternion EulerAngles(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
		public void ToAxisAngle(out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetEulerAngles(float x, float y, float z)
		{
			this.SetEulerRotation(new Vector3(x, y, z));
		}

		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetEulerAngles(Vector3 euler)
		{
			this = Quaternion.EulerRotation(euler);
		}

		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Vector3 ToEulerAngles(Quaternion rotation)
		{
			return Quaternion.Internal_ToEulerRad(rotation);
		}

		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		public Vector3 ToEulerAngles()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetAxisAngle(Vector3 axis, float angle)
		{
			this = Quaternion.AxisAngle(axis, angle);
		}

		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion AxisAngle(Vector3 axis, float angle)
		{
			return Quaternion.AngleAxis(57.29578f * angle, axis);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromToRotation_Injected(ref Vector3 fromDirection, ref Vector3 toDirection, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Inverse_Injected(ref Quaternion rotation, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SlerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_FromEulerRad_Injected(ref Vector3 euler, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToEulerRad_Injected(ref Quaternion rotation, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToAxisAngleRad_Injected(ref Quaternion q, out Vector3 axis, out float angle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AngleAxis_Injected(float angle, ref Vector3 axis, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LookRotation_Injected(ref Vector3 forward, [DefaultValue("Vector3.up")] ref Vector3 upwards, out Quaternion ret);
	}
}
