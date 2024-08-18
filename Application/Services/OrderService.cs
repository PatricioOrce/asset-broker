using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Domain.Utils;
using Infrastructure.Persistence.Contexts;
using System.Net;

namespace Application.Services
{
    public class OrderService(IOrderRepository _orderRepository) : IOrderService
    {
        public async Task<Response> CreateOrderAsync(CreateOrderDto order)
        {
            var mappedOrder = new Order
            {
                AccountId = order.AccountId,
                Amount = order.Amount,
                Operation = order.Operation.ToString(),
                Price = order.Price,
                StatusId = (int)OrderStatusesEnum.InProgress,
                AssetId = order.AssetId,
                TotalAmount = order.TotalAmount
            };
            await _orderRepository.AddAsync(mappedOrder);
            return Response.Create((int)HttpStatusCode.Created, "Orden creada con exito!");
        }

        public Task<Response<CreateOrderDto>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateOrderAsync(string assetId, CreateOrderDto order)
        {
            throw new NotImplementedException();
        }
    }
}
