using System.Collections.Generic;
using System.ComponentModel;

namespace HalWithNancy.Services.Shared {
	public interface IPagedList<T> {
		long PageNumber { get; }
		long PageSize { get; }
		long TotalResults { get; }
		long TotalPages { get; }
		string Keywords { get; }
		IDictionary<string, ListSortDirection> SortedBy { get; }
		IEnumerable<T> Data { get; }
	}

	public class PagedList<T> : IPagedList<T> {
		public long PageNumber { get; private set; }
		public long TotalPages => TotalResults % PageSize == 0 ? TotalResults / PageSize : (TotalResults / PageSize) + 1;
		public long PageSize { get; private set; }
		public long TotalResults { get; private set; }
		public IEnumerable<T> Data { get; private set; }
		public string Keywords { get; private set; }
		public IDictionary<string,ListSortDirection> SortedBy { get; private set; }

		public PagedList(long pageNumber, long pageSize, long totalResults, string keywords, IDictionary<string, ListSortDirection> sortedBy, IEnumerable<T> data) {
			PageNumber = pageNumber;
			PageSize = pageSize;
			TotalResults = totalResults;
			Keywords = keywords;
			SortedBy = sortedBy;
			Data = data;
		}
	}
}
