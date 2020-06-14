using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CounterIntelligenceCommand.Data.Entities;

namespace CounterIntelligenceCommand.Domain.Repositories
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
