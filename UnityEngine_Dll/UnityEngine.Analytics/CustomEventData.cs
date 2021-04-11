using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Analytics
{
	[NativeHeader("Modules/UnityAnalytics/Public/Events/UserCustomEvent.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class CustomEventData : IDisposable
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		private CustomEventData()
		{
		}

		public CustomEventData(string name)
		{
			this.m_Ptr = CustomEventData.Internal_Create(this, name);
		}

		~CustomEventData()
		{
			this.Destroy();
		}

		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				CustomEventData.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		public void Dispose()
		{
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create(CustomEventData ced, string name);

		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddString(string key, string value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddInt32(string key, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddUInt32(string key, uint value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddInt64(string key, long value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddUInt64(string key, ulong value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddBool(string key, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddDouble(string key, double value);

		public bool AddDictionary(IDictionary<string, object> eventData)
		{
			foreach (KeyValuePair<string, object> current in eventData)
			{
				string key = current.Key;
				object value = current.Value;
				bool flag = value == null;
				if (flag)
				{
					this.AddString(key, "null");
				}
				else
				{
					Type type = value.GetType();
					bool flag2 = type == typeof(string);
					if (flag2)
					{
						this.AddString(key, (string)value);
					}
					else
					{
						bool flag3 = type == typeof(char);
						if (flag3)
						{
							this.AddString(key, char.ToString((char)value));
						}
						else
						{
							bool flag4 = type == typeof(sbyte);
							if (flag4)
							{
								this.AddInt32(key, (int)((sbyte)value));
							}
							else
							{
								bool flag5 = type == typeof(byte);
								if (flag5)
								{
									this.AddInt32(key, (int)((byte)value));
								}
								else
								{
									bool flag6 = type == typeof(short);
									if (flag6)
									{
										this.AddInt32(key, (int)((short)value));
									}
									else
									{
										bool flag7 = type == typeof(ushort);
										if (flag7)
										{
											this.AddUInt32(key, (uint)((ushort)value));
										}
										else
										{
											bool flag8 = type == typeof(int);
											if (flag8)
											{
												this.AddInt32(key, (int)value);
											}
											else
											{
												bool flag9 = type == typeof(uint);
												if (flag9)
												{
													this.AddUInt32(current.Key, (uint)value);
												}
												else
												{
													bool flag10 = type == typeof(long);
													if (flag10)
													{
														this.AddInt64(key, (long)value);
													}
													else
													{
														bool flag11 = type == typeof(ulong);
														if (flag11)
														{
															this.AddUInt64(key, (ulong)value);
														}
														else
														{
															bool flag12 = type == typeof(bool);
															if (flag12)
															{
																this.AddBool(key, (bool)value);
															}
															else
															{
																bool flag13 = type == typeof(float);
																if (flag13)
																{
																	this.AddDouble(key, (double)Convert.ToDecimal((float)value));
																}
																else
																{
																	bool flag14 = type == typeof(double);
																	if (flag14)
																	{
																		this.AddDouble(key, (double)value);
																	}
																	else
																	{
																		bool flag15 = type == typeof(decimal);
																		if (flag15)
																		{
																			this.AddDouble(key, (double)Convert.ToDecimal((decimal)value));
																		}
																		else
																		{
																			bool isValueType = type.IsValueType;
																			if (!isValueType)
																			{
																				throw new ArgumentException(string.Format("Invalid type: {0} passed", type));
																			}
																			this.AddString(key, value.ToString());
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
							}
						}
					}
				}
			}
			return true;
		}
	}
}
