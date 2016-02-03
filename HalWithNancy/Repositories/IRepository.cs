using System;
using System.Linq.Expressions;

using HalWithNancy.DataModels;

namespace HalWithNancy.Repositories {
	public interface IRepository<T> {
		T Get(int id);
		T GetFirst(Expression<Func<T, bool>> condition);
		IPage<T> Page(ICriteria<T> criteria);
	}

	public interface IArtistRepository : IRepository<Artist> { }
}
