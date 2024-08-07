using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using traningday2.DTO;
using traningday2.Models;
using traningday2.Services;

namespace traningday2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
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

        [HttpPost]
        public IActionResult PostUsers(UsersParamDTO usersParam)
        {
            var user = _mapper.Map<Users>(usersParam);
            var userExist = _schoolContext.Users.Any(x => x.Username == user.Username);
            if (userExist) return BadRequest(new
            {
                Message = "user sudah ada"
            });

            user.Password = _password.HashPassword(user.Password, user);

            _schoolContext.Users.Add(user);

            _schoolContext.SaveChanges();
            return Ok(_mapper.Map<UsersDTO>(user));
        }
    }
}
