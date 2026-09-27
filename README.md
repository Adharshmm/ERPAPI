====================================================================================================
using AutoMapper;
using ERPAPI.Data.EF;
using ERPAPI.Data.Entities;
using ERPAPI.Dtos;
using ERPAPI.Helpers;
using ERPAPI.Models;
using ERPAPI.Services.DB.Common;
using ERPAPI.Services.DB.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JWTSettings jWTSettings;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly EmployeeDetailsDBContext EmployeeDetailsDBContext;
        private readonly IMapper _mapper;
        private readonly IEmployeeDA _employeeDA;

        private readonly IProjectDA projectDA;

        public AccountController(
            JWTSettings jWTSettings,
            UserManager<ApplicationUser> userManager,
            EmployeeDetailsDBContext EmployeeDetailsDBContext,
            IMapper mapper,
            IEmployeeDA employeeDA,
            IProjectDA projectDA)
        {
            this.jWTSettings = jWTSettings;
            this.userManager = userManager;
            this.EmployeeDetailsDBContext = EmployeeDetailsDBContext;

            _mapper = mapper;
            _employeeDA = employeeDA;

            this.projectDA = projectDA;
        }


        [HttpPost("GetToken")]
        public async Task<IActionResult> GetToken(UserLogin userLogins)
        {
            try
            {
                var Token = new UserToken();

                var user = await userManager.FindByNameAsync(userLogins.UserName);

                if (user == null)
                {
                    return BadRequest("Invalid Credentials");
                }

                var Valid = await userManager.CheckPasswordAsync(
                    user,
                    userLogins.Password);

                if (Valid)
                {
                    var strToken = Guid.NewGuid().ToString();

                    var validity = DateTime.UtcNow.AddDays(15);

                    Token = Helpers.JwtHelper.GenTokenKey(
                        new UserToken()
                        {
                            EmailId = user.Email,
                            GuidId = Guid.NewGuid(),
                            UserName = user.UserName,
                            Id = Guid.Parse(user.Id),
                            RefreshToken = strToken
                        },
                        jWTSettings);

                    var tokenupdate = EmployeeDetailsDBContext.Users
                        .Where(x => x.Id == user.Id)
                        .FirstOrDefault();

                    if (tokenupdate != null)
                    {
                        tokenupdate.RefreshToken = strToken;
                        tokenupdate.RefreshTokenValidity = validity;

                        EmployeeDetailsDBContext.Update(tokenupdate);

                        await EmployeeDetailsDBContext.SaveChangesAsync();
                    }

                    Token.RefreshToken = strToken;
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }

                return Ok(Token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(User users)
        {
            try
            {
                var applicationUser = new ApplicationUser()
                {
                    UserName = users.UserName,
                    Email = users.EmailId,
                    Firstname = users.FirstName,
                    Lastname = users.LastName,
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false
                };

                var result = await userManager.CreateAsync(
                    applicationUser,
                    users.Password);

                if (result.Succeeded)
                {
                    return Ok("User created");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(
            RefreshTokenModel userLogin)
        {
            try
            {
                var Token = new UserToken();

                var user = await userManager.FindByNameAsync(
                    userLogin.UserName);

                if (user == null)
                {
                    return BadRequest("Invalid Credentials");
                }

                var Valid = EmployeeDetailsDBContext.Users
                    .Where(x =>
                        x.UserName == userLogin.UserName &&
                        x.RefreshToken == userLogin.RefreshToken &&
                        x.RefreshTokenValidity > DateTime.UtcNow)
                    .Count() > 0;

                if (Valid)
                {
                    var strToken = Guid.NewGuid().ToString();

                    var validity = DateTime.UtcNow.AddDays(15);

                    Token = Helpers.JwtHelper.GenTokenKey(
                        new UserToken()
                        {
                            EmailId = user.Email,
                            GuidId = Guid.NewGuid(),
                            UserName = user.UserName,
                            Id = Guid.Parse(user.Id),
                            RefreshToken = strToken
                        },
                        jWTSettings);

                    user.RefreshToken = strToken;
                    user.RefreshTokenValidity = validity;

                    EmployeeDetailsDBContext.Update(user);

                    await EmployeeDetailsDBContext.SaveChangesAsync();

                    Token.RefreshToken = strToken;
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }

                return Ok(Token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Employee")]
        public async Task<ActionResult<ServiceResponse>> AddEmployee(
            EmployeeDto oEmployeeDto)
        {
            try
            {
                Employee oEmployee =
                    _mapper.Map<Employee>(oEmployeeDto);

                ServiceResponse result =
                    await _employeeDA.Insert(oEmployee);

                if (result.RecordsAffected > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }       
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Employee")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            try
            {
                if (!_employeeDA.IsEmployeeListNull())
                {
                    return NotFound();
                }

                List<Employee> employees =
                    await _employeeDA.GetList();

                List<EmployeeDto> employeesDto =
                    _mapper.Map<List<EmployeeDto>>(employees);

                return Ok(employeesDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Employee/{Id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();
                }

                if (!_employeeDA.IsEmployeeListNull())
                {
                    return NotFound();
                }

                Employee? employee =
                    await _employeeDA.GetById(Id);

                if (employee == null)
                {
                    return NotFound();
                }

                EmployeeDto employeeDto =
                    _mapper.Map<EmployeeDto>(employee);

                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Employee/{Id}")]
        public async Task<ActionResult<ServiceResponse>> UpdateEmployee(
            int Id,
            [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();
                }

                if (!_employeeDA.IsEmployeeListNull())
                {
                    return StatusCode(
                        StatusCodes.Status500InternalServerError);
                }

                Employee? employee =
                    await _employeeDA.GetById(Id);

                if (employee == null)
                {
                    return NotFound();
                }

                Employee modifiedEmployee =
                    _mapper.Map<Employee>(employeeDto);

                ServiceResponse result =
                    await _employeeDA.EmpUpdate(modifiedEmployee);

                if (result.Status == 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("Employee/{Id}")]
        public async Task<ActionResult<ServiceResponse>> DeleteEmployee(
            int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest();
                }

                if (!_employeeDA.IsEmployeeListNull())
                {
                    return StatusCode(
                        StatusCodes.Status500InternalServerError);
                }

                Employee? employee =
                    await _employeeDA.GetById(Id);

                if (employee == null)
                {
                    return NotFound();
                }

                ServiceResponse result =
                    await _employeeDA.DeleteEmp(employee);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Project")]
        public async Task<IActionResult> InsertProject(
            Project project)
        {
            try
            {
                ServiceResponse result =
                    await projectDA.Insert(project);

                if (result.Status == OperationStatus.Success)
                {
                    return Ok(result);
                }
                else if (result.Status == OperationStatus.Failure)
                {
                    return BadRequest(result);
                }
                else if (result.Status == OperationStatus.NotFound)
                {
                    return NotFound(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Project/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                Project? result =
                    await projectDA.GetById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Project")]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                return Ok(await projectDA.GetList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Project/{id}")]
        public async Task<IActionResult> UpdateProject(
            int id,
            [FromBody] Project project)
        {
            try
            {
                ServiceResponse result =
                    await projectDA.Update(id, project);

                if (result.Status == OperationStatus.Success)
                {
                    return Ok(result);
                }
                else if (result.Status == OperationStatus.Failure)
                {
                    return BadRequest(result);
                }
                else if (result.Status == OperationStatus.NotFound)
                {
                    return NotFound(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Project/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                ServiceResponse result =
                    await projectDA.Delete(id);

                if (result.Status == OperationStatus.Success)
                {
                    return Ok(result);
                }
                else if (result.Status == OperationStatus.Failure)
                {
                    return BadRequest(result);
                }
                else if (result.Status == OperationStatus.NotFound)
                {
                    return NotFound(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Project/Paged")]
        public async Task<IActionResult> GetPagedList(
            int pageNumber,
            int pageSize)
        {
            try
            {
                return Ok(
                    await projectDA.GetPagedList(
                        pageNumber,
                        pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Project/ProjectManager/{projectManagerId}")]
        public async Task<IActionResult> GetByProjectManager(
            int projectManagerId)
        {
            try
            {
                return Ok(
                    await projectDA.GetByProjectManager(
                        projectManagerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
====================================================================================================
