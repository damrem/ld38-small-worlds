//using System;
//using System.Collections;
using System.Collections.Generic;

namespace Ext
{
	public static class ListExt
	{
		public static List<T> Flatten<T>(this List<List<T>> listList, bool dedupe=true)
		{
			List<T> flat = new List<T> ();

			listList.ForEach (delegate(List<T> subList) {
				subList.ForEach(delegate(T item) {
					flat.Add(item);
				});
			});

			if (dedupe)
				flat = Deduped (flat);

			return flat;
		}

		public static List<T> Deduped<T>(this List<T> list)
		{
			List<T> deduped = new List<T> ();
			list.ForEach (delegate (T item){
				if(!deduped.Contains(item))
					deduped.Add(item);
			});
			return deduped;
		}

		public static T[] ToArray<T>(this List<T> list)
		{
			T[] array = new T[list.Count];
			for (int i = 0; i < list.Count; i++) {
				array [i] = list [i];
			}
			return array;
		}
	}


}

