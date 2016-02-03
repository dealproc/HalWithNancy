using System.Collections.Generic;

namespace HalWithNancy.Services.Shared {
	public interface IPagedList<T> {
		long PageNumber { get; }
		long PageSize { get; }
		long TotalResults { get; }
		IEnumerable<T> Data { get; }
	}

	public class PagedList<T> : IPagedList<T> {
		public long PageNumber { get; private set; }
		public long TotalPages => TotalResults % PageSize == 0 ? TotalResults / PageSize : (TotalResults / PageSize) + 1;
		public long PageSize { get; private set; }
		public long TotalResults { get; private set; }
		public IEnumerable<T> Data { get; private set; }

		public PagedList(long pageNumber, long pageSize, long totalResults, IEnumerable<T> data) {
			PageNumber = pageNumber;
			PageSize = pageSize;
			TotalResults = totalResults;
			Data = data;
		}
	}
}
