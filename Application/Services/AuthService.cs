using Domain.Interfaces;
using Domain.Models;
using Domain.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService(IAccountService _accountService, IConfiguration configuration) : IAuthService
    {
        const Int16 TOKEN_EXPIRATION = 1;
        public async Task<Response> LogIn(string email, string password)
        {

            var result = await _accountService.FindAccount(email);
            if (result != null && BCrypt.Net.BCrypt.Verify(password, result!.Password))
            {
                return Response.Create(message: this.CreateToken(result));
            }
            return Response.Create(statusCode: (int)HttpStatusCode.Unauthorized, message: "Error en el inicio de sesion: Datos incorrectos.");

        }

        public async Task<Response> Register(string email, string password)
        {
            if (!await _accountService.Exists(email))
            {
                string hashedPass = BCrypt.Net.BCrypt.HashPassword(password);
                Account account = new()
                {
                    Email = email,
                    Password = hashedPass
                };
                return Response.Create(statusCode: (int)HttpStatusCode.Created, await _accountService.Create(account));
            }
            return Response.Create(statusCode: (int)HttpStatusCode.Conflict, message: "Error en el registro: El email ya esta en uso.");
        }

        private string CreateToken(Account account)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
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
