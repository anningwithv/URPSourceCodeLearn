using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal static class VisualElementFactoryRegistry
	{
		private static Dictionary<string, List<IUxmlFactory>> s_Factories;

		internal static Dictionary<string, List<IUxmlFactory>> factories
		{
			get
			{
				bool flag = VisualElementFactoryRegistry.s_Factories == null;
				if (flag)
				{
					VisualElementFactoryRegistry.s_Factories = new Dictionary<string, List<IUxmlFactory>>();
					VisualElementFactoryRegistry.RegisterEngineFactories();
				}
				return VisualElementFactoryRegistry.s_Factories;
			}
		}

		internal static void RegisterFactory(IUxmlFactory factory)
		{
			List<IUxmlFactory> list;
			bool flag = VisualElementFactoryRegistry.factories.TryGetValue(factory.uxmlQualifiedName, out list);
			if (flag)
			{
				foreach (IUxmlFactory current in list)
				{
					bool flag2 = current.GetType() == factory.GetType();
					if (flag2)
					{
						throw new ArgumentException("A factory for the type " + factory.GetType().FullName + " was already registered");
					}
				}
				list.Add(factory);
			}
			else
			{
				list = new List<IUxmlFactory>();
				list.Add(factory);
				VisualElementFactoryRegistry.factories.Add(factory.uxmlQualifiedName, list);
			}
		}

		internal static bool TryGetValue(string fullTypeName, out List<IUxmlFactory> factoryList)
		{
			factoryList = null;
			return VisualElementFactoryRegistry.factories.TryGetValue(fullTypeName, out factoryList);
		}

		private static void RegisterEngineFactories()
		{
			IUxmlFactory[] array = new IUxmlFactory[]
			{
				new UxmlRootElementFactory(),
				new UxmlTemplateFactory(),
				new UxmlStyleFactory(),
				new UxmlAttributeOverridesFactory(),
				new Button.UxmlFactory(),
				new VisualElement.UxmlFactory(),
				new IMGUIContainer.UxmlFactory(),
				new Image.UxmlFactory(),
				new Label.UxmlFactory(),
				new RepeatButton.UxmlFactory(),
				new ScrollView.UxmlFactory(),
				new Scroller.UxmlFactory(),
				new Slider.UxmlFactory(),
				new SliderInt.UxmlFactory(),
				new MinMaxSlider.UxmlFactory(),
				new Toggle.UxmlFactory(),
				new TextField.UxmlFactory(),
				new TemplateContainer.UxmlFactory(),
				new Box.UxmlFactory(),
				new HelpBox.UxmlFactory(),
				new PopupWindow.UxmlFactory(),
				new ListView.UxmlFactory(),
				new TwoPaneSplitView.UxmlFactory(),
				new TreeView.UxmlFactory(),
				new Foldout.UxmlFactory(),
				new BindableElement.UxmlFactory()
			};
			IUxmlFactory[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				IUxmlFactory factory = array2[i];
				VisualElementFactoryRegistry.RegisterFactory(factory);
			}
		}
	}
}
