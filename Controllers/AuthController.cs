using Microsoft.AspNetCore.Mvc;
using traningday2.Models;
using traningday2.Services;

namespace traningday2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Validasi user di sini
            //if (model.Username == "test" && model.Password == "password")
            //{
            //    var token = _tokenService.GenerateToken(model.Username);
            //    return Ok(new { Token = token });
            //}
            //return Unauthorized();

            if (_authService.ValidateUser(model.Username, model.Password))
            {
                var token = _tokenService.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}
