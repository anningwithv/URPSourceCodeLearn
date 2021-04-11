using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	[Serializable]
	public class StyleSheet : ScriptableObject
	{
		[Serializable]
		internal struct ImportStruct
		{
			public StyleSheet styleSheet;

			public string[] mediaQueries;
		}

		[SerializeField]
		private StyleRule[] m_Rules;

		[SerializeField]
		private StyleComplexSelector[] m_ComplexSelectors;

		[SerializeField]
		internal float[] floats;

		[SerializeField]
		internal Dimension[] dimensions;

		[SerializeField]
		internal Color[] colors;

		[SerializeField]
		internal string[] strings;

		[SerializeField]
		internal UnityEngine.Object[] assets;

		[SerializeField]
		internal StyleSheet.ImportStruct[] imports;

		[SerializeField]
		private List<StyleSheet> m_FlattenedImportedStyleSheets;

		[SerializeField]
		private int m_ContentHash;

		[SerializeField]
		internal ScalableImage[] scalableImages;

		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedNameSelectors;

		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedTypeSelectors;

		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedClassSelectors;

		[NonSerialized]
		internal bool isUnityStyleSheet;

		private static string kCustomPropertyMarker = "--";

		internal StyleRule[] rules
		{
			get
			{
				return this.m_Rules;
			}
			set
			{
				this.m_Rules = value;
				this.SetupReferences();
			}
		}

		internal StyleComplexSelector[] complexSelectors
		{
			get
			{
				return this.m_ComplexSelectors;
			}
			set
			{
				this.m_ComplexSelectors = value;
				this.SetupReferences();
			}
		}

		internal List<StyleSheet> flattenedRecursiveImports
		{
			get
			{
				return this.m_FlattenedImportedStyleSheets;
			}
		}

		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		private static bool TryCheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle, out T value)
		{
			bool result = false;
			value = default(T);
			bool flag = handle.valueType == type && handle.valueIndex >= 0 && handle.valueIndex < list.Length;
			if (flag)
			{
				value = list[handle.valueIndex];
				result = true;
			}
			else
			{
				Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", new object[]
				{
					type,
					handle.valueType
				});
			}
			return result;
		}

		private static T CheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle)
		{
			T result = default(T);
			bool flag = handle.valueType != type;
			if (flag)
			{
				Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", new object[]
				{
					type,
					handle.valueType
				});
			}
			else
			{
				bool flag2 = list == null || handle.valueIndex < 0 || handle.valueIndex >= list.Length;
				if (flag2)
				{
					Debug.LogError("Accessing invalid property");
				}
				else
				{
					result = list[handle.valueIndex];
				}
			}
			return result;
		}

		private void OnEnable()
		{
			this.SetupReferences();
		}

		internal void FlattenImportedStyleSheetsRecursive()
		{
			this.m_FlattenedImportedStyleSheets = new List<StyleSheet>();
			this.FlattenImportedStyleSheetsRecursive(this);
		}

		private void FlattenImportedStyleSheetsRecursive(StyleSheet sheet)
		{
			bool flag = sheet.imports == null;
			if (!flag)
			{
				for (int i = 0; i < sheet.imports.Length; i++)
				{
					StyleSheet styleSheet = sheet.imports[i].styleSheet;
					bool flag2 = styleSheet == null;
					if (!flag2)
					{
						this.FlattenImportedStyleSheetsRecursive(styleSheet);
						this.m_FlattenedImportedStyleSheets.Add(styleSheet);
					}
				}
			}
		}

		private void SetupReferences()
		{
			bool flag = this.complexSelectors == null || this.rules == null;
			if (!flag)
			{
				StyleRule[] rules = this.rules;
				for (int i = 0; i < rules.Length; i++)
				{
					StyleRule styleRule = rules[i];
					StyleProperty[] properties = styleRule.properties;
					for (int j = 0; j < properties.Length; j++)
					{
						StyleProperty styleProperty = properties[j];
						bool flag2 = StyleSheet.CustomStartsWith(styleProperty.name, StyleSheet.kCustomPropertyMarker);
						if (flag2)
						{
							styleRule.customPropertiesCount++;
							styleProperty.isCustomProperty = true;
						}
						StyleValueHandle[] values = styleProperty.values;
						for (int k = 0; k < values.Length; k++)
						{
							StyleValueHandle handle = values[k];
							bool flag3 = handle.IsVarFunction();
							if (flag3)
							{
								styleProperty.requireVariableResolve = true;
								break;
							}
						}
					}
				}
				int l = 0;
				int num = this.complexSelectors.Length;
				while (l < num)
				{
					this.complexSelectors[l].CachePseudoStateMasks();
					l++;
				}
				this.orderedClassSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedNameSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedTypeSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				int m = 0;
				while (m < this.complexSelectors.Length)
				{
					StyleComplexSelector styleComplexSelector = this.complexSelectors[m];
					bool flag4 = styleComplexSelector.ruleIndex < this.rules.Length;
					if (flag4)
					{
						styleComplexSelector.rule = this.rules[styleComplexSelector.ruleIndex];
					}
					styleComplexSelector.orderInStyleSheet = m;
					StyleSelector styleSelector = styleComplexSelector.selectors[styleComplexSelector.selectors.Length - 1];
					StyleSelectorPart styleSelectorPart = styleSelector.parts[0];
					string key = styleSelectorPart.value;
					Dictionary<string, StyleComplexSelector> dictionary = null;
					switch (styleSelectorPart.type)
					{
					case StyleSelectorType.Wildcard:
					case StyleSelectorType.Type:
						key = (styleSelectorPart.value ?? "*");
						dictionary = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.Class:
						dictionary = this.orderedClassSelectors;
						break;
					case StyleSelectorType.PseudoClass:
						key = "*";
						dictionary = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.RecursivePseudoClass:
						goto IL_22B;
					case StyleSelectorType.ID:
						dictionary = this.orderedNameSelectors;
						break;
					default:
						goto IL_22B;
					}
					IL_249:
					bool flag5 = dictionary != null;
					if (flag5)
					{
						StyleComplexSelector nextInTable;
						bool flag6 = dictionary.TryGetValue(key, out nextInTable);
						if (flag6)
						{
							styleComplexSelector.nextInTable = nextInTable;
						}
						dictionary[key] = styleComplexSelector;
					}
					m++;
					continue;
					IL_22B:
					Debug.LogError(string.Format("Invalid first part type {0}", styleSelectorPart.type));
					goto IL_249;
				}
			}
		}

		internal StyleValueKeyword ReadKeyword(StyleValueHandle handle)
		{
			return (StyleValueKeyword)handle.valueIndex;
		}

		internal float ReadFloat(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Dimension;
			float result;
			if (flag)
			{
				Dimension dimension = StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
				result = dimension.value;
			}
			else
			{
				result = StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
			}
			return result;
		}

		internal bool TryReadFloat(StyleValueHandle handle, out float value)
		{
			bool flag = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Dimension dimension;
				bool flag2 = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Float, handle, out dimension);
				value = dimension.value;
				result = flag2;
			}
			return result;
		}

		internal Dimension ReadDimension(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Float;
			Dimension result;
			if (flag)
			{
				float value = StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
				result = new Dimension(value, Dimension.Unit.Unitless);
			}
			else
			{
				result = StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
			}
			return result;
		}

		internal bool TryReadDimension(StyleValueHandle handle, out Dimension value)
		{
			bool flag = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle, out value);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				float value2 = 0f;
				bool flag2 = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value2);
				value = new Dimension(value2, Dimension.Unit.Unitless);
				result = flag2;
			}
			return result;
		}

		internal Color ReadColor(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<Color>(this.colors, StyleValueType.Color, handle);
		}

		internal bool TryReadColor(StyleValueHandle handle, out Color value)
		{
			return StyleSheet.TryCheckAccess<Color>(this.colors, StyleValueType.Color, handle, out value);
		}

		internal string ReadString(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.String, handle);
		}

		internal bool TryReadString(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.String, handle, out value);
		}

		internal string ReadEnum(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Enum, handle);
		}

		internal bool TryReadEnum(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Enum, handle, out value);
		}

		internal string ReadVariable(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Variable, handle);
		}

		internal bool TryReadVariable(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Variable, handle, out value);
		}

		internal string ReadResourcePath(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle);
		}

		internal bool TryReadResourcePath(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle, out value);
		}

		internal UnityEngine.Object ReadAssetReference(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<UnityEngine.Object>(this.assets, StyleValueType.AssetReference, handle);
		}

		internal string ReadMissingAssetReferenceUrl(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.MissingAssetReference, handle);
		}

		internal bool TryReadAssetReference(StyleValueHandle handle, out UnityEngine.Object value)
		{
			return StyleSheet.TryCheckAccess<UnityEngine.Object>(this.assets, StyleValueType.AssetReference, handle, out value);
		}

		internal StyleValueFunction ReadFunction(StyleValueHandle handle)
		{
			return (StyleValueFunction)handle.valueIndex;
		}

		internal string ReadFunctionName(StyleValueHandle handle)
		{
			bool flag = handle.valueType != StyleValueType.Function;
			string result;
			if (flag)
			{
				Debug.LogErrorFormat(string.Format("Trying to read value of type {0} while reading a value of type {1}", StyleValueType.Function, handle.valueType), new object[0]);
				result = string.Empty;
			}
			else
			{
				StyleValueFunction valueIndex = (StyleValueFunction)handle.valueIndex;
				result = valueIndex.ToUssString();
			}
			return result;
		}

		internal ScalableImage ReadScalableImage(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<ScalableImage>(this.scalableImages, StyleValueType.ScalableImage, handle);
		}

		private static bool CustomStartsWith(string originalString, string pattern)
		{
			int length = originalString.Length;
			int length2 = pattern.Length;
			int num = 0;
			int num2 = 0;
			while (num < length && num2 < length2 && originalString[num] == pattern[num2])
			{
				num++;
				num2++;
			}
			return (num2 == length2 && length >= length2) || (num == length && length2 >= length);
		}
	}
}
