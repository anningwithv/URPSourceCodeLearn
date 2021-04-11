using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements
{
	internal class StyleVariableResolver
	{
		public enum Result
		{
			Valid,
			Invalid,
			NotFound
		}

		internal const int kMaxResolves = 100;

		private static StyleSyntaxParser s_SyntaxParser = new StyleSyntaxParser();

		private StylePropertyValueMatcher m_Matcher = new StylePropertyValueMatcher();

		private List<StylePropertyValue> m_ResolvedValues = new List<StylePropertyValue>();

		private Stack<string> m_ResolvedVarStack = new Stack<string>();

		private Expression m_ValidationExpression;

		private StyleProperty m_Property;

		private StyleSheet m_Sheet;

		private StyleValueHandle[] m_Handles;

		public List<StylePropertyValue> resolvedValues
		{
			get
			{
				return this.m_ResolvedValues;
			}
		}

		public StyleVariableContext variableContext
		{
			get;
			set;
		}

		public void Init(StyleProperty property, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_ResolvedValues.Clear();
			this.m_Sheet = sheet;
			this.m_Property = property;
			this.m_Handles = handles;
		}

		public void AddValue(StyleValueHandle handle)
		{
			this.m_ResolvedValues.Add(new StylePropertyValue
			{
				sheet = this.m_Sheet,
				handle = handle
			});
		}

		public StyleVariableResolver.Result ResolveVarFunction(ref int index)
		{
			this.m_ResolvedVarStack.Clear();
			this.m_ValidationExpression = null;
			bool flag = !this.m_Property.isCustomProperty;
			StyleVariableResolver.Result result;
			if (flag)
			{
				string syntax;
				bool flag2 = !StylePropertyCache.TryGetSyntax(this.m_Property.name, out syntax);
				if (flag2)
				{
					Debug.LogAssertion("Unknown style property " + this.m_Property.name);
					result = StyleVariableResolver.Result.Invalid;
					return result;
				}
				this.m_ValidationExpression = StyleVariableResolver.s_SyntaxParser.Parse(syntax);
			}
			int num;
			string variableName;
			StyleVariableResolver.ParseVarFunction(this.m_Sheet, this.m_Handles, ref index, out num, out variableName);
			StyleVariableResolver.Result result2 = this.ResolveVariable(variableName);
			bool flag3 = result2 > StyleVariableResolver.Result.Valid;
			if (flag3)
			{
				bool flag4 = result2 == StyleVariableResolver.Result.NotFound && num > 1 && !this.m_Property.isCustomProperty;
				if (flag4)
				{
					StyleValueHandle[] arg_D8_0 = this.m_Handles;
					int num2 = index + 1;
					index = num2;
					StyleValueHandle styleValueHandle = arg_D8_0[num2];
					Debug.Assert(styleValueHandle.valueType == StyleValueType.FunctionSeparator, string.Format("Unexpected value type {0} in var function", styleValueHandle.valueType));
					bool flag5 = styleValueHandle.valueType == StyleValueType.FunctionSeparator && index + 1 < this.m_Handles.Length;
					if (flag5)
					{
						index++;
						result2 = this.ResolveFallback(ref index);
					}
				}
				else
				{
					this.m_ResolvedValues.Clear();
				}
			}
			result = result2;
			return result;
		}

		private StyleVariableResolver.Result ResolveVariable(string variableName)
		{
			StyleVariable styleVariable;
			bool flag = !this.variableContext.TryFindVariable(variableName, out styleVariable);
			StyleVariableResolver.Result result;
			if (flag)
			{
				result = StyleVariableResolver.Result.NotFound;
			}
			else
			{
				bool flag2 = this.m_ResolvedVarStack.Contains(styleVariable.name);
				if (flag2)
				{
					result = StyleVariableResolver.Result.NotFound;
				}
				else
				{
					this.m_ResolvedVarStack.Push(styleVariable.name);
					StyleVariableResolver.Result result2 = StyleVariableResolver.Result.Valid;
					int num = 0;
					while (num < styleVariable.handles.Length && result2 == StyleVariableResolver.Result.Valid)
					{
						StyleValueHandle handle = styleVariable.handles[num];
						bool flag3 = handle.IsVarFunction();
						if (flag3)
						{
							int num2;
							string variableName2;
							StyleVariableResolver.ParseVarFunction(styleVariable.sheet, styleVariable.handles, ref num, out num2, out variableName2);
							result2 = this.ResolveVariable(variableName2);
						}
						else
						{
							StylePropertyValue spv = new StylePropertyValue
							{
								sheet = styleVariable.sheet,
								handle = handle
							};
							result2 = this.ValidateResolve(spv);
						}
						num++;
					}
					this.m_ResolvedVarStack.Pop();
					result = result2;
				}
			}
			return result;
		}

		private StyleVariableResolver.Result ValidateResolve(StylePropertyValue spv)
		{
			bool flag = this.m_ResolvedValues.Count + 1 > 100;
			StyleVariableResolver.Result result;
			if (flag)
			{
				result = StyleVariableResolver.Result.Invalid;
			}
			else
			{
				this.m_ResolvedValues.Add(spv);
				bool isCustomProperty = this.m_Property.isCustomProperty;
				if (isCustomProperty)
				{
					result = StyleVariableResolver.Result.Valid;
				}
				else
				{
					MatchResult matchResult = this.m_Matcher.Match(this.m_ValidationExpression, this.m_ResolvedValues);
					bool flag2 = !matchResult.success;
					if (flag2)
					{
						this.m_ResolvedValues.RemoveAt(this.m_ResolvedValues.Count - 1);
					}
					result = (matchResult.success ? StyleVariableResolver.Result.Valid : StyleVariableResolver.Result.Invalid);
				}
			}
			return result;
		}

		private StyleVariableResolver.Result ResolveFallback(ref int index)
		{
			StyleVariableResolver.Result result = StyleVariableResolver.Result.Valid;
			while (index < this.m_Handles.Length && result == StyleVariableResolver.Result.Valid)
			{
				StyleValueHandle handle = this.m_Handles[index];
				bool flag = handle.IsVarFunction();
				if (flag)
				{
					int num;
					string variableName;
					StyleVariableResolver.ParseVarFunction(this.m_Sheet, this.m_Handles, ref index, out num, out variableName);
					result = this.ResolveVariable(variableName);
					bool flag2 = result == StyleVariableResolver.Result.NotFound;
					if (flag2)
					{
						bool flag3 = num > 1;
						if (flag3)
						{
							StyleValueHandle[] arg_6D_0 = this.m_Handles;
							int num2 = index + 1;
							index = num2;
							handle = arg_6D_0[num2];
							Debug.Assert(handle.valueType == StyleValueType.FunctionSeparator, string.Format("Unexpected value type {0} in var function", handle.valueType));
							bool flag4 = handle.valueType == StyleValueType.FunctionSeparator && index + 1 < this.m_Handles.Length;
							if (flag4)
							{
								index++;
								result = this.ResolveFallback(ref index);
							}
						}
					}
				}
				else
				{
					StylePropertyValue spv = new StylePropertyValue
					{
						sheet = this.m_Sheet,
						handle = handle
					};
					result = this.ValidateResolve(spv);
				}
				index++;
			}
			return result;
		}

		private static void ParseVarFunction(StyleSheet sheet, StyleValueHandle[] handles, ref int index, out int argCount, out string variableName)
		{
			int num = index + 1;
			index = num;
			argCount = (int)sheet.ReadFloat(handles[num]);
			num = index + 1;
			index = num;
			variableName = sheet.ReadVariable(handles[num]);
		}
	}
}
