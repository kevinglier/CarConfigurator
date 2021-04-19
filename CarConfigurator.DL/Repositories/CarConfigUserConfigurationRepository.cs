using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CarConfigurator.DL.Models;
using CarConfigurator.DL.Repositories.Base;
using CarConfigurator.DL.Repositories.Interfaces;
using Dapper;

namespace CarConfigurator.DL.Repositories
{
    public class CarConfigUserConfigurationRepository : RepositoryBase, ICarConfigUserConfigurationRepository
    {
        public CarConfigUserConfigurationRepository(string connectionString) : base(connectionString)
        {
        }

        public CarConfigUserConfiguration Get(string code)
        {
            using var connection = new SqlConnection(ConnectionString);

            const string sql = "SELECT Id FROM CarConfigUserConfiguration WHERE Code=@code";

            var id = connection.ExecuteScalar<int>(sql, new { code });

            return id > 0 ? GetById(id) : null;
        }

        public CarConfigUserConfiguration GetById(int id)
        {
            using var connection = new SqlConnection(ConnectionString);

            const string sqlConfig = "SELECT * FROM CarConfigUserConfiguration WHERE Id=@id";
            var userConfiguration = connection.QuerySingleOrDefault<CarConfigUserConfiguration>(sqlConfig, new { id });

            if (userConfiguration == null)
                return null;

            const string sqlConfigProducts =
                "SELECT * FROM CarConfigUserConfigurationProduct WHERE CarConfigUserConfigurationId=@carConfigUserConfigurationId";
            var userConfigurationProducts =
                connection.Query<CarConfigUserConfigurationProduct>(sqlConfigProducts,
                    new { carConfigUserConfigurationId = userConfiguration.Id });

            userConfiguration.Products = userConfigurationProducts;

            return userConfiguration;
        }

        public CarConfigUserConfiguration Update(CarConfigUserConfiguration userConfiguration)
        {
            var currentUserConfiguration = Get(userConfiguration.Code);
            if (currentUserConfiguration == null)
                return null;

            using var connection = new SqlConnection(ConnectionString);

            // Delete old product selection for this configuration
            connection.Execute(
                "DELETE FROM CarConfigUserConfigurationProduct WHERE CarConfigUserConfigurationId=@carConfigUserConfigurationId",
                new {carConfigUserConfigurationId = currentUserConfiguration.Id});

            // Add new selected products
            InsertProducts(currentUserConfiguration.Id, userConfiguration.Products);

            return Get(currentUserConfiguration.Code);
        }

        private void InsertProducts(int userConfigurationId, IEnumerable<CarConfigUserConfigurationProduct> userConfigurationProducts)
        {
            using var connection = new SqlConnection(ConnectionString);
            foreach (var product in userConfigurationProducts)
            {
                connection.Execute(
                    @"INSERT INTO CarConfigUserConfigurationProduct (CarConfigUserConfigurationId, OptionId, SelectedOptionProductId)
                                                            VALUES (@carConfigUserConfigurationId, @optionId, @selectedProductId);",
                    new
                    {
                        carConfigUserConfigurationId = userConfigurationId,
                        optionId = product.OptionId,
                        selectedProductId = product.SelectedOptionProductId
                    });
            }
        }

        public CarConfigUserConfiguration Add(CarConfigUserConfiguration userConfiguration, string code)
        {
            using var connection = new SqlConnection(ConnectionString);
            
            if (Get(code) != null)
                throw new Exception("There's already an entry for this code.");

            // Delete old product selection for this configuration
            var insertId = connection.ExecuteScalar<int>(
                @"INSERT INTO CarConfigUserConfiguration (Code, ModelEAN)
                          output INSERTED.ID 
                          VALUES (@code, @carModelEAN);",
                new { code, carModelEAN = userConfiguration.ModelEAN });

            if (insertId <= 0)
                throw new Exception("Unknown error while saving the user configuration.");

            var newUserConfiguration = GetById(insertId);

            // Add new selected products
            InsertProducts(newUserConfiguration.Id, userConfiguration.Products);

            return GetById(newUserConfiguration.Id);
        }
    }
}