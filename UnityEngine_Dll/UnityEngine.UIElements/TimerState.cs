using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	public struct TimerState : IEquatable<TimerState>
	{
		public long start
		{
			[IsReadOnly]
			get;
			set;
		}

		public long now
		{
			[IsReadOnly]
			get;
			set;
		}

		public long deltaTime
		{
			get
			{
				return this.now - this.start;
			}
		}

		public override bool Equals(object obj)
		{
			return obj is TimerState && this.Equals((TimerState)obj);
		}

		public bool Equals(TimerState other)
		{
			return this.start == other.start && this.now == other.now && this.deltaTime == other.deltaTime;
		}

		public override int GetHashCode()
		{
			int num = 540054806;
			num = num * -1521134295 + this.start.GetHashCode();
			num = num * -1521134295 + this.now.GetHashCode();
			return num * -1521134295 + this.deltaTime.GetHashCode();
		}

		public static bool operator ==(TimerState state1, TimerState state2)
		{
			return state1.Equals(state2);
		}

		public static bool operator !=(TimerState state1, TimerState state2)
		{
			return !(state1 == state2);
		}
	}
}
