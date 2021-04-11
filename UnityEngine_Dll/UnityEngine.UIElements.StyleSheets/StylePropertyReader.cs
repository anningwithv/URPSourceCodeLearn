using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	internal class StylePropertyReader
	{
		internal delegate int GetCursorIdFunction(StyleSheet sheet, StyleValueHandle handle);

		internal static StylePropertyReader.GetCursorIdFunction getCursorIdFunc = null;

		private List<StylePropertyValue> m_Values = new List<StylePropertyValue>();

		private List<int> m_ValueCount = new List<int>();

		private StyleVariableResolver m_Resolver = new StyleVariableResolver();

		private StyleSheet m_Sheet;

		private StyleProperty[] m_Properties;

		private StylePropertyId[] m_PropertyIds;

		private int m_CurrentValueIndex;

		private int m_CurrentPropertyIndex;

		public StyleProperty property
		{
			get;
			private set;
		}

		public StylePropertyId propertyId
		{
			get;
			private set;
		}

		public int valueCount
		{
			get;
			private set;
		}

		public float dpiScaling
		{
			get;
			private set;
		}

		public void SetContext(StyleSheet sheet, StyleComplexSelector selector, StyleVariableContext varContext, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = selector.rule.properties;
			this.m_PropertyIds = StyleSheetCache.GetPropertyIds(sheet, selector.ruleIndex);
			this.m_Resolver.variableContext = varContext;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		public void SetInlineContext(StyleSheet sheet, StyleProperty[] properties, StylePropertyId[] propertyIds, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = properties;
			this.m_PropertyIds = propertyIds;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		public StylePropertyId MoveNextProperty()
		{
			this.m_CurrentPropertyIndex++;
			this.m_CurrentValueIndex += this.valueCount;
			this.SetCurrentProperty();
			return this.propertyId;
		}

		public StylePropertyValue GetValue(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index];
		}

		public StyleValueType GetValueType(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType;
		}

		public bool IsValueType(int index, StyleValueType type)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == type;
		}

		public bool IsKeyword(int index, StyleValueKeyword keyword)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.handle.valueType == StyleValueType.Keyword && stylePropertyValue.handle.valueIndex == (int)keyword;
		}

		public string ReadAsString(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
		}

		public StyleLength ReadStyleLength(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			StyleLength result;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
				result = new StyleLength(valueIndex.ToStyleKeyword());
			}
			else
			{
				result = new StyleLength(stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToLength());
			}
			return result;
		}

		public StyleFloat ReadStyleFloat(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return new StyleFloat(stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle));
		}

		public StyleInt ReadStyleInt(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return new StyleInt((int)stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle));
		}

		public StyleColor ReadStyleColor(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			Color v = Color.clear;
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				StyleSheetColor.TryGetColor(text.ToLower(), out v);
			}
			else
			{
				v = stylePropertyValue.sheet.ReadColor(stylePropertyValue.handle);
			}
			return new StyleColor(v);
		}

		public StyleInt ReadStyleEnum(StyleEnumType enumType, int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueHandle handle = stylePropertyValue.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string value;
			if (flag)
			{
				StyleValueKeyword svk = stylePropertyValue.sheet.ReadKeyword(handle);
				value = svk.ToUssString();
			}
			else
			{
				value = stylePropertyValue.sheet.ReadEnum(handle);
			}
			int enumIntValue = StylePropertyUtil.GetEnumIntValue(enumType, value);
			return new StyleInt(enumIntValue);
		}

		public StyleFont ReadStyleFont(int index)
		{
			Font font = null;
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = stylePropertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.ResourcePath)
			{
				if (styleValueType != StyleValueType.AssetReference)
				{
					Debug.LogWarning("Invalid value for font " + stylePropertyValue.handle.valueType.ToString());
				}
				else
				{
					font = (stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as Font);
					bool flag = font == null;
					if (flag)
					{
						Debug.LogWarning("Invalid font reference");
					}
				}
			}
			else
			{
				string text = stylePropertyValue.sheet.ReadResourcePath(stylePropertyValue.handle);
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					font = (Panel.LoadResource(text, typeof(Font), this.dpiScaling) as Font);
				}
				bool flag3 = font == null;
				if (flag3)
				{
					Debug.LogWarning(string.Format("Font not found for path: {0}", text));
				}
			}
			return new StyleFont(font);
		}

		public StyleBackground ReadStyleBackground(int index)
		{
			ImageSource imageSource = default(ImageSource);
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				bool flag2 = stylePropertyValue.handle.valueIndex != 6;
				if (flag2)
				{
					string arg_68_0 = "Invalid keyword for image source ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(arg_68_0 + valueIndex.ToString());
				}
			}
			else
			{
				bool flag3 = !StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.dpiScaling, out imageSource);
				if (flag3)
				{
					imageSource.texture = (Panel.LoadResource("d_console.warnicon", typeof(Texture2D), this.dpiScaling) as Texture2D);
				}
			}
			bool flag4 = imageSource.texture != null;
			StyleBackground result;
			if (flag4)
			{
				result = new StyleBackground(imageSource.texture);
			}
			else
			{
				bool flag5 = imageSource.vectorImage != null;
				if (flag5)
				{
					result = new StyleBackground(imageSource.vectorImage);
				}
				else
				{
					result = default(StyleBackground);
				}
			}
			return result;
		}

		public StyleCursor ReadStyleCursor(int index)
		{
			float x = 0f;
			float y = 0f;
			int defaultCursorId = 0;
			Texture2D texture = null;
			StyleValueType valueType = this.GetValueType(index);
			bool flag = valueType == StyleValueType.ResourcePath || valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.valueCount < 1;
				if (flag3)
				{
					Debug.LogWarning(string.Format("USS 'cursor' has invalid value at {0}.", index));
				}
				else
				{
					ImageSource imageSource = default(ImageSource);
					StylePropertyValue value = this.GetValue(index);
					bool flag4 = StylePropertyReader.TryGetImageSourceFromValue(value, this.dpiScaling, out imageSource);
					if (flag4)
					{
						texture = imageSource.texture;
						bool flag5 = this.valueCount >= 3;
						if (flag5)
						{
							StylePropertyValue value2 = this.GetValue(index + 1);
							StylePropertyValue value3 = this.GetValue(index + 2);
							bool flag6 = value2.handle.valueType != StyleValueType.Float || value3.handle.valueType != StyleValueType.Float;
							if (flag6)
							{
								Debug.LogWarning("USS 'cursor' property requires two integers for the hot spot value.");
							}
							else
							{
								x = value2.sheet.ReadFloat(value2.handle);
								y = value3.sheet.ReadFloat(value3.handle);
							}
						}
					}
				}
			}
			else
			{
				bool flag7 = StylePropertyReader.getCursorIdFunc != null;
				if (flag7)
				{
					StylePropertyValue value4 = this.GetValue(index);
					defaultCursorId = StylePropertyReader.getCursorIdFunc(value4.sheet, value4.handle);
				}
			}
			UnityEngine.UIElements.Cursor v = new UnityEngine.UIElements.Cursor
			{
				texture = texture,
				hotspot = new Vector2(x, y),
				defaultCursorId = defaultCursorId
			};
			return new StyleCursor(v);
		}

		private void LoadProperties()
		{
			this.m_CurrentPropertyIndex = 0;
			this.m_CurrentValueIndex = 0;
			this.m_Values.Clear();
			this.m_ValueCount.Clear();
			StyleProperty[] properties = this.m_Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				StyleProperty styleProperty = properties[i];
				int num = 0;
				bool flag = true;
				bool requireVariableResolve = styleProperty.requireVariableResolve;
				if (requireVariableResolve)
				{
					this.m_Resolver.Init(styleProperty, this.m_Sheet, styleProperty.values);
					int num2 = 0;
					while (num2 < styleProperty.values.Length & flag)
					{
						StyleValueHandle handle = styleProperty.values[num2];
						bool flag2 = handle.IsVarFunction();
						if (flag2)
						{
							StyleVariableResolver.Result result = this.m_Resolver.ResolveVarFunction(ref num2);
							bool flag3 = result > StyleVariableResolver.Result.Valid;
							if (flag3)
							{
								StyleValueHandle handle2 = new StyleValueHandle
								{
									valueType = StyleValueType.Keyword,
									valueIndex = 3
								};
								this.m_Values.Add(new StylePropertyValue
								{
									sheet = this.m_Sheet,
									handle = handle2
								});
								num++;
								flag = false;
							}
						}
						else
						{
							this.m_Resolver.AddValue(handle);
						}
						num2++;
					}
					bool flag4 = flag;
					if (flag4)
					{
						this.m_Values.AddRange(this.m_Resolver.resolvedValues);
						num += this.m_Resolver.resolvedValues.Count;
					}
				}
				else
				{
					num = styleProperty.values.Length;
					for (int j = 0; j < num; j++)
					{
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = styleProperty.values[j]
						});
					}
				}
				this.m_ValueCount.Add(num);
			}
			this.SetCurrentProperty();
		}

		private void SetCurrentProperty()
		{
			bool flag = this.m_CurrentPropertyIndex < this.m_PropertyIds.Length;
			if (flag)
			{
				this.property = this.m_Properties[this.m_CurrentPropertyIndex];
				this.propertyId = this.m_PropertyIds[this.m_CurrentPropertyIndex];
				this.valueCount = this.m_ValueCount[this.m_CurrentPropertyIndex];
			}
			else
			{
				this.property = null;
				this.propertyId = StylePropertyId.Unknown;
				this.valueCount = 0;
			}
		}

		internal static bool TryGetImageSourceFromValue(StylePropertyValue propertyValue, float dpiScaling, out ImageSource source)
		{
			source = default(ImageSource);
			StyleValueType valueType = propertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			bool result;
			if (styleValueType <= StyleValueType.AssetReference)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType == StyleValueType.AssetReference)
					{
						UnityEngine.Object @object = propertyValue.sheet.ReadAssetReference(propertyValue.handle);
						source.texture = (@object as Texture2D);
						source.vectorImage = (@object as VectorImage);
						bool flag = source.texture == null && source.vectorImage == null;
						if (flag)
						{
							Debug.LogWarning("Invalid image specified");
							result = false;
							return result;
						}
						goto IL_24E;
					}
				}
				else
				{
					string text = propertyValue.sheet.ReadResourcePath(propertyValue.handle);
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						source.texture = (Panel.LoadResource(text, typeof(Texture2D), dpiScaling) as Texture2D);
						bool flag3 = source.texture == null;
						if (flag3)
						{
							source.vectorImage = (Panel.LoadResource(text, typeof(VectorImage), dpiScaling) as VectorImage);
						}
					}
					bool flag4 = source.texture == null && source.vectorImage == null;
					if (flag4)
					{
						Debug.LogWarning(string.Format("Image not found for path: {0}", text));
						result = false;
						return result;
					}
					goto IL_24E;
				}
			}
			else if (styleValueType != StyleValueType.ScalableImage)
			{
				if (styleValueType == StyleValueType.MissingAssetReference)
				{
					result = false;
					return result;
				}
			}
			else
			{
				ScalableImage scalableImage = propertyValue.sheet.ReadScalableImage(propertyValue.handle);
				bool flag5 = scalableImage.normalImage == null && scalableImage.highResolutionImage == null;
				if (flag5)
				{
					Debug.LogWarning("Invalid scalable image specified");
					result = false;
					return result;
				}
				bool flag6 = dpiScaling > 1f;
				if (flag6)
				{
					source.texture = scalableImage.highResolutionImage;
					source.texture.pixelsPerPoint = 2f;
				}
				else
				{
					source.texture = scalableImage.normalImage;
				}
				bool flag7 = !Mathf.Approximately(dpiScaling % 1f, 0f);
				if (flag7)
				{
					source.texture.filterMode = FilterMode.Bilinear;
				}
				goto IL_24E;
			}
			Debug.LogWarning("Invalid value for image texture " + propertyValue.handle.valueType.ToString());
			result = false;
			return result;
			IL_24E:
			result = true;
			return result;
		}
	}
}
