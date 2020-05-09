using System;

namespace CounterIntelligenceCommand.Data.Entities
{
    public abstract class BaseEntity : ISoftDeletable
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
