using Application.UseCases.OdersOperation.Commands;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Mappers
{
    public static class OrderOperationMappers
    {
        public static CreateOrderDto ToCreateOrderDto(this CreateOrderCommandHandlerRequest orderRequest)
        {
            return new CreateOrderDto()
            {
                AccountId = int.Parse(orderRequest.AccountId),
                Amount = orderRequest.Amount,
                AssetId = orderRequest.AssetId,
                Operation = orderRequest.Operation
            };
        }
    }
}
