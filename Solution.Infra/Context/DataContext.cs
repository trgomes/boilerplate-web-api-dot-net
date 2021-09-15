using System;
using System.Data;
using System.Data.SqlClient;

namespace Solution.Infra.Context
{
    public class DataContext : IDataContext
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DataContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public IDbConnection Connection => _connection;

        public IDbTransaction Transaction { get => _transaction; set => _transaction = value; }

        public void Dispose() => _connection?.Dispose();
    }

    public interface IDataContext : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
    }
}
