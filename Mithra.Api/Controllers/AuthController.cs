using Microsoft.AspNetCore.Mvc;
using Mithra.Application.DTOs.Auth;
using Mithra.Application.Interfaces.Services;

namespace Mithra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await authService.Login(request);
            return Ok(result);
        }
    }
}