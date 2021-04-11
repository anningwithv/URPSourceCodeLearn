using System;

namespace UnityEngine.UIElements
{
	internal struct StyleVariable
	{
		public readonly string name;

		public readonly StyleSheet sheet;

		public readonly StyleValueHandle[] handles;

		public StyleVariable(string name, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.name = name;
			this.sheet = sheet;
			this.handles = handles;
		}

		public override int GetHashCode()
		{
			int num = this.name.GetHashCode();
			num = (num * 397 ^ this.sheet.GetHashCode());
			return num * 397 ^ this.handles.GetHashCode();
		}
	}
}
