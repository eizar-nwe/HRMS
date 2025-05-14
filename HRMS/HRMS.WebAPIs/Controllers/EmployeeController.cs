using Asp.Versioning;
using HRMS.Domain.Models.DataModels;
using HRMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.WebAPIs.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")] // Specify the API version
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        //https://localhost:7196/api/Employee
        [HttpGet]
        public IActionResult GetAll(int page, int to)
        {
            var employees = _employeeService.GetAll();

            if (employees == null)
            {
                return NotFound(); //404
            }
            return Ok(employees.Take(page)); //200
        }
        [HttpGet("GetByCode")]
        public IActionResult GetByCode(string code)
        {
            _logger.LogInformation($"Request code {code}");
            var employees = _employeeService.GetAll();

            if (employees == null)
            {
                return NotFound(); //404
            }

            var empe = employees.FirstOrDefault(w => w.Code == code);
            if (empe == null)
            {
                return NotFound(); //404
            }
            return Ok(empe); //200
        }
    }
}
