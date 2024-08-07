using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using traningday2.DTO;
using traningday2.Models;
using traningday2.Services;

namespace traningday2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly SchoolContext _schoolContext;
        private readonly IMapper _mapper;
        private readonly PasswordService _password;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public UsersController(SchoolContext schoolContext, IMapper mapper, IAuthService authService, ITokenService tokenService)
        {
            _schoolContext = schoolContext;
            _mapper = mapper;
            _password = new PasswordService();
            _authService = authService;
            _tokenService = tokenService;
        }

        //[HttpPost("addUser")]
        [HttpPost]
        public IActionResult PostUsers(UsersParamDTO usersParam)
        {
            var user = _mapper.Map<Users>(usersParam);
            var userExist = _schoolContext.Users.Any(x => x.Username == user.Username);
            if (userExist) return BadRequest(new
            {
                Message = "user sudah ada"
            });

            _schoolContext.Users.Add(user);

            user.Password = _password.HashPassword(user.Password, user);

            _schoolContext.SaveChanges();
            return Ok(_mapper.Map<UsersDTO>(user));
        }

        [HttpPost("AddRole")]
        public IActionResult PostUserRoles(UserRoleParamDTO userRoleParam)
        {
            var userExist = _schoolContext.Users.FirstOrDefault(x => x.Username == userRoleParam.Username);
            if (userExist == null) return BadRequest(new
            {
                Message = "user tidak ditemukan"
            });
            var userRoleExist = _schoolContext.UserRoles.FirstOrDefault(x => x.IDUser == userExist.ID && x.IDRole == userRoleParam.IDRole);
            if (userRoleExist != null) return BadRequest(new
            {
                Message = "role sudah pernah ditambahkan"
            });
            var userRole = _mapper.Map<UserRoles>(userRoleParam);
            userRole.IDUser = userExist.ID;

            _schoolContext.UserRoles.Add(userRole);

            _schoolContext.SaveChanges();
            return Ok(_mapper.Map<UserRolesDTO>(userRole));
        }

        [Authorize]
        [HttpPost("ChangeRole")]
        public IActionResult ChangeRole(int roleid)
        {
            var claim = User.Claims;
            var username = claim.FirstOrDefault(x => x.Type == "username")?.Value;
            var user = _schoolContext.Users.FirstOrDefault(x => x.Username == username);
            var userRoles = _schoolContext.UserRoles.FirstOrDefault(x => x.IDUser == user.ID && x.IDRole == roleid);
            if (userRoles == null) return BadRequest(new
            {
                Message = "role tidak ditemukan"
            });

            var userRole = _authService.ValidateUser(username, "", roleid, true);
            // Validasi user di sini
            if (userRole != null)
            {
                var token = _tokenService.GenerateToken(username, userRole);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
