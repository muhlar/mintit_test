using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.DAL.DataContext
{
    public class VisitorTrackingContext : DbContext
    {
        public VisitorTrackingContext()
            : base("name=VisitorTrackingConnection")
        {
        }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }

        //set timestamps for added/modified entitites
        public override int SaveChanges()
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Added)
              .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            var now = DateTime.UtcNow;

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedOn = now;
                added.ModifiedOn = now;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.ModifiedOn = now;
            }

            return base.SaveChanges();
        }

        //set timestamps for added/modified entitites async
        public override async Task<int> SaveChangesAsync()
        {
            var addedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Added)
              .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            var now = DateTime.UtcNow;

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedOn = now;
                added.ModifiedOn = now;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.ModifiedOn = now;
            }

            return await base.SaveChangesAsync();
        }
    }
}
