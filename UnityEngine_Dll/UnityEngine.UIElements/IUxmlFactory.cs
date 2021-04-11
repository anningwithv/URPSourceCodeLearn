using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	public interface IUxmlFactory
	{
		string uxmlName
		{
			get;
		}

		string uxmlNamespace
		{
			get;
		}

		string uxmlQualifiedName
		{
			get;
		}

		bool canHaveAnyAttribute
		{
			get;
		}

		IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get;
		}

		IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get;
		}

		string substituteForTypeName
		{
			get;
		}

		string substituteForTypeNamespace
		{
			get;
		}

		string substituteForTypeQualifiedName
		{
			get;
		}

		bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc);

		VisualElement Create(IUxmlAttributes bag, CreationContext cc);
	}
}
