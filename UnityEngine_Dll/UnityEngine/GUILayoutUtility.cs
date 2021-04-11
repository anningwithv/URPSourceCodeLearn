using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Modules/IMGUI/GUILayoutUtility.bindings.h")]
	public class GUILayoutUtility
	{
		[DebuggerDisplay("id={id}, groups={layoutGroups.Count}")]
		internal sealed class LayoutCache
		{
			internal GUILayoutGroup topLevel = new GUILayoutGroup();

			internal GenericStack layoutGroups = new GenericStack();

			internal GUILayoutGroup windows = new GUILayoutGroup();

			internal int id
			{
				get;
				private set;
			}

			internal LayoutCache(int instanceID = -1)
			{
				this.id = instanceID;
				this.layoutGroups.Push(this.topLevel);
			}

			internal LayoutCache(GUILayoutUtility.LayoutCache other)
			{
				this.id = other.id;
				this.topLevel = other.topLevel;
				this.layoutGroups = other.layoutGroups;
				this.windows = other.windows;
			}

			public void ResetCursor()
			{
				this.windows.ResetCursor();
				this.topLevel.ResetCursor();
				foreach (object current in this.layoutGroups)
				{
					((GUILayoutGroup)current).ResetCursor();
				}
			}
		}

		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredLayouts = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredWindows = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		internal static GUILayoutUtility.LayoutCache current = new GUILayoutUtility.LayoutCache(-1);

		internal static readonly Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		private static GUIStyle s_SpaceStyle;

		internal static GUILayoutGroup topLevel
		{
			get
			{
				return GUILayoutUtility.current.topLevel;
			}
		}

		internal static GUIStyle spaceStyle
		{
			get
			{
				bool flag = GUILayoutUtility.s_SpaceStyle == null;
				if (flag)
				{
					GUILayoutUtility.s_SpaceStyle = new GUIStyle();
				}
				GUILayoutUtility.s_SpaceStyle.stretchWidth = false;
				return GUILayoutUtility.s_SpaceStyle;
			}
		}

		private static Rect Internal_GetWindowRect(int windowID)
		{
			Rect result;
			GUILayoutUtility.Internal_GetWindowRect_Injected(windowID, out result);
			return result;
		}

		private static void Internal_MoveWindow(int windowID, Rect r)
		{
			GUILayoutUtility.Internal_MoveWindow_Injected(windowID, ref r);
		}

		internal static Rect GetWindowsBounds()
		{
			Rect result;
			GUILayoutUtility.GetWindowsBounds_Injected(out result);
			return result;
		}

		internal static void CleanupRoots()
		{
			GUILayoutUtility.s_SpaceStyle = null;
			GUILayoutUtility.s_StoredLayouts.Clear();
			GUILayoutUtility.s_StoredWindows.Clear();
			GUILayoutUtility.current = new GUILayoutUtility.LayoutCache(-1);
		}

		internal static GUILayoutUtility.LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts;
			GUILayoutUtility.LayoutCache layoutCache;
			bool flag = !dictionary.TryGetValue(instanceID, out layoutCache);
			if (flag)
			{
				layoutCache = new GUILayoutUtility.LayoutCache(instanceID);
				dictionary[instanceID] = layoutCache;
			}
			GUILayoutUtility.current.topLevel = layoutCache.topLevel;
			GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
			GUILayoutUtility.current.windows = layoutCache.windows;
			return layoutCache;
		}

		internal static void Begin(int instanceID)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(instanceID, false);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		internal static void BeginContainer(GUILayoutUtility.LayoutCache cache)
		{
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				cache.topLevel = new GUILayoutGroup();
				cache.layoutGroups.Clear();
				cache.layoutGroups.Push(cache.topLevel);
				cache.windows = new GUILayoutGroup();
			}
			GUILayoutUtility.current.topLevel = cache.topLevel;
			GUILayoutUtility.current.layoutGroups = cache.layoutGroups;
			GUILayoutUtility.current.windows = cache.windows;
		}

		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(windowID, true);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.topLevel.style = style;
				GUILayoutUtility.current.topLevel.windowID = windowID;
				bool flag2 = options != null;
				if (flag2)
				{
					GUILayoutUtility.current.topLevel.ApplyOptions(options);
				}
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		[Obsolete("BeginGroup has no effect and will be removed", false)]
		public static void BeginGroup(string GroupName)
		{
		}

		[Obsolete("EndGroup has no effect and will be removed", false)]
		public static void EndGroup(string groupName)
		{
		}

		internal static void Layout()
		{
			bool flag = GUILayoutUtility.current.topLevel.windowID == -1;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxWidth));
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
		}

		internal static void LayoutFromEditorWindow()
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, (float)Screen.width / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, (float)Screen.height / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		internal static void LayoutFromContainer(float w, float h)
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, w);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, h);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		internal static float LayoutFromInspector(float width)
		{
			bool flag = GUILayoutUtility.current.topLevel != null && GUILayoutUtility.current.topLevel.windowID == -1;
			float result;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, width);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				float minHeight = GUILayoutUtility.current.topLevel.minHeight;
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
				result = minHeight;
			}
			else
			{
				bool flag2 = GUILayoutUtility.current.topLevel != null;
				if (flag2)
				{
					GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				}
				result = 0f;
			}
			return result;
		}

		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			using (List<GUILayoutEntry>.Enumerator enumerator = toplevel.entries.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GUILayoutGroup i = (GUILayoutGroup)enumerator.Current;
					GUILayoutUtility.LayoutSingleGroup(i);
				}
			}
			toplevel.ResetCursor();
		}

		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			bool flag = !i.isWindow;
			if (flag)
			{
				float minWidth = i.minWidth;
				float maxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
				float minHeight = i.minHeight;
				float maxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
			}
			else
			{
				i.CalcWidth();
				Rect rect = GUILayoutUtility.Internal_GetWindowRect(i.windowID);
				i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
				i.CalcHeight();
				i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
				GUILayoutUtility.Internal_MoveWindow(i.windowID, i.rect);
			}
		}

		[SecuritySafeCritical]
		private static GUILayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
		{
			bool flag = !typeof(GUILayoutGroup).IsAssignableFrom(LayoutType);
			if (flag)
			{
				throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup", "LayoutType");
			}
			return (GUILayoutGroup)Activator.CreateInstance(LayoutType);
		}

		internal static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type layoutType)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			GUILayoutGroup gUILayoutGroup;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				gUILayoutGroup = (GUILayoutUtility.current.topLevel.GetNext() as GUILayoutGroup);
				bool flag = gUILayoutGroup == null;
				if (flag)
				{
					throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type.ToString());
				}
				gUILayoutGroup.ResetCursor();
				GUIDebugger.LogLayoutGroupEntry(gUILayoutGroup.rect, gUILayoutGroup.marginLeft, gUILayoutGroup.marginRight, gUILayoutGroup.marginTop, gUILayoutGroup.marginBottom, gUILayoutGroup.style, gUILayoutGroup.isVertical);
			}
			else
			{
				gUILayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				gUILayoutGroup.style = style;
				bool flag2 = options != null;
				if (flag2)
				{
					gUILayoutGroup.ApplyOptions(options);
				}
				GUILayoutUtility.current.topLevel.Add(gUILayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(gUILayoutGroup);
			GUILayoutUtility.current.topLevel = gUILayoutGroup;
			return gUILayoutGroup;
		}

		internal static void EndLayoutGroup()
		{
			bool flag = GUILayoutUtility.current.layoutGroups.Count == 0 || Event.current == null;
			if (flag)
			{
				Debug.LogError("EndLayoutGroup: BeginLayoutGroup must be called first.");
			}
			else
			{
				bool flag2 = Event.current.type != EventType.Layout && Event.current.type != EventType.Used;
				if (flag2)
				{
					GUIDebugger.LogLayoutEndGroup();
				}
				GUILayoutUtility.current.layoutGroups.Pop();
				bool flag3 = 0 < GUILayoutUtility.current.layoutGroups.Count;
				if (flag3)
				{
					GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
				}
				else
				{
					GUILayoutUtility.current.topLevel = new GUILayoutGroup();
				}
			}
		}

		internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type layoutType)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			GUILayoutGroup gUILayoutGroup;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				gUILayoutGroup = (GUILayoutUtility.current.windows.GetNext() as GUILayoutGroup);
				bool flag = gUILayoutGroup == null;
				if (flag)
				{
					throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type.ToString());
				}
				gUILayoutGroup.ResetCursor();
				GUIDebugger.LogLayoutGroupEntry(gUILayoutGroup.rect, gUILayoutGroup.marginLeft, gUILayoutGroup.marginRight, gUILayoutGroup.marginTop, gUILayoutGroup.marginBottom, gUILayoutGroup.style, gUILayoutGroup.isVertical);
			}
			else
			{
				gUILayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				gUILayoutGroup.style = style;
				GUILayoutUtility.current.windows.Add(gUILayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(gUILayoutGroup);
			GUILayoutUtility.current.topLevel = gUILayoutGroup;
			return gUILayoutGroup;
		}

		internal static GUILayoutGroup DoBeginLayoutArea(GUIStyle style, Type layoutType)
		{
			return GUILayoutUtility.BeginLayoutArea(style, layoutType);
		}

		public static Rect GetRect(GUIContent content, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(content, style, null);
		}

		public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(content, style, options);
		}

		private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					GUILayoutEntry next = GUILayoutUtility.current.topLevel.GetNext();
					GUIDebugger.LogLayoutEntry(next.rect, next.marginLeft, next.marginRight, next.marginTop, next.marginBottom, next.style);
					rect = next.rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				bool isHeightDependantOnWidth = style.isHeightDependantOnWidth;
				if (isHeightDependantOnWidth)
				{
					GUILayoutUtility.current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
				}
				else
				{
					Vector2 constraints = new Vector2(0f, 0f);
					bool flag = options != null;
					if (flag)
					{
						for (int i = 0; i < options.Length; i++)
						{
							GUILayoutOption gUILayoutOption = options[i];
							GUILayoutOption.Type type2 = gUILayoutOption.type;
							GUILayoutOption.Type type3 = type2;
							if (type3 != GUILayoutOption.Type.maxWidth)
							{
								if (type3 == GUILayoutOption.Type.maxHeight)
								{
									constraints.y = (float)gUILayoutOption.value;
								}
							}
							else
							{
								constraints.x = (float)gUILayoutOption.value;
							}
						}
					}
					Vector2 vector = style.CalcSizeWithConstraints(content, constraints);
					vector.x = Mathf.Ceil(vector.x);
					vector.y = Mathf.Ceil(vector.y);
					GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(vector.x, vector.x, vector.y, vector.y, style, options));
				}
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		public static Rect GetRect(float width, float height)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, null);
		}

		public static Rect GetRect(float width, float height, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, null);
		}

		public static Rect GetRect(float width, float height, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, options);
		}

		public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, options);
		}

		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, null);
		}

		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
		}

		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, options);
		}

		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
		}

		private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					rect = GUILayoutUtility.current.topLevel.GetNext().rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		public static Rect GetLastRect()
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect last;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				last = GUILayoutUtility.current.topLevel.GetLast();
			}
			else
			{
				last = GUILayoutUtility.kDummyRect;
			}
			return last;
		}

		public static Rect GetAspectRect(float aspect)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, null);
		}

		public static Rect GetAspectRect(float aspect, GUIStyle style)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, null);
		}

		public static Rect GetAspectRect(float aspect, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, options);
		}

		public static Rect GetAspectRect(float aspect, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, options);
		}

		private static Rect DoGetAspectRect(float aspect, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					rect = GUILayoutUtility.current.topLevel.GetNext().rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUIAspectSizer(aspect, options));
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetWindowRect_Injected(int windowID, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_MoveWindow_Injected(int windowID, ref Rect r);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWindowsBounds_Injected(out Rect ret);
	}
}
