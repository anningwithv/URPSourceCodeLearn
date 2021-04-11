using System;
using System.Collections.Generic;
using UnityEngine.TextCore;

namespace UnityEngine.UIElements
{
	public class TextElement : BindableElement, ITextElement, INotifyValueChanged<string>
	{
		public new class UxmlFactory : UxmlFactory<TextElement, TextElement.UxmlTraits>
		{
		}

		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			private UxmlBoolAttributeDescription m_DisplayTooltipWhenElided = new UxmlBoolAttributeDescription
			{
				name = "display-tooltip-when-elided"
			};

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextElement textElement = (TextElement)ve;
				textElement.text = this.m_Text.GetValueFromBag(bag, cc);
				textElement.displayTooltipWhenElided = this.m_DisplayTooltipWhenElided.GetValueFromBag(bag, cc);
			}
		}

		public static readonly string ussClassName = "unity-text-element";

		private TextHandle m_TextHandle = TextHandle.New();

		[SerializeField]
		private string m_Text = string.Empty;

		private bool m_DisplayTooltipWhenElided = true;

		internal static readonly string k_EllipsisText = "...";

		private bool m_WasElided;

		private bool m_UpdateTextParams = true;

		private MeshGenerationContextUtils.TextParams m_TextParams;

		private int m_PreviousTextParamsHashCode = 2147483647;

		internal TextHandle textHandle
		{
			get
			{
				return this.m_TextHandle;
			}
		}

		public virtual string text
		{
			get
			{
				return ((INotifyValueChanged<string>)this).value;
			}
			set
			{
				((INotifyValueChanged<string>)this).value = value;
			}
		}

		public bool displayTooltipWhenElided
		{
			get
			{
				return this.m_DisplayTooltipWhenElided;
			}
			set
			{
				bool flag = this.m_DisplayTooltipWhenElided != value;
				if (flag)
				{
					this.m_DisplayTooltipWhenElided = value;
					this.UpdateVisibleText();
					base.MarkDirtyRepaint();
				}
			}
		}

		public bool isElided
		{
			get;
			private set;
		}

		string INotifyValueChanged<string>.value
		{
			get
			{
				return this.m_Text ?? string.Empty;
			}
			set
			{
				bool flag = this.m_Text != value;
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<string> pooled = ChangeEvent<string>.GetPooled(this.text, value))
						{
							pooled.target = this;
							((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
							this.SendEvent(pooled);
						}
					}
					else
					{
						((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
					}
				}
			}
		}

		public TextElement()
		{
			base.requireMeasureFunction = true;
			base.AddToClassList(TextElement.ussClassName);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
		}

		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			this.m_TextHandle.useLegacy = (e.destinationPanel.contextType == ContextType.Editor);
		}

		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			this.UpdateVisibleText();
		}

		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this.UpdateVisibleText();
			mgc.Text(this.m_TextParams, this.m_TextHandle, base.scaledPixelsPerPoint);
			this.m_UpdateTextParams = true;
		}

		internal string ElideText(string drawText, string ellipsisText, float width, TextOverflowPosition textOverflowPosition)
		{
			float num = 1f / base.scaledPixelsPerPoint;
			Vector2 vector = this.MeasureTextSize(drawText, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
			bool flag = vector.x - num <= width || string.IsNullOrEmpty(ellipsisText);
			string result;
			if (flag)
			{
				result = drawText;
			}
			else
			{
				string text = (drawText.Length > 1) ? ellipsisText : drawText;
				Vector2 vector2 = this.MeasureTextSize(text, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
				bool flag2 = vector2.x >= width;
				if (flag2)
				{
					result = text;
				}
				else
				{
					int num2 = drawText.Length - 1;
					int num3 = -1;
					string text2 = drawText;
					int i = (textOverflowPosition == TextOverflowPosition.Start) ? 1 : 0;
					int num4 = (textOverflowPosition == TextOverflowPosition.Start || textOverflowPosition == TextOverflowPosition.Middle) ? num2 : (num2 - 1);
					int num5 = (i + num4) / 2;
					while (i <= num4)
					{
						bool flag3 = textOverflowPosition == TextOverflowPosition.Start;
						if (flag3)
						{
							text2 = ellipsisText + drawText.Substring(num5, num2 - (num5 - 1));
						}
						else
						{
							bool flag4 = textOverflowPosition == TextOverflowPosition.End;
							if (flag4)
							{
								text2 = drawText.Substring(0, num5) + ellipsisText;
							}
							else
							{
								bool flag5 = textOverflowPosition == TextOverflowPosition.Middle;
								if (flag5)
								{
									text2 = drawText.Substring(0, num5 - 1) + ellipsisText + drawText.Substring(num2 - (num5 - 1));
								}
							}
						}
						vector = this.MeasureTextSize(text2, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
						bool flag6 = Math.Abs(vector.x - width) < Mathf.Epsilon;
						if (flag6)
						{
							result = text2;
							return result;
						}
						bool flag7 = textOverflowPosition == TextOverflowPosition.Start;
						if (flag7)
						{
							bool flag8 = vector.x > width;
							if (flag8)
							{
								bool flag9 = num3 == num5 - 1;
								if (flag9)
								{
									result = ellipsisText + drawText.Substring(num3, num2 - (num3 - 1));
									return result;
								}
								i = num5 + 1;
							}
							else
							{
								num4 = num5 - 1;
								num3 = num5;
							}
						}
						else
						{
							bool flag10 = textOverflowPosition == TextOverflowPosition.End || textOverflowPosition == TextOverflowPosition.Middle;
							if (flag10)
							{
								bool flag11 = vector.x > width;
								if (flag11)
								{
									bool flag12 = num3 == num5 - 1;
									if (flag12)
									{
										bool flag13 = textOverflowPosition == TextOverflowPosition.End;
										if (flag13)
										{
											result = drawText.Substring(0, num3) + ellipsisText;
											return result;
										}
										result = drawText.Substring(0, num3 - 1) + ellipsisText + drawText.Substring(num2 - (num3 - 1));
										return result;
									}
									else
									{
										num4 = num5 - 1;
									}
								}
								else
								{
									i = num5 + 1;
									num3 = num5;
								}
							}
						}
						num5 = (i + num4) / 2;
					}
					result = text2;
				}
			}
			return result;
		}

		private void UpdateTooltip()
		{
			bool flag = this.displayTooltipWhenElided && this.isElided;
			bool flag2 = flag;
			if (flag2)
			{
				base.tooltip = this.text;
				this.m_WasElided = true;
			}
			else
			{
				bool wasElided = this.m_WasElided;
				if (wasElided)
				{
					base.tooltip = null;
					this.m_WasElided = false;
				}
			}
		}

		private void UpdateVisibleText()
		{
			MeshGenerationContextUtils.TextParams textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(this, this.text);
			int hashCode = textParams.GetHashCode();
			bool flag = this.m_UpdateTextParams || hashCode != this.m_PreviousTextParamsHashCode;
			if (flag)
			{
				this.m_TextParams = textParams;
				bool flag2 = this.m_TextParams.textOverflowMode == TextOverflowMode.Ellipsis;
				if (flag2)
				{
					this.m_TextParams.text = this.ElideText(this.m_TextParams.text, TextElement.k_EllipsisText, this.m_TextParams.rect.width, this.m_TextParams.textOverflowPosition);
				}
				this.isElided = (this.m_TextParams.textOverflowMode == TextOverflowMode.Ellipsis && this.m_TextParams.text != this.text);
				this.m_PreviousTextParamsHashCode = hashCode;
				this.m_UpdateTextParams = false;
				this.UpdateTooltip();
			}
		}

		public Vector2 MeasureTextSize(string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			return TextElement.MeasureVisualElementTextSize(this, textToMeasure, width, widthMode, height, heightMode, this.m_TextHandle);
		}

		internal static Vector2 MeasureVisualElementTextSize(VisualElement ve, string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode, TextHandle textHandle)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			Font value = ve.computedStyle.unityFont.value;
			bool flag = textToMeasure == null || value == null;
			Vector2 result;
			if (flag)
			{
				result = new Vector2(num, num2);
			}
			else
			{
				Vector3 vector = ve.ComputeGlobalScale();
				bool flag2 = vector.x + vector.y <= 0f || ve.scaledPixelsPerPoint <= 0f;
				if (flag2)
				{
					result = Vector2.zero;
				}
				else
				{
					float scaledPixelsPerPoint = ve.scaledPixelsPerPoint;
					float num3 = 0.02f;
					float num4 = num3 / scaledPixelsPerPoint;
					bool flag3 = widthMode == VisualElement.MeasureMode.Exactly;
					if (flag3)
					{
						num = width;
					}
					else
					{
						MeshGenerationContextUtils.TextParams textSettings = TextElement.GetTextSettings(ve, textToMeasure);
						textSettings.wordWrap = false;
						textSettings.richText = false;
						num = textHandle.ComputeTextWidth(textSettings, scaledPixelsPerPoint);
						num = ((num < num4) ? 0f : AlignmentUtils.CeilToPixelGrid(num, scaledPixelsPerPoint, num3));
						bool flag4 = widthMode == VisualElement.MeasureMode.AtMost;
						if (flag4)
						{
							num = Mathf.Min(num, width);
						}
					}
					bool flag5 = heightMode == VisualElement.MeasureMode.Exactly;
					if (flag5)
					{
						num2 = height;
					}
					else
					{
						MeshGenerationContextUtils.TextParams textSettings2 = TextElement.GetTextSettings(ve, textToMeasure);
						textSettings2.wordWrapWidth = num;
						textSettings2.richText = false;
						num2 = textHandle.ComputeTextHeight(textSettings2, scaledPixelsPerPoint);
						num2 = ((num2 < num4) ? 0f : AlignmentUtils.CeilToPixelGrid(num2, scaledPixelsPerPoint, num3));
						bool flag6 = heightMode == VisualElement.MeasureMode.AtMost;
						if (flag6)
						{
							num2 = Mathf.Min(num2, height);
						}
					}
					result = new Vector2(num, num2);
				}
			}
			return result;
		}

		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			return this.MeasureTextSize(this.text, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		private static MeshGenerationContextUtils.TextParams GetTextSettings(VisualElement ve, string text)
		{
			ComputedStyle computedStyle = ve.computedStyle;
			return new MeshGenerationContextUtils.TextParams
			{
				rect = ve.contentRect,
				text = text,
				font = computedStyle.unityFont.value,
				fontSize = (int)computedStyle.fontSize.value.value,
				fontStyle = computedStyle.unityFontStyleAndWeight.value,
				fontColor = computedStyle.color.value,
				anchor = computedStyle.unityTextAlign.value,
				wordWrap = (computedStyle.whiteSpace.value == WhiteSpace.Normal),
				wordWrapWidth = ((computedStyle.whiteSpace.value == WhiteSpace.Normal) ? ve.contentRect.width : 0f),
				richText = true,
				textOverflowMode = MeshGenerationContextUtils.TextParams.GetTextOverflowMode(computedStyle),
				textOverflowPosition = computedStyle.unityTextOverflowPosition.value
			};
		}

		void INotifyValueChanged<string>.SetValueWithoutNotify(string newValue)
		{
			bool flag = this.m_Text != newValue;
			if (flag)
			{
				this.m_Text = newValue;
				base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				bool flag2 = !string.IsNullOrEmpty(base.viewDataKey);
				if (flag2)
				{
					base.SaveViewData();
				}
			}
		}
	}
}
