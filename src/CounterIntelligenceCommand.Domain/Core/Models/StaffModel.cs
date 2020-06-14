using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CounterIntelligenceCommand.Domain.Core
{
    public class StaffModel
    {
        public int Id { get; set; }

        public string Rank { get; set; }

        public int RankId { get; set; }

        public string ArmyNumber { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RetirementDate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }

        public string State { get; set; }
        public int StateId { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
