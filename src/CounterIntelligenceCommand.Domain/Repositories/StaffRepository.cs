using System;
using System.Collections.Generic;
using System.Text;
using CounterIntelligenceCommand.Data.Entities;

namespace CounterIntelligenceCommand.Domain.Repositories
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
