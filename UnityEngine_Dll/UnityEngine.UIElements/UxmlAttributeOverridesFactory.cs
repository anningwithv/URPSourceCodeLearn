using System;

namespace UnityEngine.UIElements
{
	public class UxmlAttributeOverridesFactory : UxmlFactory<VisualElement, UxmlAttributeOverridesTraits>
	{
		internal const string k_ElementName = "AttributeOverrides";

		public override string uxmlName
		{
			get
			{
				return "AttributeOverrides";
			}
		}

		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}
	}
}
