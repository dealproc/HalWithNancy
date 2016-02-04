using System;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

using HalWithNancy.DataModels;

namespace HalWithNancy.Repositories.SqliteRepositories {
	public class AlbumRepository : IRepository<Album>, IRepositoryProjection<Album> {
		readonly string _dbConnectionString = ConfigurationManager.ConnectionStrings["chinook"].ConnectionString;

		public Album Get(int id) {
			throw new NotImplementedException();
		}

		public Album GetFirst(Expression<Func<Album, bool>> condition) {
			throw new NotImplementedException();
		}

		public IPage<Album> Page(ICriteria<Album> criteria) {
			throw new NotImplementedException();
		}

		public TOut Project<TOut>(Func<IQueryable<Album>, TOut> selector) {
			using (var cn = new SQLite.SQLiteConnection(_dbConnectionString)) {
				var q = cn.Table<Album>().AsQueryable();
				return selector.Invoke(q);
			}
		}

		public TOut Project<T1, TOut>(Func<IQueryable<Album>, IQueryable<T1>, TOut> selector) where T1 : class, new() {
			using (var cn = new SQLite.SQLiteConnection(_dbConnectionString)) {
				var q = cn.Table<Album>().AsQueryable();
				var q1 = cn.Table<T1>().AsQueryable();
				return selector.Invoke(q, q1);
			}
		}

		//public TOut Project<T1, TOut>(Func<IQueryable<T1>, TOut> selector) where T1 : class, new() {
		//	using (var cn = new SQLite.SQLiteConnection(_dbConnectionString)) {
		//		var q1 = cn.Table<T1>().AsQueryable();
		//		return selector.Invoke(q1);
		//	}
		//}

		//public TOut Project<T1, T2, TOut>(Func<IQueryable<T1>, IQueryable<T2>, TOut> selector) where T1 : class, new() where T2 : class, new() {
		//	using (var cn = new SQLite.SQLiteConnection(_dbConnectionString)) {
		//		var q1 = cn.Table<T1>().AsQueryable();
		//		var q2 = cn.Table<T2>().AsQueryable();
		//		return selector.Invoke(q1, q2);
		//	}
		//}
	}
}