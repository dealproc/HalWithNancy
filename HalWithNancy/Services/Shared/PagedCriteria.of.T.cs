namespace HalWithNancy.Services.Shared {
	public interface IPagedCriteria {
		int? Page { get; set; }
		int? PageSize { get; set; }
	}

	public abstract class PagedCriteria : IPagedCriteria {
		public int? Page { get; set; }
		public int? PageSize { get; set; }
	}
}
