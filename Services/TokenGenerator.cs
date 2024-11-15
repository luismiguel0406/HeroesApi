using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HeroesApi.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        public string GenerateToken(Users user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = configuration.GetValue<string>("Token:SecretKey")!.ToArray();

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Sub, user.Username),
                new (JwtRegisteredClaimNames.Email, user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Audience = "http://localhost:4200",// Intended web app to sent token
                Issuer = "http://localhost:5266", // Issuing token
                SigningCredentials =
                new SigningCredentials
                     (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token =  tokenHandler.CreateToken(tokenDescriptor);           
            return tokenHandler.WriteToken(token);
        }
    }
}

