using Domain.DTOs;
using Domain.Models;
using Domain.Utils;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Task<Response> CreateOrderAsync(CreateOrderDto asset);
        Task UpdateOrderAsync(Order asset);
        Task<OrderDto> GetOrderById(int orderId);
        Task<List<OrderDto>> GetAllOrdersAsync();
    }
}
