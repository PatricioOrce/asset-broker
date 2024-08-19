using API.Models;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService) : ApiControllerBase
    {

        [HttpPost("/createAccount")]
        public async Task<Response> Register(AccountDto account) => await _authService.Register(account.Email, account.Password);

        [HttpPost("/login")]
        public async Task<Response> LogIn(AccountDto account) => await _authService.LogIn(account.Email, account.Password);
    }
}
