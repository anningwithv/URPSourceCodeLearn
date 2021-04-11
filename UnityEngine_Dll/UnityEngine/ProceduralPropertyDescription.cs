using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	[EditorBrowsable(EditorBrowsableState.Never), Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProceduralPropertyDescription
	{
		public string name;

		public string label;

		public string group;

		public ProceduralPropertyType type;

		public bool hasRange;

		public float minimum;

		public float maximum;

		public float step;

		public string[] enumOptions;

		public string[] componentLabels;
	}
}
