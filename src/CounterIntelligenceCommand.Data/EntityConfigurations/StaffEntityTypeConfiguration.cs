using System;
using System.Collections.Generic;
using System.Text;
using CounterIntelligenceCommand.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CounterIntelligenceCommand.Data.EntityConfigurations
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");

            builder.HasKey(s => s.Id);


            builder.Property(s => s.FirstName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(s => s.LastName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(s => s.MiddleName)
                .HasMaxLength(250);

            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(250);

            builder.HasIndex(s => s.ArmyNumber)
                .IsUnique();

            builder.Property(s => s.Address)
                .HasMaxLength(500);

            builder.HasOne(s => s.State)
                .WithMany(s => s.Staff)
                .HasForeignKey(s => s.StateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
