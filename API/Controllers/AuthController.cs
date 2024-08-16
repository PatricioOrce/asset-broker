using API.Models;
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
        public async Task<Response> Register(string email, string password) => await _authService.Register(email, password);

        [HttpGet("/login")]
        public async Task<Response> LogIn(string email, string password) => await _authService.LogIn(email, password);
    }
}
