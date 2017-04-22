using System.Collections.Generic;

namespace Ext
{
	public static class DictionaryExt
	{
		public static V Get<K, V> (this Dictionary<K, V> dictionary, K key)
		{
			V val;
			dictionary.TryGetValue (key, out val);
			return val;
		}

		public static void Set<K, V>(this Dictionary<K,V> dictionary, K key, V val)
		{
			dictionary [key] = val;
		}
	}
}

