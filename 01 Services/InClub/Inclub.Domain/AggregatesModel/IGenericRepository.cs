using Inclub.Domain.SeedWork;
using System;
using System.Linq;

namespace Inclub.Domain.AggregatesModel
{
    public interface IGenericRepository<T> where T : Entity
	{
		T Add(T entity);
		T Update(T entity);
		T Remove(T entity);
		T GetById(int Id);
		IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<System.Linq.IQueryable<T>, System.Linq.IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool asNoTracking = false);
	}
}
