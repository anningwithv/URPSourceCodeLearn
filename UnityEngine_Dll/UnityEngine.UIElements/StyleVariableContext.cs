using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	internal class StyleVariableContext
	{
		public static readonly StyleVariableContext none = new StyleVariableContext();

		private int m_VariableHash;

		private List<StyleVariable> m_Variables;

		private List<int> m_SortedHash;

		public void Add(StyleVariable sv)
		{
			int hashCode = sv.GetHashCode();
			int num = this.m_SortedHash.BinarySearch(hashCode);
			bool flag = num >= 0;
			if (!flag)
			{
				this.m_SortedHash.Insert(~num, hashCode);
				this.m_Variables.Add(sv);
				this.m_VariableHash = ((this.m_Variables.Count == 0) ? sv.GetHashCode() : (this.m_VariableHash * 397 ^ sv.GetHashCode()));
			}
		}

		public void AddInitialRange(StyleVariableContext other)
		{
			bool flag = other.m_Variables.Count > 0;
			if (flag)
			{
				Debug.Assert(this.m_Variables.Count == 0);
				this.m_VariableHash = other.m_VariableHash;
				this.m_Variables.AddRange(other.m_Variables);
				this.m_SortedHash.AddRange(other.m_SortedHash);
			}
		}

		public void Clear()
		{
			bool flag = this.m_Variables.Count > 0;
			if (flag)
			{
				this.m_Variables.Clear();
				this.m_VariableHash = 0;
				this.m_SortedHash.Clear();
			}
		}

		public StyleVariableContext()
		{
			this.m_Variables = new List<StyleVariable>();
			this.m_VariableHash = 0;
			this.m_SortedHash = new List<int>();
		}

		public StyleVariableContext(StyleVariableContext other)
		{
			this.m_Variables = new List<StyleVariable>(other.m_Variables);
			this.m_VariableHash = other.m_VariableHash;
			this.m_SortedHash = new List<int>(other.m_SortedHash);
		}

		public bool TryFindVariable(string name, out StyleVariable v)
		{
			bool result;
			for (int i = this.m_Variables.Count - 1; i >= 0; i--)
			{
				bool flag = this.m_Variables[i].name == name;
				if (flag)
				{
					v = this.m_Variables[i];
					result = true;
					return result;
				}
			}
			v = default(StyleVariable);
			result = false;
			return result;
		}

		public int GetVariableHash()
		{
			return this.m_VariableHash;
		}
	}
}
