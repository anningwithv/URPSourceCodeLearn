using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	public class Image : VisualElement
	{
		public new class UxmlFactory : UxmlFactory<Image, Image.UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}
		}

		private ScaleMode m_ScaleMode;

		private Texture m_Image;

		private VectorImage m_VectorImage;

		private Rect m_UV;

		private Color m_TintColor;

		private bool m_ImageIsInline;

		private bool m_ScaleModeIsInline;

		private bool m_TintColorIsInline;

		public static readonly string ussClassName = "unity-image";

		private static CustomStyleProperty<Texture2D> s_ImageProperty = new CustomStyleProperty<Texture2D>("--unity-image");

		private static CustomStyleProperty<VectorImage> s_VectorImageProperty = new CustomStyleProperty<VectorImage>("--unity-image");

		private static CustomStyleProperty<string> s_ScaleModeProperty = new CustomStyleProperty<string>("--unity-image-size");

		private static CustomStyleProperty<Color> s_TintColorProperty = new CustomStyleProperty<Color>("--unity-image-tint-color");

		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				bool flag = value != null && this.vectorImage != null;
				if (flag)
				{
					Debug.LogError("Both image and vectorImage are set on Image object");
					this.m_VectorImage = null;
				}
				this.m_ImageIsInline = (value != null);
				bool flag2 = this.m_Image != value;
				if (flag2)
				{
					this.m_Image = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					bool flag3 = this.m_Image == null;
					if (flag3)
					{
						this.m_UV = new Rect(0f, 0f, 1f, 1f);
					}
				}
			}
		}

		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = value != null && this.image != null;
				if (flag)
				{
					Debug.LogError("Both image and vectorImage are set on Image object");
					this.m_Image = null;
				}
				this.m_ImageIsInline = (value != null);
				bool flag2 = this.m_VectorImage != value;
				if (flag2)
				{
					this.m_VectorImage = value;
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					bool flag3 = this.m_VectorImage == null;
					if (flag3)
					{
						this.m_UV = new Rect(0f, 0f, 1f, 1f);
					}
				}
			}
		}

		public Rect sourceRect
		{
			get
			{
				return this.GetSourceRect();
			}
			set
			{
				this.CalculateUV(value);
			}
		}

		public Rect uv
		{
			get
			{
				return this.m_UV;
			}
			set
			{
				this.m_UV = value;
			}
		}

		public ScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleModeIsInline = true;
				bool flag = this.m_ScaleMode != value;
				if (flag)
				{
					this.m_ScaleMode = value;
					base.IncrementVersion(VersionChangeType.Layout);
				}
			}
		}

		public Color tintColor
		{
			get
			{
				return this.m_TintColor;
			}
			set
			{
				this.m_TintColorIsInline = true;
				bool flag = this.m_TintColor != value;
				if (flag)
				{
					this.m_TintColor = value;
					base.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		public Image()
		{
			base.AddToClassList(Image.ussClassName);
			this.m_ScaleMode = ScaleMode.ScaleAndCrop;
			this.m_TintColor = Color.white;
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			base.requireMeasureFunction = true;
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		private Vector2 GetTextureDisplaySize(Texture texture)
		{
			Vector2 vector = Vector2.zero;
			bool flag = texture != null;
			if (flag)
			{
				vector = new Vector2((float)texture.width, (float)texture.height);
				Texture2D texture2D = texture as Texture2D;
				bool flag2 = texture2D != null;
				if (flag2)
				{
					vector /= texture2D.pixelsPerPoint;
				}
			}
			return vector;
		}

		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = this.image == null && this.vectorImage == null;
			Vector2 result;
			if (flag)
			{
				result = new Vector2(num, num2);
			}
			else
			{
				Vector2 vector = Vector2.zero;
				bool flag2 = this.image != null;
				if (flag2)
				{
					vector = this.GetTextureDisplaySize(this.image);
				}
				else
				{
					vector = this.vectorImage.size;
				}
				Rect sourceRect = this.sourceRect;
				bool flag3 = sourceRect != Rect.zero;
				num = (flag3 ? sourceRect.width : vector.x);
				num2 = (flag3 ? sourceRect.height : vector.y);
				bool flag4 = widthMode == VisualElement.MeasureMode.AtMost;
				if (flag4)
				{
					num = Mathf.Min(num, desiredWidth);
				}
				bool flag5 = heightMode == VisualElement.MeasureMode.AtMost;
				if (flag5)
				{
					num2 = Mathf.Min(num2, desiredHeight);
				}
				result = new Vector2(num, num2);
			}
			return result;
		}

		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.image == null && this.vectorImage == null;
			if (!flag)
			{
				MeshGenerationContextUtils.RectangleParams rectParams = default(MeshGenerationContextUtils.RectangleParams);
				bool flag2 = this.image != null;
				if (flag2)
				{
					rectParams = MeshGenerationContextUtils.RectangleParams.MakeTextured(base.contentRect, this.uv, this.image, this.scaleMode, base.panel.contextType);
				}
				else
				{
					bool flag3 = this.vectorImage != null;
					if (flag3)
					{
						rectParams = MeshGenerationContextUtils.RectangleParams.MakeVectorTextured(base.contentRect, this.uv, this.vectorImage, this.scaleMode, base.panel.contextType);
					}
				}
				rectParams.color = this.tintColor;
				mgc.Rectangle(rectParams);
			}
		}

		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			Texture2D image = null;
			VectorImage vectorImage = null;
			Color white = Color.white;
			ICustomStyle customStyle = e.customStyle;
			bool flag = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_ImageProperty, out image);
			if (flag)
			{
				this.m_Image = image;
				bool flag2 = this.m_Image != null;
				if (flag2)
				{
					this.m_VectorImage = null;
				}
			}
			bool flag3 = !this.m_ImageIsInline && customStyle.TryGetValue(Image.s_VectorImageProperty, out vectorImage);
			if (flag3)
			{
				this.m_VectorImage = vectorImage;
				bool flag4 = this.m_VectorImage != null;
				if (flag4)
				{
					this.m_Image = null;
				}
			}
			string value;
			bool flag5 = !this.m_ScaleModeIsInline && customStyle.TryGetValue(Image.s_ScaleModeProperty, out value);
			if (flag5)
			{
				this.m_ScaleMode = (ScaleMode)StylePropertyUtil.GetEnumIntValue(StyleEnumType.ScaleMode, value);
			}
			bool flag6 = !this.m_TintColorIsInline && customStyle.TryGetValue(Image.s_TintColorProperty, out white);
			if (flag6)
			{
				this.m_TintColor = white;
			}
		}

		private void CalculateUV(Rect srcRect)
		{
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				this.m_UV.x = srcRect.x / vector.x;
				this.m_UV.width = srcRect.width / vector.x;
				this.m_UV.height = srcRect.height / vector.y;
				this.m_UV.y = 1f - this.m_UV.height - srcRect.y / vector.y;
			}
		}

		private Rect GetSourceRect()
		{
			Rect zero = Rect.zero;
			Vector2 vector = Vector2.zero;
			Texture image = this.image;
			bool flag = image != null;
			if (flag)
			{
				vector = this.GetTextureDisplaySize(image);
			}
			VectorImage vectorImage = this.vectorImage;
			bool flag2 = vectorImage != null;
			if (flag2)
			{
				vector = vectorImage.size;
			}
			bool flag3 = vector != Vector2.zero;
			if (flag3)
			{
				zero.x = this.uv.x * vector.x;
				zero.width = this.uv.width * vector.x;
				zero.y = (1f - this.uv.y - this.uv.height) * vector.y;
				zero.height = this.uv.height * vector.y;
			}
			return zero;
		}
	}
}
