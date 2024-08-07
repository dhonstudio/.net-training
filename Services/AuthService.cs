using traningday2.DTO;
using traningday2.Models;

namespace traningday2.Services
{
    public interface IAuthService
    {
        UserRolesDTO? ValidateUser(string username, string password, int roleid = 0, bool changeRole = false);
    }

    public class AuthService : IAuthService
    {
        private readonly SchoolContext _schoolContext;
        private readonly PasswordService _password;
        public AuthService(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
            _password = new PasswordService();
        }

        public UserRolesDTO? ValidateUser(string username, string password, int roleid = 0, bool changeRole = false)
        {
            var user = _schoolContext.Users.FirstOrDefault(u => u.Username == username);
            if ((user != null && _password.VerifyPassword(user.Password, password, user)) || changeRole == true)
            {
                var userRole = new UserRolesDTO()
                {
                    IDRole = roleid,
                    Username = username
                };
                return userRole;
            }
            return null;
        }
    }
}
