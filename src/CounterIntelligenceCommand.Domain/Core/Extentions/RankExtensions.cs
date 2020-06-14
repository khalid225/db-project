using CounterIntelligenceCommand.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterIntelligenceCommand.Domain.Core
{
    public static class RankExtensions
    {
        public static IDictionary<int, string> GetRanks()
        {
            var values = Enum.GetValues(typeof(Rank))
                .Cast<Rank>()
                .ToDictionary(k => (int)k, EnumExtensions.GetDescription);
            return values;
        }
    }
}
