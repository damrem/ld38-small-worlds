//using System;
using System.Collections.Generic;
using Helpers;

namespace Ext
{
	public static class ArrayExt
	{
		public delegate U Callback<T, U>(T item);

		public static U[] Map<T, U>(this T[] array, Callback<T, U> callback)
		{
			int length = array.Length;

//			T[] output = new T[length]();
			var output = new U[length];

			for (int i = 0; i < length; i++)
				output [i] = callback (array [i]);
			
			return output;
		}

		public static string Join<T>(this T[] array, string separator=",")
		{
			string r = "";
			foreach (T item in array) {
				r += item.ToString () + separator;
			}
			r = r.Remove (r.Length - 1 - separator.Length);
			return r;
		}

		public static string Join<T>(this T[,] array, string separator, string superSeparator)
		{
			string r = "";
			for (int i = 0; i < array.GetLength (0); i++) {
				for (int j = 0; j < array.GetLength (1); j++) {
					T item = array [i, j];
					r += (item != null) ? item.ToString () : "null";
					r += separator;
				}
				r = r.Remove (r.Length - 1 - separator.Length);
				r += superSeparator;
			}
			r = r.Remove (r.Length - 1 - superSeparator.Length);
			return r;
		}

		public static T[] Filter<T>(this T[] array, Delegate.TBool<T> callback)
		{
			List<T> filteredList = new List<T> ();

			foreach (T item in array) {
				if (callback (item))
					filteredList.Add (item);
			}
			return filteredList.ToArray ();
		}

		public static T[] Filter<T>(this T[,] array2d, Delegate.TBool<T> callback)
		{
			List<T> filteredList = new List<T> ();

			foreach (T item in array2d) {
				if (callback (item))
					filteredList.Add (item);
			}
			return filteredList.ToArray ();
		}

		public static int Max(this int[] array)
		{
			int max = array [0];
			foreach (int val in array) {
				if (val > max)
					max = val;
			}
			return max;
		}

		public static int Min(this int[] array)
		{
			int min = array [0];
			foreach (int val in array) {
				if (val < min)
					min = val;
			}
			return min;
		}

		public static List<T> ToList<T>(this T[] array)
		{
			List<T> list = new List<T> ();
			foreach (T item in array)
				list.Add (item);
			return list;
		}

		public static T[] Unshifted<T>(this T[] array, params T[] items)
		{
			T[] unshifted = new T[array.Length + items.Length];
			for (int i = 0; i < items.Length; i++) {
				unshifted [i] = items [i];
			}
			for (int i = 0; i < array.Length; i++) {
				unshifted [items.Length + i] = array [i];
			}
			return unshifted;
		}


	}
}

