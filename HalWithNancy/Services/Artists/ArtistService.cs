using System;
using System.ComponentModel;
using System.Linq;

namespace HalWithNancy.Services.Artists {
	public class ArtistService : IArtistService {
		readonly Repositories.IArtistRepository _artistRepository;

		public ArtistService(Repositories.IArtistRepository artistRepository) {
			_artistRepository = artistRepository;
		}

		public Shared.IPagedList<ArtistPmo> GetPage(ArtistPagedCriteria criteria) {
			var dbCriteria = new Repositories.ArtistCriteria() {
				Page = criteria.Page,
				PageSize = criteria.PageSize,
				Keywords = (criteria.Keywords ?? string.Empty).Split(' ').ToArray(),
				SortBy = (criteria.SortBy ?? new string[0]).Zip((criteria.SortByDir ?? new string[0]), (k, v) => new { k, v })
				.ToDictionary(x => x.k, x => x.v.Equals("asc", StringComparison.OrdinalIgnoreCase) ? ListSortDirection.Ascending : ListSortDirection.Descending)
			};

			var page = _artistRepository.Page(dbCriteria);

			return new Shared.PagedList<ArtistPmo>(
				page.PageNumber,
				page.PageSize,
				page.TotalResults,
				criteria.Keywords,
				dbCriteria.SortBy,
				page.Data.Select(db => new ArtistPmo() { Id = db.Id, Name = db.Name }).ToList()
			);
		}
	}
}