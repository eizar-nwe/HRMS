using Asp.Versioning;
using HRMS.Domain.Models.ViewModels;
using HRMS.Services;
using HRMS.WebAPIs.ConfigSwaggerOptions;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.WebAPIs.Controllers
{    
    [ApiVersion(1)] //specify API version    
    [ApiVersion(2)] //specify API version    
    [Route("api/v{v:apiVersion}/department")]
    [ApiController]    
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [MapToApiVersion(1)]
        //Get:api/Department/v1/GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = _departmentService.GetAll();
            if (departments == null)
            {
                return NotFound();
            }
            return Ok(departments);
        }
        //[Authorize(Roles = "HR,EMPLOYEE")]
        [DynamicRoleAuthorize("TopLevelRoles")]
        [MapToApiVersion(1)]
        //Get:api/Department/v1/create        
        [HttpPost("Create1")]
        public IActionResult Create1([FromBody] DepartmentViewModel departmentViewModel)
        {
            if (departmentViewModel == null)
            {
                return BadRequest("Department data is null");
            }
            _departmentService.Create(departmentViewModel);
            return Ok(new { message = "department is created", StatusCode = "200" });
        }

        [DynamicRoleAuthorize("TopLevelRoles")]
        [MapToApiVersion(1)]
        //Get:api/Department/v1/Delete >> {"Id":""}        
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DepartmentViewModelV3 departmentViewModelV3)
        {
            if (string.IsNullOrEmpty(departmentViewModelV3.Id))
            {
                return BadRequest("Department Id is required.");
            }
            _departmentService.Delete(departmentViewModelV3.Id);            
            return Ok(new { message = "department is deleted", StatusCode = "200" });
        }

        [DynamicRoleAuthorize("TopLevelRoles")]
        [MapToApiVersion(2)]
        //Get:api/Department/v2/create
        [HttpPost("Create2")]
        public IActionResult Create2([FromBody] DepartmentViewModelV2 departmentViewModel2)
        {
            _logger.LogInformation("Request Body: " + departmentViewModel2);
            if (departmentViewModel2 == null)
            {
                return BadRequest("Department data is null");
            }
            //_departmentService.Create(departmentViewModel);
            return Ok(new { message = "department is created", StatusCode = "200", version = "2" });
        }
    }
}
