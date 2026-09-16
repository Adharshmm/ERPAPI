using ERPAPI.Data.Cosmos.Entities;
using ERPAPI.Services.Cosmos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCosmosController : ControllerBase
    {
        private readonly IEmployeeCosmosService _service;

        public EmployeeCosmosController(
            IEmployeeCosmosService service)
        {
            _service = service;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(
            EmployeeCosmos employee)
        {
            var result =
                await _service.CreateEmployee(employee);

            return Ok(result);
        }

        // LIST
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetEmployees();

            return Ok(result);
        }

        // GET ONE
        [HttpGet("{employeeID}")]
        public async Task<IActionResult> GetOne(
            int employeeID)
        {
            var result =
                await _service.GetEmployee(employeeID);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // UPDATE
        [HttpPut]
        public async Task<IActionResult> Update(
            EmployeeCosmos employee)
        {
            var result =
                await _service.UpdateEmployee(employee);

            return Ok(result);
        }

        // DELETE
        [HttpDelete("{employeeID}")]
        public async Task<IActionResult> Delete(
            int employeeID)
        {
            var result =
                await _service.DeleteEmployee(employeeID);

            if (!result)
            {
                return NotFound();
            }

            return Ok("Employee deleted successfully.");
        }
    }
}