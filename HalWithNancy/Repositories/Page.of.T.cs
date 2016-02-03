using System.Collections.Generic;

namespace HalWithNancy.Repositories {
	public interface IPage<T> {
		long PageNumber { get; }
		long PageSize { get; }
		long TotalResults { get; }
		IEnumerable<T> Data { get; }
	}

	public class Page<T> : IPage<T> {
		public long PageNumber { get; private set; }
		public long PageSize { get; private set; }
		public long TotalResults { get; private set; }
		public IEnumerable<T> Data { get; private set; }

		public Page(long number, long size, long total, IEnumerable<T> data) {
			PageNumber = number;
			PageSize = size;
			TotalResults = total;
			Data = data;
		}
	}
}
