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

        public AuthController(ITokenService tokenService, SchoolContext schoolContext)
        {
            _tokenService = tokenService;
            _schoolContext = schoolContext;
            _password = new PasswordService();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Validasi user di sini
            var user = _schoolContext.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user != null && _password.VerifyPassword(user.Password, model.Password, user))
            {
                var token = _tokenService.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}
