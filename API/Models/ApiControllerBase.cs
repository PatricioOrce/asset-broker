using Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
    public class ApiControllerBase : ControllerBase
    {
        private static readonly Dictionary<int, string> DefaultMessages = new Dictionary<int, string>
    {
        { 200, "Request was successful." },
        { 201, "Resource created successfully." },
        { 204, "Request was successful, but there is no content." },
        { 400, "Bad request. The server could not understand the request." },
        { 401, "Unauthorized. Authentication is required or has failed." },
        { 403, "Forbidden. The server understood the request but refuses to authorize it." },
        { 404, "Not found. The requested resource could not be found." },
        { 500, "Internal server error. An error occurred on the server." }
    };

        protected IActionResult Send(Response response)
        {
            var message = string.IsNullOrWhiteSpace(response.Message)
                ? GetDefaultMessage(response.StatusCode)
                : response.Message;

            return StatusCode(response.StatusCode, new { message });
        }

        protected IActionResult Send<T>(Response<T> response)
        {
            var message = string.IsNullOrWhiteSpace(response.Message)
                ? GetDefaultMessage(response.StatusCode)
                : response.Message;

            if (response.Body == null)
            {
                return StatusCode(response.StatusCode, new { message });
            }

            return StatusCode(response.StatusCode, new { message, data = response.Body });
        }

        private string GetDefaultMessage(int statusCode)
        {
            return DefaultMessages.TryGetValue(statusCode, out var message)
                ? message
                : "An unexpected error occurred.";
        }
    }
}
