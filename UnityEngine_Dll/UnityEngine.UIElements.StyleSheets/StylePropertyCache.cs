using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	internal static class StylePropertyCache
	{
		internal static readonly Dictionary<string, string> s_PropertySyntaxCache = new Dictionary<string, string>
		{
			{
				"align-content",
				"flex-start | flex-end | center | stretch | auto"
			},
			{
				"align-items",
				"flex-start | flex-end | center | stretch | auto"
			},
			{
				"align-self",
				"flex-start | flex-end | center | stretch | auto"
			},
			{
				"background-color",
				"<color>"
			},
			{
				"background-image",
				"<resource> | <url> | none"
			},
			{
				"border-bottom-color",
				"<color>"
			},
			{
				"border-bottom-left-radius",
				"<length> | <percentage>"
			},
			{
				"border-bottom-right-radius",
				"<length> | <percentage>"
			},
			{
				"border-bottom-width",
				"<length>"
			},
			{
				"border-color",
				"<color>{1,4}"
			},
			{
				"border-left-color",
				"<color>"
			},
			{
				"border-left-width",
				"<length>"
			},
			{
				"border-radius",
				"[ <length> | <percentage> ]{1,4}"
			},
			{
				"border-right-color",
				"<color>"
			},
			{
				"border-right-width",
				"<length>"
			},
			{
				"border-top-color",
				"<color>"
			},
			{
				"border-top-left-radius",
				"<length> | <percentage>"
			},
			{
				"border-top-right-radius",
				"<length> | <percentage>"
			},
			{
				"border-top-width",
				"<length>"
			},
			{
				"border-width",
				"<length>{1,4}"
			},
			{
				"bottom",
				"<length> | <percentage> | auto"
			},
			{
				"color",
				"<color>"
			},
			{
				"cursor",
				"[ [ <resource> | <url> ] [ <integer> <integer> ]? ] | [ arrow | text | resize-vertical | resize-horizontal | link | slide-arrow | resize-up-right | resize-up-left | move-arrow | rotate-arrow | scale-arrow | arrow-plus | arrow-minus | pan | orbit | zoom | fps | split-resize-up-down | split-resize-left-right ]"
			},
			{
				"display",
				"flex | none"
			},
			{
				"flex",
				"none | [ <'flex-grow'> <'flex-shrink'>? || <'flex-basis'> ]"
			},
			{
				"flex-basis",
				"<'width'>"
			},
			{
				"flex-direction",
				"column | row | column-reverse | row-reverse"
			},
			{
				"flex-grow",
				"<number>"
			},
			{
				"flex-shrink",
				"<number>"
			},
			{
				"flex-wrap",
				"nowrap | wrap | wrap-reverse"
			},
			{
				"font-size",
				"<length> | <percentage>"
			},
			{
				"height",
				"<length> | <percentage> | auto"
			},
			{
				"justify-content",
				"flex-start | flex-end | center | space-between | space-around"
			},
			{
				"left",
				"<length> | <percentage> | auto"
			},
			{
				"margin",
				"[ <length> | <percentage> | auto ]{1,4}"
			},
			{
				"margin-bottom",
				"<length> | <percentage> | auto"
			},
			{
				"margin-left",
				"<length> | <percentage> | auto"
			},
			{
				"margin-right",
				"<length> | <percentage> | auto"
			},
			{
				"margin-top",
				"<length> | <percentage> | auto"
			},
			{
				"max-height",
				"<length> | <percentage> | none"
			},
			{
				"max-width",
				"<length> | <percentage> | none"
			},
			{
				"min-height",
				"<length> | <percentage> | auto"
			},
			{
				"min-width",
				"<length> | <percentage> | auto"
			},
			{
				"opacity",
				"<number>"
			},
			{
				"overflow",
				"visible | hidden | scroll"
			},
			{
				"padding",
				"[ <length> | <percentage> ]{1,4}"
			},
			{
				"padding-bottom",
				"<length> | <percentage>"
			},
			{
				"padding-left",
				"<length> | <percentage>"
			},
			{
				"padding-right",
				"<length> | <percentage>"
			},
			{
				"padding-top",
				"<length> | <percentage>"
			},
			{
				"position",
				"relative | absolute"
			},
			{
				"right",
				"<length> | <percentage> | auto"
			},
			{
				"text-overflow",
				"clip | ellipsis"
			},
			{
				"top",
				"<length> | <percentage> | auto"
			},
			{
				"-unity-background-image-tint-color",
				"<color>"
			},
			{
				"-unity-background-scale-mode",
				"stretch-to-fill | scale-and-crop | scale-to-fit"
			},
			{
				"-unity-font",
				"<resource> | <url>"
			},
			{
				"-unity-font-style",
				"normal | italic | bold | bold-and-italic"
			},
			{
				"-unity-overflow-clip-box",
				"padding-box | content-box"
			},
			{
				"-unity-slice-bottom",
				"<integer>"
			},
			{
				"-unity-slice-left",
				"<integer>"
			},
			{
				"-unity-slice-right",
				"<integer>"
			},
			{
				"-unity-slice-top",
				"<integer>"
			},
			{
				"-unity-text-align",
				"upper-left | middle-left | lower-left | upper-center | middle-center | lower-center | upper-right | middle-right | lower-right"
			},
			{
				"-unity-text-overflow-position",
				"start | middle | end"
			},
			{
				"visibility",
				"visible | hidden"
			},
			{
				"white-space",
				"normal | nowrap"
			},
			{
				"width",
				"<length> | <percentage> | auto"
			}
		};

		public static bool TryGetSyntax(string name, out string syntax)
		{
			return StylePropertyCache.s_PropertySyntaxCache.TryGetValue(name, out syntax);
		}

		public static string FindClosestPropertyName(string name)
		{
			float num = 3.40282347E+38f;
			string result = null;
			foreach (string current in StylePropertyCache.s_PropertySyntaxCache.Keys)
			{
				float num2 = 1f;
				bool flag = current.Contains(name);
				if (flag)
				{
					num2 = 0.1f;
				}
				float num3 = (float)StringUtils.LevenshteinDistance(name, current) * num2;
				bool flag2 = num3 < num;
				if (flag2)
				{
					num = num3;
					result = current;
				}
			}
			return result;
		}
	}
}
