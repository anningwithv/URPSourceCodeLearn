using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	internal class StyleSheetBuilder
	{
		public struct ComplexSelectorScope : IDisposable
		{
			private StyleSheetBuilder m_Builder;

			public ComplexSelectorScope(StyleSheetBuilder builder)
			{
				this.m_Builder = builder;
			}

			public void Dispose()
			{
				this.m_Builder.EndComplexSelector();
			}
		}

		private enum BuilderState
		{
			Init,
			Rule,
			ComplexSelector,
			Property
		}

		private StyleSheetBuilder.BuilderState m_BuilderState;

		private List<float> m_Floats = new List<float>();

		private List<Dimension> m_Dimensions = new List<Dimension>();

		private List<Color> m_Colors = new List<Color>();

		private List<string> m_Strings = new List<string>();

		private List<StyleRule> m_Rules = new List<StyleRule>();

		private List<UnityEngine.Object> m_Assets = new List<UnityEngine.Object>();

		private List<ScalableImage> m_ScalableImages = new List<ScalableImage>();

		private List<StyleComplexSelector> m_ComplexSelectors = new List<StyleComplexSelector>();

		private List<StyleProperty> m_CurrentProperties = new List<StyleProperty>();

		private List<StyleValueHandle> m_CurrentValues = new List<StyleValueHandle>();

		private StyleComplexSelector m_CurrentComplexSelector;

		private List<StyleSelector> m_CurrentSelectors = new List<StyleSelector>();

		private StyleProperty m_CurrentProperty;

		private StyleRule m_CurrentRule;

		private List<StyleSheet.ImportStruct> m_Imports = new List<StyleSheet.ImportStruct>();

		public StyleProperty currentProperty
		{
			get
			{
				return this.m_CurrentProperty;
			}
		}

		public StyleRule BeginRule(int ruleLine)
		{
			StyleSheetBuilder.Log("Beginning rule");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Init);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			this.m_CurrentRule = new StyleRule
			{
				line = ruleLine
			};
			return this.m_CurrentRule;
		}

		public StyleSheetBuilder.ComplexSelectorScope BeginComplexSelector(int specificity)
		{
			StyleSheetBuilder.Log("Begin complex selector with specificity " + specificity.ToString());
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.ComplexSelector;
			this.m_CurrentComplexSelector = new StyleComplexSelector();
			this.m_CurrentComplexSelector.specificity = specificity;
			this.m_CurrentComplexSelector.ruleIndex = this.m_Rules.Count;
			return new StyleSheetBuilder.ComplexSelectorScope(this);
		}

		public void AddSimpleSelector(StyleSelectorPart[] parts, StyleSelectorRelationship previousRelationsip)
		{
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.ComplexSelector);
			StyleSelector styleSelector = new StyleSelector();
			styleSelector.parts = parts;
			styleSelector.previousRelationship = previousRelationsip;
			string arg_38_0 = "Add simple selector ";
			StyleSelector expr_2C = styleSelector;
			StyleSheetBuilder.Log(arg_38_0 + ((expr_2C != null) ? expr_2C.ToString() : null));
			this.m_CurrentSelectors.Add(styleSelector);
		}

		public void EndComplexSelector()
		{
			StyleSheetBuilder.Log("Ending complex selector");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.ComplexSelector);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			bool flag = this.m_CurrentSelectors.Count > 0;
			if (flag)
			{
				this.m_CurrentComplexSelector.selectors = this.m_CurrentSelectors.ToArray();
				this.m_ComplexSelectors.Add(this.m_CurrentComplexSelector);
				this.m_CurrentSelectors.Clear();
			}
			this.m_CurrentComplexSelector = null;
		}

		public StyleProperty BeginProperty(string name, int line = -1)
		{
			StyleSheetBuilder.Log("Begin property named " + name);
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Property;
			this.m_CurrentProperty = new StyleProperty
			{
				name = name,
				line = line
			};
			this.m_CurrentProperties.Add(this.m_CurrentProperty);
			return this.m_CurrentProperty;
		}

		public void AddImport(StyleSheet.ImportStruct importStruct)
		{
			this.m_Imports.Add(importStruct);
		}

		public void AddValue(float value)
		{
			this.RegisterValue<float>(this.m_Floats, StyleValueType.Float, value);
		}

		public void AddValue(Dimension value)
		{
			this.RegisterValue<Dimension>(this.m_Dimensions, StyleValueType.Dimension, value);
		}

		public void AddValue(StyleValueKeyword keyword)
		{
			this.m_CurrentValues.Add(new StyleValueHandle((int)keyword, StyleValueType.Keyword));
		}

		public void AddValue(StyleValueFunction function)
		{
			this.m_CurrentValues.Add(new StyleValueHandle((int)function, StyleValueType.Function));
		}

		public void AddValue(string value, StyleValueType type)
		{
			bool flag = type == StyleValueType.Variable;
			if (flag)
			{
				this.RegisterVariable(value);
			}
			else
			{
				this.RegisterValue<string>(this.m_Strings, type, value);
			}
		}

		public void AddValue(Color value)
		{
			this.RegisterValue<Color>(this.m_Colors, StyleValueType.Color, value);
		}

		public void AddValue(UnityEngine.Object value)
		{
			this.RegisterValue<UnityEngine.Object>(this.m_Assets, StyleValueType.AssetReference, value);
		}

		public void AddValue(ScalableImage value)
		{
			this.RegisterValue<ScalableImage>(this.m_ScalableImages, StyleValueType.ScalableImage, value);
		}

		public void EndProperty()
		{
			StyleSheetBuilder.Log("Ending property");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			this.m_CurrentProperty.values = this.m_CurrentValues.ToArray();
			this.m_CurrentProperty = null;
			this.m_CurrentValues.Clear();
		}

		public int EndRule()
		{
			StyleSheetBuilder.Log("Ending rule");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Init;
			this.m_CurrentRule.properties = this.m_CurrentProperties.ToArray();
			this.m_Rules.Add(this.m_CurrentRule);
			this.m_CurrentRule = null;
			this.m_CurrentProperties.Clear();
			return this.m_Rules.Count - 1;
		}

		public void BuildTo(StyleSheet writeTo)
		{
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Init);
			writeTo.floats = this.m_Floats.ToArray();
			writeTo.dimensions = this.m_Dimensions.ToArray();
			writeTo.colors = this.m_Colors.ToArray();
			writeTo.strings = this.m_Strings.ToArray();
			writeTo.rules = this.m_Rules.ToArray();
			writeTo.assets = this.m_Assets.ToArray();
			writeTo.scalableImages = this.m_ScalableImages.ToArray();
			writeTo.complexSelectors = this.m_ComplexSelectors.ToArray();
			writeTo.imports = this.m_Imports.ToArray();
			bool flag = writeTo.imports.Length != 0;
			if (flag)
			{
				writeTo.FlattenImportedStyleSheetsRecursive();
			}
		}

		private void RegisterVariable(string value)
		{
			StyleSheetBuilder.Log("Add variable : " + value);
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			int num = this.m_Strings.IndexOf(value);
			bool flag = num < 0;
			if (flag)
			{
				this.m_Strings.Add(value);
				num = this.m_Strings.Count - 1;
			}
			this.m_CurrentValues.Add(new StyleValueHandle(num, StyleValueType.Variable));
		}

		private void RegisterValue<T>(List<T> list, StyleValueType type, T value)
		{
			string arg_32_0 = "Add value of type ";
			string arg_32_1 = type.ToString();
			string arg_32_2 = " : ";
			T t = value;
			StyleSheetBuilder.Log(arg_32_0 + arg_32_1 + arg_32_2 + ((t != null) ? t.ToString() : null));
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			list.Add(value);
			this.m_CurrentValues.Add(new StyleValueHandle(list.Count - 1, type));
		}

		private static void Log(string msg)
		{
		}
	}
}
