using System;
using System.Collections.Generic;

namespace HalWithNancy {
	public static class EnumerableExtensions {
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> instruction) {
			foreach (var item in collection) {
				instruction.Invoke(item);
			}
			return collection;
		}
	}
}