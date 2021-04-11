using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Modules/TextRendering/TextGenerator.h"), UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TextGenerator : IDisposable
	{
		internal IntPtr m_Ptr;

		private string m_LastString;

		private TextGenerationSettings m_LastSettings;

		private bool m_HasGenerated;

		private TextGenerationError m_LastValid;

		private readonly List<UIVertex> m_Verts;

		private readonly List<UICharInfo> m_Characters;

		private readonly List<UILineInfo> m_Lines;

		private bool m_CachedVerts;

		private bool m_CachedCharacters;

		private bool m_CachedLines;

		private static int s_NextId = 0;

		private readonly int m_Id;

		private static readonly Dictionary<int, WeakReference> s_Instances = new Dictionary<int, WeakReference>();

		public int characterCountVisible
		{
			get
			{
				return this.characterCount - 1;
			}
		}

		public IList<UIVertex> verts
		{
			get
			{
				bool flag = !this.m_CachedVerts;
				if (flag)
				{
					this.GetVertices(this.m_Verts);
					this.m_CachedVerts = true;
				}
				return this.m_Verts;
			}
		}

		public IList<UICharInfo> characters
		{
			get
			{
				bool flag = !this.m_CachedCharacters;
				if (flag)
				{
					this.GetCharacters(this.m_Characters);
					this.m_CachedCharacters = true;
				}
				return this.m_Characters;
			}
		}

		public IList<UILineInfo> lines
		{
			get
			{
				bool flag = !this.m_CachedLines;
				if (flag)
				{
					this.GetLines(this.m_Lines);
					this.m_CachedLines = true;
				}
				return this.m_Lines;
			}
		}

		public Rect rectExtents
		{
			get
			{
				Rect result;
				this.get_rectExtents_Injected(out result);
				return result;
			}
		}

		public extern int vertexCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int characterCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int lineCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("FontSizeFoundForBestFit", false, TargetType.Function)]
		public extern int fontSizeUsedForBestFit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public TextGenerator() : this(50)
		{
		}

		public TextGenerator(int initialCapacity)
		{
			this.m_Ptr = TextGenerator.Internal_Create();
			this.m_Verts = new List<UIVertex>((initialCapacity + 1) * 4);
			this.m_Characters = new List<UICharInfo>(initialCapacity + 1);
			this.m_Lines = new List<UILineInfo>(20);
			Dictionary<int, WeakReference> obj = TextGenerator.s_Instances;
			lock (obj)
			{
				this.m_Id = TextGenerator.s_NextId++;
				TextGenerator.s_Instances.Add(this.m_Id, new WeakReference(this));
			}
		}

		~TextGenerator()
		{
			((IDisposable)this).Dispose();
		}

		void IDisposable.Dispose()
		{
			Dictionary<int, WeakReference> obj = TextGenerator.s_Instances;
			lock (obj)
			{
				TextGenerator.s_Instances.Remove(this.m_Id);
			}
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				TextGenerator.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		[RequiredByNativeCode]
		internal static void InvalidateAll()
		{
			Dictionary<int, WeakReference> obj = TextGenerator.s_Instances;
			lock (obj)
			{
				foreach (KeyValuePair<int, WeakReference> current in TextGenerator.s_Instances)
				{
					WeakReference value = current.Value;
					bool isAlive = value.IsAlive;
					if (isAlive)
					{
						(value.Target as TextGenerator).Invalidate();
					}
				}
			}
		}

		private TextGenerationSettings ValidatedSettings(TextGenerationSettings settings)
		{
			bool flag = settings.font != null && settings.font.dynamic;
			TextGenerationSettings result;
			if (flag)
			{
				result = settings;
			}
			else
			{
				bool flag2 = settings.fontSize != 0 || settings.fontStyle > FontStyle.Normal;
				if (flag2)
				{
					bool flag3 = settings.font != null;
					if (flag3)
					{
						Debug.LogWarningFormat(settings.font, "Font size and style overrides are only supported for dynamic fonts. Font '{0}' is not dynamic.", new object[]
						{
							settings.font.name
						});
					}
					settings.fontSize = 0;
					settings.fontStyle = FontStyle.Normal;
				}
				bool resizeTextForBestFit = settings.resizeTextForBestFit;
				if (resizeTextForBestFit)
				{
					bool flag4 = settings.font != null;
					if (flag4)
					{
						Debug.LogWarningFormat(settings.font, "BestFit is only supported for dynamic fonts. Font '{0}' is not dynamic.", new object[]
						{
							settings.font.name
						});
					}
					settings.resizeTextForBestFit = false;
				}
				result = settings;
			}
			return result;
		}

		public void Invalidate()
		{
			this.m_HasGenerated = false;
		}

		public void GetCharacters(List<UICharInfo> characters)
		{
			this.GetCharactersInternal(characters);
		}

		public void GetLines(List<UILineInfo> lines)
		{
			this.GetLinesInternal(lines);
		}

		public void GetVertices(List<UIVertex> vertices)
		{
			this.GetVerticesInternal(vertices);
		}

		public float GetPreferredWidth(string str, TextGenerationSettings settings)
		{
			settings.horizontalOverflow = HorizontalWrapMode.Overflow;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.width;
		}

		public float GetPreferredHeight(string str, TextGenerationSettings settings)
		{
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.height;
		}

		public bool PopulateWithErrors(string str, TextGenerationSettings settings, GameObject context)
		{
			TextGenerationError textGenerationError = this.PopulateWithError(str, settings);
			bool flag = textGenerationError == TextGenerationError.None;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = (textGenerationError & TextGenerationError.CustomSizeOnNonDynamicFont) > TextGenerationError.None;
				if (flag2)
				{
					Debug.LogErrorFormat(context, "Font '{0}' is not dynamic, which is required to override its size", new object[]
					{
						settings.font
					});
				}
				bool flag3 = (textGenerationError & TextGenerationError.CustomStyleOnNonDynamicFont) > TextGenerationError.None;
				if (flag3)
				{
					Debug.LogErrorFormat(context, "Font '{0}' is not dynamic, which is required to override its style", new object[]
					{
						settings.font
					});
				}
				result = false;
			}
			return result;
		}

		public bool Populate(string str, TextGenerationSettings settings)
		{
			TextGenerationError textGenerationError = this.PopulateWithError(str, settings);
			return textGenerationError == TextGenerationError.None;
		}

		private TextGenerationError PopulateWithError(string str, TextGenerationSettings settings)
		{
			bool flag = this.m_HasGenerated && str == this.m_LastString && settings.Equals(this.m_LastSettings);
			TextGenerationError lastValid;
			if (flag)
			{
				lastValid = this.m_LastValid;
			}
			else
			{
				this.m_LastValid = this.PopulateAlways(str, settings);
				lastValid = this.m_LastValid;
			}
			return lastValid;
		}

		private TextGenerationError PopulateAlways(string str, TextGenerationSettings settings)
		{
			this.m_LastString = str;
			this.m_HasGenerated = true;
			this.m_CachedVerts = false;
			this.m_CachedCharacters = false;
			this.m_CachedLines = false;
			this.m_LastSettings = settings;
			TextGenerationSettings textGenerationSettings = this.ValidatedSettings(settings);
			TextGenerationError textGenerationError;
			this.Populate_Internal(str, textGenerationSettings.font, textGenerationSettings.color, textGenerationSettings.fontSize, textGenerationSettings.scaleFactor, textGenerationSettings.lineSpacing, textGenerationSettings.fontStyle, textGenerationSettings.richText, textGenerationSettings.resizeTextForBestFit, textGenerationSettings.resizeTextMinSize, textGenerationSettings.resizeTextMaxSize, textGenerationSettings.verticalOverflow, textGenerationSettings.horizontalOverflow, textGenerationSettings.updateBounds, textGenerationSettings.textAnchor, textGenerationSettings.generationExtents, textGenerationSettings.pivot, textGenerationSettings.generateOutOfBounds, textGenerationSettings.alignByGeometry, out textGenerationError);
			this.m_LastValid = textGenerationError;
			return textGenerationError;
		}

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		internal bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds, bool alignByGeometry, out uint error)
		{
			return this.Populate_Internal_Injected(str, font, ref color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, verticalOverFlow, horizontalOverflow, updateBounds, anchor, extentsX, extentsY, pivotX, pivotY, generateOutOfBounds, alignByGeometry, out error);
		}

		internal bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, VerticalWrapMode verticalOverFlow, HorizontalWrapMode horizontalOverflow, bool updateBounds, TextAnchor anchor, Vector2 extents, Vector2 pivot, bool generateOutOfBounds, bool alignByGeometry, out TextGenerationError error)
		{
			bool flag = font == null;
			bool result;
			if (flag)
			{
				error = TextGenerationError.NoFont;
				result = false;
			}
			else
			{
				uint num = 0u;
				bool flag2 = this.Populate_Internal(str, font, color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, (int)verticalOverFlow, (int)horizontalOverflow, updateBounds, anchor, extents.x, extents.y, pivot.x, pivot.y, generateOutOfBounds, alignByGeometry, out num);
				error = (TextGenerationError)num;
				result = flag2;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UIVertex[] GetVerticesArray();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UICharInfo[] GetCharactersArray();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UILineInfo[] GetLinesArray();

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVerticesInternal(object vertices);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCharactersInternal(object characters);

		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLinesInternal(object lines);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rectExtents_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Populate_Internal_Injected(string str, Font font, ref Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds, bool alignByGeometry, out uint error);
	}
}
