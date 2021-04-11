using System;

namespace UnityEngine.TextCore
{
	internal static class TextUtilities
	{
		private struct LineSegment
		{
			public Vector3 Point1;

			public Vector3 Point2;

			public LineSegment(Vector3 p1, Vector3 p2)
			{
				this.Point1 = p1;
				this.Point2 = p2;
			}
		}

		private static Vector3[] s_RectWorldCorners = new Vector3[4];

		private const string k_LookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

		private const string k_LookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";

		public static bool IsIntersectingRectTransform(RectTransform rectTransform, Vector3 position, Camera camera)
		{
			TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			rectTransform.GetWorldCorners(TextUtilities.s_RectWorldCorners);
			return TextUtilities.PointIntersectRectangle(position, TextUtilities.s_RectWorldCorners[0], TextUtilities.s_RectWorldCorners[1], TextUtilities.s_RectWorldCorners[2], TextUtilities.s_RectWorldCorners[3]);
		}

		private static bool PointIntersectRectangle(Vector3 m, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			Vector3 vector = b - a;
			Vector3 rhs = m - a;
			Vector3 vector2 = c - b;
			Vector3 rhs2 = m - b;
			float num = Vector3.Dot(vector, rhs);
			float num2 = Vector3.Dot(vector2, rhs2);
			return 0f <= num && num <= Vector3.Dot(vector, vector) && 0f <= num2 && num2 <= Vector3.Dot(vector2, vector2);
		}

		public static bool ScreenPointToWorldPointInRectangle(Transform transform, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector3.zero;
			Ray ray = cam.ScreenPointToRay(screenPoint);
			float distance;
			bool flag = !new Plane(transform.rotation * Vector3.back, transform.position).Raycast(ray, out distance);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				worldPoint = ray.GetPoint(distance);
				result = true;
			}
			return result;
		}

		private static bool IntersectLinePlane(TextUtilities.LineSegment line, Vector3 point, Vector3 normal, out Vector3 intersectingPoint)
		{
			intersectingPoint = Vector3.zero;
			Vector3 vector = line.Point2 - line.Point1;
			Vector3 rhs = line.Point1 - point;
			float num = Vector3.Dot(normal, vector);
			float num2 = -Vector3.Dot(normal, rhs);
			bool flag = Mathf.Abs(num) < Mathf.Epsilon;
			bool result;
			if (flag)
			{
				result = (num2 == 0f);
			}
			else
			{
				float num3 = num2 / num;
				bool flag2 = num3 < 0f || num3 > 1f;
				if (flag2)
				{
					result = false;
				}
				else
				{
					intersectingPoint = line.Point1 + num3 * vector;
					result = true;
				}
			}
			return result;
		}

		public static float DistanceToLine(Vector3 a, Vector3 b, Vector3 point)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = a - point;
			float num = Vector3.Dot(vector, vector2);
			bool flag = num > 0f;
			float result;
			if (flag)
			{
				result = Vector3.Dot(vector2, vector2);
			}
			else
			{
				Vector3 vector3 = point - b;
				bool flag2 = Vector3.Dot(vector, vector3) > 0f;
				if (flag2)
				{
					result = Vector3.Dot(vector3, vector3);
				}
				else
				{
					Vector3 vector4 = vector2 - vector * (num / Vector3.Dot(vector, vector));
					result = Vector3.Dot(vector4, vector4);
				}
			}
			return result;
		}

		public static char ToLowerFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1;
			char result;
			if (flag)
			{
				result = c;
			}
			else
			{
				result = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
			}
			return result;
		}

		public static char ToUpperFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1;
			char result;
			if (flag)
			{
				result = c;
			}
			else
			{
				result = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
			}
			return result;
		}

		public static uint ToUpperASCIIFast(uint c)
		{
			bool flag = (ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1));
			uint result;
			if (flag)
			{
				result = c;
			}
			else
			{
				result = (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
			}
			return result;
		}

		public static uint ToLowerASCIIFast(uint c)
		{
			bool flag = (ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1));
			uint result;
			if (flag)
			{
				result = c;
			}
			else
			{
				result = (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
			}
			return result;
		}

		public static int GetHashCodeCaseSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num ^ (int)s[i]);
			}
			return num;
		}

		public static int GetHashCodeCaseInSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num ^ (int)TextUtilities.ToUpperASCIIFast((uint)s[i]));
			}
			return num;
		}

		public static uint GetSimpleHashCodeLowercase(string s)
		{
			uint num = 5381u;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num ^ (uint)TextUtilities.ToLowerFast(s[i]));
			}
			return num;
		}

		public static int HexToInt(char hex)
		{
			int result;
			switch (hex)
			{
			case '0':
				result = 0;
				return result;
			case '1':
				result = 1;
				return result;
			case '2':
				result = 2;
				return result;
			case '3':
				result = 3;
				return result;
			case '4':
				result = 4;
				return result;
			case '5':
				result = 5;
				return result;
			case '6':
				result = 6;
				return result;
			case '7':
				result = 7;
				return result;
			case '8':
				result = 8;
				return result;
			case '9':
				result = 9;
				return result;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				result = 10;
				return result;
			case 'B':
				result = 11;
				return result;
			case 'C':
				result = 12;
				return result;
			case 'D':
				result = 13;
				return result;
			case 'E':
				result = 14;
				return result;
			case 'F':
				result = 15;
				return result;
			default:
				switch (hex)
				{
				case 'a':
					result = 10;
					return result;
				case 'b':
					result = 11;
					return result;
				case 'c':
					result = 12;
					return result;
				case 'd':
					result = 13;
					return result;
				case 'e':
					result = 14;
					return result;
				case 'f':
					result = 15;
					return result;
				}
				break;
			}
			result = 15;
			return result;
		}

		public static int StringHexToInt(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num += TextUtilities.HexToInt(s[i]) * (int)Mathf.Pow(16f, (float)(s.Length - 1 - i));
			}
			return num;
		}
	}
}
