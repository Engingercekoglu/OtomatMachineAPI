using OtomatMachine.Entity.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace OtomatMachine.Bussiness.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        public Repository(DataContext dataContext)
        {
            _context = dataContext;
            this.Table = _context.Set<T>();
        }

        public DbSet<T> Table { get; set; }


        public async Task<T> Add(T entity)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var insertedModel = await Table.AddAsync(entity);
                    if (await Save() > 0)
                    {
                        ts.Complete();
                        return insertedModel.Entity;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var updatedModel = Table.Update(entity);
                    if (await Save() > 0)
                    {
                        ts.Complete();
                        return updatedModel.Entity;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> Delete(T entity)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var deletedModel = Table.Remove(entity);
                    if (await Save() > 0)
                    {
                        ts.Complete();
                        return deletedModel.Entity;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeParameters)
        {
            IQueryable<T> queryable = Table;
            foreach (Expression<Func<T, object>> includeParameter in includeParameters)
            {
                queryable = queryable.Include(includeParameter);
            }
            if (predicate == null)
            {
                return queryable;
            }
            return queryable.Where(predicate).AsQueryable();
        }

        public async Task<T> GetByParam(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeParameters)
        {
            IQueryable<T> queryable = Table;
            foreach (Expression<Func<T, object>> includeParameter in includeParameters)
            {
                queryable = queryable.Include(includeParameter);
            }
            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<int> Save()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Any(Expression<Func<T, bool>> predicate) => await Table.AnyAsync(predicate);
    }
}
