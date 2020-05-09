﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CounterIntelligenceCommand.Data.Entities;
using CounterIntelligenceCommand.Domain.Core.Paging;
using Microsoft.EntityFrameworkCore;

namespace CounterIntelligenceCommand.Domain.Core
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query,
                                                                           int pageIndex,
                                                                           int limit,
                                                                           string sortColumn = null)
        {
            int totalCount;

            // it might be faster to use a type check:
            // if query.Provider is IAsyncQueryProvider,
            // but this type is internally used and can potentially break.
            // The cost difference is not significant in this application.
            try
            {
                totalCount = await query.CountAsync();
            }
            catch (InvalidOperationException)
            {
                var list = query.ToList();
                query = list.AsQueryable();
                totalCount = list.Count;
            }

            var collection = query;
            if (sortColumn != null)
            {
                collection = query
                   .OrderBy(sortColumn, false);
            }

            collection = collection.Skip((pageIndex - 1) * limit)
                                   .Take(limit);

            ICollection<T> rows;

            try
            {
                rows = await collection.ToListAsync();
            }
            catch (InvalidOperationException)
            {
                rows = collection.ToList();
            }

            return new PaginatedList<T>(rows, totalCount);
        }



        public static IQueryable<Staff> SearchStaff(this IQueryable<Staff> query,
                                                         string searchFilter = default)
        {
            if (searchFilter == null)
            {
                return query;
            }

            return query.Where(s =>
                                   EF.Functions.Contains(s.ArmyNumber, searchFilter) ||
                                   EF.Functions.Contains(s.FirstName, searchFilter) ||
                                   EF.Functions.Contains(s.LastName, searchFilter) ||
                                   EF.Functions.Contains(s.MiddleName, searchFilter) ||
                                   EF.Functions.Contains(s.PhoneNumber, searchFilter));
        }



        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T, TKey>(this IQueryable<T> query,
                                                                                 int pageIndex,
                                                                                 int limit,
                                                                                 Expression<Func<T, TKey>> keySelector)
            where T : class => await query.ToPaginatedListAsync(pageIndex, limit, keySelector.Name);



        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source,
                                                           string orderByProperty,
                                                           bool desc)
        {
            var command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable),
                                                   command,
                                                   new Type[] { type, property.PropertyType },
                                                   source.Expression,
                                                   Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
