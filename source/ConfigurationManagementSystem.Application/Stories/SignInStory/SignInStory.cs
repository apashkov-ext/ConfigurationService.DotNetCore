using ConfigurationManagementSystem.Application.AppConfiguration;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ConfigurationManagementSystem.Application.Stories.SignInStory
{
    [UserStory]
    public class SignInStory
    {
        private readonly GetUserByCredentialsQuery _getUserByCredentialsQuery;
        private readonly SecuritySection _securitySection;

        public SignInStory(GetUserByCredentialsQuery getUserByCredentialsQuery, SecuritySection securitySection)
        {
            _getUserByCredentialsQuery = getUserByCredentialsQuery;
            _securitySection = securitySection;
        }

        public async Task<string> ExecuteAsync(string username, string password)
        {
            var usern = new Username(username);
            var pass = new Password(BCryptNet.HashPassword(password, _securitySection.Salt));

            var user = await _getUserByCredentialsQuery.ExecuteAsync(usern, pass) ?? throw new UserDoesNotExistException($"User not found: {usern}");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySection.SymmetricSecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username.Value)
            };
            var now = DateTime.Now;
            var tokeOptions = new JwtSecurityToken(
                issuer: _securitySection.ValidIssuer,
                audience: _securitySection.ValidAudience,
                claims,
                signingCredentials: signingCredentials,
                expires: now.Add(_securitySection.TokenExpiration),
                notBefore: now
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return token;
        }
    }
}
