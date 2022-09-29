﻿using DevFreela.Core.Constants;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {

        private readonly IConfiguration _configuration;

        public AuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string email, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("userName", email),
                new Claim(ClaimTypes.Role, role),

            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials,
                claims: claims
            );

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            return stringToken;
        }

        public string ComputeSha256Hash(string password)
        {
            using(var sha256Hash = SHA256.Create())
            {
                //Computes hash and returns a byte array
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Converts a byte array to string
                var stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2")); //x2 makes the conversion to hexadecimal
                }

                return stringBuilder.ToString();

            }
        }

    }
}
