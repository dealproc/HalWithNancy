using System.Linq;

namespace HalWithNancy.Services.Artists {
	public class ArtistService : IArtistService {
		readonly Repositories.IArtistRepository _artistRepository;

		public ArtistService(Repositories.IArtistRepository artistRepository) {
			_artistRepository = artistRepository;
		}

		public Shared.IPagedList<ArtistPmo> GetPage(ArtistPagedCriteria criteria) {
			var dbCriteria = new Repositories.ArtistCriteria() { Page = criteria.Page, PageSize = criteria.PageSize };
			var page = _artistRepository.Page(dbCriteria);

			return new Shared.PagedList<ArtistPmo>(
				page.PageNumber, 
				page.PageSize, 
				page.TotalResults, 
				page.Data.Select(db => new ArtistPmo() { Id = db.Id, Name = db.Name }).ToList()
			);
		}
	}
}