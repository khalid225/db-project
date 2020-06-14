using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterIntelligenceCommand.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CounterIntelligenceCommand.Domain.Services
{
    public class StateService: IStateService
    {
        private readonly IStateRepository _stateRepository;
        public StateService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<IDictionary<int, string>> GetStates()
        {
            var states = await _stateRepository.Query()
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .Select(s => new {s.Id, s.Name}).ToListAsync();

                return states.ToDictionary(s => s.Id, s=> s.Name);
        }
    }


}
