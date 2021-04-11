using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.UIR;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	public class VisualElement : Focusable, ITransform, ITransitionAnimations, IExperimentalFeatures, IVisualElementScheduler, IResolvedStyle
	{
		public class UxmlFactory : UxmlFactory<VisualElement, VisualElement.UxmlTraits>
		{
		}

		public class UxmlTraits : UnityEngine.UIElements.UxmlTraits
		{
			protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
			{
				name = "name"
			};

			private UxmlStringAttributeDescription m_ViewDataKey = new UxmlStringAttributeDescription
			{
				name = "view-data-key"
			};

			protected UxmlEnumAttributeDescription<PickingMode> m_PickingMode = new UxmlEnumAttributeDescription<PickingMode>
			{
				name = "picking-mode",
				obsoleteNames = new string[]
				{
					"pickingMode"
				}
			};

			private UxmlStringAttributeDescription m_Tooltip = new UxmlStringAttributeDescription
			{
				name = "tooltip"
			};

			private UxmlEnumAttributeDescription<UsageHints> m_UsageHints = new UxmlEnumAttributeDescription<UsageHints>
			{
				name = "usage-hints"
			};

			private UxmlIntAttributeDescription m_TabIndex;

			private UxmlStringAttributeDescription m_Class;

			private UxmlStringAttributeDescription m_ContentContainer;

			private UxmlStringAttributeDescription m_Style;

			protected UxmlIntAttributeDescription focusIndex
			{
				get;
				set;
			}

			protected UxmlBoolAttributeDescription focusable
			{
				get;
				set;
			}

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				bool flag = ve == null;
				if (flag)
				{
					throw new ArgumentNullException("ve");
				}
				ve.name = this.m_Name.GetValueFromBag(bag, cc);
				ve.viewDataKey = this.m_ViewDataKey.GetValueFromBag(bag, cc);
				ve.pickingMode = this.m_PickingMode.GetValueFromBag(bag, cc);
				bool flag2 = ve.panel == null;
				if (flag2)
				{
					ve.usageHints = this.m_UsageHints.GetValueFromBag(bag, cc);
				}
				int num = 0;
				bool flag3 = this.focusIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag3)
				{
					ve.tabIndex = ((num >= 0) ? num : 0);
					ve.focusable = (num >= 0);
				}
				bool flag4 = this.m_TabIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag4)
				{
					ve.tabIndex = num;
				}
				bool focusable = false;
				bool flag5 = this.focusable.TryGetValueFromBag(bag, cc, ref focusable);
				if (flag5)
				{
					ve.focusable = focusable;
				}
				ve.tooltip = this.m_Tooltip.GetValueFromBag(bag, cc);
			}

			public UxmlTraits()
			{
				this.<focusIndex>k__BackingField = new UxmlIntAttributeDescription
				{
					name = null,
					obsoleteNames = new string[]
					{
						"focus-index",
						"focusIndex"
					},
					defaultValue = -1
				};
				this.m_TabIndex = new UxmlIntAttributeDescription
				{
					name = "tabindex",
					defaultValue = 0
				};
				this.<focusable>k__BackingField = new UxmlBoolAttributeDescription
				{
					name = "focusable",
					defaultValue = false
				};
				this.m_Class = new UxmlStringAttributeDescription
				{
					name = "class"
				};
				this.m_ContentContainer = new UxmlStringAttributeDescription
				{
					name = "content-container",
					obsoleteNames = new string[]
					{
						"contentContainer"
					}
				};
				this.m_Style = new UxmlStringAttributeDescription
				{
					name = "style"
				};
				base..ctor();
			}
		}

		public enum MeasureMode
		{
			Undefined,
			Exactly,
			AtMost
		}

		public struct Hierarchy
		{
			private readonly VisualElement m_Owner;

			public VisualElement parent
			{
				get
				{
					return this.m_Owner.m_PhysicalParent;
				}
			}

			public int childCount
			{
				get
				{
					return this.m_Owner.m_Children.Count;
				}
			}

			public VisualElement this[int key]
			{
				get
				{
					return this.m_Owner.m_Children[key];
				}
			}

			internal Hierarchy(VisualElement element)
			{
				this.m_Owner = element;
			}

			public void Add(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot add null child");
				}
				this.Insert(this.childCount, child);
			}

			public void Insert(int index, VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot insert null child");
				}
				bool flag2 = index > this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				bool flag3 = child == this.m_Owner;
				if (flag3)
				{
					throw new ArgumentException("Cannot insert element as its own child");
				}
				child.RemoveFromHierarchy();
				bool flag4 = this.m_Owner.m_Children == VisualElement.s_EmptyList;
				if (flag4)
				{
					this.m_Owner.m_Children = VisualElementListPool.Get(0);
				}
				bool isMeasureDefined = this.m_Owner.yogaNode.IsMeasureDefined;
				if (isMeasureDefined)
				{
					this.m_Owner.RemoveMeasureFunction();
				}
				this.PutChildAtIndex(child, index);
				int num = child.imguiContainerDescendantCount + (child.isIMGUIContainer ? 1 : 0);
				bool flag5 = num > 0;
				if (flag5)
				{
					this.m_Owner.ChangeIMGUIContainerCount(num);
				}
				child.hierarchy.SetParent(this.m_Owner);
				child.PropagateEnabledToChildren(this.m_Owner.enabledInHierarchy);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				child.IncrementVersion(VersionChangeType.Hierarchy);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			public void Remove(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot remove null child");
				}
				bool flag2 = child.hierarchy.parent != this.m_Owner;
				if (flag2)
				{
					throw new ArgumentException("This visualElement is not my child");
				}
				int index = this.m_Owner.m_Children.IndexOf(child);
				this.RemoveAt(index);
			}

			public void RemoveAt(int index)
			{
				bool flag = index < 0 || index >= this.childCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				VisualElement visualElement = this.m_Owner.m_Children[index];
				visualElement.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(index);
				int num = visualElement.imguiContainerDescendantCount + (visualElement.isIMGUIContainer ? 1 : 0);
				bool flag2 = num > 0;
				if (flag2)
				{
					this.m_Owner.ChangeIMGUIContainerCount(-num);
				}
				visualElement.hierarchy.SetParent(null);
				bool flag3 = this.childCount == 0;
				if (flag3)
				{
					this.ReleaseChildList();
					this.m_Owner.AssignMeasureFunction();
				}
				BaseVisualElementPanel expr_BD = this.m_Owner.elementPanel;
				if (expr_BD != null)
				{
					expr_BD.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
				}
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			public void Clear()
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					List<VisualElement> list = VisualElementListPool.Copy(this.m_Owner.m_Children);
					this.ReleaseChildList();
					this.m_Owner.yogaNode.Clear();
					this.m_Owner.AssignMeasureFunction();
					foreach (VisualElement current in list)
					{
						current.InvokeHierarchyChanged(HierarchyChangeType.Remove);
						current.hierarchy.SetParent(null);
						current.m_LogicalParent = null;
						BaseVisualElementPanel expr_85 = this.m_Owner.elementPanel;
						if (expr_85 != null)
						{
							expr_85.OnVersionChanged(current, VersionChangeType.Hierarchy);
						}
					}
					bool flag2 = this.m_Owner.imguiContainerDescendantCount > 0;
					if (flag2)
					{
						int num = this.m_Owner.imguiContainerDescendantCount;
						bool isIMGUIContainer = this.m_Owner.isIMGUIContainer;
						if (isIMGUIContainer)
						{
							num--;
						}
						this.m_Owner.ChangeIMGUIContainerCount(-num);
					}
					VisualElementListPool.Release(list);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			internal void BringToFront(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num >= 0 && num < this.childCount - 1;
					if (flag2)
					{
						this.MoveChildElement(child, num, this.childCount);
					}
				}
			}

			internal void SendToBack(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num > 0;
					if (flag2)
					{
						this.MoveChildElement(child, num, 0);
					}
				}
			}

			internal void PlaceBehind(VisualElement child, VisualElement over)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(over);
						bool flag3 = num2 > 0 && num < num2;
						if (flag3)
						{
							num2--;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			internal void PlaceInFront(VisualElement child, VisualElement under)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(under);
						bool flag3 = num > num2;
						if (flag3)
						{
							num2++;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			private void MoveChildElement(VisualElement child, int currentIndex, int nextIndex)
			{
				child.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(currentIndex);
				this.PutChildAtIndex(child, nextIndex);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			public int IndexOf(VisualElement element)
			{
				return this.m_Owner.m_Children.IndexOf(element);
			}

			public VisualElement ElementAt(int index)
			{
				return this[index];
			}

			public IEnumerable<VisualElement> Children()
			{
				return this.m_Owner.m_Children;
			}

			private void SetParent(VisualElement value)
			{
				this.m_Owner.m_PhysicalParent = value;
				this.m_Owner.m_LogicalParent = value;
				bool flag = value != null;
				if (flag)
				{
					this.m_Owner.SetPanel(this.m_Owner.m_PhysicalParent.elementPanel);
				}
				else
				{
					this.m_Owner.SetPanel(null);
				}
			}

			public void Sort(Comparison<VisualElement> comp)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					this.m_Owner.m_Children.Sort(comp);
					this.m_Owner.yogaNode.Clear();
					for (int i = 0; i < this.m_Owner.m_Children.Count; i++)
					{
						this.m_Owner.yogaNode.Insert(i, this.m_Owner.m_Children[i].yogaNode);
					}
					this.m_Owner.InvokeHierarchyChanged(HierarchyChangeType.Move);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			private void PutChildAtIndex(VisualElement child, int index)
			{
				bool flag = index >= this.childCount;
				if (flag)
				{
					this.m_Owner.m_Children.Add(child);
					this.m_Owner.yogaNode.Insert(this.m_Owner.yogaNode.Count, child.yogaNode);
				}
				else
				{
					this.m_Owner.m_Children.Insert(index, child);
					this.m_Owner.yogaNode.Insert(index, child.yogaNode);
				}
			}

			private void RemoveChildAtIndex(int index)
			{
				this.m_Owner.m_Children.RemoveAt(index);
				this.m_Owner.yogaNode.RemoveAt(index);
			}

			private void ReleaseChildList()
			{
				bool flag = this.m_Owner.m_Children != VisualElement.s_EmptyList;
				if (flag)
				{
					List<VisualElement> children = this.m_Owner.m_Children;
					this.m_Owner.m_Children = VisualElement.s_EmptyList;
					VisualElementListPool.Release(children);
				}
			}

			public bool Equals(VisualElement.Hierarchy other)
			{
				return other == this;
			}

			public override bool Equals(object obj)
			{
				bool flag = obj == null;
				return !flag && obj is VisualElement.Hierarchy && this.Equals((VisualElement.Hierarchy)obj);
			}

			public override int GetHashCode()
			{
				return (this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0;
			}

			public static bool operator ==(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return x.m_Owner == y.m_Owner;
			}

			public static bool operator !=(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return !(x == y);
			}
		}

		private abstract class BaseVisualElementScheduledItem : ScheduledItem, IVisualElementScheduledItem, IVisualElementPanelActivatable
		{
			public bool isScheduled = false;

			private VisualElementPanelActivator m_Activator;

			public VisualElement element
			{
				get;
				private set;
			}

			public bool isActive
			{
				get
				{
					return this.m_Activator.isActive;
				}
			}

			protected BaseVisualElementScheduledItem(VisualElement handler)
			{
				this.element = handler;
				this.m_Activator = new VisualElementPanelActivator(this);
			}

			public IVisualElementScheduledItem StartingIn(long delayMs)
			{
				base.delayMs = delayMs;
				return this;
			}

			public IVisualElementScheduledItem Until(Func<bool> stopCondition)
			{
				bool flag = stopCondition == null;
				if (flag)
				{
					stopCondition = ScheduledItem.ForeverCondition;
				}
				this.timerUpdateStopCondition = stopCondition;
				return this;
			}

			public IVisualElementScheduledItem ForDuration(long durationMs)
			{
				base.SetDuration(durationMs);
				return this;
			}

			public IVisualElementScheduledItem Every(long intervalMs)
			{
				base.intervalMs = intervalMs;
				bool flag = this.timerUpdateStopCondition == ScheduledItem.OnceCondition;
				if (flag)
				{
					this.timerUpdateStopCondition = ScheduledItem.ForeverCondition;
				}
				return this;
			}

			internal override void OnItemUnscheduled()
			{
				base.OnItemUnscheduled();
				this.isScheduled = false;
				bool flag = !this.m_Activator.isDetaching;
				if (flag)
				{
					this.m_Activator.SetActive(false);
				}
			}

			public void Resume()
			{
				this.m_Activator.SetActive(true);
			}

			public void Pause()
			{
				this.m_Activator.SetActive(false);
			}

			public void ExecuteLater(long delayMs)
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.Resume();
				}
				base.ResetStartTime();
				this.StartingIn(delayMs);
			}

			public void OnPanelActivate()
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.isScheduled = true;
					base.ResetStartTime();
					this.element.elementPanel.scheduler.Schedule(this);
				}
			}

			public void OnPanelDeactivate()
			{
				bool flag = this.isScheduled;
				if (flag)
				{
					this.isScheduled = false;
					this.element.elementPanel.scheduler.Unschedule(this);
				}
			}

			public bool CanBeActivated()
			{
				return this.element != null && this.element.elementPanel != null && this.element.elementPanel.scheduler != null;
			}
		}

		private abstract class VisualElementScheduledItem<ActionType> : VisualElement.BaseVisualElementScheduledItem
		{
			public ActionType updateEvent;

			public VisualElementScheduledItem(VisualElement handler, ActionType upEvent) : base(handler)
			{
				this.updateEvent = upEvent;
			}

			public static bool Matches(ScheduledItem item, ActionType updateEvent)
			{
				VisualElement.VisualElementScheduledItem<ActionType> visualElementScheduledItem = item as VisualElement.VisualElementScheduledItem<ActionType>;
				bool flag = visualElementScheduledItem != null;
				return flag && EqualityComparer<ActionType>.Default.Equals(visualElementScheduledItem.updateEvent, updateEvent);
			}
		}

		private class TimerStateScheduledItem : VisualElement.VisualElementScheduledItem<Action<TimerState>>
		{
			public TimerStateScheduledItem(VisualElement handler, Action<TimerState> updateEvent) : base(handler, updateEvent)
			{
			}

			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent(state);
				}
			}
		}

		private class SimpleScheduledItem : VisualElement.VisualElementScheduledItem<Action>
		{
			public SimpleScheduledItem(VisualElement handler, Action updateEvent) : base(handler, updateEvent)
			{
			}

			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent();
				}
			}
		}

		private class TypeData
		{
			private string m_FullTypeName = string.Empty;

			private string m_TypeName = string.Empty;

			public Type type
			{
				[CompilerGenerated]
				get
				{
					return this.<type>k__BackingField;
				}
			}

			public string fullTypeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_FullTypeName);
					if (flag)
					{
						this.m_FullTypeName = this.type.FullName;
					}
					return this.m_FullTypeName;
				}
			}

			public string typeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_TypeName);
					if (flag)
					{
						bool isGenericType = this.type.IsGenericType;
						this.m_TypeName = this.type.Name;
						bool flag2 = isGenericType;
						if (flag2)
						{
							int num = this.m_TypeName.IndexOf('`');
							bool flag3 = num >= 0;
							if (flag3)
							{
								this.m_TypeName = this.m_TypeName.Remove(num);
							}
						}
					}
					return this.m_TypeName;
				}
			}

			public TypeData(Type type)
			{
				this.<type>k__BackingField = type;
			}
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly VisualElement.<>c <>9 = new VisualElement.<>c();

			public static Func<VisualElement, Rect> <>9__252_0;

			public static Action<VisualElement, Rect> <>9__252_1;

			public static Func<VisualElement, Vector2> <>9__253_0;

			public static Action<VisualElement, Vector2> <>9__253_1;

			public static Func<VisualElement, Vector2> <>9__254_0;

			public static Action<VisualElement, Vector2> <>9__254_1;

			public static Func<VisualElement, float> <>9__255_0;

			public static Action<VisualElement, float> <>9__255_1;

			public static Func<VisualElement, Vector3> <>9__256_0;

			public static Action<VisualElement, Vector3> <>9__256_1;

			public static Func<VisualElement, Quaternion> <>9__257_0;

			public static Action<VisualElement, Quaternion> <>9__257_1;

			public static EventCallback<TooltipEvent> <>9__337_0;

			internal Rect Layout>b__252_0(VisualElement e)
			{
				return new Rect(e.resolvedStyle.left, e.resolvedStyle.top, e.resolvedStyle.width, e.resolvedStyle.height);
			}

			internal void Layout>b__252_1(VisualElement e, Rect c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
				e.style.width = c.width;
				e.style.height = c.height;
			}

			internal Vector2 TopLeft>b__253_0(VisualElement e)
			{
				return new Vector2(e.resolvedStyle.left, e.resolvedStyle.top);
			}

			internal void TopLeft>b__253_1(VisualElement e, Vector2 c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
			}

			internal Vector2 Size>b__254_0(VisualElement e)
			{
				return e.layout.size;
			}

			internal void Size>b__254_1(VisualElement e, Vector2 c)
			{
				e.style.width = c.x;
				e.style.height = c.y;
			}

			internal float Scale>b__255_0(VisualElement e)
			{
				return e.transform.scale.x;
			}

			internal void Scale>b__255_1(VisualElement e, float c)
			{
				e.transform.scale = new Vector3(c, c, c);
			}

			internal Vector3 Position>b__256_0(VisualElement e)
			{
				return e.transform.position;
			}

			internal void Position>b__256_1(VisualElement e, Vector3 c)
			{
				e.transform.position = c;
			}

			internal Quaternion Rotation>b__257_0(VisualElement e)
			{
				return e.transform.rotation;
			}

			internal void Rotation>b__257_1(VisualElement e, Quaternion c)
			{
				e.transform.rotation = c;
			}

			internal void <set_tooltip>b__337_0(TooltipEvent evt)
			{
				VisualElement.OnTooltip(evt);
			}
		}

		private static uint s_NextId;

		private static List<string> s_EmptyClassList = new List<string>(0);

		internal static readonly PropertyName userDataPropertyKey = new PropertyName("--unity-user-data");

		public static readonly string disabledUssClassName = "unity-disabled";

		private string m_Name;

		private List<string> m_ClassList;

		private List<KeyValuePair<PropertyName, object>> m_PropertyBag;

		private VisualElementFlags m_Flags;

		private string m_ViewDataKey;

		private RenderHints m_RenderHints;

		internal Rect lastLayout;

		internal Rect lastPadding;

		internal RenderChainVEData renderChainData;

		private Vector3 m_Position = Vector3.zero;

		private Quaternion m_Rotation = Quaternion.identity;

		private Vector3 m_Scale = Vector3.one;

		private Rect m_Layout;

		private Rect m_BoundingBox;

		private Rect m_WorldBoundingBox;

		private Matrix4x4 m_WorldTransformCache = Matrix4x4.identity;

		private Matrix4x4 m_WorldTransformInverseCache = Matrix4x4.identity;

		private Rect m_WorldClip = Rect.zero;

		private Rect m_WorldClipMinusGroup = Rect.zero;

		private static readonly Rect s_InfiniteRect = new Rect(-10000f, -10000f, 40000f, 40000f);

		internal PseudoStates triggerPseudoMask;

		internal PseudoStates dependencyPseudoMask;

		private PseudoStates m_PseudoStates;

		internal ComputedStyle m_SharedStyle = InitialStyle.Get();

		internal ComputedStyle m_Style = InitialStyle.Get();

		internal StyleVariableContext variableContext = StyleVariableContext.none;

		internal int inheritedStylesHash = 0;

		internal readonly uint controlid;

		internal int imguiContainerDescendantCount = 0;

		private List<IValueAnimationUpdate> m_RunningAnimations;

		private VisualElement m_PhysicalParent;

		private VisualElement m_LogicalParent;

		private static readonly List<VisualElement> s_EmptyList = new List<VisualElement>();

		private List<VisualElement> m_Children;

		internal InlineStyleAccess inlineStyleAccess;

		internal List<StyleSheet> styleSheetList;

		private static readonly Regex s_InternalStyleSheetPath = new Regex("^instanceId:[-0-9]+$", RegexOptions.Compiled);

		internal static readonly PropertyName tooltipPropertyKey = new PropertyName("--unity-tooltip");

		private static readonly Dictionary<Type, VisualElement.TypeData> s_TypeData = new Dictionary<Type, VisualElement.TypeData>();

		private VisualElement.TypeData m_TypeData;

		internal bool isCompositeRoot
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.CompositeRoot) == VisualElementFlags.CompositeRoot;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.CompositeRoot) : (this.m_Flags & ~VisualElementFlags.CompositeRoot));
			}
		}

		public string viewDataKey
		{
			get
			{
				return this.m_ViewDataKey;
			}
			set
			{
				bool flag = this.m_ViewDataKey != value;
				if (flag)
				{
					this.m_ViewDataKey = value;
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.IncrementVersion(VersionChangeType.ViewData);
					}
				}
			}
		}

		internal bool enableViewDataPersistence
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.EnableViewDataPersistence) == VisualElementFlags.EnableViewDataPersistence;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.EnableViewDataPersistence) : (this.m_Flags & ~VisualElementFlags.EnableViewDataPersistence));
			}
		}

		public object userData
		{
			get
			{
				object result;
				this.TryGetPropertyInternal(VisualElement.userDataPropertyKey, out result);
				return result;
			}
			set
			{
				this.SetPropertyInternal(VisualElement.userDataPropertyKey, value);
			}
		}

		public override bool canGrabFocus
		{
			get
			{
				bool flag = false;
				for (VisualElement parent = this.hierarchy.parent; parent != null; parent = parent.parent)
				{
					bool isCompositeRoot = parent.isCompositeRoot;
					if (isCompositeRoot)
					{
						flag |= !parent.canGrabFocus;
						break;
					}
				}
				return !flag && this.visible && this.resolvedStyle.display != DisplayStyle.None && this.enabledInHierarchy && base.canGrabFocus;
			}
		}

		public override FocusController focusController
		{
			get
			{
				IPanel expr_07 = this.panel;
				return (expr_07 != null) ? expr_07.focusController : null;
			}
		}

		internal IEventInterpreter eventInterpreter
		{
			get
			{
				BaseVisualElementPanel expr_07 = this.elementPanel;
				return ((expr_07 != null) ? expr_07.eventInterpreter : null) ?? EventInterpreter.s_Instance;
			}
		}

		public UsageHints usageHints
		{
			get
			{
				return (((this.m_RenderHints & RenderHints.GroupTransform) != RenderHints.None) ? UsageHints.GroupTransform : UsageHints.None) | (((this.m_RenderHints & RenderHints.BoneTransform) != RenderHints.None) ? UsageHints.DynamicTransform : UsageHints.None);
			}
			set
			{
				bool flag = this.panel != null;
				if (flag)
				{
					throw new InvalidOperationException("usageHints cannot be changed once the VisualElement is part of an active visual tree");
				}
				bool flag2 = (value & UsageHints.GroupTransform) > UsageHints.None;
				if (flag2)
				{
					this.m_RenderHints |= RenderHints.GroupTransform;
				}
				else
				{
					this.m_RenderHints &= ~RenderHints.GroupTransform;
				}
				bool flag3 = (value & UsageHints.DynamicTransform) > UsageHints.None;
				if (flag3)
				{
					this.m_RenderHints |= RenderHints.BoneTransform;
				}
				else
				{
					this.m_RenderHints &= ~RenderHints.BoneTransform;
				}
			}
		}

		internal RenderHints renderHints
		{
			get
			{
				return this.m_RenderHints;
			}
			set
			{
				bool flag = this.panel != null;
				if (flag)
				{
					throw new InvalidOperationException("renderHints cannot be changed once the VisualElement is part of an active visual tree");
				}
				this.m_RenderHints = value;
			}
		}

		public ITransform transform
		{
			get
			{
				return this;
			}
		}

		Vector3 ITransform.position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				bool flag = this.m_Position == value;
				if (!flag)
				{
					this.m_Position = value;
					this.IncrementVersion(VersionChangeType.Transform);
				}
			}
		}

		Quaternion ITransform.rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				bool flag = this.m_Rotation == value;
				if (!flag)
				{
					this.m_Rotation = value;
					this.IncrementVersion(VersionChangeType.Transform);
				}
			}
		}

		Vector3 ITransform.scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				bool flag = this.m_Scale == value;
				if (!flag)
				{
					this.m_Scale = value;
					this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Transform);
				}
			}
		}

		Matrix4x4 ITransform.matrix
		{
			get
			{
				return Matrix4x4.TRS(this.m_Position, this.m_Rotation, this.m_Scale);
			}
		}

		internal bool isLayoutManual
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.LayoutManual) == VisualElementFlags.LayoutManual;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.LayoutManual) : (this.m_Flags & ~VisualElementFlags.LayoutManual));
			}
		}

		internal float scaledPixelsPerPoint
		{
			get
			{
				return (this.panel == null) ? GUIUtility.pixelsPerPoint : (this.panel as BaseVisualElementPanel).scaledPixelsPerPoint;
			}
		}

		public Rect layout
		{
			get
			{
				Rect layout = this.m_Layout;
				bool flag = this.yogaNode != null && !this.isLayoutManual;
				if (flag)
				{
					layout.x = this.yogaNode.LayoutX;
					layout.y = this.yogaNode.LayoutY;
					layout.width = this.yogaNode.LayoutWidth;
					layout.height = this.yogaNode.LayoutHeight;
				}
				return layout;
			}
			internal set
			{
				bool flag = this.yogaNode == null;
				if (flag)
				{
					this.yogaNode = new YogaNode(null);
				}
				bool flag2 = this.isLayoutManual && this.m_Layout == value;
				if (!flag2)
				{
					Rect layout = this.layout;
					VersionChangeType versionChangeType = (VersionChangeType)0;
					bool flag3 = !Mathf.Approximately(layout.x, value.x) || !Mathf.Approximately(layout.y, value.y);
					if (flag3)
					{
						versionChangeType |= VersionChangeType.Transform;
					}
					bool flag4 = !Mathf.Approximately(layout.width, value.width) || !Mathf.Approximately(layout.height, value.height);
					if (flag4)
					{
						versionChangeType |= VersionChangeType.Size;
					}
					this.m_Layout = value;
					this.isLayoutManual = true;
					IStyle style = this.style;
					style.position = Position.Absolute;
					style.marginLeft = 0f;
					style.marginRight = 0f;
					style.marginBottom = 0f;
					style.marginTop = 0f;
					style.left = value.x;
					style.top = value.y;
					style.right = float.NaN;
					style.bottom = float.NaN;
					style.width = value.width;
					style.height = value.height;
					bool flag5 = versionChangeType > (VersionChangeType)0;
					if (flag5)
					{
						this.IncrementVersion(versionChangeType);
					}
				}
			}
		}

		public Rect contentRect
		{
			get
			{
				Spacing a = new Spacing(this.resolvedStyle.paddingLeft, this.resolvedStyle.paddingTop, this.resolvedStyle.paddingRight, this.resolvedStyle.paddingBottom);
				return this.paddingRect - a;
			}
		}

		protected Rect paddingRect
		{
			get
			{
				Spacing a = new Spacing(this.resolvedStyle.borderLeftWidth, this.resolvedStyle.borderTopWidth, this.resolvedStyle.borderRightWidth, this.resolvedStyle.borderBottomWidth);
				return this.rect - a;
			}
		}

		internal bool isBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.BoundingBoxDirty) == VisualElementFlags.BoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.BoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.BoundingBoxDirty));
			}
		}

		internal bool isWorldBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldBoundingBoxDirty) == VisualElementFlags.WorldBoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldBoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.WorldBoundingBoxDirty));
			}
		}

		internal Rect boundingBox
		{
			get
			{
				bool isBoundingBoxDirty = this.isBoundingBoxDirty;
				if (isBoundingBoxDirty)
				{
					this.UpdateBoundingBox();
					this.isBoundingBoxDirty = false;
				}
				return this.m_BoundingBox;
			}
		}

		internal Rect worldBoundingBox
		{
			get
			{
				bool flag = this.isWorldBoundingBoxDirty || this.isBoundingBoxDirty;
				if (flag)
				{
					this.UpdateWorldBoundingBox();
					this.isWorldBoundingBoxDirty = false;
				}
				return this.m_WorldBoundingBox;
			}
		}

		public Rect worldBound
		{
			get
			{
				Matrix4x4 worldTransform = this.worldTransform;
				return VisualElement.TransformAlignedRect(worldTransform, this.rect);
			}
		}

		public Rect localBound
		{
			get
			{
				Matrix4x4 matrix = this.transform.matrix;
				Rect layout = this.layout;
				return VisualElement.TransformAlignedRect(matrix, layout);
			}
		}

		internal Rect rect
		{
			get
			{
				return new Rect(0f, 0f, this.layout.width, this.layout.height);
			}
		}

		internal bool isWorldTransformDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformDirty) == VisualElementFlags.WorldTransformDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformDirty));
			}
		}

		internal bool isWorldTransformInverseDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformInverseDirty) == VisualElementFlags.WorldTransformInverseDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformInverseDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformInverseDirty));
			}
		}

		public Matrix4x4 worldTransform
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return this.m_WorldTransformCache;
			}
		}

		internal Matrix4x4 worldTransformInverse
		{
			get
			{
				bool flag = this.isWorldTransformDirty || this.isWorldTransformInverseDirty;
				if (flag)
				{
					this.m_WorldTransformInverseCache = this.worldTransform.inverse;
					this.isWorldTransformInverseDirty = false;
				}
				return this.m_WorldTransformInverseCache;
			}
		}

		internal bool isWorldClipDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldClipDirty) == VisualElementFlags.WorldClipDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldClipDirty) : (this.m_Flags & ~VisualElementFlags.WorldClipDirty));
			}
		}

		internal Rect worldClip
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClip;
			}
		}

		internal Rect worldClipMinusGroup
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipMinusGroup;
			}
		}

		internal PseudoStates pseudoStates
		{
			get
			{
				return this.m_PseudoStates;
			}
			set
			{
				bool flag = this.m_PseudoStates != value;
				if (flag)
				{
					this.m_PseudoStates = value;
					bool flag2 = (this.triggerPseudoMask & this.m_PseudoStates) != (PseudoStates)0 || (this.dependencyPseudoMask & ~this.m_PseudoStates) > (PseudoStates)0;
					if (flag2)
					{
						this.IncrementVersion(VersionChangeType.StyleSheet);
					}
				}
			}
		}

		public PickingMode pickingMode
		{
			get;
			set;
		}

		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = this.m_Name == value;
				if (!flag)
				{
					this.m_Name = value;
					this.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		internal List<string> classList
		{
			get
			{
				bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
				if (flag)
				{
					this.m_ClassList = StringListPool.Get();
				}
				return this.m_ClassList;
			}
		}

		internal string fullTypeName
		{
			get
			{
				return this.typeData.fullTypeName;
			}
		}

		internal string typeName
		{
			get
			{
				return this.typeData.typeName;
			}
		}

		internal YogaNode yogaNode
		{
			get;
			private set;
		}

		internal ComputedStyle sharedStyle
		{
			get
			{
				return this.m_SharedStyle;
			}
		}

		internal ComputedStyle computedStyle
		{
			get
			{
				return this.m_Style;
			}
		}

		internal bool hasInlineStyle
		{
			get
			{
				return this.m_Style != this.m_SharedStyle;
			}
		}

		internal float opacity
		{
			get
			{
				return this.resolvedStyle.opacity;
			}
			set
			{
				this.style.opacity = value;
			}
		}

		private bool isParentEnabledInHierarchy
		{
			get
			{
				return this.hierarchy.parent == null || this.hierarchy.parent.enabledInHierarchy;
			}
		}

		public bool enabledInHierarchy
		{
			get
			{
				return (this.pseudoStates & PseudoStates.Disabled) != PseudoStates.Disabled;
			}
		}

		public bool enabledSelf
		{
			get;
			private set;
		}

		public bool visible
		{
			get
			{
				return this.resolvedStyle.visibility == Visibility.Visible;
			}
			set
			{
				this.style.visibility = (value ? Visibility.Visible : Visibility.Hidden);
			}
		}

		public Action<MeshGenerationContext> generateVisualContent
		{
			get;
			set;
		}

		internal bool requireMeasureFunction
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.RequireMeasureFunction) == VisualElementFlags.RequireMeasureFunction;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.RequireMeasureFunction) : (this.m_Flags & ~VisualElementFlags.RequireMeasureFunction));
				bool flag = value && !this.yogaNode.IsMeasureDefined;
				if (flag)
				{
					this.AssignMeasureFunction();
				}
				else
				{
					bool flag2 = !value && this.yogaNode.IsMeasureDefined;
					if (flag2)
					{
						this.RemoveMeasureFunction();
					}
				}
			}
		}

		public IExperimentalFeatures experimental
		{
			get
			{
				return this;
			}
		}

		ITransitionAnimations IExperimentalFeatures.animation
		{
			get
			{
				return this;
			}
		}

		public VisualElement.Hierarchy hierarchy
		{
			get;
			private set;
		}

		[Obsolete("VisualElement.cacheAsBitmap is deprecated and has no effect")]
		public bool cacheAsBitmap
		{
			get;
			set;
		}

		public VisualElement parent
		{
			get
			{
				return this.m_LogicalParent;
			}
		}

		internal BaseVisualElementPanel elementPanel
		{
			get;
			private set;
		}

		public IPanel panel
		{
			get
			{
				return this.elementPanel;
			}
		}

		public virtual VisualElement contentContainer
		{
			get
			{
				return this;
			}
		}

		public VisualElement this[int key]
		{
			get
			{
				bool flag = this.contentContainer == this;
				VisualElement result;
				if (flag)
				{
					result = this.hierarchy[key];
				}
				else
				{
					VisualElement expr_27 = this.contentContainer;
					result = ((expr_27 != null) ? expr_27[key] : null);
				}
				return result;
			}
		}

		public int childCount
		{
			get
			{
				bool flag = this.contentContainer == this;
				int result;
				if (flag)
				{
					result = this.hierarchy.childCount;
				}
				else
				{
					VisualElement expr_26 = this.contentContainer;
					result = ((expr_26 != null) ? expr_26.childCount : 0);
				}
				return result;
			}
		}

		public IVisualElementScheduler schedule
		{
			get
			{
				return this;
			}
		}

		public IStyle style
		{
			get
			{
				bool flag = this.inlineStyleAccess == null;
				if (flag)
				{
					this.inlineStyleAccess = new InlineStyleAccess(this);
				}
				return this.inlineStyleAccess;
			}
		}

		public ICustomStyle customStyle
		{
			get
			{
				return this.computedStyle;
			}
		}

		public VisualElementStyleSheetSet styleSheets
		{
			get
			{
				return new VisualElementStyleSheetSet(this);
			}
		}

		public string tooltip
		{
			get
			{
				string text = this.GetProperty(VisualElement.tooltipPropertyKey) as string;
				return text ?? string.Empty;
			}
			set
			{
				bool flag = !this.HasProperty(VisualElement.tooltipPropertyKey);
				if (flag)
				{
					EventCallback<TooltipEvent> arg_35_1;
					if ((arg_35_1 = VisualElement.<>c.<>9__337_0) == null)
					{
						arg_35_1 = (VisualElement.<>c.<>9__337_0 = new EventCallback<TooltipEvent>(VisualElement.<>c.<>9.<set_tooltip>b__337_0));
					}
					base.RegisterCallback<TooltipEvent>(arg_35_1, TrickleDown.NoTrickleDown);
				}
				this.SetProperty(VisualElement.tooltipPropertyKey, value);
			}
		}

		private VisualElement.TypeData typeData
		{
			get
			{
				bool flag = this.m_TypeData == null;
				if (flag)
				{
					Type type = base.GetType();
					bool flag2 = !VisualElement.s_TypeData.TryGetValue(type, out this.m_TypeData);
					if (flag2)
					{
						this.m_TypeData = new VisualElement.TypeData(type);
						VisualElement.s_TypeData.Add(type, this.m_TypeData);
					}
				}
				return this.m_TypeData;
			}
		}

		public IResolvedStyle resolvedStyle
		{
			get
			{
				return this;
			}
		}

		Align IResolvedStyle.alignContent
		{
			get
			{
				return this.computedStyle.alignContent.value;
			}
		}

		Align IResolvedStyle.alignItems
		{
			get
			{
				return this.computedStyle.alignItems.value;
			}
		}

		Align IResolvedStyle.alignSelf
		{
			get
			{
				return this.computedStyle.alignSelf.value;
			}
		}

		Color IResolvedStyle.backgroundColor
		{
			get
			{
				return this.computedStyle.backgroundColor.value;
			}
		}

		Background IResolvedStyle.backgroundImage
		{
			get
			{
				return this.computedStyle.backgroundImage.value;
			}
		}

		Color IResolvedStyle.borderBottomColor
		{
			get
			{
				return this.computedStyle.borderBottomColor.value;
			}
		}

		float IResolvedStyle.borderBottomLeftRadius
		{
			get
			{
				return this.computedStyle.borderBottomLeftRadius.value.value;
			}
		}

		float IResolvedStyle.borderBottomRightRadius
		{
			get
			{
				return this.computedStyle.borderBottomRightRadius.value.value;
			}
		}

		float IResolvedStyle.borderBottomWidth
		{
			get
			{
				return this.computedStyle.borderBottomWidth.value;
			}
		}

		Color IResolvedStyle.borderLeftColor
		{
			get
			{
				return this.computedStyle.borderLeftColor.value;
			}
		}

		float IResolvedStyle.borderLeftWidth
		{
			get
			{
				return this.computedStyle.borderLeftWidth.value;
			}
		}

		Color IResolvedStyle.borderRightColor
		{
			get
			{
				return this.computedStyle.borderRightColor.value;
			}
		}

		float IResolvedStyle.borderRightWidth
		{
			get
			{
				return this.computedStyle.borderRightWidth.value;
			}
		}

		Color IResolvedStyle.borderTopColor
		{
			get
			{
				return this.computedStyle.borderTopColor.value;
			}
		}

		float IResolvedStyle.borderTopLeftRadius
		{
			get
			{
				return this.computedStyle.borderTopLeftRadius.value.value;
			}
		}

		float IResolvedStyle.borderTopRightRadius
		{
			get
			{
				return this.computedStyle.borderTopRightRadius.value.value;
			}
		}

		float IResolvedStyle.borderTopWidth
		{
			get
			{
				return this.computedStyle.borderTopWidth.value;
			}
		}

		float IResolvedStyle.bottom
		{
			get
			{
				return this.yogaNode.LayoutBottom;
			}
		}

		Color IResolvedStyle.color
		{
			get
			{
				return this.computedStyle.color.value;
			}
		}

		DisplayStyle IResolvedStyle.display
		{
			get
			{
				return this.computedStyle.display.value;
			}
		}

		StyleFloat IResolvedStyle.flexBasis
		{
			get
			{
				return new StyleFloat(this.yogaNode.ComputedFlexBasis);
			}
		}

		FlexDirection IResolvedStyle.flexDirection
		{
			get
			{
				return this.computedStyle.flexDirection.value;
			}
		}

		float IResolvedStyle.flexGrow
		{
			get
			{
				return this.computedStyle.flexGrow.value;
			}
		}

		float IResolvedStyle.flexShrink
		{
			get
			{
				return this.computedStyle.flexShrink.value;
			}
		}

		Wrap IResolvedStyle.flexWrap
		{
			get
			{
				return this.computedStyle.flexWrap.value;
			}
		}

		float IResolvedStyle.fontSize
		{
			get
			{
				return this.computedStyle.fontSize.value.value;
			}
		}

		float IResolvedStyle.height
		{
			get
			{
				return this.yogaNode.LayoutHeight;
			}
		}

		Justify IResolvedStyle.justifyContent
		{
			get
			{
				return this.computedStyle.justifyContent.value;
			}
		}

		float IResolvedStyle.left
		{
			get
			{
				return this.yogaNode.LayoutX;
			}
		}

		float IResolvedStyle.marginBottom
		{
			get
			{
				return this.yogaNode.LayoutMarginBottom;
			}
		}

		float IResolvedStyle.marginLeft
		{
			get
			{
				return this.yogaNode.LayoutMarginLeft;
			}
		}

		float IResolvedStyle.marginRight
		{
			get
			{
				return this.yogaNode.LayoutMarginRight;
			}
		}

		float IResolvedStyle.marginTop
		{
			get
			{
				return this.yogaNode.LayoutMarginTop;
			}
		}

		StyleFloat IResolvedStyle.maxHeight
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.maxHeight, false);
			}
		}

		StyleFloat IResolvedStyle.maxWidth
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.maxWidth, true);
			}
		}

		StyleFloat IResolvedStyle.minHeight
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.minHeight, false);
			}
		}

		StyleFloat IResolvedStyle.minWidth
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.minWidth, true);
			}
		}

		float IResolvedStyle.opacity
		{
			get
			{
				return this.computedStyle.opacity.value;
			}
		}

		float IResolvedStyle.paddingBottom
		{
			get
			{
				return this.yogaNode.LayoutPaddingBottom;
			}
		}

		float IResolvedStyle.paddingLeft
		{
			get
			{
				return this.yogaNode.LayoutPaddingLeft;
			}
		}

		float IResolvedStyle.paddingRight
		{
			get
			{
				return this.yogaNode.LayoutPaddingRight;
			}
		}

		float IResolvedStyle.paddingTop
		{
			get
			{
				return this.yogaNode.LayoutPaddingTop;
			}
		}

		Position IResolvedStyle.position
		{
			get
			{
				return this.computedStyle.position.value;
			}
		}

		float IResolvedStyle.right
		{
			get
			{
				return this.yogaNode.LayoutRight;
			}
		}

		TextOverflow IResolvedStyle.textOverflow
		{
			get
			{
				return this.computedStyle.textOverflow.value;
			}
		}

		float IResolvedStyle.top
		{
			get
			{
				return this.yogaNode.LayoutY;
			}
		}

		Color IResolvedStyle.unityBackgroundImageTintColor
		{
			get
			{
				return this.computedStyle.unityBackgroundImageTintColor.value;
			}
		}

		ScaleMode IResolvedStyle.unityBackgroundScaleMode
		{
			get
			{
				return this.computedStyle.unityBackgroundScaleMode.value;
			}
		}

		Font IResolvedStyle.unityFont
		{
			get
			{
				return this.computedStyle.unityFont.value;
			}
		}

		FontStyle IResolvedStyle.unityFontStyleAndWeight
		{
			get
			{
				return this.computedStyle.unityFontStyleAndWeight.value;
			}
		}

		int IResolvedStyle.unitySliceBottom
		{
			get
			{
				return this.computedStyle.unitySliceBottom.value;
			}
		}

		int IResolvedStyle.unitySliceLeft
		{
			get
			{
				return this.computedStyle.unitySliceLeft.value;
			}
		}

		int IResolvedStyle.unitySliceRight
		{
			get
			{
				return this.computedStyle.unitySliceRight.value;
			}
		}

		int IResolvedStyle.unitySliceTop
		{
			get
			{
				return this.computedStyle.unitySliceTop.value;
			}
		}

		TextAnchor IResolvedStyle.unityTextAlign
		{
			get
			{
				return this.computedStyle.unityTextAlign.value;
			}
		}

		TextOverflowPosition IResolvedStyle.unityTextOverflowPosition
		{
			get
			{
				return this.computedStyle.unityTextOverflowPosition.value;
			}
		}

		Visibility IResolvedStyle.visibility
		{
			get
			{
				return this.computedStyle.visibility.value;
			}
		}

		WhiteSpace IResolvedStyle.whiteSpace
		{
			get
			{
				return this.computedStyle.whiteSpace.value;
			}
		}

		float IResolvedStyle.width
		{
			get
			{
				return this.yogaNode.LayoutWidth;
			}
		}

		internal Vector3 ComputeGlobalScale()
		{
			Vector3 scale = this.m_Scale;
			for (VisualElement parent = this.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				scale.Scale(parent.m_Scale);
			}
			return scale;
		}

		internal static Rect TransformAlignedRect(Matrix4x4 lhc, Rect rect)
		{
			Vector2 vector = VisualElement.MultiplyMatrix44Point2(lhc, rect.min);
			Vector2 vector2 = VisualElement.MultiplyMatrix44Point2(lhc, rect.max);
			return Rect.MinMaxRect(Math.Min(vector.x, vector2.x), Math.Min(vector.y, vector2.y), Math.Max(vector.x, vector2.x), Math.Max(vector.y, vector2.y));
		}

		internal static Vector2 MultiplyMatrix44Point2(Matrix4x4 lhs, Vector2 point)
		{
			Vector2 result;
			result.x = lhs.m00 * point.x + lhs.m01 * point.y + lhs.m03;
			result.y = lhs.m10 * point.x + lhs.m11 * point.y + lhs.m13;
			return result;
		}

		internal void UpdateBoundingBox()
		{
			bool flag = float.IsNaN(this.rect.x) || float.IsNaN(this.rect.y) || float.IsNaN(this.rect.width) || float.IsNaN(this.rect.height);
			if (flag)
			{
				this.m_BoundingBox = Rect.zero;
			}
			else
			{
				this.m_BoundingBox = this.rect;
				int count = this.m_Children.Count;
				for (int i = 0; i < count; i++)
				{
					Rect rect = this.m_Children[i].boundingBox;
					rect = this.m_Children[i].ChangeCoordinatesTo(this, rect);
					this.m_BoundingBox.xMin = Math.Min(this.m_BoundingBox.xMin, rect.xMin);
					this.m_BoundingBox.xMax = Math.Max(this.m_BoundingBox.xMax, rect.xMax);
					this.m_BoundingBox.yMin = Math.Min(this.m_BoundingBox.yMin, rect.yMin);
					this.m_BoundingBox.yMax = Math.Max(this.m_BoundingBox.yMax, rect.yMax);
				}
			}
			this.isWorldBoundingBoxDirty = true;
		}

		internal void UpdateWorldBoundingBox()
		{
			this.m_WorldBoundingBox = VisualElement.TransformAlignedRect(this.worldTransform, this.boundingBox);
		}

		private void UpdateWorldTransform()
		{
			bool flag = this.elementPanel != null && !this.elementPanel.duringLayoutPhase;
			if (flag)
			{
				this.isWorldTransformDirty = false;
			}
			Matrix4x4 matrix4x = Matrix4x4.Translate(new Vector3(this.layout.x, this.layout.y, 0f));
			bool flag2 = this.hierarchy.parent != null;
			if (flag2)
			{
				this.m_WorldTransformCache = this.hierarchy.parent.worldTransform * matrix4x * this.transform.matrix;
			}
			else
			{
				this.m_WorldTransformCache = matrix4x * this.transform.matrix;
			}
			this.isWorldTransformInverseDirty = true;
			this.isWorldBoundingBoxDirty = true;
		}

		internal void EnsureWorldTransformAndClipUpToDate()
		{
			bool isWorldTransformDirty = this.isWorldTransformDirty;
			if (isWorldTransformDirty)
			{
				this.UpdateWorldTransform();
			}
			bool isWorldClipDirty = this.isWorldClipDirty;
			if (isWorldClipDirty)
			{
				this.UpdateWorldClip();
				this.isWorldClipDirty = false;
			}
		}

		private void UpdateWorldClip()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.m_WorldClip = this.hierarchy.parent.worldClip;
				bool flag2 = this.hierarchy.parent != this.renderChainData.groupTransformAncestor;
				if (flag2)
				{
					this.m_WorldClipMinusGroup = this.hierarchy.parent.worldClipMinusGroup;
				}
				else
				{
					IPanel expr_77 = this.panel;
					this.m_WorldClipMinusGroup = ((expr_77 != null && expr_77.contextType == ContextType.Player) ? VisualElement.s_InfiniteRect : GUIClip.topmostRect);
				}
				bool flag3 = this.ShouldClip();
				if (flag3)
				{
					Rect rect = this.SubstractBorderPadding(this.worldBound);
					float num = Mathf.Max(rect.xMin, this.m_WorldClip.xMin);
					float num2 = Mathf.Min(rect.xMax, this.m_WorldClip.xMax);
					float num3 = Mathf.Max(rect.yMin, this.m_WorldClip.yMin);
					float num4 = Mathf.Min(rect.yMax, this.m_WorldClip.yMax);
					float width = Mathf.Max(num2 - num, 0f);
					float height = Mathf.Max(num4 - num3, 0f);
					this.m_WorldClip = new Rect(num, num3, width, height);
					num = Mathf.Max(rect.xMin, this.m_WorldClipMinusGroup.xMin);
					num2 = Mathf.Min(rect.xMax, this.m_WorldClipMinusGroup.xMax);
					num3 = Mathf.Max(rect.yMin, this.m_WorldClipMinusGroup.yMin);
					num4 = Mathf.Min(rect.yMax, this.m_WorldClipMinusGroup.yMax);
					width = Mathf.Max(num2 - num, 0f);
					height = Mathf.Max(num4 - num3, 0f);
					this.m_WorldClipMinusGroup = new Rect(num, num3, width, height);
				}
			}
			else
			{
				this.m_WorldClipMinusGroup = (this.m_WorldClip = ((this.panel != null) ? this.panel.visualTree.rect : VisualElement.s_InfiniteRect));
			}
		}

		private Rect SubstractBorderPadding(Rect worldRect)
		{
			float m = this.worldTransform.m00;
			float m2 = this.worldTransform.m11;
			worldRect.x += this.resolvedStyle.borderLeftWidth * m;
			worldRect.y += this.resolvedStyle.borderTopWidth * m2;
			worldRect.width -= (this.resolvedStyle.borderLeftWidth + this.resolvedStyle.borderRightWidth) * m;
			worldRect.height -= (this.resolvedStyle.borderTopWidth + this.resolvedStyle.borderBottomWidth) * m2;
			bool flag = this.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
			if (flag)
			{
				worldRect.x += this.resolvedStyle.paddingLeft * m;
				worldRect.y += this.resolvedStyle.paddingTop * m2;
				worldRect.width -= (this.resolvedStyle.paddingLeft + this.resolvedStyle.paddingRight) * m;
				worldRect.height -= (this.resolvedStyle.paddingTop + this.resolvedStyle.paddingBottom) * m2;
			}
			return worldRect;
		}

		internal static Rect ComputeAAAlignedBound(Rect position, Matrix4x4 mat)
		{
			Rect rect = position;
			Vector3 vector = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y, 0f));
			Vector3 vector2 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y, 0f));
			Vector3 vector3 = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y + rect.height, 0f));
			Vector3 vector4 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y + rect.height, 0f));
			return Rect.MinMaxRect(Mathf.Min(vector.x, Mathf.Min(vector2.x, Mathf.Min(vector3.x, vector4.x))), Mathf.Min(vector.y, Mathf.Min(vector2.y, Mathf.Min(vector3.y, vector4.y))), Mathf.Max(vector.x, Mathf.Max(vector2.x, Mathf.Max(vector3.x, vector4.x))), Mathf.Max(vector.y, Mathf.Max(vector2.y, Mathf.Max(vector3.y, vector4.y))));
		}

		private void ChangeIMGUIContainerCount(int delta)
		{
			for (VisualElement visualElement = this; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				visualElement.imguiContainerDescendantCount += delta;
			}
		}

		public VisualElement()
		{
			this.m_Children = VisualElement.s_EmptyList;
			this.controlid = (VisualElement.s_NextId += 1u);
			this.hierarchy = new VisualElement.Hierarchy(this);
			this.m_ClassList = VisualElement.s_EmptyClassList;
			this.m_Flags = VisualElementFlags.Init;
			this.SetEnabled(true);
			base.focusable = false;
			this.name = string.Empty;
			this.yogaNode = new YogaNode(null);
			this.renderHints = RenderHints.None;
		}

		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<MouseOverEvent>.TypeId() || evt.eventTypeId == EventBase<MouseOutEvent>.TypeId();
				if (flag2)
				{
					this.UpdateCursorStyle(evt.eventTypeId);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<MouseEnterEvent>.TypeId();
					if (flag3)
					{
						IPanel expr_64 = this.panel;
						IEventHandler eventHandler = (expr_64 != null) ? expr_64.GetCapturingElement(PointerId.mousePointerId) : null;
						bool flag4 = eventHandler == null || eventHandler == this;
						if (flag4)
						{
							this.pseudoStates |= PseudoStates.Hover;
						}
					}
					else
					{
						bool flag5 = evt.eventTypeId == EventBase<MouseLeaveEvent>.TypeId();
						if (flag5)
						{
							this.pseudoStates &= ~PseudoStates.Hover;
						}
						else
						{
							bool flag6 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
							if (flag6)
							{
								this.pseudoStates &= ~PseudoStates.Focus;
							}
							else
							{
								bool flag7 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
								if (flag7)
								{
									this.pseudoStates |= PseudoStates.Focus;
								}
							}
						}
					}
				}
			}
		}

		public sealed override void Focus()
		{
			bool flag = !this.canGrabFocus && this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.Focus();
			}
			else
			{
				base.Focus();
			}
		}

		internal void SetPanel(BaseVisualElementPanel p)
		{
			bool flag = this.panel == p;
			if (!flag)
			{
				List<VisualElement> list = VisualElementListPool.Get(0);
				try
				{
					list.Add(this);
					this.GatherAllChildren(list);
					EventDispatcherGate? eventDispatcherGate = null;
					bool flag2 = ((p != null) ? p.dispatcher : null) != null;
					if (flag2)
					{
						eventDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(p.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate2 = null;
					IPanel expr_6A = this.panel;
					bool flag3 = ((expr_6A != null) ? expr_6A.dispatcher : null) != null && this.panel.dispatcher != ((p != null) ? p.dispatcher : null);
					if (flag3)
					{
						eventDispatcherGate2 = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					BaseVisualElementPanel elementPanel = this.elementPanel;
					EventDispatcherGate? eventDispatcherGate3 = eventDispatcherGate;
					try
					{
						EventDispatcherGate? eventDispatcherGate4 = eventDispatcherGate2;
						try
						{
							foreach (VisualElement current in list)
							{
								current.WillChangePanel(p);
							}
							VisualElementFlags visualElementFlags = (p != null) ? VisualElementFlags.NeedsAttachToPanelEvent : ((VisualElementFlags)0);
							foreach (VisualElement current2 in list)
							{
								bool flag4 = elementPanel != current2.elementPanel;
								if (!flag4)
								{
									current2.elementPanel = p;
									current2.m_Flags |= visualElementFlags;
								}
							}
							foreach (VisualElement current3 in list)
							{
								bool flag5 = p != current3.elementPanel;
								if (!flag5)
								{
									current3.HasChangedPanel(elementPanel);
								}
							}
						}
						finally
						{
							if (eventDispatcherGate4.HasValue)
							{
								((IDisposable)eventDispatcherGate4.GetValueOrDefault()).Dispose();
							}
						}
					}
					finally
					{
						if (eventDispatcherGate3.HasValue)
						{
							((IDisposable)eventDispatcherGate3.GetValueOrDefault()).Dispose();
						}
					}
				}
				finally
				{
					VisualElementListPool.Release(list);
				}
			}
		}

		private void WillChangePanel(BaseVisualElementPanel destinationPanel)
		{
			bool flag = this.panel != null;
			if (flag)
			{
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == (VisualElementFlags)0;
				if (flag2)
				{
					using (DetachFromPanelEvent pooled = PanelChangedEventBase<DetachFromPanelEvent>.GetPooled(this.panel, destinationPanel))
					{
						pooled.target = this;
						this.elementPanel.SendEvent(pooled, DispatchMode.Immediate);
					}
				}
				this.UnregisterRunningAnimations();
			}
		}

		private void HasChangedPanel(BaseVisualElementPanel prevPanel)
		{
			bool flag = this.panel != null;
			if (flag)
			{
				this.yogaNode.Config = this.elementPanel.yogaConfig;
				this.RegisterRunningAnimations();
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == VisualElementFlags.NeedsAttachToPanelEvent;
				if (flag2)
				{
					using (AttachToPanelEvent pooled = PanelChangedEventBase<AttachToPanelEvent>.GetPooled(prevPanel, this.panel))
					{
						pooled.target = this;
						this.elementPanel.SendEvent(pooled, DispatchMode.Immediate);
					}
					this.m_Flags &= ~VisualElementFlags.NeedsAttachToPanelEvent;
				}
			}
			else
			{
				this.yogaNode.Config = YogaConfig.Default;
			}
			this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Transform);
			bool flag3 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag3)
			{
				this.IncrementVersion(VersionChangeType.ViewData);
			}
		}

		public sealed override void SendEvent(EventBase e)
		{
			BaseVisualElementPanel expr_07 = this.elementPanel;
			if (expr_07 != null)
			{
				expr_07.SendEvent(e, DispatchMode.Default);
			}
		}

		internal void IncrementVersion(VersionChangeType changeType)
		{
			BaseVisualElementPanel expr_07 = this.elementPanel;
			if (expr_07 != null)
			{
				expr_07.OnVersionChanged(this, changeType);
			}
		}

		internal void InvokeHierarchyChanged(HierarchyChangeType changeType)
		{
			BaseVisualElementPanel expr_07 = this.elementPanel;
			if (expr_07 != null)
			{
				expr_07.InvokeHierarchyChanged(this, changeType);
			}
		}

		[Obsolete("SetEnabledFromHierarchy is deprecated and will be removed in a future release. Please use SetEnabled instead.")]
		protected internal bool SetEnabledFromHierarchy(bool state)
		{
			return this.SetEnabledFromHierarchyPrivate(state);
		}

		private bool SetEnabledFromHierarchyPrivate(bool state)
		{
			bool enabledInHierarchy = this.enabledInHierarchy;
			if (state)
			{
				bool isParentEnabledInHierarchy = this.isParentEnabledInHierarchy;
				if (isParentEnabledInHierarchy)
				{
					bool enabledSelf = this.enabledSelf;
					if (enabledSelf)
					{
						this.pseudoStates &= ~PseudoStates.Disabled;
						this.RemoveFromClassList(VisualElement.disabledUssClassName);
					}
					else
					{
						this.pseudoStates |= PseudoStates.Disabled;
						this.AddToClassList(VisualElement.disabledUssClassName);
					}
				}
				else
				{
					this.pseudoStates |= PseudoStates.Disabled;
					this.RemoveFromClassList(VisualElement.disabledUssClassName);
				}
			}
			else
			{
				this.pseudoStates |= PseudoStates.Disabled;
				this.EnableInClassList(VisualElement.disabledUssClassName, this.isParentEnabledInHierarchy);
			}
			return enabledInHierarchy != this.enabledInHierarchy;
		}

		public void SetEnabled(bool value)
		{
			bool flag = this.enabledSelf == value;
			if (!flag)
			{
				this.enabledSelf = value;
				this.PropagateEnabledToChildren(value);
			}
		}

		private void PropagateEnabledToChildren(bool value)
		{
			bool flag = this.SetEnabledFromHierarchyPrivate(value);
			if (flag)
			{
				int count = this.m_Children.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_Children[i].PropagateEnabledToChildren(value);
				}
			}
		}

		public void MarkDirtyRepaint()
		{
			this.IncrementVersion(VersionChangeType.Repaint);
		}

		internal void InvokeGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.generateVisualContent != null;
			if (flag)
			{
				try
				{
					this.generateVisualContent(mgc);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}

		internal void GetFullHierarchicalViewDataKey(StringBuilder key)
		{
			bool flag = this.parent != null;
			if (flag)
			{
				this.parent.GetFullHierarchicalViewDataKey(key);
			}
			bool flag2 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag2)
			{
				key.Append("__");
				key.Append(this.viewDataKey);
			}
		}

		internal string GetFullHierarchicalViewDataKey()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetFullHierarchicalViewDataKey(stringBuilder);
			return stringBuilder.ToString();
		}

		internal T GetOrCreateViewData<T>(object existing, string key) where T : class, new()
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load persistent data.");
			ISerializableJsonDictionary serializableJsonDictionary = (this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary();
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T result;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					result = (existing as T);
				}
				else
				{
					result = Activator.CreateInstance<T>();
				}
			}
			else
			{
				string arg_9B_1 = "__";
				Type expr_8F = typeof(T);
				string key2 = key + arg_9B_1 + ((expr_8F != null) ? expr_8F.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(key2);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(key2, Activator.CreateInstance<T>());
				}
				result = serializableJsonDictionary.Get<T>(key2);
			}
			return result;
		}

		internal T GetOrCreateViewData<T>(ScriptableObject existing, string key) where T : ScriptableObject
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = (this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary();
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T result;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					result = (existing as T);
				}
				else
				{
					result = ScriptableObject.CreateInstance<T>();
				}
			}
			else
			{
				string arg_9E_1 = "__";
				Type expr_92 = typeof(T);
				string key2 = key + arg_9E_1 + ((expr_92 != null) ? expr_92.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(key2);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(key2, ScriptableObject.CreateInstance<T>());
				}
				result = serializableJsonDictionary.GetScriptable<T>(key2);
			}
			return result;
		}

		internal void OverwriteFromViewData(object obj, string key)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = (this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary();
			bool flag2 = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			if (!flag2)
			{
				string arg_8D_1 = "__";
				Type expr_81 = obj.GetType();
				string key2 = key + arg_8D_1 + ((expr_81 != null) ? expr_81.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(key2);
				if (flag3)
				{
					serializableJsonDictionary.Set<object>(key2, obj);
				}
				else
				{
					serializableJsonDictionary.Overwrite(obj, key2);
				}
			}
		}

		internal void SaveViewData()
		{
			bool flag = this.elementPanel != null && this.elementPanel.saveViewData != null && !string.IsNullOrEmpty(this.viewDataKey) && this.enableViewDataPersistence;
			if (flag)
			{
				this.elementPanel.saveViewData();
			}
		}

		internal bool IsViewDataPersitenceSupportedOnChildren(bool existingState)
		{
			bool result = existingState;
			bool flag = string.IsNullOrEmpty(this.viewDataKey) && this != this.contentContainer;
			if (flag)
			{
				result = false;
			}
			bool flag2 = this.parent != null && this == this.parent.contentContainer;
			if (flag2)
			{
				result = true;
			}
			return result;
		}

		internal void OnViewDataReady(bool enablePersistence)
		{
			this.enableViewDataPersistence = enablePersistence;
			this.OnViewDataReady();
		}

		internal virtual void OnViewDataReady()
		{
		}

		public virtual bool ContainsPoint(Vector2 localPoint)
		{
			return this.rect.Contains(localPoint);
		}

		public virtual bool Overlaps(Rect rectangle)
		{
			return this.rect.Overlaps(rectangle, true);
		}

		private void AssignMeasureFunction()
		{
			this.yogaNode.SetMeasureFunction((YogaNode node, float f, YogaMeasureMode mode, float f1, YogaMeasureMode heightMode) => this.Measure(node, f, mode, f1, heightMode));
		}

		private void RemoveMeasureFunction()
		{
			this.yogaNode.SetMeasureFunction(null);
		}

		protected internal virtual Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			return new Vector2(float.NaN, float.NaN);
		}

		internal YogaSize Measure(YogaNode node, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode)
		{
			Debug.Assert(node == this.yogaNode, "YogaNode instance mismatch");
			Vector2 vector = this.DoMeasure(width, (VisualElement.MeasureMode)widthMode, height, (VisualElement.MeasureMode)heightMode);
			float scaledPixelsPerPoint = this.scaledPixelsPerPoint;
			return MeasureOutput.Make(AlignmentUtils.RoundToPixelGrid(vector.x, scaledPixelsPerPoint, 0.02f), AlignmentUtils.RoundToPixelGrid(vector.y, scaledPixelsPerPoint, 0.02f));
		}

		internal void SetSize(Vector2 size)
		{
			Rect layout = this.layout;
			layout.width = size.x;
			layout.height = size.y;
			this.layout = layout;
		}

		private void FinalizeLayout()
		{
			bool hasInlineStyle = this.hasInlineStyle;
			if (hasInlineStyle)
			{
				this.computedStyle.SyncWithLayout(this.yogaNode);
			}
			else
			{
				this.yogaNode.CopyStyle(this.computedStyle.yogaNode);
			}
		}

		internal void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			bool flag = this.inlineStyleAccess == null;
			if (flag)
			{
				this.inlineStyleAccess = new InlineStyleAccess(this);
			}
			this.inlineStyleAccess.SetInlineRule(sheet, rule);
		}

		internal void SetSharedStyles(ComputedStyle sharedStyle)
		{
			Debug.Assert(sharedStyle.isShared);
			bool flag = sharedStyle == this.m_SharedStyle;
			if (!flag)
			{
				StyleEnum<OverflowInternal> overflow = this.m_Style.overflow;
				StyleLength borderBottomLeftRadius = this.m_Style.borderBottomLeftRadius;
				StyleLength borderBottomRightRadius = this.m_Style.borderBottomRightRadius;
				StyleLength borderTopLeftRadius = this.m_Style.borderTopLeftRadius;
				StyleLength borderTopRightRadius = this.m_Style.borderTopRightRadius;
				StyleFloat borderLeftWidth = this.m_Style.borderLeftWidth;
				StyleFloat borderTopWidth = this.m_Style.borderTopWidth;
				StyleFloat borderRightWidth = this.m_Style.borderRightWidth;
				StyleFloat borderBottomWidth = this.m_Style.borderBottomWidth;
				StyleFloat opacity = this.m_Style.opacity;
				bool hasInlineStyle = this.hasInlineStyle;
				if (hasInlineStyle)
				{
					this.inlineStyleAccess.ApplyInlineStyles(sharedStyle);
				}
				else
				{
					this.m_Style = sharedStyle;
				}
				this.m_SharedStyle = sharedStyle;
				this.FinalizeLayout();
				VersionChangeType versionChangeType = VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint;
				bool flag2 = this.m_Style.overflow != overflow;
				if (flag2)
				{
					versionChangeType |= VersionChangeType.Overflow;
				}
				bool flag3 = borderBottomLeftRadius != this.m_Style.borderBottomLeftRadius || borderBottomRightRadius != this.m_Style.borderBottomRightRadius || borderTopLeftRadius != this.m_Style.borderTopLeftRadius || borderTopRightRadius != this.m_Style.borderTopRightRadius;
				if (flag3)
				{
					versionChangeType |= VersionChangeType.BorderRadius;
				}
				bool flag4 = borderLeftWidth != this.m_Style.borderLeftWidth || borderTopWidth != this.m_Style.borderTopWidth || borderRightWidth != this.m_Style.borderRightWidth || borderBottomWidth != this.m_Style.borderBottomWidth;
				if (flag4)
				{
					versionChangeType |= VersionChangeType.BorderWidth;
				}
				bool flag5 = this.m_Style.opacity != opacity;
				if (flag5)
				{
					versionChangeType |= VersionChangeType.Opacity;
				}
				this.IncrementVersion(versionChangeType);
			}
		}

		internal void ResetPositionProperties()
		{
			bool flag = !this.hasInlineStyle;
			if (!flag)
			{
				this.style.position = StyleKeyword.Null;
				this.style.marginLeft = StyleKeyword.Null;
				this.style.marginRight = StyleKeyword.Null;
				this.style.marginBottom = StyleKeyword.Null;
				this.style.marginTop = StyleKeyword.Null;
				this.style.left = StyleKeyword.Null;
				this.style.top = StyleKeyword.Null;
				this.style.right = StyleKeyword.Null;
				this.style.bottom = StyleKeyword.Null;
				this.style.width = StyleKeyword.Null;
				this.style.height = StyleKeyword.Null;
				this.FinalizeLayout();
				this.IncrementVersion(VersionChangeType.Layout);
			}
		}

		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" ",
				this.name,
				" ",
				this.layout.ToString(),
				" world rect: ",
				this.worldBound.ToString()
			});
		}

		public IEnumerable<string> GetClasses()
		{
			return this.m_ClassList;
		}

		internal List<string> GetClassesForIteration()
		{
			return this.m_ClassList;
		}

		public void ClearClassList()
		{
			bool flag = this.m_ClassList.Count > 0;
			if (flag)
			{
				StringListPool.Release(this.m_ClassList);
				this.m_ClassList = VisualElement.s_EmptyClassList;
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		public void AddToClassList(string className)
		{
			bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
			if (flag)
			{
				this.m_ClassList = StringListPool.Get();
			}
			else
			{
				bool flag2 = this.m_ClassList.Contains(className);
				if (flag2)
				{
					return;
				}
				bool flag3 = this.m_ClassList.Capacity == this.m_ClassList.Count;
				if (flag3)
				{
					this.m_ClassList.Capacity++;
				}
			}
			this.m_ClassList.Add(className);
			this.IncrementVersion(VersionChangeType.StyleSheet);
		}

		public void RemoveFromClassList(string className)
		{
			bool flag = this.m_ClassList.Remove(className);
			if (flag)
			{
				bool flag2 = this.m_ClassList.Count == 0;
				if (flag2)
				{
					StringListPool.Release(this.m_ClassList);
					this.m_ClassList = VisualElement.s_EmptyClassList;
				}
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		public void ToggleInClassList(string className)
		{
			bool flag = this.ClassListContains(className);
			if (flag)
			{
				this.RemoveFromClassList(className);
			}
			else
			{
				this.AddToClassList(className);
			}
		}

		public void EnableInClassList(string className, bool enable)
		{
			if (enable)
			{
				this.AddToClassList(className);
			}
			else
			{
				this.RemoveFromClassList(className);
			}
		}

		public bool ClassListContains(string cls)
		{
			bool result;
			for (int i = 0; i < this.m_ClassList.Count; i++)
			{
				bool flag = this.m_ClassList[i] == cls;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public object FindAncestorUserData()
		{
			object result;
			for (VisualElement parent = this.parent; parent != null; parent = parent.parent)
			{
				bool flag = parent.userData != null;
				if (flag)
				{
					result = parent.userData;
					return result;
				}
			}
			result = null;
			return result;
		}

		internal object GetProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			object result;
			this.TryGetPropertyInternal(key, out result);
			return result;
		}

		internal void SetProperty(PropertyName key, object value)
		{
			VisualElement.CheckUserKeyArgument(key);
			this.SetPropertyInternal(key, value);
		}

		internal bool HasProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			object obj;
			return this.TryGetPropertyInternal(key, out obj);
		}

		private bool TryGetPropertyInternal(PropertyName key, out object value)
		{
			value = null;
			bool flag = this.m_PropertyBag != null;
			bool result;
			if (flag)
			{
				for (int i = 0; i < this.m_PropertyBag.Count; i++)
				{
					bool flag2 = this.m_PropertyBag[i].Key == key;
					if (flag2)
					{
						value = this.m_PropertyBag[i].Value;
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		private static void CheckUserKeyArgument(PropertyName key)
		{
			bool flag = PropertyName.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentNullException("key");
			}
			bool flag2 = key == VisualElement.userDataPropertyKey;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The {0} key is reserved by the system", VisualElement.userDataPropertyKey));
			}
		}

		private void SetPropertyInternal(PropertyName key, object value)
		{
			KeyValuePair<PropertyName, object> keyValuePair = new KeyValuePair<PropertyName, object>(key, value);
			bool flag = this.m_PropertyBag == null;
			if (flag)
			{
				this.m_PropertyBag = new List<KeyValuePair<PropertyName, object>>(1);
				this.m_PropertyBag.Add(keyValuePair);
			}
			else
			{
				for (int i = 0; i < this.m_PropertyBag.Count; i++)
				{
					bool flag2 = this.m_PropertyBag[i].Key == key;
					if (flag2)
					{
						this.m_PropertyBag[i] = keyValuePair;
						return;
					}
				}
				bool flag3 = this.m_PropertyBag.Capacity == this.m_PropertyBag.Count;
				if (flag3)
				{
					this.m_PropertyBag.Capacity++;
				}
				this.m_PropertyBag.Add(keyValuePair);
			}
		}

		private void UpdateCursorStyle(long eventType)
		{
			bool flag = this.elementPanel != null;
			if (flag)
			{
				bool flag2 = eventType == EventBase<MouseOverEvent>.TypeId() && this.elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) == this;
				if (flag2)
				{
					this.elementPanel.cursorManager.SetCursor(this.computedStyle.cursor.value);
				}
				else
				{
					bool flag3 = eventType == EventBase<MouseOutEvent>.TypeId();
					if (flag3)
					{
						this.elementPanel.cursorManager.ResetCursor();
					}
				}
			}
		}

		private VisualElementAnimationSystem GetAnimationSystem()
		{
			bool flag = this.elementPanel != null;
			VisualElementAnimationSystem result;
			if (flag)
			{
				result = (this.elementPanel.GetUpdater(VisualTreeUpdatePhase.Animation) as VisualElementAnimationSystem);
			}
			else
			{
				result = null;
			}
			return result;
		}

		internal void RegisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations == null;
			if (flag)
			{
				this.m_RunningAnimations = new List<IValueAnimationUpdate>();
			}
			this.m_RunningAnimations.Add(anim);
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.RegisterAnimation(anim);
			}
		}

		internal void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations != null;
			if (flag)
			{
				this.m_RunningAnimations.Remove(anim);
			}
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.UnregisterAnimation(anim);
			}
		}

		private void UnregisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.UnregisterAnimations(this.m_RunningAnimations);
				}
			}
		}

		private void RegisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.RegisterAnimations(this.m_RunningAnimations);
				}
			}
		}

		ValueAnimation<float> ITransitionAnimations.Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		ValueAnimation<Rect> ITransitionAnimations.Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		ValueAnimation<Color> ITransitionAnimations.Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		ValueAnimation<Vector3> ITransitionAnimations.Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		ValueAnimation<Vector2> ITransitionAnimations.Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		ValueAnimation<Quaternion> ITransitionAnimations.Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues from, StyleValues to, int durationMs)
		{
			return this.Start((VisualElement e) => from, to, durationMs);
		}

		ValueAnimation<float> ITransitionAnimations.Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return VisualElement.StartAnimation<float>(ValueAnimation<float>.Create(this, new Func<float, float, float, float>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		ValueAnimation<Rect> ITransitionAnimations.Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return VisualElement.StartAnimation<Rect>(ValueAnimation<Rect>.Create(this, new Func<Rect, Rect, float, Rect>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		ValueAnimation<Color> ITransitionAnimations.Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return VisualElement.StartAnimation<Color>(ValueAnimation<Color>.Create(this, new Func<Color, Color, float, Color>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		ValueAnimation<Vector3> ITransitionAnimations.Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector3>(ValueAnimation<Vector3>.Create(this, new Func<Vector3, Vector3, float, Vector3>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		ValueAnimation<Vector2> ITransitionAnimations.Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector2>(ValueAnimation<Vector2>.Create(this, new Func<Vector2, Vector2, float, Vector2>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		ValueAnimation<Quaternion> ITransitionAnimations.Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return VisualElement.StartAnimation<Quaternion>(ValueAnimation<Quaternion>.Create(this, new Func<Quaternion, Quaternion, float, Quaternion>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		private static ValueAnimation<T> StartAnimation<T>(ValueAnimation<T> anim, Func<VisualElement, T> fromValueGetter, T to, int durationMs, Action<VisualElement, T> onValueChanged)
		{
			anim.initialValue = fromValueGetter;
			anim.to = to;
			anim.durationMs = durationMs;
			anim.valueUpdated = onValueChanged;
			anim.Start();
			return anim;
		}

		private static void AssignStyleValues(VisualElement ve, StyleValues src)
		{
			IStyle style = ve.style;
			foreach (StyleValue current in src.m_StyleValues.m_Values)
			{
				StylePropertyId id = current.id;
				StylePropertyId stylePropertyId = id;
				switch (stylePropertyId)
				{
				case StylePropertyId.Unknown:
					break;
				case StylePropertyId.Color:
					style.color = current.color;
					break;
				case StylePropertyId.FontSize:
					style.fontSize = current.number;
					break;
				default:
					switch (stylePropertyId)
					{
					case StylePropertyId.BackgroundColor:
						style.backgroundColor = current.color;
						break;
					case StylePropertyId.BackgroundImage:
					case StylePropertyId.BorderBottomColor:
					case StylePropertyId.BorderLeftColor:
					case StylePropertyId.BorderRightColor:
					case StylePropertyId.BorderTopColor:
					case StylePropertyId.Cursor:
					case StylePropertyId.Display:
					case StylePropertyId.FlexBasis:
					case StylePropertyId.FlexDirection:
					case StylePropertyId.FlexWrap:
					case StylePropertyId.JustifyContent:
					case StylePropertyId.MaxHeight:
					case StylePropertyId.MaxWidth:
					case StylePropertyId.MinHeight:
					case StylePropertyId.MinWidth:
					case StylePropertyId.Overflow:
					case StylePropertyId.Position:
					case StylePropertyId.TextOverflow:
					case StylePropertyId.UnityBackgroundScaleMode:
					case StylePropertyId.UnityOverflowClipBox:
					case StylePropertyId.UnitySliceBottom:
					case StylePropertyId.UnitySliceLeft:
					case StylePropertyId.UnitySliceRight:
					case StylePropertyId.UnitySliceTop:
					case StylePropertyId.UnityTextOverflowPosition:
						break;
					case StylePropertyId.BorderBottomLeftRadius:
						style.borderBottomLeftRadius = current.number;
						break;
					case StylePropertyId.BorderBottomRightRadius:
						style.borderBottomRightRadius = current.number;
						break;
					case StylePropertyId.BorderBottomWidth:
						style.borderBottomWidth = current.number;
						break;
					case StylePropertyId.BorderLeftWidth:
						style.borderLeftWidth = current.number;
						break;
					case StylePropertyId.BorderRightWidth:
						style.borderRightWidth = current.number;
						break;
					case StylePropertyId.BorderTopLeftRadius:
						style.borderTopLeftRadius = current.number;
						break;
					case StylePropertyId.BorderTopRightRadius:
						style.borderTopRightRadius = current.number;
						break;
					case StylePropertyId.BorderTopWidth:
						style.borderTopWidth = current.number;
						break;
					case StylePropertyId.Bottom:
						style.bottom = current.number;
						break;
					case StylePropertyId.FlexGrow:
						style.flexGrow = current.number;
						break;
					case StylePropertyId.FlexShrink:
						style.flexShrink = current.number;
						break;
					case StylePropertyId.Height:
						style.height = current.number;
						break;
					case StylePropertyId.Left:
						style.left = current.number;
						break;
					case StylePropertyId.MarginBottom:
						style.marginBottom = current.number;
						break;
					case StylePropertyId.MarginLeft:
						style.marginLeft = current.number;
						break;
					case StylePropertyId.MarginRight:
						style.marginRight = current.number;
						break;
					case StylePropertyId.MarginTop:
						style.marginTop = current.number;
						break;
					case StylePropertyId.Opacity:
						style.opacity = current.number;
						break;
					case StylePropertyId.PaddingBottom:
						style.paddingBottom = current.number;
						break;
					case StylePropertyId.PaddingLeft:
						style.paddingLeft = current.number;
						break;
					case StylePropertyId.PaddingRight:
						style.paddingRight = current.number;
						break;
					case StylePropertyId.PaddingTop:
						style.paddingTop = current.number;
						break;
					case StylePropertyId.Right:
						style.right = current.number;
						break;
					case StylePropertyId.Top:
						style.top = current.number;
						break;
					case StylePropertyId.UnityBackgroundImageTintColor:
						style.unityBackgroundImageTintColor = current.color;
						break;
					case StylePropertyId.Width:
						style.width = current.number;
						break;
					default:
						if (stylePropertyId == StylePropertyId.BorderColor)
						{
							style.borderLeftColor = current.color;
							style.borderTopColor = current.color;
							style.borderRightColor = current.color;
							style.borderBottomColor = current.color;
						}
						break;
					}
					break;
				}
			}
		}

		private StyleValues ReadCurrentValues(VisualElement ve, StyleValues targetValuesToRead)
		{
			StyleValues result = default(StyleValues);
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			foreach (StyleValue current in targetValuesToRead.m_StyleValues.m_Values)
			{
				StylePropertyId id = current.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.Color)
				{
					if (stylePropertyId != StylePropertyId.Unknown)
					{
						if (stylePropertyId == StylePropertyId.Color)
						{
							result.color = resolvedStyle.color;
						}
					}
				}
				else
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.BackgroundColor:
						result.backgroundColor = resolvedStyle.backgroundColor;
						break;
					case StylePropertyId.BackgroundImage:
					case StylePropertyId.BorderBottomColor:
					case StylePropertyId.BorderLeftColor:
					case StylePropertyId.BorderRightColor:
					case StylePropertyId.BorderTopColor:
					case StylePropertyId.Cursor:
					case StylePropertyId.Display:
					case StylePropertyId.FlexBasis:
					case StylePropertyId.FlexDirection:
					case StylePropertyId.FlexWrap:
					case StylePropertyId.JustifyContent:
					case StylePropertyId.MaxHeight:
					case StylePropertyId.MaxWidth:
					case StylePropertyId.MinHeight:
					case StylePropertyId.MinWidth:
					case StylePropertyId.Overflow:
					case StylePropertyId.Position:
					case StylePropertyId.TextOverflow:
					case StylePropertyId.UnityBackgroundScaleMode:
					case StylePropertyId.UnityOverflowClipBox:
					case StylePropertyId.UnitySliceBottom:
					case StylePropertyId.UnitySliceLeft:
					case StylePropertyId.UnitySliceRight:
					case StylePropertyId.UnitySliceTop:
					case StylePropertyId.UnityTextOverflowPosition:
						break;
					case StylePropertyId.BorderBottomLeftRadius:
						result.borderBottomLeftRadius = resolvedStyle.borderBottomLeftRadius;
						break;
					case StylePropertyId.BorderBottomRightRadius:
						result.borderBottomRightRadius = resolvedStyle.borderBottomRightRadius;
						break;
					case StylePropertyId.BorderBottomWidth:
						result.borderBottomWidth = resolvedStyle.borderBottomWidth;
						break;
					case StylePropertyId.BorderLeftWidth:
						result.borderLeftWidth = resolvedStyle.borderLeftWidth;
						break;
					case StylePropertyId.BorderRightWidth:
						result.borderRightWidth = resolvedStyle.borderRightWidth;
						break;
					case StylePropertyId.BorderTopLeftRadius:
						result.borderTopLeftRadius = resolvedStyle.borderTopLeftRadius;
						break;
					case StylePropertyId.BorderTopRightRadius:
						result.borderTopRightRadius = resolvedStyle.borderTopRightRadius;
						break;
					case StylePropertyId.BorderTopWidth:
						result.borderTopWidth = resolvedStyle.borderTopWidth;
						break;
					case StylePropertyId.Bottom:
						result.bottom = resolvedStyle.bottom;
						break;
					case StylePropertyId.FlexGrow:
						result.flexGrow = resolvedStyle.flexGrow;
						break;
					case StylePropertyId.FlexShrink:
						result.flexShrink = resolvedStyle.flexShrink;
						break;
					case StylePropertyId.Height:
						result.height = resolvedStyle.height;
						break;
					case StylePropertyId.Left:
						result.left = resolvedStyle.left;
						break;
					case StylePropertyId.MarginBottom:
						result.marginBottom = resolvedStyle.marginBottom;
						break;
					case StylePropertyId.MarginLeft:
						result.marginLeft = resolvedStyle.marginLeft;
						break;
					case StylePropertyId.MarginRight:
						result.marginRight = resolvedStyle.marginRight;
						break;
					case StylePropertyId.MarginTop:
						result.marginTop = resolvedStyle.marginTop;
						break;
					case StylePropertyId.Opacity:
						result.opacity = resolvedStyle.opacity;
						break;
					case StylePropertyId.PaddingBottom:
						result.paddingBottom = resolvedStyle.paddingBottom;
						break;
					case StylePropertyId.PaddingLeft:
						result.paddingLeft = resolvedStyle.paddingLeft;
						break;
					case StylePropertyId.PaddingRight:
						result.paddingRight = resolvedStyle.paddingRight;
						break;
					case StylePropertyId.PaddingTop:
						result.paddingTop = resolvedStyle.paddingTop;
						break;
					case StylePropertyId.Right:
						result.right = resolvedStyle.right;
						break;
					case StylePropertyId.Top:
						result.top = resolvedStyle.top;
						break;
					case StylePropertyId.UnityBackgroundImageTintColor:
						result.unityBackgroundImageTintColor = resolvedStyle.unityBackgroundImageTintColor;
						break;
					case StylePropertyId.Width:
						result.width = resolvedStyle.width;
						break;
					default:
						if (stylePropertyId == StylePropertyId.BorderColor)
						{
							result.borderColor = resolvedStyle.borderLeftColor;
						}
						break;
					}
				}
			}
			return result;
		}

		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues to, int durationMs)
		{
			return this.Start((VisualElement e) => this.ReadCurrentValues(e, to), to, durationMs);
		}

		private ValueAnimation<StyleValues> Start(Func<VisualElement, StyleValues> fromValueGetter, StyleValues to, int durationMs)
		{
			return VisualElement.StartAnimation<StyleValues>(ValueAnimation<StyleValues>.Create(this, new Func<StyleValues, StyleValues, float, StyleValues>(Lerp.Interpolate)), fromValueGetter, to, durationMs, new Action<VisualElement, StyleValues>(VisualElement.AssignStyleValues));
		}

		ValueAnimation<Rect> ITransitionAnimations.Layout(Rect to, int durationMs)
		{
			ITransitionAnimations arg_4C_0 = this.experimental.animation;
			Func<VisualElement, Rect> arg_4C_1;
			if ((arg_4C_1 = VisualElement.<>c.<>9__252_0) == null)
			{
				arg_4C_1 = (VisualElement.<>c.<>9__252_0 = new Func<VisualElement, Rect>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Layout>b__252_0));
			}
			Action<VisualElement, Rect> arg_4C_4;
			if ((arg_4C_4 = VisualElement.<>c.<>9__252_1) == null)
			{
				arg_4C_4 = (VisualElement.<>c.<>9__252_1 = new Action<VisualElement, Rect>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Layout>b__252_1));
			}
			return arg_4C_0.Start(arg_4C_1, to, durationMs, arg_4C_4);
		}

		ValueAnimation<Vector2> ITransitionAnimations.TopLeft(Vector2 to, int durationMs)
		{
			ITransitionAnimations arg_4C_0 = this.experimental.animation;
			Func<VisualElement, Vector2> arg_4C_1;
			if ((arg_4C_1 = VisualElement.<>c.<>9__253_0) == null)
			{
				arg_4C_1 = (VisualElement.<>c.<>9__253_0 = new Func<VisualElement, Vector2>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.TopLeft>b__253_0));
			}
			Action<VisualElement, Vector2> arg_4C_4;
			if ((arg_4C_4 = VisualElement.<>c.<>9__253_1) == null)
			{
				arg_4C_4 = (VisualElement.<>c.<>9__253_1 = new Action<VisualElement, Vector2>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.TopLeft>b__253_1));
			}
			return arg_4C_0.Start(arg_4C_1, to, durationMs, arg_4C_4);
		}

		ValueAnimation<Vector2> ITransitionAnimations.Size(Vector2 to, int durationMs)
		{
			ITransitionAnimations arg_4C_0 = this.experimental.animation;
			Func<VisualElement, Vector2> arg_4C_1;
			if ((arg_4C_1 = VisualElement.<>c.<>9__254_0) == null)
			{
				arg_4C_1 = (VisualElement.<>c.<>9__254_0 = new Func<VisualElement, Vector2>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Size>b__254_0));
			}
			Action<VisualElement, Vector2> arg_4C_4;
			if ((arg_4C_4 = VisualElement.<>c.<>9__254_1) == null)
			{
				arg_4C_4 = (VisualElement.<>c.<>9__254_1 = new Action<VisualElement, Vector2>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Size>b__254_1));
			}
			return arg_4C_0.Start(arg_4C_1, to, durationMs, arg_4C_4);
		}

		ValueAnimation<float> ITransitionAnimations.Scale(float to, int durationMs)
		{
			ITransitionAnimations arg_4C_0 = this.experimental.animation;
			Func<VisualElement, float> arg_4C_1;
			if ((arg_4C_1 = VisualElement.<>c.<>9__255_0) == null)
			{
				arg_4C_1 = (VisualElement.<>c.<>9__255_0 = new Func<VisualElement, float>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Scale>b__255_0));
			}
			Action<VisualElement, float> arg_4C_4;
			if ((arg_4C_4 = VisualElement.<>c.<>9__255_1) == null)
			{
				arg_4C_4 = (VisualElement.<>c.<>9__255_1 = new Action<VisualElement, float>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Scale>b__255_1));
			}
			return arg_4C_0.Start(arg_4C_1, to, durationMs, arg_4C_4);
		}

		ValueAnimation<Vector3> ITransitionAnimations.Position(Vector3 to, int durationMs)
		{
			ITransitionAnimations arg_4C_0 = this.experimental.animation;
			Func<VisualElement, Vector3> arg_4C_1;
			if ((arg_4C_1 = VisualElement.<>c.<>9__256_0) == null)
			{
				arg_4C_1 = (VisualElement.<>c.<>9__256_0 = new Func<VisualElement, Vector3>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Position>b__256_0));
			}
			Action<VisualElement, Vector3> arg_4C_4;
			if ((arg_4C_4 = VisualElement.<>c.<>9__256_1) == null)
			{
				arg_4C_4 = (VisualElement.<>c.<>9__256_1 = new Action<VisualElement, Vector3>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Position>b__256_1));
			}
			return arg_4C_0.Start(arg_4C_1, to, durationMs, arg_4C_4);
		}

		ValueAnimation<Quaternion> ITransitionAnimations.Rotation(Quaternion to, int durationMs)
		{
			ITransitionAnimations arg_4C_0 = this.experimental.animation;
			Func<VisualElement, Quaternion> arg_4C_1;
			if ((arg_4C_1 = VisualElement.<>c.<>9__257_0) == null)
			{
				arg_4C_1 = (VisualElement.<>c.<>9__257_0 = new Func<VisualElement, Quaternion>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Rotation>b__257_0));
			}
			Action<VisualElement, Quaternion> arg_4C_4;
			if ((arg_4C_4 = VisualElement.<>c.<>9__257_1) == null)
			{
				arg_4C_4 = (VisualElement.<>c.<>9__257_1 = new Action<VisualElement, Quaternion>(VisualElement.<>c.<>9.<UnityEngine.UIElements.Experimental.ITransitionAnimations.Rotation>b__257_1));
			}
			return arg_4C_0.Start(arg_4C_1, to, durationMs, arg_4C_4);
		}

		internal bool ShouldClip()
		{
			return this.computedStyle.overflow.value > OverflowInternal.Visible;
		}

		public void Add(VisualElement child)
		{
			bool flag = child == null;
			if (!flag)
			{
				VisualElement contentContainer = this.contentContainer;
				bool flag2 = contentContainer == null;
				if (flag2)
				{
					throw new InvalidOperationException("You can't add directly to this VisualElement. Use hierarchy.Add() if you know what you're doing.");
				}
				bool flag3 = contentContainer == this;
				if (flag3)
				{
					this.hierarchy.Add(child);
				}
				else if (contentContainer != null)
				{
					contentContainer.Add(child);
				}
				child.m_LogicalParent = this;
			}
		}

		public void Insert(int index, VisualElement element)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = this.contentContainer == this;
				if (flag2)
				{
					this.hierarchy.Insert(index, element);
				}
				else
				{
					VisualElement expr_35 = this.contentContainer;
					if (expr_35 != null)
					{
						expr_35.Insert(index, element);
					}
				}
				element.m_LogicalParent = this;
			}
		}

		public void Remove(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Remove(element);
			}
			else
			{
				VisualElement expr_29 = this.contentContainer;
				if (expr_29 != null)
				{
					expr_29.Remove(element);
				}
			}
		}

		public void RemoveAt(int index)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.RemoveAt(index);
			}
			else
			{
				VisualElement expr_29 = this.contentContainer;
				if (expr_29 != null)
				{
					expr_29.RemoveAt(index);
				}
			}
		}

		public void Clear()
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Clear();
			}
			else
			{
				VisualElement expr_28 = this.contentContainer;
				if (expr_28 != null)
				{
					expr_28.Clear();
				}
			}
		}

		public VisualElement ElementAt(int index)
		{
			return this[index];
		}

		public int IndexOf(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			int result;
			if (flag)
			{
				result = this.hierarchy.IndexOf(element);
			}
			else
			{
				VisualElement expr_27 = this.contentContainer;
				result = ((expr_27 != null) ? expr_27.IndexOf(element) : -1);
			}
			return result;
		}

		public IEnumerable<VisualElement> Children()
		{
			bool flag = this.contentContainer == this;
			IEnumerable<VisualElement> result;
			if (flag)
			{
				result = this.hierarchy.Children();
			}
			else
			{
				VisualElement expr_26 = this.contentContainer;
				result = (((expr_26 != null) ? expr_26.Children() : null) ?? VisualElement.s_EmptyList);
			}
			return result;
		}

		public void Sort(Comparison<VisualElement> comp)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Sort(comp);
			}
			else
			{
				VisualElement expr_29 = this.contentContainer;
				if (expr_29 != null)
				{
					expr_29.Sort(comp);
				}
			}
		}

		public void BringToFront()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.BringToFront(this);
			}
		}

		public void SendToBack()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.SendToBack(this);
			}
		}

		public void PlaceBehind(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceBehind(this, sibling);
		}

		public void PlaceInFront(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceInFront(this, sibling);
		}

		public void RemoveFromHierarchy()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.hierarchy.Remove(this);
			}
		}

		public T GetFirstOfType<T>() where T : class
		{
			T t = this as T;
			bool flag = t != null;
			T result;
			if (flag)
			{
				result = t;
			}
			else
			{
				result = this.GetFirstAncestorOfType<T>();
			}
			return result;
		}

		public T GetFirstAncestorOfType<T>() where T : class
		{
			T result;
			for (VisualElement parent = this.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				T t = parent as T;
				bool flag = t != null;
				if (flag)
				{
					result = t;
					return result;
				}
			}
			result = default(T);
			return result;
		}

		public bool Contains(VisualElement child)
		{
			bool result;
			while (child != null)
			{
				bool flag = child.hierarchy.parent == this;
				if (flag)
				{
					result = true;
					return result;
				}
				child = child.hierarchy.parent;
			}
			result = false;
			return result;
		}

		private void GatherAllChildren(List<VisualElement> elements)
		{
			bool flag = this.m_Children.Count > 0;
			if (flag)
			{
				int i = elements.Count;
				elements.AddRange(this.m_Children);
				while (i < elements.Count)
				{
					VisualElement visualElement = elements[i];
					elements.AddRange(visualElement.m_Children);
					i++;
				}
			}
		}

		public VisualElement FindCommonAncestor(VisualElement other)
		{
			bool flag = other == null;
			if (flag)
			{
				throw new ArgumentNullException("other");
			}
			bool flag2 = this.panel != other.panel;
			VisualElement result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				VisualElement visualElement = this;
				int i = 0;
				while (visualElement != null)
				{
					i++;
					visualElement = visualElement.hierarchy.parent;
				}
				VisualElement visualElement2 = other;
				int j = 0;
				while (visualElement2 != null)
				{
					j++;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				visualElement = this;
				visualElement2 = other;
				while (i > j)
				{
					i--;
					visualElement = visualElement.hierarchy.parent;
				}
				while (j > i)
				{
					j--;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				while (visualElement != visualElement2)
				{
					visualElement = visualElement.hierarchy.parent;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				result = visualElement;
			}
			return result;
		}

		internal VisualElement GetRoot()
		{
			bool flag = this.panel != null;
			VisualElement result;
			if (flag)
			{
				result = this.panel.visualTree;
			}
			else
			{
				VisualElement visualElement = this;
				while (visualElement.m_PhysicalParent != null)
				{
					visualElement = visualElement.m_PhysicalParent;
				}
				result = visualElement;
			}
			return result;
		}

		internal VisualElement GetNextElementDepthFirst()
		{
			bool flag = this.m_Children.Count > 0;
			VisualElement result;
			if (flag)
			{
				result = this.m_Children[0];
			}
			else
			{
				VisualElement physicalParent = this.m_PhysicalParent;
				VisualElement visualElement = this;
				while (physicalParent != null)
				{
					int i;
					for (i = 0; i < physicalParent.m_Children.Count; i++)
					{
						bool flag2 = physicalParent.m_Children[i] == visualElement;
						if (flag2)
						{
							break;
						}
					}
					bool flag3 = i < physicalParent.m_Children.Count - 1;
					if (flag3)
					{
						result = physicalParent.m_Children[i + 1];
						return result;
					}
					visualElement = physicalParent;
					physicalParent = physicalParent.m_PhysicalParent;
				}
				result = null;
			}
			return result;
		}

		internal VisualElement GetPreviousElementDepthFirst()
		{
			bool flag = this.m_PhysicalParent != null;
			VisualElement result;
			if (flag)
			{
				int i;
				for (i = 0; i < this.m_PhysicalParent.m_Children.Count; i++)
				{
					bool flag2 = this.m_PhysicalParent.m_Children[i] == this;
					if (flag2)
					{
						break;
					}
				}
				bool flag3 = i > 0;
				if (flag3)
				{
					VisualElement visualElement = this.m_PhysicalParent.m_Children[i - 1];
					while (visualElement.m_Children.Count > 0)
					{
						visualElement = visualElement.m_Children[visualElement.m_Children.Count - 1];
					}
					result = visualElement;
				}
				else
				{
					result = this.m_PhysicalParent;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		internal VisualElement RetargetElement(VisualElement retargetAgainst)
		{
			bool flag = retargetAgainst == null;
			VisualElement result;
			if (flag)
			{
				result = this;
			}
			else
			{
				VisualElement visualElement = retargetAgainst.m_PhysicalParent ?? retargetAgainst;
				while (visualElement.m_PhysicalParent != null && !visualElement.isCompositeRoot)
				{
					visualElement = visualElement.m_PhysicalParent;
				}
				VisualElement visualElement2 = this;
				VisualElement physicalParent = this.m_PhysicalParent;
				while (physicalParent != null)
				{
					physicalParent = physicalParent.m_PhysicalParent;
					bool flag2 = physicalParent == visualElement;
					if (flag2)
					{
						result = visualElement2;
						return result;
					}
					bool flag3 = physicalParent != null && physicalParent.isCompositeRoot;
					if (flag3)
					{
						visualElement2 = physicalParent;
					}
				}
				result = this;
			}
			return result;
		}

		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action<TimerState> timerUpdateEvent)
		{
			VisualElement.TimerStateScheduledItem timerStateScheduledItem = new VisualElement.TimerStateScheduledItem(this, timerUpdateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			timerStateScheduledItem.Resume();
			return timerStateScheduledItem;
		}

		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action updateEvent)
		{
			VisualElement.SimpleScheduledItem simpleScheduledItem = new VisualElement.SimpleScheduledItem(this, updateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			simpleScheduledItem.Resume();
			return simpleScheduledItem;
		}

		internal void AddStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				bool flag2 = !VisualElement.s_InternalStyleSheetPath.IsMatch(sheetPath);
				if (flag2)
				{
					Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				}
			}
			else
			{
				this.styleSheets.Add(styleSheet);
			}
		}

		internal bool HasStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			bool result;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				result = false;
			}
			else
			{
				result = this.styleSheets.Contains(styleSheet);
			}
			return result;
		}

		internal void RemoveStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
			}
			else
			{
				this.styleSheets.Remove(styleSheet);
			}
		}

		private StyleFloat ResolveLengthValue(StyleLength styleLength, bool isRow)
		{
			bool flag = styleLength.keyword > StyleKeyword.Undefined;
			StyleFloat result;
			if (flag)
			{
				result = styleLength.ToStyleFloat();
			}
			else
			{
				Length value = styleLength.value;
				bool flag2 = value.unit != LengthUnit.Percent;
				if (flag2)
				{
					result = styleLength.ToStyleFloat();
				}
				else
				{
					VisualElement parent = this.hierarchy.parent;
					bool flag3 = parent == null;
					if (flag3)
					{
						result = 0f;
					}
					else
					{
						float num = isRow ? parent.resolvedStyle.width : parent.resolvedStyle.height;
						result = value.value * num / 100f;
					}
				}
			}
			return result;
		}

		private static void OnTooltip(TooltipEvent e)
		{
			VisualElement visualElement = e.currentTarget as VisualElement;
			bool flag = visualElement != null && !string.IsNullOrEmpty(visualElement.tooltip);
			if (flag)
			{
				e.rect = visualElement.worldBound;
				e.tooltip = visualElement.tooltip;
				e.StopImmediatePropagation();
			}
		}
	}
}
