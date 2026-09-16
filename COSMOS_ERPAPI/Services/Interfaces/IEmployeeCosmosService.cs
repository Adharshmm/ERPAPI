using ERPAPI.Data.Cosmos.Entities;

namespace ERPAPI.Services.Cosmos.Interfaces
{
    public interface IEmployeeCosmosService
    {
        Task<EmployeeCosmos> CreateEmployee(EmployeeCosmos employee);

        Task<List<EmployeeCosmos>> GetEmployees();

        Task<EmployeeCosmos> GetEmployee(int employeeID);

        Task<EmployeeCosmos> UpdateEmployee(EmployeeCosmos employee);

        Task<bool> DeleteEmployee(int employeeID);
    }
}