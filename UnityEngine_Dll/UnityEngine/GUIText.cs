using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("GUIText has been removed. Use UI.Text instead.", true), ExcludeFromObjectFactory, ExcludeFromPreset]
	public sealed class GUIText
	{
		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public bool text
		{
			get
			{
				GUIText.FeatureRemoved();
				return false;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public Material material
		{
			get
			{
				GUIText.FeatureRemoved();
				return null;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public Font font
		{
			get
			{
				GUIText.FeatureRemoved();
				return null;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public TextAlignment alignment
		{
			get
			{
				GUIText.FeatureRemoved();
				return TextAlignment.Left;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public TextAnchor anchor
		{
			get
			{
				GUIText.FeatureRemoved();
				return TextAnchor.UpperLeft;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public float lineSpacing
		{
			get
			{
				GUIText.FeatureRemoved();
				return 0f;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public float tabSize
		{
			get
			{
				GUIText.FeatureRemoved();
				return 0f;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public int fontSize
		{
			get
			{
				GUIText.FeatureRemoved();
				return 0;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public FontStyle fontStyle
		{
			get
			{
				GUIText.FeatureRemoved();
				return FontStyle.Normal;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public bool richText
		{
			get
			{
				GUIText.FeatureRemoved();
				return false;
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public Color color
		{
			get
			{
				GUIText.FeatureRemoved();
				return new Color(0f, 0f, 0f);
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		[Obsolete("GUIText has been removed. Use UI.Text instead.", true)]
		public Vector2 pixelOffset
		{
			get
			{
				GUIText.FeatureRemoved();
				return new Vector2(0f, 0f);
			}
			set
			{
				GUIText.FeatureRemoved();
			}
		}

		private static void FeatureRemoved()
		{
			throw new Exception("GUIText has been removed from Unity. Use UI.Text instead.");
		}
	}
}
