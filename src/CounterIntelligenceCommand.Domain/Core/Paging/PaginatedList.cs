using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterIntelligenceCommand.Domain.Core.Paging
{
    public class PaginatedList<T>
    {
        public PaginatedList(IEnumerable<T> source, int count)
        {
            TotalCount = count;
            Rows = source;
        }

        public PaginatedList(IQueryable<T> source, int count)
        {
            TotalCount = count;
            Rows = source;
        }



        public PaginatedList(ICollection<T> source, int count)
        {
            TotalCount = count;
            Rows = source;
        }

        

        public int TotalCount { get; }

        public IEnumerable<T> Rows { get; }

        public static PaginatedList<T> Default => new PaginatedList<T>(Array.Empty<T>(), 0);
    }
}
