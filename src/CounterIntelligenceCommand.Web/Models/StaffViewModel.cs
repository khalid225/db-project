using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CounterIntelligenceCommand.Web.Models
{
    public class StaffViewModel
    {
        public int Id { get; set; }

        public int Rank { get; set; }

        [Required]
        public string ArmyNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RetirementDate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }

        public int State { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
