using System;

namespace CounterIntelligenceCommand.Data.Entities
{
    public  class Staff: BaseEntity
    {
        public string ArmyNumber { get; set; }

        public Rank Rank { get; set; }
        
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
       
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RetirementDate { get; set; }
        
        public int StateId { get; set; }

        public State State { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
