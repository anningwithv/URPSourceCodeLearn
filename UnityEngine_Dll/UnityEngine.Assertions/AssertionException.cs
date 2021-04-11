using System;

namespace UnityEngine.Assertions
{
	public class AssertionException : Exception
	{
		private string m_UserMessage;

		public override string Message
		{
			get
			{
				string text = base.Message;
				bool flag = this.m_UserMessage != null;
				if (flag)
				{
					text = this.m_UserMessage + "\n" + text;
				}
				return text;
			}
		}

		public AssertionException(string message, string userMessage) : base(message)
		{
			this.m_UserMessage = userMessage;
		}
	}
}
