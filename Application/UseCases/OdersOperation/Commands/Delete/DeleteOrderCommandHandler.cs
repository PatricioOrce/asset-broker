using Application.UseCases.OdersOperation.Commands.Update;
using Domain.Interfaces;
using Domain.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Commands.Delete
{
    public class DeleteOrderCommandHandlerRequest : IRequest<Response<DeleteOrderCommandResponse>>
    {
        [JsonIgnore]
        public int OrderId { get; set; }
    }
    public class DeleteOrderCommandHandler(IOrderRepository _orderRepository) : IRequestHandler<DeleteOrderCommandHandlerRequest, Response<DeleteOrderCommandResponse>>
    {
        public async Task<Response<DeleteOrderCommandResponse>> Handle(DeleteOrderCommandHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(request.OrderId);
                if (order == null)
                {
                    return Response<DeleteOrderCommandResponse>.Create(statusCode: (int)HttpStatusCode.NotFound);
                }
                await _orderRepository.DeleteAsync(order);
                return Response<DeleteOrderCommandResponse>.Create(message: "Orden eliminada exitosamente.");

            }
            catch (Exception ex)
            {
                return Response<DeleteOrderCommandResponse>.Create(statusCode: (int)HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }
    }
}
