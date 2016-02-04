using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic;


using HalWithNancy.Repositories;
using HalWithNancy.Services.Shared;

namespace HalWithNancy.Services.Album {
	public class AlbumService : IAlbumService {
		readonly IRepositoryProjection<DataModels.Album> _projector;

		public AlbumService(IRepositoryProjection<DataModels.Album> projector) {
			_projector = projector;
		}

		public IPagedList<AlbumPmo> GetPage(AlbumPagedCriteria criteria) {
			return _projector.Project<DataModels.Artist, IPagedList<AlbumPmo>>(
				(albums, artists) => {
					var keywords = (criteria.Keywords ?? string.Empty).Split(' ').ToArray();
					var sortBy = (criteria.SortBy ?? new string[0]).Zip((criteria.SortByDir ?? new string[0]), (k, v) => new { k, v })
						.ToDictionary(x => x.k, x => x.v.Equals("asc", StringComparison.OrdinalIgnoreCase) ? ListSortDirection.Ascending : ListSortDirection.Descending);

					var query = from album in albums
								join artist in artists on album.ArtistId equals artist.Id
								select new { album, artist };

					if (keywords.Any()) {
						keywords.ForEach(kwd => query = query.Where(v => v.album.Title.ToLower().Contains(kwd.ToLower()) || v.artist.Name.ToLower().Contains(kwd.ToLower())));
					}

					var projected = query.Select((aa) => new AlbumPmo() { Id = aa.album.Id, ArtistId = aa.artist.Id, Artist = aa.artist.Name, Title = aa.album.Title });

					var total = projected.Count();

					if (sortBy.Any()) {
						projected = projected.OrderBy(
							string.Join(",", 
								sortBy.Select(x => string.Format("{0} {1}", x.Key, Enum.GetName(typeof(ListSortDirection), x.Value).ToString().ToLower()))
							)
						);
					}

					if (criteria.Page.HasValue) {
						projected = projected.Skip((criteria.Page.Value - 1) * criteria.PageSize.GetValueOrDefault());
					}

					if (criteria.PageSize.HasValue) {
						projected = projected.Take(criteria.PageSize.Value);
					}

					return new PagedList<AlbumPmo>(
						criteria.Page ?? 0,
						criteria.PageSize ?? total,
						total,
						criteria.Keywords,
						sortBy,
						projected.ToList()
					);
				}
			);
		}
	}
}