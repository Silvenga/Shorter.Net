using System;
using System.Data.Common;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

using Shorter.Data.Interfaces;
using Shorter.Data.Models;

namespace Shorter.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(string connectionStringOrName = nameof(ApplicationContext)) : base(connectionStringOrName)
        {
        }

        public ApplicationContext(DbConnection connection) : base(connection, false)
        {
        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public override int SaveChanges()
        {
            PreSaveChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            PreSaveChanges();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            PreSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void PreSaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTimeOffset.Now;
                }
                entry.Entity.ModifiedOn = DateTimeOffset.Now;
            }
        }
    }
}