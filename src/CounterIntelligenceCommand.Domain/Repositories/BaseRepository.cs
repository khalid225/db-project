using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CounterIntelligenceCommand.Data.Entities;
using CounterIntelligenceCommand.Domain.Core;
using CounterIntelligenceCommand.Domain.Core.Paging;
using Microsoft.EntityFrameworkCore;

namespace CounterIntelligenceCommand.Domain.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    where T : BaseEntity, new()
    {
        protected ApplicationDbContext _dbContext { get; set; }



        public async Task<T> GetAsync(int id) =>
            await _dbContext.FindAsync<T>(id);



        public async Task<IEnumerable<T>> GetAsync(params int[] id) =>
            await _dbContext.Set<T>().Where(el => id.Contains(el.Id)).ToListAsync();



        public Task<bool> ExistsAsync(int id) =>
            _dbContext.Set<T>()
                      .AnyAsync(e => e.Id == id);



        public IQueryable<T> Query() =>
            _dbContext.Set<T>()
                      .AsQueryable();



        public Task<int> SaveChangesAsync() =>
            _dbContext.SaveChangesAsync();



        public IQueryable<T> Query(Expression<Func<T, bool>> expression) =>
            _dbContext.Set<T>()
                      .Where(expression);



        public async Task<T> InsertAsync(T entity)
        {
            entity.DateCreated = DateTime.UtcNow;

            await _dbContext.Set<T>()
                            .AddAsync(entity);

            return entity;
        }



        public Task<T> UpdateAsync(T entity)
        {
            entity.DateLastModified = DateTime.UtcNow;

            _dbContext.Entry(entity)
                      .State = EntityState.Modified;

            return Task.FromResult(entity);
        }



        public Task DeleteAsync(int id)
        {
            var entity = new T
            {
                Id = id
            };

            _dbContext.Entry(entity)
                      .State = EntityState.Deleted;

            return Task.CompletedTask;
        }



        public Task DeleteAsync(T entity)
        {
            _dbContext.Entry(entity)
                      .State = EntityState.Deleted;

            return Task.CompletedTask;
        }



        public async Task<PaginatedList<T>> GetAllAsync(int page,
                                                        int limit,
                                                        string sortColumn = null) =>
            await Query()
               .ToPaginatedListAsync(page,
                                     limit,
                                     sortColumn);



        public async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.DateCreated = DateTime.UtcNow;
                entity.DateLastModified = DateTime.UtcNow;
            }

            await _dbContext.AddRangeAsync(entities);
            return entities;
        }
    }
}
