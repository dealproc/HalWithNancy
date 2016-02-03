namespace HalWithNancy.Services {
	public interface IArtistService {
		Shared.IPagedList<Artists.ArtistPmo> GetPage(Artists.ArtistPagedCriteria criteria);
	}
}
