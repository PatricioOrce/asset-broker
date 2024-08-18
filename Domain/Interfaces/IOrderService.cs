using Domain.DTOs;
using Domain.Utils;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Task<Response> CreateOrderAsync(CreateOrderDto asset);
        Task<Response> UpdateOrderAsync(string assetId, CreateOrderDto asset);
        Task<Response<CreateOrderDto>> GetAllOrdersAsync();
    }
}
