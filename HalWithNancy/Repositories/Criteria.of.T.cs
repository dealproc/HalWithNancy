namespace HalWithNancy.Repositories {
	public interface ICriteria<T> {
		int? Page { get; set; }
		int? PageSize { get; set; }
	}

	public abstract class Criteria<T> : ICriteria<T> {
		public int? Page { get; set; }
		public int? PageSize { get; set; }
	}

	public class ArtistCriteria : Criteria<DataModels.Artist> { }
}
