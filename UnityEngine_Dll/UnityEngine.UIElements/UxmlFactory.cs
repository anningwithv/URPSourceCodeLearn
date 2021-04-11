using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public class UxmlFactory<TCreatedType, TTraits> : IUxmlFactory where TCreatedType : VisualElement, new() where TTraits : UxmlTraits, new()
	{
		internal TTraits m_Traits;

		public virtual string uxmlName
		{
			get
			{
				return typeof(TCreatedType).Name;
			}
		}

		public virtual string uxmlNamespace
		{
			get
			{
				return typeof(TCreatedType).Namespace ?? string.Empty;
			}
		}

		public virtual string uxmlQualifiedName
		{
			get
			{
				return typeof(TCreatedType).FullName;
			}
		}

		public bool canHaveAnyAttribute
		{
			get
			{
				return this.m_Traits.canHaveAnyAttribute;
			}
		}

		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				foreach (UxmlAttributeDescription uxmlAttributeDescription in this.m_Traits.uxmlAttributesDescription)
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
				foreach (UxmlChildElementDescription uxmlChildElementDescription in this.m_Traits.uxmlChildElementsDescription)
				{
					yield return uxmlChildElementDescription;
					uxmlChildElementDescription = null;
				}
				IEnumerator<UxmlChildElementDescription> enumerator = null;
				yield break;
				yield break;
			}
		}

		public virtual string substituteForTypeName
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = typeof(VisualElement).Name;
				}
				return result;
			}
		}

		public virtual string substituteForTypeNamespace
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = (typeof(VisualElement).Namespace ?? string.Empty);
				}
				return result;
			}
		}

		public virtual string substituteForTypeQualifiedName
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = typeof(VisualElement).FullName;
				}
				return result;
			}
		}

		protected UxmlFactory()
		{
			this.m_Traits = Activator.CreateInstance<TTraits>();
		}

		public virtual bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc)
		{
			return true;
		}

		public virtual VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			TCreatedType tCreatedType = Activator.CreateInstance<TCreatedType>();
			this.m_Traits.Init(tCreatedType, bag, cc);
			return tCreatedType;
		}
	}
	public class UxmlFactory<TCreatedType> : UxmlFactory<TCreatedType, VisualElement.UxmlTraits> where TCreatedType : VisualElement, new()
	{
	}
}
