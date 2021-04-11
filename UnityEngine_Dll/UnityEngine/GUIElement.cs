using System;
using System.ComponentModel;
using UnityEngine.Internal;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("GUIElement has been removed.", true), ExcludeFromObjectFactory, ExcludeFromPreset]
	public sealed class GUIElement
	{
		private static void FeatureRemoved()
		{
			throw new Exception("GUIElement has been removed from Unity.");
		}

		[Obsolete("GUIElement has been removed.", true)]
		public bool HitTest(Vector3 screenPosition)
		{
			GUIElement.FeatureRemoved();
			return false;
		}

		[Obsolete("GUIElement has been removed.", true)]
		public bool HitTest(Vector3 screenPosition, [UnityEngine.Internal.DefaultValue("null")] Camera camera)
		{
			GUIElement.FeatureRemoved();
			return false;
		}

		[Obsolete("GUIElement has been removed.", true)]
		public Rect GetScreenRect([UnityEngine.Internal.DefaultValue("null")] Camera camera)
		{
			GUIElement.FeatureRemoved();
			return new Rect(0f, 0f, 0f, 0f);
		}

		[Obsolete("GUIElement has been removed.", true)]
		public Rect GetScreenRect()
		{
			GUIElement.FeatureRemoved();
			return new Rect(0f, 0f, 0f, 0f);
		}
	}
}
