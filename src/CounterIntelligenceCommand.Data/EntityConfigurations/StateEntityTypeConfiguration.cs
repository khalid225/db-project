using System;
using System.Collections.Generic;
using System.Text;
using CounterIntelligenceCommand.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CounterIntelligenceCommand.Data.EntityConfigurations
{
    public class StateEntityTypeConfiguration
        : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");

            builder.HasKey(u => u.Id);


            builder.Property(u => u.Name)
                .HasMaxLength(250).IsRequired();

            builder.HasIndex(u => u.Name)
                .IsUnique();

            builder.Property(u => u.Code)
                .HasMaxLength(500);

            builder.HasMany(s => s.Staff)
                .WithOne(s => s.State)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
