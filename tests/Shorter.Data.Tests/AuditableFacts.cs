using System.Threading.Tasks;

using FluentAssertions;

using Ploeh.AutoFixture;

using Xunit;

namespace Shorter.Data.Tests
{
    public class AuditableFacts
    {
        private static readonly Fixture Autofixture = new Fixture();

        [Fact]
        public async Task When_creating_an_entity_set_created_on_property()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new ApplicationContextFixture(connection);

            var fixture = Autofixture.Build<EntityFixture>()
                                      .Without(x => x.CreatedOn)
                                      .Without(x => x.ModifiedOn)
                                      .Create();

            // Act
            context.EntityFixtures.Add(fixture);
            await context.SaveChangesAsync();

            // Assert
            fixture.CreatedOn.Should().HaveValue();
        }
    }
}