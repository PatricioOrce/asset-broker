using Application.UseCases.OdersOperation.Commands.Create;
using Application.UseCases.OdersOperation.Queries.GetAll;
using Application.UseCases.OdersOperation.Queries.GetById;
using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.OdersOperation.Mappers
{
    public static class OrderOperationMappers
    {
        //No me gusta automapper
        public static CreateOrderDto ToCreateOrderDto(this CreateOrderCommandHandlerRequest orderRequest)
        {
            return new CreateOrderDto()
            {
                AccountId = int.Parse(orderRequest.AccountId),
                Amount = orderRequest.Amount,
                AssetId = orderRequest.AssetId,
                Operation = orderRequest.Operation,
                Price = orderRequest.Price
            };
        }
        public static List<GetAllOrdersQueryResponse> ToOrderDto(this List<OrderDto> orders)
        {
            return orders.Select(order => new GetAllOrdersQueryResponse()
            {
                AccountId = order.AccountId,
                Amount = order.Amount,
                AssetId = order.AssetId,
                Operation = order.Operation,
                Price = order.Price,
                OrderId = order.OrderId,
                StatusId = order.StatusId,
                TotalAmount = order.TotalAmount
            }).ToList();

        }
        public static GetOrderByIdQueryResponse ToSingleOrderDto(this OrderDto order)
        {
            return new GetOrderByIdQueryResponse()
            {
                AccountId = order.AccountId,
                Amount = order.Amount,
                AssetId = order.AssetId,
                Operation = order.Operation,
                Price = order.Price,
                OrderId = order.OrderId,
                StatusId = order.StatusId,
                TotalAmount = order.TotalAmount
            };
        }
    }
}
