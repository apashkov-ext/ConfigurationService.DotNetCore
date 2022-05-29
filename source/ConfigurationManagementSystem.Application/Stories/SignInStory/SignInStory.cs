using ConfigurationManagementSystem.Application.AppConfiguration;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.SignInStory
{
    [Component]
    public class SignInStory
    {
        private readonly GetUserByUsernameQuery _getUserByUsernameQuery;
        private readonly SecuritySection _securitySection;

        public SignInStory(GetUserByUsernameQuery getUserByUsernameQuery, 
            SecuritySection securitySection)
        {
            _getUserByUsernameQuery = getUserByUsernameQuery;
            _securitySection = securitySection;
        }

        public async Task<string> ExecuteAsync(string username, string password)
        {
            var usern = new Username(username);

            var user = await _getUserByUsernameQuery.ExecuteAsync(usern) ?? throw new UserNotFoundException($"User not found");
            PasswordVerifier.VerifyPassword(password, user.PasswordHash);

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
