using System;

namespace UnityEngine.UIElements
{
	public class ScrollView : VisualElement
	{
		public new class UxmlFactory : UxmlFactory<ScrollView, ScrollView.UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private UxmlEnumAttributeDescription<ScrollViewMode> m_ScrollViewMode = new UxmlEnumAttributeDescription<ScrollViewMode>
			{
				name = "mode",
				defaultValue = ScrollViewMode.Vertical
			};

			private UxmlBoolAttributeDescription m_ShowHorizontal = new UxmlBoolAttributeDescription
			{
				name = "show-horizontal-scroller"
			};

			private UxmlBoolAttributeDescription m_ShowVertical = new UxmlBoolAttributeDescription
			{
				name = "show-vertical-scroller"
			};

			private UxmlFloatAttributeDescription m_HorizontalPageSize = new UxmlFloatAttributeDescription
			{
				name = "horizontal-page-size",
				defaultValue = 20f
			};

			private UxmlFloatAttributeDescription m_VerticalPageSize = new UxmlFloatAttributeDescription
			{
				name = "vertical-page-size",
				defaultValue = 20f
			};

			private UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior> m_TouchScrollBehavior = new UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior>
			{
				name = "touch-scroll-type",
				defaultValue = ScrollView.TouchScrollBehavior.Clamped
			};

			private UxmlFloatAttributeDescription m_ScrollDecelerationRate = new UxmlFloatAttributeDescription
			{
				name = "scroll-deceleration-rate",
				defaultValue = ScrollView.k_DefaultScrollDecelerationRate
			};

			private UxmlFloatAttributeDescription m_Elasticity = new UxmlFloatAttributeDescription
			{
				name = "elasticity",
				defaultValue = ScrollView.k_DefaultElasticity
			};

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ScrollView scrollView = (ScrollView)ve;
				scrollView.SetScrollViewMode(this.m_ScrollViewMode.GetValueFromBag(bag, cc));
				scrollView.showHorizontal = this.m_ShowHorizontal.GetValueFromBag(bag, cc);
				scrollView.showVertical = this.m_ShowVertical.GetValueFromBag(bag, cc);
				scrollView.horizontalPageSize = this.m_HorizontalPageSize.GetValueFromBag(bag, cc);
				scrollView.verticalPageSize = this.m_VerticalPageSize.GetValueFromBag(bag, cc);
				scrollView.scrollDecelerationRate = this.m_ScrollDecelerationRate.GetValueFromBag(bag, cc);
				scrollView.touchScrollBehavior = this.m_TouchScrollBehavior.GetValueFromBag(bag, cc);
				scrollView.elasticity = this.m_Elasticity.GetValueFromBag(bag, cc);
			}
		}

		public enum TouchScrollBehavior
		{
			Unrestricted,
			Elastic,
			Clamped
		}

		private bool m_ShowHorizontal;

		private bool m_ShowVertical;

		private static readonly float k_DefaultScrollDecelerationRate = 0.135f;

		private float m_ScrollDecelerationRate = ScrollView.k_DefaultScrollDecelerationRate;

		private static readonly float k_DefaultElasticity = 0.1f;

		private float m_Elasticity = ScrollView.k_DefaultElasticity;

		private ScrollView.TouchScrollBehavior m_TouchScrollBehavior;

		private VisualElement m_ContentContainer;

		public static readonly string ussClassName = "unity-scroll-view";

		public static readonly string viewportUssClassName = ScrollView.ussClassName + "__content-viewport";

		public static readonly string contentUssClassName = ScrollView.ussClassName + "__content-container";

		public static readonly string hScrollerUssClassName = ScrollView.ussClassName + "__horizontal-scroller";

		public static readonly string vScrollerUssClassName = ScrollView.ussClassName + "__vertical-scroller";

		public static readonly string horizontalVariantUssClassName = ScrollView.ussClassName + "--horizontal";

		public static readonly string verticalVariantUssClassName = ScrollView.ussClassName + "--vertical";

		public static readonly string verticalHorizontalVariantUssClassName = ScrollView.ussClassName + "--vertical-horizontal";

		public static readonly string scrollVariantUssClassName = ScrollView.ussClassName + "--scroll";

		private int m_ScrollingPointerId = PointerId.invalidPointerId;

		private Vector2 m_StartPosition;

		private Vector2 m_PointerStartPosition;

		private Vector2 m_Velocity;

		private Vector2 m_SpringBackVelocity;

		private Vector2 m_LowBounds;

		private Vector2 m_HighBounds;

		private IVisualElementScheduledItem m_PostPointerUpAnimation;

		public bool showHorizontal
		{
			get
			{
				return this.m_ShowHorizontal;
			}
			set
			{
				this.m_ShowHorizontal = value;
				this.UpdateScrollers(this.m_ShowHorizontal, this.m_ShowVertical);
			}
		}

		public bool showVertical
		{
			get
			{
				return this.m_ShowVertical;
			}
			set
			{
				this.m_ShowVertical = value;
				this.UpdateScrollers(this.m_ShowHorizontal, this.m_ShowVertical);
			}
		}

		internal bool needsHorizontal
		{
			get
			{
				return this.showHorizontal || this.scrollableWidth > 0f;
			}
		}

		internal bool needsVertical
		{
			get
			{
				return this.showVertical || this.scrollableHeight > 0f;
			}
		}

		public Vector2 scrollOffset
		{
			get
			{
				return new Vector2(this.horizontalScroller.value, this.verticalScroller.value);
			}
			set
			{
				bool flag = value != this.scrollOffset;
				if (flag)
				{
					this.horizontalScroller.value = value.x;
					this.verticalScroller.value = value.y;
					this.UpdateContentViewTransform();
				}
			}
		}

		public float horizontalPageSize
		{
			get
			{
				return this.horizontalScroller.slider.pageSize;
			}
			set
			{
				this.horizontalScroller.slider.pageSize = value;
			}
		}

		public float verticalPageSize
		{
			get
			{
				return this.verticalScroller.slider.pageSize;
			}
			set
			{
				this.verticalScroller.slider.pageSize = value;
			}
		}

		private float scrollableWidth
		{
			get
			{
				return this.contentContainer.layout.width - this.contentViewport.layout.width;
			}
		}

		private float scrollableHeight
		{
			get
			{
				return this.contentContainer.layout.height - this.contentViewport.layout.height;
			}
		}

		private bool hasInertia
		{
			get
			{
				return this.scrollDecelerationRate > 0f;
			}
		}

		public float scrollDecelerationRate
		{
			get
			{
				return this.m_ScrollDecelerationRate;
			}
			set
			{
				this.m_ScrollDecelerationRate = Mathf.Max(0f, value);
			}
		}

		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = Mathf.Max(0f, value);
			}
		}

		public ScrollView.TouchScrollBehavior touchScrollBehavior
		{
			get
			{
				return this.m_TouchScrollBehavior;
			}
			set
			{
				this.m_TouchScrollBehavior = value;
				bool flag = this.m_TouchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
				if (flag)
				{
					this.horizontalScroller.slider.clamped = true;
					this.verticalScroller.slider.clamped = true;
				}
				else
				{
					this.horizontalScroller.slider.clamped = false;
					this.verticalScroller.slider.clamped = false;
				}
			}
		}

		public VisualElement contentViewport
		{
			get;
			private set;
		}

		public Scroller horizontalScroller
		{
			get;
			private set;
		}

		public Scroller verticalScroller
		{
			get;
			private set;
		}

		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		private void UpdateContentViewTransform()
		{
			Vector3 position = this.contentContainer.transform.position;
			Vector2 scrollOffset = this.scrollOffset;
			bool needsVertical = this.needsVertical;
			if (needsVertical)
			{
				scrollOffset.y += this.contentContainer.resolvedStyle.top;
			}
			position.x = GUIUtility.RoundToPixelGrid(-scrollOffset.x);
			position.y = GUIUtility.RoundToPixelGrid(-scrollOffset.y);
			this.contentContainer.transform.position = position;
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		public void ScrollTo(VisualElement child)
		{
			bool flag = child == null;
			if (flag)
			{
				throw new ArgumentNullException("child");
			}
			bool flag2 = !this.contentContainer.Contains(child);
			if (flag2)
			{
				throw new ArgumentException("Cannot scroll to a VisualElement that is not a child of the ScrollView content-container.");
			}
			float num = 0f;
			float num2 = 0f;
			bool flag3 = this.scrollableHeight > 0f;
			if (flag3)
			{
				num = this.GetYDeltaOffset(child);
				this.verticalScroller.value = this.scrollOffset.y + num;
			}
			bool flag4 = this.scrollableWidth > 0f;
			if (flag4)
			{
				num2 = this.GetXDeltaOffset(child);
				this.horizontalScroller.value = this.scrollOffset.x + num2;
			}
			bool flag5 = num == 0f && num2 == 0f;
			if (!flag5)
			{
				this.UpdateContentViewTransform();
			}
		}

		private float GetXDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.x * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.xMin + num;
			float num3 = worldBound.xMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.xMin + num;
			float num5 = worldBound2.xMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				result = deltaDistance * this.horizontalScroller.highValue / this.scrollableWidth;
			}
			return result;
		}

		private float GetYDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.y * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.yMin + num;
			float num3 = worldBound.yMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.yMin + num;
			float num5 = worldBound2.yMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				result = deltaDistance * this.verticalScroller.highValue / this.scrollableHeight;
			}
			return result;
		}

		private float GetDeltaDistance(float viewMin, float viewMax, float childBoundaryMin, float childBoundaryMax)
		{
			float num = viewMax - viewMin;
			float num2 = childBoundaryMax - childBoundaryMin;
			bool flag = num2 > num;
			float result;
			if (flag)
			{
				bool flag2 = viewMin > childBoundaryMin && childBoundaryMax > viewMax;
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					result = ((childBoundaryMin > viewMin) ? (childBoundaryMin - viewMin) : (childBoundaryMax - viewMax));
				}
			}
			else
			{
				float num3 = childBoundaryMax - viewMax;
				bool flag3 = num3 < -1f;
				if (flag3)
				{
					num3 = childBoundaryMin - viewMin;
				}
				result = num3;
			}
			return result;
		}

		public ScrollView() : this(ScrollViewMode.Vertical)
		{
		}

		public ScrollView(ScrollViewMode scrollViewMode)
		{
			base.AddToClassList(ScrollView.ussClassName);
			this.contentViewport = new VisualElement
			{
				name = "unity-content-viewport"
			};
			this.contentViewport.AddToClassList(ScrollView.viewportUssClassName);
			this.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.contentViewport.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.contentViewport.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.contentViewport);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.m_ContentContainer.AddToClassList(ScrollView.contentUssClassName);
			this.m_ContentContainer.usageHints = UsageHints.GroupTransform;
			this.contentViewport.Add(this.m_ContentContainer);
			this.SetScrollViewMode(scrollViewMode);
			this.horizontalScroller = new Scroller(0f, 2.14748365E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(value, this.scrollOffset.y);
				this.UpdateContentViewTransform();
			}, SliderDirection.Horizontal)
			{
				viewDataKey = "HorizontalScroller",
				visible = false
			};
			this.horizontalScroller.AddToClassList(ScrollView.hScrollerUssClassName);
			base.hierarchy.Add(this.horizontalScroller);
			this.verticalScroller = new Scroller(0f, 2.14748365E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(this.scrollOffset.x, value);
				this.UpdateContentViewTransform();
			}, SliderDirection.Vertical)
			{
				viewDataKey = "VerticalScroller",
				visible = false
			};
			this.verticalScroller.AddToClassList(ScrollView.vScrollerUssClassName);
			base.hierarchy.Add(this.verticalScroller);
			this.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			base.RegisterCallback<WheelEvent>(new EventCallback<WheelEvent>(this.OnScrollWheel), TrickleDown.NoTrickleDown);
			this.scrollOffset = Vector2.zero;
		}

		internal void SetScrollViewMode(ScrollViewMode scrollViewMode)
		{
			base.RemoveFromClassList(ScrollView.verticalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.horizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.verticalHorizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.scrollVariantUssClassName);
			switch (scrollViewMode)
			{
			case ScrollViewMode.Vertical:
				base.AddToClassList(ScrollView.verticalVariantUssClassName);
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				break;
			case ScrollViewMode.Horizontal:
				base.AddToClassList(ScrollView.horizontalVariantUssClassName);
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				break;
			case ScrollViewMode.VerticalAndHorizontal:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.verticalHorizontalVariantUssClassName);
				break;
			}
		}

		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				bool flag2 = evt.destinationPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.contentViewport.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
					this.contentViewport.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentViewport.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				}
			}
		}

		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				bool flag2 = evt.originPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.contentViewport.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
					this.contentViewport.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentViewport.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				}
			}
		}

		private void OnGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool flag2 = this.needsVertical;
				bool flag3 = this.needsHorizontal;
				bool flag4 = evt.layoutPass > 0;
				if (flag4)
				{
					flag2 = (flag2 || this.verticalScroller.visible);
					flag3 = (flag3 || this.horizontalScroller.visible);
				}
				this.UpdateScrollers(flag3, flag2);
				this.UpdateContentViewTransform();
			}
		}

		private static float ComputeElasticOffset(float deltaPointer, float initialScrollOffset, float lowLimit, float hardLowLimit, float highLimit, float hardHighLimit)
		{
			initialScrollOffset = Mathf.Max(initialScrollOffset, hardLowLimit * 0.95f);
			initialScrollOffset = Mathf.Min(initialScrollOffset, hardHighLimit * 0.95f);
			bool flag = initialScrollOffset < lowLimit && hardLowLimit < lowLimit;
			float num;
			float num3;
			if (flag)
			{
				num = lowLimit - hardLowLimit;
				float num2 = (lowLimit - initialScrollOffset) / num;
				num3 = num2 * num / (1f - num2);
				num3 += deltaPointer;
				initialScrollOffset = lowLimit;
			}
			else
			{
				bool flag2 = initialScrollOffset > highLimit && hardHighLimit > highLimit;
				if (flag2)
				{
					num = hardHighLimit - highLimit;
					float num4 = (initialScrollOffset - highLimit) / num;
					num3 = -1f * num4 * num / (1f - num4);
					num3 += deltaPointer;
					initialScrollOffset = highLimit;
				}
				else
				{
					num3 = deltaPointer;
				}
			}
			float num5 = initialScrollOffset - num3;
			bool flag3 = num5 < lowLimit;
			float num6;
			float result;
			if (flag3)
			{
				num3 = lowLimit - num5;
				initialScrollOffset = lowLimit;
				num = lowLimit - hardLowLimit;
				num6 = 1f;
			}
			else
			{
				bool flag4 = num5 <= highLimit;
				if (flag4)
				{
					result = num5;
					return result;
				}
				num3 = num5 - highLimit;
				initialScrollOffset = highLimit;
				num = hardHighLimit - highLimit;
				num6 = -1f;
			}
			bool flag5 = Mathf.Abs(num3) < Mathf.Epsilon;
			if (flag5)
			{
				result = initialScrollOffset;
			}
			else
			{
				float num7 = num3 / (num3 + num);
				num7 *= num;
				num7 *= num6;
				num5 = initialScrollOffset - num7;
				result = num5;
			}
			return result;
		}

		private void ComputeInitialSpringBackVelocity()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				bool flag2 = this.scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					this.m_SpringBackVelocity.x = this.m_LowBounds.x - this.scrollOffset.x;
				}
				else
				{
					bool flag3 = this.scrollOffset.x > this.m_HighBounds.x;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = this.m_HighBounds.x - this.scrollOffset.x;
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag4 = this.scrollOffset.y < this.m_LowBounds.y;
				if (flag4)
				{
					this.m_SpringBackVelocity.y = this.m_LowBounds.y - this.scrollOffset.y;
				}
				else
				{
					bool flag5 = this.scrollOffset.y > this.m_HighBounds.y;
					if (flag5)
					{
						this.m_SpringBackVelocity.y = this.m_HighBounds.y - this.scrollOffset.y;
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
			}
		}

		private void SpringBack()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				Vector2 scrollOffset = this.scrollOffset;
				bool flag2 = scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_LowBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
					bool flag3 = Mathf.Abs(this.m_SpringBackVelocity.x) < 1f;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				else
				{
					bool flag4 = scrollOffset.x > this.m_HighBounds.x;
					if (flag4)
					{
						scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_HighBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
						bool flag5 = Mathf.Abs(this.m_SpringBackVelocity.x) < 1f;
						if (flag5)
						{
							this.m_SpringBackVelocity.x = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag6 = scrollOffset.y < this.m_LowBounds.y;
				if (flag6)
				{
					scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_LowBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
					bool flag7 = Mathf.Abs(this.m_SpringBackVelocity.y) < 1f;
					if (flag7)
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				else
				{
					bool flag8 = scrollOffset.y > this.m_HighBounds.y;
					if (flag8)
					{
						scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_HighBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
						bool flag9 = Mathf.Abs(this.m_SpringBackVelocity.y) < 1f;
						if (flag9)
						{
							this.m_SpringBackVelocity.y = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				this.scrollOffset = scrollOffset;
			}
		}

		private void ApplyScrollInertia()
		{
			bool flag = this.hasInertia && this.m_Velocity != Vector2.zero;
			if (flag)
			{
				this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, Time.unscaledDeltaTime);
				bool flag2 = Mathf.Abs(this.m_Velocity.x) < 1f || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.x < this.m_LowBounds.x || this.scrollOffset.x > this.m_HighBounds.x));
				if (flag2)
				{
					this.m_Velocity.x = 0f;
				}
				bool flag3 = Mathf.Abs(this.m_Velocity.y) < 1f || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.y < this.m_LowBounds.y || this.scrollOffset.y > this.m_HighBounds.y));
				if (flag3)
				{
					this.m_Velocity.y = 0f;
				}
				this.scrollOffset += this.m_Velocity * Time.unscaledDeltaTime;
			}
			else
			{
				this.m_Velocity = Vector2.zero;
			}
		}

		private void PostPointerUpAnimation()
		{
			this.ApplyScrollInertia();
			this.SpringBack();
			bool flag = this.m_SpringBackVelocity == Vector2.zero && this.m_Velocity == Vector2.zero;
			if (flag)
			{
				this.m_PostPointerUpAnimation.Pause();
			}
		}

		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = evt.pointerType != PointerType.mouse && evt.isPrimary && this.m_ScrollingPointerId == PointerId.invalidPointerId;
			if (flag)
			{
				IVisualElementScheduledItem expr_39 = this.m_PostPointerUpAnimation;
				if (expr_39 != null)
				{
					expr_39.Pause();
				}
				this.m_ScrollingPointerId = evt.pointerId;
				this.m_PointerStartPosition = evt.position;
				this.m_StartPosition = this.scrollOffset;
				this.m_Velocity = Vector2.zero;
				this.m_SpringBackVelocity = Vector2.zero;
				this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				evt.StopPropagation();
			}
		}

		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.pointerId == this.m_ScrollingPointerId;
			if (flag)
			{
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
				Vector2 vector;
				if (flag2)
				{
					vector = this.m_StartPosition - (new Vector2(evt.position.x, evt.position.y) - this.m_PointerStartPosition);
					vector = Vector2.Max(vector, this.m_LowBounds);
					vector = Vector2.Min(vector, this.m_HighBounds);
				}
				else
				{
					bool flag3 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
					if (flag3)
					{
						Vector2 vector2 = new Vector2(evt.position.x, evt.position.y) - this.m_PointerStartPosition;
						vector.x = ScrollView.ComputeElasticOffset(vector2.x, this.m_StartPosition.x, this.m_LowBounds.x, this.m_LowBounds.x - this.contentViewport.resolvedStyle.width, this.m_HighBounds.x, this.m_HighBounds.x + this.contentViewport.resolvedStyle.width);
						vector.y = ScrollView.ComputeElasticOffset(vector2.y, this.m_StartPosition.y, this.m_LowBounds.y, this.m_LowBounds.y - this.contentViewport.resolvedStyle.height, this.m_HighBounds.y, this.m_HighBounds.y + this.contentViewport.resolvedStyle.height);
					}
					else
					{
						vector = this.m_StartPosition - (new Vector2(evt.position.x, evt.position.y) - this.m_PointerStartPosition);
					}
				}
				bool hasInertia = this.hasInertia;
				if (hasInertia)
				{
					float unscaledDeltaTime = Time.unscaledDeltaTime;
					Vector2 b = (vector - this.scrollOffset) / unscaledDeltaTime;
					this.m_Velocity = Vector2.Lerp(this.m_Velocity, b, unscaledDeltaTime * 10f);
				}
				this.scrollOffset = vector;
				evt.currentTarget.CapturePointer(evt.pointerId);
				evt.StopPropagation();
			}
		}

		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = evt.pointerId == this.m_ScrollingPointerId;
			if (flag)
			{
				evt.currentTarget.ReleasePointer(evt.pointerId);
				evt.StopPropagation();
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic || this.hasInertia;
				if (flag2)
				{
					this.ComputeInitialSpringBackVelocity();
					bool flag3 = this.m_PostPointerUpAnimation == null;
					if (flag3)
					{
						this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(30L);
					}
					else
					{
						this.m_PostPointerUpAnimation.Resume();
					}
				}
				this.m_ScrollingPointerId = PointerId.invalidPointerId;
			}
		}

		private void UpdateScrollers(bool displayHorizontal, bool displayVertical)
		{
			float factor = (this.contentContainer.layout.width > Mathf.Epsilon) ? (this.contentViewport.layout.width / this.contentContainer.layout.width) : 1f;
			float factor2 = (this.contentContainer.layout.height > Mathf.Epsilon) ? (this.contentViewport.layout.height / this.contentContainer.layout.height) : 1f;
			this.horizontalScroller.Adjust(factor);
			this.verticalScroller.Adjust(factor2);
			this.horizontalScroller.SetEnabled(this.contentContainer.layout.width - this.contentViewport.layout.width > 0f);
			this.verticalScroller.SetEnabled(this.contentContainer.layout.height - this.contentViewport.layout.height > 0f);
			this.contentViewport.style.marginRight = (displayVertical ? this.verticalScroller.layout.width : 0f);
			this.horizontalScroller.style.right = (displayVertical ? this.verticalScroller.layout.width : 0f);
			this.contentViewport.style.marginBottom = (displayHorizontal ? this.horizontalScroller.layout.height : 0f);
			this.verticalScroller.style.bottom = (displayHorizontal ? this.horizontalScroller.layout.height : 0f);
			bool flag = displayHorizontal && this.scrollableWidth > 0f;
			if (flag)
			{
				this.horizontalScroller.lowValue = 0f;
				this.horizontalScroller.highValue = this.scrollableWidth;
			}
			else
			{
				this.horizontalScroller.value = 0f;
			}
			bool flag2 = displayVertical && this.scrollableHeight > 0f;
			if (flag2)
			{
				this.verticalScroller.lowValue = 0f;
				this.verticalScroller.highValue = this.scrollableHeight;
			}
			else
			{
				this.verticalScroller.value = 0f;
			}
			bool flag3 = this.horizontalScroller.visible != displayHorizontal;
			if (flag3)
			{
				this.horizontalScroller.visible = displayHorizontal;
			}
			bool flag4 = this.verticalScroller.visible != displayVertical;
			if (flag4)
			{
				this.verticalScroller.visible = displayVertical;
			}
		}

		private void OnScrollWheel(WheelEvent evt)
		{
			float value = this.verticalScroller.value;
			bool flag = this.contentContainer.layout.height - base.layout.height > 0f;
			if (flag)
			{
				bool flag2 = evt.delta.y < 0f;
				if (flag2)
				{
					this.verticalScroller.ScrollPageUp(Mathf.Abs(evt.delta.y));
				}
				else
				{
					bool flag3 = evt.delta.y > 0f;
					if (flag3)
					{
						this.verticalScroller.ScrollPageDown(Mathf.Abs(evt.delta.y));
					}
				}
			}
			bool flag4 = this.verticalScroller.value != value;
			if (flag4)
			{
				evt.StopPropagation();
			}
		}
	}
}
