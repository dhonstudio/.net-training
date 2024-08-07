using traningday2.Models;

namespace traningday2.Services
{
    public interface IAuthService
    {
        bool ValidateUser(string username, string password);
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

        public bool ValidateUser(string username, string password)
        {
            var user = _schoolContext.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                return _password.VerifyPassword(user.Password, password, user);
            }
            return false;
        }
    }
}
