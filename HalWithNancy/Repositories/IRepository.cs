using System;
using System.Linq;
using System.Linq.Expressions;

using HalWithNancy.DataModels;

namespace HalWithNancy.Repositories {
	public interface IRepository<T> {
		T Get(int id);
		T GetFirst(Expression<Func<T, bool>> condition);
		IPage<T> Page(ICriteria<T> criteria);
	}

	public interface IRepositoryProjection<TEntity> {
		TOut Project<TOut>(Func<IQueryable<TEntity>, TOut> selector);
		TOut Project<T1, TOut>(Func<IQueryable<TEntity>, IQueryable<T1>, TOut> selector) where T1 : class, new();
		//TOut Project<T1, T2, T3, TOut>(Func<IQueryable<T1>, IQueryable<T2>, IQueryable<T3>, TOut> selector);
		//TOut Project<T1, T2, T3, T4, TOut>(Func<IQueryable<T1>, IQueryable<T2>, IQueryable<T3>, IQueryable<T4>, TOut> selector);
	}

	public interface IArtistRepository : IRepository<Artist> { }
	public interface IAlbumRepository : IRepository<Album> { }
}
