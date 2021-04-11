using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	[NativeType(CodegenOptions.Custom, "ManagedCoveredMethodStats", Header = "Runtime/Scripting/ScriptingCoverage.bindings.h")]
	public struct CoveredMethodStats
	{
		public MethodBase method;

		public int totalSequencePoints;

		public int uncoveredSequencePoints;

		private string GetTypeDisplayName(Type t)
		{
			bool flag = t == typeof(int);
			string result;
			if (flag)
			{
				result = "int";
			}
			else
			{
				bool flag2 = t == typeof(bool);
				if (flag2)
				{
					result = "bool";
				}
				else
				{
					bool flag3 = t == typeof(float);
					if (flag3)
					{
						result = "float";
					}
					else
					{
						bool flag4 = t == typeof(double);
						if (flag4)
						{
							result = "double";
						}
						else
						{
							bool flag5 = t == typeof(void);
							if (flag5)
							{
								result = "void";
							}
							else
							{
								bool flag6 = t == typeof(string);
								if (flag6)
								{
									result = "string";
								}
								else
								{
									bool flag7 = t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>);
									if (flag7)
									{
										result = "System.Collections.Generic.List<" + this.GetTypeDisplayName(t.GetGenericArguments()[0]) + ">";
									}
									else
									{
										bool flag8 = t.IsArray && t.GetArrayRank() == 1;
										if (flag8)
										{
											result = this.GetTypeDisplayName(t.GetElementType()) + "[]";
										}
										else
										{
											result = t.FullName;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		public override string ToString()
		{
			bool flag = this.method == null;
			string result;
			if (flag)
			{
				result = "<no method>";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.GetTypeDisplayName(this.method.DeclaringType));
				stringBuilder.Append(".");
				stringBuilder.Append(this.method.Name);
				stringBuilder.Append("(");
				bool flag2 = false;
				ParameterInfo[] parameters = this.method.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					bool flag3 = flag2;
					if (flag3)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(this.GetTypeDisplayName(parameterInfo.ParameterType));
					stringBuilder.Append(" ");
					stringBuilder.Append(parameterInfo.Name);
					flag2 = true;
				}
				stringBuilder.Append(")");
				result = stringBuilder.ToString();
			}
			return result;
		}
	}
}
