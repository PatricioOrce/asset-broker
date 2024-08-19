using API.Models;
using Application.UseCases.OdersOperation.Commands.Create;
using Application.UseCases.OdersOperation.Commands.Delete;
using Application.UseCases.OdersOperation.Commands.Update;
using Application.UseCases.OdersOperation.Queries.GetAll;
using Application.UseCases.OdersOperation.Queries.GetById;
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
    [Authorize]
    public class OrdersController(IMediator _mediator) : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandHandlerRequest request)
        {
            request.AccountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Send(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Send(await _mediator.Send(new GetAllOrdersQueryHandlerRequest()));
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            return Send(await _mediator.Send(new GetOrderByIdQueryHandlerRequest() { OrderId = orderId }));
        }
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderCommandHandlerRequest statusId)
        {
            return Send(await _mediator.Send(new UpdateOrderCommandHandlerRequest() { OrderId = orderId, StatusId = statusId.StatusId}));
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            return Send(await _mediator.Send(new DeleteOrderCommandHandlerRequest() { OrderId = orderId }));
        }
    }
}
