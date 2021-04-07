using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConfigurator.DL.Factories.Interfaces
{
    public interface ISqlConnectionFactory
    {
        public SqlConnection GetSqlConnection(string connectionString);
    }
}
