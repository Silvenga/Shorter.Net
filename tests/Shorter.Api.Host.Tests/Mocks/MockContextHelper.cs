using System.Data.Common;

using Shorter.Data;

namespace Shorter.Api.Host.Tests.Mocks
{
    public class MockContextHelper
    {
        private DbConnection _connection;

        public DbConnection CurrentConnection
        {
            get { return _connection = _connection ?? Effort.DbConnectionFactory.CreateTransient(); }
        }

        public ApplicationContext Create()
        {
            return new ApplicationContext(CurrentConnection);
        }
    }
}