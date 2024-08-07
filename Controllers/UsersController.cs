using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using traningday2.DTO;
using traningday2.Models;

namespace traningday2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly SchoolContext _schoolContext;
        private readonly IMapper _mapper;

        public UsersController(SchoolContext schoolContext, IMapper mapper)
        {
            _schoolContext = schoolContext;
            _mapper = mapper;
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

            _schoolContext.SaveChanges();
            return Ok(_mapper.Map<UsersDTO>(user));
        }
    }
}
