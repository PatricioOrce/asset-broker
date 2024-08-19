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

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var result = await _orderRepository.GetAllAsync();
            return result.Select(order => new OrderDto
            {
                OrderId = order.Id,
                AccountId = order.AccountId,
                AssetId = order.AssetId,
                Amount = order.Amount,
                Price = order.Price,
                Operation = order.Operation.FirstOrDefault(),
                StatusId = order.StatusId.GetValueOrDefault(),
                TotalAmount = order.TotalAmount ?? 0
            }).ToList();
        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            return order is not null ? new OrderDto
            {
                OrderId = order.Id,
                AccountId = order.AccountId,
                AssetId = order.AssetId,
                Amount = order.Amount,
                Price = order.Price,
                Operation = order.Operation.FirstOrDefault(),
                StatusId = order.StatusId.GetValueOrDefault(),
                TotalAmount = order.TotalAmount ?? 0
            } : null;
        }

        public Task UpdateOrderAsync(Order order)
        {
            return _orderRepository.UpdateAsync(order);
        }
    }
}
