using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using traningday2.Models;

namespace traningday2.Services
{
    public interface IAuthService
    {
        bool ValidateUser(string username, string password);
    }

    public class AuthService : IAuthService
    {
        // Ini hanya contoh hardcoded, sebaiknya diambil dari database
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "test", "password" }
        };

        private readonly SchoolContext _context;
        private readonly PasswordService _password;
        public AuthService(SchoolContext context)
        {
            _context = context;
            _password = new PasswordService();
        }

        public bool ValidateUser(string username, string password)
        {
            //return _users.ContainsKey(username) && _users[username] == password;
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) { return false; }
            return _password.VerifyPassword(user.Password, password, user);
        }
    }
}
