using Microsoft.AspNetCore.Mvc;
using traningday2.Models;
using traningday2.Services;

namespace traningday2.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Validasi user di sini
            if (model.Username == "string" && model.Password == "string")
            {
                var token = _tokenService.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}
