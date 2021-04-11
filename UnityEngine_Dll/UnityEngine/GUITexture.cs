using System;
using System.ComponentModel;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("GUITexture has been removed. Use UI.Image instead.", true), ExcludeFromObjectFactory, ExcludeFromPreset]
	public sealed class GUITexture
	{
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Color color
		{
			get
			{
				GUITexture.FeatureRemoved();
				return new Color(0f, 0f, 0f);
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Texture texture
		{
			get
			{
				GUITexture.FeatureRemoved();
				return null;
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Rect pixelInset
		{
			get
			{
				GUITexture.FeatureRemoved();
				return default(Rect);
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public RectOffset border
		{
			get
			{
				GUITexture.FeatureRemoved();
				return null;
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		private static void FeatureRemoved()
		{
			throw new Exception("GUITexture has been removed from Unity. Use UI.Image instead.");
		}
	}
}
