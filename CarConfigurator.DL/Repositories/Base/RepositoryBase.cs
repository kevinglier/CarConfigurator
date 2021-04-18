using System.Collections.Generic;
using System.Data.SqlClient;
using CarConfigurator.DL.Models;
using Dapper;

namespace CarConfigurator.DL.Repositories.Base
{
    public abstract class RepositoryBase
    {
        protected readonly string ConnectionString;

        protected RepositoryBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}