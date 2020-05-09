using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CounterIntelligenceCommand.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CounterIntelligenceCommand.Data.Entities
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        private const string IsDeletedProperty = "IsDeleted";

        private static readonly MethodInfo _propertyMethod = typeof(EF).GetMethod(nameof(EF.Property),
                BindingFlags.Static |
                BindingFlags.Public)
            .MakeGenericMethod(typeof(bool));



        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var param = Expression.Parameter(type,
                "it");
            var prop = Expression.Call(_propertyMethod,
                param,
                Expression.Constant(IsDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.Equal,
                prop,
                Expression.Constant(false));
            var lambda = Expression.Lambda(condition,
                param);
            return lambda;
        }

        private void ConfigureDeletableEntities(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));
                }
            }
        }



        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(cancellationToken);
        }



        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess,
                cancellationToken);
        }



        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[IsDeletedProperty] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[IsDeletedProperty] = true;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StateEntityTypeConfiguration());
            builder.ApplyConfiguration(new StaffEntityTypeConfiguration());
            ConfigureDeletableEntities(builder);
        }

        public DbSet<Staff> Staff { get; set; }

        public DbSet<State> States { get; set; }

    }
}
