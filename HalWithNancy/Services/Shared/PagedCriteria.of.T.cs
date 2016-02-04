using System;

namespace HalWithNancy.Services.Shared {
	public interface IPagedCriteria {
		int? Page { get; set; }
		int? PageSize { get; set; }
		string Keywords { get; set; }
		string[] SortBy { get; set; }
		string[] SortByDir { get; set; }
	}

	public abstract class PagedCriteria : IPagedCriteria {
		public int? Page { get; set; }
		public int? PageSize { get; set; }
		public string Keywords { get; set; }
		public string[] SortBy { get; set; }
		public string[] SortByDir { get; set; }
	}
}
