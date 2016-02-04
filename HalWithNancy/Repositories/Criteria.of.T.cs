using System.Collections.Generic;
using System.ComponentModel;

namespace HalWithNancy.Repositories {
	public interface ICriteria<T> {
		int? Page { get; set; }
		int? PageSize { get; set; }
		string[] Keywords { get; set; }
		IDictionary<string, ListSortDirection> SortBy { get; set; }
	}

	public abstract class Criteria<T> : ICriteria<T> {
		public int? Page { get; set; }
		public int? PageSize { get; set; }
		public string[] Keywords { get; set; }
		public IDictionary<string, ListSortDirection> SortBy { get; set; }
	}

	public class ArtistCriteria : Criteria<DataModels.Artist> { }
}
