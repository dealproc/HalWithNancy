using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

using HalWithNancy.DataModels;

namespace HalWithNancy.Repositories.SqliteRepositories {
	public class ArtistRepository : IArtistRepository {
		readonly string _dbConnectionString = ConfigurationManager.ConnectionStrings["chinook"].ConnectionString;

		public Artist Get(int id) {
			throw new NotImplementedException();
		}

		public Artist GetFirst(Expression<Func<Artist, bool>> condition) {
			throw new NotImplementedException();
		}

		public IPage<Artist> Page(ICriteria<Artist> criteria) {
			using (var cn = new SQLite.SQLiteConnection(_dbConnectionString)) {
				var query = cn.Table<Artist>().AsQueryable();

				var totalRecords = query.Count();

				if (criteria.Keywords.Any()) {
					criteria.Keywords.ToList().ForEach(word => query.Where(a => a.Name.Contains(word)));
				}

				if (criteria.SortBy.Any()) {
					foreach (var kvp in criteria.SortBy) {
						switch (kvp.Value) {
							case ListSortDirection.Ascending:
								query = query.OrderBy(kvp.Key);
								break;
							case ListSortDirection.Descending:
								query = query.OrderBy(kvp.Key + " descending");
								break;
						}
					}
				}

				if (criteria.Page.HasValue) {
					query = query.Skip((criteria.Page.Value - 1) * criteria.PageSize.GetValueOrDefault());
				}

				if (criteria.PageSize.HasValue) {
					query = query.Take(criteria.PageSize.Value);
				}

				return new Page<Artist>(criteria.Page ?? 0, criteria.PageSize ?? totalRecords, totalRecords, query.ToList());
			}
		}
	}
}