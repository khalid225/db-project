using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CounterIntelligenceCommand.Data;
using CounterIntelligenceCommand.Domain.Core;
using CounterIntelligenceCommand.Domain.Core.Paging;

namespace CounterIntelligenceCommand.Domain.Services
{
    public interface IStaffService
    {
        Task<StaffModel> GetStaffAsync(int id);
        Task<PaginatedList<StaffModel>> GetStaffsAsync(int page,
            int limit,
            string sortColumn = null);

        Task<BaseResponse> UpdateStaff(int staffId, string firstName, string lastName, string middleName,
            string armyNumber, string phoneNumber, string address, int rank, int state, DateTime birthDate,
            DateTime retirementDate);
        Task<BaseResponse> AddStaff(string firstName, string lastName, string middleName, string armyNumber,
            string phoneNumber, string address, int rank, int state, DateTime birthDate, DateTime retirementDate);
    }
}
