using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CounterIntelligenceCommand.Web.Models
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;

        public bool ShowNext => CurrentPage < TotalPages;

        public string Route { get; set; } = string.Empty;
    }
}
