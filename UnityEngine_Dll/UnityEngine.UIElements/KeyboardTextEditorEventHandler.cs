using System;

namespace UnityEngine.UIElements
{
	internal class KeyboardTextEditorEventHandler : TextEditorEventHandler
	{
		internal bool m_Changed;

		private bool m_Dragged;

		private bool m_DragToPosition;

		private bool m_PostponeMove;

		private bool m_SelectAllOnMouseUp = true;

		private string m_PreDrawCursorText;

		private readonly Event m_ImguiEvent = new Event();

		public KeyboardTextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField) : base(editorEngine, textInputField)
		{
		}

		public override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
			if (flag)
			{
				this.OnFocus(evt as FocusEvent);
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.OnBlur(evt as BlurEvent);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<MouseDownEvent>.TypeId();
					if (flag3)
					{
						this.OnMouseDown(evt as MouseDownEvent);
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<MouseUpEvent>.TypeId();
						if (flag4)
						{
							this.OnMouseUp(evt as MouseUpEvent);
						}
						else
						{
							bool flag5 = evt.eventTypeId == EventBase<MouseMoveEvent>.TypeId();
							if (flag5)
							{
								this.OnMouseMove(evt as MouseMoveEvent);
							}
							else
							{
								bool flag6 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
								if (flag6)
								{
									this.OnKeyDown(evt as KeyDownEvent);
								}
								else
								{
									bool flag7 = evt.eventTypeId == EventBase<ValidateCommandEvent>.TypeId();
									if (flag7)
									{
										this.OnValidateCommandEvent(evt as ValidateCommandEvent);
									}
									else
									{
										bool flag8 = evt.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
										if (flag8)
										{
											this.OnExecuteCommandEvent(evt as ExecuteCommandEvent);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void OnFocus(FocusEvent _)
		{
			GUIUtility.imeCompositionMode = IMECompositionMode.On;
			this.m_DragToPosition = false;
		}

		private void OnBlur(BlurEvent _)
		{
			GUIUtility.imeCompositionMode = IMECompositionMode.Auto;
		}

		private void OnMouseDown(MouseDownEvent evt)
		{
			base.textInputField.SyncTextEngine();
			this.m_Changed = false;
			bool flag = !base.textInputField.hasFocus;
			if (flag)
			{
				base.editorEngine.m_HasFocus = true;
				base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, evt.button == 0 && evt.shiftKey);
				bool flag2 = evt.button == 0;
				if (flag2)
				{
					base.textInputField.CaptureMouse();
				}
				evt.StopPropagation();
			}
			else
			{
				bool flag3 = evt.button == 0;
				if (flag3)
				{
					bool flag4 = evt.clickCount == 2 && base.textInputField.doubleClickSelectsWord;
					if (flag4)
					{
						base.editorEngine.SelectCurrentWord();
						base.editorEngine.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
						base.editorEngine.MouseDragSelectsWholeWords(true);
						this.m_DragToPosition = false;
					}
					else
					{
						bool flag5 = evt.clickCount == 3 && base.textInputField.tripleClickSelectsLine;
						if (flag5)
						{
							base.editorEngine.SelectCurrentParagraph();
							base.editorEngine.MouseDragSelectsWholeWords(true);
							base.editorEngine.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
							this.m_DragToPosition = false;
						}
						else
						{
							base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, evt.shiftKey);
							this.m_SelectAllOnMouseUp = false;
						}
					}
					base.textInputField.CaptureMouse();
					evt.StopPropagation();
				}
				else
				{
					bool flag6 = evt.button == 1;
					if (flag6)
					{
						bool flag7 = base.editorEngine.cursorIndex == base.editorEngine.selectIndex;
						if (flag7)
						{
							base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, false);
						}
						this.m_SelectAllOnMouseUp = false;
						this.m_DragToPosition = false;
					}
				}
			}
			base.editorEngine.UpdateScrollOffset();
		}

		private void OnMouseUp(MouseUpEvent evt)
		{
			bool flag = evt.button != 0;
			if (!flag)
			{
				bool flag2 = !base.textInputField.HasMouseCapture();
				if (!flag2)
				{
					base.textInputField.SyncTextEngine();
					this.m_Changed = false;
					bool flag3 = this.m_Dragged && this.m_DragToPosition;
					if (flag3)
					{
						base.editorEngine.MoveSelectionToAltCursor();
					}
					else
					{
						bool postponeMove = this.m_PostponeMove;
						if (postponeMove)
						{
							base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, evt.shiftKey);
						}
						else
						{
							bool selectAllOnMouseUp = this.m_SelectAllOnMouseUp;
							if (selectAllOnMouseUp)
							{
								this.m_SelectAllOnMouseUp = false;
							}
						}
					}
					base.editorEngine.MouseDragSelectsWholeWords(false);
					base.textInputField.ReleaseMouse();
					this.m_DragToPosition = true;
					this.m_Dragged = false;
					this.m_PostponeMove = false;
					evt.StopPropagation();
					base.editorEngine.UpdateScrollOffset();
				}
			}
		}

		private void OnMouseMove(MouseMoveEvent evt)
		{
			bool flag = evt.button != 0;
			if (!flag)
			{
				bool flag2 = !base.textInputField.HasMouseCapture();
				if (!flag2)
				{
					base.textInputField.SyncTextEngine();
					this.m_Changed = false;
					bool flag3 = !evt.shiftKey && base.editorEngine.hasSelection && this.m_DragToPosition;
					if (flag3)
					{
						base.editorEngine.MoveAltCursorToPosition(evt.localMousePosition);
					}
					else
					{
						bool shiftKey = evt.shiftKey;
						if (shiftKey)
						{
							base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, evt.shiftKey);
						}
						else
						{
							base.editorEngine.SelectToPosition(evt.localMousePosition);
						}
						this.m_DragToPosition = false;
						this.m_SelectAllOnMouseUp = !base.editorEngine.hasSelection;
					}
					this.m_Dragged = true;
					evt.StopPropagation();
					base.editorEngine.UpdateScrollOffset();
				}
			}
		}

		private void OnKeyDown(KeyDownEvent evt)
		{
			bool flag = !base.textInputField.hasFocus;
			if (!flag)
			{
				base.textInputField.SyncTextEngine();
				this.m_Changed = false;
				evt.GetEquivalentImguiEvent(this.m_ImguiEvent);
				bool flag2 = base.editorEngine.HandleKeyEvent(this.m_ImguiEvent, base.textInputField.isReadOnly);
				if (flag2)
				{
					bool flag3 = base.textInputField.text != base.editorEngine.text;
					if (flag3)
					{
						this.m_Changed = true;
					}
					evt.StopPropagation();
				}
				else
				{
					char character = evt.character;
					bool flag4 = !base.editorEngine.multiline && (evt.keyCode == KeyCode.Tab || character == '\t');
					if (flag4)
					{
						return;
					}
					bool flag5 = base.editorEngine.multiline && (evt.keyCode == KeyCode.Tab || character == '\t') && evt.modifiers > EventModifiers.None;
					if (flag5)
					{
						return;
					}
					bool flag6 = evt.actionKey && (!evt.altKey || character == '\0');
					if (flag6)
					{
						return;
					}
					evt.StopPropagation();
					bool flag7 = character == '\n' && !base.editorEngine.multiline && !evt.altKey;
					if (flag7)
					{
						return;
					}
					bool flag8 = character == '\n' && base.editorEngine.multiline && evt.shiftKey;
					if (flag8)
					{
						return;
					}
					bool flag9 = !base.textInputField.AcceptCharacter(character);
					if (flag9)
					{
						return;
					}
					Font font = base.editorEngine.style.font;
					bool flag10 = (font != null && font.HasCharacter(character)) || character == '\n' || character == '\t';
					if (flag10)
					{
						base.editorEngine.Insert(character);
						this.m_Changed = true;
					}
					else
					{
						bool flag11 = character == '\0';
						if (flag11)
						{
							bool flag12 = !string.IsNullOrEmpty(GUIUtility.compositionString);
							if (flag12)
							{
								base.editorEngine.ReplaceSelection("");
								this.m_Changed = true;
							}
						}
					}
				}
				bool changed = this.m_Changed;
				if (changed)
				{
					base.editorEngine.text = base.textInputField.CullString(base.editorEngine.text);
					base.textInputField.UpdateText(base.editorEngine.text);
				}
				base.editorEngine.UpdateScrollOffset();
			}
		}

		private void OnValidateCommandEvent(ValidateCommandEvent evt)
		{
			bool flag = !base.textInputField.hasFocus;
			if (!flag)
			{
				base.textInputField.SyncTextEngine();
				this.m_Changed = false;
				string commandName = evt.commandName;
				string text = commandName;
				if (text != null)
				{
					if (!(text == "Cut"))
					{
						if (!(text == "Copy"))
						{
							if (!(text == "Paste"))
							{
								if (!(text == "SelectAll"))
								{
									if (!(text == "Delete"))
									{
										if (!(text == "UndoRedoPerformed"))
										{
										}
									}
									else
									{
										bool isReadOnly = base.textInputField.isReadOnly;
										if (isReadOnly)
										{
											return;
										}
									}
								}
							}
							else
							{
								bool flag2 = !base.editorEngine.CanPaste() || base.textInputField.isReadOnly;
								if (flag2)
								{
									return;
								}
							}
						}
						else
						{
							bool flag3 = !base.editorEngine.hasSelection;
							if (flag3)
							{
								return;
							}
						}
					}
					else
					{
						bool flag4 = !base.editorEngine.hasSelection || base.textInputField.isReadOnly;
						if (flag4)
						{
							return;
						}
					}
				}
				evt.StopPropagation();
			}
		}

		private void OnExecuteCommandEvent(ExecuteCommandEvent evt)
		{
			bool flag = !base.textInputField.hasFocus;
			if (!flag)
			{
				base.textInputField.SyncTextEngine();
				this.m_Changed = false;
				bool flag2 = false;
				string text = base.editorEngine.text;
				bool flag3 = !base.textInputField.hasFocus;
				if (!flag3)
				{
					string commandName = evt.commandName;
					string text2 = commandName;
					if (text2 != null)
					{
						if (text2 == "OnLostFocus")
						{
							evt.StopPropagation();
							return;
						}
						if (!(text2 == "Cut"))
						{
							if (text2 == "Copy")
							{
								base.editorEngine.Copy();
								evt.StopPropagation();
								return;
							}
							if (!(text2 == "Paste"))
							{
								if (text2 == "SelectAll")
								{
									base.editorEngine.SelectAll();
									evt.StopPropagation();
									return;
								}
								if (text2 == "Delete")
								{
									bool flag4 = !base.textInputField.isReadOnly;
									if (flag4)
									{
										bool flag5 = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX;
										if (flag5)
										{
											base.editorEngine.Delete();
										}
										else
										{
											base.editorEngine.Cut();
										}
										flag2 = true;
									}
								}
							}
							else
							{
								bool flag6 = !base.textInputField.isReadOnly;
								if (flag6)
								{
									base.editorEngine.Paste();
									flag2 = true;
								}
							}
						}
						else
						{
							bool flag7 = !base.textInputField.isReadOnly;
							if (flag7)
							{
								base.editorEngine.Cut();
								flag2 = true;
							}
						}
					}
					bool flag8 = flag2;
					if (flag8)
					{
						bool flag9 = text != base.editorEngine.text;
						if (flag9)
						{
							this.m_Changed = true;
						}
						evt.StopPropagation();
					}
					bool changed = this.m_Changed;
					if (changed)
					{
						base.editorEngine.text = base.textInputField.CullString(base.editorEngine.text);
						base.textInputField.UpdateText(base.editorEngine.text);
						evt.StopPropagation();
					}
					base.editorEngine.UpdateScrollOffset();
				}
			}
		}

		public void PreDrawCursor(string newText)
		{
			base.textInputField.SyncTextEngine();
			this.m_PreDrawCursorText = base.editorEngine.text;
			int num = base.editorEngine.cursorIndex;
			bool flag = !string.IsNullOrEmpty(GUIUtility.compositionString);
			if (flag)
			{
				base.editorEngine.text = newText.Substring(0, base.editorEngine.cursorIndex) + GUIUtility.compositionString + newText.Substring(base.editorEngine.selectIndex);
				num += GUIUtility.compositionString.Length;
			}
			else
			{
				base.editorEngine.text = newText;
			}
			base.editorEngine.text = base.textInputField.CullString(base.editorEngine.text);
			num = Math.Min(num, base.editorEngine.text.Length);
			base.editorEngine.graphicalCursorPos = base.editorEngine.style.GetCursorPixelPosition(base.editorEngine.localPosition, new GUIContent(base.editorEngine.text), num);
		}

		public void PostDrawCursor()
		{
			base.editorEngine.text = this.m_PreDrawCursorText;
		}
	}
}
