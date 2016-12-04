using System;
using System.Data.Common;

using Shorter.Data;

namespace Shorter.Api.Tests.Mocks
{
    public static class MockContextHelper
    {
        [ThreadStatic] private static DbConnection _connection;

        public static DbConnection CurrentConnection
        {
            get { return _connection = _connection ?? Effort.DbConnectionFactory.CreateTransient(); }
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext(CurrentConnection);
        }
    }
}