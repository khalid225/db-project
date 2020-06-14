using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CounterIntelligenceCommand.Domain.Services
{
    public interface IStateService
    {
        Task<IDictionary<int, string>> GetStates();
    }
}
