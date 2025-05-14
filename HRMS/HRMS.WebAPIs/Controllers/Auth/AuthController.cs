using Asp.Versioning;
using HRMS.Domain.Models.ViewModels;
using HRMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.WebAPIs.Controllers.Auth
{
    //specify API version    
    [ApiVersion(1)] 
    [ApiVersion(2)] 
    [Route("api/v{v:apiVersion}/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        //3 injection types : Controller Injection, Method, Property
        public AuthController(ILogger<AuthController> logger,IAuthService authService)
        {
            this._logger = logger;
            this._authService = authService;
        }

        [MapToApiVersion(1)]
        //Get:api/Department/v1/Login        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthViewModel authViewModel)
        {
            _logger.LogInformation("Request Body : ", authViewModel);
            if (authViewModel == null)
            {
                return BadRequest("Login data is null");
            }
            var (status,generatedtoken) = await _authService.AuthLogin(authViewModel);
            if (status != 200)
            {
                return Unauthorized(new { message = "Unauthorized",statusCode = "401" });
            }
            return Ok(new { message = "Login successful with token",token = generatedtoken, StatusCode = "200" });
        }
    }
}
