using System.Collections;
using System.Collections.Generic;

namespace CounterIntelligenceCommand.Data.Entities
{
    public class State : BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<Staff> Staff { get; set; } = new HashSet<Staff>();
    }
}
