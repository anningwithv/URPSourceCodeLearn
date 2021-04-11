using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	[Serializable]
	public class VisualTreeAsset : ScriptableObject
	{
		[Serializable]
		internal struct UsingEntry
		{
			internal static readonly IComparer<VisualTreeAsset.UsingEntry> comparer = new VisualTreeAsset.UsingEntryComparer();

			[SerializeField]
			public string alias;

			[SerializeField]
			public string path;

			[SerializeField]
			public VisualTreeAsset asset;

			public UsingEntry(string alias, string path)
			{
				this.alias = alias;
				this.path = path;
				this.asset = null;
			}

			public UsingEntry(string alias, VisualTreeAsset asset)
			{
				this.alias = alias;
				this.path = null;
				this.asset = asset;
			}
		}

		private class UsingEntryComparer : IComparer<VisualTreeAsset.UsingEntry>
		{
			public int Compare(VisualTreeAsset.UsingEntry x, VisualTreeAsset.UsingEntry y)
			{
				return string.CompareOrdinal(x.alias, y.alias);
			}
		}

		[Serializable]
		internal struct SlotDefinition
		{
			[SerializeField]
			public string name;

			[SerializeField]
			public int insertionPointId;
		}

		[Serializable]
		internal struct SlotUsageEntry
		{
			[SerializeField]
			public string slotName;

			[SerializeField]
			public int assetId;

			public SlotUsageEntry(string slotName, int assetId)
			{
				this.slotName = slotName;
				this.assetId = assetId;
			}
		}

		private static readonly Dictionary<string, VisualElement> s_TemporarySlotInsertionPoints = new Dictionary<string, VisualElement>();

		[SerializeField]
		private List<VisualTreeAsset.UsingEntry> m_Usings;

		[SerializeField]
		internal StyleSheet inlineSheet;

		[SerializeField]
		private List<VisualElementAsset> m_VisualElementAssets;

		[SerializeField]
		private List<TemplateAsset> m_TemplateAssets;

		[SerializeField]
		private List<VisualTreeAsset.SlotDefinition> m_Slots;

		[SerializeField]
		private int m_ContentContainerId;

		[SerializeField]
		private int m_ContentHash;

		public IEnumerable<VisualTreeAsset> templateDependencies
		{
			get
			{
				VisualTreeAsset.<get_templateDependencies>d__8 <get_templateDependencies>d__ = new VisualTreeAsset.<get_templateDependencies>d__8(-2);
				<get_templateDependencies>d__.<>4__this = this;
				return <get_templateDependencies>d__;
			}
		}

		public IEnumerable<StyleSheet> stylesheets
		{
			get
			{
				VisualTreeAsset.<get_stylesheets>d__12 <get_stylesheets>d__ = new VisualTreeAsset.<get_stylesheets>d__12(-2);
				<get_stylesheets>d__.<>4__this = this;
				return <get_stylesheets>d__;
			}
		}

		internal List<VisualElementAsset> visualElementAssets
		{
			get
			{
				return this.m_VisualElementAssets;
			}
			set
			{
				this.m_VisualElementAssets = value;
			}
		}

		internal List<TemplateAsset> templateAssets
		{
			get
			{
				return this.m_TemplateAssets;
			}
			set
			{
				this.m_TemplateAssets = value;
			}
		}

		internal List<VisualTreeAsset.SlotDefinition> slots
		{
			get
			{
				return this.m_Slots;
			}
			set
			{
				this.m_Slots = value;
			}
		}

		internal int contentContainerId
		{
			get
			{
				return this.m_ContentContainerId;
			}
			set
			{
				this.m_ContentContainerId = value;
			}
		}

		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		internal int GetNextChildSerialNumber()
		{
			List<VisualElementAsset> expr_07 = this.m_VisualElementAssets;
			int num = (expr_07 != null) ? expr_07.Count : 0;
			int arg_27_0 = num;
			List<TemplateAsset> expr_1B = this.m_TemplateAssets;
			return arg_27_0 + ((expr_1B != null) ? expr_1B.Count : 0);
		}

		public TemplateContainer Instantiate()
		{
			TemplateContainer templateContainer = new TemplateContainer(base.name);
			try
			{
				this.CloneTree(templateContainer, VisualTreeAsset.s_TemporarySlotInsertionPoints, null);
			}
			finally
			{
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
			}
			return templateContainer;
		}

		public TemplateContainer Instantiate(string bindingPath)
		{
			TemplateContainer templateContainer = this.Instantiate();
			templateContainer.bindingPath = bindingPath;
			return templateContainer;
		}

		public TemplateContainer CloneTree()
		{
			return this.Instantiate();
		}

		public TemplateContainer CloneTree(string bindingPath)
		{
			return this.Instantiate(bindingPath);
		}

		public void CloneTree(VisualElement target)
		{
			int num;
			int num2;
			this.CloneTree(target, out num, out num2);
		}

		public void CloneTree(VisualElement target, out int firstElementIndex, out int elementAddedCount)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			firstElementIndex = target.childCount;
			try
			{
				this.CloneTree(target, VisualTreeAsset.s_TemporarySlotInsertionPoints, null);
			}
			finally
			{
				elementAddedCount = target.childCount - firstElementIndex;
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
			}
		}

		internal void CloneTree(VisualElement target, Dictionary<string, VisualElement> slotInsertionPoints, List<TemplateAsset.AttributeOverride> attributeOverrides)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = (this.visualElementAssets == null || this.visualElementAssets.Count <= 0) && (this.templateAssets == null || this.templateAssets.Count <= 0);
			if (!flag2)
			{
				Dictionary<int, List<VisualElementAsset>> dictionary = new Dictionary<int, List<VisualElementAsset>>();
				int num = (this.visualElementAssets == null) ? 0 : this.visualElementAssets.Count;
				int num2 = (this.templateAssets == null) ? 0 : this.templateAssets.Count;
				for (int i = 0; i < num + num2; i++)
				{
					VisualElementAsset visualElementAsset = (i < num) ? this.visualElementAssets[i] : this.templateAssets[i - num];
					List<VisualElementAsset> list;
					bool flag3 = !dictionary.TryGetValue(visualElementAsset.parentId, out list);
					if (flag3)
					{
						list = new List<VisualElementAsset>();
						dictionary.Add(visualElementAsset.parentId, list);
					}
					list.Add(visualElementAsset);
				}
				List<VisualElementAsset> list2;
				dictionary.TryGetValue(0, out list2);
				bool flag4 = list2 == null || list2.Count == 0;
				if (!flag4)
				{
					Debug.Assert(list2.Count == 1);
					VisualElementAsset visualElementAsset2 = list2[0];
					VisualTreeAsset.AssignClassListFromAssetToElement(visualElementAsset2, target);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(visualElementAsset2, target);
					list2.Clear();
					dictionary.TryGetValue(visualElementAsset2.id, out list2);
					bool flag5 = list2 == null || list2.Count == 0;
					if (!flag5)
					{
						list2.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						foreach (VisualElementAsset current in list2)
						{
							Assert.IsNotNull<VisualElementAsset>(current);
							VisualElement visualElement = this.CloneSetupRecursively(current, dictionary, new CreationContext(slotInsertionPoints, attributeOverrides, this, target));
							bool flag6 = visualElement != null;
							if (flag6)
							{
								target.hierarchy.Add(visualElement);
							}
							else
							{
								Debug.LogWarning("VisualTreeAsset instantiated an empty UI. Check the syntax of your UXML document.");
							}
						}
					}
				}
			}
		}

		private VisualElement CloneSetupRecursively(VisualElementAsset root, Dictionary<int, List<VisualElementAsset>> idToChildren, CreationContext context)
		{
			VisualElement visualElement = VisualTreeAsset.Create(root, context);
			bool flag = visualElement == null;
			VisualElement result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = root.id == context.visualTreeAsset.contentContainerId;
				if (flag2)
				{
					bool flag3 = context.target is TemplateContainer;
					if (flag3)
					{
						((TemplateContainer)context.target).SetContentContainer(visualElement);
					}
					else
					{
						Debug.LogError("Trying to clone a VisualTreeAsset with a custom content container into a element which is not a template container");
					}
				}
				string key;
				bool flag4 = context.slotInsertionPoints != null && this.TryGetSlotInsertionPoint(root.id, out key);
				if (flag4)
				{
					context.slotInsertionPoints.Add(key, visualElement);
				}
				bool flag5 = root.ruleIndex != -1;
				if (flag5)
				{
					bool flag6 = this.inlineSheet == null;
					if (flag6)
					{
						Debug.LogWarning("VisualElementAsset has a RuleIndex but no inlineStyleSheet");
					}
					else
					{
						StyleRule rule = this.inlineSheet.rules[root.ruleIndex];
						visualElement.SetInlineRule(this.inlineSheet, rule);
					}
				}
				TemplateAsset templateAsset = root as TemplateAsset;
				List<VisualElementAsset> list;
				bool flag7 = idToChildren.TryGetValue(root.id, out list);
				if (flag7)
				{
					list.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
					using (List<VisualElementAsset>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							VisualElementAsset childVea = enumerator.Current;
							VisualElement visualElement2 = this.CloneSetupRecursively(childVea, idToChildren, context);
							bool flag8 = visualElement2 == null;
							if (!flag8)
							{
								bool flag9 = templateAsset == null;
								if (flag9)
								{
									visualElement.Add(visualElement2);
								}
								else
								{
									int num = (templateAsset.slotUsages == null) ? -1 : templateAsset.slotUsages.FindIndex((VisualTreeAsset.SlotUsageEntry u) => u.assetId == childVea.id);
									bool flag10 = num != -1;
									if (flag10)
									{
										string slotName = templateAsset.slotUsages[num].slotName;
										Assert.IsFalse(string.IsNullOrEmpty(slotName), "a lost name should not be null or empty, this probably points to an importer or serialization bug");
										VisualElement visualElement3;
										bool flag11 = context.slotInsertionPoints == null || !context.slotInsertionPoints.TryGetValue(slotName, out visualElement3);
										if (flag11)
										{
											Debug.LogErrorFormat("Slot '{0}' was not found. Existing slots: {1}", new object[]
											{
												slotName,
												(context.slotInsertionPoints == null) ? string.Empty : string.Join(", ", context.slotInsertionPoints.Keys.ToArray<string>())
											});
											visualElement.Add(visualElement2);
										}
										else
										{
											visualElement3.Add(visualElement2);
										}
									}
									else
									{
										visualElement.Add(visualElement2);
									}
								}
							}
						}
					}
				}
				bool flag12 = templateAsset != null && context.slotInsertionPoints != null;
				if (flag12)
				{
					context.slotInsertionPoints.Clear();
				}
				result = visualElement;
			}
			return result;
		}

		private static int CompareForOrder(VisualElementAsset a, VisualElementAsset b)
		{
			return a.orderInDocument.CompareTo(b.orderInDocument);
		}

		internal bool SlotDefinitionExists(string slotName)
		{
			bool flag = this.m_Slots == null;
			return !flag && this.m_Slots.Exists((VisualTreeAsset.SlotDefinition s) => s.name == slotName);
		}

		internal bool AddSlotDefinition(string slotName, int resId)
		{
			bool flag = this.SlotDefinitionExists(slotName);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.m_Slots == null;
				if (flag2)
				{
					this.m_Slots = new List<VisualTreeAsset.SlotDefinition>(1);
				}
				this.m_Slots.Add(new VisualTreeAsset.SlotDefinition
				{
					insertionPointId = resId,
					name = slotName
				});
				result = true;
			}
			return result;
		}

		internal bool TryGetSlotInsertionPoint(int insertionPointId, out string slotName)
		{
			bool flag = this.m_Slots == null;
			bool result;
			if (flag)
			{
				slotName = null;
				result = false;
			}
			else
			{
				for (int i = 0; i < this.m_Slots.Count; i++)
				{
					VisualTreeAsset.SlotDefinition slotDefinition = this.m_Slots[i];
					bool flag2 = slotDefinition.insertionPointId == insertionPointId;
					if (flag2)
					{
						slotName = slotDefinition.name;
						result = true;
						return result;
					}
				}
				slotName = null;
				result = false;
			}
			return result;
		}

		internal VisualTreeAsset ResolveTemplate(string templateName)
		{
			bool flag = this.m_Usings == null || this.m_Usings.Count == 0;
			VisualTreeAsset result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = this.m_Usings.BinarySearch(new VisualTreeAsset.UsingEntry(templateName, string.Empty), VisualTreeAsset.UsingEntry.comparer);
				bool flag2 = num < 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = this.m_Usings[num].asset;
					if (flag3)
					{
						result = this.m_Usings[num].asset;
					}
					else
					{
						string path = this.m_Usings[num].path;
						result = (Panel.LoadResource(path, typeof(VisualTreeAsset), GUIUtility.pixelsPerPoint) as VisualTreeAsset);
					}
				}
			}
			return result;
		}

		internal bool TemplateExists(string templateName)
		{
			bool flag = this.m_Usings == null || this.m_Usings.Count == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = this.m_Usings.BinarySearch(new VisualTreeAsset.UsingEntry(templateName, string.Empty), VisualTreeAsset.UsingEntry.comparer);
				result = (num >= 0);
			}
			return result;
		}

		internal void RegisterTemplate(string templateName, string path)
		{
			this.InsertUsingEntry(new VisualTreeAsset.UsingEntry(templateName, path));
		}

		internal void RegisterTemplate(string templateName, VisualTreeAsset asset)
		{
			this.InsertUsingEntry(new VisualTreeAsset.UsingEntry(templateName, asset));
		}

		private void InsertUsingEntry(VisualTreeAsset.UsingEntry entry)
		{
			bool flag = this.m_Usings == null;
			if (flag)
			{
				this.m_Usings = new List<VisualTreeAsset.UsingEntry>();
			}
			int num = 0;
			while (num < this.m_Usings.Count && string.CompareOrdinal(entry.alias, this.m_Usings[num].alias) > 0)
			{
				num++;
			}
			this.m_Usings.Insert(num, entry);
		}

		internal static VisualElement Create(VisualElementAsset asset, CreationContext ctx)
		{
			List<IUxmlFactory> list;
			bool flag = !VisualElementFactoryRegistry.TryGetValue(asset.fullTypeName, out list);
			VisualElement result;
			if (flag)
			{
				bool flag2 = asset.fullTypeName.StartsWith("UnityEngine.Experimental.UIElements.") || asset.fullTypeName.StartsWith("UnityEditor.Experimental.UIElements.");
				if (flag2)
				{
					string fullTypeName = asset.fullTypeName.Replace(".Experimental.UIElements", ".UIElements");
					bool flag3 = !VisualElementFactoryRegistry.TryGetValue(fullTypeName, out list);
					if (flag3)
					{
						Debug.LogErrorFormat("Element '{0}' has no registered factory method.", new object[]
						{
							asset.fullTypeName
						});
						result = new Label(string.Format("Unknown type: '{0}'", asset.fullTypeName));
						return result;
					}
				}
				else
				{
					bool flag4 = asset.fullTypeName == "UXML";
					if (!flag4)
					{
						Debug.LogErrorFormat("Element '{0}' has no registered factory method.", new object[]
						{
							asset.fullTypeName
						});
						result = new Label(string.Format("Unknown type: '{0}'", asset.fullTypeName));
						return result;
					}
					VisualElementFactoryRegistry.TryGetValue(typeof(UxmlRootElementFactory).Namespace + "." + asset.fullTypeName, out list);
				}
			}
			IUxmlFactory uxmlFactory = null;
			foreach (IUxmlFactory current in list)
			{
				bool flag5 = current.AcceptsAttributeBag(asset, ctx);
				if (flag5)
				{
					uxmlFactory = current;
					break;
				}
			}
			bool flag6 = uxmlFactory == null;
			if (flag6)
			{
				Debug.LogErrorFormat("Element '{0}' has a no factory that accept the set of XML attributes specified.", new object[]
				{
					asset.fullTypeName
				});
				result = new Label(string.Format("Type with no factory: '{0}'", asset.fullTypeName));
			}
			else
			{
				VisualElement visualElement = uxmlFactory.Create(asset, ctx);
				bool flag7 = visualElement != null;
				if (flag7)
				{
					VisualTreeAsset.AssignClassListFromAssetToElement(asset, visualElement);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(asset, visualElement);
				}
				result = visualElement;
			}
			return result;
		}

		private static void AssignClassListFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool flag = asset.classes != null;
			if (flag)
			{
				for (int i = 0; i < asset.classes.Length; i++)
				{
					element.AddToClassList(asset.classes[i]);
				}
			}
		}

		private static void AssignStyleSheetFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool hasStylesheetPaths = asset.hasStylesheetPaths;
			if (hasStylesheetPaths)
			{
				for (int i = 0; i < asset.stylesheetPaths.Count; i++)
				{
					element.AddStyleSheetPath(asset.stylesheetPaths[i]);
				}
			}
			bool hasStylesheets = asset.hasStylesheets;
			if (hasStylesheets)
			{
				for (int j = 0; j < asset.stylesheets.Count; j++)
				{
					bool flag = asset.stylesheets[j] != null;
					if (flag)
					{
						element.styleSheets.Add(asset.stylesheets[j]);
					}
				}
			}
		}
	}
}
