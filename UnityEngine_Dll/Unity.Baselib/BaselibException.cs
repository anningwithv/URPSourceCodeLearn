using System;
using Unity.Baselib.LowLevel;

namespace Unity.Baselib
{
	internal class BaselibException : Exception
	{
		private readonly ErrorState errorState;

		public Binding.Baselib_ErrorCode ErrorCode
		{
			get
			{
				return this.errorState.ErrorCode;
			}
		}

		internal BaselibException(ErrorState errorState) : base(errorState.Explain(Binding.Baselib_ErrorState_ExplainVerbosity.ErrorType_SourceLocation_Explanation))
		{
			this.errorState = errorState;
		}
	}
}
