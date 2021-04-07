using System.Data.Common;
using System.Data.SqlClient;
using CarConfigurator.DL.Factories.Interfaces;

namespace CarConfigurator.DL.Factories
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        public SqlConnection GetSqlConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
