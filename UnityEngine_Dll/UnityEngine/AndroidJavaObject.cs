using System;
using System.Text;

namespace UnityEngine
{
	public class AndroidJavaObject : IDisposable
	{
		private static bool enableDebugPrints = false;

		internal GlobalJavaObjectRef m_jobject;

		internal GlobalJavaObjectRef m_jclass;

		public AndroidJavaObject(string className, string[] args) : this()
		{
			this._AndroidJavaObject(className, new object[]
			{
				args
			});
		}

		public AndroidJavaObject(string className, AndroidJavaObject[] args) : this()
		{
			this._AndroidJavaObject(className, new object[]
			{
				args
			});
		}

		public AndroidJavaObject(string className, AndroidJavaClass[] args) : this()
		{
			this._AndroidJavaObject(className, new object[]
			{
				args
			});
		}

		public AndroidJavaObject(string className, AndroidJavaProxy[] args) : this()
		{
			this._AndroidJavaObject(className, new object[]
			{
				args
			});
		}

		public AndroidJavaObject(string className, AndroidJavaRunnable[] args) : this()
		{
			this._AndroidJavaObject(className, new object[]
			{
				args
			});
		}

		public AndroidJavaObject(string className, params object[] args) : this()
		{
			this._AndroidJavaObject(className, args);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Call<T>(string methodName, T[] args)
		{
			this._Call(methodName, new object[]
			{
				args
			});
		}

		public void Call(string methodName, params object[] args)
		{
			this._Call(methodName, args);
		}

		public void CallStatic<T>(string methodName, T[] args)
		{
			this._CallStatic(methodName, new object[]
			{
				args
			});
		}

		public void CallStatic(string methodName, params object[] args)
		{
			this._CallStatic(methodName, args);
		}

		public FieldType Get<FieldType>(string fieldName)
		{
			return this._Get<FieldType>(fieldName);
		}

		public void Set<FieldType>(string fieldName, FieldType val)
		{
			this._Set<FieldType>(fieldName, val);
		}

		public FieldType GetStatic<FieldType>(string fieldName)
		{
			return this._GetStatic<FieldType>(fieldName);
		}

		public void SetStatic<FieldType>(string fieldName, FieldType val)
		{
			this._SetStatic<FieldType>(fieldName, val);
		}

		public IntPtr GetRawObject()
		{
			return this._GetRawObject();
		}

		public IntPtr GetRawClass()
		{
			return this._GetRawClass();
		}

		public ReturnType Call<ReturnType, T>(string methodName, T[] args)
		{
			return this._Call<ReturnType>(methodName, new object[]
			{
				args
			});
		}

		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return this._Call<ReturnType>(methodName, args);
		}

		public ReturnType CallStatic<ReturnType, T>(string methodName, T[] args)
		{
			return this._CallStatic<ReturnType>(methodName, new object[]
			{
				args
			});
		}

		public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return this._CallStatic<ReturnType>(methodName, args);
		}

		protected void DebugPrint(string msg)
		{
			bool flag = !AndroidJavaObject.enableDebugPrints;
			if (!flag)
			{
				Debug.Log(msg);
			}
		}

		protected void DebugPrint(string call, string methodName, string signature, object[] args)
		{
			bool flag = !AndroidJavaObject.enableDebugPrints;
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < args.Length; i++)
				{
					object obj = args[i];
					stringBuilder.Append(", ");
					stringBuilder.Append((obj == null) ? "<null>" : obj.GetType().ToString());
				}
				string[] expr_61 = new string[7];
				expr_61[0] = call;
				expr_61[1] = "(\"";
				expr_61[2] = methodName;
				expr_61[3] = "\"";
				int arg_88_1 = 4;
				StringBuilder expr_7C = stringBuilder;
				expr_61[arg_88_1] = ((expr_7C != null) ? expr_7C.ToString() : null);
				expr_61[5] = ") = ";
				expr_61[6] = signature;
				Debug.Log(string.Concat(expr_61));
			}
		}

		private void _AndroidJavaObject(string className, params object[] args)
		{
			this.DebugPrint("Creating AndroidJavaObject from " + className);
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr jobject = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(jobject);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				IntPtr constructorID = AndroidJNIHelper.GetConstructorID(this.m_jclass, args);
				IntPtr intPtr = AndroidJNISafe.NewObject(this.m_jclass, constructorID, array);
				this.m_jobject = new GlobalJavaObjectRef(intPtr);
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		internal AndroidJavaObject(IntPtr jobject) : this()
		{
			bool flag = jobject == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaObject with null ptr!");
			}
			IntPtr objectClass = AndroidJNISafe.GetObjectClass(jobject);
			this.m_jobject = new GlobalJavaObjectRef(jobject);
			this.m_jclass = new GlobalJavaObjectRef(objectClass);
			AndroidJNISafe.DeleteLocalRef(objectClass);
		}

		internal AndroidJavaObject()
		{
		}

		~AndroidJavaObject()
		{
			this.Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_jobject != null;
			if (flag)
			{
				this.m_jobject.Dispose();
				this.m_jobject = null;
			}
			bool flag2 = this.m_jclass != null;
			if (flag2)
			{
				this.m_jclass.Dispose();
				this.m_jclass = null;
			}
		}

		protected void _Call(string methodName, params object[] args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, false);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				AndroidJNISafe.CallVoidMethod(this.m_jobject, methodID, array);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		protected ReturnType _Call<ReturnType>(string methodName, params object[] args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, false);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			ReturnType result;
			try
			{
				bool flag2 = AndroidReflection.IsPrimitive(typeof(ReturnType));
				if (flag2)
				{
					bool flag3 = typeof(ReturnType) == typeof(int);
					if (flag3)
					{
						result = (ReturnType)((object)AndroidJNISafe.CallIntMethod(this.m_jobject, methodID, array));
					}
					else
					{
						bool flag4 = typeof(ReturnType) == typeof(bool);
						if (flag4)
						{
							result = (ReturnType)((object)AndroidJNISafe.CallBooleanMethod(this.m_jobject, methodID, array));
						}
						else
						{
							bool flag5 = typeof(ReturnType) == typeof(byte);
							if (flag5)
							{
								Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
								result = (ReturnType)((object)((byte)AndroidJNISafe.CallSByteMethod(this.m_jobject, methodID, array)));
							}
							else
							{
								bool flag6 = typeof(ReturnType) == typeof(sbyte);
								if (flag6)
								{
									result = (ReturnType)((object)AndroidJNISafe.CallSByteMethod(this.m_jobject, methodID, array));
								}
								else
								{
									bool flag7 = typeof(ReturnType) == typeof(short);
									if (flag7)
									{
										result = (ReturnType)((object)AndroidJNISafe.CallShortMethod(this.m_jobject, methodID, array));
									}
									else
									{
										bool flag8 = typeof(ReturnType) == typeof(long);
										if (flag8)
										{
											result = (ReturnType)((object)AndroidJNISafe.CallLongMethod(this.m_jobject, methodID, array));
										}
										else
										{
											bool flag9 = typeof(ReturnType) == typeof(float);
											if (flag9)
											{
												result = (ReturnType)((object)AndroidJNISafe.CallFloatMethod(this.m_jobject, methodID, array));
											}
											else
											{
												bool flag10 = typeof(ReturnType) == typeof(double);
												if (flag10)
												{
													result = (ReturnType)((object)AndroidJNISafe.CallDoubleMethod(this.m_jobject, methodID, array));
												}
												else
												{
													bool flag11 = typeof(ReturnType) == typeof(char);
													if (flag11)
													{
														result = (ReturnType)((object)AndroidJNISafe.CallCharMethod(this.m_jobject, methodID, array));
													}
													else
													{
														result = default(ReturnType);
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
				else
				{
					bool flag12 = typeof(ReturnType) == typeof(string);
					if (flag12)
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStringMethod(this.m_jobject, methodID, array));
					}
					else
					{
						bool flag13 = typeof(ReturnType) == typeof(AndroidJavaClass);
						if (flag13)
						{
							IntPtr intPtr = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
							result = ((intPtr == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(intPtr))));
						}
						else
						{
							bool flag14 = typeof(ReturnType) == typeof(AndroidJavaObject);
							if (flag14)
							{
								IntPtr intPtr2 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
								result = ((intPtr2 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(intPtr2))));
							}
							else
							{
								bool flag15 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType));
								if (!flag15)
								{
									string arg_408_0 = "JNI: Unknown return type '";
									Type expr_3F7 = typeof(ReturnType);
									throw new Exception(arg_408_0 + ((expr_3F7 != null) ? expr_3F7.ToString() : null) + "'");
								}
								IntPtr intPtr3 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
								result = ((intPtr3 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(intPtr3))));
							}
						}
					}
				}
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
			return result;
		}

		protected FieldType _Get<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			FieldType result;
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					result = (FieldType)((object)AndroidJNISafe.GetIntField(this.m_jobject, fieldID));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						result = (FieldType)((object)AndroidJNISafe.GetBooleanField(this.m_jobject, fieldID));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java get field call is obsolete, use field type <SByte> instead");
							result = (FieldType)((object)((byte)AndroidJNISafe.GetSByteField(this.m_jobject, fieldID)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								result = (FieldType)((object)AndroidJNISafe.GetSByteField(this.m_jobject, fieldID));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									result = (FieldType)((object)AndroidJNISafe.GetShortField(this.m_jobject, fieldID));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										result = (FieldType)((object)AndroidJNISafe.GetLongField(this.m_jobject, fieldID));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											result = (FieldType)((object)AndroidJNISafe.GetFloatField(this.m_jobject, fieldID));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												result = (FieldType)((object)AndroidJNISafe.GetDoubleField(this.m_jobject, fieldID));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													result = (FieldType)((object)AndroidJNISafe.GetCharField(this.m_jobject, fieldID));
												}
												else
												{
													result = default(FieldType);
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
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					result = (FieldType)((object)AndroidJNISafe.GetStringField(this.m_jobject, fieldID));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						IntPtr objectField = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
						result = ((objectField == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(objectField))));
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							IntPtr objectField2 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
							result = ((objectField2 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(objectField2))));
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								string arg_3D3_0 = "JNI: Unknown field type '";
								Type expr_3C2 = typeof(FieldType);
								throw new Exception(arg_3D3_0 + ((expr_3C2 != null) ? expr_3C2.ToString() : null) + "'");
							}
							IntPtr objectField3 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
							result = ((objectField3 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJNIHelper.ConvertFromJNIArray<FieldType>(objectField3))));
						}
					}
				}
			}
			return result;
		}

		protected void _Set<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					AndroidJNISafe.SetIntField(this.m_jobject, fieldID, (int)((object)val));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						AndroidJNISafe.SetBooleanField(this.m_jobject, fieldID, (bool)((object)val));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java set field call is obsolete, use field type <SByte> instead");
							AndroidJNISafe.SetSByteField(this.m_jobject, fieldID, (sbyte)((byte)((object)val)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								AndroidJNISafe.SetSByteField(this.m_jobject, fieldID, (sbyte)((object)val));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									AndroidJNISafe.SetShortField(this.m_jobject, fieldID, (short)((object)val));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										AndroidJNISafe.SetLongField(this.m_jobject, fieldID, (long)((object)val));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											AndroidJNISafe.SetFloatField(this.m_jobject, fieldID, (float)((object)val));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												AndroidJNISafe.SetDoubleField(this.m_jobject, fieldID, (double)((object)val));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													AndroidJNISafe.SetCharField(this.m_jobject, fieldID, (char)((object)val));
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
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					AndroidJNISafe.SetStringField(this.m_jobject, fieldID, (string)((object)val));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaClass)((object)val)).m_jclass);
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaObject)((object)val)).m_jobject);
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								string arg_3B5_0 = "JNI: Unknown field type '";
								Type expr_3A4 = typeof(FieldType);
								throw new Exception(arg_3B5_0 + ((expr_3A4 != null) ? expr_3A4.ToString() : null) + "'");
							}
							IntPtr val2 = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
							AndroidJNISafe.SetObjectField(this.m_jclass, fieldID, val2);
						}
					}
				}
			}
		}

		protected void _CallStatic(string methodName, params object[] args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, true);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				AndroidJNISafe.CallStaticVoidMethod(this.m_jclass, methodID, array);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		protected ReturnType _CallStatic<ReturnType>(string methodName, params object[] args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, true);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			ReturnType result;
			try
			{
				bool flag2 = AndroidReflection.IsPrimitive(typeof(ReturnType));
				if (flag2)
				{
					bool flag3 = typeof(ReturnType) == typeof(int);
					if (flag3)
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticIntMethod(this.m_jclass, methodID, array));
					}
					else
					{
						bool flag4 = typeof(ReturnType) == typeof(bool);
						if (flag4)
						{
							result = (ReturnType)((object)AndroidJNISafe.CallStaticBooleanMethod(this.m_jclass, methodID, array));
						}
						else
						{
							bool flag5 = typeof(ReturnType) == typeof(byte);
							if (flag5)
							{
								Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
								result = (ReturnType)((object)((byte)AndroidJNISafe.CallStaticSByteMethod(this.m_jclass, methodID, array)));
							}
							else
							{
								bool flag6 = typeof(ReturnType) == typeof(sbyte);
								if (flag6)
								{
									result = (ReturnType)((object)AndroidJNISafe.CallStaticSByteMethod(this.m_jclass, methodID, array));
								}
								else
								{
									bool flag7 = typeof(ReturnType) == typeof(short);
									if (flag7)
									{
										result = (ReturnType)((object)AndroidJNISafe.CallStaticShortMethod(this.m_jclass, methodID, array));
									}
									else
									{
										bool flag8 = typeof(ReturnType) == typeof(long);
										if (flag8)
										{
											result = (ReturnType)((object)AndroidJNISafe.CallStaticLongMethod(this.m_jclass, methodID, array));
										}
										else
										{
											bool flag9 = typeof(ReturnType) == typeof(float);
											if (flag9)
											{
												result = (ReturnType)((object)AndroidJNISafe.CallStaticFloatMethod(this.m_jclass, methodID, array));
											}
											else
											{
												bool flag10 = typeof(ReturnType) == typeof(double);
												if (flag10)
												{
													result = (ReturnType)((object)AndroidJNISafe.CallStaticDoubleMethod(this.m_jclass, methodID, array));
												}
												else
												{
													bool flag11 = typeof(ReturnType) == typeof(char);
													if (flag11)
													{
														result = (ReturnType)((object)AndroidJNISafe.CallStaticCharMethod(this.m_jclass, methodID, array));
													}
													else
													{
														result = default(ReturnType);
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
				else
				{
					bool flag12 = typeof(ReturnType) == typeof(string);
					if (flag12)
					{
						result = (ReturnType)((object)AndroidJNISafe.CallStaticStringMethod(this.m_jclass, methodID, array));
					}
					else
					{
						bool flag13 = typeof(ReturnType) == typeof(AndroidJavaClass);
						if (flag13)
						{
							IntPtr intPtr = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
							result = ((intPtr == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(intPtr))));
						}
						else
						{
							bool flag14 = typeof(ReturnType) == typeof(AndroidJavaObject);
							if (flag14)
							{
								IntPtr intPtr2 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
								result = ((intPtr2 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(intPtr2))));
							}
							else
							{
								bool flag15 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType));
								if (!flag15)
								{
									string arg_408_0 = "JNI: Unknown return type '";
									Type expr_3F7 = typeof(ReturnType);
									throw new Exception(arg_408_0 + ((expr_3F7 != null) ? expr_3F7.ToString() : null) + "'");
								}
								IntPtr intPtr3 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
								result = ((intPtr3 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(intPtr3))));
							}
						}
					}
				}
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
			return result;
		}

		protected FieldType _GetStatic<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			FieldType result;
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					result = (FieldType)((object)AndroidJNISafe.GetStaticIntField(this.m_jclass, fieldID));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						result = (FieldType)((object)AndroidJNISafe.GetStaticBooleanField(this.m_jclass, fieldID));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java get field call is obsolete, use field type <SByte> instead");
							result = (FieldType)((object)((byte)AndroidJNISafe.GetStaticSByteField(this.m_jclass, fieldID)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								result = (FieldType)((object)AndroidJNISafe.GetStaticSByteField(this.m_jclass, fieldID));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									result = (FieldType)((object)AndroidJNISafe.GetStaticShortField(this.m_jclass, fieldID));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										result = (FieldType)((object)AndroidJNISafe.GetStaticLongField(this.m_jclass, fieldID));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											result = (FieldType)((object)AndroidJNISafe.GetStaticFloatField(this.m_jclass, fieldID));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												result = (FieldType)((object)AndroidJNISafe.GetStaticDoubleField(this.m_jclass, fieldID));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													result = (FieldType)((object)AndroidJNISafe.GetStaticCharField(this.m_jclass, fieldID));
												}
												else
												{
													result = default(FieldType);
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
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					result = (FieldType)((object)AndroidJNISafe.GetStaticStringField(this.m_jclass, fieldID));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						IntPtr staticObjectField = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
						result = ((staticObjectField == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(staticObjectField))));
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							IntPtr staticObjectField2 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
							result = ((staticObjectField2 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(staticObjectField2))));
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								string arg_3D3_0 = "JNI: Unknown field type '";
								Type expr_3C2 = typeof(FieldType);
								throw new Exception(arg_3D3_0 + ((expr_3C2 != null) ? expr_3C2.ToString() : null) + "'");
							}
							IntPtr staticObjectField3 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
							result = ((staticObjectField3 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJNIHelper.ConvertFromJNIArray<FieldType>(staticObjectField3))));
						}
					}
				}
			}
			return result;
		}

		protected void _SetStatic<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					AndroidJNISafe.SetStaticIntField(this.m_jclass, fieldID, (int)((object)val));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						AndroidJNISafe.SetStaticBooleanField(this.m_jclass, fieldID, (bool)((object)val));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java set field call is obsolete, use field type <SByte> instead");
							AndroidJNISafe.SetStaticSByteField(this.m_jclass, fieldID, (sbyte)((byte)((object)val)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								AndroidJNISafe.SetStaticSByteField(this.m_jclass, fieldID, (sbyte)((object)val));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									AndroidJNISafe.SetStaticShortField(this.m_jclass, fieldID, (short)((object)val));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										AndroidJNISafe.SetStaticLongField(this.m_jclass, fieldID, (long)((object)val));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											AndroidJNISafe.SetStaticFloatField(this.m_jclass, fieldID, (float)((object)val));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												AndroidJNISafe.SetStaticDoubleField(this.m_jclass, fieldID, (double)((object)val));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													AndroidJNISafe.SetStaticCharField(this.m_jclass, fieldID, (char)((object)val));
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
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					AndroidJNISafe.SetStaticStringField(this.m_jclass, fieldID, (string)((object)val));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaClass)((object)val)).m_jclass);
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaObject)((object)val)).m_jobject);
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								string arg_3B5_0 = "JNI: Unknown field type '";
								Type expr_3A4 = typeof(FieldType);
								throw new Exception(arg_3B5_0 + ((expr_3A4 != null) ? expr_3A4.ToString() : null) + "'");
							}
							IntPtr val2 = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
							AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, val2);
						}
					}
				}
			}
		}

		internal static AndroidJavaObject AndroidJavaObjectDeleteLocalRef(IntPtr jobject)
		{
			AndroidJavaObject result;
			try
			{
				result = new AndroidJavaObject(jobject);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jobject);
			}
			return result;
		}

		internal static AndroidJavaClass AndroidJavaClassDeleteLocalRef(IntPtr jclass)
		{
			AndroidJavaClass result;
			try
			{
				result = new AndroidJavaClass(jclass);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jclass);
			}
			return result;
		}

		protected IntPtr _GetRawObject()
		{
			return this.m_jobject;
		}

		protected IntPtr _GetRawClass()
		{
			return this.m_jclass;
		}
	}
}
