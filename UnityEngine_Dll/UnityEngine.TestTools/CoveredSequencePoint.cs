using System;
using System.Reflection;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	[NativeType(CodegenOptions.Custom, "ManagedCoveredSequencePoint", Header = "Runtime/Scripting/ScriptingCoverage.bindings.h")]
	public struct CoveredSequencePoint
	{
		public MethodBase method;

		public uint ilOffset;

		public uint hitCount;

		public string filename;

		public uint line;

		public uint column;
	}
}
