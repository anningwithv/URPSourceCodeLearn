using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal class AttributeHelperEngine
	{
		[RequiredByNativeCode]
		private static Type GetParentTypeDisallowingMultipleInclusion(Type type)
		{
			Type result = null;
			while (type != null && type != typeof(MonoBehaviour))
			{
				bool flag = Attribute.IsDefined(type, typeof(DisallowMultipleComponent));
				if (flag)
				{
					result = type;
				}
				type = type.BaseType;
			}
			return result;
		}

		[RequiredByNativeCode]
		private static Type[] GetRequiredComponents(Type klass)
		{
			List<Type> list = null;
			Type[] result;
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				RequireComponent[] array = (RequireComponent[])klass.GetCustomAttributes(typeof(RequireComponent), false);
				Type baseType = klass.BaseType;
				RequireComponent[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RequireComponent requireComponent = array2[i];
					bool flag = list == null && array.Length == 1 && baseType == typeof(MonoBehaviour);
					if (flag)
					{
						Type[] array3 = new Type[]
						{
							requireComponent.m_Type0,
							requireComponent.m_Type1,
							requireComponent.m_Type2
						};
						result = array3;
						return result;
					}
					bool flag2 = list == null;
					if (flag2)
					{
						list = new List<Type>();
					}
					bool flag3 = requireComponent.m_Type0 != null;
					if (flag3)
					{
						list.Add(requireComponent.m_Type0);
					}
					bool flag4 = requireComponent.m_Type1 != null;
					if (flag4)
					{
						list.Add(requireComponent.m_Type1);
					}
					bool flag5 = requireComponent.m_Type2 != null;
					if (flag5)
					{
						list.Add(requireComponent.m_Type2);
					}
				}
				klass = baseType;
			}
			bool flag6 = list == null;
			if (flag6)
			{
				result = null;
				return result;
			}
			result = list.ToArray();
			return result;
		}

		private static int GetExecuteMode(Type klass)
		{
			object[] customAttributes = klass.GetCustomAttributes(typeof(ExecuteAlways), false);
			bool flag = customAttributes.Length != 0;
			int result;
			if (flag)
			{
				result = 2;
			}
			else
			{
				object[] customAttributes2 = klass.GetCustomAttributes(typeof(ExecuteInEditMode), false);
				bool flag2 = customAttributes2.Length != 0;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		[RequiredByNativeCode]
		private static int CheckIsEditorScript(Type klass)
		{
			int result;
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				int executeMode = AttributeHelperEngine.GetExecuteMode(klass);
				bool flag = executeMode > 0;
				if (flag)
				{
					result = executeMode;
					return result;
				}
				klass = klass.BaseType;
			}
			result = 0;
			return result;
		}

		[RequiredByNativeCode]
		private static int GetDefaultExecutionOrderFor(Type klass)
		{
			DefaultExecutionOrder customAttributeOfType = AttributeHelperEngine.GetCustomAttributeOfType<DefaultExecutionOrder>(klass);
			bool flag = customAttributeOfType == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = customAttributeOfType.order;
			}
			return result;
		}

		private static T GetCustomAttributeOfType<T>(Type klass) where T : Attribute
		{
			Type typeFromHandle = typeof(T);
			object[] customAttributes = klass.GetCustomAttributes(typeFromHandle, true);
			bool flag = customAttributes != null && customAttributes.Length != 0;
			T result;
			if (flag)
			{
				result = (T)((object)customAttributes[0]);
			}
			else
			{
				result = default(T);
			}
			return result;
		}
	}
}
