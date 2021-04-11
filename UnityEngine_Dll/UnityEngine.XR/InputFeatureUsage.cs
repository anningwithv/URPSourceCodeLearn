using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	[NativeConditional("ENABLE_VR"), NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h"), RequiredByNativeCode]
	public struct InputFeatureUsage : IEquatable<InputFeatureUsage>
	{
		internal string m_Name;

		[NativeName("m_FeatureType")]
		internal InputFeatureType m_InternalType;

		public string name
		{
			get
			{
				return this.m_Name;
			}
			internal set
			{
				this.m_Name = value;
			}
		}

		internal InputFeatureType internalType
		{
			get
			{
				return this.m_InternalType;
			}
			set
			{
				this.m_InternalType = value;
			}
		}

		public Type type
		{
			get
			{
				Type typeFromHandle;
				switch (this.m_InternalType)
				{
				case InputFeatureType.Custom:
					typeFromHandle = typeof(byte[]);
					break;
				case InputFeatureType.Binary:
					typeFromHandle = typeof(bool);
					break;
				case InputFeatureType.DiscreteStates:
					typeFromHandle = typeof(uint);
					break;
				case InputFeatureType.Axis1D:
					typeFromHandle = typeof(float);
					break;
				case InputFeatureType.Axis2D:
					typeFromHandle = typeof(Vector2);
					break;
				case InputFeatureType.Axis3D:
					typeFromHandle = typeof(Vector3);
					break;
				case InputFeatureType.Rotation:
					typeFromHandle = typeof(Quaternion);
					break;
				case InputFeatureType.Hand:
					typeFromHandle = typeof(Hand);
					break;
				case InputFeatureType.Bone:
					typeFromHandle = typeof(Bone);
					break;
				case InputFeatureType.Eyes:
					typeFromHandle = typeof(Eyes);
					break;
				default:
					throw new InvalidCastException("No valid managed type for unknown native type.");
				}
				return typeFromHandle;
			}
		}

		internal InputFeatureUsage(string name, InputFeatureType type)
		{
			this.m_Name = name;
			this.m_InternalType = type;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputFeatureUsage);
			return !flag && this.Equals((InputFeatureUsage)obj);
		}

		public bool Equals(InputFeatureUsage other)
		{
			return this.name == other.name && this.internalType == other.internalType;
		}

		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ this.internalType.GetHashCode() << 1;
		}

		public static bool operator ==(InputFeatureUsage a, InputFeatureUsage b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(InputFeatureUsage a, InputFeatureUsage b)
		{
			return !(a == b);
		}

		public InputFeatureUsage<T> As<T>()
		{
			bool flag = this.type != typeof(T);
			if (flag)
			{
				throw new ArgumentException("InputFeatureUsage type does not match out variable type.");
			}
			return new InputFeatureUsage<T>(this.name);
		}
	}
	public struct InputFeatureUsage<T> : IEquatable<InputFeatureUsage<T>>
	{
		public string name
		{
			[IsReadOnly]
			get;
			set;
		}

		private Type usageType
		{
			get
			{
				return typeof(T);
			}
		}

		public InputFeatureUsage(string usageName)
		{
			this.name = usageName;
		}

		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputFeatureUsage<T>);
			return !flag && this.Equals((InputFeatureUsage<T>)obj);
		}

		public bool Equals(InputFeatureUsage<T> other)
		{
			return this.name == other.name;
		}

		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		public static bool operator ==(InputFeatureUsage<T> a, InputFeatureUsage<T> b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(InputFeatureUsage<T> a, InputFeatureUsage<T> b)
		{
			return !(a == b);
		}

		public static explicit operator InputFeatureUsage(InputFeatureUsage<T> self)
		{
			InputFeatureType inputFeatureType = (InputFeatureType)4294967295u;
			Type usageType = self.usageType;
			bool flag = usageType == typeof(bool);
			if (flag)
			{
				inputFeatureType = InputFeatureType.Binary;
			}
			else
			{
				bool flag2 = usageType == typeof(uint);
				if (flag2)
				{
					inputFeatureType = InputFeatureType.DiscreteStates;
				}
				else
				{
					bool flag3 = usageType == typeof(float);
					if (flag3)
					{
						inputFeatureType = InputFeatureType.Axis1D;
					}
					else
					{
						bool flag4 = usageType == typeof(Vector2);
						if (flag4)
						{
							inputFeatureType = InputFeatureType.Axis2D;
						}
						else
						{
							bool flag5 = usageType == typeof(Vector3);
							if (flag5)
							{
								inputFeatureType = InputFeatureType.Axis3D;
							}
							else
							{
								bool flag6 = usageType == typeof(Quaternion);
								if (flag6)
								{
									inputFeatureType = InputFeatureType.Rotation;
								}
								else
								{
									bool flag7 = usageType == typeof(Hand);
									if (flag7)
									{
										inputFeatureType = InputFeatureType.Hand;
									}
									else
									{
										bool flag8 = usageType == typeof(Bone);
										if (flag8)
										{
											inputFeatureType = InputFeatureType.Bone;
										}
										else
										{
											bool flag9 = usageType == typeof(Eyes);
											if (flag9)
											{
												inputFeatureType = InputFeatureType.Eyes;
											}
											else
											{
												bool flag10 = usageType == typeof(byte[]);
												if (flag10)
												{
													inputFeatureType = InputFeatureType.Custom;
												}
												else
												{
													bool isEnum = usageType.IsEnum;
													if (isEnum)
													{
														inputFeatureType = InputFeatureType.DiscreteStates;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag11 = inputFeatureType != (InputFeatureType)4294967295u;
			if (flag11)
			{
				return new InputFeatureUsage(self.name, inputFeatureType);
			}
			throw new InvalidCastException("No valid InputFeatureType for " + self.name + ".");
		}
	}
}
