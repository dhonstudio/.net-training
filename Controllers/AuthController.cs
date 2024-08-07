using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using traningday2.Models;
using traningday2.Services;

namespace traningday2.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly SchoolContext _schoolContext;
        private readonly PasswordService _password;
        private readonly IAuthService _authService;

        public AuthController(ITokenService tokenService, SchoolContext schoolContext, IAuthService authService)
        {
            _tokenService = tokenService;
            _schoolContext = schoolContext;
            _password = new PasswordService();
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Validasi user di sini
            if (_authService.ValidateUser(model.Username, model.Password))
            {
                var token = _tokenService.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}
