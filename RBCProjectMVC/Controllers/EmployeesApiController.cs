using Business.Services.Abstract;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace RBCProjectMVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesApiController(IEmployeeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string? search, string? sortBy, string? sortDir)
        {
            try
            {
                List<Entities.Employee> employees;

                if (!string.IsNullOrEmpty(search))
                {
                    employees = await _service.SearchLiveAsync(search);
                    return Ok(employees); 
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    bool isAsc = sortDir?.ToLower() != "desc";
                    employees = await _service.GetSortedAsync(sortBy, isAsc);
                    return Ok(employees); 
                }

                employees = await _service.GetAllAsync();
                return Ok(employees); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message }); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeCreateVM createVM)
        {
            if (createVM == null)
                return BadRequest(new { error = "Request body is null" });

            try
            {
                await _service.AddAsync(createVM);
                return StatusCode(201); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message }); 
            }
        }
    }
}
