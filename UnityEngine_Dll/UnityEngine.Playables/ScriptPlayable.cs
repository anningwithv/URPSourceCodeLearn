using System;

namespace UnityEngine.Playables
{
	public struct ScriptPlayable<T> : IPlayable, IEquatable<ScriptPlayable<T>> where T : class, IPlayableBehaviour, new()
	{
		private PlayableHandle m_Handle;

		private static readonly ScriptPlayable<T> m_NullPlayable = new ScriptPlayable<T>(PlayableHandle.Null);

		public static ScriptPlayable<T> Null
		{
			get
			{
				return ScriptPlayable<T>.m_NullPlayable;
			}
		}

		public static ScriptPlayable<T> Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = ScriptPlayable<T>.CreateHandle(graph, default(T), inputCount);
			return new ScriptPlayable<T>(handle);
		}

		public static ScriptPlayable<T> Create(PlayableGraph graph, T template, int inputCount = 0)
		{
			PlayableHandle handle = ScriptPlayable<T>.CreateHandle(graph, template, inputCount);
			return new ScriptPlayable<T>(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, T template, int inputCount)
		{
			bool flag = template == null;
			object obj;
			if (flag)
			{
				obj = ScriptPlayable<T>.CreateScriptInstance();
			}
			else
			{
				obj = ScriptPlayable<T>.CloneScriptInstance(template);
			}
			bool flag2 = obj == null;
			PlayableHandle result;
			if (flag2)
			{
				string arg_4C_0 = "Could not create a ScriptPlayable of Type ";
				Type expr_40 = typeof(T);
				Debug.LogError(arg_4C_0 + ((expr_40 != null) ? expr_40.ToString() : null));
				result = PlayableHandle.Null;
			}
			else
			{
				PlayableHandle playableHandle = graph.CreatePlayableHandle();
				bool flag3 = !playableHandle.IsValid();
				if (flag3)
				{
					result = PlayableHandle.Null;
				}
				else
				{
					playableHandle.SetInputCount(inputCount);
					playableHandle.SetScriptInstance(obj);
					result = playableHandle;
				}
			}
			return result;
		}

		private static object CreateScriptInstance()
		{
			bool flag = typeof(ScriptableObject).IsAssignableFrom(typeof(T));
			IPlayableBehaviour result;
			if (flag)
			{
				result = (ScriptableObject.CreateInstance(typeof(T)) as T);
			}
			else
			{
				result = Activator.CreateInstance<T>();
			}
			return result;
		}

		private static object CloneScriptInstance(IPlayableBehaviour source)
		{
			UnityEngine.Object @object = source as UnityEngine.Object;
			bool flag = @object != null;
			object result;
			if (flag)
			{
				result = ScriptPlayable<T>.CloneScriptInstanceFromEngineObject(@object);
			}
			else
			{
				ICloneable cloneable = source as ICloneable;
				bool flag2 = cloneable != null;
				if (flag2)
				{
					result = ScriptPlayable<T>.CloneScriptInstanceFromIClonable(cloneable);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		private static object CloneScriptInstanceFromEngineObject(UnityEngine.Object source)
		{
			UnityEngine.Object @object = UnityEngine.Object.Instantiate(source);
			bool flag = @object != null;
			if (flag)
			{
				@object.hideFlags |= HideFlags.DontSave;
			}
			return @object;
		}

		private static object CloneScriptInstanceFromIClonable(ICloneable source)
		{
			return source.Clone();
		}

		internal ScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !typeof(T).IsAssignableFrom(handle.GetPlayableType());
				if (flag2)
				{
					throw new InvalidCastException(string.Format("Incompatible handle: Trying to assign a playable data of type `{0}` that is not compatible with the PlayableBehaviour of type `{1}`.", handle.GetPlayableType(), typeof(T)));
				}
			}
			this.m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		public T GetBehaviour()
		{
			return this.m_Handle.GetObject<T>();
		}

		public static implicit operator Playable(ScriptPlayable<T> playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator ScriptPlayable<T>(Playable playable)
		{
			return new ScriptPlayable<T>(playable.GetHandle());
		}

		public bool Equals(ScriptPlayable<T> other)
		{
			return this.GetHandle() == other.GetHandle();
		}
	}
}
