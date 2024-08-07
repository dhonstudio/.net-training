using Microsoft.AspNetCore.Mvc;
using traningday2.Models;
using traningday2.Services;

namespace traningday2.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly SchoolContext _schoolContext;

        public AuthController(ITokenService tokenService, SchoolContext schoolContext)
        {
            _tokenService = tokenService;
            _schoolContext = schoolContext;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Validasi user di sini
            if (_schoolContext.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password) != null)
            {
                var token = _tokenService.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}
