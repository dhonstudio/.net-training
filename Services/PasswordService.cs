using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using traningday2.Models;

namespace traningday2.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        //public string HashPassword(string password)
        public string HashPassword(string password, Users user)
        {
            //return _passwordHasher.HashPassword(null, password);
            return _passwordHasher.HashPassword(user, password);
        }

        //public bool VerifyPassword(string hashedPassword, string password)
        public bool VerifyPassword(string hashedPassword, string password, Users user)
        {
            //var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
