using System.Collections;
using System.Collections.Generic;

namespace Pushbullet.Api.Common
{
	public class KeyValuePairList<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
	{
		private readonly List<KeyValuePair<TKey, TValue>> _list = new List<KeyValuePair<TKey, TValue>>();

		internal List<KeyValuePair<TKey, TValue>> InnerList
		{
			get { return _list; }
		}

		public void Add(TKey key, TValue value)
		{
			InnerList.Add(new KeyValuePair<TKey, TValue>(key, value));
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return InnerList.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}