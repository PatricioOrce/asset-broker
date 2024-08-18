using API.Models;
using Application.UseCases.OdersOperation.Commands;
using Domain.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController(IMediator _mediator) : ApiControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<Response<CreateOrderCommandResponse>> CreateOrder(CreateOrderCommandHandlerRequest request)
        {
            request.AccountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _mediator.Send(request);
        }
    }
}
