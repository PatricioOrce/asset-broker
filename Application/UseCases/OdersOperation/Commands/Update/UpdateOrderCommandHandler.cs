using Application.UseCases.OdersOperation.Commands.Create;
using Domain.Enums;
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

namespace Application.UseCases.OdersOperation.Commands.Update
{
    public class UpdateOrderCommandHandlerRequest : IRequest<Response<UpdateOrderCommandResponse>>
    {
        [JsonIgnore]
        public int OrderId { get; set; }
        public int StatusId { get; set; }
    }
    public class UpdateOrderCommandHandler(IOrderRepository _orderRepository) : IRequestHandler<UpdateOrderCommandHandlerRequest, Response<UpdateOrderCommandResponse>>
    {
        public async Task<Response<UpdateOrderCommandResponse>> Handle(UpdateOrderCommandHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdateOrderCommandValidator();
                var validationResults = validator.Validate(request);
                if (!validationResults.IsValid) { 
                    return Response<UpdateOrderCommandResponse>.Create(statusCode: (int)HttpStatusCode.BadRequest, message: validationResults.Errors.ConvertToFormattedText()); 
                }
                var order = await _orderRepository.GetByIdAsync(request.OrderId);
                if(order == null)
                {
                    return Response<UpdateOrderCommandResponse>.Create(statusCode: (int)HttpStatusCode.NotFound);
                }
                if(order.StatusId != (int)OrderStatusesEnum.InProgress)
                {
                    return Response<UpdateOrderCommandResponse>.Create(statusCode: (int)HttpStatusCode.BadRequest, message: "No se puede modificar una orden Finalizada o Cancelada");
                }
                if (request.StatusId == (int)OrderStatusesEnum.InProgress)
                {
                    return Response<UpdateOrderCommandResponse>.Create(statusCode: (int)HttpStatusCode.BadRequest, message: "La orden ya se encuentra en progreso.");
                }
                order.StatusId = request.StatusId;
                await _orderRepository.UpdateAsync(order);
                return Response<UpdateOrderCommandResponse>.Create(message: $"Orden {(request.StatusId == (int)OrderStatusesEnum.Excecuted ? "finalizada" : "cancelada")} con exito");
            }
            catch (Exception ex)
            {
                return Response<UpdateOrderCommandResponse>.Create(statusCode: (int)HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }
    }
}
