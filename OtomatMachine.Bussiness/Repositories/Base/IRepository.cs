using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OtomatMachine.Bussiness.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);

        Task<T> GetByParam(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeParameters);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeParameters);
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<int> Save();

    }
}
