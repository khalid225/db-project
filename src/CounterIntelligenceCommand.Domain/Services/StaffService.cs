using CounterIntelligenceCommand.Data;
using CounterIntelligenceCommand.Data.Entities;
using CounterIntelligenceCommand.Domain.Core;
using CounterIntelligenceCommand.Domain.Core.Paging;
using CounterIntelligenceCommand.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CounterIntelligenceCommand.Domain.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }



        public async Task<BaseResponse> AddStaff(string firstName, string lastName, string middleName,
            string armyNumber, string phoneNumber, string address, int rank, int state, DateTime birthDate,
            DateTime retirementDate)
        {
            var exists = await _staffRepository.ExistsAsync(s => s.ArmyNumber == armyNumber);
            if (exists)
            {
                return new BaseResponse(Operation.Failed, $"{ResponseCode.StaffExists:D}",
                    string.Format(Operation.StaffExists, armyNumber));
            }
            else
            {
                var staff = new Staff
                {
                    ArmyNumber = armyNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    BirthDate = birthDate,
                    RetirementDate = retirementDate,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Rank = (Rank) rank,
                    StateId = state
                };
                await _staffRepository.InsertAsync(staff);
                await _staffRepository.SaveChangesAsync();
                return new BaseResponse(Operation.Successful, $"{ResponseCode.Successfull:D}",
                    string.Format(Operation.AddStaffSuccessfully, $"{firstName} {lastName}", armyNumber));
            }
        }

        public async Task<BaseResponse> UpdateStaff(int staffId, string firstName, string lastName, string middleName,
            string armyNumber, string phoneNumber, string address, int rank, int state, DateTime birthDate,
            DateTime retirementDate)
        {
            var exists = await _staffRepository.ExistsAsync(s => s.ArmyNumber == armyNumber && s.Id != staffId);
            if (exists)
            {
                return new BaseResponse(Operation.Failed, $"{ResponseCode.StaffExists:D}",
                    string.Format(Operation.StaffExists, armyNumber));
            }
            var staff = await _staffRepository.GetAsync(staffId);
            if (staff == null)
            {
                return new BaseResponse(Operation.Failed, $"{ResponseCode.InvalidStaffCode:D}",
                    string.Format(Operation.NotFFound, "staff"));
            }
            else
            {
                staff.FirstName = firstName;
                staff.LastName = lastName;
                staff.ArmyNumber = armyNumber;
                staff.Rank = (Rank) rank;
                staff.MiddleName = middleName;
                staff.Address = address;
                staff.BirthDate = birthDate;
                staff.RetirementDate = retirementDate;
                staff.PhoneNumber = phoneNumber;
                staff.StateId = state;
                await _staffRepository.UpdateAsync(staff);
                await _staffRepository.SaveChangesAsync();
                return new BaseResponse(Operation.Successful, $"{ResponseCode.Successfull:D}",
                    string.Format(Operation.AddStaffSuccessfully, $"{firstName} {lastName}", armyNumber));
            }
        }

        public async Task<StaffModel> GetStaffAsync(int id)
        {
            var staff = await _staffRepository.Query(s => s.Id == id)
                .Include(s => s.State)
                .FirstOrDefaultAsync();
            if (staff == null)
            {
                return null;
            }

            return new StaffModel
            {
                Id = staff.Id,
                State = staff.State.Name,
                BirthDate = staff.BirthDate,
                RetirementDate = staff.RetirementDate,
                PhoneNumber = staff.PhoneNumber,
                Address = staff.Address,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                MiddleName = staff.MiddleName,
                ArmyNumber = staff.ArmyNumber,
                Rank = EnumExtensions.GetDescription(staff.Rank),
                DateCreated = staff.DateCreated,
                DateLastModified = staff.DateLastModified,
                StateId = staff.StateId,
                RankId = (int)staff.Rank
            };
        }


        public async Task<PaginatedList<StaffModel>> GetStaffsAsync(int page,
            int limit,
            string sortColumn = null) =>
            await _staffRepository.Query()
                .Include(s => s.State)
                .Select(s => new StaffModel
                {
                    Id = s.Id,
                    State = s.State.Name,
                    BirthDate = s.BirthDate,
                    RetirementDate = s.RetirementDate,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    ArmyNumber = s.ArmyNumber,
                    Rank = EnumExtensions.GetDescription(s.Rank),
                    DateCreated = s.DateCreated,
                    DateLastModified = s.DateLastModified
                })
                .ToPaginatedListAsync(page,
                    limit,
                    sortColumn);
    }
}
