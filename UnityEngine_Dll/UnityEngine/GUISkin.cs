using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[AssetFileNameExtension("guiskin", new string[]
	{

	}), ExecuteInEditMode, RequiredByNativeCode]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		internal delegate void SkinChangedDelegate();

		[SerializeField]
		private Font m_Font;

		[SerializeField]
		private GUIStyle m_box;

		[SerializeField]
		private GUIStyle m_button;

		[SerializeField]
		private GUIStyle m_toggle;

		[SerializeField]
		private GUIStyle m_label;

		[SerializeField]
		private GUIStyle m_textField;

		[SerializeField]
		private GUIStyle m_textArea;

		[SerializeField]
		private GUIStyle m_window;

		[SerializeField]
		private GUIStyle m_horizontalSlider;

		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		[NonSerialized]
		private GUIStyle m_horizontalSliderThumbExtent;

		[SerializeField]
		private GUIStyle m_verticalSlider;

		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		[NonSerialized]
		private GUIStyle m_verticalSliderThumbExtent;

		[NonSerialized]
		private GUIStyle m_SliderMixed;

		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		[SerializeField]
		private GUIStyle m_ScrollView;

		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		internal static GUIStyle ms_Error;

		private Dictionary<string, GUIStyle> m_Styles = null;

		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		internal static GUISkin current;

		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				bool flag = GUISkin.current == this;
				if (flag)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		internal GUIStyle horizontalSliderThumbExtent
		{
			get
			{
				return this.m_horizontalSliderThumbExtent;
			}
			set
			{
				this.m_horizontalSliderThumbExtent = value;
				this.Apply();
			}
		}

		internal GUIStyle sliderMixed
		{
			get
			{
				return this.m_SliderMixed;
			}
			set
			{
				this.m_SliderMixed = value;
				this.Apply();
			}
		}

		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		internal GUIStyle verticalSliderThumbExtent
		{
			get
			{
				return this.m_verticalSliderThumbExtent;
			}
			set
			{
				this.m_verticalSliderThumbExtent = value;
				this.Apply();
			}
		}

		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		internal static GUIStyle error
		{
			get
			{
				bool flag = GUISkin.ms_Error == null;
				if (flag)
				{
					GUISkin.ms_Error = new GUIStyle();
					GUISkin.ms_Error.name = "StyleNotFoundError";
				}
				return GUISkin.ms_Error;
			}
		}

		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		internal void OnEnable()
		{
			this.Apply();
		}

		internal static void CleanupRoots()
		{
			GUISkin.current = null;
			GUISkin.ms_Error = null;
		}

		internal void Apply()
		{
			bool flag = this.m_CustomStyles == null;
			if (flag)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		private void BuildStyleCache()
		{
			bool flag = this.m_box == null;
			if (flag)
			{
				this.m_box = new GUIStyle();
			}
			bool flag2 = this.m_button == null;
			if (flag2)
			{
				this.m_button = new GUIStyle();
			}
			bool flag3 = this.m_toggle == null;
			if (flag3)
			{
				this.m_toggle = new GUIStyle();
			}
			bool flag4 = this.m_label == null;
			if (flag4)
			{
				this.m_label = new GUIStyle();
			}
			bool flag5 = this.m_window == null;
			if (flag5)
			{
				this.m_window = new GUIStyle();
			}
			bool flag6 = this.m_textField == null;
			if (flag6)
			{
				this.m_textField = new GUIStyle();
			}
			bool flag7 = this.m_textArea == null;
			if (flag7)
			{
				this.m_textArea = new GUIStyle();
			}
			bool flag8 = this.m_horizontalSlider == null;
			if (flag8)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			bool flag9 = this.m_horizontalSliderThumb == null;
			if (flag9)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			bool flag10 = this.m_verticalSlider == null;
			if (flag10)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			bool flag11 = this.m_verticalSliderThumb == null;
			if (flag11)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			bool flag12 = this.m_horizontalScrollbar == null;
			if (flag12)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			bool flag13 = this.m_horizontalScrollbarThumb == null;
			if (flag13)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			bool flag14 = this.m_horizontalScrollbarLeftButton == null;
			if (flag14)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			bool flag15 = this.m_horizontalScrollbarRightButton == null;
			if (flag15)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			bool flag16 = this.m_verticalScrollbar == null;
			if (flag16)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			bool flag17 = this.m_verticalScrollbarThumb == null;
			if (flag17)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			bool flag18 = this.m_verticalScrollbarUpButton == null;
			if (flag18)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			bool flag19 = this.m_verticalScrollbarDownButton == null;
			if (flag19)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			bool flag20 = this.m_ScrollView == null;
			if (flag20)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.m_Styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.m_Styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.m_Styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.m_Styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.m_Styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.m_Styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.m_Styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.m_Styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.m_Styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.m_Styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.m_Styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.m_Styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.m_Styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.m_Styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.m_Styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.m_Styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.m_Styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.m_Styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.m_Styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.m_Styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.m_Styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			bool flag21 = this.m_CustomStyles != null;
			if (flag21)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					bool flag22 = this.m_CustomStyles[i] == null;
					if (!flag22)
					{
						this.m_Styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			bool flag23 = !this.m_Styles.TryGetValue("HorizontalSliderThumbExtent", out this.m_horizontalSliderThumbExtent);
			if (flag23)
			{
				this.m_horizontalSliderThumbExtent = new GUIStyle();
				this.m_horizontalSliderThumbExtent.name = "horizontalsliderthumbextent";
				this.m_Styles["HorizontalSliderThumbExtent"] = this.m_horizontalSliderThumbExtent;
			}
			bool flag24 = !this.m_Styles.TryGetValue("SliderMixed", out this.m_SliderMixed);
			if (flag24)
			{
				this.m_SliderMixed = new GUIStyle();
				this.m_SliderMixed.name = "SliderMixed";
				this.m_Styles["SliderMixed"] = this.m_SliderMixed;
			}
			bool flag25 = !this.m_Styles.TryGetValue("VerticalSliderThumbExtent", out this.m_verticalSliderThumbExtent);
			if (flag25)
			{
				this.m_verticalSliderThumbExtent = new GUIStyle();
				this.m_Styles["VerticalSliderThumbExtent"] = this.m_verticalSliderThumbExtent;
				this.m_verticalSliderThumbExtent.name = "verticalsliderthumbextent";
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle gUIStyle = this.FindStyle(styleName);
			bool flag = gUIStyle != null;
			GUIStyle result;
			if (flag)
			{
				result = gUIStyle;
			}
			else
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to find style '",
					styleName,
					"' in skin '",
					base.name,
					"' ",
					(Event.current != null) ? Event.current.type.ToString() : "<called outside OnGUI>"
				}));
				result = GUISkin.error;
			}
			return result;
		}

		public GUIStyle FindStyle(string styleName)
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			GUIStyle gUIStyle;
			bool flag2 = this.m_Styles.TryGetValue(styleName, out gUIStyle);
			GUIStyle result;
			if (flag2)
			{
				result = gUIStyle;
			}
			else
			{
				result = null;
			}
			return result;
		}

		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			bool flag = GUISkin.m_SkinChanged != null;
			if (flag)
			{
				GUISkin.m_SkinChanged();
			}
		}

		public IEnumerator GetEnumerator()
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			return this.m_Styles.Values.GetEnumerator();
		}
	}
}
