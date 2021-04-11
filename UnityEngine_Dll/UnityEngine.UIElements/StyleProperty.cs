using System;

namespace UnityEngine.UIElements
{
	[Serializable]
	internal class StyleProperty
	{
		[SerializeField]
		private string m_Name;

		[SerializeField]
		private int m_Line;

		[SerializeField]
		private StyleValueHandle[] m_Values;

		[NonSerialized]
		internal bool isCustomProperty;

		[NonSerialized]
		internal bool requireVariableResolve;

		public string name
		{
			get
			{
				return this.m_Name;
			}
			internal set
			{
				this.m_Name = value;
			}
		}

		public int line
		{
			get
			{
				return this.m_Line;
			}
			internal set
			{
				this.m_Line = value;
			}
		}

		public StyleValueHandle[] values
		{
			get
			{
				return this.m_Values;
			}
			internal set
			{
				this.m_Values = value;
			}
		}
	}
}
