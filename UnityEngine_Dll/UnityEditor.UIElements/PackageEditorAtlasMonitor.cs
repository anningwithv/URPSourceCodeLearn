using System;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.UIR;

namespace UnityEditor.UIElements
{
	internal static class PackageEditorAtlasMonitor
	{
		private class TexturePostProcessor
		{
			public static int importedTexturesCount;

			public static int importedVectorImagesCount;

			public TexturePostProcessor()
			{
				EditorAtlasMonitorBridge.OnPostprocessTexture = new Action<Texture2D>(this.OnPostprocessTexture);
				EditorAtlasMonitorBridge.OnPostprocessAllAssets = new Action<string[], string[], string[], string[]>(PackageEditorAtlasMonitor.TexturePostProcessor.OnPostprocessAllAssets);
			}

			public void OnPostprocessTexture(Texture2D texture)
			{
				PackageEditorAtlasMonitor.TexturePostProcessor.importedTexturesCount++;
			}

			private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
			{
				for (int i = 0; i < importedAssets.Length; i++)
				{
					string path = importedAssets[i];
					bool flag = Path.GetExtension(path) == ".svg";
					if (flag)
					{
						PackageEditorAtlasMonitor.TexturePostProcessor.importedVectorImagesCount++;
					}
				}
			}
		}

		private static PackageEditorAtlasMonitor.TexturePostProcessor s_TexturePostProcessor = new PackageEditorAtlasMonitor.TexturePostProcessor();

		private static ColorSpace m_LastColorSpace;

		private static int m_LastImportedTexturesCount;

		private static int m_LastImportedVectorImagesCount;

		public static void StaticInit()
		{
			RenderChain.OnPreRender = (Action)Delegate.Combine(RenderChain.OnPreRender, new Action(PackageEditorAtlasMonitor.OnPreRender));
		}

		public static void OnPreRender()
		{
			bool flag = PackageEditorAtlasMonitor.CheckForColorSpaceChange();
			bool flag2 = PackageEditorAtlasMonitor.CheckForImportedTextures();
			bool flag3 = PackageEditorAtlasMonitor.CheckForImportedVectorImages();
			bool flag4 = flag | flag2;
			if (flag4)
			{
				UIRAtlasManager.MarkAllForReset();
				VectorImageManager.MarkAllForReset();
			}
			else
			{
				bool flag5 = flag | flag3;
				if (flag5)
				{
					VectorImageManager.MarkAllForReset();
				}
			}
		}

		private static bool CheckForColorSpaceChange()
		{
			ColorSpace activeColorSpace = QualitySettings.activeColorSpace;
			bool flag = PackageEditorAtlasMonitor.m_LastColorSpace == activeColorSpace;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				PackageEditorAtlasMonitor.m_LastColorSpace = activeColorSpace;
				result = true;
			}
			return result;
		}

		private static bool CheckForImportedTextures()
		{
			int importedTexturesCount = PackageEditorAtlasMonitor.TexturePostProcessor.importedTexturesCount;
			bool flag = PackageEditorAtlasMonitor.m_LastImportedTexturesCount == importedTexturesCount;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				PackageEditorAtlasMonitor.m_LastImportedTexturesCount = importedTexturesCount;
				result = true;
			}
			return result;
		}

		private static bool CheckForImportedVectorImages()
		{
			int importedVectorImagesCount = PackageEditorAtlasMonitor.TexturePostProcessor.importedVectorImagesCount;
			bool flag = PackageEditorAtlasMonitor.m_LastImportedVectorImagesCount == importedVectorImagesCount;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				PackageEditorAtlasMonitor.m_LastImportedVectorImagesCount = importedVectorImagesCount;
				result = true;
			}
			return result;
		}
	}
}
