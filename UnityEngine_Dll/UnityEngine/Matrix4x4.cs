using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[Il2CppEagerStaticClassConstruction, NativeHeader("Runtime/Math/MathScripting.h"), NativeType(Header = "Runtime/Math/Matrix4x4.h"), NativeClass("Matrix4x4f"), RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Matrix4x4 : IEquatable<Matrix4x4>, IFormattable
	{
		[NativeName("m_Data[0]")]
		public float m00;

		[NativeName("m_Data[1]")]
		public float m10;

		[NativeName("m_Data[2]")]
		public float m20;

		[NativeName("m_Data[3]")]
		public float m30;

		[NativeName("m_Data[4]")]
		public float m01;

		[NativeName("m_Data[5]")]
		public float m11;

		[NativeName("m_Data[6]")]
		public float m21;

		[NativeName("m_Data[7]")]
		public float m31;

		[NativeName("m_Data[8]")]
		public float m02;

		[NativeName("m_Data[9]")]
		public float m12;

		[NativeName("m_Data[10]")]
		public float m22;

		[NativeName("m_Data[11]")]
		public float m32;

		[NativeName("m_Data[12]")]
		public float m03;

		[NativeName("m_Data[13]")]
		public float m13;

		[NativeName("m_Data[14]")]
		public float m23;

		[NativeName("m_Data[15]")]
		public float m33;

		private static readonly Matrix4x4 zeroMatrix = new Matrix4x4(new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));

		private static readonly Matrix4x4 identityMatrix = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));

		public Quaternion rotation
		{
			get
			{
				return this.GetRotation();
			}
		}

		public Vector3 lossyScale
		{
			get
			{
				return this.GetLossyScale();
			}
		}

		public bool isIdentity
		{
			get
			{
				return this.IsIdentity();
			}
		}

		public float determinant
		{
			get
			{
				return this.GetDeterminant();
			}
		}

		public FrustumPlanes decomposeProjection
		{
			get
			{
				return this.DecomposeProjection();
			}
		}

		public Matrix4x4 inverse
		{
			get
			{
				return Matrix4x4.Inverse(this);
			}
		}

		public Matrix4x4 transpose
		{
			get
			{
				return Matrix4x4.Transpose(this);
			}
		}

		public float this[int row, int column]
		{
			get
			{
				return this[row + column * 4];
			}
			set
			{
				this[row + column * 4] = value;
			}
		}

		public float this[int index]
		{
			get
			{
				float result;
				switch (index)
				{
				case 0:
					result = this.m00;
					break;
				case 1:
					result = this.m10;
					break;
				case 2:
					result = this.m20;
					break;
				case 3:
					result = this.m30;
					break;
				case 4:
					result = this.m01;
					break;
				case 5:
					result = this.m11;
					break;
				case 6:
					result = this.m21;
					break;
				case 7:
					result = this.m31;
					break;
				case 8:
					result = this.m02;
					break;
				case 9:
					result = this.m12;
					break;
				case 10:
					result = this.m22;
					break;
				case 11:
					result = this.m32;
					break;
				case 12:
					result = this.m03;
					break;
				case 13:
					result = this.m13;
					break;
				case 14:
					result = this.m23;
					break;
				case 15:
					result = this.m33;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
				return result;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		public static Matrix4x4 zero
		{
			get
			{
				return Matrix4x4.zeroMatrix;
			}
		}

		public static Matrix4x4 identity
		{
			get
			{
				return Matrix4x4.identityMatrix;
			}
		}

		[ThreadSafe]
		private Quaternion GetRotation()
		{
			Quaternion result;
			Matrix4x4.GetRotation_Injected(ref this, out result);
			return result;
		}

		[ThreadSafe]
		private Vector3 GetLossyScale()
		{
			Vector3 result;
			Matrix4x4.GetLossyScale_Injected(ref this, out result);
			return result;
		}

		[ThreadSafe]
		private bool IsIdentity()
		{
			return Matrix4x4.IsIdentity_Injected(ref this);
		}

		[ThreadSafe]
		private float GetDeterminant()
		{
			return Matrix4x4.GetDeterminant_Injected(ref this);
		}

		[ThreadSafe]
		private FrustumPlanes DecomposeProjection()
		{
			FrustumPlanes result;
			Matrix4x4.DecomposeProjection_Injected(ref this, out result);
			return result;
		}

		[ThreadSafe]
		public bool ValidTRS()
		{
			return Matrix4x4.ValidTRS_Injected(ref this);
		}

		public static float Determinant(Matrix4x4 m)
		{
			return m.determinant;
		}

		[FreeFunction("MatrixScripting::TRS", IsThreadSafe = true)]
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			Matrix4x4 result;
			Matrix4x4.TRS_Injected(ref pos, ref q, ref s, out result);
			return result;
		}

		public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			this = Matrix4x4.TRS(pos, q, s);
		}

		[FreeFunction("MatrixScripting::Inverse3DAffine", IsThreadSafe = true)]
		public static bool Inverse3DAffine(Matrix4x4 input, ref Matrix4x4 result)
		{
			return Matrix4x4.Inverse3DAffine_Injected(ref input, ref result);
		}

		[FreeFunction("MatrixScripting::Inverse", IsThreadSafe = true)]
		public static Matrix4x4 Inverse(Matrix4x4 m)
		{
			Matrix4x4 result;
			Matrix4x4.Inverse_Injected(ref m, out result);
			return result;
		}

		[FreeFunction("MatrixScripting::Transpose", IsThreadSafe = true)]
		public static Matrix4x4 Transpose(Matrix4x4 m)
		{
			Matrix4x4 result;
			Matrix4x4.Transpose_Injected(ref m, out result);
			return result;
		}

		[FreeFunction("MatrixScripting::Ortho", IsThreadSafe = true)]
		public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 result;
			Matrix4x4.Ortho_Injected(left, right, bottom, top, zNear, zFar, out result);
			return result;
		}

		[FreeFunction("MatrixScripting::Perspective", IsThreadSafe = true)]
		public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
		{
			Matrix4x4 result;
			Matrix4x4.Perspective_Injected(fov, aspect, zNear, zFar, out result);
			return result;
		}

		[FreeFunction("MatrixScripting::LookAt", IsThreadSafe = true)]
		public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
		{
			Matrix4x4 result;
			Matrix4x4.LookAt_Injected(ref from, ref to, ref up, out result);
			return result;
		}

		[FreeFunction("MatrixScripting::Frustum", IsThreadSafe = true)]
		public static Matrix4x4 Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 result;
			Matrix4x4.Frustum_Injected(left, right, bottom, top, zNear, zFar, out result);
			return result;
		}

		public static Matrix4x4 Frustum(FrustumPlanes fp)
		{
			return Matrix4x4.Frustum(fp.left, fp.right, fp.bottom, fp.top, fp.zNear, fp.zFar);
		}

		public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
		{
			this.m00 = column0.x;
			this.m01 = column1.x;
			this.m02 = column2.x;
			this.m03 = column3.x;
			this.m10 = column0.y;
			this.m11 = column1.y;
			this.m12 = column2.y;
			this.m13 = column3.y;
			this.m20 = column0.z;
			this.m21 = column1.z;
			this.m22 = column2.z;
			this.m23 = column3.z;
			this.m30 = column0.w;
			this.m31 = column1.w;
			this.m32 = column2.w;
			this.m33 = column3.w;
		}

		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ this.GetColumn(1).GetHashCode() << 2 ^ this.GetColumn(2).GetHashCode() >> 2 ^ this.GetColumn(3).GetHashCode() >> 1;
		}

		public override bool Equals(object other)
		{
			bool flag = !(other is Matrix4x4);
			return !flag && this.Equals((Matrix4x4)other);
		}

		public bool Equals(Matrix4x4 other)
		{
			return this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) && this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
		}

		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 result;
			result.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			result.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			result.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			result.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
			result.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			result.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			result.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			result.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
			result.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			result.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			result.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			result.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
			result.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			result.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			result.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			result.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
			return result;
		}

		public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
		{
			Vector4 result;
			result.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
			result.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
			result.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
			result.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
			return result;
		}

		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return !(lhs == rhs);
		}

		public Vector4 GetColumn(int index)
		{
			Vector4 result;
			switch (index)
			{
			case 0:
				result = new Vector4(this.m00, this.m10, this.m20, this.m30);
				break;
			case 1:
				result = new Vector4(this.m01, this.m11, this.m21, this.m31);
				break;
			case 2:
				result = new Vector4(this.m02, this.m12, this.m22, this.m32);
				break;
			case 3:
				result = new Vector4(this.m03, this.m13, this.m23, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid column index!");
			}
			return result;
		}

		public Vector4 GetRow(int index)
		{
			Vector4 result;
			switch (index)
			{
			case 0:
				result = new Vector4(this.m00, this.m01, this.m02, this.m03);
				break;
			case 1:
				result = new Vector4(this.m10, this.m11, this.m12, this.m13);
				break;
			case 2:
				result = new Vector4(this.m20, this.m21, this.m22, this.m23);
				break;
			case 3:
				result = new Vector4(this.m30, this.m31, this.m32, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid row index!");
			}
			return result;
		}

		public void SetColumn(int index, Vector4 column)
		{
			this[0, index] = column.x;
			this[1, index] = column.y;
			this[2, index] = column.z;
			this[3, index] = column.w;
		}

		public void SetRow(int index, Vector4 row)
		{
			this[index, 0] = row.x;
			this[index, 1] = row.y;
			this[index, 2] = row.z;
			this[index, 3] = row.w;
		}

		public Vector3 MultiplyPoint(Vector3 point)
		{
			Vector3 result;
			result.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			result.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			result.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			float num = this.m30 * point.x + this.m31 * point.y + this.m32 * point.z + this.m33;
			num = 1f / num;
			result.x *= num;
			result.y *= num;
			result.z *= num;
			return result;
		}

		public Vector3 MultiplyPoint3x4(Vector3 point)
		{
			Vector3 result;
			result.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			result.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			result.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			return result;
		}

		public Vector3 MultiplyVector(Vector3 vector)
		{
			Vector3 result;
			result.x = this.m00 * vector.x + this.m01 * vector.y + this.m02 * vector.z;
			result.y = this.m10 * vector.x + this.m11 * vector.y + this.m12 * vector.z;
			result.z = this.m20 * vector.x + this.m21 * vector.y + this.m22 * vector.z;
			return result;
		}

		public Plane TransformPlane(Plane plane)
		{
			Matrix4x4 inverse = this.inverse;
			float x = plane.normal.x;
			float y = plane.normal.y;
			float z = plane.normal.z;
			float distance = plane.distance;
			float x2 = inverse.m00 * x + inverse.m10 * y + inverse.m20 * z + inverse.m30 * distance;
			float y2 = inverse.m01 * x + inverse.m11 * y + inverse.m21 * z + inverse.m31 * distance;
			float z2 = inverse.m02 * x + inverse.m12 * y + inverse.m22 * z + inverse.m32 * distance;
			float d = inverse.m03 * x + inverse.m13 * y + inverse.m23 * z + inverse.m33 * distance;
			return new Plane(new Vector3(x2, y2, z2), d);
		}

		public static Matrix4x4 Scale(Vector3 vector)
		{
			Matrix4x4 result;
			result.m00 = vector.x;
			result.m01 = 0f;
			result.m02 = 0f;
			result.m03 = 0f;
			result.m10 = 0f;
			result.m11 = vector.y;
			result.m12 = 0f;
			result.m13 = 0f;
			result.m20 = 0f;
			result.m21 = 0f;
			result.m22 = vector.z;
			result.m23 = 0f;
			result.m30 = 0f;
			result.m31 = 0f;
			result.m32 = 0f;
			result.m33 = 1f;
			return result;
		}

		public static Matrix4x4 Translate(Vector3 vector)
		{
			Matrix4x4 result;
			result.m00 = 1f;
			result.m01 = 0f;
			result.m02 = 0f;
			result.m03 = vector.x;
			result.m10 = 0f;
			result.m11 = 1f;
			result.m12 = 0f;
			result.m13 = vector.y;
			result.m20 = 0f;
			result.m21 = 0f;
			result.m22 = 1f;
			result.m23 = vector.z;
			result.m30 = 0f;
			result.m31 = 0f;
			result.m32 = 0f;
			result.m33 = 1f;
			return result;
		}

		public static Matrix4x4 Rotate(Quaternion q)
		{
			float num = q.x * 2f;
			float num2 = q.y * 2f;
			float num3 = q.z * 2f;
			float num4 = q.x * num;
			float num5 = q.y * num2;
			float num6 = q.z * num3;
			float num7 = q.x * num2;
			float num8 = q.x * num3;
			float num9 = q.y * num3;
			float num10 = q.w * num;
			float num11 = q.w * num2;
			float num12 = q.w * num3;
			Matrix4x4 result;
			result.m00 = 1f - (num5 + num6);
			result.m10 = num7 + num12;
			result.m20 = num8 - num11;
			result.m30 = 0f;
			result.m01 = num7 - num12;
			result.m11 = 1f - (num4 + num6);
			result.m21 = num9 + num10;
			result.m31 = 0f;
			result.m02 = num8 + num11;
			result.m12 = num9 - num10;
			result.m22 = 1f - (num4 + num5);
			result.m32 = 0f;
			result.m03 = 0f;
			result.m13 = 0f;
			result.m23 = 0f;
			result.m33 = 1f;
			return result;
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
				format = "F5";
			}
			return UnityString.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
			{
				this.m00.ToString(format, formatProvider),
				this.m01.ToString(format, formatProvider),
				this.m02.ToString(format, formatProvider),
				this.m03.ToString(format, formatProvider),
				this.m10.ToString(format, formatProvider),
				this.m11.ToString(format, formatProvider),
				this.m12.ToString(format, formatProvider),
				this.m13.ToString(format, formatProvider),
				this.m20.ToString(format, formatProvider),
				this.m21.ToString(format, formatProvider),
				this.m22.ToString(format, formatProvider),
				this.m23.ToString(format, formatProvider),
				this.m30.ToString(format, formatProvider),
				this.m31.ToString(format, formatProvider),
				this.m32.ToString(format, formatProvider),
				this.m33.ToString(format, formatProvider)
			});
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotation_Injected(ref Matrix4x4 _unity_self, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLossyScale_Injected(ref Matrix4x4 _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsIdentity_Injected(ref Matrix4x4 _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDeterminant_Injected(ref Matrix4x4 _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DecomposeProjection_Injected(ref Matrix4x4 _unity_self, out FrustumPlanes ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidTRS_Injected(ref Matrix4x4 _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TRS_Injected(ref Vector3 pos, ref Quaternion q, ref Vector3 s, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Inverse3DAffine_Injected(ref Matrix4x4 input, ref Matrix4x4 result);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Inverse_Injected(ref Matrix4x4 m, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Transpose_Injected(ref Matrix4x4 m, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Ortho_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Perspective_Injected(float fov, float aspect, float zNear, float zFar, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LookAt_Injected(ref Vector3 from, ref Vector3 to, ref Vector3 up, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Frustum_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);
	}
}
