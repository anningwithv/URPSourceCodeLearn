using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.UIElements.UIR.Implementation;

namespace UnityEngine.UIElements.UIR
{
	internal class RenderChain : IDisposable
	{
		private struct DepthOrderedDirtyTracking
		{
			public List<VisualElement> heads;

			public List<VisualElement> tails;

			public int[] minDepths;

			public int[] maxDepths;

			public uint dirtyID;

			public void EnsureFits(int maxDepth)
			{
				while (this.heads.Count <= maxDepth)
				{
					this.heads.Add(null);
					this.tails.Add(null);
				}
			}

			public void RegisterDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypes, int dirtyTypeClassIndex)
			{
				Debug.Assert(dirtyTypes > RenderDataDirtyTypes.None);
				int hierarchyDepth = ve.renderChainData.hierarchyDepth;
				this.minDepths[dirtyTypeClassIndex] = ((hierarchyDepth < this.minDepths[dirtyTypeClassIndex]) ? hierarchyDepth : this.minDepths[dirtyTypeClassIndex]);
				this.maxDepths[dirtyTypeClassIndex] = ((hierarchyDepth > this.maxDepths[dirtyTypeClassIndex]) ? hierarchyDepth : this.maxDepths[dirtyTypeClassIndex]);
				bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
				if (flag)
				{
					ve.renderChainData.dirtiedValues = (ve.renderChainData.dirtiedValues | dirtyTypes);
				}
				else
				{
					ve.renderChainData.dirtiedValues = dirtyTypes;
					bool flag2 = this.tails[hierarchyDepth] != null;
					if (flag2)
					{
						this.tails[hierarchyDepth].renderChainData.nextDirty = ve;
						ve.renderChainData.prevDirty = this.tails[hierarchyDepth];
						this.tails[hierarchyDepth] = ve;
					}
					else
					{
						List<VisualElement> arg_EF_0 = this.heads;
						int arg_EF_1 = hierarchyDepth;
						this.tails[hierarchyDepth] = ve;
						arg_EF_0[arg_EF_1] = ve;
					}
				}
			}

			public void ClearDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypesInverse)
			{
				Debug.Assert(ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None);
				ve.renderChainData.dirtiedValues = (ve.renderChainData.dirtiedValues & dirtyTypesInverse);
				bool flag = ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None;
				if (flag)
				{
					bool flag2 = ve.renderChainData.prevDirty != null;
					if (flag2)
					{
						ve.renderChainData.prevDirty.renderChainData.nextDirty = ve.renderChainData.nextDirty;
					}
					bool flag3 = ve.renderChainData.nextDirty != null;
					if (flag3)
					{
						ve.renderChainData.nextDirty.renderChainData.prevDirty = ve.renderChainData.prevDirty;
					}
					bool flag4 = this.tails[ve.renderChainData.hierarchyDepth] == ve;
					if (flag4)
					{
						Debug.Assert(ve.renderChainData.nextDirty == null);
						this.tails[ve.renderChainData.hierarchyDepth] = ve.renderChainData.prevDirty;
					}
					bool flag5 = this.heads[ve.renderChainData.hierarchyDepth] == ve;
					if (flag5)
					{
						Debug.Assert(ve.renderChainData.prevDirty == null);
						this.heads[ve.renderChainData.hierarchyDepth] = ve.renderChainData.nextDirty;
					}
					ve.renderChainData.prevDirty = (ve.renderChainData.nextDirty = null);
				}
			}

			public void Reset()
			{
				for (int i = 0; i < this.minDepths.Length; i++)
				{
					this.minDepths[i] = 2147483647;
					this.maxDepths[i] = -2147483648;
				}
			}
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		private struct RenderChainStaticIndexAllocator
		{
			private static List<RenderChain> renderChains = new List<RenderChain>(4);

			public static int AllocateIndex(RenderChain renderChain)
			{
				int num = RenderChain.RenderChainStaticIndexAllocator.renderChains.IndexOf(null);
				bool flag = num >= 0;
				if (flag)
				{
					RenderChain.RenderChainStaticIndexAllocator.renderChains[num] = renderChain;
				}
				else
				{
					num = RenderChain.RenderChainStaticIndexAllocator.renderChains.Count;
					RenderChain.RenderChainStaticIndexAllocator.renderChains.Add(renderChain);
				}
				return num;
			}

			public static void FreeIndex(int index)
			{
				RenderChain.RenderChainStaticIndexAllocator.renderChains[index] = null;
			}

			public static RenderChain AccessIndex(int index)
			{
				return RenderChain.RenderChainStaticIndexAllocator.renderChains[index];
			}
		}

		private struct RenderNodeData
		{
			public Material standardMaterial;

			public Material initialMaterial;

			public MaterialPropertyBlock matPropBlock;

			public RenderChainCommand firstCommand;

			public UIRenderDevice device;

			public Texture atlas;

			public Texture vectorAtlas;

			public Texture shaderInfoAtlas;

			public float dpiScale;

			public NativeSlice<Transform3x4> transformConstants;

			public NativeSlice<Vector4> clipRectConstants;
		}

		private struct RenderDeviceRestoreInfo
		{
			public IPanel panel;

			public VisualElement root;

			public bool hasAtlasMan;

			public bool hasVectorImageMan;
		}

		private RenderChainCommand m_FirstCommand;

		private RenderChain.DepthOrderedDirtyTracking m_DirtyTracker;

		private Pool<RenderChainCommand> m_CommandPool = new Pool<RenderChainCommand>();

		private List<RenderChain.RenderNodeData> m_RenderNodesData = new List<RenderChain.RenderNodeData>();

		private Shader m_DefaultShader;

		private Shader m_DefaultWorldSpaceShader;

		private Material m_DefaultMat;

		private Material m_DefaultWorldSpaceMat;

		private bool m_BlockDirtyRegistration;

		private bool m_DrawInCameras;

		private int m_StaticIndex = -1;

		private int m_ActiveRenderNodes = 0;

		private int m_CustomMaterialCommands = 0;

		private ChainBuilderStats m_Stats;

		private uint m_StatsElementsAdded;

		private uint m_StatsElementsRemoved;

		private VisualElement m_FirstTextElement;

		private UIRTextUpdatePainter m_TextUpdatePainter;

		private int m_TextElementCount;

		private int m_DirtyTextStartIndex;

		private int m_DirtyTextRemaining;

		private bool m_FontWasReset;

		private Dictionary<VisualElement, Vector2> m_LastGroupTransformElementScale = new Dictionary<VisualElement, Vector2>();

		private static ProfilerMarker s_MarkerProcess;

		private static ProfilerMarker s_MarkerRender;

		private static ProfilerMarker s_MarkerClipProcessing;

		private static ProfilerMarker s_MarkerOpacityProcessing;

		private static ProfilerMarker s_MarkerTransformProcessing;

		private static ProfilerMarker s_MarkerVisualsProcessing;

		private static ProfilerMarker s_MarkerTextRegen;

		internal static Action OnPreRender;

		internal UIRVEShaderInfoAllocator shaderInfoAllocator;

		private RenderChain.RenderDeviceRestoreInfo m_RenderDeviceRestoreInfo;

		internal RenderChainCommand firstCommand
		{
			get
			{
				return this.m_FirstCommand;
			}
		}

		protected bool disposed
		{
			get;
			private set;
		}

		internal ChainBuilderStats stats
		{
			get
			{
				return this.m_Stats;
			}
		}

		internal IPanel panel
		{
			get;
			private set;
		}

		internal UIRenderDevice device
		{
			get;
			private set;
		}

		internal UIRAtlasManager atlasManager
		{
			get;
			private set;
		}

		internal VectorImageManager vectorImageManager
		{
			get;
			private set;
		}

		internal UIRStylePainter painter
		{
			get;
			private set;
		}

		internal bool drawStats
		{
			get;
			set;
		}

		internal bool drawInCameras
		{
			get
			{
				return this.m_DrawInCameras;
			}
			set
			{
				bool flag = this.m_DrawInCameras != value;
				if (flag)
				{
					this.m_DrawInCameras = value;
					bool flag2 = this.panel.visualTree != null;
					if (flag2)
					{
						this.UIEOnClippingChanged(this.panel.visualTree, true);
					}
				}
				bool flag3 = this.m_DrawInCameras && this.m_StaticIndex < 0;
				if (flag3)
				{
					this.m_StaticIndex = RenderChain.RenderChainStaticIndexAllocator.AllocateIndex(this);
				}
				else
				{
					bool flag4 = !this.m_DrawInCameras && this.m_StaticIndex >= 0;
					if (flag4)
					{
						RenderChain.RenderChainStaticIndexAllocator.FreeIndex(this.m_StaticIndex);
						this.m_StaticIndex = -1;
					}
				}
			}
		}

		internal Shader defaultShader
		{
			get
			{
				return this.m_DefaultShader;
			}
			set
			{
				bool flag = this.m_DefaultShader == value;
				if (!flag)
				{
					this.m_DefaultShader = value;
					UIRUtility.Destroy(this.m_DefaultMat);
					this.m_DefaultMat = null;
				}
			}
		}

		internal Shader defaultWorldSpaceShader
		{
			get
			{
				return this.m_DefaultWorldSpaceShader;
			}
			set
			{
				bool flag = this.m_DefaultWorldSpaceShader == value;
				if (!flag)
				{
					this.m_DefaultWorldSpaceShader = value;
					UIRUtility.Destroy(this.m_DefaultWorldSpaceMat);
					this.m_DefaultWorldSpaceMat = null;
				}
			}
		}

		static RenderChain()
		{
			RenderChain.s_MarkerProcess = new ProfilerMarker("RenderChain.Process");
			RenderChain.s_MarkerRender = new ProfilerMarker("RenderChain.Draw");
			RenderChain.s_MarkerClipProcessing = new ProfilerMarker("RenderChain.UpdateClips");
			RenderChain.s_MarkerOpacityProcessing = new ProfilerMarker("RenderChain.UpdateOpacity");
			RenderChain.s_MarkerTransformProcessing = new ProfilerMarker("RenderChain.UpdateTransforms");
			RenderChain.s_MarkerVisualsProcessing = new ProfilerMarker("RenderChain.UpdateVisuals");
			RenderChain.s_MarkerTextRegen = new ProfilerMarker("RenderChain.RegenText");
			RenderChain.OnPreRender = null;
			Utility.RegisterIntermediateRenderers += new Action<Camera>(RenderChain.OnRegisterIntermediateRenderers);
			Utility.RenderNodeExecute += new Action<IntPtr>(RenderChain.OnRenderNodeExecute);
		}

		public RenderChain(IPanel panel)
		{
			UIRAtlasManager uIRAtlasManager = new UIRAtlasManager(RenderTextureFormat.ARGB32, FilterMode.Bilinear, 64, 64);
			VectorImageManager vectorImageMan = new VectorImageManager(uIRAtlasManager);
			this.Constructor(panel, new UIRenderDevice(0u, 0u), uIRAtlasManager, vectorImageMan);
		}

		protected RenderChain(IPanel panel, UIRenderDevice device, UIRAtlasManager atlasManager, VectorImageManager vectorImageManager)
		{
			this.Constructor(panel, device, atlasManager, vectorImageManager);
		}

		private void Constructor(IPanel panelObj, UIRenderDevice deviceObj, UIRAtlasManager atlasMan, VectorImageManager vectorImageMan)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			this.m_DirtyTracker.heads = new List<VisualElement>(8);
			this.m_DirtyTracker.tails = new List<VisualElement>(8);
			this.m_DirtyTracker.minDepths = new int[4];
			this.m_DirtyTracker.maxDepths = new int[4];
			this.m_DirtyTracker.Reset();
			bool flag = this.m_RenderNodesData.Count < 1;
			if (flag)
			{
				this.m_RenderNodesData.Add(new RenderChain.RenderNodeData
				{
					matPropBlock = new MaterialPropertyBlock()
				});
			}
			this.panel = panelObj;
			this.device = deviceObj;
			this.atlasManager = atlasMan;
			this.vectorImageManager = vectorImageMan;
			this.shaderInfoAllocator.Construct();
			this.painter = new UIRStylePainter(this);
			Font.textureRebuilt += new Action<Font>(this.OnFontReset);
		}

		private void Destructor()
		{
			bool flag = this.m_StaticIndex >= 0;
			if (flag)
			{
				RenderChain.RenderChainStaticIndexAllocator.FreeIndex(this.m_StaticIndex);
			}
			this.m_StaticIndex = -1;
			UIRUtility.Destroy(this.m_DefaultMat);
			UIRUtility.Destroy(this.m_DefaultWorldSpaceMat);
			this.m_DefaultMat = (this.m_DefaultWorldSpaceMat = null);
			Font.textureRebuilt -= new Action<Font>(this.OnFontReset);
			UIRStylePainter expr_64 = this.painter;
			if (expr_64 != null)
			{
				expr_64.Dispose();
			}
			UIRTextUpdatePainter expr_76 = this.m_TextUpdatePainter;
			if (expr_76 != null)
			{
				expr_76.Dispose();
			}
			UIRAtlasManager expr_88 = this.atlasManager;
			if (expr_88 != null)
			{
				expr_88.Dispose();
			}
			VectorImageManager expr_9A = this.vectorImageManager;
			if (expr_9A != null)
			{
				expr_9A.Dispose();
			}
			this.shaderInfoAllocator.Dispose();
			UIRenderDevice expr_B8 = this.device;
			if (expr_B8 != null)
			{
				expr_B8.Dispose();
			}
			this.painter = null;
			this.m_TextUpdatePainter = null;
			this.atlasManager = null;
			this.shaderInfoAllocator = default(UIRVEShaderInfoAllocator);
			this.device = null;
			this.m_ActiveRenderNodes = 0;
			this.m_RenderNodesData.Clear();
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.Destructor();
				}
				this.disposed = true;
			}
		}

		public void ProcessChanges()
		{
			RenderChain.s_MarkerProcess.Begin();
			this.m_Stats = default(ChainBuilderStats);
			this.m_Stats.elementsAdded = this.m_Stats.elementsAdded + this.m_StatsElementsAdded;
			this.m_Stats.elementsRemoved = this.m_Stats.elementsRemoved + this.m_StatsElementsRemoved;
			this.m_StatsElementsAdded = (this.m_StatsElementsRemoved = 0u);
			bool isReleased = this.shaderInfoAllocator.isReleased;
			if (isReleased)
			{
				this.RecreateDevice();
			}
			bool flag = this.m_DrawInCameras && this.m_StaticIndex < 0;
			if (flag)
			{
				this.m_StaticIndex = RenderChain.RenderChainStaticIndexAllocator.AllocateIndex(this);
			}
			else
			{
				bool flag2 = !this.m_DrawInCameras && this.m_StaticIndex >= 0;
				if (flag2)
				{
					RenderChain.RenderChainStaticIndexAllocator.FreeIndex(this.m_StaticIndex);
					this.m_StaticIndex = -1;
				}
			}
			bool flag3 = RenderChain.OnPreRender != null;
			if (flag3)
			{
				RenderChain.OnPreRender();
			}
			bool flag4 = false;
			UIRAtlasManager expr_E7 = this.atlasManager;
			bool flag5 = expr_E7 != null && expr_E7.RequiresReset();
			if (flag5)
			{
				this.atlasManager.Reset();
				flag4 = true;
			}
			VectorImageManager expr_10F = this.vectorImageManager;
			bool flag6 = expr_10F != null && expr_10F.RequiresReset();
			if (flag6)
			{
				this.vectorImageManager.Reset();
				flag4 = true;
			}
			bool flag7 = flag4;
			if (flag7)
			{
				this.RepaintAtlassedElements();
			}
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1u;
			int num = 0;
			RenderDataDirtyTypes renderDataDirtyTypes = RenderDataDirtyTypes.Clipping | RenderDataDirtyTypes.ClippingHierarchy;
			RenderDataDirtyTypes dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerClipProcessing.Begin();
			for (int i = this.m_DirtyTracker.minDepths[num]; i <= this.m_DirtyTracker.maxDepths[num]; i++)
			{
				VisualElement visualElement = this.m_DirtyTracker.heads[i];
				while (visualElement != null)
				{
					VisualElement nextDirty = visualElement.renderChainData.nextDirty;
					bool flag8 = (visualElement.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag8)
					{
						bool flag9 = visualElement.renderChainData.isInChain && visualElement.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag9)
						{
							RenderEvents.ProcessOnClippingChanged(this, visualElement, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement, dirtyTypesInverse);
					}
					visualElement = nextDirty;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1u;
				}
			}
			RenderChain.s_MarkerClipProcessing.End();
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1u;
			num = 1;
			renderDataDirtyTypes = RenderDataDirtyTypes.Opacity;
			dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerOpacityProcessing.Begin();
			for (int j = this.m_DirtyTracker.minDepths[num]; j <= this.m_DirtyTracker.maxDepths[num]; j++)
			{
				VisualElement visualElement2 = this.m_DirtyTracker.heads[j];
				while (visualElement2 != null)
				{
					VisualElement nextDirty2 = visualElement2.renderChainData.nextDirty;
					bool flag10 = (visualElement2.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag10)
					{
						bool flag11 = visualElement2.renderChainData.isInChain && visualElement2.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag11)
						{
							RenderEvents.ProcessOnOpacityChanged(this, visualElement2, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement2, dirtyTypesInverse);
					}
					visualElement2 = nextDirty2;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1u;
				}
			}
			RenderChain.s_MarkerOpacityProcessing.End();
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1u;
			num = 2;
			renderDataDirtyTypes = (RenderDataDirtyTypes.Transform | RenderDataDirtyTypes.ClipRectSize);
			dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerTransformProcessing.Begin();
			for (int k = this.m_DirtyTracker.minDepths[num]; k <= this.m_DirtyTracker.maxDepths[num]; k++)
			{
				VisualElement visualElement3 = this.m_DirtyTracker.heads[k];
				while (visualElement3 != null)
				{
					VisualElement nextDirty3 = visualElement3.renderChainData.nextDirty;
					bool flag12 = (visualElement3.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag12)
					{
						bool flag13 = visualElement3.renderChainData.isInChain && visualElement3.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag13)
						{
							RenderEvents.ProcessOnTransformOrSizeChanged(this, visualElement3, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement3, dirtyTypesInverse);
					}
					visualElement3 = nextDirty3;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1u;
				}
			}
			RenderChain.s_MarkerTransformProcessing.End();
			this.m_BlockDirtyRegistration = true;
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1u;
			num = 3;
			renderDataDirtyTypes = (RenderDataDirtyTypes.Visuals | RenderDataDirtyTypes.VisualsHierarchy);
			dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerVisualsProcessing.Begin();
			for (int l = this.m_DirtyTracker.minDepths[num]; l <= this.m_DirtyTracker.maxDepths[num]; l++)
			{
				VisualElement visualElement4 = this.m_DirtyTracker.heads[l];
				while (visualElement4 != null)
				{
					VisualElement nextDirty4 = visualElement4.renderChainData.nextDirty;
					bool flag14 = (visualElement4.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag14)
					{
						bool flag15 = visualElement4.renderChainData.isInChain && visualElement4.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag15)
						{
							RenderEvents.ProcessOnVisualsChanged(this, visualElement4, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement4, dirtyTypesInverse);
					}
					visualElement4 = nextDirty4;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1u;
				}
			}
			RenderChain.s_MarkerVisualsProcessing.End();
			this.m_BlockDirtyRegistration = false;
			this.m_DirtyTracker.Reset();
			this.ProcessTextRegen(true);
			bool fontWasReset = this.m_FontWasReset;
			if (fontWasReset)
			{
				for (int m = 0; m < 2; m++)
				{
					bool flag16 = !this.m_FontWasReset;
					if (flag16)
					{
						break;
					}
					this.m_FontWasReset = false;
					this.ProcessTextRegen(false);
				}
			}
			UIRAtlasManager expr_648 = this.atlasManager;
			if (expr_648 != null)
			{
				expr_648.Commit();
			}
			VectorImageManager expr_65A = this.vectorImageManager;
			if (expr_65A != null)
			{
				expr_65A.Commit();
			}
			this.shaderInfoAllocator.IssuePendingAtlasBlits();
			UIRenderDevice expr_678 = this.device;
			if (expr_678 != null)
			{
				expr_678.OnFrameRenderingBegin();
			}
			RenderChain.s_MarkerProcess.End();
		}

		public void Render()
		{
			RenderChain.s_MarkerRender.Begin();
			Material standardMaterial = this.GetStandardMaterial();
			((BaseVisualElementPanel)this.panel).InvokeUpdateMaterial(standardMaterial);
			Exception ex = null;
			bool flag = this.m_FirstCommand != null;
			if (flag)
			{
				bool flag2 = !this.m_DrawInCameras;
				if (flag2)
				{
					Rect layout = this.panel.visualTree.layout;
					if (standardMaterial != null)
					{
						standardMaterial.SetPass(0);
					}
					Matrix4x4 mat = ProjectionUtils.Ortho(layout.xMin, layout.xMax, layout.yMax, layout.yMin, -0.001f, 1.001f);
					GL.LoadProjectionMatrix(mat);
					GL.modelview = Matrix4x4.identity;
					UIRenderDevice arg_11F_0 = this.device;
					RenderChainCommand arg_11F_1 = this.m_FirstCommand;
					Material arg_11F_2 = standardMaterial;
					Material arg_11F_3 = standardMaterial;
					UIRAtlasManager expr_BC = this.atlasManager;
					Texture arg_11F_4 = (expr_BC != null) ? expr_BC.atlas : null;
					VectorImageManager expr_CE = this.vectorImageManager;
					arg_11F_0.EvaluateChain(arg_11F_1, arg_11F_2, arg_11F_3, arg_11F_4, (expr_CE != null) ? expr_CE.atlas : null, this.shaderInfoAllocator.atlas, (this.panel as BaseVisualElementPanel).scaledPixelsPerPoint, this.shaderInfoAllocator.transformConstants, this.shaderInfoAllocator.clipRectConstants, this.m_RenderNodesData[0].matPropBlock, true, ref ex);
				}
			}
			RenderChain.s_MarkerRender.End();
			bool flag3 = ex != null;
			if (!flag3)
			{
				bool drawStats = this.drawStats;
				if (drawStats)
				{
					this.DrawStats();
				}
				return;
			}
			bool flag4 = GUIUtility.IsExitGUIException(ex);
			if (flag4)
			{
				throw ex;
			}
			throw new ImmediateModeException(ex);
		}

		private void ProcessTextRegen(bool timeSliced)
		{
			bool flag = (timeSliced && this.m_DirtyTextRemaining == 0) || this.m_TextElementCount == 0;
			if (!flag)
			{
				RenderChain.s_MarkerTextRegen.Begin();
				bool flag2 = this.m_TextUpdatePainter == null;
				if (flag2)
				{
					this.m_TextUpdatePainter = new UIRTextUpdatePainter();
				}
				VisualElement visualElement = this.m_FirstTextElement;
				this.m_DirtyTextStartIndex = (timeSliced ? (this.m_DirtyTextStartIndex % this.m_TextElementCount) : 0);
				for (int i = 0; i < this.m_DirtyTextStartIndex; i++)
				{
					visualElement = visualElement.renderChainData.nextText;
				}
				bool flag3 = visualElement == null;
				if (flag3)
				{
					visualElement = this.m_FirstTextElement;
				}
				int num = timeSliced ? Math.Min(50, this.m_DirtyTextRemaining) : this.m_TextElementCount;
				for (int j = 0; j < num; j++)
				{
					RenderEvents.ProcessRegenText(this, visualElement, this.m_TextUpdatePainter, this.device, ref this.m_Stats);
					visualElement = visualElement.renderChainData.nextText;
					this.m_DirtyTextStartIndex++;
					bool flag4 = visualElement == null;
					if (flag4)
					{
						visualElement = this.m_FirstTextElement;
						this.m_DirtyTextStartIndex = 0;
					}
				}
				this.m_DirtyTextRemaining = Math.Max(0, this.m_DirtyTextRemaining - num);
				bool flag5 = this.m_DirtyTextRemaining > 0;
				if (flag5)
				{
					BaseVisualElementPanel expr_149 = this.panel as BaseVisualElementPanel;
					if (expr_149 != null)
					{
						expr_149.OnVersionChanged(this.m_FirstTextElement, VersionChangeType.Transform);
					}
				}
				RenderChain.s_MarkerTextRegen.End();
			}
		}

		public void UIEOnChildAdded(VisualElement parent, VisualElement ve, int index)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be added to an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			bool flag = parent != null && !parent.renderChainData.isInChain;
			if (!flag)
			{
				uint num = RenderEvents.DepthFirstOnChildAdded(this, parent, ve, index, true);
				Debug.Assert(ve.renderChainData.isInChain);
				Debug.Assert(ve.panel == this.panel);
				this.UIEOnClippingChanged(ve, true);
				this.UIEOnOpacityChanged(ve);
				this.UIEOnVisualsChanged(ve, true);
				this.m_StatsElementsAdded += num;
			}
		}

		public void UIEOnChildrenReordered(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be moved under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				RenderEvents.DepthFirstOnChildRemoving(this, ve.hierarchy[i]);
			}
			for (int j = 0; j < childCount; j++)
			{
				RenderEvents.DepthFirstOnChildAdded(this, ve, ve.hierarchy[j], j, false);
			}
			this.UIEOnClippingChanged(ve, true);
			this.UIEOnVisualsChanged(ve, true);
		}

		public void UIEOnChildRemoving(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be removed from an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			this.m_StatsElementsRemoved += RenderEvents.DepthFirstOnChildRemoving(this, ve);
			Debug.Assert(!ve.renderChainData.isInChain);
		}

		public void StopTrackingGroupTransformElement(VisualElement ve)
		{
			this.m_LastGroupTransformElementScale.Remove(ve);
		}

		public void UIEOnClippingChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change clipping state under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Clipping | (hierarchical ? RenderDataDirtyTypes.ClippingHierarchy : RenderDataDirtyTypes.None), 0);
			}
		}

		public void UIEOnOpacityChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change opacity under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Opacity, 1);
			}
		}

		public void UIEOnTransformOrSizeChanged(VisualElement ve, bool transformChanged, bool clipRectSizeChanged)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change size or transform under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				RenderDataDirtyTypes dirtyTypes = (transformChanged ? RenderDataDirtyTypes.Transform : RenderDataDirtyTypes.None) | (clipRectSizeChanged ? RenderDataDirtyTypes.ClipRectSize : RenderDataDirtyTypes.None);
				this.m_DirtyTracker.RegisterDirty(ve, dirtyTypes, 2);
			}
		}

		public void UIEOnVisualsChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot be marked for dirty repaint under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Visuals | (hierarchical ? RenderDataDirtyTypes.VisualsHierarchy : RenderDataDirtyTypes.None), 3);
			}
		}

		internal Material GetStandardMaterial()
		{
			bool flag = this.m_DefaultMat == null && this.m_DefaultShader != null;
			if (flag)
			{
				this.m_DefaultMat = new Material(this.m_DefaultShader);
				this.m_DefaultMat.hideFlags |= HideFlags.DontSaveInEditor;
			}
			return this.m_DefaultMat;
		}

		internal Material GetStandardWorldSpaceMaterial()
		{
			bool flag = this.m_DefaultWorldSpaceMat == null && this.m_DefaultWorldSpaceShader != null;
			if (flag)
			{
				this.m_DefaultWorldSpaceMat = new Material(this.m_DefaultWorldSpaceShader);
				this.m_DefaultWorldSpaceMat.hideFlags |= HideFlags.DontSaveInEditor;
			}
			return this.m_DefaultWorldSpaceMat;
		}

		internal void EnsureFitsDepth(int depth)
		{
			this.m_DirtyTracker.EnsureFits(depth);
		}

		internal void ChildWillBeRemoved(VisualElement ve)
		{
			bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
			if (flag)
			{
				this.m_DirtyTracker.ClearDirty(ve, ~ve.renderChainData.dirtiedValues);
			}
			Debug.Assert(ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None);
			Debug.Assert(ve.renderChainData.prevDirty == null);
			Debug.Assert(ve.renderChainData.nextDirty == null);
		}

		internal RenderChainCommand AllocCommand()
		{
			RenderChainCommand renderChainCommand = this.m_CommandPool.Get();
			renderChainCommand.Reset();
			return renderChainCommand;
		}

		internal void FreeCommand(RenderChainCommand cmd)
		{
			bool flag = cmd.state.material != null;
			if (flag)
			{
				this.m_CustomMaterialCommands--;
			}
			cmd.Reset();
			this.m_CommandPool.Return(cmd);
		}

		internal void OnRenderCommandAdded(RenderChainCommand command)
		{
			bool flag = command.prev == null;
			if (flag)
			{
				this.m_FirstCommand = command;
			}
			bool flag2 = command.state.material != null;
			if (flag2)
			{
				this.m_CustomMaterialCommands++;
			}
		}

		internal void OnRenderCommandsRemoved(RenderChainCommand firstCommand, RenderChainCommand lastCommand)
		{
			bool flag = firstCommand.prev == null;
			if (flag)
			{
				this.m_FirstCommand = lastCommand.next;
			}
		}

		internal void AddTextElement(VisualElement ve)
		{
			bool flag = this.m_FirstTextElement != null;
			if (flag)
			{
				this.m_FirstTextElement.renderChainData.prevText = ve;
				ve.renderChainData.nextText = this.m_FirstTextElement;
			}
			this.m_FirstTextElement = ve;
			this.m_TextElementCount++;
		}

		internal void RemoveTextElement(VisualElement ve)
		{
			bool flag = ve.renderChainData.prevText != null;
			if (flag)
			{
				ve.renderChainData.prevText.renderChainData.nextText = ve.renderChainData.nextText;
			}
			bool flag2 = ve.renderChainData.nextText != null;
			if (flag2)
			{
				ve.renderChainData.nextText.renderChainData.prevText = ve.renderChainData.prevText;
			}
			bool flag3 = this.m_FirstTextElement == ve;
			if (flag3)
			{
				this.m_FirstTextElement = ve.renderChainData.nextText;
			}
			ve.renderChainData.prevText = (ve.renderChainData.nextText = null);
			this.m_TextElementCount--;
		}

		internal void OnGroupTransformElementChangedTransform(VisualElement ve)
		{
			Vector2 vector;
			bool flag = !this.m_LastGroupTransformElementScale.TryGetValue(ve, out vector) || ve.worldTransform.m00 != vector.x || ve.worldTransform.m11 != vector.y;
			if (flag)
			{
				this.m_DirtyTextRemaining = this.m_TextElementCount;
				this.m_LastGroupTransformElementScale[ve] = new Vector2(ve.worldTransform.m00, ve.worldTransform.m11);
			}
		}

		internal void BeforeRenderDeviceRelease()
		{
			Debug.Assert(this.device != null);
			Debug.Assert(this.m_RenderDeviceRestoreInfo.panel == null);
			Debug.Assert(this.m_RenderDeviceRestoreInfo.root == null);
			this.m_RenderDeviceRestoreInfo.panel = this.panel;
			RenderChainCommand expr_55 = this.m_FirstCommand;
			this.m_RenderDeviceRestoreInfo.root = RenderChain.GetFirstElementInPanel((expr_55 != null) ? expr_55.owner : null);
			this.m_RenderDeviceRestoreInfo.hasAtlasMan = (this.atlasManager != null);
			this.m_RenderDeviceRestoreInfo.hasVectorImageMan = (this.vectorImageManager != null);
			bool flag = this.m_RenderDeviceRestoreInfo.root != null;
			if (flag)
			{
				this.UIEOnChildRemoving(this.m_RenderDeviceRestoreInfo.root);
			}
			this.Destructor();
		}

		internal void AfterRenderDeviceRelease()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			Debug.Assert(this.device == null);
			IPanel panel = this.m_RenderDeviceRestoreInfo.panel;
			VisualElement root = this.m_RenderDeviceRestoreInfo.root;
			UIRenderDevice deviceObj = new UIRenderDevice(0u, 0u);
			UIRAtlasManager uIRAtlasManager = this.m_RenderDeviceRestoreInfo.hasAtlasMan ? new UIRAtlasManager(RenderTextureFormat.ARGB32, FilterMode.Bilinear, 64, 64) : null;
			VectorImageManager vectorImageMan = this.m_RenderDeviceRestoreInfo.hasVectorImageMan ? new VectorImageManager(uIRAtlasManager) : null;
			this.m_RenderDeviceRestoreInfo = default(RenderChain.RenderDeviceRestoreInfo);
			this.Constructor(panel, deviceObj, uIRAtlasManager, vectorImageMan);
			bool flag = root != null;
			if (flag)
			{
				Debug.Assert(root.panel == panel);
				this.UIEOnChildAdded(root.parent, root, (root.hierarchy.parent == null) ? 0 : root.hierarchy.parent.IndexOf(this.panel.visualTree));
			}
		}

		internal void RecreateDevice()
		{
			this.BeforeRenderDeviceRelease();
			this.AfterRenderDeviceRelease();
		}

		private unsafe static RenderChain.RenderNodeData AccessRenderNodeData(IntPtr obj)
		{
			int* ptr = (int*)obj.ToPointer();
			RenderChain renderChain = RenderChain.RenderChainStaticIndexAllocator.AccessIndex(*ptr);
			return renderChain.m_RenderNodesData[ptr[1]];
		}

		private static void OnRenderNodeExecute(IntPtr obj)
		{
			RenderChain.RenderNodeData renderNodeData = RenderChain.AccessRenderNodeData(obj);
			Exception ex = null;
			renderNodeData.device.EvaluateChain(renderNodeData.firstCommand, renderNodeData.initialMaterial, renderNodeData.standardMaterial, renderNodeData.atlas, renderNodeData.vectorAtlas, renderNodeData.shaderInfoAtlas, renderNodeData.dpiScale, renderNodeData.transformConstants, renderNodeData.clipRectConstants, renderNodeData.matPropBlock, false, ref ex);
		}

		private static void OnRegisterIntermediateRenderers(Camera camera)
		{
			int num = 0;
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> current = panelsIterator.Current;
				Panel value = current.Value;
				UIRRepaintUpdater expr_2C = value.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				RenderChain renderChain = (expr_2C != null) ? expr_2C.renderChain : null;
				bool flag = renderChain == null || renderChain.m_StaticIndex < 0 || renderChain.m_FirstCommand == null;
				if (!flag)
				{
					BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)value;
					Material standardWorldSpaceMaterial = renderChain.GetStandardWorldSpaceMaterial();
					RenderChain.RenderNodeData renderNodeData = default(RenderChain.RenderNodeData);
					renderNodeData.device = renderChain.device;
					renderNodeData.standardMaterial = standardWorldSpaceMaterial;
					UIRAtlasManager expr_92 = renderChain.atlasManager;
					renderNodeData.atlas = ((expr_92 != null) ? expr_92.atlas : null);
					VectorImageManager expr_AB = renderChain.vectorImageManager;
					renderNodeData.vectorAtlas = ((expr_AB != null) ? expr_AB.atlas : null);
					renderNodeData.shaderInfoAtlas = renderChain.shaderInfoAllocator.atlas;
					renderNodeData.dpiScale = baseRuntimePanel.scaledPixelsPerPoint;
					renderNodeData.transformConstants = renderChain.shaderInfoAllocator.transformConstants;
					renderNodeData.clipRectConstants = renderChain.shaderInfoAllocator.clipRectConstants;
					bool flag2 = renderChain.m_CustomMaterialCommands == 0;
					if (flag2)
					{
						renderNodeData.initialMaterial = standardWorldSpaceMaterial;
						renderNodeData.firstCommand = renderChain.m_FirstCommand;
						RenderChain.OnRegisterIntermediateRendererMat(baseRuntimePanel, renderChain, ref renderNodeData, camera, num++);
					}
					else
					{
						Material material = null;
						RenderChainCommand renderChainCommand = renderChain.m_FirstCommand;
						RenderChainCommand renderChainCommand2 = renderChainCommand;
						while (renderChainCommand != null)
						{
							bool flag3 = renderChainCommand.type > CommandType.Draw;
							if (flag3)
							{
								renderChainCommand = renderChainCommand.next;
							}
							else
							{
								Material material2 = (renderChainCommand.state.material == null) ? standardWorldSpaceMaterial : renderChainCommand.state.material;
								bool flag4 = material2 != material;
								if (flag4)
								{
									bool flag5 = material != null;
									if (flag5)
									{
										renderNodeData.initialMaterial = material;
										renderNodeData.firstCommand = renderChainCommand2;
										RenderChain.OnRegisterIntermediateRendererMat(baseRuntimePanel, renderChain, ref renderNodeData, camera, num++);
										renderChainCommand2 = renderChainCommand;
									}
									material = material2;
								}
								renderChainCommand = renderChainCommand.next;
							}
						}
						bool flag6 = renderChainCommand2 != null;
						if (flag6)
						{
							renderNodeData.initialMaterial = material;
							renderNodeData.firstCommand = renderChainCommand2;
							RenderChain.OnRegisterIntermediateRendererMat(baseRuntimePanel, renderChain, ref renderNodeData, camera, num++);
						}
					}
				}
			}
		}

		private unsafe static void OnRegisterIntermediateRendererMat(BaseRuntimePanel rtp, RenderChain renderChain, ref RenderChain.RenderNodeData rnd, Camera camera, int sameDistanceSortPriority)
		{
			int activeRenderNodes = renderChain.m_ActiveRenderNodes;
			renderChain.m_ActiveRenderNodes = activeRenderNodes + 1;
			int num = activeRenderNodes;
			bool flag = num < renderChain.m_RenderNodesData.Count;
			if (flag)
			{
				RenderChain.RenderNodeData renderNodeData = renderChain.m_RenderNodesData[num];
				rnd.matPropBlock = renderNodeData.matPropBlock;
				renderChain.m_RenderNodesData[num] = rnd;
			}
			else
			{
				rnd.matPropBlock = new MaterialPropertyBlock();
				num = renderChain.m_RenderNodesData.Count;
				renderChain.m_RenderNodesData.Add(rnd);
			}
			int* ptr = stackalloc int[2];
			*ptr = renderChain.m_StaticIndex;
			ptr[1] = num;
			Utility.RegisterIntermediateRenderer(camera, rnd.initialMaterial, rtp.panelToWorld, new Bounds(Vector3.zero, new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f)), 3, 0, false, sameDistanceSortPriority, (ulong)((long)camera.cullingMask), 2, new IntPtr((void*)ptr), 8);
		}

		private void RepaintAtlassedElements()
		{
			RenderChainCommand expr_07 = this.m_FirstCommand;
			for (VisualElement visualElement = RenderChain.GetFirstElementInPanel((expr_07 != null) ? expr_07.owner : null); visualElement != null; visualElement = visualElement.renderChainData.next)
			{
				bool usesAtlas = visualElement.renderChainData.usesAtlas;
				if (usesAtlas)
				{
					this.UIEOnVisualsChanged(visualElement, false);
				}
			}
			this.UIEOnOpacityChanged(this.panel.visualTree);
		}

		private void OnFontReset(Font font)
		{
			this.m_FontWasReset = true;
		}

		private void DrawStats()
		{
			bool flag = this.device != null;
			float num = 12f;
			Rect position = new Rect(30f, 60f, 1000f, 100f);
			GUI.Box(new Rect(20f, 40f, 200f, (float)(flag ? 380 : 256)), "UI Toolkit Draw Stats");
			GUI.Label(position, "Elements added\t: " + this.m_Stats.elementsAdded.ToString());
			position.y += num;
			GUI.Label(position, "Elements removed\t: " + this.m_Stats.elementsRemoved.ToString());
			position.y += num;
			GUI.Label(position, "Mesh allocs allocated\t: " + this.m_Stats.newMeshAllocations.ToString());
			position.y += num;
			GUI.Label(position, "Mesh allocs updated\t: " + this.m_Stats.updatedMeshAllocations.ToString());
			position.y += num;
			GUI.Label(position, "Clip update roots\t: " + this.m_Stats.recursiveClipUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Clip update total\t: " + this.m_Stats.recursiveClipUpdatesExpanded.ToString());
			position.y += num;
			GUI.Label(position, "Opacity update roots\t: " + this.m_Stats.recursiveOpacityUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Opacity update total\t: " + this.m_Stats.recursiveOpacityUpdatesExpanded.ToString());
			position.y += num;
			GUI.Label(position, "Xform update roots\t: " + this.m_Stats.recursiveTransformUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Xform update total\t: " + this.m_Stats.recursiveTransformUpdatesExpanded.ToString());
			position.y += num;
			GUI.Label(position, "Xformed by bone\t: " + this.m_Stats.boneTransformed.ToString());
			position.y += num;
			GUI.Label(position, "Xformed by skipping\t: " + this.m_Stats.skipTransformed.ToString());
			position.y += num;
			GUI.Label(position, "Xformed by nudging\t: " + this.m_Stats.nudgeTransformed.ToString());
			position.y += num;
			GUI.Label(position, "Xformed by repaint\t: " + this.m_Stats.visualUpdateTransformed.ToString());
			position.y += num;
			GUI.Label(position, "Visual update roots\t: " + this.m_Stats.recursiveVisualUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Visual update total\t: " + this.m_Stats.recursiveVisualUpdatesExpanded.ToString());
			position.y += num;
			GUI.Label(position, "Visual update flats\t: " + this.m_Stats.nonRecursiveVisualUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Dirty processed\t: " + this.m_Stats.dirtyProcessed.ToString());
			position.y += num;
			GUI.Label(position, "Group-xform updates\t: " + this.m_Stats.groupTransformElementsChanged.ToString());
			position.y += num;
			GUI.Label(position, "Text regens\t: " + this.m_Stats.textUpdates.ToString());
			position.y += num;
			bool flag2 = !flag;
			if (!flag2)
			{
				position.y += num;
				UIRenderDevice.DrawStatistics drawStatistics = this.device.GatherDrawStatistics();
				GUI.Label(position, "Frame index\t: " + drawStatistics.currentFrameIndex.ToString());
				position.y += num;
				GUI.Label(position, "Command count\t: " + drawStatistics.commandCount.ToString());
				position.y += num;
				GUI.Label(position, "Draw commands\t: " + drawStatistics.drawCommandCount.ToString());
				position.y += num;
				GUI.Label(position, "Draw ranges\t: " + drawStatistics.drawRangeCount.ToString());
				position.y += num;
				GUI.Label(position, "Draw range calls\t: " + drawStatistics.drawRangeCallCount.ToString());
				position.y += num;
				GUI.Label(position, "Material sets\t: " + drawStatistics.materialSetCount.ToString());
				position.y += num;
				GUI.Label(position, "Immediate draws\t: " + drawStatistics.immediateDraws.ToString());
				position.y += num;
				GUI.Label(position, "Total triangles\t: " + (drawStatistics.totalIndices / 3u).ToString());
				position.y += num;
			}
		}

		private static VisualElement GetFirstElementInPanel(VisualElement ve)
		{
			while (true)
			{
				bool arg_32_0;
				if (ve != null)
				{
					VisualElement expr_1E = ve.renderChainData.prev;
					arg_32_0 = (expr_1E != null && expr_1E.renderChainData.isInChain);
				}
				else
				{
					arg_32_0 = false;
				}
				if (!arg_32_0)
				{
					break;
				}
				ve = ve.renderChainData.prev;
			}
			return ve;
		}
	}
}
