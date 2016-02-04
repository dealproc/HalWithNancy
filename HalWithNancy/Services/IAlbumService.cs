namespace HalWithNancy.Services {
	public interface IAlbumService {
		Shared.IPagedList<Album.AlbumPmo> GetPage(Album.AlbumPagedCriteria criteria);
	}
}