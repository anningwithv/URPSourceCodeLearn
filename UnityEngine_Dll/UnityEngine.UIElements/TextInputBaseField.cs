using System;

namespace UnityEngine.UIElements
{
	public abstract class TextInputBaseField<TValueType> : BaseField<TValueType>
	{
		public new class UxmlTraits : BaseFieldTraits<string, UxmlStringAttributeDescription>
		{
			private UxmlIntAttributeDescription m_MaxLength = new UxmlIntAttributeDescription
			{
				name = "max-length",
				obsoleteNames = new string[]
				{
					"maxLength"
				},
				defaultValue = -1
			};

			private UxmlBoolAttributeDescription m_Password = new UxmlBoolAttributeDescription
			{
				name = "password"
			};

			private UxmlStringAttributeDescription m_MaskCharacter = new UxmlStringAttributeDescription
			{
				name = "mask-character",
				obsoleteNames = new string[]
				{
					"maskCharacter"
				},
				defaultValue = '*'.ToString()
			};

			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			private UxmlBoolAttributeDescription m_IsReadOnly = new UxmlBoolAttributeDescription
			{
				name = "readonly"
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)ve;
				textInputBaseField.maxLength = this.m_MaxLength.GetValueFromBag(bag, cc);
				textInputBaseField.isPasswordField = this.m_Password.GetValueFromBag(bag, cc);
				textInputBaseField.isReadOnly = this.m_IsReadOnly.GetValueFromBag(bag, cc);
				string valueFromBag = this.m_MaskCharacter.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					textInputBaseField.maskChar = valueFromBag[0];
				}
				textInputBaseField.text = this.m_Text.GetValueFromBag(bag, cc);
			}
		}

		protected abstract class TextInputBase : VisualElement, ITextInputField, IEventHandler, ITextElement
		{
			private string m_OriginalText;

			private Color m_SelectionColor = Color.clear;

			private Color m_CursorColor = Color.grey;

			private TextHandle m_TextHandle = TextHandle.New();

			private string m_Text;

			public int cursorIndex
			{
				get
				{
					return this.editorEngine.cursorIndex;
				}
			}

			public int selectIndex
			{
				get
				{
					return this.editorEngine.selectIndex;
				}
			}

			bool ITextInputField.isReadOnly
			{
				get
				{
					return this.isReadOnly;
				}
			}

			public bool isReadOnly
			{
				get;
				set;
			}

			public int maxLength
			{
				get;
				set;
			}

			public char maskChar
			{
				get;
				set;
			}

			public virtual bool isPasswordField
			{
				get;
				set;
			}

			public bool doubleClickSelectsWord
			{
				get;
				set;
			}

			public bool tripleClickSelectsLine
			{
				get;
				set;
			}

			internal bool isDelayed
			{
				get;
				set;
			}

			internal bool isDragging
			{
				get;
				set;
			}

			private bool touchScreenTextField
			{
				get
				{
					return TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
				}
			}

			public Color selectionColor
			{
				get
				{
					return this.m_SelectionColor;
				}
			}

			public Color cursorColor
			{
				get
				{
					return this.m_CursorColor;
				}
			}

			internal bool hasFocus
			{
				get
				{
					return base.elementPanel != null && base.elementPanel.focusController.GetLeafFocusedElement() == this;
				}
			}

			internal TextEditorEventHandler editorEventHandler
			{
				get;
				private set;
			}

			internal TextEditorEngine editorEngine
			{
				get;
				private set;
			}

			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					bool flag = this.m_Text == value;
					if (!flag)
					{
						this.m_Text = value;
						this.editorEngine.text = value;
						base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					}
				}
			}

			bool ITextInputField.hasFocus
			{
				get
				{
					return this.hasFocus;
				}
			}

			TextEditorEngine ITextInputField.editorEngine
			{
				get
				{
					return this.editorEngine;
				}
			}

			bool ITextInputField.isDelayed
			{
				get
				{
					return this.isDelayed;
				}
			}

			private void SaveValueAndText()
			{
				this.m_OriginalText = this.text;
			}

			private void RestoreValueAndText()
			{
				this.text = this.m_OriginalText;
			}

			public void SelectAll()
			{
				TextEditorEngine expr_07 = this.editorEngine;
				if (expr_07 != null)
				{
					expr_07.SelectAll();
				}
			}

			internal void SelectNone()
			{
				TextEditorEngine expr_07 = this.editorEngine;
				if (expr_07 != null)
				{
					expr_07.SelectNone();
				}
			}

			private void UpdateText(string value)
			{
				bool flag = this.text != value;
				if (flag)
				{
					using (InputEvent pooled = InputEvent.GetPooled(this.text, value))
					{
						pooled.target = base.parent;
						this.text = value;
						VisualElement expr_3B = base.parent;
						if (expr_3B != null)
						{
							expr_3B.SendEvent(pooled);
						}
					}
				}
			}

			protected virtual TValueType StringToValue(string str)
			{
				throw new NotSupportedException();
			}

			internal void UpdateValueFromText()
			{
				TValueType value = this.StringToValue(this.text);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				textInputBaseField.value = value;
			}

			internal TextInputBase()
			{
				this.isReadOnly = false;
				base.focusable = true;
				base.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
				this.m_Text = string.Empty;
				base.name = TextInputBaseField<string>.textInputUssName;
				base.requireMeasureFunction = true;
				this.editorEngine = new TextEditorEngine(new TextEditorEngine.OnDetectFocusChangeFunction(this.OnDetectFocusChange), new TextEditorEngine.OnIndexChangeFunction(this.OnCursorIndexChange));
				this.editorEngine.style.richText = false;
				bool touchScreenTextField = this.touchScreenTextField;
				if (touchScreenTextField)
				{
					this.editorEventHandler = new TouchScreenTextEditorEventHandler(this.editorEngine, this);
				}
				else
				{
					this.doubleClickSelectsWord = true;
					this.tripleClickSelectsLine = true;
					this.editorEventHandler = new KeyboardTextEditorEventHandler(this.editorEngine, this);
				}
				this.editorEngine.style = new GUIStyle(this.editorEngine.style);
				base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
				base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
				base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			}

			private DropdownMenuAction.Status CutCopyActionStatus(DropdownMenuAction a)
			{
				return (this.editorEngine.hasSelection && !this.isPasswordField) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
			}

			private DropdownMenuAction.Status PasteActionStatus(DropdownMenuAction a)
			{
				return this.editorEngine.CanPaste() ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
			}

			private void ProcessMenuCommand(string command)
			{
				using (ExecuteCommandEvent pooled = CommandEventBase<ExecuteCommandEvent>.GetPooled(command))
				{
					pooled.target = this;
					this.SendEvent(pooled);
				}
			}

			private void Cut(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Cut");
			}

			private void Copy(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Copy");
			}

			private void Paste(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Paste");
			}

			private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
			{
				Color clear = Color.clear;
				Color clear2 = Color.clear;
				ICustomStyle customStyle = e.customStyle;
				bool flag = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_SelectionColorProperty, out clear);
				if (flag)
				{
					this.m_SelectionColor = clear;
				}
				bool flag2 = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_CursorColorProperty, out clear2);
				if (flag2)
				{
					this.m_CursorColor = clear2;
				}
				TextInputBaseField<TValueType>.TextInputBase.SyncGUIStyle(this, this.editorEngine.style);
			}

			private void OnAttachToPanel(AttachToPanelEvent e)
			{
				this.m_TextHandle.useLegacy = (e.destinationPanel.contextType == ContextType.Editor);
			}

			internal virtual void SyncTextEngine()
			{
				this.editorEngine.text = this.CullString(this.text);
				this.editorEngine.SaveBackup();
				this.editorEngine.position = base.layout;
				this.editorEngine.DetectFocusChange();
			}

			internal string CullString(string s)
			{
				bool flag = this.maxLength >= 0 && s != null && s.Length > this.maxLength;
				string result;
				if (flag)
				{
					result = s.Substring(0, this.maxLength);
				}
				else
				{
					result = s;
				}
				return result;
			}

			internal void OnGenerateVisualContent(MeshGenerationContext mgc)
			{
				string text = this.text;
				bool isPasswordField = this.isPasswordField;
				if (isPasswordField)
				{
					text = "".PadRight(this.text.Length, this.maskChar);
				}
				bool touchScreenTextField = this.touchScreenTextField;
				if (touchScreenTextField)
				{
					TouchScreenTextEditorEventHandler touchScreenTextEditorEventHandler = this.editorEventHandler as TouchScreenTextEditorEventHandler;
					bool flag = touchScreenTextEditorEventHandler != null;
					if (flag)
					{
						mgc.Text(MeshGenerationContextUtils.TextParams.MakeStyleBased(this, text), this.m_TextHandle, base.scaledPixelsPerPoint);
					}
				}
				else
				{
					bool flag2 = !this.hasFocus;
					if (flag2)
					{
						mgc.Text(MeshGenerationContextUtils.TextParams.MakeStyleBased(this, text), this.m_TextHandle, base.scaledPixelsPerPoint);
					}
					else
					{
						this.DrawWithTextSelectionAndCursor(mgc, text, base.scaledPixelsPerPoint);
					}
				}
			}

			internal void DrawWithTextSelectionAndCursor(MeshGenerationContext mgc, string newText, float pixelsPerPoint)
			{
				Color playmodeTintColor = (base.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
				KeyboardTextEditorEventHandler keyboardTextEditorEventHandler = this.editorEventHandler as KeyboardTextEditorEventHandler;
				bool flag = keyboardTextEditorEventHandler == null;
				if (!flag)
				{
					keyboardTextEditorEventHandler.PreDrawCursor(newText);
					int cursorIndex = this.editorEngine.cursorIndex;
					int selectIndex = this.editorEngine.selectIndex;
					Rect localPosition = this.editorEngine.localPosition;
					Vector2 scrollOffset = this.editorEngine.scrollOffset;
					float scaling = TextHandle.ComputeTextScaling(base.worldTransform, pixelsPerPoint);
					MeshGenerationContextUtils.TextParams textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(this, this.text);
					textParams.text = " ";
					textParams.wordWrapWidth = 0f;
					textParams.wordWrap = false;
					float num = this.m_TextHandle.ComputeTextHeight(textParams, scaling);
					float wordWrapWidth = 0f;
					bool flag2 = this.editorEngine.multiline && base.resolvedStyle.whiteSpace == WhiteSpace.Normal;
					if (flag2)
					{
						wordWrapWidth = base.contentRect.width;
					}
					Vector2 p = this.editorEngine.graphicalCursorPos - scrollOffset;
					p.y += num;
					GUIUtility.compositionCursorPos = this.LocalToWorld(p);
					Color cursorColor = this.cursorColor;
					int num2 = string.IsNullOrEmpty(GUIUtility.compositionString) ? selectIndex : (cursorIndex + GUIUtility.compositionString.Length);
					bool flag3 = cursorIndex != num2 && !this.isDragging;
					if (flag3)
					{
						int cursorIndex2 = (cursorIndex < num2) ? cursorIndex : num2;
						int cursorIndex3 = (cursorIndex > num2) ? cursorIndex : num2;
						CursorPositionStylePainterParameters @default = CursorPositionStylePainterParameters.GetDefault(this, this.text);
						@default.text = this.editorEngine.text;
						@default.wordWrapWidth = wordWrapWidth;
						@default.cursorIndex = cursorIndex2;
						Vector2 vector = this.m_TextHandle.GetCursorPosition(@default, scaling);
						@default.cursorIndex = cursorIndex3;
						Vector2 vector2 = this.m_TextHandle.GetCursorPosition(@default, scaling);
						vector -= scrollOffset;
						vector2 -= scrollOffset;
						bool flag4 = Mathf.Approximately(vector.y, vector2.y);
						if (flag4)
						{
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector.x, vector.y, vector2.x - vector.x, num),
								color = this.selectionColor,
								playmodeTintColor = playmodeTintColor
							});
						}
						else
						{
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector.x, vector.y, base.contentRect.xMax - vector.x, num),
								color = this.selectionColor,
								playmodeTintColor = playmodeTintColor
							});
							float num3 = vector2.y - vector.y - num;
							bool flag5 = num3 > 0f;
							if (flag5)
							{
								mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
								{
									rect = new Rect(base.contentRect.xMin, vector.y + num, base.contentRect.width, num3),
									color = this.selectionColor,
									playmodeTintColor = playmodeTintColor
								});
							}
							bool flag6 = vector2.x != base.contentRect.x;
							if (flag6)
							{
								mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
								{
									rect = new Rect(base.contentRect.xMin, vector2.y, vector2.x, num),
									color = this.selectionColor,
									playmodeTintColor = playmodeTintColor
								});
							}
						}
					}
					bool flag7 = !string.IsNullOrEmpty(this.editorEngine.text) && base.contentRect.width > 0f && base.contentRect.height > 0f;
					if (flag7)
					{
						textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(this, this.text);
						textParams.rect = new Rect(base.contentRect.x - scrollOffset.x, base.contentRect.y - scrollOffset.y, base.contentRect.width + scrollOffset.x, base.contentRect.height + scrollOffset.y);
						textParams.text = this.editorEngine.text;
						mgc.Text(textParams, this.m_TextHandle, base.scaledPixelsPerPoint);
					}
					bool flag8 = !this.isReadOnly && !this.isDragging;
					if (flag8)
					{
						bool flag9 = cursorIndex == num2 && base.computedStyle.unityFont.value != null;
						if (flag9)
						{
							CursorPositionStylePainterParameters @default = CursorPositionStylePainterParameters.GetDefault(this, this.text);
							@default.text = this.editorEngine.text;
							@default.wordWrapWidth = wordWrapWidth;
							@default.cursorIndex = cursorIndex;
							Vector2 vector3 = this.m_TextHandle.GetCursorPosition(@default, scaling);
							vector3 -= scrollOffset;
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector3.x, vector3.y, 1f, num),
								color = cursorColor,
								playmodeTintColor = playmodeTintColor
							});
						}
						bool flag10 = this.editorEngine.altCursorPosition != -1;
						if (flag10)
						{
							CursorPositionStylePainterParameters @default = CursorPositionStylePainterParameters.GetDefault(this, this.text);
							@default.text = this.editorEngine.text.Substring(0, this.editorEngine.altCursorPosition);
							@default.wordWrapWidth = wordWrapWidth;
							@default.cursorIndex = this.editorEngine.altCursorPosition;
							Vector2 vector4 = this.m_TextHandle.GetCursorPosition(@default, scaling);
							vector4 -= scrollOffset;
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector4.x, vector4.y, 1f, num),
								color = cursorColor,
								playmodeTintColor = playmodeTintColor
							});
						}
					}
					keyboardTextEditorEventHandler.PostDrawCursor();
				}
			}

			internal virtual bool AcceptCharacter(char c)
			{
				return !this.isReadOnly;
			}

			protected virtual void BuildContextualMenu(ContextualMenuPopulateEvent evt)
			{
				bool flag = ((evt != null) ? evt.target : null) is TextInputBaseField<TValueType>.TextInputBase;
				if (flag)
				{
					bool flag2 = !this.isReadOnly;
					if (flag2)
					{
						evt.menu.AppendAction("Cut", new Action<DropdownMenuAction>(this.Cut), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CutCopyActionStatus), null);
					}
					evt.menu.AppendAction("Copy", new Action<DropdownMenuAction>(this.Copy), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CutCopyActionStatus), null);
					bool flag3 = !this.isReadOnly;
					if (flag3)
					{
						evt.menu.AppendAction("Paste", new Action<DropdownMenuAction>(this.Paste), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.PasteActionStatus), null);
					}
				}
			}

			private void OnDetectFocusChange()
			{
				bool flag = this.editorEngine.m_HasFocus && !this.hasFocus;
				if (flag)
				{
					this.editorEngine.OnFocus();
				}
				bool flag2 = !this.editorEngine.m_HasFocus && this.hasFocus;
				if (flag2)
				{
					this.editorEngine.OnLostFocus();
				}
			}

			private void OnCursorIndexChange()
			{
				base.IncrementVersion(VersionChangeType.Repaint);
			}

			protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
			{
				string text = this.m_Text;
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					text = " ";
				}
				return TextElement.MeasureVisualElementTextSize(this, text, desiredWidth, widthMode, desiredHeight, heightMode, this.m_TextHandle);
			}

			protected override void ExecuteDefaultActionAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionAtTarget(evt);
				bool flag = base.elementPanel != null && base.elementPanel.contextualMenuManager != null;
				if (flag)
				{
					base.elementPanel.contextualMenuManager.DisplayMenuIfEventMatches(evt, this);
				}
				long? num = (evt != null) ? new long?(evt.eventTypeId) : null;
				long num2 = EventBase<ContextualMenuPopulateEvent>.TypeId();
				bool flag2 = num.GetValueOrDefault() == num2 & num.HasValue;
				if (flag2)
				{
					ContextualMenuPopulateEvent contextualMenuPopulateEvent = evt as ContextualMenuPopulateEvent;
					int count = contextualMenuPopulateEvent.menu.MenuItems().Count;
					this.BuildContextualMenu(contextualMenuPopulateEvent);
					bool flag3 = count > 0 && contextualMenuPopulateEvent.menu.MenuItems().Count > count;
					if (flag3)
					{
						contextualMenuPopulateEvent.menu.InsertSeparator(null, count);
					}
				}
				else
				{
					bool flag4 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
					if (flag4)
					{
						this.SaveValueAndText();
					}
					else
					{
						bool flag5 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
						if (flag5)
						{
							KeyDownEvent keyDownEvent = evt as KeyDownEvent;
							bool flag6 = keyDownEvent != null && keyDownEvent.keyCode == KeyCode.Escape;
							if (flag6)
							{
								this.RestoreValueAndText();
								base.parent.Focus();
							}
						}
					}
				}
				this.editorEventHandler.ExecuteDefaultActionAtTarget(evt);
			}

			protected override void ExecuteDefaultAction(EventBase evt)
			{
				base.ExecuteDefaultAction(evt);
				this.editorEventHandler.ExecuteDefaultAction(evt);
			}

			void ITextInputField.SyncTextEngine()
			{
				this.SyncTextEngine();
			}

			bool ITextInputField.AcceptCharacter(char c)
			{
				return this.AcceptCharacter(c);
			}

			string ITextInputField.CullString(string s)
			{
				return this.CullString(s);
			}

			void ITextInputField.UpdateText(string value)
			{
				this.UpdateText(value);
			}

			void ITextInputField.UpdateValueFromText()
			{
				this.UpdateValueFromText();
			}

			private void DeferGUIStyleRectSync()
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPercentResolved), TrickleDown.NoTrickleDown);
			}

			private void OnPercentResolved(GeometryChangedEvent evt)
			{
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPercentResolved), TrickleDown.NoTrickleDown);
				GUIStyle style = this.editorEngine.style;
				int left = (int)base.resolvedStyle.marginLeft;
				int top = (int)base.resolvedStyle.marginTop;
				int right = (int)base.resolvedStyle.marginRight;
				int bottom = (int)base.resolvedStyle.marginBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.margin, left, top, right, bottom);
				left = (int)base.resolvedStyle.paddingLeft;
				top = (int)base.resolvedStyle.paddingTop;
				right = (int)base.resolvedStyle.paddingRight;
				bottom = (int)base.resolvedStyle.paddingBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.padding, left, top, right, bottom);
			}

			private static void SyncGUIStyle(TextInputBaseField<TValueType>.TextInputBase textInput, GUIStyle style)
			{
				ComputedStyle computedStyle = textInput.computedStyle;
				style.alignment = computedStyle.unityTextAlign.value;
				style.wordWrap = (computedStyle.whiteSpace.value == WhiteSpace.Normal);
				style.clipping = ((computedStyle.overflow.value == OverflowInternal.Visible) ? TextClipping.Overflow : TextClipping.Clip);
				bool flag = computedStyle.unityFont.value != null;
				if (flag)
				{
					style.font = computedStyle.unityFont.value;
				}
				style.fontSize = (int)computedStyle.fontSize.value.value;
				style.fontStyle = computedStyle.unityFontStyleAndWeight.value;
				int left = computedStyle.unitySliceLeft.value;
				int top = computedStyle.unitySliceTop.value;
				int right = computedStyle.unitySliceRight.value;
				int bottom = computedStyle.unitySliceBottom.value;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.border, left, top, right, bottom);
				bool flag2 = TextInputBaseField<TValueType>.TextInputBase.IsLayoutUsingPercent(textInput);
				if (flag2)
				{
					textInput.DeferGUIStyleRectSync();
				}
				else
				{
					left = (int)computedStyle.marginLeft.value.value;
					top = (int)computedStyle.marginTop.value.value;
					right = (int)computedStyle.marginRight.value.value;
					bottom = (int)computedStyle.marginBottom.value.value;
					TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.margin, left, top, right, bottom);
					left = (int)computedStyle.paddingLeft.value.value;
					top = (int)computedStyle.paddingTop.value.value;
					right = (int)computedStyle.paddingRight.value.value;
					bottom = (int)computedStyle.paddingBottom.value.value;
					TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.padding, left, top, right, bottom);
				}
			}

			private static bool IsLayoutUsingPercent(VisualElement ve)
			{
				ComputedStyle computedStyle = ve.computedStyle;
				bool flag = computedStyle.marginLeft.value.unit == LengthUnit.Percent || computedStyle.marginTop.value.unit == LengthUnit.Percent || computedStyle.marginRight.value.unit == LengthUnit.Percent || computedStyle.marginBottom.value.unit == LengthUnit.Percent;
				bool result;
				if (flag)
				{
					result = true;
				}
				else
				{
					bool flag2 = computedStyle.paddingLeft.value.unit == LengthUnit.Percent || computedStyle.paddingTop.value.unit == LengthUnit.Percent || computedStyle.paddingRight.value.unit == LengthUnit.Percent || computedStyle.paddingBottom.value.unit == LengthUnit.Percent;
					result = flag2;
				}
				return result;
			}

			private static void AssignRect(RectOffset rect, int left, int top, int right, int bottom)
			{
				rect.left = left;
				rect.top = top;
				rect.right = right;
				rect.bottom = bottom;
			}
		}

		private static CustomStyleProperty<Color> s_SelectionColorProperty = new CustomStyleProperty<Color>("--unity-selection-color");

		private static CustomStyleProperty<Color> s_CursorColorProperty = new CustomStyleProperty<Color>("--unity-cursor-color");

		private TextInputBaseField<TValueType>.TextInputBase m_TextInputBase;

		internal const int kMaxLengthNone = -1;

		internal const char kMaskCharDefault = '*';

		public new static readonly string ussClassName = "unity-base-text-field";

		public new static readonly string labelUssClassName = TextInputBaseField<TValueType>.ussClassName + "__label";

		public new static readonly string inputUssClassName = TextInputBaseField<TValueType>.ussClassName + "__input";

		public static readonly string textInputUssName = "unity-text-input";

		protected TextInputBaseField<TValueType>.TextInputBase textInputBase
		{
			get
			{
				return this.m_TextInputBase;
			}
		}

		internal TextHandle textHandle
		{
			get;
			private set;
		}

		public string text
		{
			get
			{
				return this.m_TextInputBase.text;
			}
			protected set
			{
				this.m_TextInputBase.text = value;
			}
		}

		public bool isReadOnly
		{
			get
			{
				return this.m_TextInputBase.isReadOnly;
			}
			set
			{
				this.m_TextInputBase.isReadOnly = value;
			}
		}

		public bool isPasswordField
		{
			get
			{
				return this.m_TextInputBase.isPasswordField;
			}
			set
			{
				bool flag = this.m_TextInputBase.isPasswordField == value;
				if (!flag)
				{
					this.m_TextInputBase.isPasswordField = value;
					this.m_TextInputBase.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		public Color selectionColor
		{
			get
			{
				return this.m_TextInputBase.selectionColor;
			}
		}

		public Color cursorColor
		{
			get
			{
				return this.m_TextInputBase.cursorColor;
			}
		}

		public int cursorIndex
		{
			get
			{
				return this.m_TextInputBase.cursorIndex;
			}
		}

		public int selectIndex
		{
			get
			{
				return this.m_TextInputBase.selectIndex;
			}
		}

		public int maxLength
		{
			get
			{
				return this.m_TextInputBase.maxLength;
			}
			set
			{
				this.m_TextInputBase.maxLength = value;
			}
		}

		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_TextInputBase.doubleClickSelectsWord;
			}
			set
			{
				this.m_TextInputBase.doubleClickSelectsWord = value;
			}
		}

		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TextInputBase.tripleClickSelectsLine;
			}
			set
			{
				this.m_TextInputBase.tripleClickSelectsLine = value;
			}
		}

		public bool isDelayed
		{
			get
			{
				return this.m_TextInputBase.isDelayed;
			}
			set
			{
				this.m_TextInputBase.isDelayed = value;
			}
		}

		public char maskChar
		{
			get
			{
				return this.m_TextInputBase.maskChar;
			}
			set
			{
				this.m_TextInputBase.maskChar = value;
			}
		}

		internal TextEditorEventHandler editorEventHandler
		{
			get
			{
				return this.m_TextInputBase.editorEventHandler;
			}
		}

		internal TextEditorEngine editorEngine
		{
			get
			{
				return this.m_TextInputBase.editorEngine;
			}
		}

		internal bool hasFocus
		{
			get
			{
				return this.m_TextInputBase.hasFocus;
			}
		}

		internal Vector2 MeasureTextSize(string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			return TextElement.MeasureVisualElementTextSize(this, textToMeasure, width, widthMode, height, heightMode, this.textHandle);
		}

		public void SelectAll()
		{
			this.m_TextInputBase.SelectAll();
		}

		internal void SyncTextEngine()
		{
			this.m_TextInputBase.SyncTextEngine();
		}

		internal void DrawWithTextSelectionAndCursor(MeshGenerationContext mgc, string newText)
		{
			this.m_TextInputBase.DrawWithTextSelectionAndCursor(mgc, newText, base.scaledPixelsPerPoint);
		}

		protected TextInputBaseField(int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase) : this(null, maxLength, maskChar, textInputBase)
		{
		}

		protected TextInputBaseField(string label, int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase)
		{
			this.<textHandle>k__BackingField = TextHandle.New();
			base..ctor(label, textInputBase);
			base.tabIndex = 0;
			base.delegatesFocus = false;
			base.AddToClassList(TextInputBaseField<TValueType>.ussClassName);
			base.labelElement.AddToClassList(TextInputBaseField<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
			this.m_TextInputBase = textInputBase;
			this.m_TextInputBase.maxLength = maxLength;
			this.m_TextInputBase.maskChar = maskChar;
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
		}

		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			TextHandle textHandle = this.textHandle;
			textHandle.useLegacy = (e.destinationPanel.contextType == ContextType.Editor);
			this.textHandle = textHandle;
		}

		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
				if (flag2)
				{
					KeyDownEvent keyDownEvent = evt as KeyDownEvent;
					char? c = (keyDownEvent != null) ? new char?(keyDownEvent.character) : null;
					int? num = c.HasValue ? new int?((int)c.GetValueOrDefault()) : null;
					int num2 = 3;
					bool arg_E3_0;
					if (!(num.GetValueOrDefault() == num2 & num.HasValue))
					{
						c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : null);
						num = (c.HasValue ? new int?((int)c.GetValueOrDefault()) : null);
						num2 = 10;
						arg_E3_0 = (num.GetValueOrDefault() == num2 & num.HasValue);
					}
					else
					{
						arg_E3_0 = true;
					}
					bool flag3 = arg_E3_0;
					if (flag3)
					{
						VisualElement expr_EE = base.visualInput;
						if (expr_EE != null)
						{
							expr_EE.Focus();
						}
					}
				}
			}
		}
	}
}
