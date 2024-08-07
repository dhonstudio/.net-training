using AutoMapper;
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

        public UsersController(SchoolContext schoolContext, IMapper mapper)
        {
            _schoolContext = schoolContext;
            _mapper = mapper;
            _password = new PasswordService();
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

        [HttpPost]
        public IActionResult PostUserRoles(UserRoleParamDTO userRoleParam)
        {
            var userExist = _schoolContext.Users.FirstOrDefault(x => x.Username == userRoleParam.Username);
            if (userExist == null) return BadRequest(new
            {
                Message = "user tidak ditemukan"
            });
            var userRole = _mapper.Map<UserRoles>(userRoleParam);
            userRole.IDUser = userExist.ID;

            _schoolContext.UserRoles.Add(userRole);

            _schoolContext.SaveChanges();
            return Ok(_mapper.Map<UserRolesDTO>(userRole));
        }
    }
}
