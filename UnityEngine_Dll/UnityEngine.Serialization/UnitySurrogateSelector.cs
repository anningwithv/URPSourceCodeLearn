using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	public class UnitySurrogateSelector : ISurrogateSelector
	{
		public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			bool isGenericType = type.IsGenericType;
			ISerializationSurrogate result;
			if (isGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				bool flag = genericTypeDefinition == typeof(List<>);
				if (flag)
				{
					selector = this;
					result = ListSerializationSurrogate.Default;
					return result;
				}
				bool flag2 = genericTypeDefinition == typeof(Dictionary<, >);
				if (flag2)
				{
					selector = this;
					Type type2 = typeof(DictionarySerializationSurrogate<, >).MakeGenericType(type.GetGenericArguments());
					result = (ISerializationSurrogate)Activator.CreateInstance(type2);
					return result;
				}
			}
			selector = null;
			result = null;
			return result;
		}

		public void ChainSelector(ISurrogateSelector selector)
		{
			throw new NotImplementedException();
		}

		public ISurrogateSelector GetNextSelector()
		{
			throw new NotImplementedException();
		}
	}
}
