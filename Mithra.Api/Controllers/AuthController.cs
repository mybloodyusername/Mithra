using Microsoft.AspNetCore.Mvc;
using Mithra.Domain.DTOs.Auth;
using Mithra.Domain.Interfaces.Services;

namespace Mithra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await authService.Login(request);
            return Ok(result);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RegisterUserResponse>> Register([FromBody] RegisterUserRequest request)
        {
            var result = await authService.RegisterUser(request);
            return Ok(result);
        }
    }
}