using traningday2.DTO;
using traningday2.Models;

namespace traningday2.Services
{
    public interface IAuthService
    {
        bool ValidatePublic(HttpRequest request);
        UserRolesDTO? ValidateUser(string username, string password, int roleid = 0, bool changeRole = false);
    }

    public class AuthService : IAuthService
    {
        private readonly SchoolContext _schoolContext;
        private readonly PasswordService _password;
        private readonly IConfiguration _configuration;
        public AuthService(SchoolContext schoolContext, IConfiguration configuration)
        {
            _schoolContext = schoolContext;
            _password = new PasswordService();
            _configuration = configuration;
        }

        public bool ValidatePublic(HttpRequest request)
        {
            var clientId = request.Headers.FirstOrDefault(x => x.Key.ToLower() == "clientid").Value;
            var clientSecret = request.Headers.FirstOrDefault(x => x.Key.ToLower() == "clientsecret").Value;
            return clientId == _configuration["PublicToken:ClientId"] && clientSecret == _configuration["PublicToken:ClientSecret"];
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
