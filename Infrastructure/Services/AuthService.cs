using Domain.Interfaces;
using Domain.Models;
using Domain.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService(IAccountService _accountService, IConfiguration configuration) : IAuthService
    {
        const Int16 TOKEN_EXPIRATION = 1;

        public async Task<Response> LogIn(string email, string password)
        {
            var response = new Response();
            response.Message = "Error al iniciar session";

            var result = await _accountService.FindAccount(email);
            if (result != null || BCrypt.Net.BCrypt.Verify(password, result!.Password))
            {
                response.SetSuccessResponse(this.CreateToken(result));
                return response;
            }
            return response;
        }

        public async Task<Response> Register(string email, string password)
        {
            var response = new Response();
            if (!await _accountService.Exists(email))
            {
                string hashedPass = BCrypt.Net.BCrypt.HashPassword(password);
                Account account = new()
                {
                    Email = email,
                    Password = hashedPass
                };
                response.SetSuccessResponse(await _accountService.Create(account));
                return response;
            }
            throw new Exception("Hubo un error con el registro: Email ya esta en uso.");
        }

        private string CreateToken(Account account)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(TOKEN_EXPIRATION),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
