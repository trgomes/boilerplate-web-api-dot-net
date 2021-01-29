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
            _transaction = _connection.BeginTransaction();
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public void Dispose()
        {
            if(_transaction != null)
            {
                _transaction?.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection?.Dispose();
                _connection = null;
            }
        }

    }

    public interface IDataContext
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Dispose();
    }
}
