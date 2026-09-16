using ERPAPI.Data.Cosmos.Entities;
using ERPAPI.Services.Cosmos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace ERPAPI.Services.Cosmos
{
    public class EmployeeCosmosService : IEmployeeCosmosService
    {
        private readonly Container _container;

        public EmployeeCosmosService(IConfiguration configuration)
        {
            CosmosClient client = new CosmosClient(
                configuration["CosmosDb:ConnectionString"]);

            Database database = client.GetDatabase(
                configuration["CosmosDb:DatabaseName"]);

            _container = database.GetContainer(
                configuration["CosmosDb:ContainerName"]);
        }

        // CREATE
        public async Task<EmployeeCosmos> CreateEmployee(
            EmployeeCosmos employee)
        {
            employee.Id = employee.EmployeeID.ToString();

            PasswordHasher<EmployeeCosmos> hasher =
                new PasswordHasher<EmployeeCosmos>();

            employee.Password = hasher.HashPassword(
                employee,
                employee.Password);

            await _container.CreateItemAsync(
                employee,
                new PartitionKey(employee.EmployeeID.ToString()));

            return employee;
        }

        // LIST
        public async Task<List<EmployeeCosmos>> GetEmployees()
        {
            var query = _container.GetItemQueryIterator<EmployeeCosmos>(
                "SELECT * FROM c");

            List<EmployeeCosmos> employees = new List<EmployeeCosmos>();

            while (query.HasMoreResults)
            {
                FeedResponse<EmployeeCosmos> response =
                    await query.ReadNextAsync();

                employees.AddRange(response);
            }

            return employees;
        }

        // GET ONE
        public async Task<EmployeeCosmos> GetEmployee(int employeeID)
        {
            try
            {
                ItemResponse<EmployeeCosmos> response =
                    await _container.ReadItemAsync<EmployeeCosmos>(
                        employeeID.ToString(),
                        new PartitionKey(employeeID.ToString()));

                return response.Resource;
            }
            catch (CosmosException ex)
                when (ex.StatusCode ==
                       System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        // UPDATE
        public async Task<EmployeeCosmos> UpdateEmployee(
            EmployeeCosmos employee)
        {
            employee.Id = employee.EmployeeID.ToString();

            PasswordHasher<EmployeeCosmos> hasher =
                new PasswordHasher<EmployeeCosmos>();

            employee.Password = hasher.HashPassword(
                employee,
                employee.Password);

            ItemResponse<EmployeeCosmos> response =
                await _container.ReplaceItemAsync(
                    employee,
                    employee.Id,
                    new PartitionKey(
                        employee.EmployeeID.ToString()));

            return response.Resource;
        }

        // DELETE
        public async Task<bool> DeleteEmployee(int employeeID)
        {
            try
            {
                await _container.DeleteItemAsync<EmployeeCosmos>(
                    employeeID.ToString(),
                    new PartitionKey(employeeID.ToString()));

                return true;
            }
            catch (CosmosException ex)
                when (ex.StatusCode ==
                       System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }
    }
}