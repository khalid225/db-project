using CounterIntelligenceCommand.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CounterIntelligenceCommand.Domain.Core.Paging;

namespace CounterIntelligenceCommand.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAsync(params int[] id);

        Task<bool> ExistsAsync(int id);

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> Query();

        Task<T> InsertAsync(T entity);

        Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task DeleteAsync(T entity);

        Task<int> SaveChangesAsync();



        Task<PaginatedList<T>> GetAllAsync(int page,
            int limit,
            string sortColumn = null);


        IQueryable<T> Query(Expression<Func<T, bool>> expression);
      

    }
}
