using Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
    public class ApiControllerBase : ControllerBase
    {
        private static readonly Dictionary<int, string> DefaultMessages = new Dictionary<int, string>
        {
            { 200, "La solicitud fue exitosa." },
            { 201, "Recurso creado con éxito." },
            { 204, "La solicitud fue exitosa, pero no hay contenido." },
            { 400, "Solicitud incorrecta. El servidor no pudo entender la solicitud." },
            { 401, "No autorizado. Se requiere autenticación o ha fallado." },
            { 403, "Prohibido. El servidor entendió la solicitud pero se niega a autorizarla." },
            { 404, "No encontrado. No se pudo encontrar el recurso solicitado." },
            { 500, "Error interno del servidor. Ocurrió un error en el servidor." }
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
