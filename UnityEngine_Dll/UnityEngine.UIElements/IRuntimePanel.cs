using System;

namespace UnityEngine.UIElements
{
	internal interface IRuntimePanel
	{
		FocusController focusController
		{
			get;
			set;
		}

		IEventInterpreter eventInterpreter
		{
			get;
			set;
		}

		EventDispatcher dispatcher
		{
			get;
			set;
		}
	}
}
