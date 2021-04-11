using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Android
{
	public class PermissionCallbacks : AndroidJavaProxy
	{
		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<string> PermissionGranted;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<string> PermissionDenied;

		[method: CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
		public event Action<string> PermissionDeniedAndDontAskAgain;

		public PermissionCallbacks() : base("com.unity3d.player.IPermissionRequestCallbacks")
		{
		}

		private void onPermissionGranted(string permissionName)
		{
			Action<string> expr_07 = this.PermissionGranted;
			if (expr_07 != null)
			{
				expr_07(permissionName);
			}
		}

		private void onPermissionDenied(string permissionName)
		{
			Action<string> expr_07 = this.PermissionDenied;
			if (expr_07 != null)
			{
				expr_07(permissionName);
			}
		}

		private void onPermissionDeniedAndDontAskAgain(string permissionName)
		{
			bool flag = this.PermissionDeniedAndDontAskAgain != null;
			if (flag)
			{
				this.PermissionDeniedAndDontAskAgain(permissionName);
			}
			else
			{
				Action<string> expr_26 = this.PermissionDenied;
				if (expr_26 != null)
				{
					expr_26(permissionName);
				}
			}
		}
	}
}
