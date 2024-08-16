using Domain.Utils;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Response> Register(string email, string password);
        Task<Response> LogIn(string email, string password);
    }
}
