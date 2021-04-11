using System;

namespace UnityEngine.Events
{
	internal class UnityEventTools
	{
		internal static string TidyAssemblyTypeName(string assemblyTypeName)
		{
			bool flag = string.IsNullOrEmpty(assemblyTypeName);
			string result;
			if (flag)
			{
				result = assemblyTypeName;
			}
			else
			{
				int num = 2147483647;
				int num2 = assemblyTypeName.IndexOf(", Version=");
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num = Math.Min(num2, num);
				}
				num2 = assemblyTypeName.IndexOf(", Culture=");
				bool flag3 = num2 != -1;
				if (flag3)
				{
					num = Math.Min(num2, num);
				}
				num2 = assemblyTypeName.IndexOf(", PublicKeyToken=");
				bool flag4 = num2 != -1;
				if (flag4)
				{
					num = Math.Min(num2, num);
				}
				bool flag5 = num != 2147483647;
				if (flag5)
				{
					assemblyTypeName = assemblyTypeName.Substring(0, num);
				}
				num2 = assemblyTypeName.IndexOf(", UnityEngine.");
				bool flag6 = num2 != -1 && assemblyTypeName.EndsWith("Module");
				if (flag6)
				{
					assemblyTypeName = assemblyTypeName.Substring(0, num2) + ", UnityEngine";
				}
				result = assemblyTypeName;
			}
			return result;
		}
	}
}
