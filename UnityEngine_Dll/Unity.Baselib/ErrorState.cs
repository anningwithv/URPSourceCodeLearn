using System;
using System.Runtime.InteropServices;
using Unity.Baselib.LowLevel;

namespace Unity.Baselib
{
	internal struct ErrorState
	{
		private Binding.Baselib_ErrorState nativeErrorState;

		public Binding.Baselib_ErrorCode ErrorCode
		{
			get
			{
				return this.nativeErrorState.code;
			}
		}

		public unsafe Binding.Baselib_ErrorState* NativeErrorStatePtr
		{
			get
			{
				return &this.nativeErrorState;
			}
		}

		public void ThrowIfFailed()
		{
			bool flag = this.ErrorCode > Binding.Baselib_ErrorCode.Success;
			if (flag)
			{
				throw new BaselibException(this);
			}
		}

		public unsafe string Explain(Binding.Baselib_ErrorState_ExplainVerbosity verbosity = Binding.Baselib_ErrorState_ExplainVerbosity.ErrorType_SourceLocation_Explanation)
		{
			Binding.Baselib_ErrorState* errorState = &this.nativeErrorState;
			uint num = Binding.Baselib_ErrorState_Explain(errorState, null, 0u, verbosity) + 1u;
			IntPtr intPtr = Binding.Baselib_Memory_Allocate(new UIntPtr(num));
			string result;
			try
			{
				Binding.Baselib_ErrorState_Explain(errorState, (byte*)((void*)intPtr), num, verbosity);
				result = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Binding.Baselib_Memory_Free(intPtr);
			}
			return result;
		}
	}
}
