using System;
using System.Data.Common;
using System.Data.Entity;

using Shorter.Data.Interfaces;

namespace Shorter.Data.Tests
{
    public class ApplicationContextFixture : ApplicationContext
    {
        public ApplicationContextFixture(DbConnection connection) : base(connection)
        {
        }

        public DbSet<EntityFixture> EntityFixtures { get; set; }
    }

    public class EntityFixture : IEntity, IAuditable
    {
        public int Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}