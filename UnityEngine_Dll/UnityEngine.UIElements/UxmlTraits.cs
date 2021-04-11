using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public abstract class UxmlTraits
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly UxmlTraits.<>c <>9 = new UxmlTraits.<>c();

			public static Func<FieldInfo, bool> <>9__10_0;

			internal bool <GetAllAttributeDescriptionForType>b__10_0(FieldInfo f)
			{
				return typeof(UxmlAttributeDescription).IsAssignableFrom(f.FieldType);
			}
		}

		public bool canHaveAnyAttribute
		{
			get;
			protected set;
		}

		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				foreach (UxmlAttributeDescription uxmlAttributeDescription in this.GetAllAttributeDescriptionForType(base.GetType()))
				{
					yield return uxmlAttributeDescription;
					uxmlAttributeDescription = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
				yield break;
				yield break;
			}
		}

		public virtual IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		protected UxmlTraits()
		{
			this.canHaveAnyAttribute = true;
		}

		public virtual void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
		}

		private IEnumerable<UxmlAttributeDescription> GetAllAttributeDescriptionForType(Type t)
		{
			Type baseType = t.BaseType;
			bool flag = baseType != null;
			if (flag)
			{
				foreach (UxmlAttributeDescription uxmlAttributeDescription in this.GetAllAttributeDescriptionForType(baseType))
				{
					yield return uxmlAttributeDescription;
					uxmlAttributeDescription = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
			}
			IEnumerable<FieldInfo> arg_105_0 = t.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			Func<FieldInfo, bool> arg_105_1;
			if ((arg_105_1 = UxmlTraits.<>c.<>9__10_0) == null)
			{
				arg_105_1 = (UxmlTraits.<>c.<>9__10_0 = new Func<FieldInfo, bool>(UxmlTraits.<>c.<>9.<GetAllAttributeDescriptionForType>b__10_0));
			}
			foreach (FieldInfo fieldInfo in arg_105_0.Where(arg_105_1))
			{
				yield return (UxmlAttributeDescription)fieldInfo.GetValue(this);
				fieldInfo = null;
			}
			IEnumerator<FieldInfo> enumerator2 = null;
			yield break;
			yield break;
		}
	}
}
