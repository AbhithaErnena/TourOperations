using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMgmtAPI.Services;

namespace TourMgmtAPI.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var (status, message) = _authService.Register(model);
            if (status == 1) return Ok(new { success = true, message });
            return BadRequest(new { success = false, message });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var (status, token) = _authService.Login(model);
            if (status == 1) return Ok(new { token });
            return Unauthorized(new { message = token });
        }
    }

}
